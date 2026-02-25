using UnityEngine;
using TMPro;

namespace TelaVeidotajs
{
    /// <summary>
    /// Validates birth year input and calculates age.
    /// </summary>
    public class AgeCalculator : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private TMP_InputField birthYearInput;
        [SerializeField] private TMP_Text ageResultText;

        private const int MinYear = 1900;

        /// <summary>
        /// Calculates age from birth year input with validation.
        /// </summary>
        public void CalculateAge()
        {
            AudioManager.Instance?.PlayClickSound();

            if (birthYearInput == null || ageResultText == null)
                return;

            string birthYearString = birthYearInput.text;

            if (!int.TryParse(birthYearString, out int birthYear))
            {
                ageResultText.text = "Kļūda: Dzimšanas gads nav derīgs skaitlis.";
                return;
            }

            int currentYear = System.DateTime.Now.Year;

            if (birthYear < MinYear || birthYear > currentYear)
            {
                ageResultText.text = $"Kļūda: Dzimšanas gads jābūt starp {MinYear} un {currentYear}.";
                return;
            }

            int age = currentYear - birthYear;
            string characterName = string.IsNullOrEmpty(nameInput?.text) ? "Tēls" : nameInput.text;

            ageResultText.text = $"{characterName} ir {age} gadi.";
        }
    }
}
