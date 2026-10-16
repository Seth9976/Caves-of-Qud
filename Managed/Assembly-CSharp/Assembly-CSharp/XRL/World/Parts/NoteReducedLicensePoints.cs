using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02000F53 RID: 3923
	[Serializable]
	public class NoteReducedLicensePoints : IPart
	{
		// Token: 0x06009C85 RID: 40069 RVA: 0x0038C08C File Offset: 0x0038A28C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009C86 RID: 40070 RVA: 0x0038C0A4 File Offset: 0x0038A2A4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				this.ParentObject.IndicativeProximal,
				" ",
				this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
				this.ParentObject.GetVerb("cost", true, false, false, true, false),
				" ",
				Grammar.Cardinal(this.Amount),
				" fewer license ",
				(this.Amount == 1) ? "point" : "points",
				" than usual to install."
			}));
			return base.HandleEvent(E);
		}

		// Token: 0x040040BE RID: 16574
		public int Amount;
	}
}
