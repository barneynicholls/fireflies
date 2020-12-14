using System.Collections;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    [Header("Flash")]
    [SerializeField] private LayerMask flashLayer;

    [Header("Display")]
    [SerializeField] private GameObject normal;
    [SerializeField] private GameObject flashing;

    float powerLevel = 0f;

    float flashAtLevel;
    float flashDetectedBoost;
    float flashRange;

    public void SetValues(float level, float boost, float range)
    {
        flashAtLevel = level;
        flashDetectedBoost = boost;
        flashRange = range;
    }

    // Start is called before the first frame update
    void Start()
    {
        powerLevel = Random.Range(0f, flashAtLevel);
    }

    // Update is called once per frame
    void Update()
    {
        powerLevel++;

        if (powerLevel > flashAtLevel)
        {
            powerLevel = 0f;
            StartCoroutine(Flash());
        }
    }

    public void ReceiveFlash()
    {
        powerLevel += (powerLevel * flashDetectedBoost);
    }

    private IEnumerator Flash()
    {
        var colliders = Physics.OverlapSphere(transform.position, flashRange, flashLayer);

        foreach (var collider in colliders)
        {
            var firefly = collider.GetComponentInParent<Firefly>();
            firefly.ReceiveFlash();
        }

        normal.SetActive(false);
        flashing.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        normal.SetActive(true);
        flashing.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, flashRange);
    }
}
