using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RadialSelection : MonoBehaviour
{
    [Range(2, 10)] public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void SpawnRadialParts()
    {
        float angle = 360f / numberOfRadialPart;
        for (int i = 0; i < numberOfRadialPart; i++)
        {
            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = new Vector3(0, 0, angle * i);
            spawnedRadialPart.GetComponent<Image>().fillAmount = 1f / numberOfRadialPart;
        }
    }
}
