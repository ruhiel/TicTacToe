using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
	public class Board : ICloneable
	{
		/// <summary>
		/// 現在局面
		/// </summary>
		public ushort Stone => (ushort)((Black | White) & Mask);

		public Side Side = Side.Black;

		public Player BlackPlayer { get; set; }

		public Player WhitePlayer { get; set; }

		private Player NowPlayer => Side == Side.Black ? BlackPlayer : WhitePlayer;

		public ushort Empty => (ushort)(~Stone & Mask);

		public Board(Player black, Player white)
		{
			BlackPlayer = black;
			WhitePlayer = white;
		}

		public State PlayOutFast()
		{
			while (!IsGameEnd)
			{
				var hand = NowPlayer.Move(this);

				if (Side == Side.Black)
				{
					Black |= hand;
				}
				else
				{
					White |= hand;
				}

				Side = Side.Reverse();
			}

			return State();
		}

		public State PlayOut()
		{
			Console.WriteLine(ToString());
			while (!IsGameEnd)
			{
				var hand = NowPlayer.Move(this);

				if(Side == Side.Black)
				{
					Black |= hand;
				}
				else
				{
					White |= hand;
				}
				Console.WriteLine(ToString());

				Side = Side.Reverse();
			}

			return State();
		}

		/// <summary>
		/// 先手
		/// </summary>
		public ushort Black { get; set; }

		/// <summary>
		/// 後手
		/// </summary>
		public ushort White { get; set; }

		private static ushort Mask = 0x01FF;

		private static ushort[] Masks = new ushort[] {0x100,0x080,0x040,0x020,0x010,0x008,0x004,0x002,0x001};

		private static ushort[] WinPattern = new ushort[] {0x1C0, 0x038, 0x007, 0x124, 0x092, 0x049, 0x111, 0x054};

		public bool IsWin(Side side) => WinPattern.Any(x => (x & (side == Side.Black ? Black: White)) == x);

		public bool IsBlackWin => IsWin(Side.Black);

		public bool IsWhiteWin => IsWin(Side.White);

		public bool IsFull => Stone == Mask;

		public bool IsGameEnd => IsBlackWin || IsWhiteWin || IsFull;

		public State State()
		{
			if(IsBlackWin)
			{
				return TicTacToe.State.BlackWin;
			}
			else if(IsWhiteWin)
			{
				return TicTacToe.State.WhiteWin;
			}
			else if(IsFull)
			{
				return TicTacToe.State.Draw;
			}

			return TicTacToe.State.Normal;

		}

		public Board Move(ushort hand)
		{
			if (Side == Side.Black)
			{
				Black |= hand;
			}
			else
			{
				White |= hand;
			}

			return this;
		}

		public IEnumerable<ushort> LegalHands()
		{
			ushort bit = 1;
			for(int i = 0; i < 9; i++)
			{
				if((Empty & bit) == bit)
				{
					yield return bit;
				}
				bit <<= 1;
			}
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			Action<int> func = (skip) =>
			{
				sb.Append("│");
				int i = skip + 1;
				foreach (var mask in Masks.Skip(skip).Take(3))
				{
					if((Black & mask) == mask)
					{
						sb.Append("○");
					}
					else if ((White & mask) == mask)
					{
						sb.Append("×");
					}
					else
					{
						string s3 = Microsoft.VisualBasic.Strings.StrConv(i.ToString(), Microsoft.VisualBasic.VbStrConv.Wide, 0x411);
						sb.Append(s3);
					}
					i++;
					sb.Append("│");
				}
				sb.AppendLine("");
			};

			sb.AppendLine("┌─┬─┬─┐");
			func(0);
			sb.AppendLine("├─┼─┼─┤");
			func(3);
			sb.AppendLine("├─┼─┼─┤");
			func(6);
			sb.AppendLine("└─┴─┴─┘");
			return sb.ToString();
		}

		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
