using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 CameraOffSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate camera position relative to the car
        Quaternion carRotation = Player.transform.rotation;
        Vector3 offsetDirection = carRotation * CameraOffSet;
        Vector3 cameraPos = Player.transform.position + offsetDirection;

        // Set the camera position
        transform.position = cameraPos;

        // Make the camera look at the car
        transform.LookAt(Player.transform);
    }

}
