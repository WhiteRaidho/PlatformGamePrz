using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEditor : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;

    public List<TileBase> normalTiles = new List<TileBase>();

    public TileBase currentTile;
    Vector3Int? prevClickPos = null;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int p = grid.WorldToCell(click);
            if (p != prevClickPos)
            {
                prevClickPos = p;
                tilemap.SetTile(p, currentTile);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int p = grid.WorldToCell(click);
            tilemap.SetTile(p, null);
        }
        if (Input.GetMouseButtonUp(0))
        {
            prevClickPos = null;
        }
    }
}
