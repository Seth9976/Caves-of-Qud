using System;
using System.Linq;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x020011A6 RID: 4518
	[Serializable]
	public class GivesTreat : IPart
	{
		// Token: 0x0600B39C RID: 45980 RVA: 0x0041860C File Offset: 0x0041680C
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B39D RID: 45981 RVA: 0x0041860F File Offset: 0x0041680F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B39E RID: 45982 RVA: 0x00418624 File Offset: 0x00416824
		public bool ShouldFetch(GameObject o)
		{
			return o.ShouldAutoget() && !o.IsOpenLiquidVolume() && o.GetCurrentCell() != null && !o.GetCurrentCell().IsSolid();
		}

		// Token: 0x0600B39F RID: 45983 RVA: 0x00418650 File Offset: 0x00416850
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeTakeAction" && this.ParentObject.CurrentCell != null && Stat.Random(1, this.Chance) <= 1)
			{
				if (!this.ParentObject.Brain.Goals.Items.Any<GoalHandler>((GoalHandler i) => i.GetType() == typeof(GiveATreatToPartyLeader)))
				{
					this.ParentObject.Brain.PushGoal(new GiveATreatToPartyLeader(this.TreatTable));
					this.ParentObject.UseEnergy(1000);
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400486F RID: 18543
		public int Chance = 1500;

		// Token: 0x04004870 RID: 18544
		public string TreatTable = "PetMushroomTreats";
	}
}
