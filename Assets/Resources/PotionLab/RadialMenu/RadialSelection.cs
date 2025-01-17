using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class RadialPartData
{
    public string text;
    public Sprite icon;
    public UnityEvent onSelected = new UnityEvent();
}

public class RadialSelection : MonoBehaviour
{
    public InputActionReference customButton;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenPart;
    public Transform handTransform;
    
    // Customization fields
    public List<RadialPartData> radialPartDataList = new List<RadialPartData>();
    
    private bool isPressed = false;
    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;

    void Start()
    {
        customButton.action.started += ButtonWasPressed;
        customButton.action.canceled += ButtonWasReleased;
    }
    
    void Update()
    {
        if (isPressed)
        {
            GetSelectedRadialPart();
        }
    }
    
    private void ButtonWasPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("Button was pressed");
        radialPartCanvas.gameObject.SetActive(true);
        SpawnRadialParts();
        isPressed = true;
    }
    
    private void ButtonWasReleased(InputAction.CallbackContext obj)
    {
        Debug.Log("Button was released");
        HideAndTriggerSelected();
    }

    public void HideAndTriggerSelected()
    {
        if (currentSelectedRadialPart >= 0 && currentSelectedRadialPart < radialPartDataList.Count)
        {
            radialPartDataList[currentSelectedRadialPart].onSelected?.Invoke();
        }
        radialPartCanvas.gameObject.SetActive(false);
        isPressed = false;
    }

    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);
        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);
        if(angle < 0)
        {
            angle += 360;
        }
        currentSelectedRadialPart = (int)(angle * radialPartDataList.Count / 360);
        UpdateRadialPartVisuals();
    }
    
    private void UpdateRadialPartVisuals()
    {
        for (int i = 0; i < spawnedParts.Count; i++)
        {
            bool isSelected = i == currentSelectedRadialPart;
            var partRoot = spawnedParts[i];
            
            // Update color and scale
            partRoot.GetComponent<Image>().color = isSelected ? Color.cyan : Color.white;
            partRoot.transform.localScale = isSelected ? 1.1f * Vector3.one : Vector3.one;
            
            // Update icon and text visibility/scale if needed
            if (partRoot.transform.childCount > 0)
            {
                foreach (Transform child in partRoot.transform)
                {
                    child.localScale = isSelected ? 1.1f * Vector3.one : Vector3.one;
                }
            }
        }
    }
    
    public void SpawnRadialParts()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position + 0.2f * handTransform.forward;
        radialPartCanvas.rotation = handTransform.rotation;
        
        // Clean up existing parts
        foreach (var part in spawnedParts)
        {
            Destroy(part);
        }
        spawnedParts.Clear();
        
        int numberOfParts = radialPartDataList.Count;
        if (numberOfParts == 0) return;

        // Spawn new parts
        for (int i = 0; i < numberOfParts; i++)
        {
            float angle = -i * 360f / numberOfParts - angleBetweenPart/2;
            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            
            // Setup basic radial part
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = new Vector3(0, 0, angle);
            spawnedRadialPart.GetComponent<Image>().fillAmount = 1f / numberOfParts - angleBetweenPart / 360f;
            
            Transform iconTransform = spawnedRadialPart.transform.Find("Icon");
            if (iconTransform != null)
            {
                Image iconImage = iconTransform.GetComponent<Image>();
                iconImage.sprite = radialPartDataList[i].icon;
            }
            
            Transform textTransform = spawnedRadialPart.transform.Find("Text");
            if (textTransform != null)
            {
                TextMeshProUGUI text = textTransform.GetComponent<TextMeshProUGUI>();
                text.text = radialPartDataList[i].text;
            }
            
            spawnedParts.Add(spawnedRadialPart);
        }
    }
}