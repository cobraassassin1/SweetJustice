using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConvoManager : MonoBehaviour {

    public TestConvoText text;
    public int currentPerspective = 0;
    public GameObject backContainer;
    public GameObject dialogueBox;
    public GameObject response;
    public GameObject arrowPrefab;

    [System.Serializable]
    private class Characters
    {
        public GameObject bartenderPerp0;
        public GameObject bartenderPerp1;
    }
    [SerializeField]
    private Characters images;
    private List<Character> characterArray;
    private List<Dialogue> dialogueArray;
    private List<GameObject> responses = new List<GameObject>();
    private List<Response> toRemove = new List<Response>();

    internal TheBartender bartender;
    internal Character currentCharacter = new Character();

    private Dialogue currentDialogue;
    private int currentSentence = 0;
    private GameObject receiverImage;
    private GameObject arrow;
    private int selectedResponse = 0;
    private bool isDone = false;
    private List<int> alreadyRead = new List<int>();


    private void Awake()
    {
        bartender = GameObject.FindGameObjectWithTag("GameController").GetComponent<TheBartender>();

    }

    void Start () {
        string dataAsJSON = File.ReadAllText("Assets/JSON/Characters.json");
        characterArray = JSONHelper.FromJson<Character>(dataAsJSON);
        int personID = bartender.convoPersonID;
        dataAsJSON = File.ReadAllText("Assets/JSON/Dialogue" + personID + ".json");
        dialogueArray = JSONHelper.FromJson<Dialogue>(dataAsJSON);
        currentCharacter = characterArray[personID];
        text.ChangeText(currentCharacter.Name);
        LoadConvoPic();
        RunDialogue();
	}

    private void Update()
    {
        if (currentSentence + 1 <= currentDialogue.sentences.Count)
        {
            if (isDone && Input.anyKeyDown)
            {
                LoadNextSentence();
            }
        }
        if (responses.Count > 0 && isDone)
        {
            if (responses.Count > 1)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    int var1 = selectedResponse;
                    if (var1 + 1 == responses.Count)
                    {
                        var1 = 0;
                    }
                    else
                    {
                        var1++;
                    }
                    RequestArrow(var1);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    int var1 = selectedResponse;
                    if (var1 == 0)
                    {
                        var1 = responses.Count - 1;
                    }
                    else
                    {
                        var1--;
                    }
                    RequestArrow(var1);
                }
            }else if(currentDialogue.responses[0].response == "")
            {
                if (Input.anyKeyDown)
                {
                    RequestNextConvo();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                RequestNextConvo();
            }
        }
        
    }

    private void LoadNextSentence()
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentDialogue.sentences[currentSentence]));
        currentSentence++;
        isDone = false;
    }

    private void ChangePerspective()
    {
        if(currentPerspective == 1)
        {
            currentPerspective = 0;
        }
        else
        {
            currentPerspective = 1;
        }
        Destroy(receiverImage);
        LoadConvoPic();
    }

    private void LoadConvoPic()
    {
        if (currentCharacter.ID == 0)
        {
            switch (currentPerspective)
            {
                case 0:
                    receiverImage = Instantiate(images.bartenderPerp0, images.bartenderPerp0.transform.position, Quaternion.identity) as GameObject;
                    break;
                case 1:
                    receiverImage = Instantiate(images.bartenderPerp1, images.bartenderPerp1.transform.position, Quaternion.identity) as GameObject;
                    break;
                default:
                    Debug.LogError("Perspective does not exist");
                    break;
            }
        }
        else
        {
            Debug.LogError("Invalid Character");
        }
    }

    private void RunDialogue()
    {
        currentDialogue = dialogueArray[bartender.startDialogueID];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentDialogue.sentences[currentSentence]));
        currentSentence++;
    }

     IEnumerator TypeSentence(string sentence)
    {
        dialogueBox.transform.GetChild(0).GetComponent<Text>().text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueBox.transform.GetChild(0).GetComponent<Text>().text += letter;
            yield return null;
        }
        if(currentSentence == currentDialogue.sentences.Count)
        {
            float var1 = 0;
            foreach (Response resp in currentDialogue.responses)
            {
                if (CheckUnlock(resp))
                {
                    GameObject var2 = Instantiate(response, new Vector2(response.transform.position.x, response.transform.position.y + var1), Quaternion.identity) as GameObject;
                    var2.transform.SetParent(dialogueBox.transform);
                    var2.GetComponent<TestResponse>().setTestResponse(this, responses.Count);
                    var2.GetComponent<Text>().text = "";
                    foreach (char letter in resp.response)
                    {
                        var2.GetComponent<Text>().text += letter;
                        yield return null;
                    }
                    responses.Add(var2);

                    var1 -= 0.4f;
                }
            }
            RemoveExcessResponses();
            if (currentDialogue.responses[0].response != "")
            {
                arrow = Instantiate(arrowPrefab, new Vector2(-7f, responses[0].transform.position.y), arrowPrefab.transform.rotation) as GameObject;
                arrow.transform.SetParent(dialogueBox.transform);
            }
            
        }
        isDone = true;
    }

    internal void SetPerspective(int par1)
    {
        currentPerspective = par1;
        LoadConvoPic();
    }

	public void BackToLocation()
    {
        int locationID = bartender.locationID;
        LevelManager level = GetComponent<LevelManager>();
        if(locationID == 0)
        {
            level.LoadLevel("2DBar");
        }
        else
        {
            Debug.LogError("No location correctly selected");
        }
    }

    internal void RequestArrow(int par1)
    {
        if (isDone)
        {
            GameObject var1 = responses[par1];
            selectedResponse = par1;
            arrow.transform.position = new Vector2(-7f, var1.transform.position.y);
        }
    }

    internal void RequestNextConvo()
    {
        int var1 = -1;
        if (currentDialogue.responses[selectedResponse].isRand)
        {
            var1 = selectRandDialogue();
        }
        else
        {
            var1 = currentDialogue.responses[selectedResponse].toDialogue[0];
        }
        if (var1 == -1)
        {
            BackToLocation();
        }
        else
        {
            currentDialogue = dialogueArray[var1];
            currentSentence = 0;
            alreadyRead.Add(currentDialogue.ID);
            ClearResponses();
            ChangePerspective();
            LoadNextSentence();
        }

    }

    private void RemoveExcessResponses()
    {
        foreach (Response resp in toRemove)
        {
            currentDialogue.responses.Remove(resp);
        }
    }

    private int selectRandDialogue()
    {
        double var0 = currentDialogue.responses[selectedResponse].toDialogue.Length;
        double var1 = 100f / var0;
        double var2 = 100f - var1;
        double var3 = Tools.GenerateRand();
        for (int i = 0; i < var0; i++)
        {
            if(var3 > var2)
            {
                return currentDialogue.responses[selectedResponse].toDialogue[i];
            }
            else
            {
                var2 -= var1;
            }
        }
        return -1;
    }

    private bool CheckUnlock(Response par1)
    {
        bool var1 = true;
        foreach (int var2 in par1.unlockID)
        {
            switch (var2)
            {
                case -1:
                    if (alreadyRead.Contains(par1.toDialogue[0]))
                    {
                        var1 = FailUnlock(par1);
                    }
                        break;
            }
        }
        
        return var1;
    }

    private bool FailUnlock(Response par1)
    {
        toRemove.Add(par1);
        return false;
    }
    

    private void ClearResponses()
    {
        int var1 = responses.Count;
        for (int i = 0; i < var1; i++)
        {
            GameObject game = responses[0];
            responses.RemoveAt(0);
            Destroy(game);
        }
        Destroy(arrow);
    }
}
