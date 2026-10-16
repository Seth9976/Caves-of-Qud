using System;

namespace XRL.World
{
	// Token: 0x020007EE RID: 2030
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class IsConversationallyResponsiveEvent : PooledEvent<IsConversationallyResponsiveEvent>
	{
		// Token: 0x06005182 RID: 20866 RVA: 0x00189DE6 File Offset: 0x00187FE6
		public override int GetCascadeLevel()
		{
			return IsConversationallyResponsiveEvent.CascadeLevel;
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x00189DED File Offset: 0x00187FED
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x00189DF6 File Offset: 0x00187FF6
		public override void Reset()
		{
			base.Reset();
			this.Speaker = null;
			this.Actor = null;
			this.Physical = false;
			this.Mental = false;
			this.Message = null;
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x00189E24 File Offset: 0x00188024
		public static bool Check(GameObject Speaker, GameObject Actor, out string Message, bool Physical = false, bool Mental = false)
		{
			Message = null;
			bool flag = true;
			if (flag && GameObject.Validate(ref Speaker) && Speaker.HasRegisteredEvent("IsConversationallyResponsive"))
			{
				Event @event = Event.New("IsConversationallyResponsive", 0, 0, 0);
				@event.SetParameter("Speaker", Speaker);
				@event.SetParameter("Actor", Actor);
				@event.SetFlag("Physical", Physical);
				@event.SetFlag("Mental", Mental);
				@event.SetParameter("Message", Message);
				flag = Speaker.FireEvent(@event);
				Message = @event.GetStringParameter("Message", null);
			}
			if (flag && GameObject.Validate(ref Speaker) && Speaker.WantEvent(PooledEvent<IsConversationallyResponsiveEvent>.ID, IsConversationallyResponsiveEvent.CascadeLevel))
			{
				IsConversationallyResponsiveEvent isConversationallyResponsiveEvent = PooledEvent<IsConversationallyResponsiveEvent>.FromPool();
				isConversationallyResponsiveEvent.Speaker = Speaker;
				isConversationallyResponsiveEvent.Actor = Actor;
				isConversationallyResponsiveEvent.Physical = Physical;
				isConversationallyResponsiveEvent.Mental = Mental;
				isConversationallyResponsiveEvent.Message = Message;
				flag = Speaker.HandleEvent(isConversationallyResponsiveEvent);
				Message = isConversationallyResponsiveEvent.Message;
			}
			return flag;
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x00189F10 File Offset: 0x00188110
		public static bool Check(GameObject Speaker, GameObject Actor, bool Physical = false, bool Mental = false)
		{
			string text;
			return IsConversationallyResponsiveEvent.Check(Speaker, Actor, out text, Physical, Mental);
		}

		// Token: 0x040028DE RID: 10462
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028DF RID: 10463
		public GameObject Speaker;

		// Token: 0x040028E0 RID: 10464
		public GameObject Actor;

		// Token: 0x040028E1 RID: 10465
		public bool Physical;

		// Token: 0x040028E2 RID: 10466
		public bool Mental;

		// Token: 0x040028E3 RID: 10467
		public string Message;
	}
}
