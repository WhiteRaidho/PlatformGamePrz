using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActiveElement
{
    public SpriteRenderer sprite;
    public Collider2D collider;
    public override void Action()
    {
        collider.enabled = !active;
        sprite.color = active?new Color(0.25f, 0.25f, 0.25f):new Color(1f, 1f, 1f);
    }
}
