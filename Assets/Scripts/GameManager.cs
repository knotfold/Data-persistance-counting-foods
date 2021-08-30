using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject inGameScreen;

    [SerializeField] GameObject restartScreen;
    [SerializeField] TextMeshProUGUI finalScoreText;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI bananasTotalText;
    [SerializeField] TextMeshProUGUI cookiesTotalText;
    [SerializeField] TextMeshProUGUI pizzasTotalText;

    [SerializeField] AudioClip gameFinishSound;

    private AudioSource gameManagerAudio;

    [SerializeField] Animator animator;


    public List<GameObject> foods;

    public bool isGameActive = false;
    public bool fruitActive = false;

    public GameObject currentFood;

    private int timer = 60;
    public int score = 0;
    public int bananasTotal = 0;
    public int cookiesTotal = 0;
    public int pizzasTotal = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFruit();
    }

    private void SpawnFruit()
    {
        if (!fruitActive && isGameActive)
        {
            fruitActive = true;
            int randomIndex = Random.Range(0, foods.Count);
            currentFood = Instantiate(foods[randomIndex], transform.position, foods[randomIndex].transform.rotation).gameObject;
        }
    }

    IEnumerator CountDownTimer()
    {

        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            timer--;
            timeText.text = "TIME: " + timer;
            if (timer == 0)
            {
                isGameActive = false;
                //add a restart screen
                gameManagerAudio.PlayOneShot(gameFinishSound, 1.0f);
                restartScreen.SetActive(true);
                finalScoreText.text = "FINAl SCORE: " + score;

            }
        }

    }

    public void AddScore()
    {
        animator.SetTrigger("right");
        score++;
        scoreText.text = "SCORE: " + score;
        switch (currentFood.tag)
        {
            case "Pizzas":
                pizzasTotal++;
                pizzasTotalText.text = pizzasTotal.ToString();
                break;

            case "Bananas":
                bananasTotal++;
                bananasTotalText.text = bananasTotal.ToString();

                break;
            case "Cookies":
                cookiesTotal++;
                cookiesTotalText.text = cookiesTotal.ToString();
                break;
        }
    }

    public void wrongBoxResult(){
        animator.SetTrigger("wrong");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        inGameScreen.SetActive(true);
        isGameActive = true;
        timeText.text = "TIME: " + timer;
        scoreText.text = "SCORE: " + score;
        bananasTotalText.text = bananasTotal.ToString();
        cookiesTotalText.text = cookiesTotal.ToString();
        pizzasTotalText.text = pizzasTotal.ToString();
        StartCoroutine(CountDownTimer());
    }
}
