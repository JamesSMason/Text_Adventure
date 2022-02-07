using Adventure.Attributes;
using Adventure.Main;
using TMPro;
using UnityEngine;

public class AdventureSheetUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI initSkillValueText = null;
    [SerializeField] TextMeshProUGUI skillValueText = null;
    [SerializeField] TextMeshProUGUI initStaminaValueText = null;
    [SerializeField] TextMeshProUGUI staminaValueText = null;
    [SerializeField] TextMeshProUGUI initLuckValueText = null;
    [SerializeField] TextMeshProUGUI luckValueText = null;
    [SerializeField] TextMeshProUGUI goldValueText = null;
    [SerializeField] TextMeshProUGUI provisionsValueText = null;
    [SerializeField] TextMeshProUGUI jewelleryValueText = null;
    PlayerPresenter playerPresenter = null;

    void Awake()
    {
        playerPresenter = FindObjectOfType<PlayerPresenter>();
    }

    void OnEnable()
    {
        if (playerPresenter != null)
        {
            playerPresenter.OnStatisticChange += RefreshUI;
        }
    }

    void OnDisable()
    {
        if (playerPresenter != null)
        {
            playerPresenter.OnStatisticChange -= RefreshUI;
        }
    }

    private void RefreshUI()
    {
        initSkillValueText.text = playerPresenter.GetStatAsString(Stats.InitialSkill);
        skillValueText.text = playerPresenter.GetStatAsString(Stats.Skill);
        initStaminaValueText.text = playerPresenter.GetStatAsString(Stats.InitialStamina);
        staminaValueText.text = playerPresenter.GetStatAsString(Stats.Stamina);
        initLuckValueText.text = playerPresenter.GetStatAsString(Stats.InitialLuck);
        luckValueText.text = playerPresenter.GetStatAsString(Stats.Luck);
        goldValueText.text = playerPresenter.GetStatAsString(Stats.Gold);
        provisionsValueText.text = playerPresenter.GetStatAsString(Stats.Provisions);
        jewelleryValueText.text = playerPresenter.GetStatAsString(Stats.Jewellery);
    }
}