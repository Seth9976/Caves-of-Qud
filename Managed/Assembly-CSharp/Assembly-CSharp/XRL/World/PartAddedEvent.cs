using System;

namespace XRL.World
{
	// Token: 0x02000820 RID: 2080
	[GameEvent(Cascade = 0)]
	public class PartAddedEvent : PooledEvent<PartAddedEvent>
	{
		// Token: 0x060052E0 RID: 21216 RVA: 0x0018E1E4 File Offset: 0x0018C3E4
		public override int GetCascadeLevel()
		{
			return PartAddedEvent.CascadeLevel;
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x0018E1EB File Offset: 0x0018C3EB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x0018E1F4 File Offset: 0x0018C3F4
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Part = null;
			this.Type = null;
			this.Registration = false;
			this.Creation = false;
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0018E220 File Offset: 0x0018C420
		public static void Send(GameObject Actor, IPart Part, Type Type, bool Registration, bool Creation)
		{
			using (PartAddedEvent partAddedEvent = PooledEvent<PartAddedEvent>.FromPool())
			{
				partAddedEvent.Actor = Actor;
				partAddedEvent.Part = Part;
				partAddedEvent.Type = Type;
				partAddedEvent.Registration = Registration;
				partAddedEvent.Creation = Creation;
				Actor.HandleEvent(partAddedEvent);
				ComponentReflection.ExtensionRecord extensionRecord;
				if (ComponentReflection.PartExtensions.TryGetValue(Type, out extensionRecord))
				{
					extensionRecord.Add(Actor, Part, Registration, Creation);
				}
			}
		}

		// Token: 0x040029A9 RID: 10665
		public new static readonly int CascadeLevel;

		// Token: 0x040029AA RID: 10666
		public GameObject Actor;

		// Token: 0x040029AB RID: 10667
		public IPart Part;

		// Token: 0x040029AC RID: 10668
		public Type Type;

		// Token: 0x040029AD RID: 10669
		public bool Registration;

		// Token: 0x040029AE RID: 10670
		public bool Creation;
	}
}
