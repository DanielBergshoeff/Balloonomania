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

    [Header("Random values")]
    public int MinSpeed = -3;
    public int MaxSpeed = 3;

    public float MinTimePerSwitch = 3f;
    public float MaxTimePerSwitch = 10f;


    private int[] segments;
    private float[] timeTilSwitch;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        segments = new int[AmtOfSegments];
        timeTilSwitch = new float[AmtOfSegments];
        for (int i = 0; i < segments.Length; i++) {
            AssignNewSpeed(i);
            GameObject go = Instantiate(LinePrefab);
            go.transform.position = new Vector3(0f, i * SegmentSize, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < segments.Length; i++) {
            timeTilSwitch[i] -= Time.deltaTime;
            if (timeTilSwitch[i] <= 0f)
                AssignNewSpeed(i);
        }
    }

    private void AssignNewSpeed(int segment) {
        segments[segment] = Random.Range(MinSpeed, MaxSpeed);
        timeTilSwitch[segment] = Random.Range(MinTimePerSwitch, MaxTimePerSwitch);
    }

    public static int GetSpeed(Vector3 pos) {
        int segment = (int) (pos.y / instance.SegmentSize);
        return instance.segments[segment];
    }
}
