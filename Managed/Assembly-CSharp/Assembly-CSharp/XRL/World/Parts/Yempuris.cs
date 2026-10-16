using System;
using System.Collections.Generic;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02000E96 RID: 3734
	[Serializable]
	public class Yempuris : IPart
	{
		// Token: 0x06009645 RID: 38469 RVA: 0x00367477 File Offset: 0x00365677
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectEnteredCell");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009646 RID: 38470 RVA: 0x00367498 File Offset: 0x00365698
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.ParentObject.Physics.IsFrozen())
				{
					return true;
				}
				if (Yempuris.LastSplitTurn == XRLCore.Core.Game.Turns)
				{
					return true;
				}
				if (this.ParentObject.Physics.CurrentCell != null)
				{
					int num = 0;
					List<Cell> localAdjacentCells = this.ParentObject.Physics.CurrentCell.GetLocalAdjacentCells();
					for (int i = 0; i < localAdjacentCells.Count; i++)
					{
						if (localAdjacentCells[i].HasObjectWithBlueprint("Yempuris"))
						{
							num++;
						}
					}
					if (num > 6)
					{
						this.ParentObject.Destroy(null, false, false, null);
						return true;
					}
					foreach (string text in this.D)
					{
						Cell localCellFromDirection = this.ParentObject.Physics.CurrentCell.GetLocalCellFromDirection(text, true);
						if (localCellFromDirection != null && !localCellFromDirection.HasObjectWithBlueprint("Yempuris"))
						{
							localCellFromDirection.AddObject("Yempuris", null, null, null, null);
							Yempuris.LastSplitTurn = XRLCore.Core.Game.Turns;
							return true;
						}
						if (50.in100())
						{
							return true;
						}
					}
				}
			}
			else if (E.ID == "ObjectEnteredCell")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (gameObjectParameter != null && gameObjectParameter.IsCombat && this.ParentObject.IsHostileTowards(gameObjectParameter) && gameObjectParameter.PhaseAndFlightMatches(this.ParentObject) && gameObjectParameter.TakeDamage(this.Damage.RollCached(), "from %t impalement.", "Thorns", null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null))
				{
					if (gameObjectParameter.Energy != null)
					{
						gameObjectParameter.Energy.BaseValue -= 500;
					}
					gameObjectParameter.Bloodsplatter();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003E39 RID: 15929
		public string ClusterSize = "1";

		// Token: 0x04003E3A RID: 15930
		public string Damage = "1d12";

		// Token: 0x04003E3B RID: 15931
		public string[] D = new string[] { "NW", "SE", "SW", "NE", "N", "S", "E", "W" };

		// Token: 0x04003E3C RID: 15932
		public static long LastSplitTurn;

		// Token: 0x04003E3D RID: 15933
		public static long Growth;
	}
}
