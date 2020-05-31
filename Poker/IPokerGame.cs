using Poker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
  public interface IPokerGame
  {
    IEnumerable<PokerPlayer> FindPokerWinners(IEnumerable<PokerPlayer> players);
    IEnumerable<PokerPlayer> GetPlayersWithHighestCard(IEnumerable<PokerPlayer> players);
    PokerCard FindHighestCard(IEnumerable<PokerCard> cards);

    PokerHand SetPlayerHandWinningData(PokerHand hand);
    IEnumerable<PokerPlayer> GetWinnersFromWinningHandInfo(IEnumerable<PokerPlayer> players);
    IEnumerable<PokerPlayer> GetWinningPlayersBasedOnNextHighestCard(IEnumerable<PokerPlayer> players);
  }
}
