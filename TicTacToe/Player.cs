namespace TicTacToe
{
	public abstract class Player
	{
		public Side Side { get; set; }

		public Player(Side side)
		{
			Side = side;
		}

		public abstract ushort Move(Board board);
	}
}
