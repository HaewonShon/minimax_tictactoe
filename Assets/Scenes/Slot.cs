using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[Serializable]
public class Slot : MonoBehaviour
{
    public char status = ' ';
    private Image image;

    private Sprite defaultSprite;
    public Sprite Osprite;
    public Sprite Xsprite;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        status = ' ';
        image = GetComponent<Image>();
        defaultSprite = image.sprite;

        Assert.IsNotNull(Osprite);
        Assert.IsNotNull(Xsprite);
    }

    public void OnClick()
    {
        if (status != ' ') return;

        Debug.Log(name + ": OnClick");
        switch (gameManager.GetState())
        {
            case GameManager.GameState.AI_TURN:
                status = 'X';
                image.sprite = Xsprite;
                break;
            case GameManager.GameState.PLAYER_TURN:
                status = 'O';
                image.sprite = Osprite;
                break;
        }
        gameManager.OnClick();
    }

    public void OnAIPick()
    {
        if (status != ' ') return;

        Debug.Log(name + ": OnClick");
        switch (gameManager.GetState())
        {
            case GameManager.GameState.AI_TURN:
                status = 'X';
                image.sprite = Xsprite;
                break;
        }
    }

    public void Clear()
    {
        image.sprite = defaultSprite;
        status = ' ';
    }

    public char GetStatus()
    {
        return status;
    }

    public bool IsEmpty()
    {
        return status == ' ';
    }
}
