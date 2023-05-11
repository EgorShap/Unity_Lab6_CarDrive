using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float speedX = 360f;
    public float speedY = 240f;
    public float limitY = 40f;
    public float minDistance = 1.5f;

    private float _maxDistance;
    private Vector3 _localPosition;
    private float _currentYRotation;

    private Vector3 _position{
        get { return transform.position;}
        set { transform.position = value;}
    }

    void Start(){
        _localPosition = target.InverseTransformPoint(_position);
        _maxDistance = Vector3.Distance(_position, target.position);
    }

    void CameraRotation(){
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        if (mouseY != 0){
            var tmp = Mathf.Clamp(_currentYRotation + mouseY * speedY * Time.deltaTime, -limitY, limitY);
            if (tmp != _currentYRotation){
                var rot = tmp - _currentYRotation;
                transform.RotateAround(target.position, transform.right, rot);
                _currentYRotation = tmp;
            }
        }

        if (mouseX != 0){
            transform.RotateAround(target.position, Vector3.up, mouseX * speedX * Time.deltaTime);
        }

        transform.LookAt(target);
    }
    
    void LateUpdate()
    {
        _position = target.TransformPoint(_localPosition);
        CameraRotation();
        _localPosition = target.InverseTransformPoint(_position);
    }
}
