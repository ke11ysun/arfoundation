using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSText : MonoBehaviour
{
    public Text coordinates;
    public float latitude;
    public float longitude;

    // Update is called once per frame
    void Update()
    {
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        //coordinates.text = "lat:" + GPS.Instance.latitude.ToString() + "\nlon:" + GPS.Instance.longitude.ToString();
        coordinates.text = "[GPS] lat:" + latitude.ToString() + "\nlon:" + longitude.ToString();

    }
}
