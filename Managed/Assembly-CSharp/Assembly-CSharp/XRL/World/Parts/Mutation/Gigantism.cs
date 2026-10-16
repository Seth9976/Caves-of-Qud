using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D1 RID: 5073
	[Serializable]
	public class Gigantism : BaseMutation
	{
		// Token: 0x0600C402 RID: 50178 RVA: 0x004774B2 File Offset: 0x004756B2
		public Gigantism()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600C403 RID: 50179 RVA: 0x004774C5 File Offset: 0x004756C5
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C404 RID: 50180 RVA: 0x004774C8 File Offset: 0x004756C8
		public override bool GeneratesEquipment()
		{
			return false;
		}

		// Token: 0x0600C405 RID: 50181 RVA: 0x004774CB File Offset: 0x004756CB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600C406 RID: 50182 RVA: 0x004774F1 File Offset: 0x004756F1
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Object.HasTagOrProperty("GigantismNoDisplayName") && E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("gigantic", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C407 RID: 50183 RVA: 0x0047752E File Offset: 0x0047572E
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!E.Object.HasTagOrProperty("GigantismNoShortDescription"))
			{
				E.Base.Compound(this.ParentObject.Itis + " unusually large.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C408 RID: 50184 RVA: 0x0047756A File Offset: 0x0047576A
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C409 RID: 50185 RVA: 0x0047758D File Offset: 0x0047578D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C40A RID: 50186 RVA: 0x00477597 File Offset: 0x00475797
		public override string GetDescription()
		{
			return "You are unusually large.";
		}

		// Token: 0x0600C40B RID: 50187 RVA: 0x0047759E File Offset: 0x0047579E
		public override string GetLevelText(int Level)
		{
			return "" + "Can only use most equipment if it is gigantic.\n" + "Cannot enter small spaces.\n" + "Heavier.\n" + "Can carry more.";
		}

		// Token: 0x0600C40C RID: 50188 RVA: 0x004775CD File Offset: 0x004757CD
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600C40D RID: 50189 RVA: 0x004775D6 File Offset: 0x004757D6
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C40E RID: 50190 RVA: 0x004775DF File Offset: 0x004757DF
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C40F RID: 50191 RVA: 0x004775E9 File Offset: 0x004757E9
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
