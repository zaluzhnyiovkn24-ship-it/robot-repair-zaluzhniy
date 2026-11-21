using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public GameObject dialogueBubble;

    [TextArea]
    public string dialogueText;

    void Start()
    {
        dialogueBubble.SetActive(false);
    }
}
