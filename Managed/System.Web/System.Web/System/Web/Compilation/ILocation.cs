using System;

namespace System.Web.Compilation
{
	// Token: 0x02000652 RID: 1618
	internal interface ILocation
	{
		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x0600456E RID: 17774
		string Filename { get; }

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x0600456F RID: 17775
		int BeginLine { get; }

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06004570 RID: 17776
		int EndLine { get; }

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06004571 RID: 17777
		int BeginColumn { get; }

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06004572 RID: 17778
		int EndColumn { get; }

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06004573 RID: 17779
		string PlainText { get; }

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06004574 RID: 17780
		string FileText { get; }
	}
}
