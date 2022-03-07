using Adventure.Combat;
using Adventure.Main;
using UnityEngine;

namespace Adventure.UI
{
    public class CombatUI : MonoBehaviour
    {
        [SerializeField] HeadersUI headerPrefab = null;
        CombatPresenter combatPresenter = null;

        void Awake()
        {
            combatPresenter = FindObjectOfType<CombatPresenter>();
        }

        void OnEnable()
        {
            if (combatPresenter != null)
            {
                combatPresenter.OnCombatUpdate += RefreshUI;
            }
        }

        void OnDisable()
        {
            if (combatPresenter != null)
            {
                combatPresenter.OnCombatUpdate -= RefreshUI;
            }
        }

        private void RefreshUI()
        {
            foreach (Transform header in transform)
            {
                Destroy(header.gameObject);
            }

            foreach (Monster monster in combatPresenter.GetMonsters())
            {
                HeadersUI newHeader = Instantiate(headerPrefab, transform);
                newHeader.SetMonsterIcon(monster.GetMonsterImage());
                newHeader.SetMonsterName(monster.GetMonsterName());
                newHeader.SetMonsterSkill(monster.GetMonsterSkill());
                newHeader.SetMonsterStamina(monster.GetMonsterStamina());
            }
        }
    }
}