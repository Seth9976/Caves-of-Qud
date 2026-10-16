using System;

namespace XRL.World
{
	// Token: 0x0200072E RID: 1838
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetBleedLiquidEvent : PooledEvent<GetBleedLiquidEvent>
	{
		// Token: 0x06004C3E RID: 19518 RVA: 0x001794AC File Offset: 0x001776AC
		public override int GetCascadeLevel()
		{
			return GetBleedLiquidEvent.CascadeLevel;
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x001794B3 File Offset: 0x001776B3
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x001794BC File Offset: 0x001776BC
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.BaseLiquid = null;
			this.Liquid = null;
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x001794DC File Offset: 0x001776DC
		public static string GetFor(GameObject Actor, string Default = "blood-1000")
		{
			GetBleedLiquidEvent getBleedLiquidEvent = PooledEvent<GetBleedLiquidEvent>.FromPool();
			getBleedLiquidEvent.Actor = Actor;
			getBleedLiquidEvent.BaseLiquid = (getBleedLiquidEvent.Liquid = Actor.GetPropertyOrTag("BleedLiquid", Default));
			Actor.HandleEvent(getBleedLiquidEvent);
			string liquid = getBleedLiquidEvent.Liquid;
			PooledEvent<GetBleedLiquidEvent>.ResetTo(ref getBleedLiquidEvent);
			return liquid;
		}

		// Token: 0x040024E1 RID: 9441
		public new static readonly int CascadeLevel = 1;

		// Token: 0x040024E2 RID: 9442
		public GameObject Actor;

		// Token: 0x040024E3 RID: 9443
		public string BaseLiquid;

		// Token: 0x040024E4 RID: 9444
		public string Liquid;
	}
}
