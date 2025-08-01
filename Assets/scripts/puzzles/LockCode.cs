using UnityEngine;
using UnityEngine.UI;
using TMPro; // se estiver usando TextMeshPro

public class LockCode : MonoBehaviour
{
        public DialogueGraph graph;
    [System.Serializable]
    public class Digit
    {
        public TMP_Text valueText; // ou use Text se for o Text antigo
        public Button upButton;
        public Button downButton;
        [HideInInspector] public int currentValue = 0;
    }

    public Digit[] digits = new Digit[4];
    public int[] correctCode = { 3, 1, 4, 2 }; // exemplo de senha correta

    void Start()
    {
        for (int i = 0; i < digits.Length; i++)
        {
            int index = i;

            digits[i].upButton.onClick.AddListener(() => ChangeValue(index, 1));
            digits[i].downButton.onClick.AddListener(() => ChangeValue(index, -1));
            UpdateDigitText(index);
        }
    }

    void ChangeValue(int index, int delta)
    {
        Digit d = digits[index];
        d.currentValue = (d.currentValue + delta + 10) % 10;
        UpdateDigitText(index);
        CheckCode();
    }

    void UpdateDigitText(int index)
    {
        digits[index].valueText.text = digits[index].currentValue.ToString();
    }

    void CheckCode()
    {
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i].currentValue != correctCode[i])
                return;
        }

        OnCorrectCodeEntered();
    }

    void OnCorrectCodeEntered()
    {
        Debug.Log("Senha correta!");
        // chame aqui o que quiser: abrir porta, tocar som, etc
        SceneMaracutaia sm = FindObjectOfType<SceneMaracutaia>(true);
        GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
        GameManager gameManager = GMobject.GetComponent<GameManager>();
        if (!gameManager.unlockedMaps.Contains("Cantina")) 
        {
            sm.LoadSceneNDialogue("Abertura 2", graph);
        }
        else
        {
        sm.LoadSceneNDialogue("Preparation1A",graph);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneMaracutaia sm = FindObjectOfType<SceneMaracutaia>(true);
            sm.LoadScene("Preparation1A");
            GameObject GMobject = GameObject.FindGameObjectWithTag("game manager");
            GameManager gameManager = GMobject.GetComponent<GameManager>();
            if (!gameManager.unlockedMaps.Contains("Cantina"))
            {
                sm.LoadScene("Abertura 2");
            }
            else
            {
                sm.LoadSceneNDialogue("Preparation1A", graph);
            }
        }
    }
}
