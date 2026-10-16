using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x020010EB RID: 4331
	[Serializable]
	public class NoteImprovedDamageRoll : IPart
	{
		// Token: 0x0600ABB5 RID: 43957 RVA: 0x003EE904 File Offset: 0x003ECB04
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ABB6 RID: 43958 RVA: 0x003EE91C File Offset: 0x003ECB1C
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
				" damage rolls."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x040046F1 RID: 18161
		public int Amount;
	}
}
