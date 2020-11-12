using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuyTurnAround : MonoBehaviour
{

    public GameObject SwordTarget;
    Vector3 SwordTargetStartPos;
    Vector3 localScale;
  
    void Start()
    {
        localScale = transform.localScale;
        SwordTargetStartPos = SwordTarget.transform.position;
    }

    
    void Update()
    {

        Vector3 worldPosition;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + Mathf.Abs((Camera.main.transform.position.z - SwordTargetStartPos.z));
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);


        SwordTarget.transform.position = worldPosition;


        if (SwordTarget.transform.localPosition.x > transform.position.x)
        {
            localScale.x = -1;
            transform.localScale = localScale;
        }
       else
        {
            localScale.x = 1;
            transform.localScale = localScale;
        }
        
       
    }
}

