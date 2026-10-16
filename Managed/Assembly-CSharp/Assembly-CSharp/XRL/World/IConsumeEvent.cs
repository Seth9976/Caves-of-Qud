using System;

namespace XRL.World
{
	// Token: 0x020007C2 RID: 1986
	[GameEvent(Base = true, Cascade = 17)]
	public abstract class IConsumeEvent : MinEvent
	{
		// Token: 0x0600505D RID: 20573 RVA: 0x001867AC File Offset: 0x001849AC
		public override int GetCascadeLevel()
		{
			return IConsumeEvent.CascadeLevel;
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x001867B3 File Offset: 0x001849B3
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x001867BC File Offset: 0x001849BC
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Subject = null;
			this.Object = null;
			this.Eat = false;
			this.Drink = false;
			this.Inject = false;
			this.Inhale = false;
			this.Absorb = false;
			this.Voluntary = false;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06005060 RID: 20576 RVA: 0x0018680E File Offset: 0x00184A0E
		public bool Ingest
		{
			get
			{
				return this.Eat || this.Drink;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x00186820 File Offset: 0x00184A20
		public bool External
		{
			get
			{
				return this.Actor != this.Subject;
			}
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00186834 File Offset: 0x00184A34
		public static bool DispatchAll<T>(T E) where T : IConsumeEvent
		{
			if (!E.Actor.HandleEvent(E))
			{
				return false;
			}
			if (E.Subject != null && E.Subject != E.Actor && !E.Subject.HandleEvent(E))
			{
				return false;
			}
			GameObject @object = E.Object;
			return @object == null || @object.HandleEvent(E);
		}

		// Token: 0x040027D0 RID: 10192
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040027D1 RID: 10193
		public GameObject Actor;

		// Token: 0x040027D2 RID: 10194
		public GameObject Subject;

		// Token: 0x040027D3 RID: 10195
		public GameObject Object;

		// Token: 0x040027D4 RID: 10196
		public bool Eat;

		// Token: 0x040027D5 RID: 10197
		public bool Drink;

		// Token: 0x040027D6 RID: 10198
		public bool Inject;

		// Token: 0x040027D7 RID: 10199
		public bool Inhale;

		// Token: 0x040027D8 RID: 10200
		public bool Absorb;

		// Token: 0x040027D9 RID: 10201
		public bool Voluntary;
	}
}
