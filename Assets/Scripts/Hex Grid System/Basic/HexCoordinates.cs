using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChiliPepper
{
	/// <summary>
	/// 軸座標
	/// </summary>
	[System.Serializable]
	public struct HexCoordinates : IComparable<HexCoordinates>
	{
		#region Field
		[SerializeField] private int _q;
		[SerializeField] private int _r;
		#endregion

		#region Props
		public int Q => _q;
		public int Column => _q;

		public int R => _r;
		public int Row => _r;

		public int S { get { return -_q - _r; } }
		#endregion

		#region Constructor
		public HexCoordinates(int x, int z)
		{
			_q = x;
			_r = z;
		}
		#endregion

		#region Override	
		public override readonly string ToString() => $"({_q}, {_r})";

		public int CompareTo(HexCoordinates other)
		{
			int rowCompare = _r.CompareTo(other._r);
			return _r.CompareTo(other._r) != 0 ? rowCompare : _q.CompareTo(other._q);
		}

		public override bool Equals(object obj)
			=> obj is HexCoordinates other && Equals(other);

		public override readonly int GetHashCode()
			=> HashCode.Combine(_q, _r);
		#endregion

		#region Operator
		public static bool operator ==(HexCoordinates a, HexCoordinates b)
			=> (a._q == b._q) && (a._r == b._r);

		public static bool operator !=(HexCoordinates a, HexCoordinates b)
			=> !(a == b);

		public static HexCoordinates operator -(HexCoordinates a, HexCoordinates b)
			=> new HexCoordinates(a._q - b._q, a._r - b._r);

		public static HexCoordinates operator +(HexCoordinates a, HexCoordinates b)
			=> new HexCoordinates(a._q + b._q, a._r + b._r);
		#endregion

		/// <summary>
		/// 隣接する軸座標
		/// </summary>
		/// <returns></returns>
		public List<HexCoordinates> GetNeighbors()
		{
			return new List<HexCoordinates> {
				new HexCoordinates(_q+1, _r  ),
				new HexCoordinates(_q+1, _r-1),
				new HexCoordinates(  _q, _r-1),
				new HexCoordinates(_q-1, _r  ),
				new HexCoordinates(_q-1, _r+1),
				new HexCoordinates(  _q, _r+1)
			};
		}

		public static int Distance(HexCoordinates a, HexCoordinates b)
		{
			var vec = a - b;
			return (Math.Abs(vec._q) + Math.Abs(vec._q + vec._r) + Math.Abs(vec._r)) / 2;
		}
	}
}