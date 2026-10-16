using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E8A RID: 3722
	[Serializable]
	public class Raycat : IPart
	{
		// Token: 0x060095EB RID: 38379 RVA: 0x003644E8 File Offset: 0x003626E8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<PhysicalContactEvent>.ID;
		}

		// Token: 0x060095EC RID: 38380 RVA: 0x00364500 File Offset: 0x00362700
		public override bool HandleEvent(PhysicalContactEvent E)
		{
			if (E.Object == this.ParentObject && E.Actor != E.Object && GameObject.Validate(E.Actor) && !E.Actor.HasEffect(typeof(Luminous)))
			{
				E.Actor.ApplyEffect(new Luminous(100 + Stat.Random(0, 100)), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095ED RID: 38381 RVA: 0x00364571 File Offset: 0x00362771
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
