using UnityEngine;
using UnityEngine.UI;

public class Character2DReceiver : MonoBehaviour, IItemReceiver
{
    [Header("Character Parts")]
    public SpriteRenderer characterBody;
    public Image uiCharacterImage;

    [Header("Equipment Slots")]
    public Image headSlotUI;
    public Image chestSlotUI;
    public Image legSlotUI;
    public Image feetSlotUI;

    public void ApplyItem(string categoryID, Sprite itemSprite)
    {
        if (itemSprite == null)
            return;

        switch (categoryID)
        {
            case "Cepures":
                if (headSlotUI != null)
                {
                    headSlotUI.sprite = itemSprite;
                    headSlotUI.enabled = true;
                }
                break;

            case "Krekli":
                if (chestSlotUI != null)
                {
                    chestSlotUI.sprite = itemSprite;
                    chestSlotUI.enabled = true;
                }
                break;

            case "Bikses":
                if (legSlotUI != null)
                {
                    legSlotUI.sprite = itemSprite;
                    legSlotUI.enabled = true;
                }
                break;

            case "Apavi":
                if (feetSlotUI != null)
                {
                    feetSlotUI.sprite = itemSprite;
                    feetSlotUI.enabled = true;
                }
                break;
        }
    }
}
