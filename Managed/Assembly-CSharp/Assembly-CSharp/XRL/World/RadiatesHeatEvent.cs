using System;

namespace XRL.World
{
	// Token: 0x0200083C RID: 2108
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class RadiatesHeatEvent : SingletonEvent<RadiatesHeatEvent>
	{
		// Token: 0x060053C9 RID: 21449 RVA: 0x001905A8 File Offset: 0x0018E7A8
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x001905B1 File Offset: 0x0018E7B1
		public static bool Check(GameObject Object)
		{
			return GameObject.Validate(ref Object) && !Object.HandleEvent(SingletonEvent<RadiatesHeatEvent>.Instance);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x001905CC File Offset: 0x0018E7CC
		public static bool Check(Cell C)
		{
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					if (RadiatesHeatEvent.Check(C.Objects[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}
	}
}
