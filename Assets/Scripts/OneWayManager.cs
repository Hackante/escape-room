using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OneWayManager : MonoBehaviour
{
    [SerializeField] private int current = 0;
    [SerializeField] private Frage[] fragen;
    [SerializeField] private TextMeshPro frageText1;
    [SerializeField] private TextMeshPro frageText2;
    [SerializeField] private Transform bossRoomSpawn;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator crossFade;

    private void Start()
    {
        frageText1.text = fragen[current].text;
        frageText2.text = fragen[current + 1].text;
    }

    public bool? Next(string antwort)
    {
        if (antwort == fragen[current].antwort.ToString())
        {
            current++;
            if (!(current + 1 >= fragen.Length)) _ = current % 2 == 0 ? frageText2.text = fragen[current + 1].text : frageText1.text = fragen[current + 1].text;
            if (current == fragen.Length)
            {
                crossFade.SetTrigger("Start");
                playerTransform.position = bossRoomSpawn.position;
                crossFade.SetTrigger("End");
                SaveObject.Instance.SetValueOf("Tunnels", 2);
                return null;
            }
            return true;
        }
        else
        {
            current = 0;
            Start();
            return false;
        }
    }

    [System.Serializable]
    private class Frage
    {
        public String text;
        public Antwort antwort;

        public enum Antwort
        {
            Richtig,
            Falsch
        }
    }
}