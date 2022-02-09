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

            foreach (string childID in storyPresenter.GetAllChildrenIDs())
            {
                Button newButton = Instantiate(buttonPrefab, this.transform);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = storyPresenter.GetOptionText(childID);
                newButton.onClick.AddListener(() => storyPresenter.MoveToNextNode(childID));
            }
        }

        private void DestroyButtons()
        {
            foreach (Button button in GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
    }
}