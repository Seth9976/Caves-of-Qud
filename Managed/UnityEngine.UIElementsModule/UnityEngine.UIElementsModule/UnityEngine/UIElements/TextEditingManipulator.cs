using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004ED RID: 1261
	internal class TextEditingManipulator
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x0008C60E File Offset: 0x0008A80E
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x0008C618 File Offset: 0x0008A818
		internal TextEditorEventHandler touchScreenEditingEventHandler
		{
			get
			{
				return this.m_TouchScreenEditingEventHandler;
			}
			set
			{
				bool flag = this.m_TouchScreenEditingEventHandler == value;
				if (!flag)
				{
					TextEditorEventHandler touchScreenEditingEventHandler = this.m_TouchScreenEditingEventHandler;
					if (touchScreenEditingEventHandler != null)
					{
						touchScreenEditingEventHandler.UnregisterCallbacksFromTarget(this.m_TextElement);
					}
					this.m_TouchScreenEditingEventHandler = value;
					TextEditorEventHandler touchScreenEditingEventHandler2 = this.m_TouchScreenEditingEventHandler;
					if (touchScreenEditingEventHandler2 != null)
					{
						touchScreenEditingEventHandler2.RegisterCallbacksOnTarget(this.m_TextElement);
					}
				}
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x0008C66C File Offset: 0x0008A86C
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x0008C674 File Offset: 0x0008A874
		internal TextEditorEventHandler keyboardEditingEventHandler
		{
			get
			{
				return this.m_KeyboardEditingEventHandler;
			}
			set
			{
				bool flag = this.m_KeyboardEditingEventHandler == value;
				if (!flag)
				{
					TextEditorEventHandler keyboardEditingEventHandler = this.m_KeyboardEditingEventHandler;
					if (keyboardEditingEventHandler != null)
					{
						keyboardEditingEventHandler.UnregisterCallbacksFromTarget(this.m_TextElement);
					}
					this.m_KeyboardEditingEventHandler = value;
					TextEditorEventHandler keyboardEditingEventHandler2 = this.m_KeyboardEditingEventHandler;
					if (keyboardEditingEventHandler2 != null)
					{
						keyboardEditingEventHandler2.RegisterCallbacksOnTarget(this.m_TextElement);
					}
				}
			}
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0008C6C8 File Offset: 0x0008A8C8
		public TextEditingManipulator(TextElement textElement)
		{
			this.m_TextElement = textElement;
			this.editingUtilities = new TextEditingUtilities(textElement.selectingManipulator.m_SelectingUtilities, textElement.uitkTextHandle, textElement.text);
			this.UpdateTextEditorEventHandler();
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0008C714 File Offset: 0x0008A914
		public void Reset()
		{
			this.touchScreenEditingEventHandler = null;
			this.keyboardEditingEventHandler = null;
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x0008C728 File Offset: 0x0008A928
		private bool touchScreenTextFieldChanged
		{
			get
			{
				return this.touchScreenCanBeUsed != (this.touchScreenEditingEventHandler != null) || this.keyboardCanBeUsed != (this.keyboardEditingEventHandler != null);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x0008C76C File Offset: 0x0008A96C
		private bool touchScreenCanBeUsed
		{
			get
			{
				TextEditingUtilities textEditingUtilities = this.editingUtilities;
				return textEditingUtilities != null && textEditingUtilities.TouchScreenKeyboardCanBeUsed() && !this.m_TextElement.edition.hideSoftKeyboard;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x0008C798 File Offset: 0x0008A998
		private bool keyboardCanBeUsed
		{
			get
			{
				bool flag;
				if (!this.m_TextElement.edition.hideSoftKeyboard)
				{
					TextEditingUtilities textEditingUtilities = this.editingUtilities;
					flag = textEditingUtilities == null || textEditingUtilities.PhysicalKeyboardCanBeUsed();
				}
				else
				{
					flag = true;
				}
				return flag;
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0008C7C4 File Offset: 0x0008A9C4
		private void UpdateTextEditorEventHandler()
		{
			bool touchScreenCanBeUsed = this.touchScreenCanBeUsed;
			if (touchScreenCanBeUsed)
			{
				this.touchScreenEditingEventHandler = new TouchScreenTextEditorEventHandler(this.m_TextElement, this.editingUtilities);
			}
			else
			{
				this.touchScreenEditingEventHandler = null;
			}
			bool keyboardCanBeUsed = this.keyboardCanBeUsed;
			if (keyboardCanBeUsed)
			{
				this.keyboardEditingEventHandler = new KeyboardTextEditorEventHandler(this.m_TextElement, this.editingUtilities);
			}
			else
			{
				this.keyboardEditingEventHandler = null;
			}
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0008C834 File Offset: 0x0008AA34
		internal void HandleEventBubbleUp(EventBase evt)
		{
			bool isReadOnly = this.m_TextElement.edition.isReadOnly;
			if (!isReadOnly)
			{
				bool flag = evt is BlurEvent;
				if (flag)
				{
					this.m_TextElement.uitkTextHandle.RemoveTextInfoFromPermanentCache();
				}
				else
				{
					bool flag2 = (!(evt is PointerMoveEvent) && !(evt is MouseMoveEvent)) || this.m_TextElement.selectingManipulator.isClicking;
					if (flag2)
					{
						this.m_TextElement.uitkTextHandle.AddTextInfoToPermanentCache();
					}
				}
				if (!(evt is FocusInEvent))
				{
					if (evt is FocusOutEvent)
					{
						this.OnFocusOutEvent();
					}
				}
				else
				{
					this.OnFocusInEvent();
				}
				TextEditorEventHandler keyboardEditingEventHandler = this.keyboardEditingEventHandler;
				if (keyboardEditingEventHandler != null)
				{
					keyboardEditingEventHandler.HandleEventBubbleUp(evt);
				}
				TextEditorEventHandler touchScreenEditingEventHandler = this.touchScreenEditingEventHandler;
				if (touchScreenEditingEventHandler != null)
				{
					touchScreenEditingEventHandler.HandleEventBubbleUp(evt);
				}
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0008C908 File Offset: 0x0008AB08
		private void OnFocusInEvent()
		{
			this.m_TextElement.edition.SaveValueAndText();
			this.m_TextElement.focusController.selectedTextElement = this.m_TextElement;
			bool touchScreenTextFieldChanged = this.touchScreenTextFieldChanged;
			if (touchScreenTextFieldChanged)
			{
				this.UpdateTextEditorEventHandler();
			}
			bool flag = this.m_HardwareKeyboardPoller == null;
			if (flag)
			{
				this.m_HardwareKeyboardPoller = this.m_TextElement.schedule.Execute(delegate
				{
					bool touchScreenTextFieldChanged2 = this.touchScreenTextFieldChanged;
					if (touchScreenTextFieldChanged2)
					{
						this.UpdateTextEditorEventHandler();
						this.m_TextElement.Blur();
					}
				}).Every(250L);
			}
			else
			{
				this.m_HardwareKeyboardPoller.Resume();
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0008C99B File Offset: 0x0008AB9B
		private void OnFocusOutEvent()
		{
			IVisualElementScheduledItem hardwareKeyboardPoller = this.m_HardwareKeyboardPoller;
			if (hardwareKeyboardPoller != null)
			{
				hardwareKeyboardPoller.Pause();
			}
			this.editingUtilities.OnBlur();
		}

		// Token: 0x0400116A RID: 4458
		private readonly TextElement m_TextElement;

		// Token: 0x0400116B RID: 4459
		private TextEditorEventHandler m_TouchScreenEditingEventHandler;

		// Token: 0x0400116C RID: 4460
		private TextEditorEventHandler m_KeyboardEditingEventHandler;

		// Token: 0x0400116D RID: 4461
		internal TextEditingUtilities editingUtilities;

		// Token: 0x0400116E RID: 4462
		private IVisualElementScheduledItem m_HardwareKeyboardPoller = null;
	}
}
