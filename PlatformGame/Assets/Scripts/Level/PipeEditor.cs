using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeEditor : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;

    public TileBase[] startRightTile = new TileBase[3];
    public TileBase[] startLeftTile = new TileBase[3];


    public TileBase upDownTile;
    public TileBase rightLeftTile;

    public TileBase upRightTile;
    public TileBase upLeftTile;
    public TileBase downRightTile;
    public TileBase downLeftTile;

    Vector3Int? prevClickPos = null;

    public TMP_Text warningText;

    bool started = false;

    private void Start()
    {
        //ShowWarning("yesn't");
        //tilemap.SetTile(new Vector3Int(-1, 0, 0), rightLeftTile);
        //SetStart(new Vector3Int(0, 0, 0));
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int p = grid.WorldToCell(click);
            if (p != prevClickPos)
            {
                prevClickPos = p;
                //tilemap.SetTile(p, currentTile);
                UpdateCells(p);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int p = grid.WorldToCell(click);
            //tilemap.SetTile(p, null);
            if(p != prevClickPos)
            {
                prevClickPos = p;
                UpdateCells(p, true);
            }
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            Debug.Log("mouse up");
            prevClickPos = null;
        }
    }

    public void UpdateCells(Vector3Int pos, bool erase = false)
    {
        if(erase)
        {
            tilemap.SetTile(pos, null);
            FindStart();
            return;
        }


    }

    public void ShowWarning(string text)
    {
        warningText.SetText(text);
        warningText.GetComponent<Animator>().SetTrigger("PopUp");
    }
    
    

    public void FindStart()
    {

    }
    public void SetStart(Vector3Int pos)
    {
        if(tilemap.GetTile(new Vector3Int(pos.x + 1, pos.y, pos.z)) == rightLeftTile
           || tilemap.GetTile(new Vector3Int(pos.x + 1, pos.y, pos.z)) == upLeftTile
           || tilemap.GetTile(new Vector3Int(pos.x + 1, pos.y, pos.z)) == downLeftTile)
        {
            pos.y++;
            tilemap.SetTile(pos, startLeftTile[0]);
            pos.y--;
            tilemap.SetTile(pos, startLeftTile[1]);
            pos.y--;
            tilemap.SetTile(pos, startLeftTile[2]);
        } else if(tilemap.GetTile(new Vector3Int(pos.x - 1, pos.y, pos.z)) == rightLeftTile
                  || tilemap.GetTile(new Vector3Int(pos.x - 1, pos.y, pos.z)) == upRightTile
                  || tilemap.GetTile(new Vector3Int(pos.x - 1, pos.y, pos.z)) == downRightTile)
        {
            pos.y++;
            tilemap.SetTile(pos, startRightTile[0]);
            pos.y--;
            tilemap.SetTile(pos, startRightTile[1]);
            pos.y--;
            tilemap.SetTile(pos, startRightTile[2]);
        }
    }

    //IEnumerator FadeWarning()
    //{
    //    while(warningText.alpha > 0.1f)
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //        warningText.alpha -= 0.1f;
    //    }
    //    warningText.alpha = 0f;
    //}
}
