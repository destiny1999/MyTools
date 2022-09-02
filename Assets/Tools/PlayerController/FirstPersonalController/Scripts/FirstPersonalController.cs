using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonalController : MonoBehaviour
{
    [SerializeField] float LRSpeed;
    [SerializeField] float forwardSpeed;
    [SerializeField] float backSpeed;
    [SerializeField] float mouseSensitivity = 60f;
    [SerializeField] bool rotateUpDown = false;
    void FixedUpdate()
    {
        float moveLR = Input.GetAxis("Horizontal") * LRSpeed;
        float moveFB = Input.GetAxis("Vertical");
        if (moveFB > 0) moveFB *= forwardSpeed;
        else moveFB *= backSpeed;
        transform.Translate(new Vector3(moveLR, 0, moveFB) * Time.deltaTime);

        #region control rotate with mouse


        GameObject cam = Camera.main.transform.gameObject;
        float rotHorizontal = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float rotVertical = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        CameraRotation(cam, rotHorizontal, rotVertical);

        #endregion
    }
    void CameraRotation(GameObject cam, float rotHorizontal, float rotVertical)
    {

        transform.Rotate(0, rotHorizontal * Time.fixedDeltaTime, 0);
        if (rotateUpDown)
        {
            cam.transform.Rotate(-rotVertical * Time.fixedDeltaTime, 0, 0);
        }
        
        if (Mathf.Abs(cam.transform.localRotation.x) > 0.7)
        {

            float clamped = 0.7f * Mathf.Sign(cam.transform.localRotation.x);

            Quaternion adjustedRotation = new Quaternion(clamped, cam.transform.localRotation.y, cam.transform.localRotation.z, cam.transform.localRotation.w);
            cam.transform.localRotation = adjustedRotation;
        }


    }
}
