using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BalloonFlying : MonoBehaviour
{
    [Header("References")]
    public FloatReference GlobalSpeedMultiplier;
    public Vector3Variable BalloonPartPosition;
    public Transform Fire;

    [Header("Heat information")]
    public FloatReference MaxHeat;
    public FloatReference AddHeatPerSecond;
    public FloatReference RemoveHeatPerSecond;
    public FloatReference StandardHeatLoss;
    
    [SerializeField] protected FloatReference Heat;

    [Header("Speed")]
    public FloatReference UpwardVelocity;
    public FloatReference MaxUpwardVelocity;
    public FloatReference HorizontalSpeed;

    protected Rigidbody2D myRigidbody;
    protected bool grounded;
    private AudioSource myAudioSource;
    private bool heating = false;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        Heat.Value = 0f;

        AudioMixer mixer = Resources.Load("Main") as AudioMixer;
        AudioMixerGroup amx = mixer.FindMatchingGroups("Balloon")[0];

        myAudioSource = gameObject.AddComponent<AudioSource>();
        myAudioSource.spatialBlend = 1;
        myAudioSource.outputAudioMixerGroup = amx;
    }

    private void Start() {
        myAudioSource.clip = AudioManager.GetSound(Sound.Ascend);
        myAudioSource.loop = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Heat.Value > 0f) {
            Heat.Value -= StandardHeatLoss.Value * Time.deltaTime;
        }

        Fire.transform.localScale = Vector3.one * (Heat.Value / MaxHeat.Value);

        grounded = Physics2D.Raycast(transform.position - transform.up * 0.01f, -transform.up, 0.15f).collider != null;
        if (!grounded)
            transform.position = transform.position + transform.right * Time.deltaTime * HorizontalSpeed.Value * GameManager.GetSpeed(BalloonPartPosition.Value) * GlobalSpeedMultiplier.Value;
    }

    protected void FixedUpdate() {
        myRigidbody.AddForce(transform.up * Heat.Value * UpwardVelocity.Value);

        if (myRigidbody.velocity.magnitude > MaxUpwardVelocity.Value) {
            Vector2 vel = myRigidbody.velocity.normalized;
            myRigidbody.velocity = vel * MaxUpwardVelocity.Value;
        }
    }

    protected void ApplyHeat() {
        if (Heat.Value < MaxHeat.Value) {
            Heat.Value += AddHeatPerSecond.Value * Time.deltaTime;
            if (!heating) {
                myAudioSource.Play();
                heating = true;
            }
        }
    }

    protected void StopHeat() {
        if (heating) {
            myAudioSource.Stop();
            heating = false;
        }
    }

    protected void RemoveHeat() {
        if (Heat.Value > 0f) {
            Heat.Value -= RemoveHeatPerSecond.Value * Time.deltaTime;
        }
    }
}
