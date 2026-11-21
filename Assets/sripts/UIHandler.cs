using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    // Єдиний екземпляр HUD: до нього звертаються з інших скриптів як UIHandler.instance
    public static UIHandler instance { get; private set; }

    public float displayTime = 4.0f;

    private VisualElement m_Healthbar;
    private VisualElement m_NonPlayerDialogue;
    private Label m_DialogueText;
    private float m_TimerDisplay;
    private VisualElement m_WinScreen;
    private VisualElement m_LoseScreen;

    private Label m_FixedLabel;
    private int m_Fixed;
    private int m_Total;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        m_Healthbar = root.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        m_NonPlayerDialogue = root.Q<VisualElement>("NPCDialogue");
        m_DialogueText = root.Q<Label>("DialogueText");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

        m_LoseScreen = root.Q<VisualElement>("LoseScreenContainer");
        m_WinScreen = root.Q<VisualElement>("WinScreenContainer");

        m_FixedLabel = root.Q<Label>("FixedCounterLabel");
        UpdateCounter();
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    public void DisplayDialogue(string text)
    {
        m_DialogueText.text = text;
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void DisplayWinScreen()
    {
        m_WinScreen.style.opacity = 1.0f;
    }

    public void DisplayLoseScreen()
    {
        m_LoseScreen.style.opacity = 1.0f;
    }

    // Скільки всього ворогів на рівні: викликає GameManager на старті
    public void SetEnemyTotal(int total)
    {
        m_Total = total;
        UpdateCounter();
    }

    // Викликає Fix() ворога після влучання снаряда
    public void EnemyFixed()
    {
        m_Fixed++;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if (m_FixedLabel != null)
        {
            m_FixedLabel.text = "Полагоджено " + m_Fixed + " / " + m_Total;
        }
    }
}
