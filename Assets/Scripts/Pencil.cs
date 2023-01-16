using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    private Camera _camera;
    //private Transform _cameraTransform;

    [SerializeField] private Line _linePrefab;
    public const float LineQuality = 0.1f;

    private Line _currentLine;

    private GameObject _lineParent;

    private void Start()
    {
        _camera = Camera.main;
        //_cameraTransform = _camera.transform;
    }


    private void Update()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        CreateNewLine(mousePosition);

        ContinueDrawingCurrentLine(mousePosition);

        AddRigidbody();

        DeleteLine(mousePosition);
    }
    private void CreateNewLine(Vector2 mousePosition)
    {
        if (Input.GetMouseButtonDown(0)) // create new line
        {
            _lineParent = new GameObject();

            _currentLine = Instantiate(_linePrefab, mousePosition, Quaternion.identity);
            _currentLine.transform.SetParent(_lineParent.transform);
        }
    }

    private void ContinueDrawingCurrentLine(Vector2 mousePosition)
    {
        if (Input.GetMouseButton(0)) // continue drawing current line
        {
            _currentLine.SetPosition(mousePosition);
        }
    }

    private void AddRigidbody()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Rigidbody2D rb = _lineParent.AddComponent<Rigidbody2D>();
            rb.useAutoMass = true;
        }
    }

    private void DeleteLine(Vector2 mousePosition)
    {
        if (Input.GetMouseButton(1)) // delete line with mouse right click
        {
            RaycastHit2D hit2D = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            Collider2D collider2D = hit2D.collider;

            if (collider2D != null)
            {
                if (collider2D.tag == "Line")
                {
                    Destroy(collider2D.gameObject);
                }
            }
        }
    }
}

//public class ColliderToMesh : MonoBehaviour
//{
//    PolygonCollider2D pc2;
//    void Start()
//    {
//        pc2 = gameObject.GetComponent<PolygonCollider2D>();
//        //Render thing
//        int pointCount = 0;
//        pointCount = pc2.GetTotalPointCount();
//        MeshFilter mf = GetComponent<MeshFilter>();
//        Mesh mesh = new Mesh();
//        Vector2[] points = pc2.points;
//        Vector3[] vertices = new Vector3[pointCount];
//        Vector2[] uv = new Vector2[pointCount];
//        for (int j = 0; j < pointCount; j++)
//        {
//            Vector2 actual = points[j];
//            vertices[j] = new Vector3(actual.x, actual.y, 0);
//            uv[j] = actual;
//        }
//        Triangulator tr = new Triangulator(points);
//        int[] triangles = tr.Triangulate();
//        mesh.vertices = vertices;
//        mesh.triangles = triangles;
//        mesh.uv = uv;
//        mf.mesh = mesh;
//        //Render thing
//    }
//}

//public class Draw : MonoBehaviour
//{
//    private LineRenderer _lineRenderer = null;
//    private List<Vector3> _points = null;

//    private void Start()
//    {
//        _lineRenderer = gameObject.AddComponent<LineRenderer>();
//        _lineRenderer.positionCount = 0;
//        _lineRenderer.startColor = Color.white;
//        _lineRenderer.endColor = Color.white;
//        _lineRenderer.startWidth = 0.1f;
//        _lineRenderer.endWidth = 0.1f;
//        _lineRenderer.material = new Material(Shader.Find("Particles/Additive"));//("Legacy Shaders/Particles/Additive"));
//        _points = new List<Vector3>();
//    }

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            if (_lineRenderer != null)
//            {
//                _lineRenderer.positionCount = 0;
//            }

//            if (_points != null)
//            {
//                _points.Clear();
//            }
//        }

//        if (Input.GetMouseButtonDown(0))
//        {
//            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            worldPos.z = 0.0f;

//            if (_points.Contains(worldPos) == false)
//            {
//                _points.Add(worldPos);

//                _lineRenderer.positionCount = _points.Count;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, worldPos);
//            }
//        }
//    }
//}
