using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Vector3Variable PlayerBalloonPos;
    public GameObject TutorialSpritePrefab;
    public FloatReference TutorialTime;

    private List<GameObject> currentTuts;

    // Start is called before the first frame update
    void Start()
    {
        currentTuts = new List<GameObject>();
    }

    private void Update() {
        foreach(GameObject go in currentTuts) {
            go.transform.position = PlayerBalloonPos.Value + Vector3.right * 5f;
        }
    }

    public void StartTutorial(Sprite sprite) {
        GameObject go = Instantiate(TutorialSpritePrefab);
        go.GetComponent<SpriteRenderer>().sprite = sprite;
        currentTuts.Add(go);
        StartCoroutine(RemoveTutorial(go, TutorialTime.Value));
    }

    private IEnumerator RemoveTutorial(GameObject tut, float time) {
        yield return new WaitForSeconds(time);

        currentTuts.Remove(tut);
        Destroy(tut);
    }
}
