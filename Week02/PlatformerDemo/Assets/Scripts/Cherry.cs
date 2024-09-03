using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    private PlayerController _player;
    private SpriteRenderer _sRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _sRenderer = transform.GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = (_player.transform.position - transform.position).magnitude;
        if (distance < 1.0f)
        {
            _player.SetSize(1.2f);
            Destroy(gameObject);
        }
    }
}
