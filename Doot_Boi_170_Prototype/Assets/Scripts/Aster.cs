using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aster : MonoBehaviour {


    private void OnBecameInvisible() {
        // print(this.name + " is invisible");
        Destroy(this.gameObject);
    }
}
