using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snow
{
    public class ResultManager : MonoBehaviour
    {
        public Text[] score, perfect, good, miss, bell;
        void Start()
        {
            for (int i = 0; i < 2; i++)
            {
                score[i].text = NoteController.score[i].ToString("F2") + "%";
                perfect[i].text = NoteController.perfect[i].ToString();
                good[i].text = NoteController.good[i].ToString();
                miss[i].text = NoteController.miss[i].ToString();
                bell[i].text = NoteController.bell[i].ToString();
            }
        }
    }

}
