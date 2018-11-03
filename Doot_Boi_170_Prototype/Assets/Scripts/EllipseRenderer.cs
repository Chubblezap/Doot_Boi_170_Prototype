//Code base provided via Board to Bits https://www.youtube.com/watch?v=lKfqi52PqHk&t=949s&frags=pl%2Cwn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour {

    LineRenderer lr;

    //A range of values to control how may vertices the ellipse will have
    [Range(3, 36)]

    public int segments;
    public Ellipse orbitPath;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();
	}
	
    void CalculateEllipse() {
        //Creating an array of vectors to hold each segment of the orbital path
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++) {
            Vector2 position2D = orbitPath.Evaluate((float)i / (float)segments);
            points[i] = new Vector3(position2D.x, position2D.y, 0f);
        }
        //Set last point equal to the first
        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }
}
