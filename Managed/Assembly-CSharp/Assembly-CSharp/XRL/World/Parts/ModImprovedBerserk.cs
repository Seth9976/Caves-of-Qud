using System;

namespace XRL.World.Parts
{
	// Token: 0x02001193 RID: 4499
	[Serializable]
	public class ModImprovedBerserk : IModification
	{
		// Token: 0x0600B2EB RID: 45803 RVA: 0x00415090 File Offset: 0x00413290
		public ModImprovedBerserk()
		{
		}

		// Token: 0x0600B2EC RID: 45804 RVA: 0x00415098 File Offset: 0x00413298
		public ModImprovedBerserk(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B2ED RID: 45805 RVA: 0x004150A1 File Offset: 0x004132A1
		public override void Configure()
		{
			this.WorksOnEquipper = true;
			this.NameForStatus = "ImprovedBerserk";
		}

		// Token: 0x0600B2EE RID: 45806 RVA: 0x004150B5 File Offset: 0x004132B5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == ImplantedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600B2EF RID: 45807 RVA: 0x004150EB File Offset: 0x004132EB
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Your blood frenzies last twice as long.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2F0 RID: 45808 RVA: 0x00415105 File Offset: 0x00413305
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Actor.ModIntProperty("ImprovedBerserk", 1, true);
				this.Applied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2F1 RID: 45809 RVA: 0x00415136 File Offset: 0x00413336
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this.Applied)
			{
				this.Applied = false;
				E.Actor.ModIntProperty("ImprovedBerserk", -1, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2F2 RID: 45810 RVA: 0x00415161 File Offset: 0x00413361
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Implantee.ModIntProperty("ImprovedBerserk", 1, true);
				this.Applied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2F3 RID: 45811 RVA: 0x00415192 File Offset: 0x00413392
		public override bool HandleEvent(UnimplantedEvent E)
		{
			if (this.Applied)
			{
				this.Applied = false;
				E.Actor.ModIntProperty("ImprovedBerserk", -1, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2F4 RID: 45812 RVA: 0x004151BD File Offset: 0x004133BD
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004840 RID: 18496
		public bool Applied;
	}
}
