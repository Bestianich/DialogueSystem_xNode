
public class InterruptNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;

    public override NodeType GetNodeType()
    {
        return NodeType.InterruptNode;
    }
}
