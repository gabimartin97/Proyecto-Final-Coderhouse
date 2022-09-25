using UnityEngine;
public class PlayerAim_Move_Force : MonoBehaviour
{
    // --- PARAMETROS CONFIGURABLES -- //
    [SerializeField] private LayerMask groundMask; //para que el rayo de la camara choque con ciertas superficies
    [SerializeField] Animator playerAnimator;
    [SerializeField] float moveForce = 1000f;
    [SerializeField] float rotateForce = 500f;
    [SerializeField] float stopCoeficient = 0.9f; // Para que el player frene al no pulsar teclas
    // --- PARAMETROS CONFIGURABLES -- //

    private Camera mainCamera;
    Vector3 direccion;
    Rigidbody myRigidbody;

    private void Start()
    {
        // Cache the camera, Camera.main is an expensive operation.
        mainCamera = Camera.main;
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        Walk();
        SetState(); //Para las animaciones
    } 

    private void FixedUpdate()
    {
        Aim();
        if (direccion != Vector3.zero)
        {   /*La fuerza a aplicar es constante (ForceMode.Force) */
            myRigidbody.AddForce(direccion * moveForce, ForceMode.Force);
           
        }
        else
        {
            myRigidbody.velocity *= stopCoeficient; //Para que el player no patine le voy quitando velocidad
        }
    }


    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.

            Quaternion q = Quaternion.LookRotation(direction);
            myRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, q, rotateForce * Time.deltaTime));
            
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }
    void Walk()
    {
        direccion = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direccion += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direccion += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direccion += Vector3.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direccion += Vector3.back;
        }

        direccion.Normalize();
    }

    void SetState()
    {
        if (direccion != Vector3.zero)
        {
            playerAnimator.SetBool("isRunning", true);

        }
        else
        {
            playerAnimator.SetBool("isRunning", false);

        }
    }

}
