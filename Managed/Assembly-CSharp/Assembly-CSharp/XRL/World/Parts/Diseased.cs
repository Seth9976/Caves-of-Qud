using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E9E RID: 3742
	[Serializable]
	public class Diseased : IPart
	{
		// Token: 0x0600966D RID: 38509 RVA: 0x00368243 File Offset: 0x00366443
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600966E RID: 38510 RVA: 0x00368246 File Offset: 0x00366446
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600966F RID: 38511 RVA: 0x0036825C File Offset: 0x0036645C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectCreated" && 33.in100())
			{
				int num = Stat.Random(1, 2);
				if (num == 1)
				{
					this.ParentObject.ApplyEffect(new Glotrot(), null);
					this.ParentObject.GetEffect<Glotrot>().Stage = Stat.Random(0, 3);
				}
				else if (num == 2)
				{
					this.ParentObject.ApplyEffect(new Ironshank(), null);
					this.ParentObject.GetEffect<Ironshank>().SetPenalty(Stat.Random(1, 75));
				}
			}
			return base.FireEvent(E);
		}
	}
}
