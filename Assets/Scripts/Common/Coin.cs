using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class Coin : Interactable
{
    private AudioSource gameMusic;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void OnInteract(GameObject go)
    {
        var player = FindObjectOfType<RollerPlayer>();
        { 
            player.AddPoints(100);
        }

        if(interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if(gameMusic != null) gameMusic.Play();
        if(destroyOnInteract) Destroy(gameObject);
    }
}
