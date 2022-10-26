using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossFightTrigger : MonoBehaviour
{
    public GameObject bossFight;
    static public event Action OnBossFightStart;
    private bool isBossfight = false;

    // Start is called before the first frame update
    void Start()
    {
        bossFight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!isBossfight)
            {
                OnBossFightStart?.Invoke();
                bossFight.SetActive(true);
                isBossfight = true;
            }
            


        }
    }
}
