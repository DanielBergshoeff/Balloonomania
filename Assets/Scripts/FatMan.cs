using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FatMan : MonoBehaviour
{
    public FloatReference GlobalSpeedMultiplier;
    public Vector3Variable PlayerBalloonPosition;
    public FloatReference SpeedIncreaseExponential;
    public FloatReference SpeedIncreaseLinear;
    public GameEvent EndGameEvent;
    public ZoomEvent MyZoomEvent;
    public GameEventVector3 ScoreEvent;

    public GameObject AIPrefab;

    private float speed = 1f;
    private AudioSource myAudioSource;

    private void Awake() {
        AudioMixer mixer = Resources.Load("Main") as AudioMixer;
        AudioMixerGroup amx = mixer.FindMatchingGroups("Rest")[0];

        myAudioSource = gameObject.AddComponent<AudioSource>();
        myAudioSource.spatialBlend = 1f;
        myAudioSource.clip = AudioManager.GetSound(Sound.FatMan);
        myAudioSource.loop = true;
        myAudioSource.outputAudioMixerGroup = amx;
        myAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        speed = speed * SpeedIncreaseExponential.Value;
        speed += Time.deltaTime * SpeedIncreaseLinear.Value;
        transform.position = transform.position + transform.right * Time.deltaTime * speed * GlobalSpeedMultiplier.Value;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.CompareTag("Balloon")) return;

        if (collision.collider.GetComponentInParent<BalloonInfo>().IsPlayer) {
            speed = 0f;
            EndGameEvent.Raise();
        }
        else {
            ScoreEvent.Raise(new Vector3(1000f, 0f, 0f));
            speed = 1f;
            MyZoomEvent.Raise(new Zoom(transform, -5f, 1f));

            BalloonFlying bf = collision.collider.GetComponentInParent<BalloonFlying>();
            if(bf != null)
                bf.enabled = false;

            collision.collider.enabled = false;

            StartCoroutine(RespawnAI(collision.collider.transform.parent.gameObject, 5f));

            GlobalSpeedMultiplier.Value += 0.3f;
        }

        AudioManager.PlaySound(Sound.Squashed);
    }

    private IEnumerator RespawnAI(GameObject currentAI, float time) {
        yield return new WaitForSeconds(time);

        Destroy(currentAI);
        GameObject go = Instantiate(AIPrefab);
        Vector3 spawnPos = PlayerBalloonPosition.Value + Vector3.right * 30f;
        spawnPos = new Vector3(spawnPos.x, 5f, spawnPos.z);
        go.transform.position = spawnPos;
    }

    private void OnDestroy() {
        GlobalSpeedMultiplier.Value = 1f;
    }
}
