// Description: This script tracks user inactivity and loads a video scene after a specified duration of inactivity.

using UnityEngine;
using UnityEngine.SceneManagement;

public class IdleTimer : MonoBehaviour
{
    [SerializeField] private float idleDuration = 30f; // Seconds of inactivity before transition

    private float timer;
    private Vector3 lastMousePosition;

    void Start()
    {
        ResetTimer();
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        bool hasInput = CheckForInput();

        if (hasInput)
        {
            ResetTimer();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= idleDuration)
            {
                LoadVideoScene();
            }
        }
    }

    private bool CheckForInput()
    {
        // Check for keyboard/controller input
        if (Input.anyKeyDown)
        {
            return true;
        }

        // Check for mouse movement
        Vector3 currentMousePos = Input.mousePosition;
        if (currentMousePos != lastMousePosition)
        {
            lastMousePosition = currentMousePos;
            return true;
        }

        return false;
    }

    private void ResetTimer()
    {
        timer = 0f;
    }

    private void LoadVideoScene()
    {
        SceneManager.LoadScene("AFKScene");
    }
}