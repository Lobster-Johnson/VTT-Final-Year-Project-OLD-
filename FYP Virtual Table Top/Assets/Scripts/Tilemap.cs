using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Tilemap : MonoBehaviour {
    public int size_x = 100;
    public int size_z = 50;
    float tilesize = 1.0f;

    // Use this for initialization
    void Start () {
        BuildMesh();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void BuildMesh()
    {
        int numtiles = size_x * size_z;
        int numtris = numtiles * 2;
        int vsize_x = size_x + 1;
        int vsize_z = size_z + 1;
        int numverts = vsize_x * vsize_z;

        //create mesh data
        Vector3[] vertices = new Vector3[numverts];
        int[] triangles = new int[numtris *2];
        Vector3[] normals = new Vector3[numverts];

        int x, z;
        for (z =0; z< size_z; z++)
        {
            for (x = 0; x < size_x; x++)
            {
                vertices[z * vsize_x + x] = new Vector3(x*tilesize, z*tilesize, 0);
                normals[z * vsize_x + x] = Vector3.up;

            }
        }

        for (z = 0; z < size_z; z++)
        {
            for (x = 0; x < size_x; x++)
            {
                int squareindex = z * size_x + x;
                int triindex = squareindex * 6;
                triangles[triindex + 0] = z * vsize_x + x + 0;
                triangles[triindex + 1] = z * vsize_x + x + vsize_x + 1;
                triangles[triindex + 2] = z * vsize_x + x + vsize_x + 0;

                triangles[triindex + 3] = z * vsize_x + x + 0;
                triangles[triindex + 4] = z * vsize_x + x + 1;
                triangles[triindex + 5] = z * vsize_x + x + vsize_x + 1;

            }
        }



        //create new mesh and populate with data
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;


        //assign mesh to filterer/renderer/collider
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        MeshCollider mc = GetComponent<MeshCollider>();

        mf.mesh = mesh;

    }
}
