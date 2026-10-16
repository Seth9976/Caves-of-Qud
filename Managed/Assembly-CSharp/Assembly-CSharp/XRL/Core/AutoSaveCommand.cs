using System;
using XRL.World;

namespace XRL.Core
{
	// Token: 0x02000631 RID: 1585
	public class AutoSaveCommand : ISystemSaveCommand, IActionCommand, IComposite
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600422D RID: 16941 RVA: 0x00157A56 File Offset: 0x00155C56
		public string Type
		{
			get
			{
				return "AutoSave";
			}
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x00157A60 File Offset: 0x00155C60
		public static void Issue()
		{
			ActionManager actionManager = The.ActionManager;
			if (!actionManager.HasActionDescendedFrom<ISystemSaveCommand>())
			{
				actionManager.EnqueueAction(AutoSaveCommand.Instance, 0);
			}
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x00157A87 File Offset: 0x00155C87
		public void Execute(XRLGame Game, ActionManager Manager)
		{
			Game.SaveGame("Primary", "Saving game", false, false);
		}

		// Token: 0x0400208D RID: 8333
		private static AutoSaveCommand Instance = new AutoSaveCommand();
	}
}
