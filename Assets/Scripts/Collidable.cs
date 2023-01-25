using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Collidable : MonoBehaviour
{
    [SerializeField] private string hitTagName = string.Empty;

    public delegate void CollisionEvent(GameObject other);
    public CollisionEvent OnEnter;
    public CollisionEvent OnExit;
    public CollisionEvent OnStay;

    private void OnCollisionEnter(Collision collision)
    {
        if(hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
        {
            OnEnter.Invoke(collision.gameObject);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
        {
            OnExit.Invoke(collision.gameObject);

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (hitTagName == string.Empty || collision.gameObject.CompareTag(hitTagName))
        {
            OnStay.Invoke(collision.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
        {
            OnEnter.Invoke(other.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
        {
            OnExit.Invoke(other.gameObject);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hitTagName == string.Empty || other.gameObject.CompareTag(hitTagName))
        {
            OnStay.Invoke(other.gameObject);

        }
    }
}
