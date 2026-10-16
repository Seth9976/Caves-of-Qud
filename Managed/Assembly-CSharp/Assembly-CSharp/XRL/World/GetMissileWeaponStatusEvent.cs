using System;
using System.Text;
using Qud.UI;

namespace XRL.World
{
	// Token: 0x0200076A RID: 1898
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetMissileWeaponStatusEvent : PooledEvent<GetMissileWeaponStatusEvent>
	{
		// Token: 0x06004E26 RID: 20006 RVA: 0x0017F5FA File Offset: 0x0017D7FA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x0017F603 File Offset: 0x0017D803
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Items = null;
			this.Status = null;
			this.Override = null;
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x0017F628 File Offset: 0x0017D828
		public static void Send(GameObject Object, StringBuilder Items, MissileWeaponArea.MissileWeaponAreaWeaponStatus Status, IPart Override = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetMissileWeaponStatus"))
			{
				Event @event = Event.New("GetMissileWeaponStatus", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Items", Items);
				@event.SetParameter("Status", Status);
				@event.SetParameter("Override", Override);
				flag = Object.FireEvent(@event);
				Override = @event.GetParameter("Override") as IPart;
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetMissileWeaponStatusEvent>.ID, MinEvent.CascadeLevel))
			{
				GetMissileWeaponStatusEvent getMissileWeaponStatusEvent = PooledEvent<GetMissileWeaponStatusEvent>.FromPool();
				getMissileWeaponStatusEvent.Object = Object;
				getMissileWeaponStatusEvent.Items = Items;
				getMissileWeaponStatusEvent.Status = Status;
				getMissileWeaponStatusEvent.Override = Override;
				flag = Object.HandleEvent(getMissileWeaponStatusEvent);
				Override = getMissileWeaponStatusEvent.Override;
			}
		}

		// Token: 0x04002618 RID: 9752
		public GameObject Object;

		// Token: 0x04002619 RID: 9753
		public StringBuilder Items;

		// Token: 0x0400261A RID: 9754
		public MissileWeaponArea.MissileWeaponAreaWeaponStatus Status;

		// Token: 0x0400261B RID: 9755
		public IPart Override;
	}
}
