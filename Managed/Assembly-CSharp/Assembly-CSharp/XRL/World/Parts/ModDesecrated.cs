using System;

namespace XRL.World.Parts
{
	// Token: 0x02001130 RID: 4400
	[Serializable]
	public class ModDesecrated : IModification
	{
		// Token: 0x0600AE30 RID: 44592 RVA: 0x00405EFE File Offset: 0x004040FE
		public ModDesecrated()
		{
		}

		// Token: 0x0600AE31 RID: 44593 RVA: 0x00405F06 File Offset: 0x00404106
		public ModDesecrated(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE32 RID: 44594 RVA: 0x00405F0F File Offset: 0x0040410F
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AE33 RID: 44595 RVA: 0x00405F18 File Offset: 0x00404118
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AE34 RID: 44596 RVA: 0x00405F36 File Offset: 0x00404136
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				E.AddAdjective("{{K|desecrated}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE35 RID: 44597 RVA: 0x00405F60 File Offset: 0x00404160
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Desecrated: This object has been desecrated by vandals.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE36 RID: 44598 RVA: 0x00405F7A File Offset: 0x0040417A
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AE37 RID: 44599 RVA: 0x00405F7D File Offset: 0x0040417D
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors("&K", "r", ModDesecrated.ICON_COLOR_PRIORITY, ModDesecrated.ICON_COLOR_PRIORITY);
			return true;
		}

		// Token: 0x040047E6 RID: 18406
		public static readonly int ICON_COLOR_PRIORITY = 160;
	}
}
