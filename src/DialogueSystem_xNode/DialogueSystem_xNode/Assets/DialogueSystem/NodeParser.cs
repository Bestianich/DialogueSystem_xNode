using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeParser : MonoBehaviour
{
    private DialogueGraph _dialogueGraph;
    private Coroutine _coroutineParseNode;
    private TextMeshProUGUI _textField;
    private Image _imageRenderer;

    //Generic Parse Node
    public void StartParseNode(DialogueGraph dialogueGraph)
    {
        dialogueGraph.Init();
        _dialogueGraph = dialogueGraph;
        _coroutineParseNode = StartCoroutine(ParseNode());
    }
    //Parse Node to set text
    public void StartParseNode(DialogueGraph dialogueGraph , TextMeshProUGUI textField)
    {
        Debug.Log("Ciao");
        dialogueGraph.Init();
        _dialogueGraph = dialogueGraph;
        _textField = textField;
        _coroutineParseNode = StartCoroutine(ParseNode());
    }

    //Parse Node to set both text and portrait
    public void StartParseNode(DialogueGraph dialogueGraph, TextMeshProUGUI textField, Image imageRenderer)
    {
        dialogueGraph.Init();
        _dialogueGraph = dialogueGraph;
        _textField = textField;
        _imageRenderer = imageRenderer;
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
                _textField.text = dialogueLine;
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                NextNode("exit" , false);
                break;
            case NodeType.InterruptNode:
                yield return new WaitForEndOfFrame();
                NextNode("exit" , true);
                break;
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

        //If the dialogue interrupts then stop the parser
        if (interrupt)
        {
            _dialogueGraph.currentNode = null;
        }
        else // keep parsing
        {
            _coroutineParseNode = StartCoroutine(ParseNode());
        }

    }
    
}