using System.Collections.Generic;
using UnityEngine;

namespace ChiliPepper.Custom
{
	/// <summary>
	/// 拡張用 HexCoord クラス
	/// </summary>
	public abstract class HexCoord
	{
		[SerializeField] protected HexCoordinates _coord;

		public HexCoordinates Coord => _coord;

		public override int GetHashCode() => _coord.GetHashCode();

		/// <summary>
		/// 隣接する軸座標
		/// </summary>
		/// <returns></returns>
		public List<HexCoordinates> GetNeighbors()
		{
			return new List<HexCoordinates> {
				new HexCoordinates(_coord.Q+1, _coord.R  ),
				new HexCoordinates(_coord.Q+1, _coord.R-1),
				new HexCoordinates(  _coord.Q, _coord.R-1),
				new HexCoordinates(_coord.Q-1, _coord.R  ),
				new HexCoordinates(_coord.Q-1, _coord.R+1),
				new HexCoordinates(  _coord.Q, _coord.R+1)
			};
		}

		/// <summary>
		/// 経路探索アルゴリズムで使用する判定処理
		/// </summary>
		/// <param name="obj">対象</param>
		/// <returns></returns>
		public abstract bool IsValidStep(HexCoord obj);
	}
}
