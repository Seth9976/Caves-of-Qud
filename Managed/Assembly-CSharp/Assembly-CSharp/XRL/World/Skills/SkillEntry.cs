using System;
using System.Collections.Generic;

namespace XRL.World.Skills
{
	// Token: 0x02000BA8 RID: 2984
	[Serializable]
	public class SkillEntry : IBaseSkillEntry
	{
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060076CC RID: 30412 RVA: 0x002CBC3F File Offset: 0x002C9E3F
		// (set) Token: 0x060076CD RID: 30413 RVA: 0x002CBC4D File Offset: 0x002C9E4D
		public bool Initiatory
		{
			get
			{
				return this.Flags.HasBit(4);
			}
			set
			{
				(ref this.Flags).SetBit(4, value);
			}
		}

		// Token: 0x060076CE RID: 30414 RVA: 0x002CBC5C File Offset: 0x002C9E5C
		public bool Has(GameObject Object)
		{
			return Object != null && Object.HasPart(this.Class);
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x002CBC70 File Offset: 0x002C9E70
		public override bool MeetsRequirements(GameObject Object, bool ShowPopup = false)
		{
			foreach (PowerEntry powerEntry in this.PowerList)
			{
				if (powerEntry.Cost == 0 && !powerEntry.MeetsRequirements(Object, ShowPopup))
				{
					return false;
				}
			}
			return base.Generic.MeetsRequirements(Object, ShowPopup);
		}

		// Token: 0x060076D0 RID: 30416 RVA: 0x002CBCE4 File Offset: 0x002C9EE4
		public void Add(PowerEntry Power)
		{
			this.PowerList.Add(Power);
			this.Powers.Add(Power.Name, Power);
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x002CBD04 File Offset: 0x002C9F04
		public override void HandleXMLNode(XmlDataHelper Reader)
		{
			this.Initiatory = Reader.ParseAttribute<bool>("Initiatory", this.Initiatory, false, null);
			if (this.Initiatory)
			{
				base.Hidden = true;
			}
			base.HandleXMLNode(Reader);
		}

		// Token: 0x040036B1 RID: 14001
		public List<PowerEntry> PowerList = new List<PowerEntry>();

		// Token: 0x040036B2 RID: 14002
		public Dictionary<string, PowerEntry> Powers = new Dictionary<string, PowerEntry>();
	}
}
