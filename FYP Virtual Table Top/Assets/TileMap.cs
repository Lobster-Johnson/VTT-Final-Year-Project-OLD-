﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour
{

    public TileType[] tileTypes;
    int[,] tiles;

    List<Node> currentPath = null;

    int mapsizeX = 10;
    int mapsizeY = 10;

    public GameObject PC;

    Node[,] graph;

	// Use this for initialization
	void Start () {
        //make sure the PC knows where they are
        PC.GetComponent<Creature>().tileX = (int)PC.transform.position.x;
        PC.GetComponent<Creature>().tileY = (int)PC.transform.position.y;
        PC.GetComponent<Creature>().map = this;

        generateMapData();
        generatePathFindingGraph();
        generateMapVisuals();
    }

    //move cost to enter a tile for pathing purposes
    float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tiles[targetX, targetY]];
        float cost = tt.movementCost;
        if(sourceX  != targetX && sourceY != targetY)
        {
            //we are moving diagonally
            cost += 0.00001f;
        }
        return cost;
    }

    //generate graph
    void generatePathFindingGraph()
    {
        graph = new Node[mapsizeX, mapsizeY];
        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                        //add all neightbours for each node
                        //need to add diagonals
                //left neightbour
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    //diagnols
                    if (y < mapsizeY - 1)
                    {
                        graph[x, y].neighbours.Add(graph[x-1, y + 1]);
                    }
                    if (y > 0)
                    {
                        graph[x, y].neighbours.Add(graph[x-1, y - 1]);
                    }
                }
                //right neighbour
                if (x < mapsizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    //diagnols
                    if (y < mapsizeY - 1)
                    {
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                    }
                    if (y > 0)
                    {
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    }
                }
                //bottom neightbour
                if (y > 0)
                {
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                }
                //top neighbour
                if (y < mapsizeY - 1)
                {
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
                }

               
            }
        }
    }

    //create tiles and assign tile types
    //hard coded at the moment
    void generateMapData()
    {
        //create a map of default tiles
        tiles = new int[mapsizeX, mapsizeY];
        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }

        //hardcoded: Remove
        //this could cause problems as all I'm doing here is created a new block on top of the old one
        //generate test wall
        tiles[0, 4] = 2;
        tiles[1, 4] = 2;
        tiles[2, 4] = 2;
        tiles[3, 4] = 2;
        tiles[4, 4] = 2;

        //generate test swamp
        tiles[9, 9] = 1;
        tiles[8, 9] = 1;
        tiles[9, 8] = 1;
        tiles[8, 8] = 1;
    }

    void generateMapVisuals()
    {
        //go to every tile on the map and make them selectable
        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                
                GameObject go = (GameObject) Instantiate((tileTypes[tiles[x, y]]).tileVisual, new Vector3(x, y, -1), Quaternion.identity);

                TileSelectable ct = go.GetComponent<TileSelectable>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    //tile location -> world location
    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    //path creature to this location
    public void Destination(int x, int y)
    {
        //clear out preexisting path
        PC.GetComponent<Creature>().currentPath = null;
        

        //warning: following algorithm isn't the right one. Replace with A*
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        //where you start
        Node source = graph[
                            PC.GetComponent<Creature>().tileX,
                            PC.GetComponent<Creature>().tileY
                            ];

        //where you want to go
        Node target = graph[
                            x,
                            y
                            ];

        dist[source] = 0;
        prev[source] = null;

        foreach(Node v in graph)
        {
            if(v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }

        //while there's still nodes to visit
        while(unvisited.Count > 0)
        {
            //Node u = unvisited.OrderBy(n => dist[n]).First();

            Node u = null;

            foreach(Node PossibleU in unvisited)
            {
                if(u == null || dist[PossibleU] < dist[u])
                {
                    u = PossibleU;
                }
            }

            if(u == target)
            {
                break;
            }


            unvisited.Remove(u);

            foreach(Node v in u.neighbours)
            {
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if(alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        //here indicates we have either found the shortest route or there is no route
        if(prev[target] == null)
        {
            //no route between target and source
            return;
        }

        //construct path
        currentPath = new List<Node>();

        Node curr = target;

        //go back through the prev chain and add to path
        while(curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        //invert path
        currentPath.Reverse();

        PC.GetComponent<Creature>().currentPath = currentPath;
    }
	
}
