using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class PrefabSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PrefabData
    {
        public GameObject prefabToSpawn;
        public Sprite image;
        public UnityEvent onSelected;
    }

    public GameObject prefabUI;
    public PrefabData[] prefabDataList;

    void Start()
    {
        SpawnPrefabUI();
    }

    void SpawnPrefabUI()
    {
        if (prefabUI == null)
        {
            Debug.LogError("PrefabUI template is missing!");
            return;
        }

        foreach (PrefabData data in prefabDataList)
        {
            if (data.prefabToSpawn == null)
            {
                Debug.LogError("PrefabToSpawn is missing in PrefabData!");
                continue;
            }

            GameObject uiInstance = Instantiate(prefabUI, transform);

            Button button = uiInstance.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnPrefabButtonClicked(data,button.transform.position));
            }
            else
            {
                Debug.LogError("PrefabUI does not contain a Button component!");
            }

            Transform imageTransform = uiInstance.transform.Find("Image");
            if (imageTransform != null)
            {
                Image imageComponent = imageTransform.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = data.image;
                }
                else
                {
                    Debug.LogError("Image child does not have an Image component!");
                }
            }
            else
            {
                Debug.LogError("PrefabUI does not contain a child named 'Image'!");
            }
        }
    }

    public void OnPrefabButtonClicked(PrefabData data, Vector3 position)
    {
        Quaternion rotation = Camera.main.transform.rotation!=null ? Camera.main.transform.rotation : Quaternion.identity;
        Instantiate(data.prefabToSpawn, position, rotation);
        data.onSelected?.Invoke();
    }
    
    public void AddPrefabData(PrefabData data)
    {
        if (data != null && prefabDataList != null)
        {
            prefabDataList[prefabDataList.Length] = data;
        }
    }
}