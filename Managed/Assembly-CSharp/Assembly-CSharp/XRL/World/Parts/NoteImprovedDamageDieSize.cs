using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x020010EA RID: 4330
	[Serializable]
	public class NoteImprovedDamageDieSize : IPart
	{
		// Token: 0x0600ABB2 RID: 43954 RVA: 0x003EE829 File Offset: 0x003ECA29
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ABB3 RID: 43955 RVA: 0x003EE840 File Offset: 0x003ECA40
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
				" damage die size."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x040046F0 RID: 18160
		public int Amount;
	}
}
