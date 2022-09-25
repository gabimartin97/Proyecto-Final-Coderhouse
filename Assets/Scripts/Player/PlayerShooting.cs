using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gunFlash;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject weapon = GameObject.Find("Weapon2");
            Vector3 weaponPosition = weapon.transform.position;
            Quaternion rotation = weapon.transform.rotation;
            Instantiate(bullet, weaponPosition, rotation);
            Instantiate(gunFlash, weaponPosition, rotation);

        }
    }
}
