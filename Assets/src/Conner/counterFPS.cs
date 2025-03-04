using UnityEngine;

public class counterFPS : MonoBehaviour
{

    float deltaTime = 0.0f;
    private GUIStyle style;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 48;
        style.normal.textColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

    }

    private void OnGUI()
    { GUI.Label(new Rect(10, 10, 300, 100), "FPS: " + (1.0f / deltaTime).ToString("F2"), style);


    }
}
