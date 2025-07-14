namespace ChiliPepper
{
	namespace PointyTop
	{
		public enum Direction : int
		{
			RightUp,    // 右上
			Right,      // 右
			RightDown,  // 右下
			LeftDown,   // 左下
			Left,       // 左
			LeftUp,     // 左上

			Max,        // 最大数
		}
	}

	namespace FlatTop
	{
		public enum Direction : int
		{
			Up,         // 上
			RightUp,    // 右上
			RightDown,  // 右下
			Down,       // 下
			LeftDown,   // 左下
			LeftUp,     // 左上

			Max,        // 最大数
		}
	}

	/// <summary>
	/// 軸の配置方向
	/// </summary>
	public enum LayoutOrder
	{
		Upward,		// 上向き
		Downward,	// 下向き
	}

	public enum Rotate
	{
		Right,  // Clockwise
		Left    // CounterClockwise
	}
}