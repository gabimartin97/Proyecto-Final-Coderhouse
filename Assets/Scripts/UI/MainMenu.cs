using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider spawnLevelSlider;
     
    public void OnClickPlay()
    {
       SceneManager.LoadScene("MainScene");
    }

    public void OnClickSpawnLevel()
    {
       
        GameManager.DifficultyLevel = (int)spawnLevelSlider.value;
    }
}
