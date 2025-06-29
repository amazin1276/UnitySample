using System.Collections;
using System.Collections.Generic;
using Expansion;
using TMPro;
using UniRx.Triggers;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField]
    GameObject camera;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Vector2 offSet;
    [SerializeField]
    float flowSpeed;
    const float CAMERA_POSITION_OF_Z = -100;
    const float LOWEST_CAMERA_POSITION = -23;
    const float HIGHEST_CAMERA_POSITION = 23;

    void Update()
    {
        FlowPlayer();

        OffsetLimitVerticalPosition();
        OffsetCameraPosZ();
    }

    void FlowPlayer()
    {
        camera.transform.position = Vector2.Lerp(
            camera.transform.position,
            (Vector2)player.transform.position + offSet, 
            flowSpeed * Time.deltaTime);
    }

    void OffsetCameraPosZ()
    {
        Vector3 _fixedPos = camera.transform.position;
        _fixedPos.z = CAMERA_POSITION_OF_Z;
        camera.transform.position = _fixedPos;
    }


    void OffsetLimitVerticalPosition()
    {
        Vector2 _camPos = camera.transform.position;
        bool _isReachHighestPosition = _camPos.y > HIGHEST_CAMERA_POSITION;
        if (_isReachHighestPosition)
            camera.transform.position = new Vector2(_camPos.x, HIGHEST_CAMERA_POSITION);

        bool _isReachLowestPosition = _camPos.y < LOWEST_CAMERA_POSITION;
        if (_isReachLowestPosition)
            camera.transform.position = new Vector2(_camPos.x, LOWEST_CAMERA_POSITION);
    }
}
