using UnityEngine;
using UnityEngine.AI;

public class SquareMovement : MonoBehaviour
{

    public float moveSpeed =5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SquareBehave();
    }

    // Update is called once per frame
    void Update()
    {
        SquareBehave();
    }

    public void SquareBehave(){
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

    }

}

