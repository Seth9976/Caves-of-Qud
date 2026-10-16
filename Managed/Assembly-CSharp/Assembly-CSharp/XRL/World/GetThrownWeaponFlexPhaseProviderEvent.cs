using System;

namespace XRL.World
{
	// Token: 0x02000796 RID: 1942
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetThrownWeaponFlexPhaseProviderEvent : PooledEvent<GetThrownWeaponFlexPhaseProviderEvent>
	{
		// Token: 0x06004F52 RID: 20306 RVA: 0x001833E0 File Offset: 0x001815E0
		public override int GetCascadeLevel()
		{
			return GetThrownWeaponFlexPhaseProviderEvent.CascadeLevel;
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x001833E7 File Offset: 0x001815E7
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x001833F0 File Offset: 0x001815F0
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Provider = null;
			this.Priority = 0;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x00183414 File Offset: 0x00181614
		public static IThrownWeaponFlexPhaseProvider GetFor(GameObject Object, GameObject Actor)
		{
			IThrownWeaponFlexPhaseProvider thrownWeaponFlexPhaseProvider = null;
			int num = 0;
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetThrownWeaponFlexPhaseProvider");
				bool flag3 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetThrownWeaponFlexPhaseProvider");
				if (flag2 || flag3)
				{
					Event @event = Event.New("GetThrownWeaponFlexPhaseProvider", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Provider", thrownWeaponFlexPhaseProvider);
					@event.SetParameter("Priority", num);
					flag = (!flag2 || Object.FireEvent(@event)) && (!flag3 || Actor.FireEvent(@event));
					thrownWeaponFlexPhaseProvider = @event.GetParameter("Provider") as IThrownWeaponFlexPhaseProvider;
					num = @event.GetIntParameter("Priority", 0);
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetThrownWeaponFlexPhaseProviderEvent>.ID, GetThrownWeaponFlexPhaseProviderEvent.CascadeLevel);
				bool flag5 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetThrownWeaponFlexPhaseProviderEvent>.ID, GetThrownWeaponFlexPhaseProviderEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					GetThrownWeaponFlexPhaseProviderEvent getThrownWeaponFlexPhaseProviderEvent = PooledEvent<GetThrownWeaponFlexPhaseProviderEvent>.FromPool();
					getThrownWeaponFlexPhaseProviderEvent.Object = Object;
					getThrownWeaponFlexPhaseProviderEvent.Actor = Actor;
					getThrownWeaponFlexPhaseProviderEvent.Provider = thrownWeaponFlexPhaseProvider;
					getThrownWeaponFlexPhaseProviderEvent.Priority = num;
					bool flag6 = (!flag4 || Object.HandleEvent(getThrownWeaponFlexPhaseProviderEvent)) && (!flag5 || Actor.HandleEvent(getThrownWeaponFlexPhaseProviderEvent));
					thrownWeaponFlexPhaseProvider = getThrownWeaponFlexPhaseProviderEvent.Provider;
					num = getThrownWeaponFlexPhaseProviderEvent.Priority;
				}
			}
			return thrownWeaponFlexPhaseProvider;
		}

		// Token: 0x04002701 RID: 9985
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002702 RID: 9986
		public GameObject Object;

		// Token: 0x04002703 RID: 9987
		public GameObject Actor;

		// Token: 0x04002704 RID: 9988
		public IThrownWeaponFlexPhaseProvider Provider;

		// Token: 0x04002705 RID: 9989
		public int Priority;
	}
}
