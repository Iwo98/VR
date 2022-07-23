using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardHandler : MonoBehaviour
{
    public GameObject usernameText;
    private int maxUsernameLength = 20;
    private string username = "";
    // Start is called before the first frame update
    void Start()
    {
        username = PlayerPrefs.GetString("username");
        if(username == "default")
        {
            username = "";
        }
        changeUsernameText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Buttons
    public void btnAlphanumericClick(string btnVal)
    {
        username = usernameText.GetComponent<TextMeshProUGUI>().text.ToLower();
        if (username.Length < maxUsernameLength)
        {
            username = username + btnVal;
        }
        changeUsernameText();
    }

    public void btnBackspaceClick()
    {
        username = usernameText.GetComponent<TextMeshProUGUI>().text.ToLower();
        if (username.Length > 0)
        {
            username = username.Substring(0, username.Length - 1);
        }
        changeUsernameText();
    }

    public void btnClearClick()
    {
        username = "";
        changeUsernameText();
    }

    private void changeUsernameText()
    {
        usernameText.GetComponent<TextMeshProUGUI>().text = username.ToUpper();
    }
}
