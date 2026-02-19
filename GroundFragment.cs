using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFragment : MonoBehaviour
{
    [Header("下落设置")]
    public float minFallSpeed = 0.8f;
    public float maxFallSpeed = 1.5f;

    private Rigidbody rb;
    private float fallDelay = 5f;
    private float fallSpeedMultiplier = 1f;
    private bool hasStartedFalling = false;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        fallSpeedMultiplier = Random.Range(minFallSpeed, maxFallSpeed);

        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.mass = Random.Range(0.5f, 2f); // 不同质量
            rb.drag = Random.Range(0.1f, 0.5f); // 不同空气阻力
        }

        Invoke(nameof(StartFalling), fallDelay);
    }

    void StartFalling()
    {
        hasStartedFalling = true;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            rb.AddTorque(new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)
            ), ForceMode.Impulse);
        }
    }

    void Update()
    {
        if (hasStartedFalling && rb != null && rb.velocity.magnitude < 10f)
        {
            rb.AddForce(Vector3.down * fallSpeedMultiplier * 2f, ForceMode.Acceleration);
        }
    }

    void OnDrawGizmos()
    {
        if (hasStartedFalling)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector3.one * 1.1f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }
    }
}