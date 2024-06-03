using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed;
    
    void Update()
    {
        var abc = Vector2.Lerp(transform.position, target.position, cameraSpeed * Time.deltaTime);
        transform.position = new Vector3(abc.x, abc.y, -10);
    }
}
