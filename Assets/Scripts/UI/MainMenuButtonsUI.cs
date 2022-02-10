using Adventure.Saving;
using Adventure.SceneLoader;
using UnityEngine;

namespace Adventure.UI
{
    public class MainMenuButtonsUI : MonoBehaviour
    {
        public void NewGame()
        {
            FindObjectOfType<LoadScene>().LoadGame();
        }

        public void ContinueGame()
        {
            FindObjectOfType<SavingWrapper>().ContinueGame();
        }

        public void LoadIntroduction()
        {
            FindObjectOfType<LoadScene>().LoadIntroScene();
        }

        public void QuitGame()
        {
            FindObjectOfType<LoadScene>().Quit();
        }
    }
}