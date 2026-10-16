using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200073D RID: 1853
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetCyberneticsBehaviorDescriptionEvent : PooledEvent<GetCyberneticsBehaviorDescriptionEvent>
	{
		// Token: 0x06004CAC RID: 19628 RVA: 0x0017A7BC File Offset: 0x001789BC
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x0017A7C5 File Offset: 0x001789C5
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.BaseDescription = null;
			this.Description = null;
			this.ToAdd.Clear();
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0017A7ED File Offset: 0x001789ED
		public void Add(string Text)
		{
			if (this.ToAdd == null)
			{
				this.ToAdd = new List<string>();
			}
			if (!this.ToAdd.Contains(Text))
			{
				this.ToAdd.Add(Text);
			}
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x0017A81C File Offset: 0x00178A1C
		public static string GetFor(GameObject Object, string BaseDescription = null)
		{
			string text = BaseDescription;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetCyberneticsBehaviorDescription"))
			{
				Event @event = Event.New("GetCyberneticsBehaviorDescription", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("BaseDescription", BaseDescription);
				@event.SetParameter("Description", text);
				flag = Object.FireEvent(@event);
				text = @event.GetStringParameter("Description", null);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID, MinEvent.CascadeLevel))
			{
				GetCyberneticsBehaviorDescriptionEvent getCyberneticsBehaviorDescriptionEvent = PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.FromPool();
				getCyberneticsBehaviorDescriptionEvent.Object = Object;
				getCyberneticsBehaviorDescriptionEvent.BaseDescription = BaseDescription;
				getCyberneticsBehaviorDescriptionEvent.Description = text;
				flag = Object.HandleEvent(getCyberneticsBehaviorDescriptionEvent);
				text = getCyberneticsBehaviorDescriptionEvent.Description;
				if (getCyberneticsBehaviorDescriptionEvent.ToAdd != null)
				{
					foreach (string text2 in getCyberneticsBehaviorDescriptionEvent.ToAdd)
					{
						if (text == null)
						{
							text = "";
						}
						else if (text != "" && !text.EndsWith("\n"))
						{
							text += "\n";
						}
						text += text2;
					}
				}
			}
			return text;
		}

		// Token: 0x04002525 RID: 9509
		public GameObject Object;

		// Token: 0x04002526 RID: 9510
		public string BaseDescription;

		// Token: 0x04002527 RID: 9511
		public string Description;

		// Token: 0x04002528 RID: 9512
		public List<string> ToAdd = new List<string>();
	}
}
