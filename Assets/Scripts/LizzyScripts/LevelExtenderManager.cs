using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExtenderManager : MonoBehaviour
{
    public LevelExtender currentLevelExtender;
    public GameObject playerBalloon;
    public GameObject AIBalloon;
    public int regularMinForwardSegments = 2;
    public GameObject introductionLevelRepeatables;
    public GameObject regularLevelRepeatables;
    public GameObject fatMan;
    [Tooltip("The offset distance at which the fat man will spawn behind the slowest balloon on the x axis.")]
    public float spawnOffset;
    public GameManager gameManager;

    private float slowestBalloonPos;

    // Start is called before the first frame update
    void Start()
    {
        
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
            ChangeLevelRepeatables(); //make sure the new levels to be displayed and extended are the regularLevelRepeatables
            SetMinForwardSegments(); //make sure the wind now changes direction as well
            EnableFatMan(); //enable the fat man behind the slowest ballon at a X distance
            gameObject.SetActive(false);//disable this script so you cannot accedentally reset things
        } 
        //set LevelExtender's Background to regularLevelRepeatables
    }

    void StayAtPlayerHeight() {
        transform.position = new Vector3(transform.position.x, playerBalloon.transform.position.y, transform.position.z);
    }

    private void SetMinForwardSegments() {
        gameManager.MinForwardSegments = regularMinForwardSegments;
    }

    private void ChangeLevelRepeatables() {
        regularLevelRepeatables.SetActive(true);
        introductionLevelRepeatables.SetActive(false);
        currentLevelExtender.Background = regularLevelRepeatables;
    }

    private void EnableFatMan() {

        slowestBalloonPos = Mathf.Max(playerBalloon.transform.position.x, AIBalloon.transform.position.x); //figure out which character is behind
        Debug.Log("Slowest balloon is at " + slowestBalloonPos);
        fatMan.transform.position = new Vector3(slowestBalloonPos - spawnOffset, fatMan.transform.position.y, fatMan.transform.position.z); //set fat man to spawn miniman distance from the player most behind
        fatMan.SetActive(true); //enable fatman
    }
}
    