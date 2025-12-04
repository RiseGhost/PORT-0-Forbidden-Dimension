using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class DataRoot
{
    public string ID;

    public DataRoot(string ID)
    {
        this.ID = ID;
    }

    public override bool Equals(object obj)
    {
        if (obj is DataRoot)
        {
            return this.ID == ((DataRoot)obj).ID;
        }
        return false;
    }
}

[System.Serializable]
public class StorageItem
{
    public List<string> itens = new List<string>();
}

public class StorageManager : MonoBehaviour
{
    private StorageItem storageItem = new StorageItem();
    void Awake()
    {
        Debug.Log("Storage Manager UP 🔝");
        this.name = "Storage Manager";
        this.tag = "StorageManager";
    }

    void Start()
    {
        Debug.Log("Data saved -> " + PlayerPrefs.GetString("DataSave", ""));
        storageItem = JsonUtility.FromJson<StorageItem>(PlayerPrefs.GetString("DataSave", ""));
        if (storageItem == null)
        {
            Debug.Log("Não existia data salva");
            storageItem = new StorageItem();
        }
        DontDestroyOnLoad(this);
        StartCoroutine(Saving());
    }

    private static List<GameObject> GetStoragesEntities()
    {
        List<GameObject> entities = new List<GameObject>();
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObject.GetComponent<StorageEntity>() != null) entities.Add(gameObject);
        }
        return entities;
    }

    private void Save()
    {
        List<DataRoot> saveItems = storageItem.itens.Select((x) => JsonUtility.FromJson<DataRoot>(x)).ToList();
        foreach (GameObject gameObject in GetStoragesEntities())
        {
            StorageEntity storageEntity = gameObject.GetComponent<StorageEntity>();
            if (!saveItems.Contains(new DataRoot(storageEntity.GetSaveItem().getID())))
            {
                Debug.Log("Item salvo -> " + storageEntity.GetSaveItem().toJSON());
                storageItem.itens.Add(storageEntity.GetSaveItem().toJSON());
            }
        }
        PlayerPrefs.SetString("DataSave", JsonUtility.ToJson(storageItem));
        PlayerPrefs.Save();
    }

    public static void Load<T>() where T : SaveItem
    {
        Debug.Log("ℹ️ Load iniciado.");
        string data = PlayerPrefs.GetString("DataSave", "");
        if (data.Trim().Length == 0) return;
        StorageItem storage = JsonUtility.FromJson<StorageItem>(data);
        if (storage == null)
        {
            Debug.Log("O Storage está vazio não a Load para carregar.");
            return;
        }
        Debug.Log("Loaded Data:");
        Debug.Log("--------------");
        foreach (string item in storage.itens)
        {
            Debug.Log(item);
            if (JsonUtility.FromJson<T>(item) != null)
                JsonUtility.FromJson<T>(item).Load(item);
        }
        Debug.Log("--------------");
    }

    public void UpdateData<T>(T data) where T : SaveItem
    {
        if (storageItem == null)
        {
            Debug.Log("The storage is empty. There is noting to update.");
            return;
        }
        for(int i = 0; i < storageItem.itens.Count; i++)
        {
            if (JsonUtility.FromJson<DataRoot>(storageItem.itens[i]).ID.Equals(data.getID()))
            {
                storageItem.itens.RemoveAt(i);
                storageItem.itens.Add(data.toJSON());
                Save();
                return;
            }
        }
    }

    public static void DeletedStorage()
    {
        var manager = GameObject.FindAnyObjectByType<StorageManager>();
        manager.StopAllCoroutines();
        foreach (GameObject entity in GetStoragesEntities())
        {
            Destroy(entity);
        }
        PlayerPrefs.SetString("DataSave", "");
        PlayerPrefs.DeleteKey("DataSave");
        PlayerPrefs.Save();
    }

    private IEnumerator Saving()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(3f);
            Debug.Log("✅ Jogo Salvo");
            Save();
        }
    }

    public void ResetStorage(){ PlayerPrefs.DeleteAll(); }
}