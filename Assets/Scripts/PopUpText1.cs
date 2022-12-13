// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class PopUpText1 : MonoBehaviour
// {
//     public float delay = 0.1f;
//     private string fullText;
//     private string fullTextMin1;
//     private bool done = false;

//     private string currentText = "";
//     private string doneText;
//     public TextMeshProUGUI texto;

//     // Start is called before the first frame update
//     void Start()
//     {
//         fullText = "hola que tal dios que guapo esto.";
//         fullTextMin1 = "hola que tal dios que guapo esto";

//         StartCoroutine(ShowText());
//     }
//     void Update()
//     {
//         if (texto.text.Equals(fullTextMin1))
//         {
//             done = true;
//         }
//         else
//         {
//             done = false;
//         }
//     }

//     IEnumerator ShowText()
//     {
//         Debug.Log(" currentText " + currentText);
//         if (done)
//         {
//             Debug.Log(" currentText1 " + currentText);

//             currentText = "";
//         }
//         Debug.Log(" currentText1 " + currentText);
//         for (int i = 0; i < fullText.Length; i++)
//         {
//             currentText = fullText.Substring(0, i);
//             texto.text = currentText;
//             yield return new WaitForSeconds(delay);
//         }
//     }
// }
