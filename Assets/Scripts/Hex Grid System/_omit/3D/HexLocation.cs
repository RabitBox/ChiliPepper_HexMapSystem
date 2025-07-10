using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 高さ情報付きヘックス座標
/// </summary>
[System.Serializable]
public struct HexLocation
{
    [SerializeField] private HexCoordinates _coordinates;
    [SerializeField] private int _height;

    public HexCoordinates Coordinates => _coordinates;
    public int Height => _height;

	#region Override
	public override bool Equals(object obj)
		=> obj is HexLocation other && Equals(other);

	public override readonly int GetHashCode()
		=> _coordinates.GetHashCode();
	#endregion

	#region Operator
	public static bool operator ==(HexLocation a, HexLocation b)
		=> (a._coordinates.Q == b._coordinates.Q) && (a._coordinates.R == b._coordinates.R) && (a._height == b._height);

	public static bool operator !=(HexLocation a, HexLocation b)
		=> !(a == b);
	#endregion

	public HexLocation(int height, int x, int z)
    {
        _height = height;
        _coordinates = new HexCoordinates(x, z);
    }

	public HexLocation(int height, HexCoordinates hexCoordinates)
	{
		_height = height;
		_coordinates = hexCoordinates;
	}

	public List<HexLocation> GetNeighbor()
	{
		var neighbor = _coordinates.GetNeighbor();
		List<HexLocation> result = new List<HexLocation>();

		foreach (var item in neighbor)
		{
			result.Add(new HexLocation(0, item));
		}

		return result;
	}
}
