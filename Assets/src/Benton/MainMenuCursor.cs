using UnityEngine;

public class MainMenuCursor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }
}
