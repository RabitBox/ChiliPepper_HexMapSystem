using System;
using UnityEngine;

namespace ChiliPepper
{
	/// <summary>
	/// 高さ情報付きヘックス座標
	/// </summary>
	[System.Serializable]
	public class HexLocation : Custom.HexCoord
	{
		#region Field
		/// <summary>
		/// 高さ
		/// </summary>
		[SerializeField] private int _height;
		#endregion

		#region Props
		public int Height => _height;
		#endregion

		#region Constructor
		public HexLocation(int height, int x, int z)
		{
			_height = height;
			_coord = new HexCoordinates(x, z);
		}

		public HexLocation(int height, HexCoordinates hexCoordinates)
		{
			_height = height;
			_coord = hexCoordinates;
		}
		#endregion

		#region Operator
		public static bool operator ==(HexLocation a, HexLocation b)
		{
			if (ReferenceEquals(a, b)) return true;
			if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
			return (a._coord.Q == b._coord.Q) && (a._coord.R == b._coord.R) && (a._height == b._height);
		}

		public static bool operator !=(HexLocation a, HexLocation b)
			=> !(a == b);
		#endregion

		#region Override
		public override string ToString() => $"({_coord.Q}, {_coord.R}, {_height})";

		public override bool Equals(object obj)
			=> obj is HexLocation other && Equals(other);

		public override int GetHashCode() => _coord.GetHashCode();

		public override bool IsValidStep(Custom.HexCoord obj)
		{
			if (obj is HexLocation other)
			{
				return Math.Abs(this._height - other._height) <= 1;
			}
			return false;
		}
		#endregion
	}
}

