using UnityEngine;
using UnityEngine.UI;

namespace TelaVeidotajs
{
    /// <summary>
    /// Controls character resize via width and height sliders.
    /// </summary>
    public class ResizeController : MonoBehaviour
    {
        [SerializeField] private RectTransform characterTransform;
        [SerializeField] private Slider widthSlider;
        [SerializeField] private Slider heightSlider;

        private void Start()
        {
            if (widthSlider != null)
            {
                widthSlider.onValueChanged.AddListener(OnWidthChanged);
            }

            if (heightSlider != null)
            {
                heightSlider.onValueChanged.AddListener(OnHeightChanged);
            }
        }

        /// <summary>
        /// Updates character width based on slider value.
        /// </summary>
        /// <param name="value">Slider value for width scale.</param>
        public void OnWidthChanged(float value)
        {
            Debug.Log($"[ResizeController] OnWidthChanged: {value}");
            
            if (characterTransform != null)
            {
                Vector3 scale = characterTransform.localScale;
                scale.x = value;
                characterTransform.localScale = scale;
            }
        }

        /// <summary>
        /// Updates character height based on slider value.
        /// </summary>
        /// <param name="value">Slider value for height scale.</param>
        public void OnHeightChanged(float value)
        {
            Debug.Log($"[ResizeController] OnHeightChanged: {value}");
            
            if (characterTransform != null)
            {
                Vector3 scale = characterTransform.localScale;
                scale.y = value;
                characterTransform.localScale = scale;
            }
        }
    }
}
