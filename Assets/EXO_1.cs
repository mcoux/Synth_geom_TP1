using UnityEngine;

namespace Synth_Geom
{
    public class EXO_1 : MonoBehaviour
    {
        [SerializeField] Vector3[] points;
        [SerializeField] int heigth;
        [SerializeField] int width;
        MeshFilter meshFilter;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh=createVertex(points);
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Mesh createPlane(Vector3[] data)
        {

            Mesh mesh = new Mesh();

            mesh.vertices =  data ;
            mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3};
            
            return mesh;

        }

        public Mesh createVertex(Vector3[] data)
        {

            Mesh mesh = new Mesh();

            mesh.vertices = data;
            mesh.triangles = new int[] { 0, 1, 2 };

            return mesh;

        }
    }

}

