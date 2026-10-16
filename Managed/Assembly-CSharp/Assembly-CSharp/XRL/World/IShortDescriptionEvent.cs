using System;
using System.Text;

namespace XRL.World
{
	// Token: 0x020007DC RID: 2012
	[GameEvent(Base = true)]
	public abstract class IShortDescriptionEvent : MinEvent
	{
		// Token: 0x0600510C RID: 20748 RVA: 0x0018847E File Offset: 0x0018667E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x00188488 File Offset: 0x00186688
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Base.Clear();
			this.Prefix.Clear();
			this.Infix.Clear();
			this.Postfix.Clear();
			this.Context = null;
			this.AsIfKnown = false;
			this.NoStatus = false;
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x001884E8 File Offset: 0x001866E8
		public void ApplyTo(IShortDescriptionEvent E)
		{
			E.Object = this.Object;
			if (E.Base == null)
			{
				E.Base = new StringBuilder(512);
			}
			E.Base.Clear().Append(this.Base);
			if (E.Prefix == null)
			{
				E.Prefix = new StringBuilder(512);
			}
			E.Prefix.Clear().Append(this.Prefix);
			if (E.Infix == null)
			{
				E.Infix = new StringBuilder(512);
			}
			E.Infix.Clear().Append(this.Infix);
			if (E.Postfix == null)
			{
				E.Postfix = new StringBuilder(512);
			}
			E.Postfix.Clear().Append(this.Postfix);
			E.Context = this.Context;
			E.AsIfKnown = this.AsIfKnown;
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x001885D8 File Offset: 0x001867D8
		public bool Process(GameObject Handler, IShortDescriptionEvent ParentEvent = null)
		{
			bool flag = false;
			try
			{
				string registeredEventID = this.GetRegisteredEventID();
				if (!string.IsNullOrEmpty(registeredEventID) && Handler.HasRegisteredEvent(registeredEventID))
				{
					if (!flag && ParentEvent != null)
					{
						ParentEvent.ApplyTo(this);
						flag = true;
					}
					string text = this.Base.ToString();
					string text2 = this.Prefix.ToString();
					string text3 = this.Infix.ToString();
					string text4 = this.Postfix.ToString();
					Event @event = Event.New(registeredEventID, 0, 0, 0);
					@event.SetParameter("Object", this.Object);
					@event.SetParameter("Handler", Handler);
					@event.SetParameter("ShortDescription", text);
					@event.SetParameter("BaseBuilder", this.Base);
					@event.SetParameter("Prefix", text2);
					@event.SetParameter("PrefixBuilder", this.Prefix);
					@event.SetParameter("Infix", text3);
					@event.SetParameter("InfixBuilder", this.Infix);
					@event.SetParameter("Postfix", text4);
					@event.SetParameter("PostfixBuilder", this.Postfix);
					@event.SetParameter("Context", this.Context);
					@event.SetFlag("AsIfKnown", this.AsIfKnown);
					bool flag2 = Handler.FireEvent(@event);
					string stringParameter = @event.GetStringParameter("Prefix", null);
					if (stringParameter != text2)
					{
						this.Prefix.Clear().Append(stringParameter);
					}
					string stringParameter2 = @event.GetStringParameter("ShortDescription", null);
					if (stringParameter2 != text)
					{
						this.Base.Clear().Append(stringParameter2);
					}
					string stringParameter3 = @event.GetStringParameter("Infix", null);
					if (stringParameter3 != text3)
					{
						this.Infix.Clear().Append(stringParameter3);
					}
					string stringParameter4 = @event.GetStringParameter("Postfix", null);
					if (stringParameter4 != text4)
					{
						this.Postfix.Clear().Append(stringParameter4);
					}
					if (!flag2)
					{
						return false;
					}
				}
				if (Handler.WantEvent(base.GetID(), this.GetCascadeLevel()))
				{
					if (!flag && ParentEvent != null)
					{
						ParentEvent.ApplyTo(this);
						flag = true;
					}
					if (!Handler.HandleEvent(this))
					{
						return false;
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.ApplyTo(ParentEvent);
				}
			}
			return true;
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x00188840 File Offset: 0x00186A40
		public bool Understood()
		{
			return this.AsIfKnown || this.Object.Understood();
		}

		// Token: 0x06005111 RID: 20753
		public abstract string GetRegisteredEventID();

		// Token: 0x04002880 RID: 10368
		public GameObject Object;

		// Token: 0x04002881 RID: 10369
		public StringBuilder Base = new StringBuilder(512);

		// Token: 0x04002882 RID: 10370
		public StringBuilder Prefix = new StringBuilder(512);

		// Token: 0x04002883 RID: 10371
		public StringBuilder Infix = new StringBuilder(512);

		// Token: 0x04002884 RID: 10372
		public StringBuilder Postfix = new StringBuilder(512);

		// Token: 0x04002885 RID: 10373
		public string Context;

		// Token: 0x04002886 RID: 10374
		public bool AsIfKnown;

		// Token: 0x04002887 RID: 10375
		public bool NoStatus;
	}
}
