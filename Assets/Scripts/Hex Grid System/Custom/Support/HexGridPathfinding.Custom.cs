using System.Collections.Generic;

namespace ChiliPepper.Custom
{
	public interface IHexGridPathfinding<T>
		where T : IHexCell
	{
		/// <summary>
		/// 経路探索
		/// </summary>
		/// <param name="locations">探索する座標データ群</param>
		/// <param name="start">開始地点</param>
		/// <param name="end">終了地点</param>
		/// <returns></returns>
		public List<T> FindPath(in Dictionary<int, T> locations, T start, T end);
	}

	namespace ASter
	{
		internal class ANode<T>
			where T : IHexCell
		{
			/// <summary>
			/// 親ノード
			/// </summary>
			public ANode<T> Parent;

			/// <summary>
			/// 値
			/// </summary>
			public T Value;

			/// <summary>
			/// 実コスト
			/// </summary>
			public int Cost;

			/// <summary>
			/// 推定コスト
			/// </summary>
			public int Heuristic;

			/// <summary>
			/// スコア
			/// </summary>
			public int Score => (Cost + Heuristic);

			public ANode(ANode<T> parent, T value)
			{
				Parent = parent;
				Value = value;
				Cost = 0;
				Heuristic = 0;
			}
		}

		public class Pathfinding<T> 
			: IHexGridPathfinding<T>
			where T : IHexCell
		{
			public List<T> FindPath(
				in Dictionary<int, T> gridCells,
				T start,
				T end)
			{
				// どちらかがマップ情報に格納されていなかった場合、即座に処理を抜ける
				if (!gridCells.ContainsKey(start.HashCode) || !gridCells.ContainsKey(end.HashCode))
				{
					return null;
				}

				ANode<T> node_start = new ANode<T>(null, gridCells[start.HashCode]);
				ANode<T> node_end = new ANode<T>(null, gridCells[end.HashCode]);
				ANode<T> node_current = null;
				Dictionary<int, ANode<T>> open_list = new Dictionary<int, ANode<T>>();
				Dictionary<int, ANode<T>> close_list = new Dictionary<int, ANode<T>>();
				List<ANode<T>> children = new List<ANode<T>>();

				open_list.Add(node_start.Value.HashCode, node_start);

				while (open_list.Count > 0)
				{
					//----------------------------------------
					// 基準ノードの選定
					//----------------------------------------
					// 現在ノードを初期化
					node_current = null;

					// 現在のノードを取得
					foreach (var item in open_list)
					{
						// スコアが一番低いノードを探す
						if (node_current is null || node_current.Score > item.Value.Score)
						{
							node_current = open_list[item.Key];
						}

					}

					// 見つけたノードをクローズリストへ移動
					{
						var curren_hash = node_current.Value.HashCode;
						open_list.Remove(curren_hash);
						close_list.Add(curren_hash, node_current);
					}

					//----------------------------------------
					// 終了チェック
					//----------------------------------------
					if (node_current.Value.Coordinates == node_end.Value.Coordinates)
					{
						return GetPathToRoot(node_current);
					}

					//----------------------------------------
					// 隣接ノードの選定
					//----------------------------------------
					children.Clear();

					foreach (var neighbor in node_current.Value.Coordinates.GetNeighbors())
					{
						var hash = neighbor.GetHashCode();

						// ハッシュが一致するか調べる
						if (!gridCells.ContainsKey(hash))
						{
							continue;
						}
						if (!gridCells[hash].IsValidStep(node_current.Value))
						{
							continue;	// 隣接条件を満たせていない
						}

						var target = gridCells[hash];

						// childrenに追加
						children.Add(new ANode<T>(node_current, target));
					}

					//----------------------------------------
					// 隣接ノードを初期化してopen_listに登録
					//----------------------------------------
					foreach (var child in children)
					{
						// ハッシュ値を算出
						var hash = child.Value.HashCode;

						// 既にリストに入っているものは上書きしない
						if (open_list.ContainsKey(hash) || close_list.ContainsKey(hash))
						{
							continue;
						}

						child.Cost = node_current.Cost + 1;
						child.Heuristic = HexCoordinates.Distance(child.Value.Coordinates, node_end.Value.Coordinates);
						open_list.Add(hash, child);
					}
				}

				return null;
			}

			private List<T> GetPathToRoot(in ANode<T> node)
			{
				List<T> result = new List<T>();
				var current = node;

				while (current.Parent != null)
				{
					result.Insert(0, current.Value);
					current = current.Parent;
				}

				return result;
			}
		}
	}
}
