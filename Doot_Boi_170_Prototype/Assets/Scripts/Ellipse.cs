using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse {

    public float xAxis;
    public float yAxis;

    //Constructor
    public Ellipse(float x, float y){
        this.xAxis = x;
        this.yAxis = y;
    }

    public Vector2 Evaluate (float t) {
        float angle = (t * 360 * Mathf.Deg2Rad);
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;
        return new Vector2(x, y);
    }
}
