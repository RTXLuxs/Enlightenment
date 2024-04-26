using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class InteractorTest : MonoBehaviour, IInteractable
{

    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.score = 1;
            Object.Destroy(gameObject);
        }
    }
}
