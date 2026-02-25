using UnityEngine;
using UnityEngine.EventSystems;

namespace TelaVeidotajs
{
    /// <summary>
    /// UI rakstura mērķis, kas pieņem DraggableItem ar IDropHandler.
    /// Pievieno šo skriptu pie CharacterImage (UI Image).
    /// </summary>
    public class CharacterDropTargetUI : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            var draggable = eventData.pointerDrag != null
                ? eventData.pointerDrag.GetComponent<DraggableItem>()
                : null;

            if (draggable == null)
                return;

            Debug.Log($"[CharacterDropTargetUI] Dropped {draggable.name} on UI character {gameObject.name}.");

            // Šeit var pielāgot loģiku, lai piemērotu priekšmetu tēlam,
            // piemēram, atjauninot aprīkojumu vai vizuālo izskatu.
        }
    }
}

