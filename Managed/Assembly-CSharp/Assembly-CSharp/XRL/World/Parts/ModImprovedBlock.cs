using System;

namespace XRL.World.Parts
{
	// Token: 0x02001194 RID: 4500
	[Serializable]
	public class ModImprovedBlock : IModification
	{
		// Token: 0x0600B2F5 RID: 45813 RVA: 0x004151C0 File Offset: 0x004133C0
		public ModImprovedBlock()
		{
		}

		// Token: 0x0600B2F6 RID: 45814 RVA: 0x004151C8 File Offset: 0x004133C8
		public ModImprovedBlock(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B2F7 RID: 45815 RVA: 0x004151D1 File Offset: 0x004133D1
		public override void Configure()
		{
			this.WorksOnEquipper = true;
			this.NameForStatus = "ImprovedBlock";
		}

		// Token: 0x0600B2F8 RID: 45816 RVA: 0x004151E5 File Offset: 0x004133E5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == ImplantedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600B2F9 RID: 45817 RVA: 0x0041521B File Offset: 0x0041341B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Your chance to block with shields is increased by 25%.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2FA RID: 45818 RVA: 0x00415235 File Offset: 0x00413435
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Actor.ModIntProperty("ImprovedBlock", 1, true);
				this.Applied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2FB RID: 45819 RVA: 0x00415266 File Offset: 0x00413466
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this.Applied)
			{
				this.Applied = false;
				E.Actor.ModIntProperty("ImprovedBlock", -1, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2FC RID: 45820 RVA: 0x00415291 File Offset: 0x00413491
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Implantee.ModIntProperty("ImprovedBlock", 1, true);
				this.Applied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2FD RID: 45821 RVA: 0x004152C2 File Offset: 0x004134C2
		public override bool HandleEvent(UnimplantedEvent E)
		{
			if (this.Applied)
			{
				this.Applied = false;
				E.Actor.ModIntProperty("ImprovedBlock", -1, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2FE RID: 45822 RVA: 0x004152ED File Offset: 0x004134ED
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004841 RID: 18497
		public bool Applied;
	}
}
