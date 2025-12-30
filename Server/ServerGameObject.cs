using UnityEngine;

public class ServerGameObject : MonoBehaviour, StorageEntity
{
    [SerializeField] private Canvas Warring;
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
        Warring.gameObject.SetActive(!server.serverStatus.isOperational());
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