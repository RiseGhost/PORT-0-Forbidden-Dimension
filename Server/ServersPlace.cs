using System.Collections.Generic;
using UnityEngine;

public class ServersPlace : MonoBehaviour
{
    private static List<ServerGameObject> servers = new List<ServerGameObject>();
    [SerializeField] private ushort lines = 1, columns = 1;
    [SerializeField] private float gap = 0f;
    private ushort capacity = 0;

    void Awake()
    {
        this.tag = "ServersPlace";
        capacity = (ushort)(lines * columns);
    }

    public void PutNewServer(ServerGameObject server)
    {
        if (servers.Count >= capacity) return;
        if (servers.Contains(server)) return;
        servers.Add(server);
        float column = ((servers.Count - 1) % columns);
        float line = (int) ((servers.Count-1) / columns);
        server.transform.position = transform.position + new Vector3(column + (gap * column), 6.0f, line + (gap * line));
        server.transform.Rotate(0f, 180f, 0f);
    }
}
