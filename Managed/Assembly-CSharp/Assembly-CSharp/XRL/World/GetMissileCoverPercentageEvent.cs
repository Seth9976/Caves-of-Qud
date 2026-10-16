using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000766 RID: 1894
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetMissileCoverPercentageEvent : PooledEvent<GetMissileCoverPercentageEvent>
	{
		// Token: 0x06004E0B RID: 19979 RVA: 0x0017ECCB File Offset: 0x0017CECB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x0017ECD4 File Offset: 0x0017CED4
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Projectile = null;
			this.PenetrateCreatures = false;
			this.PenetrateWalls = false;
			this.Percentage = 0;
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x0017ED06 File Offset: 0x0017CF06
		public void MinPercentage(int Percentage)
		{
			if (this.Percentage < Percentage)
			{
				this.Percentage = Percentage;
			}
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x0017ED18 File Offset: 0x0017CF18
		public static int GetFor(GameObject Object, GameObject Actor = null, GameObject Projectile = null, bool PenetrateCreatures = false, bool PenetrateWalls = false)
		{
			int num = 0;
			if (GameObject.Validate(ref Object))
			{
				if (!PenetrateCreatures && !PenetrateWalls)
				{
					Render render = Object.Render;
					if (render != null && render.Occluding && Object.IsReal)
					{
						num = RuleSettings.DEFAULT_OCCLUDING_COVER_PERCENTAGE;
					}
				}
				if (Object.WantEvent(PooledEvent<GetMissileCoverPercentageEvent>.ID, MinEvent.CascadeLevel))
				{
					GetMissileCoverPercentageEvent getMissileCoverPercentageEvent = PooledEvent<GetMissileCoverPercentageEvent>.FromPool();
					getMissileCoverPercentageEvent.Object = Object;
					getMissileCoverPercentageEvent.Actor = Actor;
					getMissileCoverPercentageEvent.Projectile = Projectile;
					getMissileCoverPercentageEvent.PenetrateCreatures = PenetrateCreatures;
					getMissileCoverPercentageEvent.PenetrateWalls = PenetrateWalls;
					getMissileCoverPercentageEvent.Percentage = num;
					Object.HandleEvent(getMissileCoverPercentageEvent);
					num = getMissileCoverPercentageEvent.Percentage;
				}
			}
			return num;
		}

		// Token: 0x040025FC RID: 9724
		public GameObject Object;

		// Token: 0x040025FD RID: 9725
		public GameObject Actor;

		// Token: 0x040025FE RID: 9726
		public GameObject Projectile;

		// Token: 0x040025FF RID: 9727
		public bool PenetrateCreatures;

		// Token: 0x04002600 RID: 9728
		public bool PenetrateWalls;

		// Token: 0x04002601 RID: 9729
		public int Percentage;
	}
}
