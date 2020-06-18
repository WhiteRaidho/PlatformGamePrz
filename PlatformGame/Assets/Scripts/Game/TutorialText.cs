using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public TMP_Text textObject;
    public string text;
    private bool showed = false;

    private void Start()
    {
        text = textObject.text;
        textObject.SetText("");
    }

    private void OnTriggerEnter2D()
    {
        if (!showed) {
            textObject.gameObject.SetActive(true);
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        showed = true;
        for (int i = 0; i <= text.Length; i++)
        {
            textObject.SetText(text.Substring(0, i));
            yield return new WaitForSeconds(0.03f);
        }
        showed = false;
        Destroy(this);
    }
}
