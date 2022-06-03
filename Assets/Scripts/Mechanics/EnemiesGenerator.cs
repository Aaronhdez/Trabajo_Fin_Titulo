using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    public List<GameObject> enemiesAvailable;
    public Dictionary<string, List<GameObject>> enemiesCombinations;

    private void Start() {
        LoadEnemyCombinations();
    }

    private void LoadEnemyCombinations() {
        enemiesCombinations = new Dictionary<string, List<GameObject>>();
        enemiesCombinations.Add("1", LoadEasyMode());
        enemiesCombinations.Add("2", LoadMediumMode());
        enemiesCombinations.Add("3", LoadHardMode());
    }

    private List<GameObject> LoadHardMode() {
        var list = new List<GameObject>();
        list.Add(enemiesAvailable[0]);
        return list;
    }

    private List<GameObject> LoadMediumMode() {
        var list = new List<GameObject>();
        list.Add(enemiesAvailable[1]);
        list.Add(enemiesAvailable[2]);
        return list;
    }

    private List<GameObject> LoadEasyMode() {
        var list = new List<GameObject>();
        list.Add(enemiesAvailable[3]);
        list.Add(enemiesAvailable[4]);
        return list;
    }

    public List<GameObject> GetEnemiesList(string index) {
        Debug.Log(index);
        if (enemiesCombinations == null) {
            LoadEnemyCombinations();
        }
        return enemiesCombinations[index];
    }
}
