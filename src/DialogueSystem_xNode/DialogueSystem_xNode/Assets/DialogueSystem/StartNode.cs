
public class StartNode : BaseNode
{
    [Output] public int exit;
    public override NodeType GetNodeType()
    {
        return NodeType.StartNode;
    }
}
