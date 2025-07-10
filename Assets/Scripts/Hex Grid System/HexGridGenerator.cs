using System.Collections.Generic;
using UnityEngine;

public class HexGridGenerator
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static List<HexCoordinates> Generate(int width, int height)
	{
		List<HexCoordinates> results = new();

		for (int dy = 0; dy <= height; dy++)
		{
			for (int dx = 0; dx <= width; dx++)
			{
				results.Add(
					new HexCoordinates(
						dx - (dy/2), 
						dy)
				);
			}
		}

		return results;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="offset"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <returns></returns>
	public static List<HexCoordinates> Generate(HexCoordinates offset, int width, int height)
	{
		List<HexCoordinates> results = new();

		for (int dy = 0; dy <= height; dy++)
		{
			for (int dx = 0; dx <= width; dx++)
			{
				results.Add(
					new HexCoordinates(
						offset.Q + dx - (dy / 2),
						offset.R + dy)
				);
			}
		}

		return results;
	}

	/// <summary>
	/// 指定範囲内のHexGird座標情報を生成
	/// </summary>
	/// <param name="range"></param>
	/// <returns></returns>
	public static List<HexCoordinates> GenerateRange(int range)
	{
		List<HexCoordinates> results = new();

		for (int dx = -range; dx <= range; dx++)
		{
			for (int dy = Mathf.Max(-range, -dx - range); dy <= Mathf.Min(range, -dx + range); dy++)
			{
				results.Add(
					new HexCoordinates(dx, dy)
				);
			}
		}

		return results;
	}

	/// <summary>
	/// 指定範囲内のHexGird座標情報を生成
	/// </summary>
	/// <param name="offset"></param>
	/// <param name="range"></param>
	/// <returns></returns>
	public static List<HexCoordinates> GenerateRange(HexCoordinates offset, int range)
	{
		List<HexCoordinates> results = new();

		for (int dx = -range; dx <= range; dx++)
		{
			for (int dy = Mathf.Max(-range, -dx - range); dy <= Mathf.Min(range, -dx + range); dy++)
			{
				results.Add(
					new HexCoordinates(offset.Q + dx, offset.R + dy)
				);
			}
		}

		return results;
	}
}
