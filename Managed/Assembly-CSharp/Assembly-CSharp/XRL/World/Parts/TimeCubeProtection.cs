using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F0E RID: 3854
	[Serializable]
	public class TimeCubeProtection : IPart
	{
		// Token: 0x060099A6 RID: 39334 RVA: 0x0037CE34 File Offset: 0x0037B034
		public void IncrementHarm()
		{
			if (!this.ParentObject.HasEffect<Nullphased>())
			{
				int num = this.HarmCount + 1;
				this.HarmCount = num;
				if (num >= 3)
				{
					this.HarmCount = 0;
					this.ParentObject.ApplyEffect(new Nullphased(1), null);
					if (!this.Message.IsNullOrEmpty())
					{
						this.ParentObject.EmitMessage(this.Message.StartReplace().AddObject(this.ParentObject, null).ToString(), null, IComponent<GameObject>.ConsequentialColor(this.ParentObject, null), false);
					}
					return;
				}
			}
		}

		// Token: 0x060099A7 RID: 39335 RVA: 0x0037CEBF File Offset: 0x0037B0BF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == TookDamageEvent.ID || ID == EffectAppliedEvent.ID;
		}

		// Token: 0x060099A8 RID: 39336 RVA: 0x0037CEDD File Offset: 0x0037B0DD
		public override bool HandleEvent(TookDamageEvent E)
		{
			if ((E.Actor != null && E.Actor.HasEffect<TimeCubed>()) || The.Player.HasEffect<TimeCubed>())
			{
				this.IncrementHarm();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060099A9 RID: 39337 RVA: 0x0037CF0D File Offset: 0x0037B10D
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (E.Effect.IsOfType(33554432) && The.Player.HasEffect<TimeCubed>())
			{
				this.IncrementHarm();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400400D RID: 16397
		public string Message;

		// Token: 0x0400400E RID: 16398
		public int HarmCount;
	}
}
