using System;
using System.Collections;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public Mesh mesh;
    public Material material;
    public int maxDepth;
    public float childScale;

    private int depth;

    public void Start() {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        if(depth<maxDepth){
            new GameObject("Fractal Child "+depth).AddComponent<Fractal>().Initialize(this, Vector3.up);
            new GameObject("Fractal Child "+depth).AddComponent<Fractal>().Initialize(this, Vector3.right);
        }

    }
    
    private void Initialize (Fractal parent, Vector3 direction) {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        childScale = parent.childScale;
        depth = parent.depth + 1;

        //Creates hierarchical parent-children objects
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = direction * (0.5f + 0.5f * childScale);
    }

    // Update is called once per frame
    void Update () {
        
    }
}
