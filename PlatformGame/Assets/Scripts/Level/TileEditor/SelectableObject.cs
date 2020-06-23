using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectableObject : MonoBehaviour
{
    public TileEditor editor;
    public Tile tile;
    
    public void Clicked()
    {
        editor.SetTile(tile);
    }
}
