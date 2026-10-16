using System;
using HistoryKit;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x020011A4 RID: 4516
	[Serializable]
	public class Fetches : IPart
	{
		// Token: 0x0600B392 RID: 45970 RVA: 0x00418166 File Offset: 0x00416366
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID;
		}

		// Token: 0x0600B393 RID: 45971 RVA: 0x0041817C File Offset: 0x0041637C
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (this.ParentObject.CurrentCell != null && 7.in100())
			{
				if (If.Chance(this.SniffMessageChance, null, null))
				{
					IComponent<GameObject>.AddPlayerMessage(GameText.VariableReplace(this.SniffMessage, this.ParentObject, null, false), null, true);
				}
				GameObject randomElement = this.ParentObject.CurrentZone.GetObjectsReadonly(new Predicate<GameObject>(this.ShouldFetch)).GetRandomElement<GameObject>(null);
				if (randomElement != null)
				{
					IComponent<GameObject>.AddPlayerMessage(GameText.VariableReplace(this.FetchMessage, this.ParentObject, null, false) + randomElement.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "!", null, true);
					this.ParentObject.Brain.PushGoal(new GoFetch(randomElement.GetCurrentCell()));
				}
				this.ParentObject.UseEnergy(1000);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B394 RID: 45972 RVA: 0x0041826E File Offset: 0x0041646E
		public bool ShouldFetch(GameObject o)
		{
			return o.ShouldAutoget() && !o.IsOpenLiquidVolume() && o.GetCurrentCell() != null && !o.GetCurrentCell().IsSolid();
		}

		// Token: 0x04004866 RID: 18534
		public int SniffChance = 7;

		// Token: 0x04004867 RID: 18535
		public int SniffMessageChance = 33;

		// Token: 0x04004868 RID: 18536
		public bool FetchLiquids;

		// Token: 0x04004869 RID: 18537
		public string SniffMessage = "=subject.The==subject.name= &y=verb:sniff= the air.";

		// Token: 0x0400486A RID: 18538
		public string FetchMessage = "=subject.The==subject.name= &y=verb:run= off to fetch ";
	}
}
