using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChageRepeatables : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider) //Once the player hits the end of the introduction level
    {
        Debug.Log("Entering trigger collider");
        
        //set LevelExtender's Background to regularLevelRepeatables
    }
}
