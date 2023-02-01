using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CollisionEvent))]
public class HealthPickup : Interactable
{
    [SerializeField] private float heal;
    private AudioSource gameMusic;
    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
    }

    public override void OnInteract(GameObject target)
    {
        if (target. TryGetComponent<Health>(out Health health)) 
        {
            health.OnApplyHealth(heal);
        }

        if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (gameMusic != null) gameMusic.Play();
        if (destroyOnInteract) Destroy(gameObject);

    }

}
