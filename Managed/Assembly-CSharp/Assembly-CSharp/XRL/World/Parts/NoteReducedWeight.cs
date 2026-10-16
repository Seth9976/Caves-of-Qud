using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001078 RID: 4216
	[Serializable]
	public class NoteReducedWeight : IPart
	{
		// Token: 0x0600A7CC RID: 42956 RVA: 0x003D50C4 File Offset: 0x003D32C4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A7CD RID: 42957 RVA: 0x003D50DC File Offset: 0x003D32DC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				this.ParentObject.IndicativeProximal,
				" ",
				this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
				this.ParentObject.GetVerb("weigh", true, false, false, true, false),
				" ",
				Grammar.Cardinal(this.Amount),
				" ",
				(this.Amount == 1) ? "pound" : "pounds",
				" less than usual."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x04004539 RID: 17721
		public int Amount;
	}
}
