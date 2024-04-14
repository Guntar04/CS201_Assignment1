using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 CameraOffSet;
    private float smoothTime = 0.05f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Quaternion carRotation = Player.transform.rotation;
        Vector3 offsetDirection = carRotation * CameraOffSet;
        Vector3 cameraPos = Player.transform.position + offsetDirection;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, smoothTime);

        // Make the camera look at the car
        transform.LookAt(Player.transform);
    }

}
