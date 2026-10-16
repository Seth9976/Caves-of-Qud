using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000EDF RID: 3807
	public abstract class INephalChord : IPart
	{
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06009850 RID: 38992
		public abstract string Source { get; }

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06009851 RID: 38993 RVA: 0x00375324 File Offset: 0x00373524
		public virtual string SourceName
		{
			get
			{
				string text;
				if ((text = this._DisplayName) == null)
				{
					text = (this._DisplayName = GameObjectFactory.Factory.GetBlueprint(this.Source).CachedDisplayNameStripped);
				}
				return text;
			}
		}

		// Token: 0x06009852 RID: 38994 RVA: 0x00375359 File Offset: 0x00373559
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009853 RID: 38995 RVA: 0x00375370 File Offset: 0x00373570
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Append("\n{{rules|").Append("This creature burns bright in the chord of ").Append("{{")
				.AppendLower(this.Source)
				.Append('|')
				.Append(this.SourceName)
				.Append(".}}");
			this.AppendRules(E.Postfix);
			E.Postfix.Append("}}");
			return base.HandleEvent(E);
		}

		// Token: 0x06009854 RID: 38996
		public abstract void AppendRules(StringBuilder Postfix);

		// Token: 0x04003F78 RID: 16248
		public const string BASE_DESC = "This creature burns bright in the chord of ";

		// Token: 0x04003F79 RID: 16249
		[NonSerialized]
		private string _DisplayName;
	}
}
