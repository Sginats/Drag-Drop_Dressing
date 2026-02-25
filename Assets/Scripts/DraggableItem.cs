using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Item Settings")]
    public string categoryID;
    public Sprite itemSprite;

    [Header("2D Drop Detection")]
    public LayerMask characterLayerMask = -1;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Transform startParent;
    private Canvas mainCanvas;
    private Camera worldCamera;

    private GameObject dragGhost;
    private RectTransform dragGhostRect;
    private CanvasGroup dragGhostCanvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        mainCanvas = GetComponentInParent<Canvas>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        if (itemSprite == null)
        {
            Image img = GetComponent<Image>();
            if (img != null)
            {
                itemSprite = img.sprite;
            }
        }

        startPosition = rectTransform.localPosition;
        startParent = transform.parent;

        worldCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        if (mainCanvas == null)
        {
            mainCanvas = GetComponentInParent<Canvas>();
        }

        if (itemSprite != null && mainCanvas != null)
        {
            dragGhost = new GameObject("DragGhost");
            dragGhostRect = dragGhost.AddComponent<RectTransform>();
            
            Image ghostImage = dragGhost.AddComponent<Image>();
            ghostImage.sprite = itemSprite;
            ghostImage.raycastTarget = false;
            ghostImage.preserveAspect = true;

            dragGhostCanvasGroup = dragGhost.AddComponent<CanvasGroup>();
            dragGhostCanvasGroup.blocksRaycasts = false;
            dragGhostCanvasGroup.interactable = false;

            dragGhost.transform.SetParent(mainCanvas.transform, false);
            dragGhostRect.sizeDelta = rectTransform.rect.size;

            RectTransform canvasRect = mainCanvas.transform as RectTransform;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                eventData.position,
                mainCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCanvas.worldCamera,
                out localPoint
            );
            dragGhostRect.anchoredPosition = localPoint;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        if (mainCanvas == null || dragGhostRect == null)
            return;

        RectTransform canvasRect = mainCanvas.transform as RectTransform;
        if (canvasRect == null)
            return;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            mainCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCanvas.worldCamera,
            out localPoint
        );
        dragGhostRect.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragGhost != null)
        {
            Destroy(dragGhost);
            dragGhost = null;
            dragGhostRect = null;
            dragGhostCanvasGroup = null;
        }

        bool dropped = TryDropOn2DCharacter(eventData);

        if (!dropped)
        {
            ReturnToCloset();
        }
    }

    public void ReturnToCloset()
    {
        if (rectTransform == null)
            return;

        transform.SetParent(startParent);
        rectTransform.localPosition = startPosition;
    }

    private bool TryDropOn2DCharacter(PointerEventData eventData)
    {
        if (worldCamera == null)
        {
            worldCamera = Camera.main;
        }

        if (worldCamera == null)
            return false;

        Vector3 worldPos = worldCamera.ScreenToWorldPoint(new Vector3(
            eventData.position.x,
            eventData.position.y,
            worldCamera.nearClipPlane
        ));

        Collider2D hit = Physics2D.OverlapPoint(new Vector2(worldPos.x, worldPos.y), characterLayerMask);

        if (hit != null)
        {
            IItemReceiver receiver = hit.GetComponentInParent<IItemReceiver>();
            if (receiver != null)
            {
                receiver.ApplyItem(categoryID, itemSprite);
                return true;
            }
        }

        return false;
    }
}