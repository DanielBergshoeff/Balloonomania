using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatMan : MonoBehaviour
{
    public Vector3Variable PlayerBalloonPosition;
    public FloatReference SpeedIncrease;

    private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = speed * SpeedIncrease.Value;
        transform.position = transform.position + transform.right * Time.deltaTime * speed;
    }
}
