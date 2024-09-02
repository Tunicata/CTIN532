using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 3f;

    private RaycastHit hit;

    private Ray ray;

    private Vector3 endPosition;

    Vector3 step;
    private Quaternion _rotation;
    private Action _postMove = () => { };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            SetEndPos(ray.origin, () => { });
        }

        Move();
        Stop();
    }

    public void Move()
    {
        if (step != Vector3.zero)
        {
            // transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, 0.2f);

            
            transform.Translate(step * speed * Time.deltaTime, Space.World);
        }
    }

    public void Stop()
    {
        float distance = Vector3.Distance(transform.position, endPosition);
        if (distance < 1f)
        {
            step = Vector3.zero;
            _postMove();
            _postMove = () => { };
        }
    }

    public void SetEndPos(Vector3 inPos, Action postMove)
    {
        endPosition.x = inPos.x;
        endPosition.y = inPos.y;
        step = (endPosition - transform.position).normalized;
        transform.localEulerAngles = new Vector3(0f, (step.x < 0) ? -180f : 0f, 0f);

        _postMove = postMove;
    }
}
