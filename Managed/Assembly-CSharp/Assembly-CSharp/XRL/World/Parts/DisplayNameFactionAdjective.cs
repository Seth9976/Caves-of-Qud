using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F58 RID: 3928
	[Serializable]
	public class DisplayNameFactionAdjective : IPart
	{
		// Token: 0x06009CA8 RID: 40104 RVA: 0x0038C885 File Offset: 0x0038AA85
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06009CA9 RID: 40105 RVA: 0x0038C89C File Offset: 0x0038AA9C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.EvenIfProperName || !E.Object.HasProperName)
			{
				if (E.Object.IsFactionMember(this.Faction))
				{
					if (!this.FactionAdjective.IsNullOrEmpty())
					{
						E.AddAdjective(this.FactionAdjective, 110);
					}
				}
				else if (!this.NonFactionAdjective.IsNullOrEmpty())
				{
					E.AddAdjective(this.NonFactionAdjective, 110);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040040C7 RID: 16583
		public string Faction;

		// Token: 0x040040C8 RID: 16584
		public string FactionAdjective;

		// Token: 0x040040C9 RID: 16585
		public string NonFactionAdjective;

		// Token: 0x040040CA RID: 16586
		public bool EvenIfProperName;
	}
}
