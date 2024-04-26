using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{

    public Transform InteractorSource;
    public float InteractRange;

    public bool lookingAt;

    // Start is called before the first frame update
    void Start()
    {
        lookingAt = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactobj))
            {
                lookingAt = true;
                interactobj.Interact();
            }
        }
        else
        {
            lookingAt = false;
        }
    }
}
