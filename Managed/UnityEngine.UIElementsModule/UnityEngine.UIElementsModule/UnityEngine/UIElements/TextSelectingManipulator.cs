using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004F0 RID: 1264
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class TextSelectingManipulator
	{
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x0008D5A8 File Offset: 0x0008B7A8
		// (set) Token: 0x06002694 RID: 9876 RVA: 0x0008D5B0 File Offset: 0x0008B7B0
		internal bool isClicking
		{
			get
			{
				return this.m_IsClicking;
			}
			private set
			{
				bool flag = this.m_IsClicking == value;
				if (!flag)
				{
					this.m_IsClicking = value;
				}
			}
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0008D5D4 File Offset: 0x0008B7D4
		public TextSelectingManipulator(TextElement textElement)
		{
			this.m_TextElement = textElement;
			this.m_SelectingUtilities = new TextSelectingUtilities(this.m_TextElement.uitkTextHandle);
			TextSelectingUtilities selectingUtilities = this.m_SelectingUtilities;
			selectingUtilities.OnCursorIndexChange = (Action)Delegate.Combine(selectingUtilities.OnCursorIndexChange, new Action(this.OnCursorIndexChange));
			TextSelectingUtilities selectingUtilities2 = this.m_SelectingUtilities;
			selectingUtilities2.OnSelectIndexChange = (Action)Delegate.Combine(selectingUtilities2.OnSelectIndexChange, new Action(this.OnSelectIndexChange));
			TextSelectingUtilities selectingUtilities3 = this.m_SelectingUtilities;
			selectingUtilities3.OnRevealCursorChange = (Action)Delegate.Combine(selectingUtilities3.OnRevealCursorChange, new Action(this.OnRevealCursor));
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x0008D68E File Offset: 0x0008B88E
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x0008D6A2 File Offset: 0x0008B8A2
		internal int cursorIndex
		{
			get
			{
				TextSelectingUtilities selectingUtilities = this.m_SelectingUtilities;
				return (selectingUtilities != null) ? selectingUtilities.cursorIndex : (-1);
			}
			set
			{
				this.m_SelectingUtilities.cursorIndex = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x0008D6B1 File Offset: 0x0008B8B1
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x0008D6C5 File Offset: 0x0008B8C5
		internal int selectIndex
		{
			get
			{
				TextSelectingUtilities selectingUtilities = this.m_SelectingUtilities;
				return (selectingUtilities != null) ? selectingUtilities.selectIndex : (-1);
			}
			set
			{
				this.m_SelectingUtilities.selectIndex = value;
			}
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x0008D6D4 File Offset: 0x0008B8D4
		private void OnRevealCursor()
		{
			this.m_TextElement.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0008D6E8 File Offset: 0x0008B8E8
		private void OnSelectIndexChange()
		{
			this.m_TextElement.IncrementVersion(VersionChangeType.Repaint);
			bool flag = this.HasSelection() && this.m_TextElement.focusController != null;
			if (flag)
			{
				this.m_TextElement.focusController.selectedTextElement = this.m_TextElement;
			}
			bool revealCursor = this.m_SelectingUtilities.revealCursor;
			if (revealCursor)
			{
				Action<bool> updateScrollOffset = this.m_TextElement.edition.UpdateScrollOffset;
				if (updateScrollOffset != null)
				{
					updateScrollOffset(false);
				}
			}
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x0008D768 File Offset: 0x0008B968
		private void OnCursorIndexChange()
		{
			this.m_TextElement.IncrementVersion(VersionChangeType.Repaint);
			bool flag = this.HasSelection() && this.m_TextElement.focusController != null;
			if (flag)
			{
				this.m_TextElement.focusController.selectedTextElement = this.m_TextElement;
			}
			bool revealCursor = this.m_SelectingUtilities.revealCursor;
			if (revealCursor)
			{
				Action<bool> updateScrollOffset = this.m_TextElement.edition.UpdateScrollOffset;
				if (updateScrollOffset != null)
				{
					updateScrollOffset(false);
				}
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x0008D7E8 File Offset: 0x0008B9E8
		internal bool RevealCursor()
		{
			return this.m_SelectingUtilities.revealCursor;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0008D808 File Offset: 0x0008BA08
		internal bool HasSelection()
		{
			return this.m_SelectingUtilities.hasSelection;
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x0008D828 File Offset: 0x0008BA28
		internal bool HasFocus()
		{
			return this.m_TextElement.hasFocus;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x0008D848 File Offset: 0x0008BA48
		internal void HandleEventBubbleUp(EventBase evt)
		{
			bool flag = evt is BlurEvent;
			if (flag)
			{
				this.m_TextElement.uitkTextHandle.RemoveTextInfoFromPermanentCache();
			}
			else
			{
				bool flag2 = (!(evt is PointerMoveEvent) && !(evt is MouseMoveEvent)) || this.isClicking;
				if (flag2)
				{
					this.m_TextElement.uitkTextHandle.AddTextInfoToPermanentCache();
				}
			}
			if (!(evt is FocusEvent))
			{
				if (!(evt is BlurEvent))
				{
					ValidateCommandEvent validateCommandEvent = evt as ValidateCommandEvent;
					if (validateCommandEvent == null)
					{
						ExecuteCommandEvent executeCommandEvent = evt as ExecuteCommandEvent;
						if (executeCommandEvent == null)
						{
							KeyDownEvent keyDownEvent = evt as KeyDownEvent;
							if (keyDownEvent == null)
							{
								PointerDownEvent pointerDownEvent = evt as PointerDownEvent;
								if (pointerDownEvent == null)
								{
									PointerMoveEvent pointerMoveEvent = evt as PointerMoveEvent;
									if (pointerMoveEvent == null)
									{
										PointerUpEvent pointerUpEvent = evt as PointerUpEvent;
										if (pointerUpEvent != null)
										{
											this.OnPointerUpEvent(pointerUpEvent);
										}
									}
									else
									{
										this.OnPointerMoveEvent(pointerMoveEvent);
									}
								}
								else
								{
									this.OnPointerDownEvent(pointerDownEvent);
								}
							}
							else
							{
								this.OnKeyDown(keyDownEvent);
							}
						}
						else
						{
							this.OnExecuteCommandEvent(executeCommandEvent);
						}
					}
					else
					{
						this.OnValidateCommandEvent(validateCommandEvent);
					}
				}
				else
				{
					this.OnBlurEvent();
				}
			}
			else
			{
				this.OnFocusEvent();
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0008D96C File Offset: 0x0008BB6C
		private void OnFocusEvent()
		{
			this.selectAllOnMouseUp = false;
			bool flag = this.isClicking || (this.m_TextElement.panel.contextType == ContextType.Editor && (Event.current == null || Event.current.type == EventType.Ignore));
			if (flag)
			{
				this.selectAllOnMouseUp = this.m_TextElement.selection.selectAllOnMouseUp;
			}
			this.m_SelectingUtilities.OnFocus(this.m_TextElement.selection.selectAllOnFocus && !this.isClicking);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0008D9FE File Offset: 0x0008BBFE
		private void OnBlurEvent()
		{
			this.selectAllOnMouseUp = this.m_TextElement.selection.selectAllOnMouseUp;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0008DA18 File Offset: 0x0008BC18
		private void OnKeyDown(KeyDownEvent evt)
		{
			bool flag = !this.m_TextElement.hasFocus;
			if (!flag)
			{
				evt.GetEquivalentImguiEvent(this.m_ImguiEvent);
				bool flag2 = this.m_SelectingUtilities.HandleKeyEvent(this.m_ImguiEvent);
				if (flag2)
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x0008DA64 File Offset: 0x0008BC64
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			Vector3 vector = evt.localPosition - this.m_TextElement.contentRect.min;
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = evt.timestamp - this.m_LastMouseDownTimeStamp < (long)Event.GetDoubleClickTime();
				if (flag2)
				{
					this.m_ConsecutiveMouseDownCount++;
				}
				else
				{
					this.m_ConsecutiveMouseDownCount = 1;
				}
				bool flag3 = this.m_ConsecutiveMouseDownCount == 2 && this.m_TextElement.selection.doubleClickSelectsWord;
				if (flag3)
				{
					bool flag4 = this.cursorIndex == 0 && this.cursorIndex != this.selectIndex;
					if (flag4)
					{
						this.m_SelectingUtilities.MoveCursorToPosition_Internal(vector, evt.shiftKey);
					}
					this.m_SelectingUtilities.SelectCurrentWord();
					this.m_SelectingUtilities.MouseDragSelectsWholeWords(true);
					this.m_SelectingUtilities.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
				}
				else
				{
					bool flag5 = this.m_ConsecutiveMouseDownCount == 3 && this.m_TextElement.selection.tripleClickSelectsLine;
					if (flag5)
					{
						this.m_SelectingUtilities.SelectCurrentParagraph();
						this.m_SelectingUtilities.MouseDragSelectsWholeWords(true);
						this.m_SelectingUtilities.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
					}
					else
					{
						this.m_SelectingUtilities.MoveCursorToPosition_Internal(vector, evt.shiftKey);
						Action<bool> updateScrollOffset = this.m_TextElement.edition.UpdateScrollOffset;
						if (updateScrollOffset != null)
						{
							updateScrollOffset(false);
						}
						this.m_SelectingUtilities.MouseDragSelectsWholeWords(false);
						this.m_SelectingUtilities.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
					}
				}
				this.m_LastMouseDownTimeStamp = evt.timestamp;
				this.isClicking = true;
				this.m_TextElement.CapturePointer(evt.pointerId);
				this.m_ClickStartPosition = vector;
				evt.StopPropagation();
			}
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0008DC37 File Offset: 0x0008BE37
		internal void ResetClickCount()
		{
			this.m_ConsecutiveMouseDownCount = 0;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0008DC44 File Offset: 0x0008BE44
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool flag = !this.isClicking;
			if (!flag)
			{
				Vector3 vector = evt.localPosition - this.m_TextElement.contentRect.min;
				this.m_Dragged = this.m_Dragged || this.MoveDistanceQualifiesForDrag(this.m_ClickStartPosition, vector);
				bool dragged = this.m_Dragged;
				if (dragged)
				{
					this.m_SelectingUtilities.SelectToPosition(vector);
					Action<bool> updateScrollOffset = this.m_TextElement.edition.UpdateScrollOffset;
					if (updateScrollOffset != null)
					{
						updateScrollOffset(false);
					}
					this.selectAllOnMouseUp = this.m_TextElement.selection.selectAllOnMouseUp && !this.m_SelectingUtilities.hasSelection;
				}
				evt.StopPropagation();
			}
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0008DD18 File Offset: 0x0008BF18
		private void OnPointerUpEvent(PointerUpEvent evt)
		{
			bool flag = evt.button != 0 || !this.isClicking;
			if (!flag)
			{
				bool flag2 = this.selectAllOnMouseUp;
				if (flag2)
				{
					this.m_SelectingUtilities.SelectAll();
				}
				this.selectAllOnMouseUp = false;
				this.m_Dragged = false;
				this.isClicking = false;
				this.m_TextElement.ReleasePointer(evt.pointerId);
				evt.StopPropagation();
			}
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x0008DD88 File Offset: 0x0008BF88
		private void OnValidateCommandEvent(ValidateCommandEvent evt)
		{
			bool flag = !this.m_TextElement.hasFocus;
			if (!flag)
			{
				string commandName = evt.commandName;
				string text = commandName;
				if (!(text == "Cut") && !(text == "Paste") && !(text == "Delete") && !(text == "UndoRedoPerformed"))
				{
					if (!(text == "Copy"))
					{
						if (!(text == "SelectAll"))
						{
						}
					}
					else
					{
						bool flag2 = !this.m_SelectingUtilities.hasSelection;
						if (flag2)
						{
							return;
						}
					}
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x0008DE24 File Offset: 0x0008C024
		private void OnExecuteCommandEvent(ExecuteCommandEvent evt)
		{
			bool flag = !this.m_TextElement.hasFocus;
			if (!flag)
			{
				string commandName = evt.commandName;
				string text = commandName;
				if (!(text == "OnLostFocus"))
				{
					if (!(text == "Copy"))
					{
						if (text == "SelectAll")
						{
							this.m_SelectingUtilities.SelectAll();
							evt.StopPropagation();
						}
					}
					else
					{
						this.m_SelectingUtilities.Copy();
						evt.StopPropagation();
					}
				}
				else
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0008DEAC File Offset: 0x0008C0AC
		private bool MoveDistanceQualifiesForDrag(Vector2 start, Vector2 current)
		{
			return (start - current).sqrMagnitude >= 16f;
		}

		// Token: 0x0400117E RID: 4478
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal TextSelectingUtilities m_SelectingUtilities;

		// Token: 0x0400117F RID: 4479
		private bool selectAllOnMouseUp;

		// Token: 0x04001180 RID: 4480
		private TextElement m_TextElement;

		// Token: 0x04001181 RID: 4481
		private Vector2 m_ClickStartPosition;

		// Token: 0x04001182 RID: 4482
		private bool m_Dragged;

		// Token: 0x04001183 RID: 4483
		private bool m_IsClicking;

		// Token: 0x04001184 RID: 4484
		private const int k_DragThresholdSqr = 16;

		// Token: 0x04001185 RID: 4485
		private int m_ConsecutiveMouseDownCount;

		// Token: 0x04001186 RID: 4486
		private long m_LastMouseDownTimeStamp = 0L;

		// Token: 0x04001187 RID: 4487
		private readonly Event m_ImguiEvent = new Event();
	}
}
