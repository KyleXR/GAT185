using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class TheKey : Interactable
{
    public override void OnInteract(GameObject target)
    {
        var player = FindObjectOfType<RollerPlayer>();
        {
            
            RollerGameManager.Instance.SetGameWon();
        }

        if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
