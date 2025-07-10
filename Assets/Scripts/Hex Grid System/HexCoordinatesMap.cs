using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HexCoordinatesの集合情報を管理するクラス
/// </summary>
public class HexCoordinatesMap
{
	HexCoordinatesMap()
	{
		_coordinates = new Dictionary<int, HexCoordinates>();
	}

	private Dictionary<int, HexCoordinates> _coordinates;

	public Dictionary<int, HexCoordinates> Coordinates => _coordinates;
}
