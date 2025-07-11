using UnityEngine;

namespace ChiliPepper
{
	/// <summary>
	/// HexCoordinatesに
	/// </summary>
	[System.Serializable]
	public abstract class CHexCoord
	{
		[SerializeField] protected HexCoordinates _coord;

		public HexCoordinates Coord => _coord;

		public override int GetHashCode()
			=> _coord.GetHashCode();

		/// <summary>
		/// 経路探索アルゴリズムで使用する判定処理
		/// </summary>
		/// <param name="obj">対象</param>
		/// <returns></returns>
		public abstract bool IsValidStep(CHexCoord obj);
	}
}