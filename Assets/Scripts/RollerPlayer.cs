using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody))]

public class RollerPlayer : MonoBehaviour
{
    [SerializeField] private Transform view;
    [SerializeField] private float maxForce = 5;

    [SerializeField] private float groundRayLength = 1;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 force;
    private Rigidbody rb;
    private int score;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = Camera.main.transform;
        Camera.main.GetComponent<RollerCamera>().SetTarget(transform);

        GetComponent<Health>().onDamage += OnDamage;
        GetComponent<Health>().onDeath += OnDeath;
        GetComponent<Health>().onHeal += OnHeal;
        RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        force = viewSpace * (direction * maxForce);

        Ray ray = new Ray(transform.position, Vector3.down);
        bool onGround = Physics.Raycast(ray, groundRayLength, groundLayer);
        Debug.DrawRay(transform.position, ray.direction * groundRayLength);

        if (onGround && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(force);
    }

    public void AddPoints(int points)
    {
        score += points;
        RollerGameManager.Instance.SetScore(score);
        Debug.Log(score);
    }

    public void OnHeal()
    {
        RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }
    public void OnDamage()
    {
        RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    public void OnDeath()
    {
        RollerGameManager.Instance.SetGameOver();
        Destroy(gameObject);
    }
}
