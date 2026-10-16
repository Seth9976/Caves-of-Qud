using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Core;
using XRL.World;

namespace XRL.UI.ObjectFinderContexts
{
	// Token: 0x020014E5 RID: 5349
	public class AutogotItems : ObjectFinder.Context
	{
		// Token: 0x0600D11D RID: 53533 RVA: 0x004DA79E File Offset: 0x004D899E
		public AutogotItems()
		{
			GameManager.Instance.gameQueue.queueSingletonTask("AutogotItemsInit", delegate
			{
				this.UpdateItems(The.Core);
			}, false);
		}

		// Token: 0x0600D11E RID: 53534 RVA: 0x004DA7C8 File Offset: 0x004D89C8
		public override string GetDisplayName()
		{
			return "Nearby Items";
		}

		// Token: 0x0600D11F RID: 53535 RVA: 0x004DA7CF File Offset: 0x004D89CF
		public override void Enable()
		{
			XRLCore.RegisterOnBeginPlayerTurnCallback(new Action<XRLCore>(this.UpdateItems));
			XRLCore.RegisterOnEndPlayerTurnCallback(new Action<XRLCore>(this.UpdateItems), true);
		}

		// Token: 0x0600D120 RID: 53536 RVA: 0x004DA7F4 File Offset: 0x004D89F4
		public override void Disable()
		{
			XRLCore.RemoveOnBeginPlayerTurnCallback(new Action<XRLCore>(this.UpdateItems));
			XRLCore.RemoveOnEndPlayerTurnCallback(new Action<XRLCore>(this.UpdateItems), true);
		}

		// Token: 0x0600D121 RID: 53537 RVA: 0x004DA819 File Offset: 0x004D8A19
		public void UpdateItems(XRLCore core)
		{
			ObjectFinder finder = this.finder;
			if (finder == null)
			{
				return;
			}
			finder.UpdateContext(this, Sidebar.AutogotItems.Keys);
		}

		// Token: 0x0600D122 RID: 53538 RVA: 0x004DA836 File Offset: 0x004D8A36
		private IEnumerable<GameObject> GetObjectsFromCell(Cell c)
		{
			return c.Objects.Where<GameObject>((GameObject o) => o.IsVisible());
		}
	}
}
