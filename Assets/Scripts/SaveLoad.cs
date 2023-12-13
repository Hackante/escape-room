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
        if (SaveObject.Instance == null)
        {
            new GameObject("SaveObject").AddComponent<SaveObject>();
            this.saveObject = SaveObject.Instance;
        }
        else
        {
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
            case "Evil -Village":
                if (saveObject.evilVillagePosition != Vector3.zero) GameObject.Find("Player").transform.position = saveObject.evilVillagePosition;
                LoadEvilVillage();
                saveObject.CurrentScene = 5;
                break;
            case "Katakomben":
                if (saveObject.katakombenPosition != Vector3.zero) GameObject.Find("Player").transform.position = saveObject.katakombenPosition;
                LoadKatakomben();
                saveObject.CurrentScene = 6;
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
            case "Evil -Village":
                saveObject.evilVillagePosition = coordinates;
                break;
            case "Katakomben":
                saveObject.katakombenPosition = coordinates;
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
            case "Evil -Village":
                saveObject.evilVillagePosition = GameObject.Find("Player").transform.position;
                break;
            case "Katakomben":
                saveObject.katakombenPosition = GameObject.Find("Player").transform.position;
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

    private void LoadEvilVillage()
    {
        // Path
        if (saveObject.PathToEvilVillage == 2)
        {
            GameObject.Find("Quest - Path").GetComponent<Solve>().SolveQuest();
        }
        // Video Tape
        if (saveObject.VideoTape == 2)
        {
            GameObject.Find("Quest - Tape").GetComponent<Solve>().SolveQuest();
        }
    }

    private void LoadKatakomben()
    {
        if (saveObject.VideoTape == 2)
        {
            GameObject.Find("Task - Key").SetActive(false);
        } else {
            GameObject.Find("HelperElf").SetActive(false);
        }
        // Key
        if (saveObject.Key == 2)
        {
            GameObject.Find("HelperElf").SetActive(false);
            GameObject.Find("Quest - Key").GetComponent<Solve>().SolveQuest();
        }
        // First Door
        if (saveObject.FirstDoor == 2)
        {
            GameObject.Find("Quest - LockedDoor").GetComponent<Solve>().SolveQuest();
        }
        // Second Door
        if (saveObject.SecondDoor == 2)
        {
            GameObject.Find("Quest - Code").GetComponent<Solve>().SolveQuest();
        }
        // Tunnels
        if (saveObject.Tunnels == 2)
        {
            GameObject.Find("Quest - Tunnels").GetComponent<Solve>().SolveQuest();
        }
    }

    public void CompleteQuest(QuestObject quest)
    {
        if (quest == null)
        {
            Debug.LogError("Quest is null");
            return;
        }
        saveObject.SetValueOf(quest.saveObjectQuestName.ToString(), 2);
    }

    public void PlayButton()
    {
        gameObject.GetComponent<teleport>().SetSceneName(saveObject.CurrentScene < 0 ? 8 : saveObject.CurrentScene);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        // reset saveObject
        Destroy(SaveObject.Instance.gameObject);
        new GameObject("SaveObject").AddComponent<SaveObject>();
        DontDestroyOnLoad(SaveObject.Instance.gameObject);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetScene() {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Elfendorf":
                saveObject.elfendorfPosition = Vector3.zero;
                saveObject.TaskBrokenBridge = 0;
                saveObject.TaskBoatOpened = 0;
                break;
            case "Ocean":
                saveObject.ozeanPosition = Vector3.zero;
                saveObject.TaskHoleFixed = 0;
                break;
            case "Evil -Village":
                saveObject.evilVillagePosition = Vector3.zero;
                saveObject.PathToEvilVillage = 0;
                saveObject.VideoTape = 0;
                break;
            case "Katakomben":
                saveObject.katakombenPosition = Vector3.zero;
                saveObject.Key = 0;
                saveObject.FirstDoor = 0;
                saveObject.SecondDoor = 0;
                saveObject.Tunnels = 0;
                break;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}