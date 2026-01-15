using System;
using UnityEngine;

[System.Serializable]
public class Server : SaveItem
{
    public string ID = Guid.NewGuid().ToString();
    public ServerStatusStruct serverStatus;
    public PositionStatus positionStatus = new PositionStatus();
    public string resource = "ServerBox";

    public Server(){}

    public Server(ServerStatusStruct serverStatus)
    {
        this.serverStatus = serverStatus;
        GameObject gameObject = Resources.Load<GameObject>(resource);
        var server = MonoBehaviour.Instantiate(gameObject, positionStatus.pos + new Vector3(0, 5, 0), Quaternion.identity);
        server.GetComponent<ServerGameObject>().Init(this);
        Debug.Log("Server Create, status -> " + JsonUtility.ToJson(serverStatus));
        Save();
    }

    public void Load(string json)
    {
        var data = JsonUtility.FromJson<Server>(json);
        GameObject gameObject = Resources.Load<GameObject>(resource);
        var server = MonoBehaviour.Instantiate(gameObject, positionStatus.pos + new Vector3(0, 5, 0), Quaternion.identity);
        server.GetComponent<ServerGameObject>().server = data;
        Debug.Log("\n\nLoader CPU Status is null -> " + (data.serverStatus.cpu.getStatus() == null) + "\n\n");
    }
    public string toJSON() { return JsonUtility.ToJson(this); }
    public string getID() { return ID; }

    public override bool Equals(object obj)
    {
        if (obj is Server)
        {
            return ((Server)obj).ID == this.ID;
        }
        return false;
    }

    public void Update()
    {
        
    }
    
    public override string ToString()
    {
        return "";
    }

    public void Save()
    {
        StorageManager storageManager = GameObject.FindGameObjectWithTag("StorageManager").GetComponent<StorageManager>();
        if (storageManager == null) return;
        storageManager.Save(this);
    }
}