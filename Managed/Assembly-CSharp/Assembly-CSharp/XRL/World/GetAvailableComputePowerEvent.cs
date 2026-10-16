using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200072C RID: 1836
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Singleton)]
	public class GetAvailableComputePowerEvent : SingletonEvent<GetAvailableComputePowerEvent>
	{
		// Token: 0x06004C21 RID: 19489 RVA: 0x00178D65 File Offset: 0x00176F65
		public override int GetCascadeLevel()
		{
			return GetAvailableComputePowerEvent.CascadeLevel;
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x00178D6C File Offset: 0x00176F6C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x00178D75 File Offset: 0x00176F75
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Amount = 0;
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x00178D8B File Offset: 0x00176F8B
		public static int GetFor(GameObject Actor)
		{
			if (GameObject.Validate(ref Actor))
			{
				SingletonEvent<GetAvailableComputePowerEvent>.Instance.Actor = Actor;
				SingletonEvent<GetAvailableComputePowerEvent>.Instance.Amount = 0;
				Actor.HandleEvent(SingletonEvent<GetAvailableComputePowerEvent>.Instance);
				return SingletonEvent<GetAvailableComputePowerEvent>.Instance.Amount;
			}
			return 0;
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x00178DC4 File Offset: 0x00176FC4
		public static int GetFor(IActivePart Part)
		{
			int num = 0;
			if (Part.ActivePartHasMultipleSubjects())
			{
				List<GameObject> activePartSubjects = Part.GetActivePartSubjects();
				int i = 0;
				int count = activePartSubjects.Count;
				while (i < count)
				{
					num += GetAvailableComputePowerEvent.GetFor(activePartSubjects[i]);
					i++;
				}
			}
			else
			{
				GameObject activePartFirstSubject = Part.GetActivePartFirstSubject();
				if (activePartFirstSubject != null)
				{
					num += GetAvailableComputePowerEvent.GetFor(activePartFirstSubject);
				}
			}
			return num;
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x00178E20 File Offset: 0x00177020
		public static int AdjustUp(GameObject Actor, int Amount)
		{
			if (Amount != 0)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Actor);
				if (@for != 0)
				{
					Amount = Amount * (100 + @for) / 100;
				}
			}
			return Amount;
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x00178E48 File Offset: 0x00177048
		public static int AdjustUp(GameObject Actor, int Amount, float Factor)
		{
			if (Amount != 0 && Factor != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Actor);
				if (@for != 0)
				{
					Amount = (int)((float)Amount * (100f + (float)@for * Factor) / 100f);
				}
			}
			return Amount;
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x00178E84 File Offset: 0x00177084
		public static float AdjustUp(GameObject Actor, float Amount)
		{
			if (Amount != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Actor);
				if (@for != 0)
				{
					Amount = Amount * (float)(100 + @for) / 100f;
				}
			}
			return Amount;
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x00178EB4 File Offset: 0x001770B4
		public static float AdjustUp(GameObject Actor, float Amount, float Factor)
		{
			if (Amount != 0f && Factor != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Actor);
				if (@for != 0)
				{
					Amount = Amount * (100f + (float)@for * Factor) / 100f;
				}
			}
			return Amount;
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x00178EF0 File Offset: 0x001770F0
		public static int AdjustDown(GameObject Actor, int Amount, int FloorDivisor = 2)
		{
			if (Amount != 0)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Actor);
				if (@for != 0)
				{
					Amount = Math.Max(Amount * (100 - @for) / 100, Amount / FloorDivisor);
				}
			}
			return Amount;
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x00178F20 File Offset: 0x00177120
		public static int AdjustDown(GameObject Actor, int Amount, float Factor, int FloorDivisor = 2)
		{
			if (Amount != 0)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Actor);
				if (@for != 0)
				{
					Amount = Math.Max((int)((float)Amount * (100f - (float)@for * Factor) / 100f), Amount / FloorDivisor);
				}
			}
			return Amount;
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x00178F5C File Offset: 0x0017715C
		public static float AdjustDown(GameObject Actor, float Amount, int FloorDivisor = 2)
		{
			if (Amount != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Actor);
				if (@for != 0)
				{
					Amount = Math.Max(Amount * (float)(100 - @for) / 100f, Amount / (float)FloorDivisor);
				}
			}
			return Amount;
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x00178F94 File Offset: 0x00177194
		public static float AdjustDown(GameObject Actor, float Amount, float Factor, int FloorDivisor = 2)
		{
			if (Amount != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Actor);
				if (@for != 0)
				{
					Amount = Math.Max(Amount * (100f - (float)@for * Factor) / 100f, Amount / (float)FloorDivisor);
				}
			}
			return Amount;
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x00178FD4 File Offset: 0x001771D4
		public static int AdjustUp(IActivePart Part, int Amount)
		{
			if (Amount != 0)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Part);
				if (@for != 0)
				{
					Amount = Amount * (100 + @for) / 100;
				}
			}
			return Amount;
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x00178FFC File Offset: 0x001771FC
		public static int AdjustUp(IActivePart Part, int Amount, float Factor)
		{
			if (Amount != 0 && Factor != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Part);
				if (@for != 0)
				{
					Amount = (int)((float)Amount * (100f + (float)@for * Factor) / 100f);
				}
			}
			return Amount;
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x00179038 File Offset: 0x00177238
		public static float AdjustUp(IActivePart Part, float Amount)
		{
			if (Amount != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Part);
				if (@for != 0)
				{
					Amount = Amount * (float)(100 + @for) / 100f;
				}
			}
			return Amount;
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x00179068 File Offset: 0x00177268
		public static float AdjustUp(IActivePart Part, float Amount, float Factor)
		{
			if (Amount != 0f && Factor != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Part);
				if (@for != 0)
				{
					Amount = Amount * (100f + (float)@for * Factor) / 100f;
				}
			}
			return Amount;
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x001790A4 File Offset: 0x001772A4
		public static int AdjustDown(IActivePart Part, int Amount, int FloorDivisor = 2)
		{
			if (Amount != 0)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Part);
				if (@for != 0)
				{
					Amount = Math.Max(Amount * (100 - @for) / 100, Amount / FloorDivisor);
				}
			}
			return Amount;
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x001790D4 File Offset: 0x001772D4
		public static int AdjustDown(IActivePart Part, int Amount, float Factor, int FloorDivisor = 2)
		{
			if (Amount != 0)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Part);
				if (@for != 0)
				{
					Amount = Math.Max((int)((float)Amount * (100f - (float)@for * Factor) / 100f), Amount / FloorDivisor);
				}
			}
			return Amount;
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x00179110 File Offset: 0x00177310
		public static float AdjustDown(IActivePart Part, float Amount, float FloorDivisor = 2f)
		{
			if (Amount != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Part);
				if (@for != 0)
				{
					Amount = Math.Max(Amount * (float)(100 - @for) / 100f, Amount / FloorDivisor);
				}
			}
			return Amount;
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x00179148 File Offset: 0x00177348
		public static float AdjustDown(IActivePart Part, float Amount, float Factor, float FloorDivisor = 2f)
		{
			if (Amount != 0f)
			{
				int @for = GetAvailableComputePowerEvent.GetFor(Part);
				if (@for != 0)
				{
					Amount = Math.Max(Amount * (100f - (float)@for * Factor) / 100f, Amount / FloorDivisor);
				}
			}
			return Amount;
		}

		// Token: 0x040024D5 RID: 9429
		public new static readonly int CascadeLevel = 3;

		// Token: 0x040024D6 RID: 9430
		public GameObject Actor;

		// Token: 0x040024D7 RID: 9431
		public int Amount;
	}
}
