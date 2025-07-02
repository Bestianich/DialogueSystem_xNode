using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using XNode;

public class NodeParser : MonoBehaviour
{
    [SerializeField] private DialogueGraph _dialogueGraph;
    private Coroutine _coroutineParseNode;
    
    public void StartParseNode(DialogueGraph dialogueGraph)
    {
        dialogueGraph.Init();
        _dialogueGraph = dialogueGraph;
        _coroutineParseNode = StartCoroutine(ParseNode());
    }

    private IEnumerator ParseNode()
    {
        BaseNode currentNode = _dialogueGraph.currentNode;
        switch (currentNode.GetNodeType())
        {
            case NodeType.StartNode: 
               yield return new WaitForEndOfFrame();
               NextNode("exit" , false);
               break;
            case NodeType.DialogueNode:
                var dialogueNode = currentNode as DialogueNode;
                string dialogueLine = dialogueNode.dialogueLine;
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                NextNode("exit" , false);
                break;
            case NodeType.InterruptNode:
                yield return new WaitForEndOfFrame();
                NextNode("exit" , true);
            default:
                yield return new WaitForEndOfFrame();
                break;
        }
    }

    private void NextNode(string portName , bool interrupt)
    {
        if (_coroutineParseNode != null)
        {
            StopCoroutine(_coroutineParseNode);
            _coroutineParseNode = null;
        }
        //Find the name of the output port and goes to the connected node
        foreach (NodePort nodePort in _dialogueGraph.currentNode.Ports)
        {
            if(nodePort.fieldName == portName)
            {
                if (nodePort.Connection.IsUnityNull())
                {
                    return;
                }
                _dialogueGraph.currentNode = nodePort.Connection.node as BaseNode;
                break;
            }
        }

        //If the dialogue interrupts stop t
        if (interrupt)
        {
            _dialogueGraph.currentNode = null;
        }
        else
        {
            _coroutineParseNode = StartCoroutine(ParseNode());
        }

    }
    
}