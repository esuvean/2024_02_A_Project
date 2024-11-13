using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextMananger : MonoBehaviour
{
    public static FloatingTextMananger instance;
    public GameObject textPrefabs;

    private void Awake()
    {
        instance = this;
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject textObj = Instantiate(textPrefabs, transform);
        textObj.transform.position = screenPos;

        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

        if (temp != null )
        {
            temp.text = text;
            StartCoroutine(AnimateText(textObj));
        }
    }

    private IEnumerator AnimateText(GameObject textObj)
    {
        float duration = 1f;
        float timer = 0f;

        Vector3 startPos = textObj.transform.position;
        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textObj.transform.position = startPos + Vector3.up * (progress * 50f);

            if (temp != null )
            {
                temp.alpha = 1 - progress;
            }
            yield return null;
        }

        Destroy( textObj );
    }
}
