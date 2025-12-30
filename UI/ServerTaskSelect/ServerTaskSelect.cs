using UnityEngine;

public class ServerTaskSelect : MonoBehaviour
{
    private ServerGameObject servers;

    void Start()
    {
        servers = GameObject.FindAnyObjectByType<ServerGameObject>();
        
    }

    void Update()
    {
        
    }
}
