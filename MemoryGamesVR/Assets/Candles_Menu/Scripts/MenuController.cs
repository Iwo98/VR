using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    private ConstantGameValues game_values;
    public GameObject LoginCanvas;
    public GameObject doorButtonsCanvas;
    public GameObject loginCandleFire;
    public GameObject avatarImg;
    public GameObject usernameText;

    // Start is called before the first frame update
    void Start()
    {
        game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
        game_values.initAllValues();  // Inicjalizacja wartości w ConstantGameValues(potrzebna do awatarów, bez tego lista nie inicjalizuje się wystarczająco szybko)
        clearPrefs();
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        int result = user_data.LoadFile();
        if (result != 0)
        {
            user_data.SaveFile();
        }
        avatarImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Avatars/empty");
        avatarImg.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.0f, false);
        updateAvatarAndUsername();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Buttons
    public void EnterTraining()
    {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Candles_Menu/Scenes/ModulTreningowy"));
    }

    public void EnterGameChoice()
    {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Candles_Menu/Scenes/WyborGry"));
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void openLoginCanvas()
    {
        doorButtonsCanvas.SetActive(false);
        LoginCanvas.SetActive(true);
    }

    public void closeLoginCanvas()
    {
        doorButtonsCanvas.SetActive(true);
        LoginCanvas.SetActive(false);
    }

    //
    public void updateAvatarAndUsername()
    {
        string username = PlayerPrefs.GetString("username");
        if (username != "default" && username != "")
        {
            UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
            user_data.LoadFile();
            int avatar_id = user_data.data.avatar_id;
            avatarImg.SetActive(true);
            usernameText.SetActive(true);
            loginCandleFire.SetActive(true);
            usernameText.GetComponent<TextMeshProUGUI>().text = "Zalogowany gracz:\n" + username.ToUpper();
            StartCoroutine(fadeAvatarImg(avatar_id, false, 0.2f));
        }
        else
        {
            StartCoroutine(fadeAvatarImg(0, true, 0.2f));
            usernameText.SetActive(false);
            loginCandleFire.SetActive(false);
        }
    }

    IEnumerator fadeAvatarImg(int avatar_id, bool loggingOut, float waitTime)
    {
        avatarImg.GetComponent<Image>().CrossFadeAlpha(0.0f, waitTime, false);
        yield return new WaitForSeconds(waitTime);
        if(loggingOut)
        {
            avatarImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Avatars/empty");
        }
        else
        {
            avatarImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(game_values.avatarSpritesPaths[avatar_id]);
        }
        avatarImg.GetComponent<Image>().CrossFadeAlpha(1.0f, waitTime, false);
        yield return new WaitForSeconds(waitTime);
        if(loggingOut)
        {
            avatarImg.SetActive(false);
        }
    }


// Misc
private void clearPrefs()
    {
        string username = "default";
        if (!PlayerPrefs.HasKey("username") || PlayerPrefs.GetString("username") == "")
        {
            username = "default";
        }
        else
        {
            UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
            int result = user_data.LoadFile();
            if (result == 0)
            {
                username = PlayerPrefs.GetString("username");
            }
            else
            {
                PlayerPrefs.SetString("username", "default");
            }
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("curr_game_num", 0);
    }
}
