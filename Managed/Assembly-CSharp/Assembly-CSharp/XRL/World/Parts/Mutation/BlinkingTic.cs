using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013CA RID: 5066
	[Serializable]
	public class BlinkingTic : BaseMutation
	{
		// Token: 0x0600C3BA RID: 50106 RVA: 0x00476625 File Offset: 0x00474825
		public BlinkingTic()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C3BB RID: 50107 RVA: 0x00476638 File Offset: 0x00474838
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3BC RID: 50108 RVA: 0x0047663B File Offset: 0x0047483B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C3BD RID: 50109 RVA: 0x00476651 File Offset: 0x00474851
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C3BE RID: 50110 RVA: 0x00476679 File Offset: 0x00474879
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C3BF RID: 50111 RVA: 0x0047668E File Offset: 0x0047488E
		public override string GetDescription()
		{
			return "You teleport about uncontrollably.\n\nSmall chance each round you're in combat that you randomly teleport to a nearby location.";
		}

		// Token: 0x0600C3C0 RID: 50112 RVA: 0x00476695 File Offset: 0x00474895
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3C1 RID: 50113 RVA: 0x0047669C File Offset: 0x0047489C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell == null || currentCell.ParentZone.IsWorldMap())
				{
					return true;
				}
				if (!base.CheckMyRealityDistortionUsability(null, null, null, null, null, null, 0))
				{
					return true;
				}
				if (1.in1000())
				{
					if (this.ParentObject.IsPlayer() && !this.ParentObject.AreHostilesNearby())
					{
						return true;
					}
					if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You lurch suddenly!", 'r', true);
					}
					this.ParentObject.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C3C2 RID: 50114 RVA: 0x00476753 File Offset: 0x00474953
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}
	}
}
