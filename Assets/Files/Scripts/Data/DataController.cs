using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TextMeshProUGUI textCash;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textTarget;
    [SerializeField] private TextMeshPro[] textBonus;
    [SerializeField] private Button buttonLevelUp;
    [SerializeField] private TextMeshProUGUI textButtonLevelUp;
    [SerializeField] private Material playerMaterial;

    [Header("Scripts")]
    [SerializeField] private LevelManager levelManager;

    [Header("Display")]
    public float currentCash;
    public int currentTargetValue;
    public int level;
    public int capacity;
    public float bonus;
    public float costToNextLvl;
    public Color color;

    private bool isLevelMaxed;

    private void OnEnable()
    {
        buttonLevelUp.onClick.AddListener(OnButtonLevelUpClick);
    }

    private void Start()
    {
        level = 1;
        currentCash = 0;
        currentTargetValue = 0;
        LoadLevelData(level);
    }

    private void LoadLevelData(int level)
    {
        var levelData = levelManager.GetLevelData(level);
        var nextLevelData = levelManager.GetLevelData(level + 1);

        capacity = levelData.capacity;
        bonus = levelData.bonus;
        color = levelData.color;

        if (nextLevelData != null)
            costToNextLvl = nextLevelData.cost;
        else
            isLevelMaxed = true;

        UpdateCash();
        UpdateTarget();
        UpdateLevel();
        UpdateBonus();
    }

    public void UpdateCash()
    {
        //currentCash += value;
        textCash.text = currentCash.ToString(CultureInfo.InvariantCulture);

        UpdateLevelUpButton();
    }

    public void UpdateTarget()
    {
        textTarget.text = currentTargetValue + "/" + capacity;
    }

    public void UpdateLevel()
    {
        textLevel.text = level.ToString();
        playerMaterial.color = color;
    }

    private void UpdateBonus()
    {
        foreach (TextMeshPro text in textBonus)
            text.text = "x" + bonus.ToString(CultureInfo.InvariantCulture);
    }

    private void UpdateLevelUpButton()
    {
        if (currentCash >= costToNextLvl && !isLevelMaxed)
        {
            buttonLevelUp.gameObject.SetActive(true);
            textButtonLevelUp.text = costToNextLvl.ToString(CultureInfo.InvariantCulture) + " Cash";
        }
        else
            buttonLevelUp.gameObject.SetActive(false);
    }

    private void OnButtonLevelUpClick()
    {
        level += 1;
        currentCash -= costToNextLvl;
        LoadLevelData(level);
        UpdateLevelUpButton();
    }
}
