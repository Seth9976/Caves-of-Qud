using System;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013CF RID: 5071
	[Serializable]
	public class ElectromagneticImpulse : BaseMutation
	{
		// Token: 0x0600C3EE RID: 50158 RVA: 0x00476D16 File Offset: 0x00474F16
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C3EF RID: 50159 RVA: 0x00476D2C File Offset: 0x00474F2C
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("circuitry", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C3F0 RID: 50160 RVA: 0x00476D54 File Offset: 0x00474F54
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C3F1 RID: 50161 RVA: 0x00476D69 File Offset: 0x00474F69
		public override string GetDescription()
		{
			return "You involuntarily release electromagnetic pulses, deactivating robots and artifacts around yourself.\n\nSmall chance each round you're in combat that you release an electromagnetic pulse with radius 3, deactivating robots and artifacts (including those you carry) for 11-20 rounds.";
		}

		// Token: 0x0600C3F2 RID: 50162 RVA: 0x00476D70 File Offset: 0x00474F70
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3F3 RID: 50163 RVA: 0x00476D77 File Offset: 0x00474F77
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3F4 RID: 50164 RVA: 0x00476D7C File Offset: 0x00474F7C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && !this.ParentObject.OnWorldMap() && 2.in1000())
			{
				if (this.ParentObject.IsPlayer() && !this.ParentObject.AreHostilesNearby())
				{
					return true;
				}
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("{{r|You surge with energy!}}", null, true);
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					ElectromagneticPulse.EMP(currentCell, (int)Math.Ceiling(2.5), Stat.Roll("1d10", null) + 10, true, this.ParentObject.GetPhase());
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C3F5 RID: 50165 RVA: 0x00476E2D File Offset: 0x0047502D
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}
	}
}
