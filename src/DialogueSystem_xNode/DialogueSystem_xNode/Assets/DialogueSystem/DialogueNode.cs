using UnityEngine;

public class DialogueNode : BaseNode
{
    [Input] public int entry;
    [TextArea (5 , 5)]public string dialogueLine;
    [Output] public int output;

    public override NodeType GetNodeType()
    {
        return NodeType.DialogueNode;
    }
}
