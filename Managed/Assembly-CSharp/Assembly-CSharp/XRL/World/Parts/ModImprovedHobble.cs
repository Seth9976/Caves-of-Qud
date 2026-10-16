using System;

namespace XRL.World.Parts
{
	// Token: 0x02001196 RID: 4502
	[Serializable]
	public class ModImprovedHobble : IModification
	{
		// Token: 0x0600B309 RID: 45833 RVA: 0x00415420 File Offset: 0x00413620
		public ModImprovedHobble()
		{
		}

		// Token: 0x0600B30A RID: 45834 RVA: 0x00415428 File Offset: 0x00413628
		public ModImprovedHobble(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B30B RID: 45835 RVA: 0x00415431 File Offset: 0x00413631
		public override void Configure()
		{
			this.WorksOnEquipper = true;
			this.NameForStatus = "ImprovedHobble";
		}

		// Token: 0x0600B30C RID: 45836 RVA: 0x00415445 File Offset: 0x00413645
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == ImplantedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600B30D RID: 45837 RVA: 0x0041547B File Offset: 0x0041367B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Your Hobble cooldown is reduced by 5 rounds.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B30E RID: 45838 RVA: 0x00415495 File Offset: 0x00413695
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Actor.ModIntProperty("ImprovedHobble", 1, true);
				this.Applied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B30F RID: 45839 RVA: 0x004154C6 File Offset: 0x004136C6
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this.Applied)
			{
				this.Applied = false;
				E.Actor.ModIntProperty("ImprovedHobble", -1, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B310 RID: 45840 RVA: 0x004154F1 File Offset: 0x004136F1
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Implantee.ModIntProperty("ImprovedHobble", 1, true);
				this.Applied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B311 RID: 45841 RVA: 0x00415522 File Offset: 0x00413722
		public override bool HandleEvent(UnimplantedEvent E)
		{
			if (this.Applied)
			{
				this.Applied = false;
				E.Actor.ModIntProperty("ImprovedHobble", -1, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B312 RID: 45842 RVA: 0x0041554D File Offset: 0x0041374D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004843 RID: 18499
		public bool Applied;
	}
}
