using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// Стартовий екран: назва гри й мета. З'являється один раз за запуск, гра стоїть, поки гравець не почне.
public class StartScreen : MonoBehaviour
{
    public UIDocument uiDocument;
    public Font font;
    public string title = "Robot Repair";
    public string goal = "Полагодь усіх зламаних сторожових, кидаючи в них деталі клавішею C.";
    public string hint = "Натисни будь-яку клавішу, щоб почати";

    static bool shown;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetShown()
    {
        shown = false;
    }
    VisualElement panel;

    void Start()
    {
        if (shown)
        {
            return;
        }
        shown = true;

        panel = new VisualElement();
        panel.style.position = Position.Absolute;
        panel.style.left = 0;
        panel.style.right = 0;
        panel.style.top = 0;
        panel.style.bottom = 0;
        panel.style.backgroundColor = new Color(0.08f, 0.1f, 0.14f, 0.92f);
        panel.style.justifyContent = Justify.Center;
        panel.style.alignItems = Align.Center;
        panel.Add(MakeLabel(title, 96, Color.white));
        panel.Add(MakeLabel(goal, 36, new Color(0.9f, 0.9f, 0.9f)));
        panel.Add(MakeLabel(hint, 30, new Color(0.94f, 0.66f, 0.09f)));
        uiDocument.rootVisualElement.Add(panel);
        Time.timeScale = 0f;
    }

    Label MakeLabel(string text, int size, Color color)
    {
        Label l = new Label(text);
        l.style.fontSize = size;
        l.style.color = color;
        l.style.unityFontStyleAndWeight = FontStyle.Bold;
        l.style.unityTextAlign = TextAnchor.MiddleCenter;
        l.style.whiteSpace = WhiteSpace.Normal;
        l.style.marginBottom = 30;
        l.style.maxWidth = 1400;
        if (font != null)
        {
            l.style.unityFontDefinition = FontDefinition.FromFont(font);
        }
        return l;
    }

    void Update()
    {
        if (panel == null)
        {
            return;
        }
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame
            || Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            panel.RemoveFromHierarchy();
            panel = null;
            Time.timeScale = 1f;
        }
    }
}
