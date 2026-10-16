using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000111 RID: 273
	internal static class StateTuple
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x0000E97B File Offset: 0x0000CB7B
		public static StateTuple<T1> Create<T1>(T1 item1)
		{
			return StatePool<T1>.Create(item1);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000E983 File Offset: 0x0000CB83
		public static StateTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return StatePool<T1, T2>.Create(item1, item2);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000E98C File Offset: 0x0000CB8C
		public static StateTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return StatePool<T1, T2, T3>.Create(item1, item2, item3);
		}
	}
}
