using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    public GameObject bossFight;
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
            bossFight.SetActive(true);

        }
    }
}
