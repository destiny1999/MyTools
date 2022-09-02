using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemController : MonoBehaviour
{
    [SerializeField] Text showTextView;
    [SerializeField] TextAsset textFile;
    [SerializeField] List<CharacterImageInfo> characterImages;
    [SerializeField] List<RectTransform> characterImagePosition;
    [SerializeField][Tooltip("the key code name that click will show next sentence")] string nextSentenceKey;
    KeyCode nextSentenceKeyCode;
    Dictionary<string, CharacterImageInfo> useCharacterNameGetImage = new Dictionary<string, CharacterImageInfo>();
    // Start is called before the first frame update
    public static DialogueSystemController Instance;
    string[] eachDialogue;
    int currentSentence = -1;
    void Awake()
    {
        Instance = this;

        for(int i = 0; i<characterImages.Count; i++)
        {
            useCharacterNameGetImage.Add(characterImages[i].name, characterImages[i]);
        }
    }
    void Start()
    {
        nextSentenceKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), nextSentenceKey);

        string[] stringSeparators = new string[] { "\r\n" };
        eachDialogue = textFile.text.Split(stringSeparators, StringSplitOptions.None);
        GetNextDialogue();
    }
    public void GetNextDialogue()
    {
        currentSentence++;
        if(currentSentence >= eachDialogue.Length - 1)
        {
            Debug.Log("file sentence end");
            return;
        }
        CheckPreImageStatus();

        string[] dealSentence = eachDialogue[currentSentence].Split(' ');
        string sentence = dealSentence[0];
        int showIndex = int.Parse(dealSentence[1].Substring(0, 1));
        string imageStatus = dealSentence[2];
        showTextView.text = sentence;

        if(showIndex != 9)
        {
            string characterName = sentence.Split(':')[0];
            characterImagePosition[showIndex].GetComponent<Image>().sprite =
                useCharacterNameGetImage[characterName].sprite;
            Color newColor = characterImagePosition[showIndex].GetComponent<Image>().color;
            newColor.a = 1;
            characterImagePosition[showIndex].GetComponent<Image>().color = newColor;
            characterImagePosition[showIndex].gameObject.SetActive(true);
            characterImagePosition[showIndex].tag = imageStatus;
        }

    }
    /// <summary>
    /// when change sentence should deal with the pre character image status, 
    /// maybe leave or stay and change color alpha value
    /// </summary>
    void CheckPreImageStatus()
    {
        for(int i = 0; i <characterImagePosition.Count; i++)
        {
            Color newColor = characterImagePosition[i].GetComponent<Image>().color;
            switch (characterImagePosition[i].tag)
            {
                case "h":
                    newColor.a = 0;
                    break;
                case "s":
                    newColor.a = 0.3f;
                    break;
                        
            }
            characterImagePosition[i].GetComponent<Image>().color = newColor;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(nextSentenceKeyCode))
        {
            GetNextDialogue();
        }
    }
}

[Serializable]
public class CharacterImageInfo
{
    public string name;
    public Sprite sprite;
}
