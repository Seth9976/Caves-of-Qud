using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006C1 RID: 1729
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanDrinkEvent : PooledEvent<CanDrinkEvent>
	{
		// Token: 0x06004949 RID: 18761 RVA: 0x001713B4 File Offset: 0x0016F5B4
		public override int GetCascadeLevel()
		{
			return CanDrinkEvent.CascadeLevel;
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x001713BB File Offset: 0x0016F5BB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x001713C4 File Offset: 0x0016F5C4
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Liquid = null;
			this.CanDrinkThis = false;
			this.CouldDrinkOther = false;
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x001713E8 File Offset: 0x0016F5E8
		public static void GetFor(GameObject Object, LiquidVolume Liquid, out bool CanDrinkThis, out bool CouldDrinkOther)
		{
			bool flag = true;
			CanDrinkThis = false;
			CouldDrinkOther = false;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanDrink"))
			{
				Event @event = Event.New("CanDrink", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Liquid", Liquid);
				@event.SetFlag("CanDrinkThis", CanDrinkThis);
				@event.SetFlag("CouldDrinkOther", CouldDrinkOther);
				flag = Object.FireEvent(@event);
				CanDrinkThis = @event.HasFlag("CanDrinkThis");
				CouldDrinkOther = @event.HasFlag("CouldDrinkOther");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CanDrinkEvent>.ID, CanDrinkEvent.CascadeLevel))
			{
				CanDrinkEvent canDrinkEvent = PooledEvent<CanDrinkEvent>.FromPool();
				canDrinkEvent.Object = Object;
				canDrinkEvent.Liquid = Liquid;
				canDrinkEvent.CanDrinkThis = CanDrinkThis;
				canDrinkEvent.CouldDrinkOther = CouldDrinkOther;
				flag = Object.HandleEvent(canDrinkEvent);
				CanDrinkThis = canDrinkEvent.CanDrinkThis;
				CouldDrinkOther = canDrinkEvent.CouldDrinkOther;
			}
		}

		// Token: 0x0400232E RID: 9006
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400232F RID: 9007
		public GameObject Object;

		// Token: 0x04002330 RID: 9008
		public LiquidVolume Liquid;

		// Token: 0x04002331 RID: 9009
		public bool CanDrinkThis;

		// Token: 0x04002332 RID: 9010
		public bool CouldDrinkOther;
	}
}
