using System;

namespace XRL.World.Parts
{
	// Token: 0x02001195 RID: 4501
	[Serializable]
	public class ModImprovedBludgeon : IModification
	{
		// Token: 0x0600B2FF RID: 45823 RVA: 0x004152F0 File Offset: 0x004134F0
		public ModImprovedBludgeon()
		{
		}

		// Token: 0x0600B300 RID: 45824 RVA: 0x004152F8 File Offset: 0x004134F8
		public ModImprovedBludgeon(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B301 RID: 45825 RVA: 0x00415301 File Offset: 0x00413501
		public override void Configure()
		{
			this.WorksOnEquipper = true;
			this.NameForStatus = "ImprovedBludgeon";
		}

		// Token: 0x0600B302 RID: 45826 RVA: 0x00415315 File Offset: 0x00413515
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == ImplantedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600B303 RID: 45827 RVA: 0x0041534B File Offset: 0x0041354B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Your chance to Bludgeon is doubled.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B304 RID: 45828 RVA: 0x00415365 File Offset: 0x00413565
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Actor.ModIntProperty("ImprovedBludgeon", 1, true);
				this.Applied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B305 RID: 45829 RVA: 0x00415396 File Offset: 0x00413596
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this.Applied)
			{
				this.Applied = false;
				E.Actor.ModIntProperty("ImprovedBludgeon", -1, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B306 RID: 45830 RVA: 0x004153C1 File Offset: 0x004135C1
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (this.ParentObject.IsEquippedProperly(null))
			{
				E.Implantee.ModIntProperty("ImprovedBludgeon", 1, true);
				this.Applied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B307 RID: 45831 RVA: 0x004153F2 File Offset: 0x004135F2
		public override bool HandleEvent(UnimplantedEvent E)
		{
			if (this.Applied)
			{
				this.Applied = false;
				E.Actor.ModIntProperty("ImprovedBludgeon", -1, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B308 RID: 45832 RVA: 0x0041541D File Offset: 0x0041361D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004842 RID: 18498
		public bool Applied;
	}
}
