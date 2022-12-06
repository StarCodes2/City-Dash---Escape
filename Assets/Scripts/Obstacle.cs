using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.position.z - 20 > transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
