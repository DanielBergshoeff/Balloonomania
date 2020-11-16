using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public float MinHeight = 5f;
    public float MaxHeight = 1000f;

    public Vector3Variable PlayerPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PlayerPosition.Value.x, Mathf.Clamp(PlayerPosition.Value.y, MinHeight, MaxHeight), transform.position.z);
    }
}
