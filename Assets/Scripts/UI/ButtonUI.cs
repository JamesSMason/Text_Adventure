using Adventure.Main;
using Adventure.SceneLoader;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Adventure.UI
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] Button buttonPrefab = null;
        StoryPresenter storyPresenter = null;

        void Awake()
        {
            storyPresenter = FindObjectOfType<StoryPresenter>();
        }

        void OnEnable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate += RefreshStoryButtons;
            }
        }

        void OnDisable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate -= RefreshStoryButtons;
            }
        }

        private void RefreshStoryButtons()
        {
            DestroyButtons();

            if (storyPresenter.GetIsEncounter())
            {
                PresentCombatButtons();
            }
            else
            {
                PresentStoryButtons();
            }
        }

        private void PresentCombatButtons()
        {
            Debug.Log("Combat Buttons Appear Here.");
            foreach (string childID in storyPresenter.GetAllChildrenIDs())
            {
                Button newButton = Instantiate(buttonPrefab, this.transform);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = storyPresenter.GetOptionText(childID);
                newButton.onClick.AddListener(() => storyPresenter.MoveToNextNode(childID));
            }
        }

        private void PresentStoryButtons()
        {
            int buttonCount = 0;
            foreach (string childID in storyPresenter.GetAllChildrenIDs())
            {
                Button newButton = Instantiate(buttonPrefab, this.transform);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = storyPresenter.GetOptionText(childID);
                newButton.onClick.AddListener(() => storyPresenter.MoveToNextNode(childID));
                buttonCount++;
            }

            if (buttonCount == 0)
            {
                Button newButton = Instantiate(buttonPrefab, this.transform);
                Button quitButton = Instantiate(buttonPrefab, this.transform);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Main Menu";
                quitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Quit Game";
                LoadScene loadScene = FindObjectOfType<LoadScene>();
                newButton.onClick.AddListener(() => loadScene.LoadMainMenu());
                quitButton.onClick.AddListener(() => loadScene.Quit());
            }
        }

        private void DestroyButtons()
        {
            foreach (Transform button in transform)
            {
                Destroy(button.gameObject);
            }
        }
    }
}