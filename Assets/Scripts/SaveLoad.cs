using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad Instance { get; private set; }
    public SaveObject saveObject;

    public void Awake()
    {
        Instance = this;
        if(SaveObject.Instance == null) {
            new GameObject("SaveObject").AddComponent<SaveObject>();
            this.saveObject = SaveObject.Instance;
        } else {
            this.saveObject = SaveObject.Instance;
        }
    }

    public void Start()
    {
        Load();
    }

    public string Export()
    {
        string json = JsonUtility.ToJson(this.saveObject);
        PlayerPrefs.SetString("SaveObject", json);
        return json;
    }

    public void Import(string json)
    {
        this.saveObject = JsonUtility.FromJson<SaveObject>(json);
    }

    public void Load()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Elfendorf":
                if (saveObject.elfendorfPosition != Vector3.zero) GameObject.Find("Player").transform.position = saveObject.elfendorfPosition;
                LoadElfendorf();
                saveObject.CurrentScene = 3;
                break;
            case "Ocean":
                if (saveObject.ozeanPosition != Vector3.zero) GameObject.Find("Player").transform.position = saveObject.ozeanPosition;
                LoadOzean();
                saveObject.CurrentScene = 4;
                break;
        }
    }

    public void SetSpawnpoint(Vector3 coordinates)
    {
        if (GameObject.Find("Player").transform == null) return;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Elfendorf":
                saveObject.elfendorfPosition = coordinates;
                break;
            case "Ocean":
                saveObject.ozeanPosition = coordinates;
                break;
        }
    }

    public void SavePosition()
    {
        if (GameObject.Find("Player").transform == null) return;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Elfendorf":
                saveObject.elfendorfPosition = GameObject.Find("Player").transform.position;
                break;
            case "Ocean":
                saveObject.ozeanPosition = GameObject.Find("Player").transform.position;
                break;
        }
    }

    private void LoadElfendorf()
    {
        // Broken Bridge
        if (saveObject.TaskBrokenBridge == 2)
        {
            GameObject.Find("Quest - Bridge").GetComponent<Solve>().SolveQuest();
        }
        // Captain 1
        if (saveObject.TaskBoatOpened == 2)
        {
            GameObject.Find("Quest - Captain").GetComponent<Solve>().SolveQuest();
        }
    }

    private void LoadOzean()
    {
        // Hole
        if (saveObject.TaskHoleFixed == 2)
        {
            GameObject.Find("Quest - Hole").GetComponent<Solve>().SolveQuest();
        }
    }

    public void CompleteQuest(QuestObject quest) {
        if (quest == null) {
            Debug.LogError("Quest is null");
            return;
        }
        saveObject.SetValueOf(quest.saveObjectQuestName.ToString(), 2);
    }

    public void PlayButton() {
        gameObject.GetComponent<teleport>().SetSceneName(saveObject.CurrentScene < 0 ? 3 : saveObject.CurrentScene);
    }
}
