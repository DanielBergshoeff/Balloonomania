using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExtenderManager : MonoBehaviour
{
    public LevelExtender currentLevelExtender;
    public GameObject playerBalloon;
    public GameObject regularLevelRepeatables;
    public float xPosOffset = 3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LevelExtenderManager online!");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, playerBalloon.transform.position.y, transform.position.z);
    }



    private void OnTriggerEnter2D(Collider2D collider) //Once the player hits the end of the introduction level
    {
        Debug.Log("Entering trigger collider");
        if (collider == playerBalloon){ 
            Debug.Log("YEAAAAAAAAH!!!");
        } 
        //set LevelExtender's Background to regularLevelRepeatables
    }
}
