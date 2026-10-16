using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000E0F RID: 3599
	[Serializable]
	public class IntegralAnatomy : IPart
	{
		// Token: 0x060092A0 RID: 37536 RVA: 0x0034D1A0 File Offset: 0x0034B3A0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x060092A1 RID: 37537 RVA: 0x0034D1B8 File Offset: 0x0034B3B8
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			foreach (BodyPart bodyPart in E.Object.Body.LoopParts())
			{
				if (this.IsApplicable(bodyPart))
				{
					bodyPart.Integral = true;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060092A2 RID: 37538 RVA: 0x0034D220 File Offset: 0x0034B420
		public bool IsApplicable(BodyPart Part)
		{
			if (Part.Integral || (this.MortalOnly && !Part.Mortal))
			{
				return false;
			}
			if (!this.IncludeTypes.IsNullOrEmpty())
			{
				if (Part.Type == null)
				{
					return false;
				}
				if (!this.IncludeTypes.CachedCommaExpansion().Contains(Part.Type))
				{
					return false;
				}
			}
			return Part.Type == null || this.ExcludeTypes.IsNullOrEmpty() || !this.ExcludeTypes.CachedCommaExpansion().Contains(Part.Type);
		}

		// Token: 0x04003CBA RID: 15546
		public string IncludeTypes;

		// Token: 0x04003CBB RID: 15547
		public string ExcludeTypes;

		// Token: 0x04003CBC RID: 15548
		public bool MortalOnly;
	}
}
