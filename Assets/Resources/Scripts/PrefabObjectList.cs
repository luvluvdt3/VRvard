using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PrefabObjectList : MonoBehaviour
{
    public GameObject[] prefabObjects;
    public XRRayInteractor[] xrOrigins;
    void Start()
    {
        //Create an UI list of prefab object as child of this object using Button with a child TMP Text
        for (int i = 0; i < prefabObjects.Length; i++)
        {
            GameObject button = new GameObject("Button");
            button.transform.parent = transform;
            button.AddComponent<RectTransform>();
            button.AddComponent<UnityEngine.UI.Button>();
            button.AddComponent<UnityEngine.UI.Image>();
            button.AddComponent<UnityEngine.UI.LayoutElement>();
            button.AddComponent<UnityEngine.UI.VerticalLayoutGroup>();
            button.AddComponent<UnityEngine.UI.Image>();
            button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => CreateObject(i));

            GameObject text = new GameObject("Text");
            text.transform.parent = button.transform;
            text.AddComponent<RectTransform>();
            text.AddComponent<TMPro.TextMeshProUGUI>();
            text.GetComponent<TMPro.TextMeshProUGUI>().text = prefabObjects[i].name;
            text.GetComponent<TMPro.TextMeshProUGUI>().alignment = TMPro.TextAlignmentOptions.Center;
            text.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 20;
            text.GetComponent<TMPro.TextMeshProUGUI>().color = Color.black;
            text.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            text.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            text.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            text.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            
        }

    }

    public void CreateObject(int index)
    {
        GameObject obj = Instantiate(prefabObjects[index], xrOrigins[0].transform.position, xrOrigins[0].transform.rotation);
        obj.transform.parent = xrOrigins[0].transform;
        gameObject.SetActive(false);
    }
}