using Adventure.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Adventure.SceneLoader
{
    public class LoadScene : MonoBehaviour
    {
        const string mainMenuScene = "Main Menu";
        const string gameOverScene = "Game Over";
        const string gameScene = "Game";
        const string introScene = "Introduction";

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(mainMenuScene);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(gameScene);
        }

        public void LoadIntroScene()
        {
            SceneManager.LoadScene(introScene);
        }

        public void LoadGameOverScene()
        {
            SceneManager.LoadScene(gameOverScene);
        }

        public void Quit()
        {
            Debug.Log("Quitting game");
            Application.Quit();
        }

        public void LoadSaveGame()
        {
            GetComponent<SavingWrapper>().ContinueGame();
        }
    }
}