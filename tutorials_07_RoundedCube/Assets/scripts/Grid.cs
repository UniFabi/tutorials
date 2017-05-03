using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {

    public int xSize, ySize;
    Mesh mesh;
    Vector3[] vertices;

    private void Awake(){
       Generate();
    }

    private void Generate(){
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        Vector2[] uv;

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        uv = new Vector2[vertices.Length];

        for(int i = 0, y = 0; y <= ySize; y++){
            for(int x = 0; x <= xSize; x++, i++){
                vertices [i] = new Vector3(x,y);
                uv[i] = new Vector2((float) x / xSize, (float) y / ySize);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[xSize*6*ySize];
        Debug.Log(triangles.Length);
        for(int i = 0,j = 0,k = 0; k < ySize; j++, k++){
            Debug.Log(" "+i);
            for(int x = 0; x < xSize; i+=6,j++,x++){
                triangles[i] = j;
                triangles[i+2] = triangles[i+3] = j + 1;
                triangles[i+1] = triangles[i+4] = xSize + 1 + j;
                triangles[i+5] = j + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();




    }

    private void OnDrawGizmos(){
        if(vertices==null)
            return;
        Gizmos.color = Color.black;
        for(int i = 0; i < vertices.Length; i++){
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

}
