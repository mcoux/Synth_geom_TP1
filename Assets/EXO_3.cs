using System;
using System.Drawing;
using UnityEngine;

public class EXO_3 : MonoBehaviour
{

    [SerializeField] float rayon;
    [SerializeField] float paralleles;
    [SerializeField] float meridiens;
    [SerializeField] Vector3 centre;

    private System.Collections.Generic.List<Vector3> pointList = new System.Collections.Generic.List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = createSphere( paralleles, rayon, meridiens, centre);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Mesh createSphere(float paralleles, float rayon, float meridiens, Vector3 centre)
    {
        if (meridiens < 3 || paralleles < 2)
        {
            return null;
        }

        //System.Collections.Generic.List<Vector3> points = new();
        System.Collections.Generic.List<int> triangles = new();

        //pointList.Add(new Vector3(centre.x, centre.y + rayon, centre.z));

        Mesh mesh = new Mesh();
        Vector3 currentPoint;
        float meridianAngle = 2*Mathf.PI/meridiens;
        float parallAngle = Mathf.PI / (paralleles+1);

        //Ajout des points
        //int j = 1; j < meridiens; j++
        //int i = 0; i <= paralleles; i++
        for (int i = 0; i <= paralleles; i++) 
        {

            for (int j = 0; j <= meridiens; j++)
            {
                currentPoint = new Vector3(centre.x + Mathf.Cos(meridianAngle * i) * Mathf.Sin(parallAngle * j) , centre.y + Mathf.Cos(parallAngle * j), centre.z + Mathf.Sin(meridianAngle * i) * Mathf.Sin(parallAngle * j));
                currentPoint *= rayon;
                pointList.Add(currentPoint);
            }
        }

        //pointList.Add(new Vector3(centre.x, centre.y - rayon, centre.z));
        mesh.vertices = pointList.ToArray();

     

        int a, b;

        //Ajout des faces des poles
        for (int i = 0;i< paralleles; i++)
        {
            for(int j = 0;j<meridiens; j++)
            {
                a = (int)(i * (meridiens + 1)+j);
                b = (int)(a + meridiens + 1);
                triangles.Add(a);
                triangles.Add(b);
                triangles.Add(a+1);


                triangles.Add(b);
                triangles.Add(b+1);
                triangles.Add(a+1);
            }
         

        }

       


        mesh.triangles = triangles.ToArray();
        Debug.Log(mesh.triangles.Length);

        return mesh;
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 point in pointList) {
            Gizmos.DrawIcon(point,"P");
        }
        Gizmos.DrawWireSphere(centre, rayon);

    }
}
