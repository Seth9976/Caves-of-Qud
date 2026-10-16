using System;
using ConsoleLib.Console;

namespace XRL.UI.Framework
{
	// Token: 0x02001500 RID: 5376
	[Serializable]
	public class StartingLocationGridElement : IRenderable
	{
		// Token: 0x0600D1CA RID: 53706 RVA: 0x004DEC7C File Offset: 0x004DCE7C
		public ColorChars getColorChars()
		{
			return new ColorChars
			{
				foreground = this.Foreground[0],
				background = this.Background[0],
				detail = this.Detail[0]
			};
		}

		// Token: 0x0600D1CB RID: 53707 RVA: 0x004DECCB File Offset: 0x004DCECB
		public string getColorString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600D1CC RID: 53708 RVA: 0x004DECD2 File Offset: 0x004DCED2
		public char getDetailColor()
		{
			return this.Detail[0];
		}

		// Token: 0x0600D1CD RID: 53709 RVA: 0x004DECE0 File Offset: 0x004DCEE0
		public bool getHFlip()
		{
			return false;
		}

		// Token: 0x0600D1CE RID: 53710 RVA: 0x004DECE3 File Offset: 0x004DCEE3
		public string getRenderString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600D1CF RID: 53711 RVA: 0x004DECEA File Offset: 0x004DCEEA
		public string getTile()
		{
			return this.Tile;
		}

		// Token: 0x0600D1D0 RID: 53712 RVA: 0x004DECF2 File Offset: 0x004DCEF2
		public string getTileColor()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600D1D1 RID: 53713 RVA: 0x004DECF9 File Offset: 0x004DCEF9
		public bool getVFlip()
		{
			return false;
		}

		// Token: 0x040052F7 RID: 21239
		public string Tile;

		// Token: 0x040052F8 RID: 21240
		public string Foreground;

		// Token: 0x040052F9 RID: 21241
		public string Detail;

		// Token: 0x040052FA RID: 21242
		public string Background;
	}
}
