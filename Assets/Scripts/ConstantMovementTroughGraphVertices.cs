using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovementTroughGraphVertices : MonoBehaviour
{
    public float speed;
    public List<Vector3> vertices;

    List<Vector3> normalizedVectorsToVertices = new List<Vector3>();

    int currentVertexIndex = 1;
    int currentVectorIndex = 0;
    void Start()
    {
        if (vertices.Count <= 1)
        {
            enabled = false;
            return;
        }

        for (int i = 1; i < vertices.Count; ++i)
        {
            normalizedVectorsToVertices.Add((vertices[i] - vertices[i - 1]).normalized);
        }
        normalizedVectorsToVertices.Add((vertices[0] - vertices[vertices.Count - 1]).normalized);

        transform.position = vertices[0];

    }

    void Update()
    {
        transform.position += normalizedVectorsToVertices[currentVectorIndex] * speed * Time.deltaTime;

        // If dot product of both vectors is negative -> vectors point in different dirrections -> we passed the vertex
        if (Vector3.Dot(normalizedVectorsToVertices[currentVectorIndex], (vertices[currentVertexIndex] - transform.position)) <= 0)
        {
            transform.position = vertices[currentVertexIndex];
            currentVectorIndex = (currentVectorIndex + 1) % normalizedVectorsToVertices.Count;
            currentVertexIndex = (currentVertexIndex + 1) % vertices.Count;
        }
    }
}
