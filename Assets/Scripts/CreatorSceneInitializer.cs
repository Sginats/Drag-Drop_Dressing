using UnityEngine;

namespace TelaVeidotajs
{
    /// <summary>
    /// Initializes Creator scene by starting background music.
    /// </summary>
    public class CreatorSceneInitializer : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance?.PlayBackgroundMusic();
        }
    }
}
