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

    public float MinTimePerSwitch = 3f;
    public float MaxTimePerSwitch = 10f;


    private int[] segments;
    private float[] timeTilSwitch;
    private ParticleSystem[] effects;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        segments = new int[AmtOfSegments];
        timeTilSwitch = new float[AmtOfSegments];
        effects = new ParticleSystem[AmtOfSegments];
        for (int i = 0; i < segments.Length; i++) {
            GameObject vfx = Instantiate(EffectPrefab);
            vfx.transform.position = new Vector3(0f, i * SegmentSize, 0f);

            effects[i] = vfx.GetComponentInChildren<ParticleSystem>();
            ParticleSystem.ShapeModule sm = effects[i].shape;
            sm.scale = new Vector3(sm.scale.x, SegmentSize, sm.scale.z);

            effects[i].transform.localPosition = new Vector3(0f, SegmentSize / 2f, 0f);

            AssignNewSpeed(i);
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < segments.Length; i++) {
            timeTilSwitch[i] -= Time.deltaTime;
            if (timeTilSwitch[i] <= 0f)
                AssignNewSpeed(i);

            effects[i].transform.parent.position = new Vector3(player.transform.position.x, effects[i].transform.parent.position.y, effects[i].transform.parent.position.z);
        }
    }

    private void AssignNewSpeed(int segment) {
        segments[segment] = Mathf.Clamp(segments[segment] + ((Random.value < 0.5f) ? 1 : -1) , MinSpeed, MaxSpeed);
        timeTilSwitch[segment] = Random.Range(MinTimePerSwitch, MaxTimePerSwitch);
        ParticleSystem.VelocityOverLifetimeModule vom = effects[segment].velocityOverLifetime;
        vom.x = segments[segment] * 3f;

        ParticleSystem.NoiseModule nm = effects[segment].noise;
        nm.strength = segments[segment] * 0.4f;

        /*ParticleSystem.TrailModule tm = effects[segment].trails;
        tm.lifetime = 0.4f / segments[segment];*/
    }

    public static int GetSpeed(Vector3 pos) {
        int segment = (int) (pos.y / instance.SegmentSize);
        return instance.segments[segment];
    }

    public static int GetDirection(Vector3 pos, int segmentBuffer) {
        int segment = (int)(pos.y / instance.SegmentSize);
        int speed = instance.segments[segment];
        int topSpeed = 0;
        for (int i = -segmentBuffer; i < segmentBuffer; i++) {
            if(segment + i < instance.segments.Length && segment + i >= 0) {
                if(instance.segments[segment + i] > speed) {

                }
            }
        }

        return 0;
    }
}
