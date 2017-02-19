using System.Linq;

namespace TicTacToe
{
	public class RandomPlayer : Player
	{
		public RandomPlayer(Side side) : base(side)
		{
		}

		public override ushort Move(Board board)
		{
			var b = board.LegalHands()
				.Select(x => new { Board = (board.Clone() as Board).Move(x), Hand = x })
				.FirstOrDefault(y => y.Board.IsGameEnd);

			if (b != null)
			{
				return b.Hand;
			}

			return Util.RandomItem(board.LegalHands().ToArray());
		}
	}
}
