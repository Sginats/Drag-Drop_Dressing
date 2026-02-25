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
            GameObject droppedObject = eventData.pointerDrag;

            if (droppedObject == null)
                return;

            DragItem dragItem = droppedObject.GetComponent<DragItem>();
            if (dragItem == null)
                return;

            ItemCategory itemCategory = droppedObject.GetComponent<ItemCategory>();
            if (itemCategory == null || itemCategory.category != slotCategory)
            {
                return;
            }

            EquipItem(droppedObject, dragItem);
        }

        /// <summary>
        /// Equips the item to this slot, replacing any existing item.
        /// </summary>
        /// <param name="item">Item GameObject to equip.</param>
        /// <param name="dragItem">DragItem component of the item.</param>
        private void EquipItem(GameObject item, DragItem dragItem)
        {
            if (currentEquippedItem != null)
            {
                DragItem previousDrag = currentEquippedItem.GetComponent<DragItem>();
                if (previousDrag != null)
                {
                    previousDrag.ReturnToOriginal();
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
