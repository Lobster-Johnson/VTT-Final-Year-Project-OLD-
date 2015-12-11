using UnityEngine;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

    public TileType[] tileTypes;
    int[,] tiles;
    int mapsizeX = 10;
    int mapsizeY = 10;

    public GameObject PC;

    Node[,] graph;

	// Use this for initialization
	void Start () {
        generateMapData();
        generatePathFindingGraph();
        generateMapVisuals();
    }

    class Node
    {
        List<Node> neighbours;

        public Node()
        {
            neighbours = new List<Node>();

        } 
    }

    //generate graph
    void generatePathFindingGraph()
    {
        graph = new Node[mapsizeX, mapsizeY];
        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                //add all neightbours for each node
                //graph[x, y].neighbours.Add(graph[x - 1, y]);
            }
        }
    }

    //create tiles and assign tile types
    void generateMapData()
    {
        tiles = new int[mapsizeX, mapsizeY];
        for (int x = 0; x < mapsizeX; x++)
        {
            for (int y = 0; y < mapsizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }

        //hardcoded: Remove
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

    //move to this location
    public void Destination(int x, int y)
    {
        //update the creatures location to the destination, hardcode to destination
        PC.GetComponent<Creature>().tileX = x;
        PC.GetComponent<Creature>().tileY = y;
        PC.transform.position = new Vector3(x, y, -5);
    }
	
}
