using System.Collections.Generic;

namespace ChiliPepper
{
	public class HexGrid
	{
		public HexGrid()
		{
			_coordinates = new Dictionary<int, HexCoordinates>();
		}

		protected Dictionary<int, HexCoordinates> _coordinates;
		protected IHexNeighborResolver _neighborResolver;
		protected IHexRotateResolver _rotateResolver;

		public Dictionary<int, HexCoordinates> Coordinates => _coordinates;
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
		}

		public HexCoordinates GetNeighbor(HexCoordinates coord, FlatTop.Direction dir)
			=> _neighborResolver.GetNeighbor(coord, (int)dir);
	}
}