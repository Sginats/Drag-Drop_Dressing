using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TelaVeidotajs
{
    /// <summary>
    /// Manages character selection via dropdown and updates sprite and description.
    /// </summary>
    public class CharacterSelector : MonoBehaviour
    {
        [System.Serializable]
        public class CharacterData
        {
            public string name;
            public Sprite sprite;
            public string description;
        }

        [SerializeField] private TMP_Dropdown characterDropdown;
        [SerializeField] private Image characterImage;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private CharacterData[] characters;

        private void Start()
        {
            if (characterDropdown != null)
            {
                characterDropdown.onValueChanged.AddListener(OnCharacterChanged);
                OnCharacterChanged(0);
            }
        }

        /// <summary>
        /// Updates character sprite and description when dropdown selection changes.
        /// </summary>
        /// <param name="index">Selected character index.</param>
        public void OnCharacterChanged(int index)
        {
            Debug.Log($"[CharacterSelector] OnCharacterChanged called with index: {index}");
            
            if (characters == null || index < 0 || index >= characters.Length)
                return;

            CharacterData selected = characters[index];
            Debug.Log($"[CharacterSelector] Selected character: {selected.name}");

            if (characterImage != null && selected.sprite != null)
            {
                characterImage.sprite = selected.sprite;
            }

            if (descriptionText != null)
            {
                descriptionText.text = selected.description;
            }
        }
    }
}
