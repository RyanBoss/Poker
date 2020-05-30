using Poker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
  public class PokerGame : IPokerGame
  {
    public IEnumerable<PokerPlayer> FindPokerWinners(IEnumerable<PokerPlayer> players)
    {
      var winners = new List<PokerPlayer>();

      //initialize to lowest possible value
      PokerCardValue currentHighCard = PokerCardValue.Two;
      foreach(var player in players)
      {
        var currentPlayerHigh = FindHighCard(player.Hand);
        if (currentPlayerHigh.Value > currentHighCard)
        {
          currentHighCard = currentPlayerHigh.Value;
        }
      }

      return players.Where(x => x.Hand.Select(y => y.Value).Contains(currentHighCard));
    }

    public PokerCard FindHighCard(IEnumerable<PokerCard> cards)
    {
      var maxCard = cards.Max(x => x.Value);
      return cards.Where(x => x.Value == maxCard).First();
    }
  }
}
