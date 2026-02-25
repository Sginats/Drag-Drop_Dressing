using UnityEngine;
using UnityEngine.SceneManagement;

namespace TelaVeidotajs
{
    /// <summary>
    /// Handles scene navigation between Menu and Creator scenes.
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        /// <summary>
        /// Loads the Creator scene.
        /// </summary>
        public void LoadCreatorScene()
        {
            AudioManager.Instance?.PlayClickSound();
            SceneManager.LoadScene("Creator");
        }

        /// <summary>
        /// Loads the Menu scene.
        /// </summary>
        public void LoadMenuScene()
        {
            Debug.Log("[SceneController] LoadMenuScene CLICKED");
            AudioManager.Instance?.PlayClickSound();
            SceneManager.LoadScene("Menu");
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void ExitApplication()
        {
            AudioManager.Instance?.PlayClickSound();
            Application.Quit();
        }
    }
}
