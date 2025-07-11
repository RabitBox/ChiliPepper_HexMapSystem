using System.Collections.Generic;

namespace ChiliPepper
{
	public class HexGrid
	{
		/// <summary>
		/// 座標データ
		/// </summary>
		protected Dictionary<int, HexCoordinates> _coordinates;

		/// <summary>
		/// 隣接情報処理
		/// </summary>
		protected IHexNeighborResolver _neighborResolver;

		/// <summary>
		/// 回転処理
		/// </summary>
		protected IHexRotateResolver _rotateResolver;

		/// <summary>
		/// ジェネレータ
		/// </summary>
		protected IHexGridGenerator _hexGridGenerator;

		public HexGrid()
		{
			_coordinates = new Dictionary<int, HexCoordinates>();
		}

		public Dictionary<int, HexCoordinates> Coordinates => _coordinates;
		public IHexNeighborResolver HexNeighbor => _neighborResolver;
		public IHexRotateResolver HexRotate => _rotateResolver;
		public IHexGridGenerator Generator => _hexGridGenerator;
	}

	/// <summary>
	/// PointyTop系HexGridの管理クラス
	/// </summary>
	public class PointyTopHexGrid : HexGrid
	{
		public PointyTopHexGrid(LayoutOrder order)
		{
			_coordinates = new Dictionary<int, HexCoordinates>();
			_neighborResolver = order switch
			{
				LayoutOrder.Upward => new PointyTop.Upward.NeighborResolver(),
				LayoutOrder.Downward => new PointyTop.Downward.NeighborResolver(),
				_ => throw new System.NotImplementedException(),
			};
			_rotateResolver = order switch
			{
				LayoutOrder.Upward => new UpwardRotateResolver(),
				LayoutOrder.Downward => new DownwardRotateResolver(),
				_ => throw new System.NotImplementedException(),
			};
			_hexGridGenerator = new PointyTop.GridGenerator();
		}

		public HexCoordinates GetNeighbor(HexCoordinates coord, PointyTop.Direction dir)
			=> _neighborResolver.GetNeighbor(coord, (int)dir);
	}

	/// <summary>
	/// FlatTop系HexGrid管理クラス
	/// </summary>
	public class FlatTopHexGrid : HexGrid
	{
		public FlatTopHexGrid(LayoutOrder order)
		{
			_coordinates = new Dictionary<int, HexCoordinates>();
			_neighborResolver = order switch
			{
				LayoutOrder.Upward => new FlatTop.Upward.NeighborResolver(),
				LayoutOrder.Downward => new FlatTop.Downward.NeighborResolver(),
				_ => throw new System.NotImplementedException(),
			};
			_rotateResolver = order switch
			{
				LayoutOrder.Upward => new UpwardRotateResolver(),
				LayoutOrder.Downward => new DownwardRotateResolver(),
				_ => throw new System.NotImplementedException(),
			};
			_hexGridGenerator = new FlatTop.GridGenerator();
		}

		public HexCoordinates GetNeighbor(HexCoordinates coord, FlatTop.Direction dir)
			=> _neighborResolver.GetNeighbor(coord, (int)dir);
	}
}