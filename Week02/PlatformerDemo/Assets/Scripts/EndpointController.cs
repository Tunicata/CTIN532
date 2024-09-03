using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndpointController : MonoBehaviour
{
    private bool playerInsideTrigger = false;
    private float delay = 0.5f;
    private PlayerController _player;
    public string NextSceneName;

    public void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public void FixedUpdate()
    {
        
        if (Mathf.Abs(transform.position.x - _player.transform.position.x) < 1.0f) { 
            LoadNextScene();
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            Invoke("LoadNextScene", delay);
        }
    }

    private void LoadNextScene()
    {
        //if (playerInsideTrigger)
        //{
            // Load the next scene by name
            SceneManager.LoadScene(NextSceneName);
        //}
    }
}
