using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(10, 120, w, h * 2 / 100); // Lowered again

        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 25;
        style.normal.textColor = Color.black;

        float fps = 1.0f / deltaTime;
        string text = $"FPS: {fps:F1}";
        GUI.Label(rect, text, style);
    }
}
