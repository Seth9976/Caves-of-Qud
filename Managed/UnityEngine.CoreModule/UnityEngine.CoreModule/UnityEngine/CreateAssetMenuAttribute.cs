using System;

namespace UnityEngine
{
	// Token: 0x0200025D RID: 605
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class CreateAssetMenuAttribute : Attribute
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0003C454 File Offset: 0x0003A654
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x0003C45C File Offset: 0x0003A65C
		public string menuName { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x0003C465 File Offset: 0x0003A665
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x0003C46D File Offset: 0x0003A66D
		public string fileName { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0003C476 File Offset: 0x0003A676
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x0003C47E File Offset: 0x0003A67E
		public int order { get; set; }
	}
}
