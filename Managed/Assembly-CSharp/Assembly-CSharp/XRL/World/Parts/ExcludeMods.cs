using System;

namespace XRL.World.Parts
{
	// Token: 0x02001027 RID: 4135
	[Serializable]
	public class ExcludeMods : IPart
	{
		// Token: 0x0600A4BD RID: 42173 RVA: 0x003C3B80 File Offset: 0x003C1D80
		public override bool SameAs(IPart p)
		{
			return !((p as ExcludeMods).Exclude != this.Exclude) && base.SameAs(p);
		}

		// Token: 0x0600A4BE RID: 42174 RVA: 0x003C3BA3 File Offset: 0x003C1DA3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanBeModdedEvent.ID;
		}

		// Token: 0x0600A4BF RID: 42175 RVA: 0x003C3BBC File Offset: 0x003C1DBC
		public override bool HandleEvent(CanBeModdedEvent E)
		{
			return !(this.Exclude == "*") && (this.Exclude.IsNullOrEmpty() || E.ModName.IsNullOrEmpty() || !this.Exclude.HasDelimitedSubstring(',', E.ModName, StringComparison.Ordinal)) && base.HandleEvent(E);
		}

		// Token: 0x04004436 RID: 17462
		public string Exclude;
	}
}
