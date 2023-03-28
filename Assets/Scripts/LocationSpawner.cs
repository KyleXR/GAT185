using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;
public class LocationSpawner : Spawner
{
    [SerializeField] GameObject spawnPrefab;

    Transform[] locations;

    private new void Start()
    {
        // call spawner parent start 
        base.Start();
        // get all transform children under this game object 
        // gets parent transform also, uses linq to exclude parent 
        locations = transform.GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
    }
    public override void Clear()
    {
    }
    public override void Spawn()
    {
        // spawn at random location 
        Transform t = locations[Random.Range(0, (locations.Length - 1))];
        Instantiate(spawnPrefab, t.position, t.rotation);
    }

}
