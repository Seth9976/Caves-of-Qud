using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F7D RID: 3965
	[Serializable]
	public class BreatheOnEat : IPart
	{
		// Token: 0x06009DBD RID: 40381 RVA: 0x00391F0C File Offset: 0x0039010C
		public override bool SameAs(IPart p)
		{
			BreatheOnEat breatheOnEat = p as BreatheOnEat;
			return breatheOnEat.Class == this.Class && breatheOnEat.Level == this.Level;
		}

		// Token: 0x06009DBE RID: 40382 RVA: 0x00391F43 File Offset: 0x00390143
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Eaten");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DBF RID: 40383 RVA: 0x00391F58 File Offset: 0x00390158
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Eaten")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				BreatherBase breatherBase = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Parts.Mutation." + this.Class, false, false, true)) as BreatherBase;
				breatherBase.ParentObject = gameObjectParameter;
				breatherBase.Level = this.Level;
				return BreatherBase.Cast(breatherBase);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004135 RID: 16693
		public string Class = "";

		// Token: 0x04004136 RID: 16694
		public int Level = 5;
	}
}
