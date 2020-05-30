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
  }
}
