using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1, 60), Tooltip("Controls the speed of the player")] public float speed = 10;
    [Range(1, 360)] public float rotationRate = 180;
    public GameObject prefab;
    public Transform[] bulletSpawn;

    private void Awake()
    {
        Debug.Log("awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(2, 3, 2);
        //transform.rotation = Quaternion.Euler(30, 30, 30);
        //transform.localScale = Vector3.one * Random.value * 5;

        Vector3 direction = Vector3.zero; // Vecter{0,0,0}
        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");

        Quaternion rotate = Quaternion.Euler(rotation * rotationRate * Time.deltaTime);
        transform.rotation = transform.rotation * rotate;
        transform.Translate(direction * speed * Time.deltaTime);
        //transform.position += direction * speed * Time.deltaTime;

        if(Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("pew!");
            //GetComponent<AudioSource>().Play();
            GameObject go1 = Instantiate(prefab, bulletSpawn[0].position, bulletSpawn[0].rotation);
            GameObject go2 = Instantiate(prefab, bulletSpawn[1].position, bulletSpawn[1].rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AstroidGameManager>()?.SetGameOver();
        }
    }
}
