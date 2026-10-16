using System;

namespace XRL.World
{
	// Token: 0x02000801 RID: 2049
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class LoadAmmoEvent : PooledEvent<LoadAmmoEvent>
	{
		// Token: 0x060051FC RID: 20988 RVA: 0x0018B5A0 File Offset: 0x001897A0
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x0018B5A9 File Offset: 0x001897A9
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Projectile = null;
			this.LoadedAmmo = null;
			this.Message = null;
			this.ActivePartsIgnoreSubject = false;
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0018B5DC File Offset: 0x001897DC
		public static bool Check(GameObject Object, GameObject Actor, out GameObject Projectile, out GameObject LoadedAmmo, out string Message, bool ActivePartsIgnoreSubject = false)
		{
			Message = null;
			Projectile = null;
			LoadedAmmo = null;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("LoadAmmo"))
			{
				Event @event = Event.New("LoadAmmo", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Loader", Actor);
				@event.SetParameter("Ammo", Projectile);
				@event.SetParameter("LoadedAmmo", LoadedAmmo);
				@event.SetParameter("Message", Message);
				@event.SetFlag("ActivePartsIgnoreSubject", ActivePartsIgnoreSubject);
				flag = Object.FireEvent(@event);
				Projectile = @event.GetGameObjectParameter("Ammo");
				LoadedAmmo = @event.GetGameObjectParameter("LoadedAmmo");
				Message = @event.GetStringParameter("Message", null);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<LoadAmmoEvent>.ID, MinEvent.CascadeLevel))
			{
				LoadAmmoEvent loadAmmoEvent = PooledEvent<LoadAmmoEvent>.FromPool();
				loadAmmoEvent.Object = Object;
				loadAmmoEvent.Actor = Actor;
				loadAmmoEvent.Projectile = Projectile;
				loadAmmoEvent.LoadedAmmo = LoadedAmmo;
				loadAmmoEvent.Message = Message;
				loadAmmoEvent.ActivePartsIgnoreSubject = ActivePartsIgnoreSubject;
				flag = Object.HandleEvent(loadAmmoEvent);
				Projectile = loadAmmoEvent.Projectile;
				LoadedAmmo = loadAmmoEvent.LoadedAmmo;
				Message = loadAmmoEvent.Message;
			}
			return flag;
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x0018B72C File Offset: 0x0018992C
		public static bool Check(GameObject Object, GameObject Actor, out GameObject Projectile, bool ActivePartsIgnoreSubject = false)
		{
			GameObject gameObject;
			string text;
			return LoadAmmoEvent.Check(Object, Actor, out Projectile, out gameObject, out text, ActivePartsIgnoreSubject);
		}

		// Token: 0x04002923 RID: 10531
		public GameObject Object;

		// Token: 0x04002924 RID: 10532
		public GameObject Actor;

		// Token: 0x04002925 RID: 10533
		public GameObject Projectile;

		// Token: 0x04002926 RID: 10534
		public GameObject LoadedAmmo;

		// Token: 0x04002927 RID: 10535
		public string Message;

		// Token: 0x04002928 RID: 10536
		public bool ActivePartsIgnoreSubject;
	}
}
