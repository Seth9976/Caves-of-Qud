using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x0200118A RID: 4490
	[Serializable]
	public class ModLegendary : IModification
	{
		// Token: 0x0600B283 RID: 45699 RVA: 0x004143ED File Offset: 0x004125ED
		public ModLegendary()
		{
		}

		// Token: 0x0600B284 RID: 45700 RVA: 0x004143FC File Offset: 0x004125FC
		public ModLegendary(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B285 RID: 45701 RVA: 0x0041440C File Offset: 0x0041260C
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B286 RID: 45702 RVA: 0x00414415 File Offset: 0x00412615
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<ModMasterwork>();
		}

		// Token: 0x0600B287 RID: 45703 RVA: 0x0041441D File Offset: 0x0041261D
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B288 RID: 45704 RVA: 0x00414428 File Offset: 0x00412628
		public override bool SameAs(IPart p)
		{
			return (p as ModLegendary).Bonus == this.Bonus && base.SameAs(p);
		}

		// Token: 0x0600B289 RID: 45705 RVA: 0x00414446 File Offset: 0x00412646
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCriticalThresholdEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<TriggersMakersMarkCreationEvent>.ID;
		}

		// Token: 0x0600B28A RID: 45706 RVA: 0x0041447C File Offset: 0x0041267C
		public override bool HandleEvent(TriggersMakersMarkCreationEvent E)
		{
			return E.ModAdded != null && E.ModAdded != this && base.HandleEvent(E);
		}

		// Token: 0x0600B28B RID: 45707 RVA: 0x00414498 File Offset: 0x00412698
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{Y|lege{{W|n}}dary}}", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B28C RID: 45708 RVA: 0x004144C4 File Offset: 0x004126C4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Append("\n{{rules|Legendary").Append(": This weapon is ").Append(this.Bonus * 5)
				.Append("% ")
				.Append((this.Bonus >= 0) ? "more" : "less")
				.Append(" likely to score critical hits (standard chance is 5%).")
				.Append("}}");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B28D RID: 45709 RVA: 0x00414538 File Offset: 0x00412738
		public override bool HandleEvent(GetCriticalThresholdEvent E)
		{
			if (E.Weapon == this.ParentObject || E.Projectile == this.ParentObject)
			{
				E.Threshold -= this.Bonus;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B28E RID: 45710 RVA: 0x00414570 File Offset: 0x00412770
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B28F RID: 45711 RVA: 0x00414593 File Offset: 0x00412793
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004835 RID: 18485
		public int Bonus = 4;
	}
}
