using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGettingStabbed : MonoBehaviour
{
    public GameObjectVariable HolePrefab;
    public IntVariable MaxHoles;
    public FloatReference Heat;
    public FloatReference HoleHeatLoss;
    public FloatReference TimeForHoleFix;

    protected List<GameObject> stabs;
    protected List<GameObject> fixingStabs;
    protected float stabbedCooldown = 0f;

    protected void Awake()
    {
        stabs = new List<GameObject>();
        fixingStabs = new List<GameObject>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Heat.Value > 0f) {
            foreach (GameObject go in stabs) {
                Heat.Value -= HoleHeatLoss.Value * Time.deltaTime;
            }
            foreach (GameObject go in fixingStabs) { 
                Heat.Value -= HoleHeatLoss.Value * Time.deltaTime;
            }
        }

        if (stabbedCooldown > 0f)
            stabbedCooldown -= Time.deltaTime;
    }

    public void Stabbed(Vector3 pos, Vector3 normal) {
        if (stabs.Count >= MaxHoles.Value)
            return;

        stabbedCooldown = 0.5f;

        GameObject go = Instantiate(HolePrefab.Value);
        go.transform.position = new Vector3(pos.x, pos.y, go.transform.position.z);
        go.transform.rotation = Quaternion.LookRotation(-normal, Vector3.up);
        go.transform.parent = transform;
        stabs.Add(go);
    }

    protected IEnumerator FixHole(GameObject hole) {
        stabs.Remove(hole);
        fixingStabs.Add(hole);
        hole.transform.DOScale(0f, TimeForHoleFix.Value);

        yield return new WaitForSeconds(TimeForHoleFix.Value);

        fixingStabs.Remove(hole);
        Destroy(hole);
    }
}
