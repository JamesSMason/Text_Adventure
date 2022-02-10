using Adventure.Main;
using TMPro;
using UnityEngine;

namespace Adventure.UI
{
    public class StoryUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI storyTextPanel = null;
        StoryPresenter storyPresenter = null;

        void Awake()
        {
            storyPresenter = FindObjectOfType<StoryPresenter>();
        }

        private void OnEnable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate += RefreshUI;
            }
        }

        private void OnDisable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate -= RefreshUI;
            }
        }

        private void RefreshUI()
        {
            storyTextPanel.text = storyPresenter.GetStoryText();
        }
    }
}