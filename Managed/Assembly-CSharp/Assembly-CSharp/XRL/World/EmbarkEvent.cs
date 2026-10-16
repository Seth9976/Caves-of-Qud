using System;
using XRL.CharacterBuilds;

namespace XRL.World
{
	// Token: 0x02000702 RID: 1794
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class EmbarkEvent : SingletonEvent<EmbarkEvent>
	{
		// Token: 0x06004AFC RID: 19196 RVA: 0x00175ABB File Offset: 0x00173CBB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x00175AC4 File Offset: 0x00173CC4
		public override void Reset()
		{
			base.Reset();
			this.Info = null;
			this.EventID = null;
			this.Element = null;
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x00175AE4 File Offset: 0x00173CE4
		public static void Send(XRLGame Game, EmbarkInfo Info, string EventID)
		{
			SingletonEvent<EmbarkEvent>.Instance.Info = Info;
			SingletonEvent<EmbarkEvent>.Instance.EventID = EventID;
			if (Game != null)
			{
				Game.HandleEvent(SingletonEvent<EmbarkEvent>.Instance, true);
			}
			foreach (IGameStateSingleton gameStateSingleton in Info.GameStateSingletons)
			{
				gameStateSingleton.HandleEvent(SingletonEvent<EmbarkEvent>.Instance);
			}
			SingletonEvent<EmbarkEvent>.Instance.Reset();
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00175B6C File Offset: 0x00173D6C
		public static void Send(XRLGame Game, EmbarkInfo Info, string EventID, ref object Element)
		{
			SingletonEvent<EmbarkEvent>.Instance.Info = Info;
			SingletonEvent<EmbarkEvent>.Instance.EventID = EventID;
			SingletonEvent<EmbarkEvent>.Instance.Element = Element;
			if (Game != null)
			{
				Game.HandleEvent(SingletonEvent<EmbarkEvent>.Instance, true);
			}
			foreach (IGameStateSingleton gameStateSingleton in Info.GameStateSingletons)
			{
				gameStateSingleton.HandleEvent(SingletonEvent<EmbarkEvent>.Instance);
			}
			Element = SingletonEvent<EmbarkEvent>.Instance.Element;
			SingletonEvent<EmbarkEvent>.Instance.Reset();
		}

		// Token: 0x04002433 RID: 9267
		public EmbarkInfo Info;

		// Token: 0x04002434 RID: 9268
		public string EventID;

		// Token: 0x04002435 RID: 9269
		public object Element;
	}
}
