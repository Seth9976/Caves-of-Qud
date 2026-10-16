using System;

namespace XRL.World
{
	// Token: 0x02000754 RID: 1876
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetIdealTemperatureEvent : PooledEvent<GetIdealTemperatureEvent>
	{
		// Token: 0x06004D7F RID: 19839 RVA: 0x0017D38C File Offset: 0x0017B58C
		public override int GetCascadeLevel()
		{
			return GetIdealTemperatureEvent.CascadeLevel;
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x0017D393 File Offset: 0x0017B593
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x0017D39C File Offset: 0x0017B59C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Temperature = 0;
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x0017D3B4 File Offset: 0x0017B5B4
		public static int GetFor(GameObject Object, int DefaultTemperature = 25)
		{
			bool flag = true;
			int num = DefaultTemperature;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetIdealTemperature"))
			{
				Event @event = Event.New("GetIdealTemperature", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Temperature", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Temperature", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetIdealTemperatureEvent>.ID, GetIdealTemperatureEvent.CascadeLevel))
			{
				GetIdealTemperatureEvent getIdealTemperatureEvent = PooledEvent<GetIdealTemperatureEvent>.FromPool();
				getIdealTemperatureEvent.Object = Object;
				getIdealTemperatureEvent.Temperature = num;
				flag = Object.HandleEvent(getIdealTemperatureEvent);
				num = getIdealTemperatureEvent.Temperature;
			}
			return num;
		}

		// Token: 0x040025A0 RID: 9632
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040025A1 RID: 9633
		public GameObject Object;

		// Token: 0x040025A2 RID: 9634
		public int Temperature;
	}
}
