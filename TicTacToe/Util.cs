using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
	public static class Util
	{
		public static long NumOfBits(long bits)
		{
			bits = (bits & 0x55555555) + (bits >> 1 & 0x55555555);
			bits = (bits & 0x33333333) + (bits >> 2 & 0x33333333);
			bits = (bits & 0x0f0f0f0f) + (bits >> 4 & 0x0f0f0f0f);
			bits = (bits & 0x00ff00ff) + (bits >> 8 & 0x00ff00ff);
			return (bits & 0x0000ffff) + (bits >> 16 & 0x0000ffff);
		}

		public static T RandomItem<T>(T[] arrays)
		{
			Random rnd = new Random();
			int index = rnd.Next(arrays.Length);
			return arrays[index];
		}

		public static Side Reverse(this Side side)
		{
			return side == Side.Black ? Side.White : Side.Black;
		}
	}
}
