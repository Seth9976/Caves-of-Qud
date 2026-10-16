using System;

namespace XRL.World
{
	// Token: 0x0200078B RID: 1931
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetSlotsRequiredEvent : PooledEvent<GetSlotsRequiredEvent>
	{
		// Token: 0x06004F08 RID: 20232 RVA: 0x00182498 File Offset: 0x00180698
		public override int GetCascadeLevel()
		{
			return GetSlotsRequiredEvent.CascadeLevel;
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x0018249F File Offset: 0x0018069F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x001824A8 File Offset: 0x001806A8
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.SlotType = null;
			this.Base = 0;
			this.Increases = 0;
			this.Decreases = 0;
			this.AllowReduction = false;
			this.CanBeTooSmall = false;
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x001824E8 File Offset: 0x001806E8
		public static int GetFor(GameObject Object, GameObject Actor, string SlotType, int Base = 1, int Increases = 0, int Decreases = 0, bool AllowReduction = true, bool CanBeTooSmall = false)
		{
			bool flag = true;
			bool flag2 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetSlotsRequiredEvent>.ID, GetSlotsRequiredEvent.CascadeLevel);
			bool flag3 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetSlotsRequiredEvent>.ID, GetSlotsRequiredEvent.CascadeLevel);
			int num;
			if (flag2 || flag3)
			{
				GetSlotsRequiredEvent getSlotsRequiredEvent = PooledEvent<GetSlotsRequiredEvent>.FromPool();
				getSlotsRequiredEvent.Object = Object;
				getSlotsRequiredEvent.Actor = Actor;
				getSlotsRequiredEvent.SlotType = SlotType;
				getSlotsRequiredEvent.Base = Base;
				getSlotsRequiredEvent.Increases = Increases;
				getSlotsRequiredEvent.Decreases = Decreases;
				getSlotsRequiredEvent.AllowReduction = AllowReduction;
				getSlotsRequiredEvent.CanBeTooSmall = CanBeTooSmall;
				if (flag2 && GameObject.Validate(ref Object) && (!flag3 || Object.Equipped != Actor))
				{
					flag = Object.HandleEvent(getSlotsRequiredEvent);
				}
				if (flag && flag3 && GameObject.Validate(ref Actor))
				{
					flag = Actor.HandleEvent(getSlotsRequiredEvent);
				}
				num = getSlotsRequiredEvent.Base;
				if (getSlotsRequiredEvent.Increases > 0)
				{
					num <<= getSlotsRequiredEvent.Increases;
				}
				if (getSlotsRequiredEvent.Decreases > 0 && getSlotsRequiredEvent.AllowReduction)
				{
					num >>= getSlotsRequiredEvent.Decreases;
				}
				if (!getSlotsRequiredEvent.CanBeTooSmall && num < 1)
				{
					num = 1;
				}
			}
			else
			{
				num = Base;
				if (Increases > 0)
				{
					num <<= Increases;
				}
				if (Decreases > 0 && AllowReduction)
				{
					num >>= Decreases;
				}
				if (!CanBeTooSmall && num < 1)
				{
					num = 1;
				}
			}
			return num;
		}

		// Token: 0x040026C0 RID: 9920
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040026C1 RID: 9921
		public GameObject Object;

		// Token: 0x040026C2 RID: 9922
		public GameObject Actor;

		// Token: 0x040026C3 RID: 9923
		public string SlotType;

		// Token: 0x040026C4 RID: 9924
		public int Base;

		// Token: 0x040026C5 RID: 9925
		public int Increases;

		// Token: 0x040026C6 RID: 9926
		public int Decreases;

		// Token: 0x040026C7 RID: 9927
		public bool AllowReduction;

		// Token: 0x040026C8 RID: 9928
		public bool CanBeTooSmall;
	}
}
