using Adventure.Main;
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
                storyPresenter.OnStoryUpdate += RefreshUI;
            }
        }

        void OnDisable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate -= RefreshUI;
            }
        }

        private void RefreshUI()
        {
            foreach (Button button in GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }

            foreach (string childID in storyPresenter.GetAllChildren())
            {
                Button newButton = Instantiate(buttonPrefab, this.transform);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = storyPresenter.GetOptionText(childID);
                newButton.onClick.AddListener(() => storyPresenter.MoveToNextNode(childID));
            }
        }
    }
}