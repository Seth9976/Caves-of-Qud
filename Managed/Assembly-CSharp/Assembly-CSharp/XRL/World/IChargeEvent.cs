using System;

namespace XRL.World
{
	// Token: 0x020007BF RID: 1983
	[GameEvent(Base = true, Cascade = 4)]
	public abstract class IChargeEvent : MinEvent
	{
		// Token: 0x0600504B RID: 20555 RVA: 0x00186446 File Offset: 0x00184646
		public override int GetCascadeLevel()
		{
			return IChargeEvent.CascadeLevel;
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0018644D File Offset: 0x0018464D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x00186458 File Offset: 0x00184658
		public override void Reset()
		{
			base.Reset();
			this.Source = null;
			this.Amount = 0;
			this.StartingAmount = 0;
			this.Multiple = 1;
			this.Pass = 0;
			this.GridMask = 0L;
			this.Forced = false;
			this.LiveOnly = false;
			this.IncludeTransient = true;
			this.IncludeBiological = true;
			this.PowerLoadLevel = null;
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600504E RID: 20558 RVA: 0x001864BE File Offset: 0x001846BE
		// (set) Token: 0x0600504F RID: 20559 RVA: 0x001864CD File Offset: 0x001846CD
		public int Used
		{
			get
			{
				return this.StartingAmount - this.Amount;
			}
			set
			{
				this.Amount = this.StartingAmount - value;
			}
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x001864E0 File Offset: 0x001846E0
		public virtual Event GenerateRegisteredEvent(string ID)
		{
			Event @event = Event.New(ID, 0, 0, 0);
			@event.SetParameter("Source", this.Source);
			@event.SetParameter("Charge", this.Amount);
			@event.SetParameter("StartingCharge", this.StartingAmount);
			@event.SetParameter("MultipleCharge", this.Multiple);
			@event.SetParameter("GridMask", this.GridMask);
			@event.SetFlag("Forced", this.Forced);
			@event.SetFlag("LiveOnly", this.LiveOnly);
			@event.SetFlag("IncludeTransient", this.IncludeTransient);
			@event.SetFlag("IncludeBiological", this.IncludeBiological);
			@event.SetParameter("PowerLoadLevel", (this.PowerLoadLevel == null) ? 100 : this.PowerLoadLevel.Value);
			return @event;
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x001865C8 File Offset: 0x001847C8
		public virtual void SyncFromRegisteredEvent(Event E, bool AllFields = false)
		{
			this.Amount = E.GetIntParameter("Charge", 0);
			if (AllFields)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Source");
				if (gameObjectParameter != null)
				{
					this.Source = gameObjectParameter;
				}
				this.StartingAmount = E.GetIntParameter("StartingCharge", 0);
				this.Multiple = E.GetIntParameter("MultipleCharge", 0);
				this.GridMask = (long)E.GetIntParameter("GridMask", 0);
				this.Forced = E.HasFlag("Forced");
				this.LiveOnly = E.HasFlag("LiveOnly");
				this.IncludeTransient = E.HasFlag("IncludeTransient");
				this.IncludeBiological = E.HasFlag("IncludeBiological");
				this.PowerLoadLevel = new int?(E.GetIntParameter("PowerLoadLevel", 0));
			}
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x00186695 File Offset: 0x00184895
		public bool SendRegisteredEvent(GameObject Object, string ID)
		{
			return !Object.HasRegisteredEvent(ID) || Object.FireEvent(this.GenerateRegisteredEvent(ID));
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x001866B0 File Offset: 0x001848B0
		public bool CheckRegisteredEvent(GameObject Object, string ID)
		{
			if (Object.HasRegisteredEvent(ID))
			{
				Event @event = this.GenerateRegisteredEvent(ID);
				bool flag = Object.FireEvent(@event);
				this.SyncFromRegisteredEvent(@event, false);
				return flag;
			}
			return true;
		}

		// Token: 0x040027C2 RID: 10178
		public new static readonly int CascadeLevel = 4;

		// Token: 0x040027C3 RID: 10179
		public GameObject Source;

		// Token: 0x040027C4 RID: 10180
		public int Amount;

		// Token: 0x040027C5 RID: 10181
		public int StartingAmount;

		// Token: 0x040027C6 RID: 10182
		public int Multiple = 1;

		// Token: 0x040027C7 RID: 10183
		public int Pass;

		// Token: 0x040027C8 RID: 10184
		public long GridMask;

		// Token: 0x040027C9 RID: 10185
		public bool Forced;

		// Token: 0x040027CA RID: 10186
		public bool LiveOnly;

		// Token: 0x040027CB RID: 10187
		public bool IncludeTransient = true;

		// Token: 0x040027CC RID: 10188
		public bool IncludeBiological = true;

		// Token: 0x040027CD RID: 10189
		public int? PowerLoadLevel;
	}
}
