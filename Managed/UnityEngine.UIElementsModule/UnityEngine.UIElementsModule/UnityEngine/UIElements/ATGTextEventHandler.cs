using System;
using UnityEngine.TextCore;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E6 RID: 1254
	internal class ATGTextEventHandler
	{
		// Token: 0x0600262A RID: 9770 RVA: 0x0008A533 File Offset: 0x00088733
		public ATGTextEventHandler(TextElement textElement)
		{
			Debug.Assert(textElement.uitkTextHandle.useAdvancedText);
			this.m_TextElement = textElement;
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0008A55C File Offset: 0x0008875C
		public void OnDestroy()
		{
			this.UnRegisterLinkTagCallbacks();
			this.UnRegisterHyperlinkCallbacks();
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0008A570 File Offset: 0x00088770
		private bool HasAllocatedLinkCallbacks()
		{
			return this.m_LinkTagOnPointerDown != null;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0008A58C File Offset: 0x0008878C
		private void AllocateLinkCallbacks()
		{
			bool flag = this.HasAllocatedLinkCallbacks();
			if (!flag)
			{
				this.m_LinkTagOnPointerDown = new EventCallback<PointerDownEvent>(this.LinkTagOnPointerDown);
				this.m_LinkTagOnPointerUp = new EventCallback<PointerUpEvent>(this.LinkTagOnPointerUp);
				this.m_LinkTagOnPointerMove = new EventCallback<PointerMoveEvent>(this.LinkTagOnPointerMove);
				this.m_LinkTagOnPointerOut = new EventCallback<PointerOutEvent>(this.LinkTagOnPointerOut);
			}
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0008A5F0 File Offset: 0x000887F0
		private bool HasAllocatedHyperlinkCallbacks()
		{
			return this.m_HyperlinkOnPointerUp != null;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0008A60C File Offset: 0x0008880C
		private void AllocateHyperlinkCallbacks()
		{
			bool flag = this.HasAllocatedHyperlinkCallbacks();
			if (!flag)
			{
				this.m_HyperlinkOnPointerUp = new EventCallback<PointerUpEvent>(this.HyperlinkOnPointerUp);
				this.m_HyperlinkOnPointerMove = new EventCallback<PointerMoveEvent>(this.HyperlinkOnPointerMove);
				this.m_HyperlinkOnPointerOver = new EventCallback<PointerOverEvent>(this.HyperlinkOnPointerOver);
				this.m_HyperlinkOnPointerOut = new EventCallback<PointerOutEvent>(this.HyperlinkOnPointerOut);
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0008A670 File Offset: 0x00088870
		private void HyperlinkOnPointerUp(PointerUpEvent pue)
		{
			Vector3 vector = pue.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(vector);
			RichTextTagParser.TagType item = valueTuple.Item1;
			string item2 = valueTuple.Item2;
			bool flag = item2 == null || item > RichTextTagParser.TagType.Hyperlink;
			if (!flag)
			{
				bool flag2 = Uri.IsWellFormedUriString(item2, UriKind.Absolute);
				if (flag2)
				{
					Application.OpenURL(item2);
				}
			}
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0008A708 File Offset: 0x00088908
		private void HyperlinkOnPointerOver(PointerOverEvent _)
		{
			this.isOverridingCursor = false;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0008A714 File Offset: 0x00088914
		private void HyperlinkOnPointerMove(PointerMoveEvent pme)
		{
			Vector3 vector = pme.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(vector);
			RichTextTagParser.TagType item = valueTuple.Item1;
			string item2 = valueTuple.Item2;
			BaseVisualElementPanel baseVisualElementPanel = this.m_TextElement.panel as BaseVisualElementPanel;
			ICursorManager cursorManager = ((baseVisualElementPanel != null) ? baseVisualElementPanel.cursorManager : null);
			bool flag = item2 != null && item == RichTextTagParser.TagType.Hyperlink;
			if (flag)
			{
				bool flag2 = !this.isOverridingCursor;
				if (flag2)
				{
					this.isOverridingCursor = true;
					if (cursorManager != null)
					{
						cursorManager.SetCursor(new Cursor
						{
							defaultCursorId = 4
						});
					}
				}
			}
			else
			{
				bool flag3 = this.isOverridingCursor;
				if (flag3)
				{
					if (cursorManager != null)
					{
						cursorManager.SetCursor(this.m_TextElement.computedStyle.cursor);
					}
					this.isOverridingCursor = false;
				}
			}
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0008A708 File Offset: 0x00088908
		private void HyperlinkOnPointerOut(PointerOutEvent evt)
		{
			this.isOverridingCursor = false;
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x0008A820 File Offset: 0x00088A20
		private void LinkTagOnPointerDown(PointerDownEvent pde)
		{
			Vector3 vector = pde.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(vector);
			RichTextTagParser.TagType item = valueTuple.Item1;
			string item2 = valueTuple.Item2;
			bool flag = item2 == null || item != RichTextTagParser.TagType.Link;
			if (!flag)
			{
				using (PointerDownLinkTagEvent pooled = PointerDownLinkTagEvent.GetPooled(pde, item2, "test"))
				{
					pooled.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(pooled);
				}
			}
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x0008A8F4 File Offset: 0x00088AF4
		private void LinkTagOnPointerUp(PointerUpEvent pue)
		{
			Vector3 vector = pue.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(vector);
			RichTextTagParser.TagType item = valueTuple.Item1;
			string item2 = valueTuple.Item2;
			bool flag = item2 == null || item != RichTextTagParser.TagType.Link;
			if (!flag)
			{
				using (PointerUpLinkTagEvent pooled = PointerUpLinkTagEvent.GetPooled(pue, item2, "test"))
				{
					pooled.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(pooled);
				}
			}
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x0008A9C8 File Offset: 0x00088BC8
		private void LinkTagOnPointerMove(PointerMoveEvent pme)
		{
			Vector3 vector = pme.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			ValueTuple<RichTextTagParser.TagType, string> valueTuple = this.m_TextElement.uitkTextHandle.ATGFindIntersectingLink(vector);
			RichTextTagParser.TagType item = valueTuple.Item1;
			string item2 = valueTuple.Item2;
			bool flag = item2 != null && item == RichTextTagParser.TagType.Link;
			if (flag)
			{
				bool flag2 = this.currentLinkIDHash == -1;
				if (flag2)
				{
					this.currentLinkIDHash = 0;
					using (PointerOverLinkTagEvent pooled = PointerOverLinkTagEvent.GetPooled(pme, item2, "test"))
					{
						pooled.elementTarget = this.m_TextElement;
						this.m_TextElement.SendEvent(pooled);
					}
					return;
				}
				bool flag3 = this.currentLinkIDHash == 0;
				if (flag3)
				{
					using (PointerMoveLinkTagEvent pooled2 = PointerMoveLinkTagEvent.GetPooled(pme, item2, "test"))
					{
						pooled2.elementTarget = this.m_TextElement;
						this.m_TextElement.SendEvent(pooled2);
					}
					return;
				}
			}
			bool flag4 = this.currentLinkIDHash != -1;
			if (flag4)
			{
				this.currentLinkIDHash = -1;
				using (PointerOutLinkTagEvent pooled3 = PointerOutLinkTagEvent.GetPooled(pme, string.Empty))
				{
					pooled3.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(pooled3);
				}
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x0008AB74 File Offset: 0x00088D74
		private void LinkTagOnPointerOut(PointerOutEvent poe)
		{
			bool flag = this.currentLinkIDHash != -1;
			if (flag)
			{
				using (PointerOutLinkTagEvent pooled = PointerOutLinkTagEvent.GetPooled(poe, string.Empty))
				{
					pooled.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(pooled);
				}
				this.currentLinkIDHash = -1;
			}
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0008ABE0 File Offset: 0x00088DE0
		internal void RegisterLinkTagCallbacks()
		{
			TextElement textElement = this.m_TextElement;
			bool flag = ((textElement != null) ? textElement.panel : null) == null;
			if (!flag)
			{
				this.AllocateLinkCallbacks();
				this.m_TextElement.RegisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
				this.m_TextElement.RegisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
				this.m_TextElement.RegisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
				this.m_TextElement.RegisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
			}
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x0008AC5C File Offset: 0x00088E5C
		internal void UnRegisterLinkTagCallbacks()
		{
			bool flag = this.HasAllocatedLinkCallbacks();
			if (flag)
			{
				this.m_TextElement.UnregisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x0008ACC4 File Offset: 0x00088EC4
		internal void RegisterHyperlinkCallbacks()
		{
			TextElement textElement = this.m_TextElement;
			bool flag = ((textElement != null) ? textElement.panel : null) == null;
			if (!flag)
			{
				this.AllocateHyperlinkCallbacks();
				this.m_TextElement.RegisterCallback<PointerUpEvent>(this.m_HyperlinkOnPointerUp, TrickleDown.TrickleDown);
				bool flag2 = this.m_TextElement.panel.contextType == ContextType.Editor;
				if (flag2)
				{
					this.m_TextElement.RegisterCallback<PointerMoveEvent>(this.m_HyperlinkOnPointerMove, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerOverEvent>(this.m_HyperlinkOnPointerOver, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerOutEvent>(this.m_HyperlinkOnPointerOut, TrickleDown.TrickleDown);
				}
			}
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x0008AD5C File Offset: 0x00088F5C
		internal void UnRegisterHyperlinkCallbacks()
		{
			TextElement textElement = this.m_TextElement;
			bool flag = ((textElement != null) ? textElement.panel : null) == null;
			if (!flag)
			{
				bool flag2 = this.HasAllocatedHyperlinkCallbacks();
				if (flag2)
				{
					this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_HyperlinkOnPointerUp, TrickleDown.TrickleDown);
					bool flag3 = this.m_TextElement.panel.contextType == ContextType.Editor;
					if (flag3)
					{
						this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_HyperlinkOnPointerMove, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerOverEvent>(this.m_HyperlinkOnPointerOver, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_HyperlinkOnPointerOut, TrickleDown.TrickleDown);
					}
				}
			}
		}

		// Token: 0x0400113E RID: 4414
		private TextElement m_TextElement;

		// Token: 0x0400113F RID: 4415
		private EventCallback<PointerDownEvent> m_LinkTagOnPointerDown;

		// Token: 0x04001140 RID: 4416
		private EventCallback<PointerUpEvent> m_LinkTagOnPointerUp;

		// Token: 0x04001141 RID: 4417
		private EventCallback<PointerMoveEvent> m_LinkTagOnPointerMove;

		// Token: 0x04001142 RID: 4418
		private EventCallback<PointerOutEvent> m_LinkTagOnPointerOut;

		// Token: 0x04001143 RID: 4419
		private EventCallback<PointerUpEvent> m_HyperlinkOnPointerUp;

		// Token: 0x04001144 RID: 4420
		private EventCallback<PointerMoveEvent> m_HyperlinkOnPointerMove;

		// Token: 0x04001145 RID: 4421
		private EventCallback<PointerOverEvent> m_HyperlinkOnPointerOver;

		// Token: 0x04001146 RID: 4422
		private EventCallback<PointerOutEvent> m_HyperlinkOnPointerOut;

		// Token: 0x04001147 RID: 4423
		internal bool isOverridingCursor;

		// Token: 0x04001148 RID: 4424
		internal int currentLinkIDHash = -1;
	}
}
