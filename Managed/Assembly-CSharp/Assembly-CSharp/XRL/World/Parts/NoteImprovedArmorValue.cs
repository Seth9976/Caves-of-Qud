using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001075 RID: 4213
	[Serializable]
	public class NoteImprovedArmorValue : IPart
	{
		// Token: 0x0600A7C3 RID: 42947 RVA: 0x003D4E30 File Offset: 0x003D3030
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A7C4 RID: 42948 RVA: 0x003D4E48 File Offset: 0x003D3048
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
				" armor value."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x04004536 RID: 17718
		public int Amount;
	}
}
