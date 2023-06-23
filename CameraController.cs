using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CharacterManager CM;
    public float camOffest, cameraSmoothing, zoomMultiplier, mouseSensitivity, scrollSensitivty, minimum;


    [Range(0.0f, 89f)]
    public float topClampAngle = 90f;
    [Range(0.0f, 90.0f)]
    public float bottomClampAngle = 90f;

    float rotat, mouseY;

    void Start()
    {
        CM = FindObjectOfType<CharacterManager>();
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = SetCamPosition();
    }

    void FixedUpdate()
    {
        //rotation.position = CM.GetMidPoint();
        mouseY = Input.GetAxisRaw("Mouse Y");
        zoomMultiplier -= Input.mouseScrollDelta.y * scrollSensitivty;

        if (zoomMultiplier < 1)
            zoomMultiplier = 1;
        else if (zoomMultiplier > 2.5f)
            zoomMultiplier = 2.5f;


        rotat -= mouseY * mouseSensitivity * Time.deltaTime;
        rotat = Mathf.Clamp(rotat, bottomClampAngle, topClampAngle);

        transform.RotateAround(CM.GetMidPoint(),Vector3.forward, Input.GetAxisRaw("Mouse Y"));
        //Physics.SyncTransforms();
        //rotation.rotation = Quaternion.Euler(rotat, rotation.rotation.eulerAngles.y, rotation.rotation.eulerAngles.z);
        transform.position = Vector3.MoveTowards(transform.position, SetCamPosition(), cameraSmoothing);


        //Calculates the direction of the camera from the player ignoring the y value
        Vector3 direction = (transform.position - new Vector3(CM.GetMidPoint().x, transform.position.y, CM.GetMidPoint().z)).normalized;
        //If the camera is upsidedown then invert the direction so it is on the other side
        if (transform.up.y < 0)
            direction = -direction;

        //Calculates the angle bettween the camera position and the camera direction from the player position
        float rotation = Mathf.Abs(Vector3.SignedAngle(transform.position - CM.GetMidPoint(), direction, CM.GetMidPoint()));
        rotation = transform.position.y > CM.GetMidPoint().y ? rotation : -rotation;

        //If the rotation of the camera attempts to rotate further than 90 deg from the player rotate the camera back to the 90 deg mark, clamping it from over rotating
        if (rotation > topClampAngle && transform.position.y > CM.GetMidPoint().y)
            transform.RotateAround(CM.GetMidPoint(), transform.right, topClampAngle - rotation);
        else if (rotation < bottomClampAngle)
            transform.RotateAround(CM.GetMidPoint(), transform.right, bottomClampAngle - rotation);
    }

    Vector3 SetCamPosition()
    {
        float distanceOffset = Vector3.Distance(CM.characterOne.position, CM.characterTwo.position) * zoomMultiplier;
        Vector3 standardPos = CM.GetMidPoint() - transform.forward * (camOffest * distanceOffset);

        return standardPos - transform.forward * minimum;

    }
}