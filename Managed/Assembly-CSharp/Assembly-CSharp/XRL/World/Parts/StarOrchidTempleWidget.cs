using System;

namespace XRL.World.Parts
{
	// Token: 0x0200130C RID: 4876
	[Serializable]
	public class StarOrchidTempleWidget : IPart
	{
		// Token: 0x0600BED2 RID: 48850 RVA: 0x0045DD8C File Offset: 0x0045BF8C
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600BED3 RID: 48851 RVA: 0x0045DD98 File Offset: 0x0045BF98
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			StarOrchidTempleWidget.MapCell = string.Format("{0}.{1}.{2}", E.Zone.ZoneWorld, E.Zone.wX, E.Zone.wY);
			XRLGame game = The.Game;
			StarOrchidTempleWidget.Handler handler;
			if ((handler = StarOrchidTempleWidget.HandlerInstance) == null)
			{
				handler = (StarOrchidTempleWidget.HandlerInstance = new StarOrchidTempleWidget.Handler());
			}
			game.RegisterEvent(handler, PooledEvent<GetZoneEvent>.ID, 0, false);
			return base.HandleEvent(E);
		}

		// Token: 0x04004CF4 RID: 19700
		public static string MapCell;

		// Token: 0x04004CF5 RID: 19701
		public static StarOrchidTempleWidget.Handler HandlerInstance;

		// Token: 0x02001D2E RID: 7470
		public class Handler : IEventHandler
		{
			// Token: 0x0600F36E RID: 62318 RVA: 0x0054FF14 File Offset: 0x0054E114
			public bool HandleEvent(GetZoneEvent E)
			{
				if (E.ZoneID.StartsWith(StarOrchidTempleWidget.MapCell, StringComparison.Ordinal))
				{
					string text;
					int num;
					int num2;
					int num3;
					int num4;
					int num5;
					ZoneID.Parse(E.ZoneID, out text, out num, out num2, out num3, out num4, out num5);
					if (num3 != 1 || num4 != 1)
					{
						E.Result = The.ZoneManager.GetZone(ZoneID.Assemble(text, num, num2, 1, 1, num5));
						return false;
					}
				}
				return true;
			}
		}
	}
}
