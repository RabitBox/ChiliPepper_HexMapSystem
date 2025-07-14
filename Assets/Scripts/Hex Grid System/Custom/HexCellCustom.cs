namespace ChiliPepper.Custom
{
	/// <summary>
	/// HexGridで管理されるセルデータのインターフェース
	/// </summary>
	public interface IHexCell
	{
		/// <summary>
		/// 軸座標
		/// </summary>
		public HexCoordinates Coordinates { get; set; }

		/// <summary>
		/// 移動可能判定
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool IsValidStep(IHexCell other);

		/// <summary>
		/// ハッシュ値
		/// </summary>
		/// <returns></returns>
		public int HashCode { get; }
	}
}

