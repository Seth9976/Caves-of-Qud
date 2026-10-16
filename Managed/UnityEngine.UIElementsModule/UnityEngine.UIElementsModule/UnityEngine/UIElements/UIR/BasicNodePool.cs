using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200064B RID: 1611
	internal class BasicNodePool<T> : LinkedPool<BasicNode<T>>
	{
		// Token: 0x06002F2D RID: 12077 RVA: 0x000AC4A3 File Offset: 0x000AA6A3
		private static void Reset(BasicNode<T> node)
		{
			node.next = null;
			node.data = default(T);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000AC4BC File Offset: 0x000AA6BC
		private static BasicNode<T> Create()
		{
			return new BasicNode<T>();
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000AC4D3 File Offset: 0x000AA6D3
		public BasicNodePool()
			: base(new Func<BasicNode<T>>(BasicNodePool<T>.Create), new Action<BasicNode<T>>(BasicNodePool<T>.Reset), 10000)
		{
		}
	}
}
