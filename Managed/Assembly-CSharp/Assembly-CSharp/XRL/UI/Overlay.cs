using System;

namespace XRL.UI
{
	// Token: 0x020014C1 RID: 5313
	internal static class Overlay
	{
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600D06E RID: 53358 RVA: 0x004CFB1C File Offset: 0x004CDD1C
		public static Vector2i CurrentCell
		{
			get
			{
				if (The.Player != null)
				{
					return new Vector2i(The.Player.Physics.CurrentCell.X, The.Player.Physics.CurrentCell.Y);
				}
				return new Vector2i(39, 12);
			}
		}

		// Token: 0x040051E4 RID: 20964
		public static string CurrentScreen = Options.StageViewID;
	}
}
