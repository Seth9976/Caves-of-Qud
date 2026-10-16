using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F63 RID: 3939
	[Serializable]
	public class SifrahRulesDescription : IPart
	{
		// Token: 0x06009CF3 RID: 40179 RVA: 0x0038DA23 File Offset: 0x0038BC23
		public SifrahRulesDescription()
		{
		}

		// Token: 0x06009CF4 RID: 40180 RVA: 0x0038DA2B File Offset: 0x0038BC2B
		public SifrahRulesDescription(string Text)
			: this()
		{
			this.Text = Text;
		}

		// Token: 0x06009CF5 RID: 40181 RVA: 0x0038DA3A File Offset: 0x0038BC3A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009CF6 RID: 40182 RVA: 0x0038DA50 File Offset: 0x0038BC50
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (Options.AnySifrah && !GameText.VariableReplace(this.Text, this.ParentObject, The.Player, false).IsNullOrEmpty())
			{
				for (int i = 0; i < this.PrecedingNewlines; i++)
				{
					E.Postfix.Compound("", "\n");
				}
				E.Postfix.AppendRules(this.Text);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040040E2 RID: 16610
		public string Text;

		// Token: 0x040040E3 RID: 16611
		public int PrecedingNewlines;
	}
}
