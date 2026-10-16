using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011D RID: 285
	internal class TextEditorEventHandler
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x000279A6 File Offset: 0x00025BA6
		protected TextEditorEventHandler(TextElement textElement, TextEditingUtilities editingUtilities)
		{
			this.textElement = textElement;
			this.editingUtilities = editingUtilities;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void RegisterCallbacksOnTarget(VisualElement target)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void UnregisterCallbacksFromTarget(VisualElement target)
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void HandleEventBubbleUp(EventBase evt)
		{
		}

		// Token: 0x04000516 RID: 1302
		protected TextElement textElement;

		// Token: 0x04000517 RID: 1303
		protected TextEditingUtilities editingUtilities;
	}
}
