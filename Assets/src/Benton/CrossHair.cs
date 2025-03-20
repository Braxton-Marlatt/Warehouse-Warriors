using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D crosshairTexture;  // Drag and drop your crosshair texture here in the Inspector

    void Start()
    {
        // Set the mouse cursor to the crosshair texture
        Cursor.SetCursor(crosshairTexture, Vector2.zero, CursorMode.ForceSoftware);

        // Optionally, hide the default system cursor
    }

    void Update()
    {
       if (Time.timeScale == 0)
       {
           // Show the cursor when the game is paused
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
       }
       else
       {
           // Hide the cursor when the game is not paused
            Cursor.SetCursor(crosshairTexture, Vector2.zero, CursorMode.ForceSoftware);
       }
    }

}
