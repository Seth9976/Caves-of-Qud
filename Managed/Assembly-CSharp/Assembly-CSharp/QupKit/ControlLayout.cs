using System;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003B2 RID: 946
	public class ControlLayout
	{
		// Token: 0x06002320 RID: 8992 RVA: 0x000B260A File Offset: 0x000B080A
		public ControlLayout()
		{
			this.Anchor = ControlAnchor.Custom;
			this.Offset = new Vector3(0f, 0f, 0f);
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000B263E File Offset: 0x000B083E
		public ControlLayout(ControlLayout Source)
		{
			this.Parent = null;
			this.Anchor = Source.Anchor;
			this.Offset = Source.Offset;
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000B2670 File Offset: 0x000B0870
		public ControlLayout(ControlAnchor Anchor)
		{
			this.Anchor = Anchor;
			this.Offset = new Vector3(0f, 0f, 0f);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000B26A4 File Offset: 0x000B08A4
		public ControlLayout(ControlAnchor Anchor, Vector3 Offset)
		{
			this.Anchor = Anchor;
			this.Offset = Offset;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000B26C5 File Offset: 0x000B08C5
		public void Apply()
		{
			this.Parent.ApplyLayout();
		}

		// Token: 0x040011C3 RID: 4547
		public BaseControl Parent;

		// Token: 0x040011C4 RID: 4548
		public ControlAnchor Anchor;

		// Token: 0x040011C5 RID: 4549
		public Vector3 Offset;

		// Token: 0x040011C6 RID: 4550
		public LayoutSides Margin = new LayoutSides();
	}
}
