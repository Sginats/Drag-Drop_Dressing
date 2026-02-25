using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TelaVeidotajs
{
    /// <summary>
    /// Handles drop logic for equipment slots. Equips items and manages replacement.
    /// </summary>
    public class DropSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private string slotCategory;
        [SerializeField] private Image equippedItemDisplay;

        private GameObject currentEquippedItem;

        /// <summary>
        /// Called when an item is dropped on this slot. Equips if category matches.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"[DropSlot] OnDrop called on {gameObject.name}");
            
            GameObject droppedObject = eventData.pointerDrag;

            if (droppedObject == null)
                return;

            // Pārliecināmies, ka objekts ir velkams DraggableItem
            DraggableItem dragItem = droppedObject.GetComponent<DraggableItem>();
            if (dragItem == null)
                return;

            ItemCategory itemCategory = droppedObject.GetComponent<ItemCategory>();
            if (itemCategory == null || itemCategory.category != slotCategory)
            {
                Debug.Log($"[DropSlot] Category mismatch: {itemCategory?.category} != {slotCategory}");
                return;
            }

            Debug.Log($"[DropSlot] Equipping {droppedObject.name} to {gameObject.name}");
            EquipItem(droppedObject, dragItem);
        }

        /// <summary>
        /// Equips the item to this slot, replacing any existing item.
        /// </summary>
        /// <param name="item">Item GameObject to equip.</param>
        /// <param name="dragItem">DraggableItem component of the item.</param>
        private void EquipItem(GameObject item, DraggableItem dragItem)
        {
            if (currentEquippedItem != null)
            {
                DraggableItem previousDrag = currentEquippedItem.GetComponent<DraggableItem>();
                if (previousDrag != null)
                {
                    previousDrag.ReturnToCloset();
                }
            }

            currentEquippedItem = item;
            item.transform.SetParent(transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            if (equippedItemDisplay != null)
            {
                Image itemImage = item.GetComponent<Image>();
                if (itemImage != null)
                {
                    equippedItemDisplay.sprite = itemImage.sprite;
                    equippedItemDisplay.enabled = true;
                }
            }
        }
    }
}
