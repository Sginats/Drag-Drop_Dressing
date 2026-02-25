using UnityEngine;

namespace TelaVeidotajs
{
    /// <summary>
    /// 3D rakstura mērķis, kas pieņem DraggableItem, ja tas tiek nomests ar Physics raycast.
    /// Pievieno šo skriptu 3D Character GameObject ar Collider komponentu.
    /// </summary>
    public class CharacterDropTarget3D : MonoBehaviour
    {
        public void OnItemDropped(DraggableItem item)
        {
            if (item == null)
                return;

            Debug.Log($"[CharacterDropTarget3D] Dropped {item.name} on 3D character {name}.");

            // Šeit var pielāgot loģiku, lai piemērotu priekšmetu 3D tēlam.
        }
    }
}

