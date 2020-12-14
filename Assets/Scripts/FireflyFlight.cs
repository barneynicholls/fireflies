using UnityEngine;

public class FireflyFlight : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 targetPosition = Vector3.zero;
    Vector3 boundsCenter;
    float bounds;

    public void SetValues(float speed, Vector3 center, float size)
    {
        this.speed = speed;
        boundsCenter = center;
        bounds = size;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        var dist = Vector3.Distance(targetPosition, transform.position);    

        if(dist < 2f)
        {
            SetTargetPosition();
        }
    }

    void SetTargetPosition()
    {
        var position = new Vector3(Random.Range(-bounds, bounds),
                                      Random.Range(-bounds, bounds),
                                      Random.Range(-bounds, bounds));

        targetPosition = boundsCenter + position;
    }
}
