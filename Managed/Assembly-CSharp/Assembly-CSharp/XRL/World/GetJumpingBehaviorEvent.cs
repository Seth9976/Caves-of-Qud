using System;

namespace XRL.World
{
	// Token: 0x0200075B RID: 1883
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetJumpingBehaviorEvent : PooledEvent<GetJumpingBehaviorEvent>
	{
		// Token: 0x06004DB8 RID: 19896 RVA: 0x0017DB43 File Offset: 0x0017BD43
		public override int GetCascadeLevel()
		{
			return GetJumpingBehaviorEvent.CascadeLevel;
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x0017DB4A File Offset: 0x0017BD4A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x0017DB54 File Offset: 0x0017BD54
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.AbilityName = null;
			this.Verb = null;
			this.ProviderKey = null;
			this.RangeModifier = 0;
			this.MinimumRange = 0;
			this.Priority = 0;
			this.CanJumpOverCreatures = false;
			this.Stats = null;
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x0017DBA8 File Offset: 0x0017BDA8
		public static void Retrieve(GameObject Actor, out int RangeModifier, out int MinimumRange, out string AbilityName, out string Verb, out string ProviderKey, out bool CanJumpOverCreatures, Templates.StatCollector Stats = null)
		{
			RangeModifier = 0;
			MinimumRange = 2;
			AbilityName = "Jump";
			Verb = "jump";
			ProviderKey = "Acrobatics_Jump";
			CanJumpOverCreatures = false;
			int num = 0;
			GetJumpingBehaviorEvent getJumpingBehaviorEvent = PooledEvent<GetJumpingBehaviorEvent>.FromPool();
			getJumpingBehaviorEvent.Actor = Actor;
			getJumpingBehaviorEvent.AbilityName = AbilityName;
			getJumpingBehaviorEvent.Verb = Verb;
			getJumpingBehaviorEvent.ProviderKey = ProviderKey;
			getJumpingBehaviorEvent.RangeModifier = RangeModifier;
			getJumpingBehaviorEvent.MinimumRange = MinimumRange;
			getJumpingBehaviorEvent.Priority = num;
			getJumpingBehaviorEvent.CanJumpOverCreatures = CanJumpOverCreatures;
			getJumpingBehaviorEvent.Stats = Stats;
			Actor.HandleEvent(getJumpingBehaviorEvent);
			AbilityName = getJumpingBehaviorEvent.AbilityName;
			Verb = getJumpingBehaviorEvent.Verb;
			ProviderKey = getJumpingBehaviorEvent.ProviderKey;
			RangeModifier = getJumpingBehaviorEvent.RangeModifier;
			MinimumRange = getJumpingBehaviorEvent.MinimumRange;
			CanJumpOverCreatures = getJumpingBehaviorEvent.CanJumpOverCreatures;
			num = getJumpingBehaviorEvent.Priority;
		}

		// Token: 0x040025B5 RID: 9653
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040025B6 RID: 9654
		public GameObject Actor;

		// Token: 0x040025B7 RID: 9655
		public string AbilityName;

		// Token: 0x040025B8 RID: 9656
		public string Verb;

		// Token: 0x040025B9 RID: 9657
		public string ProviderKey;

		// Token: 0x040025BA RID: 9658
		public int RangeModifier;

		// Token: 0x040025BB RID: 9659
		public int MinimumRange;

		// Token: 0x040025BC RID: 9660
		public int Priority;

		// Token: 0x040025BD RID: 9661
		public bool CanJumpOverCreatures;

		// Token: 0x040025BE RID: 9662
		public Templates.StatCollector Stats;
	}
}
