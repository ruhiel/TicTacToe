using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
	public class MonteCarloPlayer : Player
	{
		public MonteCarloPlayer(Side side) : base(side)
		{
		}

		public override ushort Move(Board board)
		{
			var dic = new Dictionary<ushort, int>();
			foreach(var hand in board.LegalHands())
			{
				int win = 0;
				for(int i = 0; i < 100; i++)
				{
					var b = board.Clone() as Board;
					b.BlackPlayer = new RandomPlayer(Side.Black);
					b.WhitePlayer = new RandomPlayer(Side.White);

					b = b.Move(hand);
					if (b.PlayOutFast() == (this.Side == Side.Black ? State.BlackWin : State.WhiteWin))
					{
						win++;
					}
				}

				dic[hand] = win;
			}

			var result = dic.Select(x => new { x.Key, x.Value });

			var max = result.Max(x => x.Value);

			return result.First(x => x.Value == max).Key;
		}
	}
}
