using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class AvatarChanger : MonoBehaviour
{
    [System.Serializable]
    public class AvatarData
    {
        public Sprite image;
        public UnityEvent onSelected;
    }

    public GameObject prefabUI;
    public AvatarData[] prefabDataList;

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

        foreach (AvatarData data in prefabDataList)
        {

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

    public void OnPrefabButtonClicked(AvatarData data, Vector3 position)
    {
        data.onSelected?.Invoke();
    }
    
    public void AddPrefabData(AvatarData data)
    {
        if (data != null && prefabDataList != null)
        {
            prefabDataList[prefabDataList.Length] = data;
        }
    }
}
