using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenuScene : MonoBehaviour
{
    public static UIMenuScene Instance;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TMP_InputField nameInput;
    public string playerName;
    public string bestPlayerNameAndScore;
    private MainManager mainManager;

   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadNameValueChange();
    }
    
    
    void Start()
    {
        if (bestPlayerNameAndScore != "")
        {
            Debug.Log("Load name");
            LoadName();
            bestScoreText.text = bestPlayerNameAndScore;
        }
    }

    public void FindGameManager()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        if (mainManager != null)
        { 
            bestPlayerNameAndScore = mainManager.BestScoreText();
            SaveName();
        }
    }

    public void StartNew()
    {
        playerName = nameInput.text;
        SaveName();
        SceneManager.LoadScene(1);
    }
    void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    void LoadNameValueChange()
    {
        LoadName();
        nameInput.text = playerName;
        bestScoreText.text = mainManager.BestScoreText();
    }
    void LoadBestScore()
    {
        bestScoreText.text = "Best Score : " + playerName;
    }
    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string bestPlayerNameAndScore;
    }
    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.bestPlayerNameAndScore = bestPlayerNameAndScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savejson.json", json);
    }
    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savejson.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            bestPlayerNameAndScore = data.bestPlayerNameAndScore;
        }
    }
}
    /*
    // Update is called once per frame
    void Update()
    {
        if (mainManager.m_GameOver) {
            if (nameInput != "" && mainManager.m_Points > highestScore) {
                highestScore = mainManager.m_Points;
                playerNameText.text = "Best Score : " + nameInput + " : " + highestScore;
            }
        }
    }
    */
    /*
    public void ReadStringInput(string s) 
    {
        nameInput = s;
    }
    
    
    public void LoadMain() 
    {
        SceneManager.LoadScene(1);
    }
    */
    

