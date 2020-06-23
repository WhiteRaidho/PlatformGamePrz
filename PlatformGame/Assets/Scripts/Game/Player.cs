using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer renderer;

    public List<Sprite> sprites = new List<Sprite>(); 
    public List<Collider2D> colliders= new List<Collider2D>();

    public Shape state = Shape.Square;

    public int HP = 3;


    public Checkpoint currentCheckpoint;

    public enum Shape
    {
        Square,
        Circle
    }

    public void ChangeState(Shape newState)
    {
        state = newState;
        renderer.sprite = sprites[(int)state];
        for(int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = (i==(int)state);
        }
    }


    private void Start()
    {
        LevelManager.instance.player = this;
        HP = GameManager.instance.saveProperties.startingLives;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) ChangeState(Shape.Square);
        if (Input.GetKeyDown(KeyCode.S)) ChangeState(Shape.Circle);

        if(transform.position.y <= -10)
        {
            Death();
        }
    }

    public void Death()
    {
        HP--;
        if (HP <= 0)
        {
            LevelManager.instance.GameOver();
        }
        Restore();
    }

    public void AddLive()
    {
        HP++;
        Restore();
    }

    void Restore()
    {
        LevelManager.instance.HPText.text = HP.ToString();
        if (!currentCheckpoint) currentCheckpoint = LevelManager.instance.startCheckpoint;
        
        currentCheckpoint.Restore(this);
    }

}
