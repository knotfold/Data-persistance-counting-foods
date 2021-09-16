using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField inputPlayerName;
    [SerializeField] TextMeshProUGUI scores;

    private List<int> scoresList;
    private List<string> scoreList = new List<string>();

    private List<string> playerList = new List<string>();

    private MainManager.SaveData saveData;

    private string scoresToDisplay;

    void Start()
    {
        DisplayTopScores();
    }

    void DisplayTopScores()
    {   
        saveData = MainManager.Instance.LoadScores();

        string holder;
        for(int i=0; i<saveData.scores.Count ; i++)
        {
            Debug.Log(saveData.scores[i].ToString());
            holder = (i + 1).ToString() + ".-" + saveData.players[i] + " " + saveData.scores[i].ToString();
            scoreList.Add(holder);
        }

        scores.text = string.Join("\n", scoreList.ToArray());

    }

    public void ClearSaveButton()
    {

        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {

            File.Delete(Application.persistentDataPath + "/savefile.json");

           Exit();
        }
    }

    void Update()
    {
        // if(MainManager.Instance.character != null){
        //      MainManager.Instance.skinnedMeshRenderer = MainManager.Instance.character.GetComponent<SkinnedMeshRenderer>();
        // }
       
    }
    public void StartGame()
    {
        if(inputPlayerName.text != null){
            MainManager.Instance.playerName = inputPlayerName.text;
        } else{
            MainManager.Instance.playerName =" ";
        }
        
       
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
