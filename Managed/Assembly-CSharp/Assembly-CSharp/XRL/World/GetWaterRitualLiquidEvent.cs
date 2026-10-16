using System;

namespace XRL.World
{
	// Token: 0x020007A4 RID: 1956
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetWaterRitualLiquidEvent : PooledEvent<GetWaterRitualLiquidEvent>
	{
		// Token: 0x06004FB7 RID: 20407 RVA: 0x0018500F File Offset: 0x0018320F
		public override int GetCascadeLevel()
		{
			return GetWaterRitualLiquidEvent.CascadeLevel;
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x00185016 File Offset: 0x00183216
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0018501F File Offset: 0x0018321F
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Liquid = null;
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x0018503C File Offset: 0x0018323C
		public static string GetFor(GameObject Actor, GameObject Target)
		{
			string text = Target.GetPropertyOrTag("WaterRitualLiquid", "water");
			bool flag = true;
			if (flag && Target.HasRegisteredEvent("GetWaterRitualLiquid"))
			{
				Event @event = Event.New("GetWaterRitualLiquid", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Liquid", text);
				flag = Target.FireEvent(@event);
				text = @event.GetStringParameter("Liquid", null);
			}
			if (flag && Target.WantEvent(PooledEvent<GetWaterRitualLiquidEvent>.ID, GetWaterRitualLiquidEvent.CascadeLevel))
			{
				GetWaterRitualLiquidEvent getWaterRitualLiquidEvent = PooledEvent<GetWaterRitualLiquidEvent>.FromPool();
				getWaterRitualLiquidEvent.Actor = Actor;
				getWaterRitualLiquidEvent.Target = Target;
				getWaterRitualLiquidEvent.Liquid = text;
				flag = Target.HandleEvent(getWaterRitualLiquidEvent);
				text = getWaterRitualLiquidEvent.Liquid;
			}
			int num = text.IndexOf('-');
			if (num != -1)
			{
				string text2 = text.Substring(0, num);
				if (text2 == "water")
				{
					int num2 = text.IndexOf(',');
					if (num2 != -1)
					{
						text2 = text.Substring(num2 + 1);
						num = text2.IndexOf('-');
						if (num != -1)
						{
							text2 = text2.Substring(0, num);
						}
					}
				}
				text = text2;
			}
			return text;
		}

		// Token: 0x04002764 RID: 10084
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002765 RID: 10085
		public GameObject Actor;

		// Token: 0x04002766 RID: 10086
		public GameObject Target;

		// Token: 0x04002767 RID: 10087
		public string Liquid;
	}
}
