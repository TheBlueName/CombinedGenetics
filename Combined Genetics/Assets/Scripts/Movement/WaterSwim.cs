using UnityEngine;

public class WaterSwim : MonoBehaviour
{
    public float swimForce = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 swimDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        swimDirection = Vector3.zero;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        swimDirection = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void FixedUpdate()
    {
        Swim();
        Rotate();
    }

    void Swim()
    {
        rb.AddForce(swimDirection * swimForce);
    }

    void Rotate()
    {
        if (swimDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(swimDirection, Vector3.up);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(rotation);
        }
    }
}
