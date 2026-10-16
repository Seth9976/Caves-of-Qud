using System;

namespace XRL.World
{
	// Token: 0x020006E8 RID: 1768
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CommandEvent : PooledEvent<CommandEvent>
	{
		// Token: 0x06004A3B RID: 19003 RVA: 0x00173BD5 File Offset: 0x00171DD5
		public override int GetCascadeLevel()
		{
			return CommandEvent.CascadeLevel;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x00173BDC File Offset: 0x00171DDC
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x00173BE5 File Offset: 0x00171DE5
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.TargetCell = null;
			this.Command = null;
			this.StandoffDistance = 0;
			this.Forced = false;
			this.Silent = false;
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x00173C20 File Offset: 0x00171E20
		public static bool Send(GameObject Actor, string Command, ref bool InterfaceExitRequested, GameObject Target = null, Cell TargetCell = null, int StandoffDistance = 0, GameObject Handler = null)
		{
			if (Handler == null)
			{
				Handler = Actor;
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent(Command))
			{
				Event @event = Event.New(Command, 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("Command", Command);
				@event.SetParameter("StandoffDistance", StandoffDistance);
				flag = Handler.FireEvent(@event);
				if (@event.InterfaceExitRequested())
				{
					InterfaceExitRequested = true;
				}
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<CommandEvent>.ID, CommandEvent.CascadeLevel))
			{
				CommandEvent commandEvent = PooledEvent<CommandEvent>.FromPool();
				commandEvent.Actor = Actor;
				commandEvent.Target = Target;
				commandEvent.TargetCell = TargetCell;
				commandEvent.Command = Command;
				commandEvent.StandoffDistance = StandoffDistance;
				flag = Handler.HandleEvent(commandEvent);
				if (commandEvent.InterfaceExitRequested())
				{
					InterfaceExitRequested = true;
				}
			}
			return flag;
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x00173D0C File Offset: 0x00171F0C
		public static bool Send(GameObject Actor, string Command, ref bool InterfaceExitRequested, GameObject Target = null, Cell TargetCell = null, int StandoffDistance = 0, bool Forced = false, bool Silent = false, GameObject Handler = null)
		{
			if (Handler == null)
			{
				Handler = Actor;
			}
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent(Command))
			{
				Event @event = Event.New(Command, 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("Command", Command);
				@event.SetParameter("StandoffDistance", StandoffDistance);
				@event.SetFlag("Forced", Forced);
				@event.SetSilent(Silent);
				flag = Handler.FireEvent(@event);
				if (@event.InterfaceExitRequested())
				{
					InterfaceExitRequested = true;
				}
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<CommandEvent>.ID, CommandEvent.CascadeLevel))
			{
				CommandEvent commandEvent = PooledEvent<CommandEvent>.FromPool();
				commandEvent.Actor = Actor;
				commandEvent.Target = Target;
				commandEvent.TargetCell = TargetCell;
				commandEvent.Command = Command;
				commandEvent.StandoffDistance = StandoffDistance;
				commandEvent.Forced = Forced;
				commandEvent.Silent = Silent;
				flag = Handler.HandleEvent(commandEvent);
				if (commandEvent.InterfaceExitRequested())
				{
					InterfaceExitRequested = true;
				}
			}
			return flag;
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x00173E24 File Offset: 0x00172024
		public static bool Send(GameObject Actor, string Command, GameObject Target = null, Cell TargetCell = null, int StandoffDistance = 0, bool Forced = false, bool Silent = false, GameObject Handler = null)
		{
			bool flag = false;
			return CommandEvent.Send(Actor, Command, ref flag, Target, TargetCell, StandoffDistance, Forced, Silent, Handler);
		}

		// Token: 0x040023B8 RID: 9144
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040023B9 RID: 9145
		public GameObject Actor;

		// Token: 0x040023BA RID: 9146
		public GameObject Target;

		// Token: 0x040023BB RID: 9147
		public Cell TargetCell;

		// Token: 0x040023BC RID: 9148
		public string Command;

		// Token: 0x040023BD RID: 9149
		public int StandoffDistance;

		// Token: 0x040023BE RID: 9150
		public bool Forced;

		// Token: 0x040023BF RID: 9151
		public bool Silent;
	}
}
