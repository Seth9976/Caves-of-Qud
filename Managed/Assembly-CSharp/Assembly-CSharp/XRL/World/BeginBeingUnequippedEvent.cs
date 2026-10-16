using System;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x020006A8 RID: 1704
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeginBeingUnequippedEvent : PooledEvent<BeginBeingUnequippedEvent>
	{
		// Token: 0x0600488E RID: 18574 RVA: 0x0016CA13 File Offset: 0x0016AC13
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x0016CA1C File Offset: 0x0016AC1C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Equipper = null;
			this.Actor = null;
			this.BodyPart = null;
			this.Silent = false;
			this.Forced = false;
			this.SemiForced = false;
			this.DestroyOnUnequipDeclined = false;
			this.AutoEquipTry = 0;
			this.FailureMessage = null;
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x0016CA78 File Offset: 0x0016AC78
		public static bool Check(GameObject Object, ref string FailureMessage, ref bool DestroyOnUnequipDeclined, GameObject Equipper = null, GameObject Actor = null, BodyPart BodyPart = null, bool Silent = false, bool Forced = false, bool SemiForced = false, int AutoEquipTry = 0)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeginBeingUnequipped"))
			{
				Event @event = Event.New("BeginBeingUnequipped", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Equipper", Equipper);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("BodyPart", BodyPart);
				@event.SetParameter("AutoEquipTry", AutoEquipTry);
				@event.SetParameter("FailureMessage", FailureMessage);
				@event.SetFlag("Silent", Silent);
				@event.SetFlag("Forced", Forced);
				@event.SetFlag("SemiForced", SemiForced);
				@event.SetFlag("DestroyOnUnequipDeclined", DestroyOnUnequipDeclined);
				flag = Object.FireEvent(@event);
				FailureMessage = @event.GetStringParameter("FailureMessage", null);
				DestroyOnUnequipDeclined = @event.HasFlag("DestroyOnUnequipDeclined");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<BeginBeingUnequippedEvent>.ID, MinEvent.CascadeLevel))
			{
				BeginBeingUnequippedEvent beginBeingUnequippedEvent = PooledEvent<BeginBeingUnequippedEvent>.FromPool();
				beginBeingUnequippedEvent.Object = Object;
				beginBeingUnequippedEvent.Equipper = Equipper;
				beginBeingUnequippedEvent.Actor = Actor;
				beginBeingUnequippedEvent.BodyPart = BodyPart;
				beginBeingUnequippedEvent.AutoEquipTry = AutoEquipTry;
				beginBeingUnequippedEvent.FailureMessage = FailureMessage;
				beginBeingUnequippedEvent.Silent = Silent;
				beginBeingUnequippedEvent.Forced = Forced;
				beginBeingUnequippedEvent.SemiForced = SemiForced;
				beginBeingUnequippedEvent.DestroyOnUnequipDeclined = DestroyOnUnequipDeclined;
				flag = Object.HandleEvent(beginBeingUnequippedEvent);
				FailureMessage = beginBeingUnequippedEvent.FailureMessage;
				DestroyOnUnequipDeclined = beginBeingUnequippedEvent.DestroyOnUnequipDeclined;
			}
			return flag;
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x0016CBF2 File Offset: 0x0016ADF2
		public void AddFailureMessage(string Message)
		{
			if (this.FailureMessage.IsNullOrEmpty())
			{
				this.FailureMessage = Message;
				return;
			}
			if (!this.FailureMessage.Contains(Message))
			{
				this.FailureMessage = this.FailureMessage + " " + Message;
			}
		}

		// Token: 0x040022BE RID: 8894
		public GameObject Object;

		// Token: 0x040022BF RID: 8895
		public GameObject Equipper;

		// Token: 0x040022C0 RID: 8896
		public GameObject Actor;

		// Token: 0x040022C1 RID: 8897
		public BodyPart BodyPart;

		// Token: 0x040022C2 RID: 8898
		public bool Silent;

		// Token: 0x040022C3 RID: 8899
		public bool Forced;

		// Token: 0x040022C4 RID: 8900
		public bool SemiForced;

		// Token: 0x040022C5 RID: 8901
		public bool DestroyOnUnequipDeclined;

		// Token: 0x040022C6 RID: 8902
		public int AutoEquipTry;

		// Token: 0x040022C7 RID: 8903
		public string FailureMessage;
	}
}
