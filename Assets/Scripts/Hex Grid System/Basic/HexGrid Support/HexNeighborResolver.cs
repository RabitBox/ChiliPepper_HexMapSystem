namespace ChiliPepper
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
						0 => new HexCoordinates(coord.Q, coord.R + 1),      // 右上
						1 => new HexCoordinates(coord.Q + 1, coord.R),      // 右
						2 => new HexCoordinates(coord.Q + 1, coord.R - 1),  // 右下
						3 => new HexCoordinates(coord.Q, coord.R - 1),      // 左下
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
						2 => new HexCoordinates(coord.Q, coord.R + 1),      // 右下
						3 => new HexCoordinates(coord.Q - 1, coord.R + 1),  // 左下
						4 => new HexCoordinates(coord.Q - 1, coord.R),      // 左
						5 => new HexCoordinates(coord.Q, coord.R - 1),      // 左上
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
						0 => new HexCoordinates(coord.Q, coord.R + 1),      // 上
						1 => new HexCoordinates(coord.Q + 1, coord.R),      // 右上
						2 => new HexCoordinates(coord.Q + 1, coord.R - 1),  // 右下
						3 => new HexCoordinates(coord.Q, coord.R - 1),      // 下
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
						0 => new HexCoordinates(coord.Q, coord.R - 1),      // 上
						1 => new HexCoordinates(coord.Q + 1, coord.R - 1),  // 右上
						2 => new HexCoordinates(coord.Q + 1, coord.R),      // 右下
						3 => new HexCoordinates(coord.Q, coord.R + 1),      // 下
						4 => new HexCoordinates(coord.Q - 1, coord.R + 1),  // 左下
						5 => new HexCoordinates(coord.Q - 1, coord.R),      // 左上
						_ => throw new System.NotImplementedException(),
					};
				}
			}
		}		
	}
}
