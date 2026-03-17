using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float score;
    [SerializeField] TMP_Text tmp;
   
    public void SetScore(float points)
    {
        score += points;
        tmp.text = score.ToString();
    }

}
