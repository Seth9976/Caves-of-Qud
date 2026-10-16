using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FA RID: 506
	public readonly struct HandleDragAndDropArgs
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00043DBE File Offset: 0x00041FBE
		public Vector2 position { get; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00043DC6 File Offset: 0x00041FC6
		public object target
		{
			get
			{
				return this.m_DragAndDropArgs.target;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00043DD3 File Offset: 0x00041FD3
		public int insertAtIndex
		{
			get
			{
				return this.m_DragAndDropArgs.insertAtIndex;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00043DE0 File Offset: 0x00041FE0
		public int parentId
		{
			get
			{
				return this.m_DragAndDropArgs.parentId;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00043DED File Offset: 0x00041FED
		public int childIndex
		{
			get
			{
				return this.m_DragAndDropArgs.childIndex;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00043DFA File Offset: 0x00041FFA
		public DragAndDropPosition dropPosition
		{
			get
			{
				return this.m_DragAndDropArgs.dragAndDropPosition;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00043E07 File Offset: 0x00042007
		public DragAndDropData dragAndDropData
		{
			get
			{
				return this.m_DragAndDropArgs.dragAndDropData;
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00043E14 File Offset: 0x00042014
		internal HandleDragAndDropArgs(Vector2 position, DragAndDropArgs dragAndDropArgs)
		{
			this.position = position;
			this.m_DragAndDropArgs = dragAndDropArgs;
		}

		// Token: 0x0400096C RID: 2412
		private readonly DragAndDropArgs m_DragAndDropArgs;
	}
}
