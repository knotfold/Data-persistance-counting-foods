using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Specialized;
using System.Linq;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject inGameScreen;

    [SerializeField] GameObject player;
    SkinnedMeshRenderer playerMesh;

    [SerializeField] GameObject restartScreen;
    [SerializeField] TextMeshProUGUI finalScoreText;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI bananasTotalText;
    [SerializeField] TextMeshProUGUI cookiesTotalText;

    [SerializeField] GameObject newHighScoreText;
    [SerializeField] TextMeshProUGUI pizzasTotalText;

    [SerializeField] AudioClip gameFinishSound;

    private AudioSource gameManagerAudio;

    [SerializeField] Animator animator;


    public List<GameObject> foods;

    public List<GameObject> characters;

    private List<string> playersListBackUp = new List<string> {};

    private List<int> scoresListBackUp = new List<int> {};

    public bool isGameActive = false;
    public bool fruitActive = false;

    public GameObject currentFood;

    private int timer = 40;
    public int score = 0;
    public int bananasTotal = 0;
    public int cookiesTotal = 0;
    public int pizzasTotal = 0;
    // Start is called before the first frame update
    void Start()
    {
        changeMesh();
        gameManagerAudio = gameObject.GetComponent<AudioSource>();
        StartGame();

    }

    private void changeMesh()
    {
        if (MainManager.Instance.character != 5)
        {
            Debug.Log("mesh! isso");
            GameObject newPlayer = Instantiate(characters[MainManager.Instance.character]);
            newPlayer.transform.position = player.transform.position;
            newPlayer.transform.rotation = player.transform.rotation;
            animator = newPlayer.GetComponent<Animator>();
            Destroy(player);

        }
        else
        {
            Debug.Log("no mesh");
        }
    }

    private void Awake()
    {

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

    private void CreateBackUpLists(MainManager.SaveData saveData){
        foreach (string player in saveData.players ){
            playersListBackUp.Add(player);
        }

        foreach(int score in saveData.scores){
            scoresListBackUp.Add(score);
        }
    }

    private void CheckHighScore()
    {
        MainManager.SaveData saveData = MainManager.Instance.LoadScores();
        CreateBackUpLists(saveData);


        for (var i = 0; i < saveData.scores.Count; i++)
        {
            if (score >= saveData.scores.ElementAt(i))
            {

                if (i != saveData.scores.Count - 1)
                {

                    DataReplacement(saveData, i, MainManager.Instance.playerName, score);
                    for (var j = i; j < saveData.scores.Count - 1; j++)
                    {
                        string playerkeep = playersListBackUp[j];
                        int scorekeep = scoresListBackUp[j];

                        DataReplacement(saveData, j + 1, playerkeep, scorekeep);

                    }
                    UpdateLists(saveData);

                }
                else
                {
                    DataReplacement(saveData, i, MainManager.Instance.name, score);
                    UpdateLists(saveData);

                }

                break;
            }
            else
            {
                Debug.Log("nope");
            }


        }
    }

    public void UpdateLists(MainManager.SaveData saveData)
    {
        MainManager.Instance.scoresList = saveData.scores;
        MainManager.Instance.playersList = saveData.players;
        MainManager.Instance.SaveScore();
        newHighScoreText.SetActive(true);
    }

    public void DataReplacement(MainManager.SaveData saveData, int i, string name, int scorekeep)
    {
        saveData.scores[i] = scorekeep;
        saveData.players[i] = name;

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
                CheckHighScore();
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

    public void wrongBoxResult()
    {
        animator.SetTrigger("wrong");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {

        inGameScreen.SetActive(true);
        isGameActive = true;
        timeText.text = "TIME: " + timer;
        scoreText.text = "SCORE: " + score;
        bananasTotalText.text = bananasTotal.ToString();
        cookiesTotalText.text = cookiesTotal.ToString();
        pizzasTotalText.text = pizzasTotal.ToString();
        StartCoroutine(CountDownTimer());
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
