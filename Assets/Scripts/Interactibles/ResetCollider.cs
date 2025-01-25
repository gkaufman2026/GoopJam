using UnityEngine;

public class ResetCollider : MonoBehaviour
{
    Level levelStarter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelStarter = GetComponentInParent<Level>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.rigidbody.MovePosition(levelStarter.levelStartPoint.transform.position);

            LevelManager.Instance.TryRestartLevel();
        }
    }
}
