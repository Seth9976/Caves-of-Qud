using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E0C RID: 3596
	public abstract class BasePrefabImposter : IPart
	{
		// Token: 0x06009155 RID: 37205 RVA: 0x00347434 File Offset: 0x00345634
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID;
		}

		// Token: 0x06009156 RID: 37206 RVA: 0x0034744A File Offset: 0x0034564A
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Prefab", this.Prefab);
			E.AddEntry(this, "Disabled", this.Disabled);
			return base.HandleEvent(E);
		}

		// Token: 0x04003CA0 RID: 15520
		public string Prefab;

		// Token: 0x04003CA1 RID: 15521
		public string Config;

		// Token: 0x04003CA2 RID: 15522
		public bool Disabled;

		// Token: 0x04003CA3 RID: 15523
		public int Layer;

		// Token: 0x04003CA4 RID: 15524
		public int X;

		// Token: 0x04003CA5 RID: 15525
		public int Y;

		// Token: 0x04003CA6 RID: 15526
		public int Z;
	}
}
