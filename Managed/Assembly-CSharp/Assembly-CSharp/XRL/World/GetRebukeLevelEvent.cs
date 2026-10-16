using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000780 RID: 1920
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Singleton)]
	public class GetRebukeLevelEvent : SingletonEvent<GetRebukeLevelEvent>
	{
		// Token: 0x06004EBE RID: 20158 RVA: 0x00181505 File Offset: 0x0017F705
		public override int GetCascadeLevel()
		{
			return GetRebukeLevelEvent.CascadeLevel;
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x0018150C File Offset: 0x0017F70C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x00181515 File Offset: 0x0017F715
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Level = 0;
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x00181534 File Offset: 0x0017F734
		public static int GetFor(GameObject Actor, GameObject Target)
		{
			SingletonEvent<GetRebukeLevelEvent>.Instance.Actor = Actor;
			SingletonEvent<GetRebukeLevelEvent>.Instance.Target = Target;
			SingletonEvent<GetRebukeLevelEvent>.Instance.Level = Actor.Stat("Level", 0);
			Actor.HandleEvent(SingletonEvent<GetRebukeLevelEvent>.Instance);
			int num = SingletonEvent<GetRebukeLevelEvent>.Instance.Level;
			bool flag;
			if (Actor == null)
			{
				flag = true;
			}
			else
			{
				GenotypeEntry genotypeEntry = Actor.genotypeEntry;
				bool? flag2;
				if (genotypeEntry == null)
				{
					flag2 = null;
				}
				else
				{
					List<string> skills = genotypeEntry.Skills;
					flag2 = ((skills != null) ? new bool?(skills.Contains("Persuasion_RebukeRobot")) : null);
				}
				bool? flag3 = flag2;
				bool flag4 = true;
				flag = !((flag3.GetValueOrDefault() == flag4) & (flag3 != null));
			}
			if (flag)
			{
				num -= 5;
			}
			return num;
		}

		// Token: 0x0400267C RID: 9852
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400267D RID: 9853
		public GameObject Actor;

		// Token: 0x0400267E RID: 9854
		public GameObject Target;

		// Token: 0x0400267F RID: 9855
		public int Level;
	}
}
