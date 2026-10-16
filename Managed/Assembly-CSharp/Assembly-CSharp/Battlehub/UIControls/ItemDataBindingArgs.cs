using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x020003C8 RID: 968
	public class ItemDataBindingArgs : EventArgs
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x000B590F File Offset: 0x000B3B0F
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x000B5917 File Offset: 0x000B3B17
		public object Item { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000B5920 File Offset: 0x000B3B20
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x000B5928 File Offset: 0x000B3B28
		public GameObject ItemPresenter { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000B5931 File Offset: 0x000B3B31
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x000B5939 File Offset: 0x000B3B39
		public GameObject EditorPresenter { get; set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000B5942 File Offset: 0x000B3B42
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x000B594A File Offset: 0x000B3B4A
		public bool CanEdit
		{
			get
			{
				return this.m_canEdit;
			}
			set
			{
				this.m_canEdit = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000B5953 File Offset: 0x000B3B53
		// (set) Token: 0x060023F4 RID: 9204 RVA: 0x000B595B File Offset: 0x000B3B5B
		public bool CanDrag
		{
			get
			{
				return this.m_canDrag;
			}
			set
			{
				this.m_canDrag = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x000B5964 File Offset: 0x000B3B64
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x000B596C File Offset: 0x000B3B6C
		public bool CanDrop
		{
			get
			{
				return this.m_canDrop;
			}
			set
			{
				this.m_canDrop = value;
			}
		}

		// Token: 0x04001211 RID: 4625
		private bool m_canEdit = true;

		// Token: 0x04001212 RID: 4626
		private bool m_canDrag = true;

		// Token: 0x04001213 RID: 4627
		private bool m_canDrop = true;
	}
}
