using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    private AudioSource sound;
    public bool rotate = false;
    bool isOpen = false;
    Transform hingeTransform;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        hingeTransform = transform.Find("Hinge");
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate)
        {
            rotate = false;
            if(!isOpen)
            {
                transform.RotateAround(hingeTransform.position, Vector3.up, -90f);
                isOpen = true;
            } else
            {
                transform.RotateAround(hingeTransform.position, Vector3.up, 90f);
                isOpen = false;
            }

            
        }
    }
    public void Open()
    {
        if (!isOpen)
        {
            transform.RotateAround(hingeTransform.position, Vector3.up, -90f);
            isOpen = true;
            sound.Play();
        }
        else
        {
            transform.RotateAround(hingeTransform.position, Vector3.up, 90f);
            isOpen = false;
        }

    }
}
