using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class AutoPlacement : MonoBehaviour
{

    public GameObject placedPrefab;
    private GameObject placedObject;
    public ARPlaneManager arPlaneManager;

    void Awake()
    {
        //arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        //if (args.added != null && placedObject == null)
        if (args.added != null)
        {
            ARPlane arPlane = args.added[0];
            placedObject = Instantiate(placedPrefab, arPlane.transform.position, Quaternion.identity);
        }
    }

}
