using System;

namespace XRL.World
{
	// Token: 0x02000725 RID: 1829
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class GetAmbientLightEvent : SingletonEvent<GetAmbientLightEvent>
	{
		// Token: 0x06004BF5 RID: 19445 RVA: 0x001785E8 File Offset: 0x001767E8
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x001785F1 File Offset: 0x001767F1
		public override void Reset()
		{
			base.Reset();
			this.Source = null;
			this.Type = null;
			this.Light = LightLevel.Blackout;
			this.Radius = 0;
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x00178618 File Offset: 0x00176818
		public static void Send(object Source, string Type, ref LightLevel Light, ref int Radius)
		{
			SingletonEvent<GetAmbientLightEvent>.Instance.Source = Source;
			SingletonEvent<GetAmbientLightEvent>.Instance.Type = Type;
			SingletonEvent<GetAmbientLightEvent>.Instance.Light = Light;
			SingletonEvent<GetAmbientLightEvent>.Instance.Radius = Radius;
			The.Game.HandleEvent(SingletonEvent<GetAmbientLightEvent>.Instance);
			Light = SingletonEvent<GetAmbientLightEvent>.Instance.Light;
			Radius = SingletonEvent<GetAmbientLightEvent>.Instance.Radius;
			SingletonEvent<GetAmbientLightEvent>.Instance.Reset();
		}

		// Token: 0x040024BC RID: 9404
		public object Source;

		// Token: 0x040024BD RID: 9405
		public string Type;

		// Token: 0x040024BE RID: 9406
		public LightLevel Light;

		// Token: 0x040024BF RID: 9407
		public int Radius;
	}
}
