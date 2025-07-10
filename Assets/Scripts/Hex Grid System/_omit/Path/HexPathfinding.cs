using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IHexAStar<T>
{
	public List<HexCoordinates> FindPath(in Dictionary<int, T> locations, T start, T end);
}

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
		Parent		= parent;
		Value		= value;
		Cost		= 0;
		Heuristic	= 0;
	}
}

public class HexAStar2D : IHexAStar<HexCoordinates>
{
	public List<HexCoordinates> FindPath(in Dictionary<int, HexCoordinates> locations, HexCoordinates start, HexCoordinates end)
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

			foreach (var neighbor in node_current.Value.GetNeighbor())
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

public class HexAStar3D : IHexAStar<HexLocation>
{
	public List<HexCoordinates> FindPath(in Dictionary<int, HexLocation> locations, HexLocation start, HexLocation end)
	{
		// どちらかがマップ情報に格納されていなかった場合、即座に処理を抜ける
		if (!locations.ContainsKey(start.GetHashCode()) || !locations.ContainsKey(end.GetHashCode()))
		{
			return null;
		}

		ANode<HexLocation> node_start = new ANode<HexLocation>(null, locations[start.GetHashCode()]);
		ANode<HexLocation> node_end = new ANode<HexLocation>(null, locations[end.GetHashCode()]);
		ANode<HexLocation> node_current = null;
		Dictionary<int, ANode<HexLocation>> open_list = new Dictionary<int, ANode<HexLocation>>();
		Dictionary<int, ANode<HexLocation>> close_list = new Dictionary<int, ANode<HexLocation>>();
		List<ANode<HexLocation>> children = new List<ANode<HexLocation>>();

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

			foreach (var neighbor in node_current.Value.GetNeighbor())
			{
				var hash = neighbor.GetHashCode();

				// ハッシュが一致するか調べる
				if (!locations.ContainsKey(hash))
				{
					continue;
				}

				var target = locations[hash];

				// 条件を満たしているかをチェック
				if (Mathf.Abs(target.Height - node_current.Value.Height) >= 2)
				{
					continue;   // 高低差が2以上
				}

				// childrenに追加
				children.Add(new ANode<HexLocation>(node_current, target));
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
				child.Heuristic = HexCoordinates.Distance(child.Value.Coordinates, node_end.Value.Coordinates);
				// (int)Mathf.Pow(HexCoordinates.Distance(child.Value.Coordinates, node_end.Value.Coordinates), 2);
				open_list.Add(hash, child);
			}
		}

		return null;
	}

	private List<HexCoordinates> GetPathToRoot(in ANode<HexLocation> node)
	{
		List<HexCoordinates> result = new List<HexCoordinates>();
		var current = node;

		while (current.Parent != null)
		{
			result.Insert(0, current.Value.Coordinates);
			current = current.Parent;
		}

		return result;
	}
}