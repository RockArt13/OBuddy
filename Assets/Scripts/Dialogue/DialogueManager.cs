using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject DialoguePanel;
    private Queue<string> sentences;

    #region Singleton
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("DialogueManager has more tha one instance");
            return;
        }

        instance = this;
    }
    #endregion
    private void Start()
    {
        sentences = new Queue<string>();
    }

    void CloseDialoguePanel()
    {
        DialoguePanel.SetActive(false);
    }

    void OpenDialoguePanel()
    {
        DialoguePanel.SetActive(true);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        OpenDialoguePanel();
        nameText.text = dialogue.characterName;
        sentences.Clear();

        foreach (string  sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        CloseDialoguePanel();
    }
    

}
