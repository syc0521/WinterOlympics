using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snow;
using TMPro;

public class ComboText : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    public Transform startPos, endPos;
    public GameObject P1, P2;
    void Update()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = NoteController.combo[i].ToString();
        }
        P1.transform.position = Vector3.Lerp(startPos.position, endPos.position, NoteController.score[0]);
        P2.transform.position = Vector3.Lerp(startPos.position, endPos.position, NoteController.score[1]);

    }
}
