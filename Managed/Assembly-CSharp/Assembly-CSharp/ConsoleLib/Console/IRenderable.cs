using System;

namespace ConsoleLib.Console
{
	// Token: 0x02001534 RID: 5428
	public interface IRenderable
	{
		// Token: 0x0600D37A RID: 54138
		string getTile();

		// Token: 0x0600D37B RID: 54139
		string getRenderString();

		// Token: 0x0600D37C RID: 54140
		string getColorString();

		// Token: 0x0600D37D RID: 54141
		string getTileColor();

		// Token: 0x0600D37E RID: 54142
		char getDetailColor();

		// Token: 0x0600D37F RID: 54143
		ColorChars getColorChars();

		// Token: 0x0600D380 RID: 54144
		bool getHFlip();

		// Token: 0x0600D381 RID: 54145
		bool getVFlip();
	}
}
