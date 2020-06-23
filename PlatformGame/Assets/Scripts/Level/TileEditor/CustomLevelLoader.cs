using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomLevelLoader : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap spikes;

    void Start()
    {
        Load(GameManager.instance.customLevelName);
    }

    public void Load(string fileName)
    {
        fileName = Application.persistentDataPath + "/saves/" + fileName;
        if (System.IO.File.Exists(fileName))
        {
            string json = System.IO.File.ReadAllText(fileName);
            TileEditor.TileWrapper wrapper = JsonUtility.FromJson<TileEditor.TileWrapper>(json);
            if (wrapper.tiles != null && wrapper.tiles.Length > 0)
                foreach (TileEditor.TileData tileData in wrapper.tiles)
                {
                    Debug.Log(tileData.position);
                    tilemap.SetTile(tileData.position, tileData.tile);
                }
            if (wrapper.spikes != null && wrapper.spikes.Length > 0)
                foreach (TileEditor.TileData tileData in wrapper.spikes)
                {
                    spikes.SetTile(tileData.position, tileData.tile);
                }
        }
        else
        {
            Debug.LogWarning($"File {fileName} doesn't exist.");
        }
    }
}
