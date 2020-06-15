using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStand : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public Player.Shape currentShape = Player.Shape.Circle;

    public Player.Shape savedShape;

    [SerializeField]
    private SpriteRenderer renderer;
    private bool change = false;

    private void Start()
    {
        renderer.sprite = sprites[(int)currentShape];
        savedShape = currentShape;
    }

    public void ChangeShape(Player.Shape shape)
    {
        currentShape = shape;
        renderer.sprite = sprites[(int)currentShape];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(change)
        {
            change = false;
            return;
        }

        if(collision.tag.Equals("Player"))
        {
            change = true;
            Player player = collision.GetComponent<Player>();

            Player.Shape s = player.state;
            player.ChangeState(currentShape);
            ChangeShape(s);
        }
    }
}
