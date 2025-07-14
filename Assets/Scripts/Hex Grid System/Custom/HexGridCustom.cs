using System.Collections.Generic;
using UnityEngine;

namespace ChiliPepper.Custom
{
	public class HexGrid<T>
		where T : IHexCell
	{
		/// <summary>
		/// 座標データ
		/// </summary>
		protected Dictionary<int, T> _coords;

		/// <summary>
		/// 隣接情報処理
		/// </summary>
		protected IHexNeighborResolver _neighborResolver;

		/// <summary>
		/// 回転処理
		/// </summary>
		protected IHexRotateResolver _rotateResolver;

		/// <summary>
		/// コンバーター
		/// </summary>
		protected IHexGridConverter _hexGridConverter;

		/// <summary>
		/// 経路探索
		/// </summary>
		protected IHexGridPathfinding<T> _hexGridPathfinding;

		public HexGrid()
		{
			_coords = new Dictionary<int, T>();
			_neighborResolver = new PointyTop.Upward.NeighborResolver();
			_rotateResolver = new UpwardRotateResolver();
			_hexGridConverter = new PointyTop.Upward.Converter();
			_hexGridPathfinding = new ASter.Pathfinding<T>();
		}

		#region Props
		public Dictionary<int, T> Coords => _coords;
		public IHexNeighborResolver HexNeighbor => _neighborResolver;
		public IHexRotateResolver HexRotate => _rotateResolver;
		public IHexGridConverter Converter => _hexGridConverter;
		public IHexGridPathfinding<T> Pathfinding => _hexGridPathfinding;
		#endregion
	}
}

