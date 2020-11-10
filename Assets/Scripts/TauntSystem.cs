using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TauntSystem : MonoBehaviour
{
    public GameEventBalloonInfo TauntEvent;
    public BalloonInfo bi;

    public StringListVariable Taunts;
    public GameObject TauntPrefab;

    public FloatReference TauntLength; 

    private void Start() {
        TauntEvent.Raise(bi);
    }

    public void Taunt(BalloonInfo bi) {
        GameObject go = Instantiate(TauntPrefab);
        go.transform.position = bi.transform.position + Vector3.up * 2f + Vector3.right * 3f;
        go.GetComponentInChildren<TextMeshProUGUI>().text = GetRandomTaunt();
        go.transform.parent = bi.transform;

        StartCoroutine(RemoveTaunt(TauntLength.Value, go));
    }

    private string GetRandomTaunt() {
        return Taunts.Value[Random.Range(0, Taunts.Value.Count)].Value;
    }

    private IEnumerator RemoveTaunt(float time, GameObject taunt) {
        yield return new WaitForSeconds(time);
        Destroy(taunt);
    }
}
