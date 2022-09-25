using UnityEngine;
public class PlayerAim_Move : MonoBehaviour
{

    [SerializeField] private LayerMask groundMask;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float speed = 5f;
    private Camera mainCamera;
    Vector3 direccion;

    private void Start()
    {
        // Cache the camera, Camera.main is an expensive operation.
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Aim();
        Walk();
        SetState();
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
            transform.forward = direction;
            Debug.DrawLine(transform.position, direction * 20);

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
        transform.position += direccion * Time.deltaTime * speed;
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
