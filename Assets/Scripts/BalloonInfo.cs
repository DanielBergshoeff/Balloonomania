using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonInfo : MonoBehaviour
{
    public Transform BalloonPart;
    public Vector3Variable BalloonPartPosition;
    public bool IsPlayer = false;

    // Update is called once per frame
    void Update()
    {
        BalloonPartPosition.Value = BalloonPart.position;
    }
}
