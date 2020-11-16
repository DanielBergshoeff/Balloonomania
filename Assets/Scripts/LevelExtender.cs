using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExtender : MonoBehaviour
{
    public GameObject Background;
    public float DuplicateDistance;

    private GameObject currentBackground;
    private GameObject previousBackground;

    private void Duplicate() {
        GameObject go = Instantiate(Background);
        go.transform.position = Background.transform.position + Vector3.right * DuplicateDistance;
        previousBackground = Background;
        Background = go;
        transform.position = transform.position + Vector3.right * DuplicateDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Balloon"))
            return;

        Duplicate();
    }
}
