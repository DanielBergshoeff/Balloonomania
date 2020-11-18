using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatMan : MonoBehaviour
{
    public Vector3Variable PlayerBalloonPosition;
    public FloatReference SpeedIncreaseExponential;
    public FloatReference SpeedIncreaseLinear;
    public GameEvent EndGameEvent;
    public ZoomEvent MyZoomEvent;
    public IntVariable Score;

    public GameObject AIPrefab;

    private float speed = 1f;
    private AudioSource myAudioSource;

    private void Awake() {
        myAudioSource = gameObject.AddComponent<AudioSource>();
        myAudioSource.spatialBlend = 1f;
        myAudioSource.clip = AudioManager.GetSound(Sound.FatMan);
        myAudioSource.loop = true;
        myAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        speed = speed * SpeedIncreaseExponential.Value;
        speed += Time.deltaTime * SpeedIncreaseLinear.Value;
        transform.position = transform.position + transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.CompareTag("Balloon")) return;

        if (collision.collider.GetComponentInParent<BalloonInfo>().IsPlayer) {
            speed = 0f;
            EndGameEvent.Raise();
        }
        else {
            Score.Value += 1000;
            speed = 1f;
            MyZoomEvent.Raise(new Zoom(transform, -5f, 1f));

            BalloonFlying bf = collision.collider.GetComponentInParent<BalloonFlying>();
            if(bf != null)
                bf.enabled = false;

            StartCoroutine(RespawnAI(collision.collider.transform.parent.gameObject, 5f));
        }

        AudioManager.PlaySound(Sound.Squashed);
    }

    private IEnumerator RespawnAI(GameObject currentAI, float time) {
        yield return new WaitForSeconds(time);

        Destroy(currentAI);
        GameObject go = Instantiate(AIPrefab);
        go.transform.position = PlayerBalloonPosition.Value + Vector3.right * 30f;
    }
}
