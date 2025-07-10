using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[System.Serializable]
public struct HexCoordinates : IComparable<HexCoordinates>
{
	#region Field
	[SerializeField] private int _q;
	[SerializeField] private int _r;
	#endregion

	#region Props
	public int X => _q;
	public int Q => _q;
	public int Column => _q;

	public int Z => _r;
	public int R => _r;
	public int Row => _r;

	public int Y { get { return -_q - _r; } }
	public int S { get { return -_q - _r; } }
	#endregion

	#region Constructor
	public HexCoordinates(int x, int z)
	{
		_q = x;
		_r = z;
	}
	#endregion

	#region Override	
	public override readonly string ToString() => $"({_q}, {_r})";

	public int CompareTo(HexCoordinates other)
	{
		int rowCompare = _r.CompareTo(other._r);
		return _r.CompareTo(other._r) != 0 ? rowCompare : _q.CompareTo(other._q);
	}

	public override bool Equals(object obj)
		=> obj is HexCoordinates other && Equals(other);

	public override readonly int GetHashCode()
		=> HashCode.Combine(_q, _r);
	#endregion

	#region Operator
	public static bool operator ==(HexCoordinates a, HexCoordinates b)
		=> (a._q == b._q) && (a._r == b._r);

	public static bool operator !=(HexCoordinates a, HexCoordinates b)
		=> !(a == b);

	public static HexCoordinates operator -(HexCoordinates a, HexCoordinates b)
		=> new HexCoordinates(a._q - b._q, a._r - b._r);

	public static HexCoordinates operator +(HexCoordinates a, HexCoordinates b)
		=> new HexCoordinates(a._q + b._q, a._r + b._r);
	#endregion

	/// <summary>
	/// Get information on adjacent coordinates.
	/// </summary>
	/// <param name="dir">Direction</param>
	/// <returns>Coordinate</returns>
	public HexCoordinates GetNeighborCoordinates(Hex.Direction dir)
	{
		return dir switch
		{
			Hex.Direction.Right => new HexCoordinates(X + 1, Z),
			Hex.Direction.Left => new HexCoordinates(X - 1, Z),
			Hex.Direction.RightUp => new HexCoordinates(X, Z + 1),
			Hex.Direction.LeftDown => new HexCoordinates(X, Z - 1),
			Hex.Direction.LeftUp => new HexCoordinates(X - 1, Z + 1),
			Hex.Direction.RightDown => new HexCoordinates(X + 1, Z - 1),
			_ => throw new System.NotImplementedException(),
		};
	}

	public List<HexCoordinates> GetNeighbor()
	{
		return new List<HexCoordinates> {
			new HexCoordinates(_q+1, _r  ),
			new HexCoordinates(_q+1, _r-1),
			new HexCoordinates(  _q, _r-1),
			new HexCoordinates(_q-1, _r  ),
			new HexCoordinates(_q-1, _r+1),
			new HexCoordinates(  _q, _r+1)
		};
	}

	public static int Distance(HexCoordinates a, HexCoordinates b)
	{
		var vec = a - b;
		return (Mathf.Abs(vec._q) + Mathf.Abs(vec._q + vec._r) + Mathf.Abs(vec._r)) / 2;
	}

	public void Rotate(Hex.Rotate rotate = Hex.Rotate.Right)
	{
		var (q, r) = rotate switch {
			Hex.Rotate.Right	=> (-R, -S),
			Hex.Rotate.Left	=> (-S, -Q),
			_ => throw new System.NotImplementedException(),
		};

		_q = q;
		_r = r;
	}

	public void Rotate(HexCoordinates center, Hex.Rotate rotate = Hex.Rotate.Right)
	{
		var offset_coord = this - center;
		offset_coord.Rotate(rotate);
		offset_coord += center;
		
		_q = offset_coord.Q;
		_r = offset_coord.R;
	}
}