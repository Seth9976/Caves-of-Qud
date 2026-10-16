using System;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020004EE RID: 1262
	internal class TextEventHandler
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x0008C9E9 File Offset: 0x0008ABE9
		private TextInfo textInfo
		{
			get
			{
				return this.m_TextElement.uitkTextHandle.textInfo;
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0008C9FB File Offset: 0x0008ABFB
		public TextEventHandler(TextElement textElement)
		{
			this.m_TextElement = textElement;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0008CA14 File Offset: 0x0008AC14
		public void OnDestroy()
		{
			bool flag = this.HasAllocatedLinkCallbacks();
			if (flag)
			{
				this.m_TextElement.UnregisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
			}
			bool flag2 = this.HasAllocatedATagCallbacks();
			if (flag2)
			{
				this.m_TextElement.UnregisterCallback<PointerOverEvent>(this.m_ATagOnPointerOver, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_ATagOnPointerMove, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_ATagOnPointerUp, TrickleDown.TrickleDown);
				this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_ATagOnPointerOut, TrickleDown.TrickleDown);
			}
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0008CAD4 File Offset: 0x0008ACD4
		private bool HasAllocatedLinkCallbacks()
		{
			return this.m_LinkTagOnPointerDown != null;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0008CAF0 File Offset: 0x0008ACF0
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

		// Token: 0x06002684 RID: 9860 RVA: 0x0008CB54 File Offset: 0x0008AD54
		private bool HasAllocatedATagCallbacks()
		{
			return this.m_ATagOnPointerUp != null;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0008CB70 File Offset: 0x0008AD70
		private void AllocateATagCallbacks()
		{
			bool flag = this.HasAllocatedATagCallbacks();
			if (!flag)
			{
				this.m_ATagOnPointerUp = new EventCallback<PointerUpEvent>(this.ATagOnPointerUp);
				this.m_ATagOnPointerMove = new EventCallback<PointerMoveEvent>(this.ATagOnPointerMove);
				this.m_ATagOnPointerOver = new EventCallback<PointerOverEvent>(this.ATagOnPointerOver);
				this.m_ATagOnPointerOut = new EventCallback<PointerOutEvent>(this.ATagOnPointerOut);
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0008CBD4 File Offset: 0x0008ADD4
		private void ATagOnPointerUp(PointerUpEvent pue)
		{
			Vector3 vector = pue.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int num = this.m_TextElement.uitkTextHandle.FindIntersectingLink(vector, true);
			bool flag = num < 0;
			if (!flag)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[num];
				bool flag2 = linkInfo.hashCode != 2535353;
				if (!flag2)
				{
					bool flag3 = linkInfo.linkId == null || linkInfo.linkIdLength <= 0;
					if (!flag3)
					{
						string linkId = linkInfo.GetLinkId();
						bool flag4 = Uri.IsWellFormedUriString(linkId, UriKind.Absolute);
						if (flag4)
						{
							Application.OpenURL(linkId);
						}
					}
				}
			}
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0008CCA9 File Offset: 0x0008AEA9
		private void ATagOnPointerOver(PointerOverEvent _)
		{
			this.isOverridingCursor = false;
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0008CCB4 File Offset: 0x0008AEB4
		private void ATagOnPointerMove(PointerMoveEvent pme)
		{
			Vector3 vector = pme.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int num = this.m_TextElement.uitkTextHandle.FindIntersectingLink(vector, true);
			BaseVisualElementPanel baseVisualElementPanel = this.m_TextElement.panel as BaseVisualElementPanel;
			ICursorManager cursorManager = ((baseVisualElementPanel != null) ? baseVisualElementPanel.cursorManager : null);
			bool flag = num >= 0;
			if (flag)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[num];
				bool flag2 = linkInfo.hashCode == 2535353;
				if (flag2)
				{
					bool flag3 = !this.isOverridingCursor;
					if (flag3)
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
					return;
				}
			}
			bool flag4 = this.isOverridingCursor;
			if (flag4)
			{
				if (cursorManager != null)
				{
					cursorManager.SetCursor(this.m_TextElement.computedStyle.cursor);
				}
				this.isOverridingCursor = false;
			}
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0008CCA9 File Offset: 0x0008AEA9
		private void ATagOnPointerOut(PointerOutEvent evt)
		{
			this.isOverridingCursor = false;
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0008CDD4 File Offset: 0x0008AFD4
		private void LinkTagOnPointerDown(PointerDownEvent pde)
		{
			Vector3 vector = pde.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int num = this.m_TextElement.uitkTextHandle.FindIntersectingLink(vector, true);
			bool flag = num < 0;
			if (!flag)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[num];
				bool flag2 = linkInfo.hashCode == 2535353;
				if (!flag2)
				{
					bool flag3 = linkInfo.linkId == null || linkInfo.linkIdLength <= 0;
					if (!flag3)
					{
						using (PointerDownLinkTagEvent pooled = PointerDownLinkTagEvent.GetPooled(pde, linkInfo.GetLinkId(), linkInfo.GetLinkText(this.textInfo)))
						{
							pooled.elementTarget = this.m_TextElement;
							this.m_TextElement.SendEvent(pooled);
						}
					}
				}
			}
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0008CEE4 File Offset: 0x0008B0E4
		private void LinkTagOnPointerUp(PointerUpEvent pue)
		{
			Vector3 vector = pue.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int num = this.m_TextElement.uitkTextHandle.FindIntersectingLink(vector, true);
			bool flag = num < 0;
			if (!flag)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[num];
				bool flag2 = linkInfo.hashCode == 2535353;
				if (!flag2)
				{
					bool flag3 = linkInfo.linkId == null || linkInfo.linkIdLength <= 0;
					if (!flag3)
					{
						using (PointerUpLinkTagEvent pooled = PointerUpLinkTagEvent.GetPooled(pue, linkInfo.GetLinkId(), linkInfo.GetLinkText(this.textInfo)))
						{
							pooled.elementTarget = this.m_TextElement;
							this.m_TextElement.SendEvent(pooled);
						}
					}
				}
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x0008CFF4 File Offset: 0x0008B1F4
		private void LinkTagOnPointerMove(PointerMoveEvent pme)
		{
			Vector3 vector = pme.localPosition - new Vector3(this.m_TextElement.contentRect.min.x, this.m_TextElement.contentRect.min.y);
			int num = this.m_TextElement.uitkTextHandle.FindIntersectingLink(vector, true);
			bool flag = num >= 0;
			if (flag)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[num];
				bool flag2 = linkInfo.hashCode != 2535353;
				if (flag2)
				{
					bool flag3 = this.currentLinkIDHash == -1;
					if (flag3)
					{
						this.currentLinkIDHash = linkInfo.hashCode;
						using (PointerOverLinkTagEvent pooled = PointerOverLinkTagEvent.GetPooled(pme, linkInfo.GetLinkId(), linkInfo.GetLinkText(this.textInfo)))
						{
							pooled.elementTarget = this.m_TextElement;
							this.m_TextElement.SendEvent(pooled);
						}
						return;
					}
					bool flag4 = this.currentLinkIDHash == linkInfo.hashCode;
					if (flag4)
					{
						using (PointerMoveLinkTagEvent pooled2 = PointerMoveLinkTagEvent.GetPooled(pme, linkInfo.GetLinkId(), linkInfo.GetLinkText(this.textInfo)))
						{
							pooled2.elementTarget = this.m_TextElement;
							this.m_TextElement.SendEvent(pooled2);
						}
						return;
					}
				}
			}
			bool flag5 = this.currentLinkIDHash != -1;
			if (flag5)
			{
				this.currentLinkIDHash = -1;
				using (PointerOutLinkTagEvent pooled3 = PointerOutLinkTagEvent.GetPooled(pme, string.Empty))
				{
					pooled3.elementTarget = this.m_TextElement;
					this.m_TextElement.SendEvent(pooled3);
				}
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0008D1E0 File Offset: 0x0008B3E0
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

		// Token: 0x0600268E RID: 9870 RVA: 0x0008D24C File Offset: 0x0008B44C
		internal void HandleLinkAndATagCallbacks()
		{
			TextElement textElement = this.m_TextElement;
			bool flag = ((textElement != null) ? textElement.panel : null) == null;
			if (!flag)
			{
				bool flag2 = this.hasLinkTag;
				if (flag2)
				{
					this.AllocateLinkCallbacks();
					this.m_TextElement.RegisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
					this.m_TextElement.RegisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
				}
				else
				{
					bool flag3 = this.HasAllocatedLinkCallbacks();
					if (flag3)
					{
						this.m_TextElement.UnregisterCallback<PointerDownEvent>(this.m_LinkTagOnPointerDown, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_LinkTagOnPointerUp, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_LinkTagOnPointerMove, TrickleDown.TrickleDown);
						this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_LinkTagOnPointerOut, TrickleDown.TrickleDown);
					}
				}
				bool flag4 = this.hasATag;
				if (flag4)
				{
					this.AllocateATagCallbacks();
					this.m_TextElement.RegisterCallback<PointerUpEvent>(this.m_ATagOnPointerUp, TrickleDown.TrickleDown);
					bool flag5 = this.m_TextElement.panel.contextType == ContextType.Editor;
					if (flag5)
					{
						this.m_TextElement.RegisterCallback<PointerMoveEvent>(this.m_ATagOnPointerMove, TrickleDown.TrickleDown);
						this.m_TextElement.RegisterCallback<PointerOverEvent>(this.m_ATagOnPointerOver, TrickleDown.TrickleDown);
						this.m_TextElement.RegisterCallback<PointerOutEvent>(this.m_ATagOnPointerOut, TrickleDown.TrickleDown);
					}
				}
				else
				{
					bool flag6 = this.HasAllocatedATagCallbacks();
					if (flag6)
					{
						this.m_TextElement.UnregisterCallback<PointerUpEvent>(this.m_ATagOnPointerUp, TrickleDown.TrickleDown);
						bool flag7 = this.m_TextElement.panel.contextType == ContextType.Editor;
						if (flag7)
						{
							this.m_TextElement.UnregisterCallback<PointerMoveEvent>(this.m_ATagOnPointerMove, TrickleDown.TrickleDown);
							this.m_TextElement.UnregisterCallback<PointerOverEvent>(this.m_ATagOnPointerOver, TrickleDown.TrickleDown);
							this.m_TextElement.UnregisterCallback<PointerOutEvent>(this.m_ATagOnPointerOut, TrickleDown.TrickleDown);
						}
					}
				}
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0008D424 File Offset: 0x0008B624
		internal void HandleLinkTag()
		{
			for (int i = 0; i < this.textInfo.linkCount; i++)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[i];
				bool flag = linkInfo.hashCode != 2535353;
				if (flag)
				{
					this.hasLinkTag = true;
					this.m_TextElement.uitkTextHandle.AddTextInfoToPermanentCache();
					return;
				}
			}
			bool flag2 = this.hasLinkTag;
			if (flag2)
			{
				this.hasLinkTag = false;
				this.m_TextElement.uitkTextHandle.RemoveTextInfoFromPermanentCache();
				return;
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0008D4B8 File Offset: 0x0008B6B8
		internal void HandleATag()
		{
			for (int i = 0; i < this.textInfo.linkCount; i++)
			{
				LinkInfo linkInfo = this.textInfo.linkInfo[i];
				bool flag = linkInfo.hashCode == 2535353;
				if (flag)
				{
					this.hasATag = true;
					this.m_TextElement.uitkTextHandle.AddTextInfoToPermanentCache();
					return;
				}
			}
			bool flag2 = this.hasATag;
			if (flag2)
			{
				this.hasATag = false;
				this.m_TextElement.uitkTextHandle.RemoveTextInfoFromPermanentCache();
				return;
			}
		}

		// Token: 0x0400116F RID: 4463
		private TextElement m_TextElement;

		// Token: 0x04001170 RID: 4464
		private EventCallback<PointerDownEvent> m_LinkTagOnPointerDown;

		// Token: 0x04001171 RID: 4465
		private EventCallback<PointerUpEvent> m_LinkTagOnPointerUp;

		// Token: 0x04001172 RID: 4466
		private EventCallback<PointerMoveEvent> m_LinkTagOnPointerMove;

		// Token: 0x04001173 RID: 4467
		private EventCallback<PointerOutEvent> m_LinkTagOnPointerOut;

		// Token: 0x04001174 RID: 4468
		private EventCallback<PointerUpEvent> m_ATagOnPointerUp;

		// Token: 0x04001175 RID: 4469
		private EventCallback<PointerMoveEvent> m_ATagOnPointerMove;

		// Token: 0x04001176 RID: 4470
		private EventCallback<PointerOverEvent> m_ATagOnPointerOver;

		// Token: 0x04001177 RID: 4471
		private EventCallback<PointerOutEvent> m_ATagOnPointerOut;

		// Token: 0x04001178 RID: 4472
		internal bool isOverridingCursor;

		// Token: 0x04001179 RID: 4473
		internal int currentLinkIDHash = -1;

		// Token: 0x0400117A RID: 4474
		internal bool hasLinkTag;

		// Token: 0x0400117B RID: 4475
		internal bool hasATag;
	}
}
