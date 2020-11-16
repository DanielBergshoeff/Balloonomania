using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnEvent : MonoBehaviour
{
    public List<MonoBehaviour> ScriptsToEnable;

    // Start is called before the first frame update
    void Start() {
        foreach(MonoBehaviour mb in ScriptsToEnable) {
            mb.enabled = false;
        }
    }

    public void EnableScripts() {
        foreach(MonoBehaviour mb in ScriptsToEnable) {
            mb.enabled = true;
        }
    }
}
