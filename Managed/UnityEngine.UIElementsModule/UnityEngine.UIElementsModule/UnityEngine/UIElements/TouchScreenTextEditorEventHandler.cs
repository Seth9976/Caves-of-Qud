using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000127 RID: 295
	internal class TouchScreenTextEditorEventHandler : TextEditorEventHandler
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0002A4A7 File Offset: 0x000286A7
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0002A4AE File Offset: 0x000286AE
		internal static long Frame { get; private set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002A4B6 File Offset: 0x000286B6
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x0002A4BD File Offset: 0x000286BD
		internal static TouchScreenKeyboard activeTouchScreenKeyboard { get; private set; }

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002A4C5 File Offset: 0x000286C5
		public TouchScreenTextEditorEventHandler(TextElement textElement, TextEditingUtilities editingUtilities)
			: base(textElement, editingUtilities)
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002A4E0 File Offset: 0x000286E0
		private void PollTouchScreenKeyboard()
		{
			bool isSupported = TouchScreenKeyboard.isSupported;
			if (isSupported)
			{
				bool flag = this.m_TouchKeyboardPoller == null;
				if (flag)
				{
					TextElement textElement = this.textElement;
					this.m_TouchKeyboardPoller = ((textElement != null) ? textElement.schedule.Execute(new Action(this.DoPollTouchScreenKeyboard)).Every(100L) : null);
				}
				else
				{
					this.m_TouchKeyboardPoller.Resume();
				}
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002A548 File Offset: 0x00028748
		private void DoPollTouchScreenKeyboard()
		{
			TouchScreenTextEditorEventHandler.Frame += 1L;
			bool flag = this.editingUtilities.TouchScreenKeyboardCanBeUsed() && !this.textElement.edition.hideSoftKeyboard;
			if (flag)
			{
				bool flag2 = this.textElement.m_TouchScreenKeyboard == null;
				if (!flag2)
				{
					ITextEdition edition = this.textElement.edition;
					TouchScreenKeyboard touchScreenKeyboard = this.textElement.m_TouchScreenKeyboard;
					string text = touchScreenKeyboard.text;
					bool flag3 = touchScreenKeyboard.status > TouchScreenKeyboard.Status.Visible;
					if (flag3)
					{
						bool flag4 = touchScreenKeyboard.status == TouchScreenKeyboard.Status.Canceled;
						if (flag4)
						{
							edition.RestoreValueAndText();
						}
						else
						{
							text = touchScreenKeyboard.text;
							bool flag5 = this.editingUtilities.text != text;
							if (flag5)
							{
								edition.UpdateText(text);
								this.textElement.uitkTextHandle.Update();
							}
						}
						this.CloseTouchScreenKeyboard();
						bool flag6 = !edition.isDelayed;
						if (flag6)
						{
							Action updateValueFromText = edition.UpdateValueFromText;
							if (updateValueFromText != null)
							{
								updateValueFromText();
							}
						}
						bool flag7 = !edition.isDelayed;
						if (flag7)
						{
							Action updateTextFromValue = edition.UpdateTextFromValue;
							if (updateTextFromValue != null)
							{
								updateTextFromValue();
							}
						}
						this.textElement.Blur();
					}
					else
					{
						bool hideMobileInput = edition.hideMobileInput;
						if (hideMobileInput)
						{
							bool flag8 = this.editingUtilities.text != text;
							if (flag8)
							{
								bool flag9 = false;
								this.editingUtilities.text = "";
								foreach (char c in text)
								{
									bool flag10 = !edition.AcceptCharacter(c);
									if (flag10)
									{
										return;
									}
									bool flag11 = c > '\0';
									if (flag11)
									{
										TextEditingUtilities editingUtilities = this.editingUtilities;
										editingUtilities.text += c.ToString();
										flag9 = true;
									}
								}
								bool flag12 = flag9;
								if (flag12)
								{
									this.UpdateStringPositionFromKeyboard();
								}
								edition.UpdateText(this.editingUtilities.text);
								this.textElement.uitkTextHandle.ComputeSettingsAndUpdate();
							}
							else
							{
								bool flag13 = !this.m_IsClicking && touchScreenKeyboard != null && touchScreenKeyboard.canGetSelection;
								if (flag13)
								{
									this.UpdateStringPositionFromKeyboard();
								}
							}
						}
						else
						{
							bool flag14 = this.editingUtilities.text == text;
							if (flag14)
							{
								return;
							}
							edition.UpdateText(text);
							this.textElement.uitkTextHandle.ComputeSettingsAndUpdate();
						}
						bool flag15 = !edition.isDelayed;
						if (flag15)
						{
							Action updateValueFromText2 = edition.UpdateValueFromText;
							if (updateValueFromText2 != null)
							{
								updateValueFromText2();
							}
						}
						bool flag16 = !edition.isDelayed;
						if (flag16)
						{
							Action updateTextFromValue2 = edition.UpdateTextFromValue;
							if (updateTextFromValue2 != null)
							{
								updateTextFromValue2();
							}
						}
						Action<bool> updateScrollOffset = this.textElement.edition.UpdateScrollOffset;
						if (updateScrollOffset != null)
						{
							updateScrollOffset(false);
						}
					}
				}
			}
			else
			{
				this.CloseTouchScreenKeyboard();
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002A83C File Offset: 0x00028A3C
		private void UpdateStringPositionFromKeyboard()
		{
			bool flag = this.textElement.m_TouchScreenKeyboard == null;
			if (!flag)
			{
				RangeInt selection = this.textElement.m_TouchScreenKeyboard.selection;
				int start = selection.start;
				int end = selection.end;
				bool flag2 = this.textElement.selection.selectIndex != start;
				if (flag2)
				{
					this.textElement.selection.selectIndex = start;
				}
				bool flag3 = this.textElement.selection.cursorIndex != end;
				if (flag3)
				{
					this.textElement.selection.cursorIndex = end;
				}
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002A8DC File Offset: 0x00028ADC
		private void CloseTouchScreenKeyboard()
		{
			bool flag = this.textElement.m_TouchScreenKeyboard != null;
			if (flag)
			{
				this.textElement.m_TouchScreenKeyboard.active = false;
				this.textElement.m_TouchScreenKeyboard = null;
				IVisualElementScheduledItem touchKeyboardPoller = this.m_TouchKeyboardPoller;
				if (touchKeyboardPoller != null)
				{
					touchKeyboardPoller.Pause();
				}
			}
			TouchScreenTextEditorEventHandler.activeTouchScreenKeyboard = null;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002A938 File Offset: 0x00028B38
		private void OpenTouchScreenKeyboard()
		{
			ITextEdition edition = this.textElement.edition;
			TouchScreenKeyboard.hideInput = edition.hideMobileInput;
			this.textElement.m_TouchScreenKeyboard = TouchScreenKeyboard.Open(this.textElement.text, edition.keyboardType, !edition.isPassword && edition.autoCorrection, edition.multiline, edition.isPassword);
			bool hideMobileInput = edition.hideMobileInput;
			if (hideMobileInput)
			{
				int selectIndex = this.textElement.selection.selectIndex;
				int cursorIndex = this.textElement.selection.cursorIndex;
				int num = ((selectIndex < cursorIndex) ? (cursorIndex - selectIndex) : (selectIndex - cursorIndex));
				int num2 = ((selectIndex < cursorIndex) ? selectIndex : cursorIndex);
				this.textElement.m_TouchScreenKeyboard.selection = new RangeInt(num2, num);
			}
			else
			{
				TouchScreenKeyboard touchScreenKeyboard = this.textElement.m_TouchScreenKeyboard;
				string text = this.textElement.m_TouchScreenKeyboard.text;
				touchScreenKeyboard.selection = new RangeInt((text != null) ? text.Length : 0, 0);
			}
			TouchScreenTextEditorEventHandler.activeTouchScreenKeyboard = this.textElement.m_TouchScreenKeyboard;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002AA44 File Offset: 0x00028C44
		public override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag = !this.editingUtilities.TouchScreenKeyboardCanBeUsed() || this.textElement.edition.isReadOnly || this.textElement.edition.hideSoftKeyboard;
			if (!flag)
			{
				if (!(evt is PointerDownEvent))
				{
					PointerUpEvent pointerUpEvent = evt as PointerUpEvent;
					if (pointerUpEvent == null)
					{
						if (!(evt is FocusInEvent))
						{
							FocusOutEvent focusOutEvent = evt as FocusOutEvent;
							if (focusOutEvent != null)
							{
								this.OnFocusOutEvent(focusOutEvent);
							}
						}
						else
						{
							this.OnFocusInEvent();
						}
					}
					else
					{
						this.OnPointerUpEvent(pointerUpEvent);
					}
				}
				else
				{
					this.OnPointerDownEvent();
				}
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002AAE8 File Offset: 0x00028CE8
		private void OnPointerDownEvent()
		{
			this.m_IsClicking = true;
			bool flag = this.textElement.m_TouchScreenKeyboard != null && this.textElement.edition.hideMobileInput;
			if (flag)
			{
				int num = this.textElement.selection.cursorIndex;
				string text = this.textElement.m_TouchScreenKeyboard.text;
				int num2 = ((text != null) ? text.Length : 0);
				bool flag2 = num < 0;
				if (flag2)
				{
					num = 0;
				}
				bool flag3 = num > num2;
				if (flag3)
				{
					num = num2;
				}
				this.textElement.m_TouchScreenKeyboard.selection = new RangeInt(num, 0);
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002AB7F File Offset: 0x00028D7F
		private void OnPointerUpEvent(PointerUpEvent evt)
		{
			this.m_IsClicking = false;
			evt.StopPropagation();
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002AB90 File Offset: 0x00028D90
		private void OnFocusInEvent()
		{
			bool flag = this.textElement.m_TouchScreenKeyboard != null;
			if (!flag)
			{
				this.OpenTouchScreenKeyboard();
				bool flag2 = this.textElement.m_TouchScreenKeyboard != null;
				if (flag2)
				{
					this.PollTouchScreenKeyboard();
				}
				this.textElement.edition.SaveValueAndText();
				Action<bool> updateScrollOffset = this.textElement.edition.UpdateScrollOffset;
				if (updateScrollOffset != null)
				{
					updateScrollOffset(false);
				}
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002AC00 File Offset: 0x00028E00
		private void OnFocusOutEvent(FocusOutEvent evt)
		{
			TextElement textElement = (TextElement)evt.target;
			TextElement textElement2 = textElement.focusController.m_LastPendingFocusedElement as TextElement;
			bool flag = textElement2 == textElement || textElement2 == null || textElement2.edition.keyboardType != textElement.edition.keyboardType || textElement2.edition.multiline != textElement.edition.multiline || textElement2.edition.hideMobileInput != textElement.edition.hideMobileInput || textElement2.edition.hideSoftKeyboard != textElement.edition.hideSoftKeyboard;
			if (flag)
			{
				this.CloseTouchScreenKeyboard();
			}
			else
			{
				this.textElement.m_TouchScreenKeyboard = null;
				IVisualElementScheduledItem touchKeyboardPoller = this.m_TouchKeyboardPoller;
				if (touchKeyboardPoller != null)
				{
					touchKeyboardPoller.Pause();
				}
			}
		}

		// Token: 0x04000588 RID: 1416
		private IVisualElementScheduledItem m_TouchKeyboardPoller = null;

		// Token: 0x04000589 RID: 1417
		private bool m_IsClicking = false;
	}
}
