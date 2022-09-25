using UnityEngine;

public class GunFlashBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
