using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F4 RID: 500
	internal struct DragAndDropArgs : IListDragAndDropArgs
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x00043D0C File Offset: 0x00041F0C
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x00043D14 File Offset: 0x00041F14
		public object target { readonly get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00043D1D File Offset: 0x00041F1D
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x00043D25 File Offset: 0x00041F25
		public int insertAtIndex { readonly get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00043D2E File Offset: 0x00041F2E
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x00043D36 File Offset: 0x00041F36
		public int parentId { readonly get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00043D3F File Offset: 0x00041F3F
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x00043D47 File Offset: 0x00041F47
		public int childIndex { readonly get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00043D50 File Offset: 0x00041F50
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x00043D58 File Offset: 0x00041F58
		public DragAndDropPosition dragAndDropPosition { readonly get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00043D61 File Offset: 0x00041F61
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x00043D69 File Offset: 0x00041F69
		public DragAndDropData dragAndDropData { readonly get; set; }
	}
}
