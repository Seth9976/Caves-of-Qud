using System;
using System.Text;
using XRL;

namespace ConsoleLib.Console
{
	// Token: 0x02001540 RID: 5440
	public class SolidColor : IMarkupShader
	{
		// Token: 0x0600D405 RID: 54277 RVA: 0x004EBF8B File Offset: 0x004EA18B
		public SolidColor(string Name, char? Foreground = 'y', char? Background = 'k', char? LightTone = null, char? DarkTone = null)
			: base(Name)
		{
			this.Foreground = Foreground;
			this.Background = Background;
			this.LightTone = LightTone;
			this.DarkTone = DarkTone;
		}

		// Token: 0x0600D406 RID: 54278 RVA: 0x004EBFB2 File Offset: 0x004EA1B2
		public override char? GetForegroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
		{
			return this.Foreground;
		}

		// Token: 0x0600D407 RID: 54279 RVA: 0x004EBFBA File Offset: 0x004EA1BA
		public override char? GetBackgroundColor(char ch, int localPos, int localLen, int totalPos, int totalLen)
		{
			return this.Background;
		}

		// Token: 0x0600D408 RID: 54280 RVA: 0x004EBFC4 File Offset: 0x004EA1C4
		public void AssembleCode(StringBuilder SB)
		{
			if (this.Foreground != null)
			{
				SB.Append('&').Append(this.Foreground);
			}
			if (this.Background != null)
			{
				SB.Append('^').Append(this.Background);
			}
		}

		// Token: 0x0600D409 RID: 54281 RVA: 0x004EC020 File Offset: 0x004EA220
		public string AssembleCode()
		{
			StringBuilder stringBuilder = The.StringBuilder;
			this.AssembleCode(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x04005509 RID: 21769
		public char? Foreground;

		// Token: 0x0400550A RID: 21770
		public char? Background;

		// Token: 0x0400550B RID: 21771
		public char? LightTone;

		// Token: 0x0400550C RID: 21772
		public char? DarkTone;
	}
}
