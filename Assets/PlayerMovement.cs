using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkMoveStopRadius = 0.2f;
    ThirdPersonCharacter _character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster _cameraRaycaster;
    Vector3 _currentClickTarget;
        
    private void Start()
    {
        _cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        _character = GetComponent<ThirdPersonCharacter>();
        _currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            print("Cursor raycast hit layer: " + _cameraRaycaster.layerHit);
            switch (_cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    _currentClickTarget = _cameraRaycaster.Hit.point;
                    break;
                case Layer.Enemy:
                    print("Not moving to enemy");
                    break;
                default:
                    print("Unexpected layer found");
                    return;
            }
        }
        var playerToClickPoint = _currentClickTarget - transform.position;

        if (playerToClickPoint.magnitude >= _walkMoveStopRadius)
        {
            _character.Move(playerToClickPoint, false, false);
        }
        else
        {
            _character.Move(Vector3.zero, false, false);
        }
    }
}

