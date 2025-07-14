using UnityEngine;

namespace ChiliPepper.Custom.Sample
{
	/// <summary>
	/// IHexCellの継承サンプル
	/// </summary>
	public struct HexGridCell : IHexCell
	{
		[SerializeField] private HexCoordinates _coordinates;

		public HexCoordinates Coordinates { get => _coordinates; set => _coordinates = value; }

		public int HashCode => _coordinates.GetHashCode();

		public bool IsValidStep(IHexCell other)
		{
			if (other is HexGridCell obj)
			{
				return true;
			}
			return false;
		}
	}
}