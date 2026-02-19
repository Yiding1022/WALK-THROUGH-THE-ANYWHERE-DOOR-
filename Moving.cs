using UnityEngine;

public class SimpleMoveToPosition : MonoBehaviour
{
    public Vector3 targetPosition; 
    public float moveSpeed = 2.0f; 
    private bool isMoving = true; 

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition; 
                isMoving = false; 
            }
        }
    }
}
