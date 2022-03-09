using Adventure.Main;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Adventure.UI
{
    public class StoryUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI storyTextPanel = null;
        [SerializeField] RectTransform contentViewer = null;
        [SerializeField] Scrollbar scrollbar = null;

        StoryPresenter storyPresenter = null;
        CombatPresenter combatPresenter = null;

        void Awake()
        {
            storyPresenter = FindObjectOfType<StoryPresenter>();
            combatPresenter = FindObjectOfType<CombatPresenter>();
        }

        private void OnEnable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate += RefreshUI;
            }
            if (combatPresenter != null)
            {
                combatPresenter.OnCombatUpdate += RefreshCombatReportUI;
            }
        }

        private void OnDisable()
        {
            if (storyPresenter != null)
            {
                storyPresenter.OnStoryUpdate -= RefreshUI;
            }
            if (combatPresenter != null)
            {
                combatPresenter.OnCombatUpdate -= RefreshCombatReportUI;
            }
        }

        private void RefreshUI()
        {
            if (scrollbar.gameObject.activeInHierarchy)
            {
                contentViewer.pivot = new Vector2(0, 1);
                contentViewer.anchorMin = new Vector2(0, 1);
                contentViewer.anchorMax = new Vector2(1, 1);
            }
            storyTextPanel.text = storyPresenter.GetStoryText();
        }

        private void RefreshCombatReportUI()
        {
            if (scrollbar.gameObject.activeInHierarchy)
            {
                contentViewer.pivot = new Vector2(0, 0);
                contentViewer.anchorMin = new Vector2(0, 0);
                contentViewer.anchorMax = new Vector2(1, 0);
            }
            storyTextPanel.text += combatPresenter.GetCombatMessages();
        }
    }
}