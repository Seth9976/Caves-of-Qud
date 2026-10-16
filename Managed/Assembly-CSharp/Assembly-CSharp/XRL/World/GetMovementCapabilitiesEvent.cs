using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200076C RID: 1900
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 273)]
	public class GetMovementCapabilitiesEvent : MinEvent
	{
		// Token: 0x06004E30 RID: 20016 RVA: 0x0017F938 File Offset: 0x0017DB38
		public GetMovementCapabilitiesEvent()
		{
			this.ID = GetMovementCapabilitiesEvent.ID;
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x0017F977 File Offset: 0x0017DB77
		public override int GetCascadeLevel()
		{
			return GetMovementCapabilitiesEvent.CascadeLevel;
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x0017F97E File Offset: 0x0017DB7E
		public static int CountPool()
		{
			if (GetMovementCapabilitiesEvent.Pool != null)
			{
				return GetMovementCapabilitiesEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x0017F993 File Offset: 0x0017DB93
		public static void ResetPool()
		{
			while (GetMovementCapabilitiesEvent.PoolCounter > 0)
			{
				GetMovementCapabilitiesEvent.Pool[--GetMovementCapabilitiesEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x0017F9BB File Offset: 0x0017DBBB
		public static void ResetTo(ref GetMovementCapabilitiesEvent E)
		{
			MinEvent.ResetTo<GetMovementCapabilitiesEvent>(E, GetMovementCapabilitiesEvent.Pool, ref GetMovementCapabilitiesEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x0017F9D1 File Offset: 0x0017DBD1
		public static GetMovementCapabilitiesEvent FromPool()
		{
			return MinEvent.FromPool<GetMovementCapabilitiesEvent>(ref GetMovementCapabilitiesEvent.Pool, ref GetMovementCapabilitiesEvent.PoolCounter, 8192);
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x0017F9E7 File Offset: 0x0017DBE7
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x0017F9F0 File Offset: 0x0017DBF0
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Descriptions.Clear();
			this.Commands.Clear();
			this.Abilities.Clear();
			this.Order.Clear();
			this.IncludeAttacks = false;
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x0017FA40 File Offset: 0x0017DC40
		public void Add(string Description, string Command, int Order, ActivatedAbilityEntry Ability = null, bool IsAttack = false)
		{
			if (IsAttack && !this.IncludeAttacks)
			{
				return;
			}
			int num = -1;
			int i = 0;
			int count = this.Order.Count;
			while (i < count)
			{
				if (this.Order[i] > Order)
				{
					num = i;
					break;
				}
				if (this.Order[i] == Order && Description.CompareTo(this.Descriptions[i]) <= 0)
				{
					num = i;
					break;
				}
				i++;
			}
			if (Ability != null && !Ability.IsUsable)
			{
				if (IsAttack)
				{
					Description += " [attack]";
				}
				if (Ability.Toggleable)
				{
					if (Ability.ToggleState)
					{
						Description += " [toggled on]";
					}
					else
					{
						Description += " [toggled off]";
					}
				}
				Description = Description.Color("K");
			}
			else
			{
				if (IsAttack)
				{
					Description += " {{W|[attack]}}";
				}
				if (Ability != null && Ability.Toggleable)
				{
					if (Ability.ToggleState)
					{
						Description += " {{g|[toggled on]}}";
					}
					else
					{
						Description += " [toggled off]";
					}
				}
			}
			if (num == -1)
			{
				this.Descriptions.Add(Description);
				this.Commands.Add(Command);
				this.Abilities.Add(Ability);
				this.Order.Add(Order);
				return;
			}
			this.Descriptions.Insert(num, Description);
			this.Commands.Insert(num, Command);
			this.Abilities.Insert(num, Ability);
			this.Order.Insert(num, Order);
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x0017FBB8 File Offset: 0x0017DDB8
		public static GetMovementCapabilitiesEvent GetFor(GameObject Actor, bool IncludeAttacks = true)
		{
			GetMovementCapabilitiesEvent getMovementCapabilitiesEvent = null;
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(GetMovementCapabilitiesEvent.ID, GetMovementCapabilitiesEvent.CascadeLevel))
			{
				if (getMovementCapabilitiesEvent == null)
				{
					getMovementCapabilitiesEvent = GetMovementCapabilitiesEvent.FromPool();
				}
				getMovementCapabilitiesEvent.Actor = Actor;
				getMovementCapabilitiesEvent.IncludeAttacks = IncludeAttacks;
				Actor.HandleEvent(getMovementCapabilitiesEvent);
			}
			return getMovementCapabilitiesEvent;
		}

		// Token: 0x04002622 RID: 9762
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetMovementCapabilitiesEvent), null, new MinEvent.EventPoolCount(GetMovementCapabilitiesEvent.CountPool), new MinEvent.EventPoolReset(GetMovementCapabilitiesEvent.ResetPool));

		// Token: 0x04002623 RID: 9763
		public new static readonly int CascadeLevel = 273;

		// Token: 0x04002624 RID: 9764
		private static List<GetMovementCapabilitiesEvent> Pool;

		// Token: 0x04002625 RID: 9765
		private static int PoolCounter;

		// Token: 0x04002626 RID: 9766
		public GameObject Actor;

		// Token: 0x04002627 RID: 9767
		public List<string> Descriptions = new List<string>();

		// Token: 0x04002628 RID: 9768
		public List<string> Commands = new List<string>();

		// Token: 0x04002629 RID: 9769
		public List<ActivatedAbilityEntry> Abilities = new List<ActivatedAbilityEntry>();

		// Token: 0x0400262A RID: 9770
		public List<int> Order = new List<int>();

		// Token: 0x0400262B RID: 9771
		public bool IncludeAttacks;
	}
}
