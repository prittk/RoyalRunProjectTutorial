using NUnit.Framework.Interfaces;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float score;
    [SerializeField] TMP_Text tmp;
    [SerializeField] TimeManager tm;
   
    public void SetScore(float points)
    {
        if (tm.IsOver)  return;

        score += points;
        tmp.text = score.ToString();
    }

}
