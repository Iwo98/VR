using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginCanvasController : MonoBehaviour
{
    private ConstantGameValues game_values;
    public GameObject loginCanvas, doorButtonsCanvas;
    public GameObject loginWindow, newAccountWindow, accountWindow;
    public GameObject usernameText, gameNumText;
    public GameObject avatarImg;
    // Start is called before the first frame update
    void Start()
    {
        game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
        string username = PlayerPrefs.GetString("username");
        if (username != "default" && username != "")
        {
            UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
            int load_result = user_data.LoadFile();
            if (load_result != 0) // Load failed
            {
                loginWindow.SetActive(true);
            }
            else
            {
                usernameText.GetComponent<TextMeshProUGUI>().text = username.ToUpper();
                int num_games = user_data.data.gameScores[0].currGameScores.Count;
                gameNumText.GetComponent<TextMeshProUGUI>().text = "Liczba rozegranych treningów: " + num_games.ToString();
                changeAvatarLoginCanvasOnly();
                accountWindow.SetActive(true);
            }
        }
        else
        {
            loginWindow.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Buttons
    public void loginButtonCLick()
    {
        string username = usernameText.GetComponent<TextMeshProUGUI>().text.ToLower();
        if (username != "default" && username != "")
        {
            PlayerPrefs.SetString("username", username);
            UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
            int load_result = user_data.LoadFile();
            if (load_result != 0) // Load failed
            {
                loginWindow.SetActive(false);
                newAccountWindow.SetActive(true);
            }
            else
            {
                int num_games = user_data.data.gameScores[0].currGameScores.Count;
                gameNumText.GetComponent<TextMeshProUGUI>().text = "Liczba rozegranych treningów: " + num_games.ToString();
                changeAvatarBtnClick(0);
                loginWindow.SetActive(false);
                accountWindow.SetActive(true);
                loginCanvas.SetActive(false);
                doorButtonsCanvas.SetActive(true);
            }
        }
    }

    public void logoutButtonCLick()
    {
        PlayerPrefs.SetString("username", "default");
        usernameText.GetComponent<TextMeshProUGUI>().text = "";
        MenuController menu_controller = GameObject.FindObjectsOfType<MenuController>()[0];
        menu_controller.updateAvatarAndUsername();
        accountWindow.SetActive(false);
        loginWindow.SetActive(true);
    }

    public void createYesButtonClick()
    {
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.SaveFile();
        int num_games = user_data.data.gameScores[0].currGameScores.Count;
        gameNumText.GetComponent<TextMeshProUGUI>().text = "Liczba rozegranych treningów: " + num_games.ToString();
        changeAvatarBtnClick(0);
        newAccountWindow.SetActive(false);
        accountWindow.SetActive(true);
    }

    public void createNoButtonClick()
    {
        PlayerPrefs.SetString("username", "default");
        usernameText.GetComponent<TextMeshProUGUI>().text = "";
        MenuController menu_controller = GameObject.FindObjectsOfType<MenuController>()[0];
        menu_controller.updateAvatarAndUsername();
        newAccountWindow.SetActive(false);
        loginWindow.SetActive(true);
    }

    public void changeAvatarBtnClick(int val)
    {
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();
        int avatar_id = user_data.data.avatar_id;
        avatar_id += val;
        if (avatar_id < 0)
        {
            avatar_id = game_values.avatarSpritesPaths.Count - 1;
        }
        else if (avatar_id >= game_values.avatarSpritesPaths.Count)
        {
            avatar_id = 0;
        }
        user_data.data.avatar_id = avatar_id;
        user_data.SaveFile();
        avatarImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(game_values.avatarSpritesPaths[avatar_id]);
        MenuController menu_controller = GameObject.FindObjectsOfType<MenuController>()[0];
        menu_controller.updateAvatarAndUsername();
    }

    public void changeAvatarLoginCanvasOnly()
    {
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();
        int avatar_id = user_data.data.avatar_id;
        user_data.data.avatar_id = avatar_id;
        user_data.SaveFile();
        avatarImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(game_values.avatarSpritesPaths[avatar_id]);
    }
}
