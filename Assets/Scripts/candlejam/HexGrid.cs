using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instructions for use.
// Create a HexGrid instance and set it's size. The size is the distance from the center to one of the hex corners. Defaults to 1.0f (which is a width of 2.0 in world co-ordinates)
// Reference: https://www.redblobgames.com/grids/hexagons/


[System.Serializable]
public struct OffsetCoordinate
{
    public int col;
    public int row;

    public OffsetCoordinate(int _col, int _row)
    {
        col = _col;
        row = _row;
    }

    public OffsetCoordinate(CubeCoordinate cc)
    {
        col = cc.x + (cc.z - (cc.z & 1)) / 2;
        row = cc.z;
    }

    public OffsetCoordinate(AxialCoordinate ac)
    {
        col = ac.q + (ac.r - (ac.r & 1)) / 2;
        row = ac.r;
    }

}

[System.Serializable]
public struct AxialCoordinate : System.IEquatable<AxialCoordinate>
{
    public static AxialCoordinate[] Directions =
    {
        new AxialCoordinate(1, 0),
        new AxialCoordinate(1, -1),
        new AxialCoordinate(0, -1),
        new AxialCoordinate(-1, 0),
        new AxialCoordinate(-1, 1),
        new AxialCoordinate(0, 1),
    };

    public static AxialCoordinate zero = new AxialCoordinate(0, 0);

    public int q;
    public int r;

    public static AxialCoordinate operator +(AxialCoordinate a, AxialCoordinate b)
    {
        return new AxialCoordinate(a.q + b.q, a.r + b.r);
    }

    public static int DistanceBetween(AxialCoordinate a, AxialCoordinate b)
    {
        return (Mathf.Abs(a.q - b.q)
              + Mathf.Abs(a.q + a.r - b.q - b.r)
              + Mathf.Abs(a.r - b.r)) / 2;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            AxialCoordinate ac = (AxialCoordinate)obj;
            return (q == ac.q) && (r == ac.r);
        }
    }

    public bool Equals(AxialCoordinate ac)
    {
        return (q == ac.q) && (r == ac.r);
    }

    public override int GetHashCode()
    {
        return q * 0x00010000 + r;
    }

    public AxialCoordinate(int _q, int _r)
    {
        q = _q;
        r = _r;
    }

    public AxialCoordinate(CubeCoordinate cc)
    {
        q = cc.x;
        r = cc.z;
    }

    public AxialCoordinate(OffsetCoordinate oc)
    {
        q = oc.col - (oc.row - (oc.row & 1)) / 2;
        r = oc.row;
    }
}

[System.Serializable]
public struct CubeCoordinate
{
    public int x;
    public int y;
    public int z;

    public CubeCoordinate(int _x, int _y, int _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    public CubeCoordinate(AxialCoordinate ac)
    {
        x = ac.q;
        z = ac.r;
        y = -x - z;
    }

    public CubeCoordinate Rotate(int steps)
    {
        steps = steps % 6;
        int _x = 0;
        int _y = 0;
        int _z = 0;

        switch (steps)
        {
            case 0:
                _x = x;
                _y = y;
                _z = z;
                break;
            case 1:
                _x = -z;
                _y = -x;
                _z = -y;
                break;
            case 2:
                _x = y;
                _y = z;
                _z = x;
                break;
            case 3:
                _x = -x;
                _y = -y;
                _z = -z;
                break;
            case 4:
                _x = z;
                _y = x;
                _z = y;
                break;
            case 5:
                _x = -y;
                _y = -z;
                _z = -x;
                break;
        }
        x = _x;
        y = _y;
        z = _z;

        return this;
    }
}


public class HexGrid 
{
    private float Size = 0.0f;
    private float HexWidth = 0.0f;
    private float HexHeight = 0.0f;
    private float GridWidth = 0.0f;
    private float GridHeight = 0.0f;

    public HexGrid(float _size = 1.0f)
    {
        SetGridSize(_size);
    }

    public void SetGridSize(float _size)
    {
        Size = _size;
        HexWidth = Size * 1.732050807f;
        HexHeight = Size * 2.0f;
        GridWidth = HexWidth;
        GridHeight = HexHeight * 0.75f;
    }

    public AxialCoordinate AxialCoordinateFromWorldPosition(Vector3 wp)
    {
        OffsetCoordinate oc = new OffsetCoordinate();
        oc.row = Mathf.FloorToInt((wp.z + GridHeight * 0.5f) / GridHeight);
        oc.col = Mathf.FloorToInt((wp.x + GridWidth * 0.5f - (oc.row & 1) * GridWidth * 0.5f) / GridWidth);

        return new AxialCoordinate(oc);
    }

    public Vector3 WorldPositionFromAxialPosition(AxialCoordinate ac)
    {
        OffsetCoordinate oc = new OffsetCoordinate(ac);

        return new Vector3(
            GridWidth * oc.col + (oc.row & 1) * GridWidth * 0.5f,
            0.0f,
            GridHeight * oc.row
            );
    }

    private int NumValidCollidersAtAxialPositionQuery = 0;
    private Collider[] CollidersAtLastAxialPositionQueried = new Collider[20];
    public bool QueryForCollidersAtAxialPosition(AxialCoordinate ac, int layerMask)
    {
        NumValidCollidersAtAxialPositionQuery = Physics.OverlapSphereNonAlloc(WorldPositionFromAxialPosition(ac), 0.0f, CollidersAtLastAxialPositionQueried, layerMask);
        return NumValidCollidersAtAxialPositionQuery > 0;
    }
    public int GetQueriedColliderCount()
    {
        return NumValidCollidersAtAxialPositionQuery;
    }
    public IEnumerable<Collider> IterateQueriedColliders()
    {
        for (int i = 0; i < NumValidCollidersAtAxialPositionQuery; i++)
        {
            yield return CollidersAtLastAxialPositionQueried[i];
        }
    }

    public T GetAnyComponentAtAxialPosition<T>(AxialCoordinate ac, int layerMask)
    {

        QueryForCollidersAtAxialPosition(ac, layerMask);
        foreach (Collider c in IterateQueriedColliders())
        {
            T retval = c.GetComponent<T>();
            if (!EqualityComparer<T>.Default.Equals(retval, default(T)))
                return retval;
        }
        return default(T);
    }

    public IEnumerable<T> GetAllComponentsAtAxialPosition<T>(AxialCoordinate ac, int layerMask)
    {
        QueryForCollidersAtAxialPosition(ac, layerMask);
        foreach (Collider c in IterateQueriedColliders())
        {
            T retval = c.GetComponent<T>();
            if (!EqualityComparer<T>.Default.Equals(retval, default(T)))
                yield return retval;
        }
    }
}
