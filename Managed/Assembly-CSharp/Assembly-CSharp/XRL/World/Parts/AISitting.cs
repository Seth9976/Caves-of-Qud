using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E46 RID: 3654
	[Serializable]
	public class AISitting : AIBehaviorPart
	{
		// Token: 0x06009420 RID: 37920 RVA: 0x0035542B File Offset: 0x0035362B
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009421 RID: 37921 RVA: 0x0035542E File Offset: 0x0035362E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009422 RID: 37922 RVA: 0x00355443 File Offset: 0x00353643
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && this.bFirst)
			{
				this.bFirst = false;
				this.ParentObject.ApplyEffect(new Sitting(), null);
			}
			return true;
		}

		// Token: 0x04003D44 RID: 15684
		public bool bFirst = true;
	}
}
