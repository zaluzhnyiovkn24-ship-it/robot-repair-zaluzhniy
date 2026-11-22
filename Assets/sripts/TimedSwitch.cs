using System.Collections;
using UnityEngine;

// Плита-перемикач: коли робот на неї ступає, двері відчиняються на openTime секунд.
public class TimedSwitch : MonoBehaviour
{
    public GameObject door;
    public float openTime = 4.0f;
    // галочка Active: вимкнена - двері відчинені завжди (рівень без твіста)
    public bool active = true;
    public SpriteRenderer plate;
    public Color idleColor = new Color(0.45f, 0.35f, 0.7f);
    public Color pressedColor = new Color(1f, 0.95f, 0.5f);

    void Start()
    {
        if (!active)
        {
            door.SetActive(false);
        }
        SetPlate(idleColor);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!active || other.GetComponent<PlayerController>() == null)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(OpenDoorFor(openTime));
    }

    IEnumerator OpenDoorFor(float seconds)
    {
        door.SetActive(false);
        SetPlate(pressedColor);
        float warn = Mathf.Min(1f, seconds * 0.25f);
        yield return new WaitForSeconds(seconds - warn);
        // за секунду до закриття плита блимає
        float t = 0f;
        while (t < warn)
        {
            SetPlate(Mathf.FloorToInt(t * 6f) % 2 == 0 ? idleColor : pressedColor);
            t += Time.deltaTime;
            yield return null;
        }
        door.SetActive(true);
        SetPlate(idleColor);
    }

    void SetPlate(Color c)
    {
        if (plate != null)
        {
            plate.color = c;
        }
    }
}
