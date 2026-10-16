using System;

namespace fsm
{
	// Token: 0x0200034E RID: 846
	public class Event : EventArgs
	{
		// Token: 0x06001F7F RID: 8063 RVA: 0x00099308 File Offset: 0x00097508
		public Event(int _id = -1)
		{
			this.id = _id;
		}

		// Token: 0x0400102B RID: 4139
		public static readonly int UNKNOWN = -1;

		// Token: 0x0400102C RID: 4140
		public static readonly int NULL = 0;

		// Token: 0x0400102D RID: 4141
		public static readonly int NEXT = 1;

		// Token: 0x0400102E RID: 4142
		public static readonly int TRIGGER = 2;

		// Token: 0x0400102F RID: 4143
		public static readonly int FINISHED = 3;

		// Token: 0x04001030 RID: 4144
		public const int USER_FIELD = 1000;

		// Token: 0x04001031 RID: 4145
		public int id = Event.UNKNOWN;
	}
}
