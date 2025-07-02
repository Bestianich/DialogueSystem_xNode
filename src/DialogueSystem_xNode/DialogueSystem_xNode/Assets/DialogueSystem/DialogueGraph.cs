using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu (fileName = "New Dialogue Graph", menuName = "Dialogue System/DialogueGraph")]
public class DialogueGraph : NodeGraph
{
	public BaseNode currentNode;
	public bool _isInitalized = false;

	public DialogueGraph Init()
	{
		if (!_isInitalized)
		{
			_isInitalized = true;
			foreach (BaseNode node in nodes)
			{
				if (node.GetNodeType().Equals(NodeType.StartNode))
				{
					this.currentNode = node;
					break;
				}
			}
		}

		return this;
	}
}