using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x0200111E RID: 4382
	[Serializable]
	public class NoteImprovedAccuracy : IPart
	{
		// Token: 0x0600AD7B RID: 44411 RVA: 0x004039D2 File Offset: 0x00401BD2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AD7C RID: 44412 RVA: 0x004039E8 File Offset: 0x00401BE8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				this.ParentObject.IndicativeProximal,
				" ",
				this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
				this.ParentObject.GetVerb("have", true, false, false, true, false),
				" improved accuracy."
			}));
			return base.HandleEvent(E);
		}
	}
}
