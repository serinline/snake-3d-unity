using UnityEngine;

public class InfoStyle : MonoBehaviour
{
    GUIStyle text_style;
    PlaySnake playSnake;

    void Start()
    {
        playSnake = GetComponent<PlaySnake>();
        this.SetTextStyle();
    }

    void OnGUI()
    {
        if (playSnake.game_over)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 30), "Game Over", text_style);
        }
    }

    void SetTextStyle()
    {
        text_style = new GUIStyle()
        {
            fontSize = 30,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Italic
        };
    }
}
