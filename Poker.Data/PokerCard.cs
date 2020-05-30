using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Data
{
  public class PokerCard
  {
    public PokerCardSuit Suit { get; set; }
    public PokerCardValue Value { get; set; }
  }

  public enum PokerCardSuit
  {
    Spade = 1,
    Club = 2,
    Heart = 3,
    Diamond = 4
  }

  //Assumption : Aces HIGH!
  public enum PokerCardValue
  {
    Two = 2,
    Three = 3, 
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
  }
}
