using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


public class BaseNode : Node {
    
    public virtual NodeType GetNodeType()
    {
        return NodeType.BaseNode;
    }
}

