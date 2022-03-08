using Adventure.Main;
using Adventure.SceneLoader;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Adventure.UI
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] Button buttonPrefab = null;
        StoryPresenter storyPresenter = null;
        CombatPresenter combatPresenter = null;

        void Awake()
        {
            storyPresenter = FindObjectOfType<StoryPresenter>();
            combatPresenter = FindObjectOfType<CombatPresenter>();
        }

        void OnEnable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate += RefreshStoryButtons;
            }
            if (combatPresenter != null)
            {
                combatPresenter.OnCombatUpdate += RefreshCombatButtons;
            }
        }

        void OnDisable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate -= RefreshStoryButtons;
            }
            if (combatPresenter != null)
            {
                combatPresenter.OnCombatUpdate -= RefreshCombatButtons;
            }
        }

        private void RefreshStoryButtons()
        {
            DestroyButtons();

            PresentStoryButtons();
        }

        private void RefreshCombatButtons()
        {
            DestroyButtons();

            PresentCombatButtons();
        }

        private void DestroyButtons()
        {
            foreach (Transform button in transform)
            {
                Destroy(button.gameObject);
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

        private void PresentCombatButtons()
        {
            List<string> options = combatPresenter.GetOptions();
            foreach (string option in options)
            {
                Button newButton = Instantiate(buttonPrefab, this.transform);

                if (option == "FightOn")
                {
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fight On";
                    newButton.onClick.AddListener(() => combatPresenter.FightOn());
                }
                if (option == "Escape")
                {
                    int childIndex = 0;
                    string[] childIDs = { "", "" };
                    foreach (string childID in storyPresenter.GetAllChildrenIDs())
                    {
                        childIDs[childIndex] = childID;
                        childIndex++;
                    }
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = storyPresenter.GetOptionText(childIDs[1]);
                    newButton.onClick.AddListener(() => storyPresenter.MoveToNextNode(childIDs[1]));
                }
                if (option == "Dead")
                {
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = "You Dead";
                    newButton.onClick.AddListener(() => combatPresenter.Death());
                }
                if (option == "Victory")
                {
                    int childIndex = 0;
                    string[] childIDs = { "", ""};
                    foreach (string childID in storyPresenter.GetAllChildrenIDs())
                    {
                        childIDs[childIndex] = childID;
                        childIndex++;
                    }
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = storyPresenter.GetOptionText(childIDs[0]);
                    newButton.onClick.AddListener(() => storyPresenter.MoveToNextNode(childIDs[0]));
                }
                if (option == "TYL")
                {
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Test Your Luck?";
                    newButton.onClick.AddListener(() => combatPresenter.TestYourLuck());
                }
                if (option == "SetTarget")
                {
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Set Target";
                    newButton.onClick.AddListener(() => combatPresenter.SetTarget());
                }
            }
        }
    }
}