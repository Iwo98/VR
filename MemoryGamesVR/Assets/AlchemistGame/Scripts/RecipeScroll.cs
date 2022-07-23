using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeScroll : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> potImgs;
    [SerializeField]
    private List<GameObject> checkImgs;
    [SerializeField]
    private List<GameObject> plusImgs;

    public List<Sprite> potionSpritesCol1;
    public List<Sprite> potionSpritesCol2;
    public List<Sprite> potionSpritesItem;
    public Sprite correctSprite, wrongSprite, arrowSprite, plusSprite;

    private List<int> recipe;
    private List<int> recipeCorrect;
    private int recipeId = 0;
    private int recipeLen = 0;
    private int maxRecipeLen = 5;


    void Start()
    {
        recipe = new List<int>();
        recipeCorrect = new List<int>();
        for (int i = 0; i < maxRecipeLen; i++)
        {
            recipe.Add(0);
            recipeCorrect.Add(0);
        }

        transform.Find("Canvas").GetComponent<Canvas>().enabled = true;
        Image[] images = transform.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            image.CrossFadeAlpha(0.0f, 0.0f, false);
        }

        redrawPotions();
    }

    void Update()
    {

    }

    public void disableCanvas()
    {
        transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
    }

    public void crossFadeCanvas(float time)
    {
        Image[] images = transform.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            image.CrossFadeAlpha(1.0f, 0.0f, false);
            image.CrossFadeAlpha(0.0f, time, false);
        }
    }

    public void updateRecipeValues(List<int> new_recipe, List<int> new_recipeCorrect, int new_recipeId, int new_recipeLen)
    {
        transform.Find("Canvas").GetComponent<Canvas>().enabled = true;
        Image[] images = transform.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            image.CrossFadeAlpha(1.0f, 0.25f, false);
        }
        recipe = new_recipe;
        recipeCorrect = new_recipeCorrect;
        recipeId = new_recipeId;
        recipeLen = new_recipeLen;

        redrawPotions();
    }
    private void redrawPotions()
    {
        for (int i = 0; i < maxRecipeLen; i++)
        {
            if (i < recipeLen)
            {
                potImgs[i].GetComponent<Image>().enabled = true;
                if(recipe[i] < 10)
                    potImgs[i].GetComponent<Image>().sprite = potionSpritesCol1[recipe[i] % 10];
                else if (recipe[i] < 20)
                    potImgs[i].GetComponent<Image>().sprite = potionSpritesCol2[recipe[i] % 10];
                else
                    potImgs[i].GetComponent<Image>().sprite = potionSpritesItem[recipe[i] % 10];

                if (i == recipeId)
                {
                    checkImgs[i].GetComponent<Image>().enabled = true;
                    checkImgs[i].GetComponent<Image>().sprite = arrowSprite;
                }
                else if (recipeCorrect[i] == 1)
                {
                    checkImgs[i].GetComponent<Image>().enabled = true;
                    checkImgs[i].GetComponent<Image>().sprite = correctSprite;
                }
                else if (recipeCorrect[i] == -1)
                {
                    checkImgs[i].GetComponent<Image>().enabled = true;
                    checkImgs[i].GetComponent<Image>().sprite = wrongSprite;
                }
                else
                {
                    checkImgs[i].GetComponent<Image>().enabled = false;
                }

                if (i + 1 < recipeLen && i < 4)
                {
                    plusImgs[i].GetComponent<Image>().enabled = true;
                }
                else if (i < 4)
                {
                    plusImgs[i].GetComponent<Image>().enabled = false;
                }
            }
            else
            {
                potImgs[i].GetComponent<Image>().enabled = false;
                checkImgs[i].GetComponent<Image>().enabled = false;
                if (i < 4)
                {
                    plusImgs[i].GetComponent<Image>().enabled = false;
                }
            }
        }
    }
}
