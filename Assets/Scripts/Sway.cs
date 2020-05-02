using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    private Quaternion originLocalRotation;

    private void Start()
    {
        originLocalRotation = transform.localRotation;
    }

    void Update()
    {
        updateSway();
    }

    private void updateSway()
    {
        float t_xLookInput = Input.GetAxis("Mouse X");
        float t_yLookInput = Input.GetAxis("Mouse Y");
        //Calculate the weapon rotation
        Quaternion t_xAngleAdjustment = Quaternion.AngleAxis(-t_xLookInput * 2f, Vector3.up);
        Quaternion t_yAngleAdjustment = Quaternion.AngleAxis(t_yLookInput * 2f, Vector3.right);
        Quaternion t_targerRotation = originLocalRotation * t_xAngleAdjustment * t_yAngleAdjustment;
        //Rotate towards tarjet rotation 
        transform.localRotation = Quaternion.Lerp(transform.localRotation, t_targerRotation, Time.deltaTime * 10f);
    }
}
