using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E1A RID: 3610
	[Serializable]
	public class StandaloneMarkovBook : IPart
	{
		// Token: 0x0600930A RID: 37642 RVA: 0x0034FD18 File Offset: 0x0034DF18
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600930B RID: 37643 RVA: 0x0034FD1B File Offset: 0x0034DF1B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600930C RID: 37644 RVA: 0x0034FD30 File Offset: 0x0034DF30
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectCreated")
			{
				this.ParentObject.RequirePart<MarkovBook>(false).SetContents(Stat.Random(0, 2147483646), this.Chain);
				this.ParentObject.RemovePart(this);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003CD7 RID: 15575
		public string Chain = "";
	}
}
