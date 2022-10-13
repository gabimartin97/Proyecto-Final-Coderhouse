using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleKey : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] color keyColor;
    private Light light;
    enum color
    {
        red, green, blue
    }
    public UnityEvent onPickedUp;
    
    void Start()
    {
        light = GetComponent<Light>();
        switch (keyColor)
        {
            case color.red: light.color = Color.red; break;
                case color.green: light.color = Color.green; break;
                    case color.blue: light.color = Color.blue; break;
                default: light.color = Color.white; break;
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPickedUp.Invoke();
            gameObject.SetActive(false);
                        
        }

    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }
}
