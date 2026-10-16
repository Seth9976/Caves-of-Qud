using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001076 RID: 4214
	[Serializable]
	public class NoteImprovedDodgeValue : IPart
	{
		// Token: 0x0600A7C6 RID: 42950 RVA: 0x003D4F0C File Offset: 0x003D310C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A7C7 RID: 42951 RVA: 0x003D4F24 File Offset: 0x003D3124
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				this.ParentObject.IndicativeProximal,
				" ",
				this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
				this.ParentObject.GetVerb("have", true, false, false, true, false),
				" ",
				this.Amount.Signed(false),
				" to ",
				this.ParentObject.its,
				" dodge value."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x04004537 RID: 17719
		public int Amount;
	}
}
