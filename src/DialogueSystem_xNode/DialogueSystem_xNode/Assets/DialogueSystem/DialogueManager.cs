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
            _nodeParser.StartParseNode(dialogueGraph);
        }
    }
}