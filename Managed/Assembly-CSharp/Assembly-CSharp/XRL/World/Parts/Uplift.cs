using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F11 RID: 3857
	[Serializable]
	public class Uplift : IPart
	{
		// Token: 0x060099C5 RID: 39365 RVA: 0x0037DAC3 File Offset: 0x0037BCC3
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060099C6 RID: 39366 RVA: 0x0037DAC6 File Offset: 0x0037BCC6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x060099C7 RID: 39367 RVA: 0x0037DADC File Offset: 0x0037BCDC
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (E.ReplacementObject == null)
			{
				HeroMaker.MakeHero(this.ParentObject, this.AdditionalBaseTemplate, this.AdditionalSpecializationTemplate, -1, "Hero");
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x04004013 RID: 16403
		public string AdditionalBaseTemplate;

		// Token: 0x04004014 RID: 16404
		public string AdditionalSpecializationTemplate;
	}
}
