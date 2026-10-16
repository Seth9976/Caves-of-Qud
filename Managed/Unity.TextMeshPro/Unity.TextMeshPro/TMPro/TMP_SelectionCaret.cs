using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000045 RID: 69
	[RequireComponent(typeof(CanvasRenderer))]
	public class TMP_SelectionCaret : MaskableGraphic
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00011F9D File Offset: 0x0001019D
		public override void Cull(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.cull = false;
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
				return;
			}
			base.Cull(clipRect, validRect);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00011FBD File Offset: 0x000101BD
		protected override void UpdateGeometry()
		{
		}
	}
}
