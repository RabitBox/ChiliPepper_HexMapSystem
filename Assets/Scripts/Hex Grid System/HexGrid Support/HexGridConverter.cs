using System;

namespace ChiliPepper
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