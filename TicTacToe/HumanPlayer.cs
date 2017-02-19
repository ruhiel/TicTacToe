using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
	public class HumanPlayer : Player
	{
		public HumanPlayer(Side side) : base(side)
		{
		}

		public override ushort Move(Board board)
		{
			while(true)
			{
				Console.WriteLine("置く場所を入力してください。");
				var key = Console.ReadLine();
				int result;
				if(!int.TryParse(key, out result))
				{
					Console.WriteLine("数字を入力してください。");
					continue;
				}

				ushort hand = (ushort)(1 << (9 - result));

				if(!board.LegalHands().Contains(hand))
				{
					Console.WriteLine("そこに置くことはできません。");
					continue;
				}

				return hand;
			}
		}
	}
}
