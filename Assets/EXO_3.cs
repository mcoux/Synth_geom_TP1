using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

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
        float parallAngle = Mathf.PI / paralleles;
        float r,x,y,z;
        //Ajout des points
        for (int i = 0; i <= paralleles; i++) 
        {
            r = Mathf.Sin(parallAngle*i);
            y = Mathf.Cos(parallAngle*i);
            for (int j = 0; j <= meridiens; j++)
            {
                x = Mathf.Cos(meridianAngle * j)*r;
                z = Mathf.Sin(meridianAngle*j)*r;
                currentPoint = new Vector3(centre.x + x , centre.y +y, centre.z + z);
                currentPoint *= rayon;
                pointList.Add(currentPoint);
            }
        }

        //pointList.Add(new Vector3(centre.x, centre.y - rayon, centre.z));
        mesh.vertices = pointList.ToArray();

     

        int a, b;

        //Ajout des faces des poles
        for (int i = 0;i < paralleles; i++)
        {
            for(int j = 0;j < meridiens; j++)
            {
                a = (int)(i * (meridiens + 1)+j);
                b = (int)(a + meridiens + 1);
                triangles.Add(a+1);
                triangles.Add(b);
                triangles.Add(a);

                triangles.Add(a + 1);
                triangles.Add(b+1);
                triangles.Add(b);
            }
         

        }

       


        mesh.triangles = triangles.ToArray();
        Debug.Log(mesh.triangles.Length);

        return mesh;
    }
    //Fonction de débuggage, pour visualiser les points
    //private void OnDrawGizmos()
    //{
    //    foreach (Vector3 point in pointList) {
    //        Gizmos.DrawIcon(point,"P");
    //    }
    //    Gizmos.DrawWireSphere(centre, rayon);

    //}
}
