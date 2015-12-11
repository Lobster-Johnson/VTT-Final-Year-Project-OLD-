using UnityEngine;
using System.Collections;

public class TileSelectable : MonoBehaviour {
    public int tileX;
    public int tileY;
    public TileMap map;
    void OnMouseUp()
    {
        Debug.Log("Click!");
        map.Destination(tileX, tileY);
    }
    
}
