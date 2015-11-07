using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    //temp script
    //grid in scene view for me to gauge sizes
    public float width = 1.0f;
    public float height = 1.0f;

    public Color color = Color.white;

    void OnDrawGizmos()
    {
        Vector3 pos = Camera.current.transform.position;
        Gizmos.color = this.color;

        for(float y = pos.y - 800.0f; y < pos.y + 800.0f; y += this.height)
        {
            Gizmos.DrawLine(
                new Vector3(-1000.0f, Mathf.Floor(y / height)* height, 0.0f),
                new Vector3(1000.0f, Mathf.Floor(y / height) * height, 0.0f));
        }

        for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += this.height)
        {
            Gizmos.DrawLine(
                new Vector3(Mathf.Floor(x / width) * width, - 1000.0f, 0.0f),
                new Vector3(Mathf.Floor(x / width) * width,  1000.0f, 0.0f));
        }
    }

    
}

