using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200083B RID: 2107
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class RadiatesHeatAdjacentEvent : SingletonEvent<RadiatesHeatAdjacentEvent>
	{
		// Token: 0x060053C4 RID: 21444 RVA: 0x001904DF File Offset: 0x0018E6DF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x001904E8 File Offset: 0x0018E6E8
		public static bool Check(GameObject Object)
		{
			return GameObject.Validate(ref Object) && !Object.HandleEvent(SingletonEvent<RadiatesHeatAdjacentEvent>.Instance);
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x00190504 File Offset: 0x0018E704
		public static bool Check(Cell C)
		{
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					if (RadiatesHeatAdjacentEvent.Check(C.Objects[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00190544 File Offset: 0x0018E744
		public static bool CheckAdjacent(Cell C)
		{
			if (C != null)
			{
				using (List<Cell>.Enumerator enumerator = C.GetLocalAdjacentCells().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (RadiatesHeatAdjacentEvent.Check(enumerator.Current))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}
	}
}
