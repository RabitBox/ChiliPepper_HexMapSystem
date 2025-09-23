// zlib/libpng License
//
// Copyright (c) 2025 RabitBox
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.
namespace RVSpiceKit.ChiliPepper
{
	/// <summary>
	/// 隣接する軸座標を取得する処理のインターフェース
	/// </summary>
	public interface IHexNeighborResolver
	{
		public HexCoordinates GetNeighbor(HexCoordinates coord, int dir);
	}

	namespace PointyTop
	{
		namespace Upward
		{
			/// <summary>
			/// Upward PointyTop
			/// </summary>
			public class NeighborResolver : IHexNeighborResolver
			{
				HexCoordinates IHexNeighborResolver.GetNeighbor(HexCoordinates coord, int dir)
				{
					return dir switch
					{
						0 => new HexCoordinates(coord.Q    , coord.R + 1),  // 右上
						1 => new HexCoordinates(coord.Q + 1, coord.R),      // 右
						2 => new HexCoordinates(coord.Q + 1, coord.R - 1),  // 右下
						3 => new HexCoordinates(coord.Q    , coord.R - 1),  // 左下
						4 => new HexCoordinates(coord.Q - 1, coord.R),      // 左
						5 => new HexCoordinates(coord.Q - 1, coord.R + 1),  // 左上
						_ => throw new System.NotImplementedException(),
					};

				}
			}
		}

		namespace Downward
		{
			/// <summary>
			/// Downward PointyTop
			/// </summary>
			public class NeighborResolver : IHexNeighborResolver
			{
				HexCoordinates IHexNeighborResolver.GetNeighbor(HexCoordinates coord, int dir)
				{
					return dir switch
					{
						0 => new HexCoordinates(coord.Q + 1, coord.R - 1),  // 右上
						1 => new HexCoordinates(coord.Q + 1, coord.R),      // 右
						2 => new HexCoordinates(coord.Q    , coord.R + 1),  // 右下
						3 => new HexCoordinates(coord.Q - 1, coord.R + 1),  // 左下
						4 => new HexCoordinates(coord.Q - 1, coord.R),      // 左
						5 => new HexCoordinates(coord.Q    , coord.R - 1),  // 左上
						_ => throw new System.NotImplementedException(),
					};
				}
			}
		}
	}

	namespace FlatTop
	{
		namespace Upward
		{
			/// <summary>
			/// Upward FlatTop
			/// </summary>
			public class NeighborResolver : IHexNeighborResolver
			{
				HexCoordinates IHexNeighborResolver.GetNeighbor(HexCoordinates coord, int dir)
				{
					return dir switch
					{
						0 => new HexCoordinates(coord.Q    , coord.R + 1),  // 上
						1 => new HexCoordinates(coord.Q + 1, coord.R),      // 右上
						2 => new HexCoordinates(coord.Q + 1, coord.R - 1),  // 右下
						3 => new HexCoordinates(coord.Q    , coord.R - 1),  // 下
						4 => new HexCoordinates(coord.Q - 1, coord.R),      // 左下
						5 => new HexCoordinates(coord.Q - 1, coord.R + 1),  // 左上
						_ => throw new System.NotImplementedException(),
					};
				}
			}
		}

		namespace Downward
		{
			/// <summary>
			/// Downward FlatTop
			/// </summary>
			public class NeighborResolver : IHexNeighborResolver
			{
				HexCoordinates IHexNeighborResolver.GetNeighbor(HexCoordinates coord, int dir)
				{
					return dir switch
					{
						0 => new HexCoordinates(coord.Q    , coord.R - 1),  // 上
						1 => new HexCoordinates(coord.Q + 1, coord.R - 1),  // 右上
						2 => new HexCoordinates(coord.Q + 1, coord.R),      // 右下
						3 => new HexCoordinates(coord.Q    , coord.R + 1),  // 下
						4 => new HexCoordinates(coord.Q - 1, coord.R + 1),  // 左下
						5 => new HexCoordinates(coord.Q - 1, coord.R),      // 左上
						_ => throw new System.NotImplementedException(),
					};
				}
			}
		}		
	}
}
