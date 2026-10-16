using System;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000847 RID: 2119
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class ReplaceThrownWeaponEvent : PooledEvent<ReplaceThrownWeaponEvent>
	{
		// Token: 0x06005413 RID: 21523 RVA: 0x00191094 File Offset: 0x0018F294
		public override int GetCascadeLevel()
		{
			return ReplaceThrownWeaponEvent.CascadeLevel;
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0019109B File Offset: 0x0018F29B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x001910A4 File Offset: 0x0018F2A4
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.PreviouslyEquipped = null;
			this.Part = null;
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x001910C4 File Offset: 0x0018F2C4
		public bool Equip(GameObject Object, bool Silent = false)
		{
			Event @event = Event.New("CommandEquipObject", 0, 0, 0);
			@event.SetParameter("Object", Object);
			@event.SetParameter("BodyPart", this.Part);
			@event.SetSilent(Silent);
			return this.Actor.FireEvent(@event);
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x00191114 File Offset: 0x0018F314
		public bool EquipOrDestroy(GameObject Object, bool Silent = false)
		{
			Event @event = Event.New("CommandEquipObject", 0, 0, 0);
			@event.SetParameter("Object", Object);
			@event.SetParameter("BodyPart", this.Part);
			@event.SetSilent(Silent);
			bool flag = this.Actor.FireEvent(@event);
			if (!flag)
			{
				Object.Obliterate(null, false, null);
			}
			return flag;
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x00191170 File Offset: 0x0018F370
		public static bool Check(GameObject Actor, GameObject PreviouslyEquipped, BodyPart Part)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("ReplaceThrownWeapon"))
			{
				Event @event = Event.New("ReplaceThrownWeapon", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("PreviouslyEquipped", PreviouslyEquipped);
				@event.SetParameter("Part", Part);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<ReplaceThrownWeaponEvent>.ID, ReplaceThrownWeaponEvent.CascadeLevel))
			{
				ReplaceThrownWeaponEvent replaceThrownWeaponEvent = PooledEvent<ReplaceThrownWeaponEvent>.FromPool();
				replaceThrownWeaponEvent.Actor = Actor;
				replaceThrownWeaponEvent.PreviouslyEquipped = PreviouslyEquipped;
				replaceThrownWeaponEvent.Part = Part;
				flag = Actor.HandleEvent(replaceThrownWeaponEvent);
			}
			return flag;
		}

		// Token: 0x04002A3E RID: 10814
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002A3F RID: 10815
		public GameObject Actor;

		// Token: 0x04002A40 RID: 10816
		public GameObject PreviouslyEquipped;

		// Token: 0x04002A41 RID: 10817
		public BodyPart Part;
	}
}
