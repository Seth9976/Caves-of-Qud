using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02001182 RID: 4482
	[Serializable]
	public class ModImprovedTemporalFugue : ModImprovedMutationBase<TemporalFugue>
	{
		// Token: 0x0600B232 RID: 45618 RVA: 0x004136C6 File Offset: 0x004118C6
		public ModImprovedTemporalFugue()
		{
		}

		// Token: 0x0600B233 RID: 45619 RVA: 0x004136CE File Offset: 0x004118CE
		public ModImprovedTemporalFugue(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B234 RID: 45620 RVA: 0x004136D7 File Offset: 0x004118D7
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600B235 RID: 45621 RVA: 0x004136ED File Offset: 0x004118ED
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("time", 1);
			}
			return base.HandleEvent(E);
		}
	}
}
