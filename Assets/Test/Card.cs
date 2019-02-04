﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card  {
    public CardType type;
    public CardRank rank;
    public Sprite faceImage;
    public Sprite backImage;
    
}

public enum CardType
{
    Clubs=0,
    Diamonds=1,
    Hearts=2,
    Spades=3 
}

public enum CardRank
{
    Ace=14,
    Two=2,
    Three=3,
    Four=4,
    Five=5,
    Six=6,
    Seven=7,
    Eight=8,
    Nine=9,
    Ten=10,
    Jack=11,
    Queen12,
    King=13,
    
}

