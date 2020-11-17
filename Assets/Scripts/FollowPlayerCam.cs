using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public float MinHeight = 5f;
    public float MaxHeight = 1000f;
    public FloatReference ZoomTime;

    public Vector3Variable PlayerPosition;

    private bool stopFollow = false;
    private Zoom currentZoom;
    private Vector3 startPosition;
    private float startZoomTime;
    private bool moving = false;
    private bool zoomed = false;

    private float startZ = 0f;

    private void Awake() {
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow)
            transform.position = new Vector3(PlayerPosition.Value.x, Mathf.Clamp(PlayerPosition.Value.y, MinHeight, MaxHeight), transform.position.z);
        else {
            if (moving) {
                if (!zoomed)
                    UpdateZoom();
                else
                    ReturnFromZoom();
            }
            else {
                transform.position = new Vector3(currentZoom.Target.position.x, currentZoom.Target.position.y, startZ + currentZoom.ZoomDistance);
            }
        }
        
    }

    public void OnZoom(Zoom z) {
        zoomed = false;
        stopFollow = true;
        currentZoom = z;
        startPosition = transform.position;
        startZoomTime = Time.time;
        moving = true;
    }

    private void UpdateZoom() {
        float t = (Time.time - startZoomTime) / ZoomTime.Value;
        if(t > 1f) {
            Invoke("ReturnZoom", currentZoom.ZoomTime);
            moving = false;
            return;
        }
        transform.position = Vector3.Lerp(startPosition, new Vector3(currentZoom.Target.position.x, currentZoom.Target.position.y, startZ + currentZoom.ZoomDistance), t);
    }

    private void ReturnFromZoom() {
        float t = (Time.time - startZoomTime) / ZoomTime.Value;
        transform.position = Vector3.Lerp(startPosition, new Vector3(PlayerPosition.Value.x, Mathf.Clamp(PlayerPosition.Value.y, MinHeight, MaxHeight), startZ), t);
        if(t > 1f) {
            stopFollow = false;
        }
    }

    private void ReturnZoom() {
        zoomed = true;
        startZoomTime = Time.time;
        startPosition = transform.position;
        moving = true;
    }
}
