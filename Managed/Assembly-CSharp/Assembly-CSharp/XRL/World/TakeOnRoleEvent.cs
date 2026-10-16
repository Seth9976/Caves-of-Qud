using System;

namespace XRL.World
{
	// Token: 0x02000865 RID: 2149
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class TakeOnRoleEvent : PooledEvent<TakeOnRoleEvent>
	{
		// Token: 0x06005585 RID: 21893 RVA: 0x00198B5C File Offset: 0x00196D5C
		public override int GetCascadeLevel()
		{
			return TakeOnRoleEvent.CascadeLevel;
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x00198B63 File Offset: 0x00196D63
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x00198B6C File Offset: 0x00196D6C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Role = null;
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x00198B84 File Offset: 0x00196D84
		public static void Send(GameObject Object, string Role)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("TakeOnRole"))
			{
				Event @event = Event.New("TakeOnRole", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Role", Role);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<TakeOnRoleEvent>.ID, TakeOnRoleEvent.CascadeLevel))
			{
				TakeOnRoleEvent takeOnRoleEvent = PooledEvent<TakeOnRoleEvent>.FromPool();
				takeOnRoleEvent.Object = Object;
				takeOnRoleEvent.Role = Role;
				flag = Object.HandleEvent(takeOnRoleEvent);
			}
		}

		// Token: 0x04002B1A RID: 11034
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002B1B RID: 11035
		public GameObject Object;

		// Token: 0x04002B1C RID: 11036
		public string Role;
	}
}
