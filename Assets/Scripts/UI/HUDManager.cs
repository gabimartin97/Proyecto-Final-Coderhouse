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

    // Start is called before the first frame update
    void Start()
    {
        PlayerBehaviour.OnHealthChange += OnHealthChangeManager; //Me suscribo al evento

        pointsText = pointsIndicatorObject.GetComponent<TextMeshProUGUI>();
        healthBar = healthBarObject.GetComponent<Slider>();
        OnHealthChangeManager(100f, 100f);
    }   
    

    // Update is called once per frame
    void Update()
    {

        pointsText.SetText(GameManager.Score.ToString());

        if (GameManager.IsGameOver) gameOverObject.SetActive(true);
    }
    private void OnHealthChangeManager(float actualHealth, float totalHealth)
    {
        healthBar.maxValue = totalHealth;
        healthBar.value = actualHealth;

    }
}
