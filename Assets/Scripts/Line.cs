using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private PolygonCollider2D _polygonCollider2D;

    private readonly List<Vector2> _points = new List<Vector2>();
    private Vector2 _nearCurrentPosition = Vector2.zero;

    private float _secondLineDistance = 0.1f;

    private List<Vector2> _reverseVector = new List<Vector2>();
    private List<Vector2> _sumVector = new List<Vector2>();

    private void Start()
    {
        _polygonCollider2D.transform.position = Vector3.zero;
    }

    public void SetPosition(Vector2 position)
    {
        if ( ! CanAppend(position))
        {
            return;
        }

        _points.Add(position);

        ++_lineRenderer.positionCount;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);

        if (_points.Count >= 2)
        {
            MakeObjectsEmpty(); // xor 
            //MakeObjectsNotEmpty();
        }
    }

    private void MakeObjectsNotEmpty()
    {
        _polygonCollider2D.points = _points.ToArray();
    }

    private void MakeObjectsEmpty()
    {
        _reverseVector.Clear();
        for (int i = _points.Count - 1; i >= 0; --i)
        {
            _nearCurrentPosition.x = _points[i].x + _secondLineDistance;
            _nearCurrentPosition.y = _points[i].y + _secondLineDistance;
            _reverseVector.Add(_nearCurrentPosition);
        }

        _sumVector.Clear();
        for (int i = 0; i < _points.Count; ++i)
        {
            _sumVector.Add(_points[i]);
        }
        for (int i = 0; i < _points.Count; ++i)
        {
            _sumVector.Add(_reverseVector[i]);
        }

        _polygonCollider2D.points = _sumVector.ToArray();
    }

    //public void AddReverseColliderLine()
    //{
    //    //int pointsCount = _points.Count - 1;
    //    //for (int i = pointsCount; i >= 0; --i)
    //    //{
    //    //    _nearCurrentPosition.x = _points[i].x + 0.01f;
    //    //    _nearCurrentPosition.y = _points[i].y + 0.01f;
    //    //    _points.Add(_nearCurrentPosition);
    //    //}

    //    //_polygonCollider2D.points = _points.ToArray();
    //}

    private bool CanAppend(Vector2 position)
    {
        if (_lineRenderer.positionCount == 0)
        {
            return true;
        }

        Vector3 previousPosition = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
        float distance = Vector2.Distance(previousPosition, position);

        return distance > Pencil.LineQuality;
    }
}
