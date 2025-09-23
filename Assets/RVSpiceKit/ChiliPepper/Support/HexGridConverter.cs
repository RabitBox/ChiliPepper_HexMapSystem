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
using System;

namespace RVSpiceKit.ChiliPepper
{
	public interface IHexGridConverter
	{
		public (float x, float y) ToWorld(int col, int row, float radius = 1f);
		public (float x, float y) ToWorld(HexCoordinates coordinates, float radius = 1f);
	}

	namespace PointyTop
	{
		namespace Upward
		{
			/// <summary>
			/// PointyTopレイアウトの座標に変換するクラス
			/// </summary>
			public class Converter : IHexGridConverter
			{
				public (float x, float y) ToWorld(int col, int row, float radius = 1f)
				{
					float width = (float)Math.Sqrt(3f) * radius;
					float height = 1.5f * radius;

					float x = width * col + (row * width * 0.5f);
					float y = row * height;

					return (x, y);
				}

				public (float x, float y) ToWorld(HexCoordinates coordinates, float radius = 1f)
					=> ToWorld(coordinates.Column, coordinates.Row, radius);
			}
		}

		namespace Downward
		{
			/// <summary>
			/// PointyTopレイアウトの座標に変換するクラス
			/// </summary>
			public class Converter : IHexGridConverter
			{
				public (float x, float y) ToWorld(int col, int row, float radius = 1f)
				{
					float width = (float)Math.Sqrt(3f) * radius;
					float height = 1.5f * radius;

					float x = width * col + (row * width * 0.5f);
					float y = row * height;

					return (x, -y);
				}

				public (float x, float y) ToWorld(HexCoordinates coordinates, float radius = 1f)
					=> ToWorld(coordinates.Column, coordinates.Row, radius);
			}
		}
	}

	namespace FlatTop
	{
		namespace Upward
		{
			/// <summary>
			/// FlatTopレイアウトの座標に変換するクラス
			/// </summary>
			public class Converter : IHexGridConverter
			{
				public (float x, float y) ToWorld(int col, int row, float radius = 1f)
				{
					float horiz = 1.5f * radius;
					float vert = (float)Math.Sqrt(3f) * radius;

					float x = horiz * col;
					float y = vert * row + (col * vert * 0.5f);

					return (x, y);
				}

				public (float x, float y) ToWorld(HexCoordinates coordinates, float radius = 1f)
					=> ToWorld(coordinates.Column, coordinates.Row, radius);
			}
		}

		namespace Downward
		{
			/// <summary>
			/// FlatTopレイアウトの座標に変換するクラス
			/// </summary>
			public class Converter : IHexGridConverter
			{
				public (float x, float y) ToWorld(int col, int row, float radius = 1f)
				{
					float horiz = 1.5f * radius;
					float vert = (float)Math.Sqrt(3f) * radius;

					float x = horiz * col;
					float y = vert * row + (col * vert * 0.5f);

					return (x, -y);
				}

				public (float x, float y) ToWorld(HexCoordinates coordinates, float radius = 1f)
					=> ToWorld(coordinates.Column, coordinates.Row, radius);
			}
		}
	}
}