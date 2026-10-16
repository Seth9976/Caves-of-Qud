using System;

namespace XRL.World
{
	// Token: 0x020006D0 RID: 1744
	[GameEvent(Cascade = 17)]
	public class CanTravelEvent : PooledEvent<CanTravelEvent>
	{
		// Token: 0x060049AB RID: 18859 RVA: 0x00172500 File Offset: 0x00170700
		public override int GetCascadeLevel()
		{
			return CanTravelEvent.CascadeLevel;
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x00172507 File Offset: 0x00170707
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x00172510 File Offset: 0x00170710
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x00172520 File Offset: 0x00170720
		public static bool Check(GameObject Object)
		{
			if (!GameObject.Validate(Object))
			{
				return false;
			}
			CanTravelEvent canTravelEvent = PooledEvent<CanTravelEvent>.FromPool();
			try
			{
				canTravelEvent.Object = Object;
				if (!Object.HandleEvent(canTravelEvent))
				{
					return false;
				}
				if (!The.Game.HandleEvent(canTravelEvent))
				{
					return false;
				}
			}
			finally
			{
				PooledEvent<CanTravelEvent>.ResetTo(ref canTravelEvent);
			}
			return true;
		}

		// Token: 0x0400236A RID: 9066
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400236B RID: 9067
		public GameObject Object;
	}
}
