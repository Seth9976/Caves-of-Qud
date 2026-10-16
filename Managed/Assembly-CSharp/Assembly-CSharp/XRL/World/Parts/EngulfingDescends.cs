using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000EAD RID: 3757
	[Serializable]
	public class EngulfingDescends : IPart
	{
		// Token: 0x060096D5 RID: 38613 RVA: 0x0036B7D4 File Offset: 0x003699D4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060096D6 RID: 38614 RVA: 0x0036B7D7 File Offset: 0x003699D7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurnEngulfing");
			base.Register(Object, Registrar);
		}

		// Token: 0x060096D7 RID: 38615 RVA: 0x0036B7EC File Offset: 0x003699EC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurnEngulfing")
			{
				if (this.TurnsLeft < 0)
				{
					this.TurnsLeft = Stat.Roll(this.Cooldown, null);
					return true;
				}
				if (this.TurnsLeft == 0)
				{
					Engulfing engulfing = this.ParentObject.GetPart<Engulfing>();
					if (this.ParentObject.CurrentCell != null && engulfing != null && engulfing.Engulfed != null && engulfing.Engulfed.CanBeInvoluntarilyMoved())
					{
						Cell cellFromDirection = this.ParentObject.CurrentCell.GetCellFromDirection("D", false);
						if (cellFromDirection != null && cellFromDirection.IsPassable(this.ParentObject, true) && cellFromDirection.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, false, false, null) == null)
						{
							if (this.ParentObject.IsPlayer())
							{
								Popup.Show("You melt through the floor and descend with your meal.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							else if (engulfing.Engulfed.IsPlayer())
							{
								Popup.Show(this.ParentObject.The + this.ParentObject.ShortDisplayName + "&y engulfing you melts through the floor! You fall to the level below.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							else
							{
								base.DidXToY("melt", "through the floor and" + this.ParentObject.GetVerb("descend", true, false, false, true, false) + " with", engulfing.Engulfed, null, null, null, null, null, engulfing.Engulfed, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							}
							cellFromDirection.ClearWalls();
							Cell currentCell = this.ParentObject.CurrentCell;
							this.ParentObject.DirectMoveTo(cellFromDirection, 0, false, false, false, null);
							currentCell.AddObject("OpenShaft", null, null, null, null);
							this.TurnsLeft = Stat.Roll(this.Cooldown, null);
							engulfing = this.ParentObject.GetPart<Engulfing>();
							if (engulfing != null && engulfing.Engulfed != null && engulfing.Engulfed.IsPlayer())
							{
								engulfing.Engulfed.CurrentCell.ParentZone.SetActive();
							}
						}
					}
				}
				else
				{
					this.TurnsLeft--;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003EA2 RID: 16034
		public int TurnsLeft = -1;

		// Token: 0x04003EA3 RID: 16035
		public string Cooldown = "10";
	}
}
