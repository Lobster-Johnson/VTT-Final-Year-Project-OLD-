using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {
    public float movesensitivityX = 1.0f;
    public float movesensitivityY = 1.0f;

    //selection ray
    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

	
	// Update is called once per frame
	void Update () {
        //touchscreen camera controls



        //if the user is touching the screen
        if (Input.touchCount > 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Selected " + hit.transform.gameObject.name);

            }
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touch Began");
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Debug.Log("Touch Moved");

            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("Touch Ended");
            }
        }
    }
}
