using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000776 RID: 1910
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetPrecognitionRestoreGameStateEvent : PooledEvent<GetPrecognitionRestoreGameStateEvent>
	{
		// Token: 0x06004E81 RID: 20097 RVA: 0x00180BF9 File Offset: 0x0017EDF9
		public override int GetCascadeLevel()
		{
			return GetPrecognitionRestoreGameStateEvent.CascadeLevel;
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x00180C00 File Offset: 0x0017EE00
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x00180C09 File Offset: 0x0017EE09
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.GameState = null;
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x00180C1F File Offset: 0x0017EE1F
		public void Set(string Key, object Value)
		{
			if (this.GameState == null)
			{
				this.GameState = new Dictionary<string, object>();
			}
			this.GameState[Key] = Value;
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x00180C44 File Offset: 0x0017EE44
		public static Dictionary<string, object> GetFor(GameObject Object)
		{
			bool flag = true;
			Dictionary<string, object> dictionary = null;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetPrecognitionRestoreGameState"))
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
				}
				Event @event = Event.New("GetPrecognitionRestoreGameState", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("GameState", dictionary);
				flag = Object.FireEvent(@event);
				dictionary = @event.GetParameter("GameState") as Dictionary<string, object>;
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetPrecognitionRestoreGameStateEvent>.ID, GetPrecognitionRestoreGameStateEvent.CascadeLevel))
			{
				GetPrecognitionRestoreGameStateEvent getPrecognitionRestoreGameStateEvent = PooledEvent<GetPrecognitionRestoreGameStateEvent>.FromPool();
				getPrecognitionRestoreGameStateEvent.Object = Object;
				getPrecognitionRestoreGameStateEvent.GameState = dictionary;
				flag = Object.HandleEvent(getPrecognitionRestoreGameStateEvent);
				dictionary = getPrecognitionRestoreGameStateEvent.GameState;
			}
			return dictionary;
		}

		// Token: 0x04002653 RID: 9811
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002654 RID: 9812
		public GameObject Object;

		// Token: 0x04002655 RID: 9813
		public Dictionary<string, object> GameState;
	}
}
