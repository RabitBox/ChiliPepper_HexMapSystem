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
	/// �e�m�[�h
	/// </summary>
	public ANode<T> Parent;

	/// <summary>
	/// �l
	/// </summary>
	public T Value;

	/// <summary>
	/// ���R�X�g
	/// </summary>
	public int Cost;
	
	/// <summary>
	/// ����R�X�g
	/// </summary>
	public int Heuristic;
	
	/// <summary>
	/// �X�R�A
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
		// �ǂ��炩���}�b�v���Ɋi�[����Ă��Ȃ������ꍇ�A�����ɏ����𔲂���
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
			// ��m�[�h�̑I��
			//----------------------------------------
			// ���݃m�[�h��������
			node_current = null;

			// ���݂̃m�[�h���擾
			foreach (var item in open_list)
			{
				// �X�R�A����ԒႢ�m�[�h��T��
				if (node_current is null || node_current.Score > item.Value.Score)
				{
					node_current = open_list[item.Key];
				}

			}

			// �������m�[�h���N���[�Y���X�g�ֈړ�
			{
				var curren_hash = node_current.Value.GetHashCode();
				open_list.Remove(curren_hash);
				close_list.Add(curren_hash, node_current);
			}

			//----------------------------------------
			// �I���`�F�b�N
			//----------------------------------------
			if (node_current.Value == node_end.Value)
			{
				return GetPathToRoot(node_current);
			}

			//----------------------------------------
			// �אڃm�[�h�̑I��
			//----------------------------------------
			children.Clear();

			foreach (var neighbor in node_current.Value.GetNeighbor())
			{
				var hash = neighbor.GetHashCode();

				// �n�b�V������v���邩���ׂ�
				if (!locations.ContainsKey(hash))
				{
					continue;
				}

				var target = locations[hash];

				// children�ɒǉ�
				children.Add(new ANode<HexCoordinates>(node_current, target));
			}

			//----------------------------------------
			// �אڃm�[�h������������open_list�ɓo�^
			//----------------------------------------
			foreach (var child in children)
			{
				// �n�b�V���l���Z�o
				var hash = child.Value.GetHashCode();

				// ���Ƀ��X�g�ɓ����Ă�����̂͏㏑�����Ȃ�
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
		// �ǂ��炩���}�b�v���Ɋi�[����Ă��Ȃ������ꍇ�A�����ɏ����𔲂���
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
			// ��m�[�h�̑I��
			//----------------------------------------
			// ���݃m�[�h��������
			node_current = null;

			// ���݂̃m�[�h���擾
			foreach (var item in open_list)
			{
				// �X�R�A����ԒႢ�m�[�h��T��
				if (node_current is null || node_current.Score > item.Value.Score)
				{
					node_current = open_list[item.Key];
				}
				
			}

			// �������m�[�h���N���[�Y���X�g�ֈړ�
			{
				var curren_hash = node_current.Value.GetHashCode();
				open_list.Remove(curren_hash);
				close_list.Add(curren_hash, node_current);
			}

			//----------------------------------------
			// �I���`�F�b�N
			//----------------------------------------
			if (node_current.Value == node_end.Value)
			{
				return GetPathToRoot(node_current);
			}

			//----------------------------------------
			// �אڃm�[�h�̑I��
			//----------------------------------------
			children.Clear();

			foreach (var neighbor in node_current.Value.GetNeighbor())
			{
				var hash = neighbor.GetHashCode();

				// �n�b�V������v���邩���ׂ�
				if (!locations.ContainsKey(hash))
				{
					continue;
				}

				var target = locations[hash];

				// �����𖞂����Ă��邩���`�F�b�N
				if (Mathf.Abs(target.Height - node_current.Value.Height) >= 2)
				{
					continue;   // ���፷��2�ȏ�
				}

				// children�ɒǉ�
				children.Add(new ANode<HexLocation>(node_current, target));
			}

			//----------------------------------------
			// �אڃm�[�h������������open_list�ɓo�^
			//----------------------------------------
			foreach (var child in children)
			{
				// �n�b�V���l���Z�o
				var hash = child.Value.GetHashCode();

				// ���Ƀ��X�g�ɓ����Ă�����̂͏㏑�����Ȃ�
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