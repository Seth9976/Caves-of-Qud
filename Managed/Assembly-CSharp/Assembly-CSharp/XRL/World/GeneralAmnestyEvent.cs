using System;

namespace XRL.World
{
	// Token: 0x0200071B RID: 1819
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class GeneralAmnestyEvent : SingletonEvent<GeneralAmnestyEvent>
	{
		// Token: 0x06004BBA RID: 19386 RVA: 0x00177A1F File Offset: 0x00175C1F
		public override int GetCascadeLevel()
		{
			return GeneralAmnestyEvent.CascadeLevel;
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x00177A26 File Offset: 0x00175C26
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x00177A2F File Offset: 0x00175C2F
		public static void Send(Zone Z)
		{
			if (Z == null)
			{
				return;
			}
			Z.HandleEvent(SingletonEvent<GeneralAmnestyEvent>.Instance);
			SingletonEvent<GeneralAmnestyEvent>.Instance.Reset();
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x00177A4B File Offset: 0x00175C4B
		public static void Send()
		{
			The.ZoneManager.HandleEvent<GeneralAmnestyEvent>(SingletonEvent<GeneralAmnestyEvent>.Instance);
			SingletonEvent<GeneralAmnestyEvent>.Instance.Reset();
		}

		// Token: 0x04002485 RID: 9349
		public new static readonly int CascadeLevel = 15;
	}
}
