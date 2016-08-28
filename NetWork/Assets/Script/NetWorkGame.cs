using UnityEngine;
using System.Collections;

public enum BallGameState
{
    lobby,
    idle,
    gaming,
    lose,
}

public class NetWorkGame : MonoBehaviour
{
    public GameObject prefab_ball;
    private BallGameState state = BallGameState.lobby;
    private string IP = "10.234.41.123";
    private int PORT = 1000;
    private Rigidbody m_Ball;

    void OnGUI()
    {
        switch(state)
        {
            case BallGameState.lobby:
                OnLobby();
                break;
            case BallGameState.idle:
                OnIdle();
                break;
            case BallGameState.gaming:
                OnGaming();
                break;
            case BallGameState.lose:
                OnLose();
                break;
        }
    }

    void OnLobby()
    {
        if (GUILayout.Button("建立服务器"))
        {
            NetworkConnectionError error = Network.InitializeServer(10, PORT, false);
            if (error == NetworkConnectionError.NoError)
            {
                state = BallGameState.idle;
            }
        }
        GUILayout.Label("======================================");
        GUILayout.BeginHorizontal();
        GUILayout.Label("服务器地址：");
        IP = GUILayout.TextField(IP);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("连接服务器"))
        {
            NetworkConnectionError error = Network.Connect(IP, PORT);
            if (error == NetworkConnectionError.NoError)
            {
                state = BallGameState.idle;
            }
        }
    }
    void OnIdle()
    {
        if (GUILayout.Button("Start!"))
        {
            StartGaming();
        }
    }

    void OnGaming()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        m_Ball.AddForce(new Vector3(x, 0, y));

        if (m_Ball.transform.position.y < -5)
        {
            Network.Destroy(m_Ball.GetComponent<NetworkView>().viewID);
            state = BallGameState.lose;
        }
    }

    void OnLose()
    {
        GUILayout.Label("You lose!");
        if(GUILayout.Button("Start Again!"))
        {
            StartGaming();
        }
    }

    void StartGaming()
    {
        state = BallGameState.gaming;

        GameObject obj = Network.Instantiate(prefab_ball, new Vector3(0, 1, 0), Quaternion.identity, 0) as GameObject;

        m_Ball = obj.GetComponent<Rigidbody>();

    }
}
