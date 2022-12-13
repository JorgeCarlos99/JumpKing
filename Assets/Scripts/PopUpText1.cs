using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText1 : MonoBehaviour
{
    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    public TextMeshProUGUI texto;

    // Start is called before the first frame update
    void Start()
    {
        fullText = "hola que tal dios que guapo esto.";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        for (int i = 0; i < fullText.Length; i++) {
            currentText = fullText.Substring(0, i);
            texto.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
