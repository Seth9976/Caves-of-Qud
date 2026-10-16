using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000F84 RID: 3972
	[Serializable]
	public class JumpOnEat : IPart
	{
		// Token: 0x06009DE2 RID: 40418 RVA: 0x00392D28 File Offset: 0x00390F28
		public override bool SameAs(IPart p)
		{
			JumpOnEat jumpOnEat = p as JumpOnEat;
			return !(jumpOnEat.Hops != this.Hops) && !(jumpOnEat.BonusRange != this.BonusRange) && !(jumpOnEat.SourceKey != this.SourceKey) && base.SameAs(p);
		}

		// Token: 0x06009DE3 RID: 40419 RVA: 0x00392D82 File Offset: 0x00390F82
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DE4 RID: 40420 RVA: 0x00392D98 File Offset: 0x00390F98
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				int num = this.Hops.RollCached();
				bool flag = false;
				int num2 = 0;
				while (num2 < num && Acrobatics_Jump.Jump(gameObjectParameter, this.BonusRange.RollCached(), null, this.SourceKey))
				{
					flag = true;
					num2++;
				}
				if (flag && gameObjectParameter.IsPlayer())
				{
					E.RequestInterfaceExit();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400414A RID: 16714
		public string Hops = "1";

		// Token: 0x0400414B RID: 16715
		public string BonusRange = "0";

		// Token: 0x0400414C RID: 16716
		public string SourceKey = "JumpOnEat";
	}
}
