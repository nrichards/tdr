using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] private float acceleration = 8;
    [SerializeField] private float turnSpeed = 5;

    private Quaternion targetRotation;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent <Rigidbody> ();
    }

    void Update()
    {
        SetRotationPoint();
    }

    private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);

            Vector3 direction = target - transform.position;

            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    private void FixedUpdate()
    {

        float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        _rigidbody.AddRelativeForce(Vector3.forward * accelerationInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
    }
}
