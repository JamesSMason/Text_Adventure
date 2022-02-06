using Adventure.Main;
using UnityEngine;

namespace Adventure.UI
{
    public class DisplayStoryElements : MonoBehaviour
    {
        [SerializeField] GameObject headersUI = null;
        [SerializeField] GameObject combatUI = null;
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
            bool inCombat = storyPresenter.GetIsEncounter();
            headersUI.SetActive(inCombat);
            combatUI.SetActive(inCombat);
        }
    }
}