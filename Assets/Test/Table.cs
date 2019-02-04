using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour {

    [HideInInspector]
    public List<Card> deck;
    public Player player1;
    public Player player2;
    public Sprite[] Clubs;
    public Sprite[] Diamonds;
    public Sprite[] Hearts;
    public Sprite[] Spades;
    public Sprite backFace;

    public GameState gameState;

    public void Awake()
    {
        Init();
    }

    //initializing card
    public void Init()
    {
        deck = new List<Card>();
        for (int i = 0; i < 52; i++)
        {
            Card card= new Card();
            deck.Add( card);
            card.rank =(CardRank) ((i % 13)+2);
            card.type = (CardType)((i / 13) );

            switch (card.type)
            {
                case CardType.Clubs:
                    card.faceImage = Clubs[(i % 13)];
                    break;
                case CardType.Diamonds:
                    card.faceImage = Diamonds[(i % 13)];
                    break;
                case CardType.Hearts:
                    card.faceImage = Hearts[(i % 13)];
                    break;
                case CardType.Spades:
                    card.faceImage = Spades[(i % 13)];
                    break;
                default:
                    break;
            }
        }
        
        Shuffle(deck);
        DealCard();
        gameState = GameState.Start;
        currentStartStayTime = 0;
        player1.Reset();
        player2.Reset();
    }

    public float startStayTime = 3;
    public float currentStartStayTime ;

    public float compareTime = 3;
    public float currentCompareTime ;
    private int compareCardNo;


    public void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                currentStartStayTime += Time.deltaTime;
                if(currentStartStayTime>= startStayTime)
                {
                    player1.Reset();
                    player2.Reset();
                    gameState = GameState.Player1Turn;
                    compareCardNo = 0;
                    player1.compareCardNo = compareCardNo;
                    player1.ChangeState( PlayerState.Turn);
                    currentStartStayTime = 0;
                    
                }
                break;
            case GameState.Player1Turn:
               
                if (player1.state == PlayerState.Idle)
                {
                    player2.compareCardNo = compareCardNo;
                    player2.ChangeState(PlayerState.Turn);
                    gameState = GameState.Player2Turn;
                }
                break;
            case GameState.Player2Turn:
                if (player2.state == PlayerState.Idle)
                {
                    
                    gameState = GameState.Compare;
                    currentCompareTime = 0;
                }
                break;
            case GameState.Compare:
                currentCompareTime += Time.deltaTime;
                if(currentCompareTime>= compareTime)
                {
                    gameState = GameState.Player1Turn;
                    player1.Reset();
                    player2.Reset();
                    player1.ChangeState(PlayerState.Turn);

                    if(player1.compareCard.rank == player2.compareCard.rank)
                    {
                        for (int i = 0; i <= compareCardNo; i++)
                        {
                            Card card = player1.cards[0];
                            player1.cards.RemoveAt(0);
                            deck.Add(card);
                            card = player2.cards[0];
                            player2.cards.RemoveAt(0);
                            deck.Add(card);

                        }
                        compareCardNo = (int)player1.compareCard.rank - 2;
                        if (compareCardNo > player1.cards.Count - 1)
                        {
                            player1.ChangeState(PlayerState.Lose);
                            player2.ChangeState(PlayerState.Win);
                            gameState = GameState.Complete;
                        }
                        else if (compareCardNo > player2.cards.Count - 1)
                        {
                            player1.ChangeState(PlayerState.Win);
                            player2.ChangeState(PlayerState.Lose);
                            gameState = GameState.Complete;
                        }


                    }
                    else if ((int)player1.compareCard.rank >= (int)player2.compareCard.rank)
                    {
                        for(int i = 0; i <= compareCardNo; i++)
                        {
                            Card card = player1.cards[0];
                            player1.cards.RemoveAt(0);
                            deck.Add(card);
                            card = player2.cards[0];
                            player2.cards.RemoveAt(0);
                            deck.Add(card);

                        }
                        for (int i = 0; i < deck.Count; i++) {
                            player1.cards.Add(deck[i]);
                        }
                        deck.Clear();
                        compareCardNo = 0;
                        if (player2.cards.Count == 0)
                        {
                            player1.ChangeState(PlayerState.Win);
                            player2.ChangeState(PlayerState.Lose);
                            gameState = GameState.Complete;
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= compareCardNo; i++)
                        {
                            Card card = player1.cards[0];
                            player1.cards.RemoveAt(0);
                            deck.Add(card);
                            card = player2.cards[0];
                            player2.cards.RemoveAt(0);
                            deck.Add(card);

                        }

                        for (int i = 0; i < deck.Count; i++)
                        {
                            player2.cards.Add(deck[i]);
                        }
                        deck.Clear();
                        compareCardNo = 0;
                        if (player1.cards.Count == 0)
                        {
                            player1.ChangeState(PlayerState.Lose);
                            player2.ChangeState(PlayerState.Win);
                            gameState = GameState.Complete;
                        }
                    }
                }
                

                break;
            case GameState.Complete:
                break;
            default:
                break;
        }
    }




    public void DealCard()
    {
        player1.cards = new List<Card>();
        player2.cards = new List<Card>();
        for (int i = 0; i < 52; i++)
        {
            if (i % 2 == 1)
            {
                player1.cards.Add(deck[i]);
            }
            else
            {
                player2.cards.Add(deck[i]);
            }

        }
        deck.Clear();
    }


    public void Shuffle(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card tmp = deck[i];
            int r = Random.Range(i, deck.Count);
            deck[i] = deck[r];
            deck[r] = tmp;
        }
    }

}


public enum GameState
{
    Start,
    Compare,
    Player1Turn,
    Player2Turn,
    Complete

}
