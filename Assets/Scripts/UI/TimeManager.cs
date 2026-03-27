using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float startTime = 15f;
    [SerializeField] float resetTimer = 5f; //time to end the gameover screen

    [SerializeField] PlayerController playerCont;
   
    [SerializeField] TMP_Text tmp;
    [SerializeField] GameObject gameOver;

     private bool isOver = false;

    private float timeLeft = 0f;

    public bool IsOver=>isOver;
    
 
    // Update is called once per frame
    void Start()
    {
        Time.timeScale = 1f;
        timeLeft = startTime;
    }
    void Update()
    {
        decreaseTime();
       
    }

    IEnumerator GameOver()
    {

        isOver = true;

        playerCont.enabled=false;

        gameOver.SetActive(true);
        Time.timeScale = .1f;//Slows down game while

        yield return new WaitForSeconds(resetTimer * Time.timeScale);//set resttimer to be in match with the slowdown

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void increaseTime(float increase)
    {
        timeLeft += increase;
    }

    private void decreaseTime()
    {
        if(isOver) return;

        timeLeft -= Time.deltaTime;

        tmp.text = "00:00:"+timeLeft.ToString("F1");

        if (timeLeft <= 0) StartCoroutine(GameOver());
    }

    
}
