using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawners;
    // Start is called before the first frame update
    void Start()
    {
        SetSpawnLevel(GameManager.DifficultyLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawnLevel(int level)
    {
        if (level <= spawners.Length)
        {
            for (int i = 0; i < level; i++)
            {
                spawners[i].SetActive(true);
                Debug.Log("Spawner " + i + " Seteado");
            }
        }
    }
}

