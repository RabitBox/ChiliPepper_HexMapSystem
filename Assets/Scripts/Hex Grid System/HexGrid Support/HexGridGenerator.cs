using System;
using System.Collections.Generic;

namespace ChiliPepper
{
	/// <summary>
	/// 指定した範囲の軸座標情報を出力
	/// </summary>
	public interface IHexGridGenerator
	{
		/// <summary>
		/// 指定した縦横幅の軸座標を出力
		/// </summary>
		/// <param name="width">横幅</param>
		/// <param name="height">縦幅</param>
		/// <returns></returns>
		public List<HexCoordinates> Generate(int width, int height);

		/// <summary>
		/// 指定した範囲の軸座標を出力
		/// </summary>
		/// <param name="range">範囲</param>
		/// <returns></returns>
		public List<HexCoordinates> GenerateRange(int range);
	}

	namespace PointyTop
	{
		/// <summary>
		/// odd-r 配置の軸座標を出力
		/// </summary>
		public class GridGenerator : IHexGridGenerator
		{
			List<HexCoordinates> IHexGridGenerator.Generate(int width, int height)
			{
				List<HexCoordinates> results = new();
				for (int dy = 0; dy <= height; dy++)
				{
					for (int dx = 0; dx <= width; dx++)
					{
						results.Add( new HexCoordinates(dx-(dy/2), dy) );
					}
				}
				return results;
			}

			List<HexCoordinates> IHexGridGenerator.GenerateRange(int range)
			{
				List<HexCoordinates> results = new();
				for (int dx = -range; dx <= range; dx++)
				{
					for (int dy = Math.Max(-range, -dx - range); dy <= Math.Min(range, -dx + range); dy++)
					{
						results.Add(
							new HexCoordinates(dx, dy)
						);
					}
				}
				return results;
			}
		}
	}

	namespace FlatTop
	{
		/// <summary>
		/// odd-q 配置の軸座標を出力
		/// </summary>
		public class GridGenerator : IHexGridGenerator
		{
			List<HexCoordinates> IHexGridGenerator.Generate(int width, int height)
			{
				List<HexCoordinates> results = new();
				for (int dy = 0; dy <= height; dy++)
				{
					for (int dx = 0; dx <= width; dx++)
					{
						results.Add(new HexCoordinates(dx, dy-(dx/2)));
					}
				}
				return results;
			}

			List<HexCoordinates> IHexGridGenerator.GenerateRange(int range)
			{
				List<HexCoordinates> results = new();
				for (int dx = -range; dx <= range; dx++)
				{
					for (int dy = Math.Max(-range, -dx - range); dy <= Math.Min(range, -dx + range); dy++)
					{
						results.Add(
							new HexCoordinates(dx, dy)
						);
					}
				}
				return results;
			}
		}
	}
}


