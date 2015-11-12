using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Tilemap : MonoBehaviour {
    public int size_x = 100;
    public int size_z = 50;
    public float tilesize = 1.0f;

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
        int[] triangles = new int[numtris * 3];
        Vector3[] normals = new Vector3[numverts];
        Vector2[] uv = new Vector2[numverts];

        int x, z;
        for (z =0; z< size_z; z++)
        {
            for (x = 0; x < size_x; x++)
            {
                vertices[z * vsize_x + x] = new Vector3(x*tilesize, z*tilesize, 0);
                normals[z * vsize_x + x] = Vector3.up;
                uv[z * vsize_x + x] = new Vector2((float)x / vsize_x, (float)z / vsize_z);

            }
        }

        for (z = 0; z < size_z; z++)
        {
            for (x = 0; x < size_x; x++)
            {
                int squareindex = z * size_x + x;
                int triOffset = squareindex * 6;
                triangles[triOffset + 0] = z * vsize_x + x + 0;
                triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 0;
                triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;

                triangles[triOffset + 3] = z * vsize_x + x + 0;
                triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
                triangles[triOffset + 5] = z * vsize_x + x + 1;

            }
        }



        //create new mesh and populate with data
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;


        //assign mesh to filterer/renderer/collider
        MeshFilter mf = GetComponent<MeshFilter>();
        //MeshRenderer mr = GetComponent<MeshRenderer>();
        MeshCollider mc = GetComponent<MeshCollider>();

        mf.mesh = mesh;
        mc.sharedMesh = mesh;

    }
}
