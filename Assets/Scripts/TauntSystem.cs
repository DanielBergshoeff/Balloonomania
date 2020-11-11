using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TauntSystem : MonoBehaviour
{
    public GameEventBalloonInfo TauntEvent;
    public BalloonInfo bi;

    public SpriteListVariable Taunts;
    public GameObject TauntPrefab;

    public FloatReference TauntLength;

    private List<BalloonInfoTaunt> currentTaunts;

    private void Awake() {
        currentTaunts = new List<BalloonInfoTaunt>();
    }

    private void Start() {
        //TauntEvent.Raise(bi);
    }

    public void Taunt(BalloonInfo bi) {
        foreach(BalloonInfoTaunt b in currentTaunts) {
            if (b.Balloon == bi)
                return;
        }

        GameObject go = Instantiate(TauntPrefab);
        go.transform.position = bi.transform.position + Vector3.up * 1f + Vector3.right * 2f;
        go.GetComponentInChildren<Image>().sprite = GetRandomTaunt();
        go.transform.parent = bi.transform;

        BalloonInfoTaunt bit = new BalloonInfoTaunt(bi, go);
        currentTaunts.Add(bit);

        StartCoroutine(RemoveTaunt(TauntLength.Value, bit));
    }

    private Sprite GetRandomTaunt() {
        return Taunts.Value[Random.Range(0, Taunts.Value.Count)];
    }

    private IEnumerator RemoveTaunt(float time, BalloonInfoTaunt taunt) {
        yield return new WaitForSeconds(time);
        Destroy(taunt.Taunt);
        currentTaunts.Remove(taunt);
    }
}

public class BalloonInfoTaunt
{
    public BalloonInfo Balloon;
    public GameObject Taunt;

    public BalloonInfoTaunt(BalloonInfo bi, GameObject t) {
        Balloon = bi;
        Taunt = t;
    }
}
