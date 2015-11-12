using UnityEngine;
using System.Collections;

public class SimpleCameraControl : MonoBehaviour {
    public KeyCode forward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode backward;

    public float speed = 10;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(forward))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(left))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(right))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(backward))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }
}
