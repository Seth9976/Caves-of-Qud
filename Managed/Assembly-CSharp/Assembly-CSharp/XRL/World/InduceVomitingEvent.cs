using System;
using System.Text;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x020007E7 RID: 2023
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class InduceVomitingEvent : PooledEvent<InduceVomitingEvent>
	{
		// Token: 0x0600514D RID: 20813 RVA: 0x0018921E File Offset: 0x0018741E
		public override int GetCascadeLevel()
		{
			return InduceVomitingEvent.CascadeLevel;
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x00189225 File Offset: 0x00187425
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x0018922E File Offset: 0x0018742E
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.MessageHolder = null;
			this.Vomited = false;
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x0018924C File Offset: 0x0018744C
		public void Message(string Text)
		{
			if (this.MessageHolder != null)
			{
				this.MessageHolder.Compound(Text);
				return;
			}
			if (GameObject.Validate(ref this.Object) && this.Object.IsPlayer())
			{
				Popup.Show(Text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x0018929C File Offset: 0x0018749C
		public static bool Send(GameObject Object, ref bool InterfaceExitRequested, StringBuilder MessageHolder = null)
		{
			bool flag = true;
			bool flag2 = false;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("InduceVomiting"))
			{
				Event @event = Event.New("InduceVomiting", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("MessageHolder", MessageHolder);
				@event.SetFlag("Vomited", flag2);
				@event.SetFlag("InterfaceExitRequested", InterfaceExitRequested);
				flag = Object.FireEvent(@event);
				flag2 = @event.HasFlag("Vomited");
				InterfaceExitRequested = @event.HasFlag("InterfaceExitRequested");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<InduceVomitingEvent>.ID, InduceVomitingEvent.CascadeLevel))
			{
				InduceVomitingEvent induceVomitingEvent = PooledEvent<InduceVomitingEvent>.FromPool();
				induceVomitingEvent.Object = Object;
				induceVomitingEvent.MessageHolder = MessageHolder;
				induceVomitingEvent.Vomited = flag2;
				induceVomitingEvent.InterfaceExit = InterfaceExitRequested;
				flag = Object.HandleEvent(induceVomitingEvent);
				flag2 = induceVomitingEvent.Vomited;
				InterfaceExitRequested = induceVomitingEvent.InterfaceExit;
			}
			return flag2;
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x00189384 File Offset: 0x00187584
		public static bool Send(GameObject Object, StringBuilder MessageHolder = null, IEvent FromEvent = null)
		{
			bool flag = FromEvent != null && FromEvent.InterfaceExitRequested();
			bool flag2 = InduceVomitingEvent.Send(Object, ref flag, MessageHolder);
			if (flag)
			{
				FromEvent.RequestInterfaceExit();
			}
			return flag2;
		}

		// Token: 0x040028B8 RID: 10424
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028B9 RID: 10425
		public GameObject Object;

		// Token: 0x040028BA RID: 10426
		public StringBuilder MessageHolder;

		// Token: 0x040028BB RID: 10427
		public bool Vomited;
	}
}
