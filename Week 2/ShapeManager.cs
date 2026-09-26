using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [Header("Shapes Arrays")]
    public GameObject[] cubes = new GameObject[5];
    public GameObject[] spheres = new GameObject[5];

    [Header("Size Settings")]
    public float cubeSize = 1f;
    public float sphereSize = 1f;
}