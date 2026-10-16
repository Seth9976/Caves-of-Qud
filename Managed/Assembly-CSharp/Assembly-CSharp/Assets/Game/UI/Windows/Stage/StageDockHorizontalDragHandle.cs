using System;
using Qud.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Game.UI.Windows.Stage
{
	// Token: 0x020003E8 RID: 1000
	public class StageDockHorizontalDragHandle : MonoBehaviour, IDragHandler, IEventSystemHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x06002568 RID: 9576 RVA: 0x000BEE73 File Offset: 0x000BD073
		public void SyncCursor()
		{
			if (this.dragging || this.inside)
			{
				CursorManager.instance.currentStyle = CursorManager.Style.ResizeWestEast;
				return;
			}
			CursorManager.instance.currentStyle = CursorManager.Style.Pointer;
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000BEE9C File Offset: 0x000BD09C
		public void OnDrag(PointerEventData eventData)
		{
			if (!this.dragging)
			{
				this.draggingWidth = this.dock.calculatedWidth;
			}
			this.dragging = true;
			bool flag = false;
			if (GameManager.Instance.DockMovable == 3)
			{
				flag = GameManager.Instance.currentSidebarPosition == GameManager.PreferredSidebarPosition.Left;
			}
			if (GameManager.Instance.DockMovable == 1)
			{
				flag = true;
			}
			if (flag)
			{
				this.draggingWidth += eventData.delta.x / UIManager.scale;
			}
			else
			{
				this.draggingWidth -= eventData.delta.x / UIManager.scale;
			}
			this.dock.preferredWidth = this.draggingWidth;
			MessageLogWindow instance = SingletonWindowBase<MessageLogWindow>.instance;
			if (instance == null)
			{
				return;
			}
			instance.Reflow();
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000BEF56 File Offset: 0x000BD156
		public void OnEndDrag(PointerEventData eventData)
		{
			this.dragging = false;
			PlayerPrefs.SetFloat("DockPreferredWith", this.dock.preferredWidth);
			this.SyncCursor();
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000BEF7A File Offset: 0x000BD17A
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.inside = true;
			this.SyncCursor();
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000BEF89 File Offset: 0x000BD189
		public void OnPointerExit(PointerEventData eventData)
		{
			this.inside = false;
			this.SyncCursor();
		}

		// Token: 0x040012C7 RID: 4807
		public StageDock dock;

		// Token: 0x040012C8 RID: 4808
		public Texture2D hoverCursor;

		// Token: 0x040012C9 RID: 4809
		private bool dragging;

		// Token: 0x040012CA RID: 4810
		private bool inside;

		// Token: 0x040012CB RID: 4811
		private float draggingWidth;
	}
}
