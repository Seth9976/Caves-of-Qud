using System;

namespace XRL.World
{
	// Token: 0x0200082D RID: 2093
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Singleton)]
	public class ProducesLiquidEvent : ILiquidEvent
	{
		// Token: 0x06005335 RID: 21301 RVA: 0x0018ED12 File Offset: 0x0018CF12
		public ProducesLiquidEvent()
		{
			this.ID = ProducesLiquidEvent.ID;
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0018ED25 File Offset: 0x0018CF25
		public override int GetCascadeLevel()
		{
			return ProducesLiquidEvent.CascadeLevel;
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x0018ED2C File Offset: 0x0018CF2C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x0018ED40 File Offset: 0x0018CF40
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x0018ED50 File Offset: 0x0018CF50
		public static bool Check(GameObject Object, string Liquid)
		{
			if (!Object.Understood())
			{
				return false;
			}
			if (Object.WantEvent(ProducesLiquidEvent.ID, ProducesLiquidEvent.CascadeLevel))
			{
				ProducesLiquidEvent.Instance.Object = Object;
				ProducesLiquidEvent.Instance.Liquid = Liquid;
				if (!Object.HandleEvent(ProducesLiquidEvent.Instance))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040029DE RID: 10718
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ProducesLiquidEvent), null, null, null);

		// Token: 0x040029DF RID: 10719
		public new static readonly int CascadeLevel = 0;

		// Token: 0x040029E0 RID: 10720
		public static readonly ProducesLiquidEvent Instance = new ProducesLiquidEvent();

		// Token: 0x040029E1 RID: 10721
		public GameObject Object;
	}
}
