using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyFiguresMove : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float frequency = 20f;

    [SerializeField]
    float magnitude = 0.5f;

    [SerializeField]
    Vector2 moveRange = new Vector2(-5f, 5f);



    int facingRight = 1;

    Vector3 pos, localScale;

    Vector2 localRange;

 
    void Start()
    {
        pos = transform.position;

        localScale = transform.localScale;

        localRange.x = pos.x + moveRange.y;
        localRange.y = pos.x + moveRange.x;
    }

    void Update()
    {

        if (transform.position.x >= localRange.x || transform.position.x <= localRange.y)
        {
            facingRight *= -1;

            localScale.x *= -1;

            transform.localScale = localScale;
        }


        pos.y += Mathf.Sin(Time.time * frequency) * magnitude;
        pos.x += Time.deltaTime * moveSpeed * facingRight;


        transform.position = pos;

    }
}
