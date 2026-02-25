using UnityEngine;
using UnityEngine.UI;

namespace TelaVeidotajs
{
    /// <summary>
    /// Controls visibility of equipment category panel via toggle.
    /// </summary>
    public class ToggleCategory : MonoBehaviour
    {
        [SerializeField] private Toggle categoryToggle;
        [SerializeField] private GameObject itemsPanel;

        private void Start()
        {
            if (categoryToggle != null)
            {
                categoryToggle.onValueChanged.AddListener(OnToggleChanged);
                OnToggleChanged(categoryToggle.isOn);
            }
        }

        /// <summary>
        /// Shows or hides the items panel based on toggle state.
        /// </summary>
        /// <param name="isOn">Toggle state.</param>
        public void OnToggleChanged(bool isOn)
        {
            Debug.Log($"[ToggleCategory] {gameObject.name} OnToggleChanged: {isOn}");
            
            if (itemsPanel != null)
            {
                itemsPanel.SetActive(isOn);
                Debug.Log($"[ToggleCategory] Panel {itemsPanel.name} set active: {isOn}");
            }
        }
    }
}
