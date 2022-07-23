using UnityEngine;
using System.Collections;

public class destroyEffect : MonoBehaviour
{
    float destroyTime = 0.0f;
    bool automaticDestroy = false;
    bool isPlayerSpell = false;

    void Update()
    {
        if (automaticDestroy)
        {
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0)
            {
                destroy();
                if (isPlayerSpell)
                {
                    MainGameMagicDuel[] main_game = Object.FindObjectsOfType<MainGameMagicDuel>();
                    main_game[0].destroySpells();
                }
            }
        }
    }

    public void setAutomaticDestroy(float time)
    {
        automaticDestroy = true;
        destroyTime = time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int col_layer = collision.gameObject.layer;

        if (col_layer == 10) // Destoy spells when tehy collide
        {
            if (isPlayerSpell)
            {
                MainGameMagicDuel[] main_game = Object.FindObjectsOfType<MainGameMagicDuel>();
                main_game[0].destroySpells();
            }
            destroy();
        }
    }



    public void setSpellType(bool isPlayer)
    {
        isPlayerSpell = isPlayer;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
