// zlib/libpng License
//
// Copyright (c) 2025 RabitBox
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.
namespace RV.SpiceKit.ChiliPepper
{
	/// <summary>
	/// 軸座標を回転させる処理のインターフェース
	/// </summary>
	public interface IHexRotateResolver
	{
		public HexCoordinates Rotate(HexCoordinates coord, Rotate rotate);
		public HexCoordinates Rotate(HexCoordinates coord, HexCoordinates center, Rotate rotate);
	}

	namespace Upward
	{
		/// <summary>
		/// Upward
		/// </summary>
		public class RotateResolver : IHexRotateResolver
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
	}

	namespace Downward
	{
		/// <summary>
		/// Downward
		/// </summary>
		public class RotateResolver : IHexRotateResolver
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
}