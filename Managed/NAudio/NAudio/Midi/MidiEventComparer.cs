using System;
using System.Collections.Generic;

namespace NAudio.Midi
{
	// Token: 0x020000D6 RID: 214
	public class MidiEventComparer : IComparer<MidiEvent>
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000FB60 File Offset: 0x0000DD60
		public int Compare(MidiEvent x, MidiEvent y)
		{
			long num = x.AbsoluteTime;
			long num2 = y.AbsoluteTime;
			if (num == num2)
			{
				MetaEvent metaEvent = x as MetaEvent;
				MetaEvent metaEvent2 = y as MetaEvent;
				if (metaEvent != null)
				{
					if (metaEvent.MetaEventType == MetaEventType.EndTrack)
					{
						num = long.MaxValue;
					}
					else
					{
						num = long.MinValue;
					}
				}
				if (metaEvent2 != null)
				{
					if (metaEvent2.MetaEventType == MetaEventType.EndTrack)
					{
						num2 = long.MaxValue;
					}
					else
					{
						num2 = long.MinValue;
					}
				}
			}
			return num.CompareTo(num2);
		}
	}
}
