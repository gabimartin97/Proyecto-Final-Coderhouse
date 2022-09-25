using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject pointsIndicatorObject;
    [SerializeField] GameObject healthBarObject;
    [SerializeField] GameObject gameOverObject;
    TextMeshProUGUI pointsText;
    Slider healthBar;
    GameObject playerObject;
    PlayerBehaviour playerBehaviourScript;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerBehaviourScript = playerObject.GetComponent<PlayerBehaviour>();
        pointsText = pointsIndicatorObject.GetComponent<TextMeshProUGUI>();
        healthBar = healthBarObject.GetComponent<Slider>();
        healthBar.maxValue = playerBehaviourScript.Health;
    }   
    

    // Update is called once per frame
    void Update()
    {

        pointsText.SetText(GameManager.Score.ToString());
        healthBar.value = playerBehaviourScript.Health;
        if (GameManager.IsGameOver) gameOverObject.SetActive(true);

    }
}
