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
    [SerializeField] GameObject gameWinObject;
    [SerializeField] GameObject weaponImageObject;
    [SerializeField] GameObject[] weaponSlots;
    [SerializeField] Sprite[] weaponImages;

    TextMeshProUGUI pointsText;
    Slider healthBar;
    Image weaponImage;

    // Start is called before the first frame update
    void Start()
    {
        PlayerBehaviour.OnHealthChange += OnHealthChangeManager;  //Me suscribo al evento
        PlayerBehaviour.OnWeaponInHand += OnWeapoinInHandManager; //Me suscribo al evento
        PlayerBehaviour.OnWeaponPickedUp += OnWeaponPickedUpManager; //Me suscribo al evento
        PlayerBehaviour.OnDead += OnplayerDeath;
        BossBehaviour.OnDead += OnBossDeath;

        pointsText = pointsIndicatorObject.GetComponent<TextMeshProUGUI>();
        healthBar = healthBarObject.GetComponent<Slider>();
        weaponImageObject.SetActive(false);
        weaponImage =weaponImageObject.GetComponent<Image>();
        OnHealthChangeManager(100f, 100f);
    }   
    

    // Update is called once per frame
    void Update()
    {

        pointsText.SetText(GameManager.Score.ToString());

        
    }
    private void OnHealthChangeManager(float actualHealth, float totalHealth)
    {
        healthBar.maxValue = totalHealth;
        healthBar.value = actualHealth;

    }

    private void OnWeapoinInHandManager(int weaponNumber)
    {
        
        weaponImage.sprite = weaponImages[weaponNumber];
        weaponImageObject.SetActive(true);
    }
    private void OnWeaponPickedUpManager(int weaponNumber)
    {
        weaponSlots[weaponNumber - 1].GetComponent<Image>().color = Color.white;
        
    }
    private void OnBossDeath()
    {
        gameWinObject.SetActive(true);
    }
    private void OnplayerDeath()
    {
        gameOverObject.SetActive(true);
    }


}
