using UnityEngine;

public class BubbleShot : MonoBehaviour
{
    [SerializeField] GameObject gravityBubbleObject;

    //TODO: Do we need some sort of evaluation system for whether or not a shot should become a bubble?
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(gravityBubbleObject, transform.position, transform.rotation);

        Destroy(gameObject);
    }

}
