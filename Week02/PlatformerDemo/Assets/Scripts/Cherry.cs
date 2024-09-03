using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    public PlayerController Player;
    private SpriteRenderer _sRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _sRenderer = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (Player.transform.position - transform.position).magnitude;
        if (distance < 1.0f)
        {
            Player.SetSize(1.2f);
            _sRenderer.enabled = false;
            enabled = false;
            Destroy(gameObject);
        }
    }
}
