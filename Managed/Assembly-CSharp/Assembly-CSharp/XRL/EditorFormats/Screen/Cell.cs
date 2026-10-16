using System;
using ConsoleLib.Console;
using UnityEngine;

namespace XRL.EditorFormats.Screen
{
	// Token: 0x020005C3 RID: 1475
	[Serializable]
	public class Cell
	{
		// Token: 0x06003ED9 RID: 16089 RVA: 0x00134F03 File Offset: 0x00133103
		public Cell()
		{
			this.Char = ' ';
			this.Foreground = 'K';
			this.Background = 'k';
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x00134F24 File Offset: 0x00133124
		public static Color GetColor(char c)
		{
			Color grey = Color.grey;
			if (ConsoleLib.Console.ColorUtility.ColorMap.TryGetValue(c, out grey))
			{
				return grey;
			}
			return Color.grey;
		}

		// Token: 0x04001F0D RID: 7949
		public static global::System.Random r = new global::System.Random();

		// Token: 0x04001F0E RID: 7950
		public char Char;

		// Token: 0x04001F0F RID: 7951
		public char Foreground;

		// Token: 0x04001F10 RID: 7952
		public char Background;
	}
}
