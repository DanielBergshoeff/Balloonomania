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
        if (SwordTarget.transform.position.x > transform.position.x)
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

