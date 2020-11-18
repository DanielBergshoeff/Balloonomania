using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Segments")]
    public int AmtOfSegments = 5;
    public float SegmentSize = 5f;
    public GameObject LinePrefab;
    public GameObject EffectPrefab;

    [Header("Random values")]
    public int MinSpeed = -3;
    public int MaxSpeed = 3;
    public int TopLaneSpeed = 3;

    public float MinTimePerSwitch = 3f;
    public float MaxTimePerSwitch = 10f;

    public int MinForwardSegments = 2;

    [Header("Collision")]
    public LayerMask CollisionMask;

    private int[] segments;
    private float[] timeTilSwitch;
    private ParticleSystem[] effects;

    public Vector3Variable PlayerBalloonPosition;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        segments = new int[AmtOfSegments];
        timeTilSwitch = new float[AmtOfSegments];
        effects = new ParticleSystem[AmtOfSegments];
        for (int i = 0; i < segments.Length; i++) {
            segments[i] = 1;
            GameObject vfx = Instantiate(EffectPrefab);
            vfx.transform.position = new Vector3(0f, i * SegmentSize, 0f);

            effects[i] = vfx.GetComponentInChildren<ParticleSystem>();
            ParticleSystem.ShapeModule sm = effects[i].shape;
            sm.scale = new Vector3(sm.scale.x, SegmentSize, sm.scale.z);

            effects[i].transform.localPosition = new Vector3(0f, SegmentSize / 2f, 0f);

            AssignNewSpeed(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < segments.Length; i++) {
            timeTilSwitch[i] -= Time.deltaTime;
            if (timeTilSwitch[i] <= 0f)
                AssignNewSpeed(i);

            effects[i].transform.parent.position = new Vector3(PlayerBalloonPosition.Value.x, effects[i].transform.parent.position.y, effects[i].transform.parent.position.z);
        }
    }

    private void AssignNewSpeed(int segment) {
        int val = 0;
        if (segment == segments.Length - 1) {
            val = Random.value < 0.5f ? TopLaneSpeed : -TopLaneSpeed;
            segments[segment] = val;
        }
        else {
            val = Random.value < 0.5f ? 1 : -1;
            if (segments[segment] + val == 0)
                val = val * 2;
            if (segments[segment] + val < 0) {
                if (AmtOfForwardLanes() <= MinForwardSegments)
                    val = 0;
            }
            segments[segment] = Mathf.Clamp(segments[segment] + val, MinSpeed, MaxSpeed);
        }
        timeTilSwitch[segment] = Random.Range(MinTimePerSwitch, MaxTimePerSwitch);

        UpdateLaneVisuals(segment);

        int playerSegment = (int)(PlayerBalloonPosition.Value.y / instance.SegmentSize);
        if (playerSegment == segment) {
            AudioManager.PlaySound(val > 0 ? Sound.WindFaster : Sound.WindSlower);
        }
    }

    private void UpdateLaneVisuals(int segment) {
        ParticleSystem.VelocityOverLifetimeModule vom = effects[segment].velocityOverLifetime;
        vom.x = segments[segment] * 3f;

        ParticleSystem.NoiseModule nm = effects[segment].noise;
        nm.strength = segments[segment] * 0.4f;
    }

    private int AmtOfForwardLanes() {
        int count = 0;
        for (int i = 0; i < segments.Length; i++) {
            if (segments[i] > 0)
                count++;
        }
        return count;
    }

    public static int GetSpeed(Vector3 pos) {
        int segment = (int) (pos.y / instance.SegmentSize);

        if (segment < 0 || segment >= instance.segments.Length)
            return 0;

        return instance.segments[segment];
    }

    public static float GetDirection(Vector3 pos, int segmentBuffer, bool left) {
        int segment = (int)(pos.y / instance.SegmentSize);

        if (segment < 0) 
            return -1;
        else if (segment >= instance.segments.Length)
            return 1;

        int speed = instance.segments[segment];
        int topSpeed = 0;
        for (int i = -segmentBuffer; i <= segmentBuffer; i++) {
            if (segment + i < instance.segments.Length && segment + i >= 0) {
                if ((instance.segments[segment + i] > speed && !left) || (instance.segments[segment + i] < speed && left )) {
                    float dist = (segment + i) * instance.SegmentSize - pos.y;
                    RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.up * i, dist, instance.CollisionMask);
                    Debug.DrawRay(pos, Vector3.up * i * dist, Color.magenta);

                    if (hit.collider == null) {
                        speed = instance.segments[segment + i];
                        topSpeed = i;
                    }
                }
            }
        }

        float dir = pos.y - (segment + topSpeed) * instance.SegmentSize;

        return dir;
    }
}
