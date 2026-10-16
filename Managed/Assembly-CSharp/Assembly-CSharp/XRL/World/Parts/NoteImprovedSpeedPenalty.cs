using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001077 RID: 4215
	[Serializable]
	public class NoteImprovedSpeedPenalty : IPart
	{
		// Token: 0x0600A7C9 RID: 42953 RVA: 0x003D4FE8 File Offset: 0x003D31E8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A7CA RID: 42954 RVA: 0x003D5000 File Offset: 0x003D3200
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
				" speed penalty."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x04004538 RID: 17720
		public int Amount;
	}
}
