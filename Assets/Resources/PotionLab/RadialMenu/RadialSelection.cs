using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class RadialSelection : MonoBehaviour
{
    [Range(2, 10)] public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenPart;
    public Transform handTransform;
    
    public UnityEvent<int> OnPartSelected;
    
    
    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;
    void Update()
    {

    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }

    public void GetSelecteRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);
        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);
        if(angle < 0)
        {
            angle += 360;
        }
        currentSelectedRadialPart = (int) angle * numberOfRadialPart / 360;
        for (int i =0; i < spawnedParts.Count; i++)
        {
            if (i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.yellow;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
    }
    
    public void SpawnRadialParts()
    {
        foreach (var part in spawnedParts)
        {
            Destroy(part);
        }
        spawnedParts.Clear();
        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = - i*360f / numberOfRadialPart - angleBetweenPart/2;
            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = new Vector3(0, 0, angle);
            spawnedRadialPart.GetComponent<Image>().fillAmount = 1f / numberOfRadialPart - angleBetweenPart / 360f;
            spawnedParts.Add(spawnedRadialPart);
        }
    }
}
