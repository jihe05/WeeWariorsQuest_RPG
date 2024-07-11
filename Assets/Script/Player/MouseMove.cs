using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public  float mouseSpeed = 5f;
    public  float xRotation = 25f; 

    public Transform plauyerObj;


    public void Update()
    {

        if (Input.GetMouseButton(1))
        { 
           
            float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
            float mouseY  = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;


            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            //카메라X축 제한
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 60f);

          

            plauyerObj.Rotate(Vector3.up * mouseX);
        
        }



    }

}
