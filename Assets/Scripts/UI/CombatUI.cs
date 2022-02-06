using Adventure.Main;
using UnityEngine;

namespace Adventure.UI
{
    public class CombatUI : MonoBehaviour
    {
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
            
        }
    }
}