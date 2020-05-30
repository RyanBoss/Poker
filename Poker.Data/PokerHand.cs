using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Data
{
  public class PokerHand
  {
    public HandWinningType WinningType{ get; set; }
    public PokerCardValue? WinningValue { get; set; }
    public IEnumerable<PokerCard> Cards{ get; set; }
  }

  public enum HandWinningType
  {
    HighCard = 1,
    OnePair =2,
    ThreeOfAKind=3,
    Flush = 4
  }
}
