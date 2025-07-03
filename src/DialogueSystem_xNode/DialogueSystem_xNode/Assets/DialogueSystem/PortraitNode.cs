
using Unity.Collections;
using UnityEngine;


public class PortraitNode : BaseNode
{
    [Input] public int entry;
    public Sprite portrait;
    [Output] public int exit;

    public override NodeType GetNodeType()
    {
        return NodeType.PortraitNode;
    }
}
