using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExtenderManager : MonoBehaviour
{
    public LevelExtender currentLevelExtender;
    public GameObject playerBalloon;
    public GameObject introductionLevelRepeatables;
    public GameObject regularLevelRepeatables;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LevelExtenderManager online!");
    }

    // Update is called once per frame
    void Update()
    {
        StayAtPlayerHeight();
    }

    private void OnTriggerEnter2D(Collider2D collider) //Once the player hits the end of the introduction level
    {
        Debug.Log("Entering trigger collider with " + collider.name + " of " + collider.transform.parent); //check who is colliding
        if (collider.transform.parent.name == "PlayerBalloon"){
            ChangeLevelRepeatables();
        } 
        //set LevelExtender's Background to regularLevelRepeatables
    }

    void StayAtPlayerHeight() {
        transform.position = new Vector3(transform.position.x, playerBalloon.transform.position.y, transform.position.z);
    }
    private void ChangeLevelRepeatables() {
        regularLevelRepeatables.SetActive(true);
        introductionLevelRepeatables.SetActive(false);
    }
}
