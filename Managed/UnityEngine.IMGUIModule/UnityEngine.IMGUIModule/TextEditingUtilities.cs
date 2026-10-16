using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x02000046 RID: 70
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class TextEditingUtilities
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00012527 File Offset: 0x00010727
		private bool hasSelection
		{
			get
			{
				return this.m_TextSelectingUtility.hasSelection;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00012534 File Offset: 0x00010734
		private string SelectedText
		{
			get
			{
				return this.m_TextSelectingUtility.selectedText;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00012541 File Offset: 0x00010741
		private int m_iAltCursorPos
		{
			get
			{
				return this.m_TextSelectingUtility.iAltCursorPos;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001254E File Offset: 0x0001074E
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001255B File Offset: 0x0001075B
		internal bool revealCursor
		{
			get
			{
				return this.m_TextSelectingUtility.revealCursor;
			}
			set
			{
				this.m_TextSelectingUtility.revealCursor = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001256A File Offset: 0x0001076A
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0001257D File Offset: 0x0001077D
		internal int stringCursorIndex
		{
			get
			{
				return this.textHandle.GetCorrespondingStringIndex(this.cursorIndex);
			}
			set
			{
				this.cursorIndex = this.textHandle.GetCorrespondingCodePointIndex(value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00012592 File Offset: 0x00010792
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001259F File Offset: 0x0001079F
		private int cursorIndex
		{
			get
			{
				return this.m_TextSelectingUtility.cursorIndex;
			}
			set
			{
				this.m_TextSelectingUtility.cursorIndex = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000125AE File Offset: 0x000107AE
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x000125BB File Offset: 0x000107BB
		private int cursorIndexNoValidation
		{
			get
			{
				return this.m_TextSelectingUtility.cursorIndexNoValidation;
			}
			set
			{
				this.m_TextSelectingUtility.cursorIndexNoValidation = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x000125CA File Offset: 0x000107CA
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x000125D7 File Offset: 0x000107D7
		private int selectIndexNoValidation
		{
			get
			{
				return this.m_TextSelectingUtility.selectIndexNoValidation;
			}
			set
			{
				this.m_TextSelectingUtility.selectIndexNoValidation = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000125E6 File Offset: 0x000107E6
		private int stringCursorIndexNoValidation
		{
			get
			{
				return this.textHandle.GetCorrespondingStringIndex(this.m_TextSelectingUtility.cursorIndexNoValidation);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000125FE File Offset: 0x000107FE
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00012611 File Offset: 0x00010811
		internal int stringSelectIndex
		{
			get
			{
				return this.textHandle.GetCorrespondingStringIndex(this.selectIndex);
			}
			set
			{
				this.selectIndex = this.textHandle.GetCorrespondingCodePointIndex(value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00012626 File Offset: 0x00010826
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00012633 File Offset: 0x00010833
		private int selectIndex
		{
			get
			{
				return this.m_TextSelectingUtility.selectIndex;
			}
			set
			{
				this.m_TextSelectingUtility.selectIndex = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00012642 File Offset: 0x00010842
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0001264C File Offset: 0x0001084C
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				bool flag = value == this.m_Text;
				if (!flag)
				{
					this.m_Text = value ?? string.Empty;
					Action onTextChanged = this.OnTextChanged;
					if (onTextChanged != null)
					{
						onTextChanged();
					}
				}
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001268E File Offset: 0x0001088E
		internal void SetTextWithoutNotify(string value)
		{
			this.m_Text = value;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00012698 File Offset: 0x00010898
		public TextEditingUtilities(TextSelectingUtilities selectingUtilities, TextHandle textHandle, string text)
		{
			this.m_TextSelectingUtility = selectingUtilities;
			this.textHandle = textHandle;
			this.m_Text = text;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000126C8 File Offset: 0x000108C8
		public bool UpdateImeState()
		{
			bool flag = GUIUtility.compositionString.Length > 0;
			if (flag)
			{
				bool flag2 = !this.isCompositionActive;
				if (flag2)
				{
					this.m_UpdateImeWindowPosition = true;
					this.ReplaceSelection(string.Empty);
				}
				this.isCompositionActive = true;
			}
			else
			{
				this.isCompositionActive = false;
			}
			return this.isCompositionActive;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00012728 File Offset: 0x00010928
		public bool ShouldUpdateImeWindowPosition()
		{
			return this.m_UpdateImeWindowPosition;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00012740 File Offset: 0x00010940
		public void SetImeWindowPosition(Vector2 worldPosition)
		{
			Vector2 cursorPositionFromStringIndexUsingCharacterHeight = this.textHandle.GetCursorPositionFromStringIndexUsingCharacterHeight(this.cursorIndex, true);
			GUIUtility.compositionCursorPos = worldPosition + cursorPositionFromStringIndexUsingCharacterHeight;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00012770 File Offset: 0x00010970
		public string GeneratePreviewString(bool richText)
		{
			this.RestoreCursorState();
			string compositionString = GUIUtility.compositionString;
			bool flag = this.isCompositionActive;
			string text;
			if (flag)
			{
				text = (richText ? this.text.Insert(this.stringCursorIndex, "<u>" + compositionString + "</u>") : this.text.Insert(this.stringCursorIndex, compositionString));
			}
			else
			{
				text = this.text;
			}
			return text;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000127DC File Offset: 0x000109DC
		public void EnableCursorPreviewState()
		{
			bool flag = this.m_CursorIndexSavedState != -1;
			if (!flag)
			{
				this.m_CursorIndexSavedState = this.m_TextSelectingUtility.cursorIndexNoValidation;
				this.cursorIndexNoValidation = (this.selectIndexNoValidation = this.m_CursorIndexSavedState + GUIUtility.compositionString.Length);
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00012830 File Offset: 0x00010A30
		public void RestoreCursorState()
		{
			bool flag = this.m_CursorIndexSavedState == -1;
			if (!flag)
			{
				this.cursorIndex = (this.selectIndex = this.m_CursorIndexSavedState);
				this.m_CursorIndexSavedState = -1;
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001286C File Offset: 0x00010A6C
		[VisibleToOtherModules]
		internal bool HandleKeyEvent(Event e)
		{
			this.RestoreCursorState();
			this.InitKeyActions();
			EventModifiers modifiers = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			bool flag = TextEditingUtilities.s_KeyEditOps.ContainsKey(e);
			bool flag2;
			if (flag)
			{
				TextEditOp textEditOp = TextEditingUtilities.s_KeyEditOps[e];
				this.PerformOperation(textEditOp);
				e.modifiers = modifiers;
				flag2 = true;
			}
			else
			{
				e.modifiers = modifiers;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000128DC File Offset: 0x00010ADC
		private void PerformOperation(TextEditOp operation)
		{
			this.revealCursor = true;
			switch (operation)
			{
			case TextEditOp.MoveLeft:
				this.m_TextSelectingUtility.MoveLeft();
				return;
			case TextEditOp.MoveRight:
				this.m_TextSelectingUtility.MoveRight();
				return;
			case TextEditOp.MoveUp:
				this.m_TextSelectingUtility.MoveUp();
				return;
			case TextEditOp.MoveDown:
				this.m_TextSelectingUtility.MoveDown();
				return;
			case TextEditOp.MoveLineStart:
				this.m_TextSelectingUtility.MoveLineStart();
				return;
			case TextEditOp.MoveLineEnd:
				this.m_TextSelectingUtility.MoveLineEnd();
				return;
			case TextEditOp.MoveTextStart:
				this.m_TextSelectingUtility.MoveTextStart();
				return;
			case TextEditOp.MoveTextEnd:
				this.m_TextSelectingUtility.MoveTextEnd();
				return;
			case TextEditOp.MoveGraphicalLineStart:
				this.m_TextSelectingUtility.MoveGraphicalLineStart();
				return;
			case TextEditOp.MoveGraphicalLineEnd:
				this.m_TextSelectingUtility.MoveGraphicalLineEnd();
				return;
			case TextEditOp.MoveWordLeft:
				this.m_TextSelectingUtility.MoveWordLeft();
				return;
			case TextEditOp.MoveWordRight:
				this.m_TextSelectingUtility.MoveWordRight();
				return;
			case TextEditOp.MoveParagraphForward:
				this.m_TextSelectingUtility.MoveParagraphForward();
				return;
			case TextEditOp.MoveParagraphBackward:
				this.m_TextSelectingUtility.MoveParagraphBackward();
				return;
			case TextEditOp.MoveToStartOfNextWord:
				this.m_TextSelectingUtility.MoveToStartOfNextWord();
				return;
			case TextEditOp.MoveToEndOfPreviousWord:
				this.m_TextSelectingUtility.MoveToEndOfPreviousWord();
				return;
			case TextEditOp.Delete:
				this.Delete();
				return;
			case TextEditOp.Backspace:
				this.Backspace();
				return;
			case TextEditOp.DeleteWordBack:
				this.DeleteWordBack();
				return;
			case TextEditOp.DeleteWordForward:
				this.DeleteWordForward();
				return;
			case TextEditOp.DeleteLineBack:
				this.DeleteLineBack();
				return;
			case TextEditOp.Cut:
				this.Cut();
				return;
			case TextEditOp.Paste:
				this.Paste();
				return;
			}
			Debug.Log("Unimplemented: " + operation.ToString());
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00012ACA File Offset: 0x00010CCA
		private static void MapKey(string key, TextEditOp action)
		{
			TextEditingUtilities.s_KeyEditOps[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00012AE0 File Offset: 0x00010CE0
		private void InitKeyActions()
		{
			bool flag = TextEditingUtilities.s_KeyEditOps != null;
			if (!flag)
			{
				TextEditingUtilities.s_KeyEditOps = new Dictionary<Event, TextEditOp>();
				TextEditingUtilities.MapKey("left", TextEditOp.MoveLeft);
				TextEditingUtilities.MapKey("right", TextEditOp.MoveRight);
				TextEditingUtilities.MapKey("up", TextEditOp.MoveUp);
				TextEditingUtilities.MapKey("down", TextEditOp.MoveDown);
				TextEditingUtilities.MapKey("delete", TextEditOp.Delete);
				TextEditingUtilities.MapKey("backspace", TextEditOp.Backspace);
				TextEditingUtilities.MapKey("#backspace", TextEditOp.Backspace);
				bool flag2 = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX;
				if (flag2)
				{
					TextEditingUtilities.MapKey("^left", TextEditOp.MoveGraphicalLineStart);
					TextEditingUtilities.MapKey("^right", TextEditOp.MoveGraphicalLineEnd);
					TextEditingUtilities.MapKey("&left", TextEditOp.MoveWordLeft);
					TextEditingUtilities.MapKey("&right", TextEditOp.MoveWordRight);
					TextEditingUtilities.MapKey("&up", TextEditOp.MoveParagraphBackward);
					TextEditingUtilities.MapKey("&down", TextEditOp.MoveParagraphForward);
					TextEditingUtilities.MapKey("%left", TextEditOp.MoveGraphicalLineStart);
					TextEditingUtilities.MapKey("%right", TextEditOp.MoveGraphicalLineEnd);
					TextEditingUtilities.MapKey("%up", TextEditOp.MoveTextStart);
					TextEditingUtilities.MapKey("%down", TextEditOp.MoveTextEnd);
					TextEditingUtilities.MapKey("%x", TextEditOp.Cut);
					TextEditingUtilities.MapKey("%v", TextEditOp.Paste);
					TextEditingUtilities.MapKey("^d", TextEditOp.Delete);
					TextEditingUtilities.MapKey("^h", TextEditOp.Backspace);
					TextEditingUtilities.MapKey("^b", TextEditOp.MoveLeft);
					TextEditingUtilities.MapKey("^f", TextEditOp.MoveRight);
					TextEditingUtilities.MapKey("^a", TextEditOp.MoveLineStart);
					TextEditingUtilities.MapKey("^e", TextEditOp.MoveLineEnd);
					TextEditingUtilities.MapKey("&delete", TextEditOp.DeleteWordForward);
					TextEditingUtilities.MapKey("&backspace", TextEditOp.DeleteWordBack);
					TextEditingUtilities.MapKey("%backspace", TextEditOp.DeleteLineBack);
				}
				else
				{
					TextEditingUtilities.MapKey("home", TextEditOp.MoveGraphicalLineStart);
					TextEditingUtilities.MapKey("end", TextEditOp.MoveGraphicalLineEnd);
					TextEditingUtilities.MapKey("%left", TextEditOp.MoveWordLeft);
					TextEditingUtilities.MapKey("%right", TextEditOp.MoveWordRight);
					TextEditingUtilities.MapKey("%up", TextEditOp.MoveParagraphBackward);
					TextEditingUtilities.MapKey("%down", TextEditOp.MoveParagraphForward);
					TextEditingUtilities.MapKey("^left", TextEditOp.MoveToEndOfPreviousWord);
					TextEditingUtilities.MapKey("^right", TextEditOp.MoveToStartOfNextWord);
					TextEditingUtilities.MapKey("^up", TextEditOp.MoveParagraphBackward);
					TextEditingUtilities.MapKey("^down", TextEditOp.MoveParagraphForward);
					TextEditingUtilities.MapKey("^delete", TextEditOp.DeleteWordForward);
					TextEditingUtilities.MapKey("^backspace", TextEditOp.DeleteWordBack);
					TextEditingUtilities.MapKey("%backspace", TextEditOp.DeleteLineBack);
					TextEditingUtilities.MapKey("^x", TextEditOp.Cut);
					TextEditingUtilities.MapKey("^v", TextEditOp.Paste);
					TextEditingUtilities.MapKey("#delete", TextEditOp.Cut);
					TextEditingUtilities.MapKey("#insert", TextEditOp.Paste);
				}
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00012D60 File Offset: 0x00010F60
		public bool DeleteLineBack()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				bool useAdvancedText = this.textHandle.useAdvancedText;
				if (useAdvancedText)
				{
					int firstCharacterIndexOnLine = this.textHandle.GetFirstCharacterIndexOnLine(this.cursorIndex);
					bool flag2 = firstCharacterIndexOnLine != this.cursorIndex;
					if (flag2)
					{
						this.text = this.text.Remove(firstCharacterIndexOnLine, this.stringCursorIndex - firstCharacterIndexOnLine);
						this.cursorIndex = (this.selectIndex = firstCharacterIndexOnLine);
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					LineInfo lineInfoFromCharacterIndex = this.textHandle.GetLineInfoFromCharacterIndex(this.cursorIndex);
					int firstCharacterIndex = lineInfoFromCharacterIndex.firstCharacterIndex;
					int correspondingStringIndex = this.textHandle.GetCorrespondingStringIndex(firstCharacterIndex);
					bool flag3 = firstCharacterIndex != this.cursorIndex;
					if (flag3)
					{
						this.text = this.text.Remove(correspondingStringIndex, this.stringCursorIndex - correspondingStringIndex);
						this.cursorIndex = (this.selectIndex = firstCharacterIndex);
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012E78 File Offset: 0x00011078
		public bool DeleteWordBack()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				int num = this.m_TextSelectingUtility.FindEndOfPreviousWord(this.cursorIndex);
				bool flag2 = this.cursorIndex != num;
				if (flag2)
				{
					int correspondingStringIndex = this.textHandle.GetCorrespondingStringIndex(num);
					this.text = this.text.Remove(correspondingStringIndex, this.stringCursorIndex - correspondingStringIndex);
					this.selectIndex = (this.cursorIndex = num);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00012F10 File Offset: 0x00011110
		public bool DeleteWordForward()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				int num = this.m_TextSelectingUtility.FindStartOfNextWord(this.cursorIndex);
				bool flag2 = this.cursorIndex < this.text.Length;
				if (flag2)
				{
					int correspondingStringIndex = this.textHandle.GetCorrespondingStringIndex(num);
					this.text = this.text.Remove(this.stringCursorIndex, correspondingStringIndex - this.stringCursorIndex);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00012FA0 File Offset: 0x000111A0
		public bool Delete()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				bool flag2 = this.stringCursorIndex < this.text.Length;
				if (flag2)
				{
					bool useAdvancedText = this.textHandle.useAdvancedText;
					int num;
					if (useAdvancedText)
					{
						num = Mathf.Abs(this.textHandle.NextCodePointIndex(this.cursorIndex) - this.cursorIndex);
					}
					else
					{
						num = this.textHandle.textInfo.textElementInfo[this.cursorIndex].stringLength;
					}
					this.text = this.text.Remove(this.stringCursorIndex, num);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001305C File Offset: 0x0001125C
		public bool Backspace()
		{
			this.RestoreCursorState();
			bool hasSelection = this.hasSelection;
			bool flag;
			if (hasSelection)
			{
				this.DeleteSelection();
				flag = true;
			}
			else
			{
				bool flag2 = this.cursorIndex > 0;
				if (flag2)
				{
					int num = this.m_TextSelectingUtility.PreviousCodePointIndex(this.cursorIndex);
					bool useAdvancedText = this.textHandle.useAdvancedText;
					int num2;
					if (useAdvancedText)
					{
						num2 = Mathf.Abs(this.cursorIndex - num);
					}
					else
					{
						num2 = this.textHandle.textInfo.textElementInfo[this.cursorIndex - 1].stringLength;
					}
					this.text = this.text.Remove(this.stringCursorIndex - num2, num2);
					this.cursorIndex = (this.textHandle.useAdvancedText ? Math.Max(0, this.cursorIndex - num2) : num);
					this.selectIndex = (this.textHandle.useAdvancedText ? Math.Max(0, this.selectIndex - num2) : num);
					this.m_TextSelectingUtility.ClearCursorPos();
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00013174 File Offset: 0x00011374
		public bool DeleteSelection()
		{
			bool flag = this.cursorIndex == this.selectIndex;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.cursorIndex < this.selectIndex;
				if (flag3)
				{
					this.text = this.text.Substring(0, this.stringCursorIndex) + this.text.Substring(this.stringSelectIndex, this.text.Length - this.stringSelectIndex);
					this.selectIndex = this.cursorIndex;
				}
				else
				{
					this.text = this.text.Substring(0, this.stringSelectIndex) + this.text.Substring(this.stringCursorIndex, this.text.Length - this.stringCursorIndex);
					this.cursorIndex = this.selectIndex;
				}
				this.m_TextSelectingUtility.ClearCursorPos();
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00013260 File Offset: 0x00011460
		public void ReplaceSelection(string replace)
		{
			this.RestoreCursorState();
			this.DeleteSelection();
			this.text = this.text.Insert(this.stringCursorIndex, replace);
			int num = (this.textHandle.useAdvancedText ? replace.Length : new StringInfo(replace).LengthInTextElements);
			int num2 = this.cursorIndexNoValidation + num;
			this.cursorIndexNoValidation = num2;
			this.selectIndexNoValidation = num2;
			this.m_TextSelectingUtility.ClearCursorPos();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000132DC File Offset: 0x000114DC
		public bool Insert(char c)
		{
			bool flag = char.IsHighSurrogate(c);
			bool flag2;
			if (flag)
			{
				this.m_HighSurrogate = c;
				flag2 = false;
			}
			else
			{
				bool flag3 = char.IsLowSurrogate(c);
				if (flag3)
				{
					char c2 = c;
					string text = new string(new char[] { this.m_HighSurrogate, c2 });
					this.ReplaceSelection(text.ToString());
					flag2 = true;
				}
				else
				{
					this.ReplaceSelection(c.ToString());
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001334C File Offset: 0x0001154C
		public void MoveSelectionToAltCursor()
		{
			this.RestoreCursorState();
			bool flag = this.m_iAltCursorPos == -1;
			if (!flag)
			{
				int iAltCursorPos = this.m_iAltCursorPos;
				string selectedText = this.SelectedText;
				this.text = this.text.Insert(iAltCursorPos, selectedText);
				bool flag2 = iAltCursorPos < this.cursorIndex;
				if (flag2)
				{
					this.cursorIndex += selectedText.Length;
					this.selectIndex += selectedText.Length;
				}
				this.DeleteSelection();
				this.selectIndex = (this.cursorIndex = iAltCursorPos);
				this.m_TextSelectingUtility.ClearCursorPos();
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000133F4 File Offset: 0x000115F4
		public bool CanPaste()
		{
			return GUIUtility.systemCopyBuffer.Length != 0;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00013414 File Offset: 0x00011614
		public bool Cut()
		{
			this.m_TextSelectingUtility.Copy();
			return this.DeleteSelection();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00013438 File Offset: 0x00011638
		public bool Paste()
		{
			this.RestoreCursorState();
			string text = GUIUtility.systemCopyBuffer;
			bool flag = text != "";
			bool flag3;
			if (flag)
			{
				bool flag2 = !this.multiline;
				if (flag2)
				{
					text = TextEditingUtilities.ReplaceNewlinesWithSpaces(text);
				}
				this.ReplaceSelection(text);
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00013488 File Offset: 0x00011688
		private static string ReplaceNewlinesWithSpaces(string value)
		{
			value = value.Replace("\r\n", " ");
			value = value.Replace('\n', ' ');
			value = value.Replace('\r', ' ');
			return value;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000134C5 File Offset: 0x000116C5
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void OnBlur()
		{
			this.revealCursor = false;
			this.isCompositionActive = false;
			this.RestoreCursorState();
			this.m_TextSelectingUtility.SelectNone();
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000134EC File Offset: 0x000116EC
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal bool TouchScreenKeyboardCanBeUsed()
		{
			RuntimePlatform platform = Application.platform;
			RuntimePlatform runtimePlatform = platform;
			bool flag;
			if (runtimePlatform != RuntimePlatform.Android && runtimePlatform - RuntimePlatform.WebGLPlayer > 3)
			{
				flag = TouchScreenKeyboard.isSupported;
			}
			else
			{
				flag = !TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
			return flag;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00013528 File Offset: 0x00011728
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal bool PhysicalKeyboardCanBeUsed()
		{
			return !TouchScreenKeyboard.isSupported || TouchScreenKeyboard.isInPlaceEditingAllowed;
		}

		// Token: 0x04000189 RID: 393
		private TextSelectingUtilities m_TextSelectingUtility;

		// Token: 0x0400018A RID: 394
		internal TextHandle textHandle;

		// Token: 0x0400018B RID: 395
		private int m_CursorIndexSavedState = -1;

		// Token: 0x0400018C RID: 396
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal bool isCompositionActive;

		// Token: 0x0400018D RID: 397
		private bool m_UpdateImeWindowPosition;

		// Token: 0x0400018E RID: 398
		internal Action OnTextChanged;

		// Token: 0x0400018F RID: 399
		public bool multiline = false;

		// Token: 0x04000190 RID: 400
		private string m_Text;

		// Token: 0x04000191 RID: 401
		private static Dictionary<Event, TextEditOp> s_KeyEditOps;

		// Token: 0x04000192 RID: 402
		private char m_HighSurrogate;
	}
}
