using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileEditor : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public Tilemap spikes;

    public List<Tile> normalTiles = new List<Tile>();
    public List<Tile> spikesTiles = new List<Tile>();

    public TileBase currentTile;
    Vector3Int? prevClickPos = null;

    public Transform tileList;
    public GameObject prefab;

    public TMP_InputField fileName;

    private bool addingSpikes = false;


    private void Start()
    {
        CreateTilesOnUI();
        if(!GameManager.instance.customLevelName.Equals(""))
        {
            Load(GameManager.instance.customLevelName);
            fileName.text = GameManager.instance.customLevelName.Remove(GameManager.instance.customLevelName.Length - 5);
        }
    }

    public void SetTile(Tile tile)
    {
        addingSpikes = spikesTiles.Contains(tile);
        currentTile = tile;
    }

    public void CreateTilesOnUI()
    {
        foreach (Tile t in normalTiles)
        {
            GameObject obj = Instantiate(prefab, tileList);
            SelectableObject so = obj.GetComponent<SelectableObject>();
            so.editor = this;
            so.tile = t;
            Image img = obj.GetComponent<Image>();
            img.sprite = t.sprite;
        }

        foreach (Tile t in spikesTiles)
        {
            GameObject obj = Instantiate(prefab, tileList);
            SelectableObject so = obj.GetComponent<SelectableObject>();
            so.editor = this;
            so.tile = t;
            Image img = obj.GetComponent<Image>();
            img.sprite = t.sprite;
            img.color = new Color(1, 0.8627451f, 0.8627451f);
        }
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButton(0))
        {
            Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int p = grid.WorldToCell(click);
            if (p != prevClickPos)
            {
                prevClickPos = p;
                if (addingSpikes) spikes.SetTile(p, currentTile);
                else tilemap.SetTile(p, currentTile);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int p = grid.WorldToCell(click);
            tilemap.SetTile(p, null);
            spikes.SetTile(p, null);
        }
        if (Input.GetMouseButtonUp(0))
        {
            prevClickPos = null;
        }
    }

    private string GenerateFileName()
    {
        string name = DateTime.UtcNow.ToString();
        name = name.Replace(".", "_").Replace(":", "_");
        return name;
    }

    public void Save()
    {
        List<TileData> tiles = new List<TileData>();
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;
            var tile = tilemap.GetTile(pos);
            var tileData = new TileData
            {
                position = pos,
                tile = tile,
                tilemapName = tilemap.name
            };
            tiles.Add(tileData);
        }

        List<TileData> spikesList = new List<TileData>();
        foreach (Vector3Int pos in spikes.cellBounds.allPositionsWithin)
        {
            if (!spikes.HasTile(pos)) continue;
            var tile = spikes.GetTile(pos);
            var tileData = new TileData
            {
                position = pos,
                tile = tile,
                tilemapName = spikes.name
            };
            spikesList.Add(tileData);
        }

        TileWrapper wrapper = new TileWrapper
        {
            tiles = tiles.ToArray(),
            spikes = spikesList.ToArray()
        };
        string json = JsonUtility.ToJson(wrapper);
        if (fileName.text.Equals("")) fileName.text = GenerateFileName();
        fileName.text = fileName.text.Replace(".", "_").Replace(":", "_");
        System.IO.FileInfo file = new System.IO.FileInfo(Application.persistentDataPath + "/saves/" + fileName.text + ".json");
        file.Directory.Create();
        System.IO.File.WriteAllText(file.FullName, json);
        Debug.Log(file.FullName);
    }

    public void Load(string fileName)
    {
        fileName = Application.persistentDataPath + "/saves/" + fileName;
        if (System.IO.File.Exists(fileName))
        {
            string json = System.IO.File.ReadAllText(fileName);
            TileWrapper wrapper = JsonUtility.FromJson<TileWrapper>(json);
            if (wrapper.tiles != null && wrapper.tiles.Length > 0)
                foreach (TileData tileData in wrapper.tiles)
                {
                    Debug.Log(tileData.position);
                    tilemap.SetTile(tileData.position, tileData.tile);
                }
            if (wrapper.spikes != null && wrapper.spikes.Length > 0)
                foreach (TileData tileData in wrapper.spikes)
                {
                    spikes.SetTile(tileData.position, tileData.tile);
                }
        }
        else
        {
            Debug.LogWarning($"File {fileName} doesn't exist.");
        }
    }

    [Serializable]
    public class TileData
    {
        public TileBase tile;
        public Vector3Int position;
        public string tilemapName;
    }

    [Serializable]
    public class TileWrapper
    {
        public TileData[] tiles;
        public TileData[] spikes;
    }
}
