using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x020010EC RID: 4332
	[Serializable]
	public class NoteImprovedPenetrationBonus : IPart
	{
		// Token: 0x0600ABB8 RID: 43960 RVA: 0x003EE9E0 File Offset: 0x003ECBE0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ABB9 RID: 43961 RVA: 0x003EE9F8 File Offset: 0x003ECBF8
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
				" penetration rolls."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x040046F2 RID: 18162
		public int Amount;
	}
}
