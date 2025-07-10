
namespace Hex
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

	public enum Rotate
	{
		Right,  // Clockwise
		Left    // CounterClockwise
	}

	//public static class HexDirectionExtensions
	//{
	//	public static HexDirection Opposite(this HexDirection direction)
	//	{
	//		return (int)direction < 3 ? (direction + 3) : (direction - 3);
	//	}
	//}
}