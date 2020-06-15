using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public List<Vector2> nodes = new List<Vector2>();

    [SerializeField]
    private BoxCollider2D entryCollider;

    [Range(0.5f, 30f)]
    public float speed = 5f;

    public float playerScale = 0.75f;
    
    private float currentState = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if(player.state == Player.Shape.Circle)
            {
                StartCoroutine(TransportPlayer());
            }
        }
    }

    IEnumerator TransportPlayer()
    {
        LevelManager.instance.LockMovement(true);
        Player player = LevelManager.instance.player;
        player.transform.localScale = new Vector2(playerScale, playerScale);

        entryCollider.enabled = false;

        for (int i = 0; i < nodes.Count - 1;)
        {
            yield return new WaitForFixedUpdate();

            float t = currentState / Vector2.Distance(nodes[i], nodes[i + 1]);
            player.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(nodes[i], nodes[i + 1], t));

            currentState += Time.fixedDeltaTime * speed;
            
            if (Vector2.Distance(player.transform.position, nodes[i + 1]) <= 0.1f)
            {
                player.transform.position = nodes[i + 1];
                currentState = 0f;
                i++;
            }
        }

        player.transform.localScale = new Vector2(1, 1);
        LevelManager.instance.LockMovement(false);

        entryCollider.enabled = true;
    }
}
