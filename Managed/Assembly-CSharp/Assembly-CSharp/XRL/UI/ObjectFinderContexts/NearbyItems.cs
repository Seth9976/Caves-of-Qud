using System;
using System.Collections.Generic;
using System.Linq;
using Qud.UI;
using XRL.Core;
using XRL.World;

namespace XRL.UI.ObjectFinderContexts
{
	// Token: 0x020014E4 RID: 5348
	public class NearbyItems : ObjectFinder.Context
	{
		// Token: 0x0600D116 RID: 53526 RVA: 0x004DA668 File Offset: 0x004D8868
		public NearbyItems()
		{
			GameManager.Instance.gameQueue.queueSingletonTask("NearbyItemsInit", delegate
			{
				this.UpdateItems(The.Core);
			}, false);
		}

		// Token: 0x0600D117 RID: 53527 RVA: 0x004DA692 File Offset: 0x004D8892
		public override string GetDisplayName()
		{
			return "Nearby Items";
		}

		// Token: 0x0600D118 RID: 53528 RVA: 0x004DA699 File Offset: 0x004D8899
		public override void Enable()
		{
			XRLCore.RegisterOnBeginPlayerTurnCallback(new Action<XRLCore>(this.UpdateItems));
			XRLCore.RegisterOnEndPlayerTurnCallback(new Action<XRLCore>(this.UpdateItems), true);
		}

		// Token: 0x0600D119 RID: 53529 RVA: 0x004DA6BE File Offset: 0x004D88BE
		public override void Disable()
		{
			XRLCore.RemoveOnBeginPlayerTurnCallback(new Action<XRLCore>(this.UpdateItems));
			XRLCore.RemoveOnEndPlayerTurnCallback(new Action<XRLCore>(this.UpdateItems), true);
		}

		// Token: 0x0600D11A RID: 53530 RVA: 0x004DA6E4 File Offset: 0x004D88E4
		public void UpdateItems(XRLCore core)
		{
			GameObject player = The.Player;
			Cell cell = ((player != null) ? player.CurrentCell : null);
			IEnumerable<GameObject> enumerable;
			if (Options.OverlayNearbyObjectsLocal)
			{
				enumerable = this.GetObjectsFromCell(cell);
			}
			else
			{
				IEnumerable<GameObject> enumerable2;
				if (cell == null)
				{
					enumerable2 = null;
				}
				else
				{
					List<Cell> localAdjacentCells = cell.GetLocalAdjacentCells();
					enumerable2 = ((localAdjacentCells != null) ? localAdjacentCells.Prepend(cell).SelectMany<Cell, GameObject>(new Func<Cell, IEnumerable<GameObject>>(this.GetObjectsFromCell)) : null);
				}
				enumerable = enumerable2;
			}
			ObjectFinder finder = this.finder;
			if (finder != null)
			{
				finder.UpdateContext(this, enumerable);
			}
			SingletonWindowBase<NearbyItemsWindow>.instance.UpdateGameContext();
		}

		// Token: 0x0600D11B RID: 53531 RVA: 0x004DA75E File Offset: 0x004D895E
		private IEnumerable<GameObject> GetObjectsFromCell(Cell c)
		{
			c.IsSolid();
			return c.Objects.Where<GameObject>((GameObject o) => o.ShouldShowInNearbyItemsList());
		}
	}
}
