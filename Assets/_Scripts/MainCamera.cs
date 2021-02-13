using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform Player;
    public UnityEngine.Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - Player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UnityEngine.Vector3 newPosition = Player.position + _cameraOffset;

        transform.position = UnityEngine.Vector3.Slerp(transform.position, newPosition, SmoothFactor);
    }
}
