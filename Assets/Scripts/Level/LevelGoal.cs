using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] GameObject successParticle;

    LevelManager manager;

    bool completed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = LevelManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !completed)
        {
            Instantiate(successParticle, transform.position, transform.rotation);

            manager.LevelComplete();
            completed = true;
        }
    }
}
