﻿using UnityEngine;
using System.Collections.Generic;

public class Creature : MonoBehaviour {
    //generic stuff for every creature, whether PC or enemy

    public int tileX;
    public int tileY;

    public TileMap map;

    public List<Node> currentPath = null;
    float movespeed = 1;

    //if it has a path, draw it out
    void Update()
    {
        if(currentPath != null)
        {
            int currnode = 0;

            while(currnode < currentPath.Count -1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currnode].x,
                                                            currentPath[currnode].y
                                                            )
                                                            + new Vector3(0, 0, -1f);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currnode+1].x,
                                                            currentPath[currnode+1].y
                                                            )
                                                            + new Vector3(0, 0, -1f);

                Debug.DrawLine(start, end, Color.red);

                currnode++;
            }
        }
    }

    
    public void MoveNextTile()
    {
        float remainingmovespeed = movespeed;
        while (remainingmovespeed > 0)
        {
            //if there's no path nothing happens
            if (currentPath == null)
            {
                return;
            }
            //remove the old/current node from path
            currentPath.RemoveAt(0);

            //now grab the new first node and move us to that position
            tileX = currentPath[0].x;
            tileY = currentPath[0].y;
            transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
            transform.position += new Vector3(0, 0, -5);

            remainingmovespeed--;

            if (currentPath.Count == 1)
            {
                //we only have tile left in the path, that tile is the destination
                //clear pathfinding info
                currentPath = null;
            }
        }
    }

}
