using UnityEngine;

public class ServerGameObject : MonoBehaviour, StorageEntity
{
    public Server server;

    public SaveItem GetSaveItem() { return server; }

    void Start()
    {
        ServersPlace place = GameObject.FindGameObjectWithTag("ServersPlace").GetComponent<ServersPlace>();
        if (place == null) return;
        place.PutNewServer(this);
    }

    public void Init(Server server)
    {
        this.server = server;
    }

    void FixedUpdate()
    {
        server.positionStatus = new PositionStatus(transform);
    }

    public override bool Equals(object other)
    {
        if (other is ServerGameObject)
        {
            return ((ServerGameObject)other).GetSaveItem().Equals(this.GetSaveItem());
        }
        return false;
    }
}