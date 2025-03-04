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

}
