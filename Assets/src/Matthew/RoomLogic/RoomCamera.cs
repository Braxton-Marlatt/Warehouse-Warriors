using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public Transform target;  // Assign the target position in the Inspector or set dynamically
    public float speed = 5f;  // Adjust the speed of the pan
    private Vector3 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null){
            targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
