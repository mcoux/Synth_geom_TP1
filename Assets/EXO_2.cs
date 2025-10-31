using NUnit.Framework.Internal;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace Synth_Geom
{
    public class EXO_2 : MonoBehaviour
    {
        [SerializeField] float hauteur;
        [SerializeField] float rayon;
        [SerializeField] float meridiens;
        [SerializeField] Vector3 centre;
       
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = createCylinder(hauteur, rayon, meridiens, centre);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Mesh createCylinder(float h, float r, float m, Vector3 centre)
        {
            Mesh mesh = new Mesh();

            System.Collections.Generic.List<Vector3> points =new();
            System.Collections.Generic.List<int> triangles = new();

            //Creation des premiers points avant la boucle

            //Centres des cercles
            points.Add(new Vector3
            {
                x = centre.x,
                y = centre.y + h / 2,
                z = centre.z,
            });

            points.Add(new Vector3
            {
                x = centre.x,
                y = centre.y - h / 2,
                z = centre.z,
            });

            

            float currentAngle;
            int tIndex = 0;
            //Disposition des points
            for (int i =0; i<=m; i++)
            {
                currentAngle = i*(2 * Mathf.PI) / m;

                points.Add(new Vector3
                {
                    x = r *Mathf.Cos(currentAngle),
                    y = centre.y + h / 2,
                    z = r * Mathf.Sin(currentAngle),
                });

                points.Add(new Vector3
                {
                    x = r * Mathf.Cos(currentAngle),
                    y = centre.y - h / 2,
                    z = r * Mathf.Sin(currentAngle),
                });

            }
            //Création des faces latérales
            for (int i = 2; i <= m*2; i+=2) {

                triangles.Add(i+2);
                triangles.Add(i+1);
                triangles.Add(i);

                triangles.Add(i + 1);
                triangles.Add(i + 2);
                triangles.Add(i + 3);
            }

            //Création des faces des disques
            for(int i = 0; i<=m; i++)
            {
                triangles.Add(i * 2 + 2);
                triangles.Add(i*2);
                triangles.Add(0);

                triangles.Add(1);
                triangles.Add(i * 2 + 1);
                triangles.Add(i * 2 + 3);
            }
            mesh.vertices = points.ToArray();
            mesh.triangles = triangles.ToArray();

            return mesh;
        }
    }

}

