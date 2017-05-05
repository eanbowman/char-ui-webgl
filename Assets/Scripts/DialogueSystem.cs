using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {
    public static DialogueSystem Instance { get; set; }
    public List<string> dialogueLines = new List<string>();
    public string npcName;
    public GameObject dialoguePanel;

	Button nextButton, previousButton;
    Text dialogueText;
    int dialogueIndex;

    void Awake() {
		nextButton = dialoguePanel.transform.FindChild("Next").GetComponent<Button>();
		previousButton = dialoguePanel.transform.FindChild("Previous").GetComponent<Button>();
		dialogueText = dialoguePanel.transform.FindChild("Dialogue").GetComponent<Text>();
        
		nextButton.onClick.AddListener(delegate { nextDialogue(); });
		previousButton.onClick.AddListener(delegate { previousDialogue(); });

		previousButton.gameObject.SetActive (false);

		dialogueText.text = dialogueLines[0];

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
	}

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;
        CreateDialogue();

        Debug.Log(dialogueLines.Count);
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        dialoguePanel.SetActive(true);
	}

	public void nextDialogue()
	{
		if (dialogueIndex < dialogueLines.Count - 1)
		{
			dialogueIndex++;
			dialogueText.text = dialogueLines[dialogueIndex];
			if (dialogueIndex == dialogueLines.Count - 1) {
				Debug.Log ("Panel is the last one");
				nextButton.gameObject.SetActive (false);
			} else {
				nextButton.gameObject.SetActive (true);
				previousButton.gameObject.SetActive (true);
			}
		}
		else
		{
			dialoguePanel.SetActive(false);
		}
		Debug.Log (dialogueIndex + " " + dialogueLines.Count);
	}

	public void previousDialogue()
	{
		if (dialogueIndex > 0)
		{
			dialogueIndex--;
			dialogueText.text = dialogueLines[dialogueIndex];
			if (dialogueIndex == 0) {
				Debug.Log ("Panel is the first one");
				previousButton.gameObject.SetActive (false);
			} else {
				nextButton.gameObject.SetActive (true);
				previousButton.gameObject.SetActive (true);
			}
		}
		else
		{
			dialoguePanel.SetActive(false);
		}
		Debug.Log (dialogueIndex + " " + dialogueLines.Count);
	}
}
