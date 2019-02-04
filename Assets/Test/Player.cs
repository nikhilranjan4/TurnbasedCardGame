using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public List<Card> cards;
    public bool isTurn;

    public PlayerState state;
    public System.Action<Card> OnTurnComplete;
    public int compareCardNo;
    public Card compareCard;
    public Image cardImage;
    public Text info;
    public Text cardCount;

    public void Update()
    {

        cardCount.text = cards.Count + "";
        switch (state)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Turn:
                if (Input.GetButtonDown("Fire1"))
                {
                    //Touch touch = Input.GetTouch(0);
                    //if (touch.phase == TouchPhase.Ended)
                    //{
                        //touch.position.
                        ChangeState(PlayerState.Idle);
                        compareCard = cards[compareCardNo];
                        cardImage.sprite = compareCard.faceImage;
                    //}
                }

                break;
            case PlayerState.Win:
                break;
            case PlayerState.Lose:
                break;
            default:
                break;
        }
    }

    public void ChangeState(PlayerState playerState)
    {
        state = playerState;
        switch (state)
        {
            case PlayerState.Idle:
                info.text = "";
                break;
            case PlayerState.Turn:
                info.text = "Tap";
                break;
            case PlayerState.Win:
                info.text = "Win";

                break;
            case PlayerState.Lose:
                info.text = "Lose";
                break;
            default:
                break;
        }
    }

    public void Reset()
    {
        cardImage.sprite=cards[0].backImage;
        info.text = "";
    }

}

public enum PlayerState
{
    Idle,
    Turn,
    Win,
    Lose
}
