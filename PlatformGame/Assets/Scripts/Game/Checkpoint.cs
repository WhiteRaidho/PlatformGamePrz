using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Material normalMaterial;
    public Material activeMaterial;

    public bool isActive = false;

    public Player.Shape playerState = Player.Shape.Square;

    public void Deactivate()
    {
        isActive = false;
        GetComponentInChildren<SpriteRenderer>().material = normalMaterial;
    }

    public void Activate(Player player)
    {
        isActive = true;
        GetComponentInChildren<SpriteRenderer>().material = activeMaterial;

        if (player.currentCheckpoint && player.currentCheckpoint != this) player.currentCheckpoint.Deactivate();
        player.currentCheckpoint = this;

        playerState = player.state;
        LevelManager.instance.SaveStates();
    }

    public void Restore(Player player)
    {
        player.ChangeState(playerState);
        player.transform.position = this.transform.position + new Vector3(0.5f, 0.2f, 0);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        LevelManager.instance.Restore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Player player = collision.GetComponent<Player>();
            Activate(player);
        }
    }
}
