using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueCanvas;
        [SerializeField] private TextMeshProUGUI _dialogueTextField;
        public static DialogueManager Instance;
        private NodeParser _nodeParser;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                _nodeParser = gameObject.AddComponent<NodeParser>();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void ShowCanvas()
        {
            _dialogueCanvas.SetActive(true);
        }

        public void HideCanvas()
        {
            _dialogueCanvas.SetActive(false);
        }
        
        public void StartDialogue(DialogueGraph dialogueGraph)
        {
            ShowCanvas();
            Debug.Log(dialogueGraph);
            Debug.Log(_nodeParser);
            DialogueGraph copy = dialogueGraph.Copy() as DialogueGraph;
            _nodeParser.StartParseNode(copy , _dialogueTextField);
        }
    }
}