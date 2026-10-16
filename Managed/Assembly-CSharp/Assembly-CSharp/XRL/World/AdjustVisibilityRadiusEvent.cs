using System;

namespace XRL.World
{
	// Token: 0x02000650 RID: 1616
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class AdjustVisibilityRadiusEvent : PooledEvent<AdjustVisibilityRadiusEvent>
	{
		// Token: 0x06004603 RID: 17923 RVA: 0x00165872 File Offset: 0x00163A72
		public override int GetCascadeLevel()
		{
			return AdjustVisibilityRadiusEvent.CascadeLevel;
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00165879 File Offset: 0x00163A79
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x00165882 File Offset: 0x00163A82
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Radius = 0;
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x00165898 File Offset: 0x00163A98
		public static AdjustVisibilityRadiusEvent FromPool(GameObject Object, int Radius)
		{
			AdjustVisibilityRadiusEvent adjustVisibilityRadiusEvent = PooledEvent<AdjustVisibilityRadiusEvent>.FromPool();
			adjustVisibilityRadiusEvent.Object = Object;
			adjustVisibilityRadiusEvent.Radius = Radius;
			return adjustVisibilityRadiusEvent;
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x001658B0 File Offset: 0x00163AB0
		public static int GetFor(GameObject Actor, int Radius = 80)
		{
			if (Actor.WantEvent(PooledEvent<AdjustVisibilityRadiusEvent>.ID, AdjustVisibilityRadiusEvent.CascadeLevel))
			{
				AdjustVisibilityRadiusEvent adjustVisibilityRadiusEvent = AdjustVisibilityRadiusEvent.FromPool(Actor, Radius);
				Actor.HandleEvent(adjustVisibilityRadiusEvent);
				Radius = adjustVisibilityRadiusEvent.Radius;
			}
			return Radius;
		}

		// Token: 0x0400213A RID: 8506
		public new static readonly int CascadeLevel = 1;

		// Token: 0x0400213B RID: 8507
		public GameObject Object;

		// Token: 0x0400213C RID: 8508
		public int Radius;
	}
}
