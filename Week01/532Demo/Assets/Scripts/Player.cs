using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioSource PlayerAudio;
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
        bool key_up = Input.GetKey(KeyCode.W);
        bool key_down = Input.GetKey(KeyCode.S);
        bool key_left = Input.GetKey(KeyCode.A);
        bool key_right = Input.GetKey(KeyCode.D);
        bool key_q = Input.GetKey(KeyCode.Q);
        bool key_e = Input.GetKey(KeyCode.E);

        float hSpeed = (key_right ? 1 : 0) - (key_left ? 1 : 0);
        float vSpeed = (key_up ? 1 : 0) - (key_down ? 1 : 0);
        float rSpeed = (key_q ? 1 : 0) - (key_e ? 1 : 0);

        transform.position += (new Vector3(hSpeed, vSpeed) * 0.05f);
        transform.localEulerAngles += new Vector3(0, 0, rSpeed);

        if (hSpeed + vSpeed != 0f)
        {
            if (!PlayerAudio.isPlaying)
                PlayerAudio.Play();
        }

        // Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // float lookAngle = Mathf.Atan2(mouseWorldPos.y - transform.position.y, mouseWorldPos.x - transform.position.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
        
        Move();
        Stop();
    }

    public void Move()
    {
        if (step != Vector3.zero)
        {
            // transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, 0.2f);
            if (!PlayerAudio.isPlaying)
                PlayerAudio.Play();
            
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
            PlayerAudio.Stop();
        }
    }

    public void SetEndPos(Vector3 inPos, Action postMove)
    {
        endPosition.x = inPos.x;
        endPosition.y = inPos.y;
        step = (endPosition - transform.position).normalized;
        // transform.localEulerAngles = new Vector3(0f, (step.x < 0) ? -180f : 0f, 0f);
        // transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        _postMove = postMove;
    }
}
