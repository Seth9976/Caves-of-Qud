using System;
using XRL.World.Effects;

namespace XRL.World
{
	// Token: 0x0200083D RID: 2109
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class RealityStabilizeEvent : PooledEvent<RealityStabilizeEvent>
	{
		// Token: 0x060053CD RID: 21453 RVA: 0x00190612 File Offset: 0x0018E812
		public override int GetCascadeLevel()
		{
			return RealityStabilizeEvent.CascadeLevel;
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00190619 File Offset: 0x0018E819
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x00190622 File Offset: 0x0018E822
		public override void Reset()
		{
			base.Reset();
			this.Effect = null;
			this.Object = null;
			this.Projecting = false;
			this.Relevant = false;
			this.CanDestroy = false;
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0019064D File Offset: 0x0018E84D
		public static RealityStabilizeEvent FromPool(RealityStabilized Effect, GameObject Object, bool Projecting = false)
		{
			RealityStabilizeEvent realityStabilizeEvent = PooledEvent<RealityStabilizeEvent>.FromPool();
			realityStabilizeEvent.Effect = Effect;
			realityStabilizeEvent.Object = Object;
			realityStabilizeEvent.Projecting = Projecting;
			realityStabilizeEvent.Relevant = false;
			realityStabilizeEvent.CanDestroy = false;
			return realityStabilizeEvent;
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x00190677 File Offset: 0x0018E877
		public bool Check(bool CanDestroy = false)
		{
			this.Relevant = true;
			if (CanDestroy)
			{
				this.CanDestroy = true;
				GameObject @object = this.Object;
				if (@object != null && @object.IsNatural())
				{
					return false;
				}
			}
			return this.Effect.RandomlyTakeEffect();
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x001906AC File Offset: 0x0018E8AC
		public static void Send(RealityStabilized Effect, GameObject Object, bool Projecting, out bool Relevant, out bool CanDestroy)
		{
			if (Object.WantEvent(PooledEvent<RealityStabilizeEvent>.ID, RealityStabilizeEvent.CascadeLevel))
			{
				RealityStabilizeEvent realityStabilizeEvent = RealityStabilizeEvent.FromPool(Effect, Object, Projecting);
				Object.HandleEvent(realityStabilizeEvent);
				Relevant = realityStabilizeEvent.Relevant;
				CanDestroy = realityStabilizeEvent.CanDestroy;
				return;
			}
			Relevant = false;
			CanDestroy = false;
		}

		// Token: 0x04002A21 RID: 10785
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002A22 RID: 10786
		public RealityStabilized Effect;

		// Token: 0x04002A23 RID: 10787
		public GameObject Object;

		// Token: 0x04002A24 RID: 10788
		public bool Projecting;

		// Token: 0x04002A25 RID: 10789
		public bool Relevant;

		// Token: 0x04002A26 RID: 10790
		public bool CanDestroy;
	}
}
