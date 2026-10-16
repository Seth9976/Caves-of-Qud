using System;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000435 RID: 1077
	[Serializable]
	public class CyberneticsTerminalLineData : FrameworkDataElement
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x000D3CC7 File Offset: 0x000D1EC7
		public bool CursorDone
		{
			get
			{
				CyberneticsTerminalRow cyberneticsTerminalRow = this.row;
				return cyberneticsTerminalRow != null && cyberneticsTerminalRow.cursorDone;
			}
		}

		// Token: 0x0400148A RID: 5258
		public string Text;

		// Token: 0x0400148B RID: 5259
		public int OptionID;

		// Token: 0x0400148C RID: 5260
		public CyberneticsTerminalRow row;

		// Token: 0x0400148D RID: 5261
		public CyberneticsTerminalLineData nextCursorData;

		// Token: 0x0400148E RID: 5262
		public CyberneticsTerminalScreen screen;
	}
}
