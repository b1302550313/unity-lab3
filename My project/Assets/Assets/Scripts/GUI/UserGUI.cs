using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;
    public string gameMessage;
    public int time;
    GUIStyle style, bigstyle;
    private GUIStyle buttonStyle;//��ť�����ֵ�style
    // Start is called before the first frame update
    void Start()
    {
        time = 60;
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;

        style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        bigstyle = new GUIStyle();
        bigstyle.normal.textColor = Color.white;
        bigstyle.fontSize = 50;

        buttonStyle = new GUIStyle(GUI.skin.button);// ����һ����ť��style
        buttonStyle.fontSize = 20;// ���������С
    }

    // Update is called once per frame
    void OnGUI()
    {
        GUI.Label(new Rect(160, Screen.height * 0.1f, 50, 200), "Pastor and Devils", bigstyle);
        GUI.Label(new Rect(250, 100, 50, 200), gameMessage, style);
        GUI.Label(new Rect(0, 0, 100, 50), "Time: " + time, style);
    }
}

