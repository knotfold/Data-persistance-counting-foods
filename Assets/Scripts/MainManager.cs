using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string playerName = " ";
    public int character = 5;

    public SkinnedMeshRenderer skinnedMeshRenderer;



    public List<int> scoresList = new List<int> { 80, 60, 50, 40, 10 };
    public List<string> playersList = new List<string> { "John Doe", "Adam Novak", "Robin Brown", "Rowan Cruise", "Joe Draker" };




    // Start is called before the first frame update
    void Start()
    {

    }

    void SortList(List<(string, int)> list)
    {
        list.Sort((e1, e2) =>
{
    return e2.Item2.CompareTo(e1.Item2);
});
    }




    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject);
    }





    [System.Serializable]
    public class SaveData
    {


        public List<int> scores;
        public List<string> players;


    }

    public void SelectCharacter(GameObject gameObject)
    {

    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.scores = scoresList;
        data.players = playersList;




        string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    public SaveData LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log("file exists");
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log(json);

            return data;
        }
        else
        {
            SaveData data = new SaveData();
            data.players = playersList;
            data.scores = scoresList;
            return data;
        }
    }
}
