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
using System.Collections.Generic;

namespace RV.SpiceKit.ChiliPepper
{
	/// <summary>
	/// 経路探索アルゴリズム
	/// </summary>
	public interface IHexGridPathfinding
	{
		/// <summary>
		/// 経路探索
		/// </summary>
		/// <param name="locations">探索する座標データ群</param>
		/// <param name="start">開始地点</param>
		/// <param name="end">終了地点</param>
		/// <returns></returns>
		public List<HexCoordinates> FindPath(in Dictionary<int, HexCoordinates> locations, HexCoordinates start, HexCoordinates end);
	}

	namespace ASter
	{
		public class ANode<T>
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

		public class Pathfinding : IHexGridPathfinding
		{
			List<HexCoordinates> IHexGridPathfinding.FindPath(
				in Dictionary<int, HexCoordinates> locations,
				HexCoordinates start,
				HexCoordinates end)
			{
				// どちらかがマップ情報に格納されていなかった場合、即座に処理を抜ける
				if (!locations.ContainsKey(start.GetHashCode()) || !locations.ContainsKey(end.GetHashCode()))
				{
					return null;
				}

				ANode<HexCoordinates> node_start = new ANode<HexCoordinates>(null, locations[start.GetHashCode()]);
				ANode<HexCoordinates> node_end = new ANode<HexCoordinates>(null, locations[end.GetHashCode()]);
				ANode<HexCoordinates> node_current = null;
				Dictionary<int, ANode<HexCoordinates>> open_list = new Dictionary<int, ANode<HexCoordinates>>();
				Dictionary<int, ANode<HexCoordinates>> close_list = new Dictionary<int, ANode<HexCoordinates>>();
				List<ANode<HexCoordinates>> children = new List<ANode<HexCoordinates>>();

				open_list.Add(node_start.Value.GetHashCode(), node_start);

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
						var curren_hash = node_current.Value.GetHashCode();
						open_list.Remove(curren_hash);
						close_list.Add(curren_hash, node_current);
					}

					//----------------------------------------
					// 終了チェック
					//----------------------------------------
					if (node_current.Value == node_end.Value)
					{
						return GetPathToRoot(node_current);
					}

					//----------------------------------------
					// 隣接ノードの選定
					//----------------------------------------
					children.Clear();

					foreach (var neighbor in node_current.Value.GetNeighbors())
					{
						var hash = neighbor.GetHashCode();

						// ハッシュが一致するか調べる
						if (!locations.ContainsKey(hash))
						{
							continue;
						}

						var target = locations[hash];

						// childrenに追加
						children.Add(new ANode<HexCoordinates>(node_current, target));
					}

					//----------------------------------------
					// 隣接ノードを初期化してopen_listに登録
					//----------------------------------------
					foreach (var child in children)
					{
						// ハッシュ値を算出
						var hash = child.Value.GetHashCode();

						// 既にリストに入っているものは上書きしない
						if (open_list.ContainsKey(hash) || close_list.ContainsKey(hash))
						{
							continue;
						}

						child.Cost = node_current.Cost + 1;
						child.Heuristic = HexCoordinates.Distance(child.Value, node_end.Value);
						open_list.Add(hash, child);
					}
				}

				return null;
			}

			private List<HexCoordinates> GetPathToRoot(in ANode<HexCoordinates> node)
			{
				List<HexCoordinates> result = new List<HexCoordinates>();
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

