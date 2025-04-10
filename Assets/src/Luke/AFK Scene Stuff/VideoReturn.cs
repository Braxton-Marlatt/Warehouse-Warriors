using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoReturn : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private Vector3 lastMousePosition;

    void Start()
    {
        InitializeVideoPlayer();
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        if (CheckForInput())
        {
            ReturnToMainMenu();
        }
    }

    private void InitializeVideoPlayer()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private bool CheckForInput()
    {
        if (Input.anyKeyDown)
        {
            return true;
        }

        Vector3 currentMousePos = Input.mousePosition;
        if (currentMousePos != lastMousePosition)
        {
            return true;
        }
        lastMousePosition = currentMousePos;

        return false;
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Start_Menu");
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Optional: Restart video if it ends before user input
        vp.Play();
    }
}