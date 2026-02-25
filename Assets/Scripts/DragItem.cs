using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TelaVeidotajs
{
    /// <summary>
    /// Handles drag logic for equipment items.
    /// </summary>
    public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Canvas canvas;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector2 originalPosition;
        private Transform originalParent;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            canvas = GetComponentInParent<Canvas>();
        }

        /// <summary>
        /// Called when drag begins. Stores original position and parent.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            originalPosition = rectTransform.anchoredPosition;
            originalParent = transform.parent;

            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;

            transform.SetParent(canvas.transform);
        }

        /// <summary>
        /// Called during drag. Updates item position to follow cursor.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnDrag(PointerEventData eventData)
        {
            if (canvas != null)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }

        /// <summary>
        /// Called when drag ends. Returns to original position if not dropped on valid slot.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            if (transform.parent == canvas.transform)
            {
                transform.SetParent(originalParent);
                rectTransform.anchoredPosition = originalPosition;
            }
        }

        /// <summary>
        /// Returns item to its original parent and position.
        /// </summary>
        public void ReturnToOriginal()
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
