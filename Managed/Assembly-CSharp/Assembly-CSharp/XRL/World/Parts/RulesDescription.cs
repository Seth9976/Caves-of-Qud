using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F62 RID: 3938
	[Serializable]
	public class RulesDescription : IPart
	{
		// Token: 0x06009CEF RID: 40175 RVA: 0x0038D970 File Offset: 0x0038BB70
		public RulesDescription()
		{
		}

		// Token: 0x06009CF0 RID: 40176 RVA: 0x0038D978 File Offset: 0x0038BB78
		public RulesDescription(string Text)
			: this()
		{
			this.Text = Text;
		}

		// Token: 0x06009CF1 RID: 40177 RVA: 0x0038D987 File Offset: 0x0038BB87
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009CF2 RID: 40178 RVA: 0x0038D9A0 File Offset: 0x0038BBA0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			string text = ((!this.AltForGenotype.IsNullOrEmpty() && IComponent<GameObject>.ThePlayer.GetGenotype() == this.AltForGenotype) ? this.GenotypeAlt : this.Text);
			if (!text.IsNullOrEmpty())
			{
				for (int i = 0; i < this.PrecedingNewlines; i++)
				{
					E.Postfix.Compound("", "\n");
				}
				E.Postfix.AppendRules(text);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040040DE RID: 16606
		public string Text;

		// Token: 0x040040DF RID: 16607
		public string AltForGenotype;

		// Token: 0x040040E0 RID: 16608
		public string GenotypeAlt;

		// Token: 0x040040E1 RID: 16609
		public int PrecedingNewlines;
	}
}
