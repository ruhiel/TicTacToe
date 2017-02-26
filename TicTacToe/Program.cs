using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
	class Program
	{
		static void Main(string[] args)
		{
			var b = new Board(new MonteCarloPlayer(Side.Black), new HumanPlayer(Side.White));

			b.PlayOut();
		}
	}
}
