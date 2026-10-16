using System;
using ConsoleLib.Console;
using Genkit;

namespace XRL
{
	/// This class is not used in the base game.
	// Token: 0x02000515 RID: 1301
	[Serializable]
	public class SifrahRenderable : Renderable
	{
		// Token: 0x060037D8 RID: 14296 RVA: 0x001113C7 File Offset: 0x0010F5C7
		public virtual string GetAltColorString(string ColorString)
		{
			return ColorString;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x001113CA File Offset: 0x0010F5CA
		public virtual string GetAltTileColor(string ColorString)
		{
			return ColorString;
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x001113CD File Offset: 0x0010F5CD
		public virtual char GetAltDetailColor(char DetailColor)
		{
			return DetailColor;
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x001113D0 File Offset: 0x0010F5D0
		public virtual string GetHighlightColorString()
		{
			if (this.ColorString.IsNullOrEmpty())
			{
				return this.ColorString + "^Y";
			}
			return "^Y";
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x001113F8 File Offset: 0x0010F5F8
		public virtual string GetHighlightTileColor()
		{
			string text = this.TileColor;
			if (text != null)
			{
				text += "^Y";
			}
			return text;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x0011141C File Offset: 0x0010F61C
		public virtual char GetHighlightDetailColor()
		{
			return this.DetailColor;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x00111424 File Offset: 0x0010F624
		public void WriteTo(ScreenBuffer Buffer)
		{
			Buffer.Write(this);
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x00111430 File Offset: 0x0010F630
		public void WriteTo(ScreenBuffer Buffer, string ColorString, char DetailColor)
		{
			Buffer.Write(this, null, null, this.GetAltColorString(ColorString), this.GetAltTileColor(ColorString), new char?(this.GetAltDetailColor(DetailColor)), true, false, false);
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x00111464 File Offset: 0x0010F664
		public void WriteTo(ScreenBuffer Buffer, string ColorString)
		{
			Buffer.Write(this, null, null, this.GetAltColorString(ColorString), this.GetAltTileColor(ColorString), null, true, false, false);
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x00111494 File Offset: 0x0010F694
		public void WriteHighlightedTo(ScreenBuffer Buffer)
		{
			Buffer.Write(this, null, null, this.GetHighlightColorString(), this.GetHighlightTileColor(), new char?(this.GetHighlightDetailColor()), true, false, false);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x001114C4 File Offset: 0x0010F6C4
		public Renderable GetAlternate(string ColorString, char DetailColor)
		{
			return new Renderable(this, null, null, this.GetAltColorString(ColorString), this.GetAltTileColor(ColorString), new char?(this.GetAltDetailColor(DetailColor)));
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x001114E8 File Offset: 0x0010F6E8
		public Renderable GetAlternate(string ColorString)
		{
			return new Renderable(this, null, null, this.GetAltColorString(ColorString), this.GetAltTileColor(ColorString), null);
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x00111514 File Offset: 0x0010F714
		public Location2D GetLocation(int DX = 0, int DY = 0)
		{
			return Location2D.Get(this.X + DX, this.Y + DY);
		}

		// Token: 0x04001C03 RID: 7171
		public int X;

		// Token: 0x04001C04 RID: 7172
		public int Y;
	}
}
