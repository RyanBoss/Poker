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
      foreach(var player in players)
      {
        player.Hand = SetPlayerHand(player.Hand);
      }

      var playersGroupedByWinning = players.OrderByDescending(x => x.Hand.WinningType).GroupBy(x => x.Hand.WinningType);

      //first group should always be the highest value since we order them first
      if (playersGroupedByWinning.First().Count() == 1)
      {
        return playersGroupedByWinning.First();
      } else
      {
        var winningPlayers = playersGroupedByWinning.First();
        //grab the first occurance of a winner and the first winner to determine the type for tiebreakers
        var winningHandType = winningPlayers.First().Hand.WinningType;

        //tie breakers
        switch (winningHandType)
        {
          case HandWinningType.Flush:
            return GetWinningPlayersBasedOnNextHighestCard(winningPlayers);
          case HandWinningType.ThreeOfAKind:
            return GetWinnersFromWinningHandValue(winningPlayers);
          case HandWinningType.OnePair:
              return GetWinnersFromWinningHandValue(winningPlayers);
          default: return GetWinningPlayersBasedOnNextHighestCard(winningPlayers);
        }
      }
    }
    public PokerHand SetPlayerHand(PokerHand hand)
    {
      var suitCounts = hand.Cards.GroupBy(x => x.Suit);

      if (suitCounts.Any(x => x.Count() == 5))
      {
        hand.WinningType = HandWinningType.Flush;
        hand.WinningValue = null;
        return hand;
      }

      var cardCounts = hand.Cards.GroupBy(x => x.Value);
      if (cardCounts.Any(x => x.Count() == 3))
      {
        //grab the first occurance of a three pair and determine the value used to find it
        hand.WinningType = HandWinningType.ThreeOfAKind;
        hand.WinningValue = cardCounts.First(x => x.Count() == 3).First().Value;
        return hand;
      }
      if (cardCounts.Any(x => x.Count() == 2))
      {
        //grab the first occurance of a pair and determine the value used to find it
        hand.WinningType = HandWinningType.OnePair;
        hand.WinningValue = cardCounts.First(x => x.Count() == 2).First().Value;
        return hand;
      }

      hand.WinningType = HandWinningType.HighCard;
      hand.WinningValue = null;
      return hand;
    }


    //Determine who has the highest winning hand value (three of a kind, 2 pair, etc)
    //if theres a tie, next highest card wins
    public IEnumerable<PokerPlayer> GetWinnersFromWinningHandValue(IEnumerable<PokerPlayer> players)
    {
      var winnersGroupByValue = players.OrderByDescending(x => x.Hand.WinningValue).GroupBy(x => x.Hand.WinningValue);

      var highestPairWinners = winnersGroupByValue.First();
      if (highestPairWinners.Count() == 1)
      {
        return highestPairWinners;
      }
      else
      {
        foreach (var player in highestPairWinners)
        {
          player.Hand.Cards = player.Hand.Cards.Where(x => x.Value != player.Hand.WinningValue).ToList();
        }

        //ASSUMPTION: Since we are only checking three of a kind, technically in this assingment three of a kind is highest.
        //Logic being used skips ALL where the winningvalue matches the cards value, but if a player had 4 of the same, it would techinically be three of a kind
        //Assumption here is that in a normal poker program this case would not occur at this level because the winning hand type would be set differently and be valued higher

        //fallback to highest card after the three of a kind
        return GetWinningPlayersBasedOnNextHighestCard(highestPairWinners);
      }
    }

    //Determine who has the winning hand based on the highest card in the hand
    //Recursively checks the next cards by skipping the highest one and continuing down until a player has a higher card, or both players have identical hands
    //Ideally at this point we'd make data clones of the players original cards to not remove from the original list, but im assuming that isnt a concern since we only care about that player that wins
    public IEnumerable<PokerPlayer> GetWinningPlayersBasedOnNextHighestCard(IEnumerable<PokerPlayer> players)
    {
      var playersWithHighCard = GetPlayersWithHighestCard(players).ToList();
      if (playersWithHighCard.Count() == 1)
      {
        return playersWithHighCard;
      } else
      {
        foreach (var player in playersWithHighCard)
        {
          var removedHand = player.Hand.Cards.OrderByDescending(x => x.Value).ToList();
          removedHand.RemoveAt(0);
          player.Hand.Cards = removedHand;
        }

        if (playersWithHighCard.Any(x => x.Hand.Cards.Count() == 0))
        {
          return playersWithHighCard;
        }
        return GetWinningPlayersBasedOnNextHighestCard(playersWithHighCard);
      }
    }

    public IEnumerable<PokerPlayer> GetPlayersWithHighestCard(IEnumerable<PokerPlayer> players)
    {
      //lowest value card as baseline
      PokerCardValue currentHighCard = PokerCardValue.Two;
      foreach (var player in players)
      {
        var currentPlayerHigh = FindHighestCard(player.Hand.Cards);
        if (currentPlayerHigh.Value > currentHighCard)
        {
          currentHighCard = currentPlayerHigh.Value;
        }
      }

      return players.Where(x => x.Hand.Cards.Select(y => y.Value).Contains(currentHighCard));
    }

    public PokerCard FindHighestCard(IEnumerable<PokerCard> cards)
    {
      var maxCard = cards.Max(x => x.Value);
      return cards.Where(x => x.Value == maxCard).First();
    }
  }
}
