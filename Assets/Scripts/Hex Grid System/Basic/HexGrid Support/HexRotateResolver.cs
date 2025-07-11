namespace ChiliPepper
{
	/// <summary>
	/// 軸座標を回転させる処理のインターフェース
	/// </summary>
	public interface IHexRotateResolver
	{
		public HexCoordinates Rotate(HexCoordinates coord, Rotate rotate);
		public HexCoordinates Rotate(HexCoordinates coord, HexCoordinates center, Rotate rotate);
	}

	/// <summary>
	/// Upward
	/// </summary>
	public class UpwardRotateResolver : IHexRotateResolver
	{
		HexCoordinates IHexRotateResolver.Rotate(HexCoordinates coord, Rotate rotate)
		{
			return rotate switch
			{
				Rotate.Right => new HexCoordinates(-coord.S, -coord.Q),
				Rotate.Left => new HexCoordinates(-coord.R, -coord.S),
				_ => throw new System.NotImplementedException(),
			};
		}

		HexCoordinates IHexRotateResolver.Rotate(HexCoordinates coord, HexCoordinates center, Rotate rotate)
		{
			var offset = coord - center;
			offset = rotate switch
			{
				Rotate.Right => new HexCoordinates(-offset.S, -offset.Q),
				Rotate.Left => new HexCoordinates(-offset.R, -offset.S),
				_ => throw new System.NotImplementedException(),
			};
			return offset + center;
		}
	}

	/// <summary>
	/// Downward
	/// </summary>
	public class DownwardRotateResolver : IHexRotateResolver
	{
		HexCoordinates IHexRotateResolver.Rotate(HexCoordinates coord, Rotate rotate)
		{
			return rotate switch
			{
				Rotate.Right => new HexCoordinates(-coord.R, -coord.S),
				Rotate.Left => new HexCoordinates(-coord.S, -coord.Q),
				_ => throw new System.NotImplementedException(),
			};
		}

		HexCoordinates IHexRotateResolver.Rotate(HexCoordinates coord, HexCoordinates center, Rotate rotate)
		{
			var offset = coord - center;
			offset = rotate switch
			{
				Rotate.Right => new HexCoordinates(-coord.R, -coord.S),
				Rotate.Left => new HexCoordinates(-coord.S, -coord.Q),
				_ => throw new System.NotImplementedException(),
			};
			return offset + center;
		}
	}
}