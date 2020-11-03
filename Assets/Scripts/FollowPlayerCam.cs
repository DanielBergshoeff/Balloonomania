using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public float MinHeight = 5f;
    public float MaxHeight = 1000f;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, Mathf.Clamp(player.position.y, MinHeight, MaxHeight), transform.position.z);
    }
}
