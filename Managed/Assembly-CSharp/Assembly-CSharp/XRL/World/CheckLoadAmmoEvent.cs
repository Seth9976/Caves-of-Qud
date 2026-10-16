using System;

namespace XRL.World
{
	// Token: 0x020006DC RID: 1756
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckLoadAmmoEvent : PooledEvent<CheckLoadAmmoEvent>
	{
		// Token: 0x060049FE RID: 18942 RVA: 0x00172FE6 File Offset: 0x001711E6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x00172FEF File Offset: 0x001711EF
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Message = null;
			this.ActivePartsIgnoreSubject = false;
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x00173014 File Offset: 0x00171214
		public static bool Check(GameObject Object, GameObject Actor, out string Message, bool ActivePartsIgnoreSubject = false)
		{
			Message = null;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CheckLoadAmmo"))
			{
				Event @event = Event.New("CheckLoadAmmo", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Loader", Actor);
				@event.SetParameter("Message", Message);
				@event.SetFlag("ActivePartsIgnoreSubject", ActivePartsIgnoreSubject);
				flag = Object.FireEvent(@event);
				Message = @event.GetStringParameter("Message", null);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CheckLoadAmmoEvent>.ID, MinEvent.CascadeLevel))
			{
				CheckLoadAmmoEvent checkLoadAmmoEvent = PooledEvent<CheckLoadAmmoEvent>.FromPool();
				checkLoadAmmoEvent.Object = Object;
				checkLoadAmmoEvent.Actor = Actor;
				checkLoadAmmoEvent.Message = Message;
				checkLoadAmmoEvent.ActivePartsIgnoreSubject = ActivePartsIgnoreSubject;
				flag = Object.HandleEvent(checkLoadAmmoEvent);
				Message = checkLoadAmmoEvent.Message;
			}
			return flag;
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x001730F8 File Offset: 0x001712F8
		public static bool Check(GameObject Object, GameObject Actor, bool ActivePartsIgnoreSubject = false)
		{
			string text;
			return CheckLoadAmmoEvent.Check(Object, Actor, out text, ActivePartsIgnoreSubject);
		}

		// Token: 0x0400238C RID: 9100
		public GameObject Object;

		// Token: 0x0400238D RID: 9101
		public GameObject Actor;

		// Token: 0x0400238E RID: 9102
		public string Message;

		// Token: 0x0400238F RID: 9103
		public bool ActivePartsIgnoreSubject;
	}
}
