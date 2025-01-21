using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnObject : MonoBehaviour
{
    public GameObject prefab;
    public XRRayInteractor rayInteractor;
    private Button Button;
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Spawn);
    }

    void Spawn()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Instantiate(prefab, hit.point, Quaternion.identity);
        }
    }
}
