using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
    public static Vector3 Position;
    public static int Bounds = 5;

    [Header("Spawning")]
    [SerializeField] int spawnBounds = 10;
    [SerializeField] int spawnCount = 10;
    [SerializeField] GameObject spawnObject;

    [Header("Firefly")]
    [SerializeField] float flashLevel = 250f;
    [Range(0.01f, 0.99f)]
    [SerializeField] float flashBoot = .1f;
    [SerializeField] float flashDetectionRange = 5f;
    [SerializeField] float speed = 5f;

    private void Awake()
    {
        Position = transform.position;
        Bounds = spawnBounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var bounds = (float)spawnBounds / 2;

            var position = new Vector3(Random.Range(-bounds, bounds),
                                       Random.Range(-bounds, bounds),
                                       Random.Range(-bounds, bounds));

            var firefly = Instantiate(spawnObject,
                                      position,
                                      Quaternion.identity);

            firefly.GetComponent<Firefly>().SetValues(flashLevel,
                                                      flashBoot,
                                                      flashDetectionRange);

            firefly.GetComponent<FireflyFlight>().SetValues(speed, transform.position, bounds);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnBounds, spawnBounds, spawnBounds));
    }
}
