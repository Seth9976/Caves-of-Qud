using System;

namespace ConsoleLib.Console
{
	// Token: 0x0200152D RID: 5421
	public struct ColorChars : IEquatable<ColorChars>
	{
		// Token: 0x0600D2D9 RID: 53977 RVA: 0x004E3C21 File Offset: 0x004E1E21
		public bool Equals(ColorChars Other)
		{
			return this.foreground == Other.foreground && this.background == Other.background && this.detail == Other.detail;
		}

		// Token: 0x0600D2DA RID: 53978 RVA: 0x004E3C50 File Offset: 0x004E1E50
		public override bool Equals(object Object)
		{
			if (Object is ColorChars)
			{
				ColorChars colorChars = (ColorChars)Object;
				return this.Equals(colorChars);
			}
			return false;
		}

		// Token: 0x0600D2DB RID: 53979 RVA: 0x004E3C75 File Offset: 0x004E1E75
		public override int GetHashCode()
		{
			return HashCode.Combine<char, char, char>(this.foreground, this.background, this.detail);
		}

		// Token: 0x040053AE RID: 21422
		public static readonly char FOREGROUND_INDICATOR = '&';

		// Token: 0x040053AF RID: 21423
		public static readonly char BACKGROUND_INDICATOR = '^';

		// Token: 0x040053B0 RID: 21424
		public char foreground;

		// Token: 0x040053B1 RID: 21425
		public char background;

		// Token: 0x040053B2 RID: 21426
		public char detail;
	}
}
