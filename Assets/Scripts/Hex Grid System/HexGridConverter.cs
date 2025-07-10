using UnityEngine;

public interface IHexGridConverter
{
	public (float x, float y) ToWorld(int col, int row, float radius = 1f);
	public (float x, float y) ToWorld(HexCoordinates coordinates, float radius = 1f);
}

/// <summary>
/// PointyTopレイアウトの座標に変換するクラス
/// </summary>
public class PointyTopHexConverter : IHexGridConverter
{
	public (float x, float y) ToWorld(int col, int row, float radius = 1f)
	{
		float width = Mathf.Sqrt(3f) * radius;
		float height = 1.5f * radius;

		float x = width * col + (row * width * 0.5f);
		float y = row * height;

		return (x, y);
	}

	public (float x, float y) ToWorld(HexCoordinates coordinates, float radius = 1f)
		=> ToWorld(coordinates.Column, coordinates.Row, radius);
}

/// <summary>
/// FlatTopレイアウトの座ホ湯に変換するクラス
/// </summary>
public class FlatTopHexConverter : IHexGridConverter
{
	public (float x, float y) ToWorld(int col, int row, float radius = 1f)
	{
		float horiz = 1.5f * radius;
		float vert = Mathf.Sqrt(3f) * radius;

		float x = horiz * col;
		float y = vert * row + (col * vert * 0.5f);

		return (x, y);
	}

	public (float x, float y) ToWorld(HexCoordinates coordinates, float radius = 1f)
		=> ToWorld(coordinates.Column, coordinates.Row, radius);
}