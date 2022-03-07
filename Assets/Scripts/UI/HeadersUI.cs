using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeadersUI : MonoBehaviour
{
    [SerializeField] Image monsterIcon = null;
    [SerializeField] TextMeshProUGUI monsterName = null;
    [SerializeField] TextMeshProUGUI monsterSkill = null;
    [SerializeField] TextMeshProUGUI monsterStamina = null;

    public void SetMonsterIcon(Sprite icon)
    {
        monsterIcon.sprite = icon;
    }

    public void SetMonsterName(string name)
    {
        monsterName.text = name;
    }

    public void SetMonsterSkill(int skill)
    {
        monsterSkill.text = skill.ToString();
    }

    public void SetMonsterStamina(int stamina)
    {
        monsterStamina.text = stamina.ToString();
    }
}
