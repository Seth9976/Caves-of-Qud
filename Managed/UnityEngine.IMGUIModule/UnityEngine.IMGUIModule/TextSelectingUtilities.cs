using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x02000049 RID: 73
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
	internal class TextSelectingUtilities
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00014615 File Offset: 0x00012815
		public bool hasSelection
		{
			get
			{
				return this.cursorIndex != this.selectIndex;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00014628 File Offset: 0x00012828
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00014630 File Offset: 0x00012830
		public bool revealCursor
		{
			get
			{
				return this.m_RevealCursor;
			}
			set
			{
				bool flag = this.m_RevealCursor != value;
				if (flag)
				{
					this.m_RevealCursor = value;
					Action onRevealCursorChange = this.OnRevealCursorChange;
					if (onRevealCursorChange != null)
					{
						onRevealCursorChange();
					}
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00014669 File Offset: 0x00012869
		private int m_CharacterCount
		{
			get
			{
				return this.textHandle.characterCount;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00014678 File Offset: 0x00012878
		private int characterCount
		{
			get
			{
				return (!this.textHandle.useAdvancedText && this.m_CharacterCount > 0 && this.textHandle.textInfo.textElementInfo[this.m_CharacterCount - 1].character == 8203U) ? (this.m_CharacterCount - 1) : this.m_CharacterCount;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000146D4 File Offset: 0x000128D4
		private TextElementInfo[] m_TextElementInfos
		{
			get
			{
				return this.textHandle.textInfo.textElementInfo;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000146E6 File Offset: 0x000128E6
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00014704 File Offset: 0x00012904
		public int cursorIndex
		{
			get
			{
				return this.textHandle.IsPlaceholder ? 0 : this.ClampTextIndex(this.m_CursorIndex);
			}
			set
			{
				bool flag = this.m_CursorIndex != value;
				if (flag)
				{
					this.m_CursorIndex = value;
					Action onCursorIndexChange = this.OnCursorIndexChange;
					if (onCursorIndexChange != null)
					{
						onCursorIndexChange();
					}
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00014740 File Offset: 0x00012940
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00014758 File Offset: 0x00012958
		internal int cursorIndexNoValidation
		{
			get
			{
				return this.m_CursorIndex;
			}
			set
			{
				bool flag = this.m_CursorIndex != value;
				if (flag)
				{
					this.SetCursorIndexWithoutNotify(value);
					Action onCursorIndexChange = this.OnCursorIndexChange;
					if (onCursorIndexChange != null)
					{
						onCursorIndexChange();
					}
				}
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00014792 File Offset: 0x00012992
		internal void SetCursorIndexWithoutNotify(int index)
		{
			this.m_CursorIndex = index;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0001479C File Offset: 0x0001299C
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x000147BC File Offset: 0x000129BC
		public int selectIndex
		{
			get
			{
				return this.textHandle.IsPlaceholder ? 0 : this.ClampTextIndex(this.m_SelectIndex);
			}
			set
			{
				bool flag = this.m_SelectIndex != value;
				if (flag)
				{
					this.SetSelectIndexWithoutNotify(value);
					Action onSelectIndexChange = this.OnSelectIndexChange;
					if (onSelectIndexChange != null)
					{
						onSelectIndexChange();
					}
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000147F8 File Offset: 0x000129F8
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00014810 File Offset: 0x00012A10
		internal int selectIndexNoValidation
		{
			get
			{
				return this.m_SelectIndex;
			}
			set
			{
				bool flag = this.m_SelectIndex != value;
				if (flag)
				{
					this.SetSelectIndexWithoutNotify(value);
					Action onSelectIndexChange = this.OnSelectIndexChange;
					if (onSelectIndexChange != null)
					{
						onSelectIndexChange();
					}
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001484A File Offset: 0x00012A4A
		internal void SetSelectIndexWithoutNotify(int index)
		{
			this.m_SelectIndex = index;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00014854 File Offset: 0x00012A54
		public string selectedText
		{
			get
			{
				bool flag = this.cursorIndex == this.selectIndex;
				string text;
				if (flag)
				{
					text = "";
				}
				else
				{
					bool flag2 = this.cursorIndex < this.selectIndex;
					if (flag2)
					{
						text = this.textHandle.Substring(this.cursorIndex, this.selectIndex - this.cursorIndex);
					}
					else
					{
						text = this.textHandle.Substring(this.selectIndex, this.cursorIndex - this.selectIndex);
					}
				}
				return text;
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000148D4 File Offset: 0x00012AD4
		public TextSelectingUtilities(TextHandle textHandle)
		{
			this.textHandle = textHandle;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00014930 File Offset: 0x00012B30
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal bool HandleKeyEvent(Event e)
		{
			this.InitKeyActions();
			EventModifiers modifiers = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			bool flag = TextSelectingUtilities.s_KeySelectOps.ContainsKey(e);
			bool flag2;
			if (flag)
			{
				TextSelectOp textSelectOp = TextSelectingUtilities.s_KeySelectOps[e];
				this.PerformOperation(textSelectOp);
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

		// Token: 0x0600056E RID: 1390 RVA: 0x0001499C File Offset: 0x00012B9C
		private bool PerformOperation(TextSelectOp operation)
		{
			switch (operation)
			{
			case TextSelectOp.SelectLeft:
				this.SelectLeft();
				return false;
			case TextSelectOp.SelectRight:
				this.SelectRight();
				return false;
			case TextSelectOp.SelectUp:
				this.SelectUp();
				return false;
			case TextSelectOp.SelectDown:
				this.SelectDown();
				return false;
			case TextSelectOp.SelectTextStart:
				this.SelectTextStart();
				return false;
			case TextSelectOp.SelectTextEnd:
				this.SelectTextEnd();
				return false;
			case TextSelectOp.ExpandSelectGraphicalLineStart:
				this.ExpandSelectGraphicalLineStart();
				return false;
			case TextSelectOp.ExpandSelectGraphicalLineEnd:
				this.ExpandSelectGraphicalLineEnd();
				return false;
			case TextSelectOp.SelectGraphicalLineStart:
				this.SelectGraphicalLineStart();
				return false;
			case TextSelectOp.SelectGraphicalLineEnd:
				this.SelectGraphicalLineEnd();
				return false;
			case TextSelectOp.SelectWordLeft:
				this.SelectWordLeft();
				return false;
			case TextSelectOp.SelectWordRight:
				this.SelectWordRight();
				return false;
			case TextSelectOp.SelectToEndOfPreviousWord:
				this.SelectToEndOfPreviousWord();
				return false;
			case TextSelectOp.SelectToStartOfNextWord:
				this.SelectToStartOfNextWord();
				return false;
			case TextSelectOp.SelectParagraphBackward:
				this.SelectParagraphBackward();
				return false;
			case TextSelectOp.SelectParagraphForward:
				this.SelectParagraphForward();
				return false;
			case TextSelectOp.Copy:
				this.Copy();
				return false;
			case TextSelectOp.SelectAll:
				this.SelectAll();
				return false;
			case TextSelectOp.SelectNone:
				this.SelectNone();
				return false;
			}
			Debug.Log("Unimplemented: " + operation.ToString());
			return false;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00014AF4 File Offset: 0x00012CF4
		private static void MapKey(string key, TextSelectOp action)
		{
			TextSelectingUtilities.s_KeySelectOps[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00014B0C File Offset: 0x00012D0C
		private void InitKeyActions()
		{
			bool flag = TextSelectingUtilities.s_KeySelectOps != null;
			if (!flag)
			{
				TextSelectingUtilities.s_KeySelectOps = new Dictionary<Event, TextSelectOp>();
				TextSelectingUtilities.MapKey("#left", TextSelectOp.SelectLeft);
				TextSelectingUtilities.MapKey("#right", TextSelectOp.SelectRight);
				TextSelectingUtilities.MapKey("#up", TextSelectOp.SelectUp);
				TextSelectingUtilities.MapKey("#down", TextSelectOp.SelectDown);
				bool flag2 = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX;
				if (flag2)
				{
					TextSelectingUtilities.MapKey("#home", TextSelectOp.SelectTextStart);
					TextSelectingUtilities.MapKey("#end", TextSelectOp.SelectTextEnd);
					TextSelectingUtilities.MapKey("#^left", TextSelectOp.ExpandSelectGraphicalLineStart);
					TextSelectingUtilities.MapKey("#^right", TextSelectOp.ExpandSelectGraphicalLineEnd);
					TextSelectingUtilities.MapKey("#^up", TextSelectOp.SelectParagraphBackward);
					TextSelectingUtilities.MapKey("#^down", TextSelectOp.SelectParagraphForward);
					TextSelectingUtilities.MapKey("#&left", TextSelectOp.SelectWordLeft);
					TextSelectingUtilities.MapKey("#&right", TextSelectOp.SelectWordRight);
					TextSelectingUtilities.MapKey("#&up", TextSelectOp.SelectParagraphBackward);
					TextSelectingUtilities.MapKey("#&down", TextSelectOp.SelectParagraphForward);
					TextSelectingUtilities.MapKey("#%left", TextSelectOp.ExpandSelectGraphicalLineStart);
					TextSelectingUtilities.MapKey("#%right", TextSelectOp.ExpandSelectGraphicalLineEnd);
					TextSelectingUtilities.MapKey("#%up", TextSelectOp.SelectTextStart);
					TextSelectingUtilities.MapKey("#%down", TextSelectOp.SelectTextEnd);
					TextSelectingUtilities.MapKey("%a", TextSelectOp.SelectAll);
					TextSelectingUtilities.MapKey("%c", TextSelectOp.Copy);
				}
				else
				{
					TextSelectingUtilities.MapKey("#^left", TextSelectOp.SelectToEndOfPreviousWord);
					TextSelectingUtilities.MapKey("#^right", TextSelectOp.SelectToStartOfNextWord);
					TextSelectingUtilities.MapKey("#^up", TextSelectOp.SelectParagraphBackward);
					TextSelectingUtilities.MapKey("#^down", TextSelectOp.SelectParagraphForward);
					TextSelectingUtilities.MapKey("#home", TextSelectOp.SelectGraphicalLineStart);
					TextSelectingUtilities.MapKey("#end", TextSelectOp.SelectGraphicalLineEnd);
					TextSelectingUtilities.MapKey("^a", TextSelectOp.SelectAll);
					TextSelectingUtilities.MapKey("^c", TextSelectOp.Copy);
					TextSelectingUtilities.MapKey("^insert", TextSelectOp.Copy);
				}
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00014CB9 File Offset: 0x00012EB9
		public void ClearCursorPos()
		{
			this.hasHorizontalCursorPos = false;
			this.iAltCursorPos = -1;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00014CCC File Offset: 0x00012ECC
		public void OnFocus(bool selectAll = true)
		{
			if (selectAll)
			{
				this.SelectAll();
			}
			this.revealCursor = true;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00014CEE File Offset: 0x00012EEE
		public void SelectAll()
		{
			this.cursorIndex = 0;
			this.selectIndex = int.MaxValue;
			this.ClearCursorPos();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014D0C File Offset: 0x00012F0C
		public void SelectNone()
		{
			this.selectIndex = this.cursorIndex;
			this.ClearCursorPos();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00014D24 File Offset: 0x00012F24
		public void SelectLeft()
		{
			bool bJustSelected = this.m_bJustSelected;
			if (bJustSelected)
			{
				bool flag = this.cursorIndex > this.selectIndex;
				if (flag)
				{
					int cursorIndex = this.cursorIndex;
					this.cursorIndex = this.selectIndex;
					this.selectIndex = cursorIndex;
				}
			}
			this.m_bJustSelected = false;
			this.cursorIndex = this.PreviousCodePointIndex(this.cursorIndex);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00014D88 File Offset: 0x00012F88
		public void SelectRight()
		{
			bool bJustSelected = this.m_bJustSelected;
			if (bJustSelected)
			{
				bool flag = this.cursorIndex < this.selectIndex;
				if (flag)
				{
					int cursorIndex = this.cursorIndex;
					this.cursorIndex = this.selectIndex;
					this.selectIndex = cursorIndex;
				}
			}
			this.m_bJustSelected = false;
			this.cursorIndex = this.NextCodePointIndex(this.cursorIndex);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00014DEA File Offset: 0x00012FEA
		public void SelectUp()
		{
			this.cursorIndex = this.textHandle.LineUpCharacterPosition(this.cursorIndex);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00014E05 File Offset: 0x00013005
		public void SelectDown()
		{
			this.cursorIndex = this.textHandle.LineDownCharacterPosition(this.cursorIndex);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00014E20 File Offset: 0x00013020
		public void SelectTextEnd()
		{
			this.cursorIndex = this.characterCount;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00014E30 File Offset: 0x00013030
		public void SelectTextStart()
		{
			this.cursorIndex = 0;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00014E3B File Offset: 0x0001303B
		public void SelectToStartOfNextWord()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.FindStartOfNextWord(this.cursorIndex);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00014E58 File Offset: 0x00013058
		public void SelectToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.FindEndOfPreviousWord(this.cursorIndex);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00014E78 File Offset: 0x00013078
		public void SelectWordRight()
		{
			this.ClearCursorPos();
			int selectIndex = this.selectIndex;
			bool flag = this.cursorIndex < this.selectIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordRight();
				this.selectIndex = selectIndex;
				this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			}
			else
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordRight();
				this.selectIndex = selectIndex;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00014F04 File Offset: 0x00013104
		public void SelectWordLeft()
		{
			this.ClearCursorPos();
			int selectIndex = this.selectIndex;
			bool flag = this.cursorIndex > this.selectIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordLeft();
				this.selectIndex = selectIndex;
				this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
			}
			else
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordLeft();
				this.selectIndex = selectIndex;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00014F90 File Offset: 0x00013190
		public void SelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.GetGraphicalLineStart(this.cursorIndex);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00014FAD File Offset: 0x000131AD
		public void SelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.GetGraphicalLineEnd(this.cursorIndex);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00014FCC File Offset: 0x000131CC
		public void SelectParagraphForward()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex < this.selectIndex;
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorIndex = this.cursorIndex;
				this.textHandle.SelectToNextParagraph(ref cursorIndex);
				this.cursorIndex = cursorIndex;
			}
			else
			{
				bool flag2 = this.cursorIndex < this.characterCount;
				if (flag2)
				{
					this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex + 1);
					bool flag3 = flag && this.cursorIndex > this.selectIndex;
					if (flag3)
					{
						this.cursorIndex = this.selectIndex;
					}
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00015070 File Offset: 0x00013270
		public void SelectParagraphBackward()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex > this.selectIndex;
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorIndex = this.cursorIndex;
				this.textHandle.SelectToPreviousParagraph(ref cursorIndex);
				this.cursorIndex = cursorIndex;
			}
			else
			{
				bool flag2 = this.cursorIndex > 1;
				if (flag2)
				{
					this.cursorIndex = this.textHandle.LastIndexOf('\n', this.cursorIndex - 2) + 1;
					bool flag3 = flag && this.cursorIndex < this.selectIndex;
					if (flag3)
					{
						this.cursorIndex = this.selectIndex;
					}
				}
				else
				{
					this.selectIndex = (this.cursorIndex = 0);
				}
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00015130 File Offset: 0x00013330
		public void SelectCurrentWord()
		{
			int cursorIndex = this.cursorIndex;
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int num = 0;
				int num2 = 0;
				this.textHandle.SelectCurrentWord(cursorIndex, ref num, ref num2);
				bool flag = this.cursorIndex < this.selectIndex;
				if (flag)
				{
					this.cursorIndex = num;
					this.selectIndex = num2;
				}
				else
				{
					this.cursorIndex = num2;
					this.selectIndex = num;
				}
			}
			else
			{
				bool flag2 = this.cursorIndex < this.selectIndex;
				if (flag2)
				{
					this.cursorIndex = this.FindEndOfClassification(cursorIndex, TextSelectingUtilities.Direction.Backward);
					this.selectIndex = this.FindEndOfClassification(cursorIndex, TextSelectingUtilities.Direction.Forward);
				}
				else
				{
					this.cursorIndex = this.FindEndOfClassification(cursorIndex, TextSelectingUtilities.Direction.Forward);
					this.selectIndex = this.FindEndOfClassification(cursorIndex, TextSelectingUtilities.Direction.Backward);
				}
			}
			this.ClearCursorPos();
			this.m_bJustSelected = true;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00015210 File Offset: 0x00013410
		public void SelectCurrentParagraph()
		{
			this.ClearCursorPos();
			int characterCount = this.characterCount;
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorIndex = this.cursorIndex;
				int selectIndex = this.selectIndex;
				this.textHandle.SelectCurrentParagraph(ref cursorIndex, ref selectIndex);
				this.cursorIndex = cursorIndex;
				this.selectIndex = selectIndex;
			}
			else
			{
				bool flag = this.cursorIndex < characterCount;
				if (flag)
				{
					this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex);
				}
				bool flag2 = this.selectIndex != 0;
				if (flag2)
				{
					this.selectIndex = this.textHandle.LastIndexOf('\n', this.selectIndex - 1) + 1;
				}
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000152BC File Offset: 0x000134BC
		public void MoveRight()
		{
			this.ClearCursorPos();
			bool flag = this.selectIndex == this.cursorIndex;
			if (flag)
			{
				this.cursorIndex = this.NextCodePointIndex(this.cursorIndex);
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				bool flag2 = this.selectIndex > this.cursorIndex;
				if (flag2)
				{
					this.cursorIndex = this.selectIndex;
				}
				else
				{
					this.selectIndex = this.cursorIndex;
				}
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00015338 File Offset: 0x00013538
		public void MoveLeft()
		{
			bool flag = this.selectIndex == this.cursorIndex;
			if (flag)
			{
				this.cursorIndex = this.PreviousCodePointIndex(this.cursorIndex);
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				bool flag2 = this.selectIndex > this.cursorIndex;
				if (flag2)
				{
					this.selectIndex = this.cursorIndex;
				}
				else
				{
					this.cursorIndex = this.selectIndex;
				}
			}
			this.ClearCursorPos();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000153B4 File Offset: 0x000135B4
		public void MoveUp()
		{
			bool flag = this.selectIndex < this.cursorIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.cursorIndex = (this.selectIndex = this.textHandle.LineUpCharacterPosition(this.cursorIndex));
			bool flag2 = this.cursorIndex <= 0;
			if (flag2)
			{
				this.ClearCursorPos();
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001542C File Offset: 0x0001362C
		public void MoveDown()
		{
			bool flag = this.selectIndex > this.cursorIndex;
			if (flag)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.cursorIndex = (this.selectIndex = this.textHandle.LineDownCharacterPosition(this.cursorIndex));
			bool flag2 = this.cursorIndex == this.characterCount;
			if (flag2)
			{
				this.ClearCursorPos();
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000154A4 File Offset: 0x000136A4
		public void MoveLineStart()
		{
			int num = ((this.selectIndex < this.cursorIndex) ? this.selectIndex : this.cursorIndex);
			int num2 = num;
			while (num2-- != 0)
			{
				bool flag = this.m_TextElementInfos[num2].character == 10U;
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = num2 + 1);
					return;
				}
			}
			this.selectIndex = (this.cursorIndex = 0);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00015524 File Offset: 0x00013724
		public void MoveLineEnd()
		{
			int num = ((this.selectIndex > this.cursorIndex) ? this.selectIndex : this.cursorIndex);
			int i = num;
			int characterCount = this.characterCount;
			while (i < characterCount)
			{
				bool flag = this.m_TextElementInfos[i].character == 10U;
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = i);
					return;
				}
				i++;
			}
			this.selectIndex = (this.cursorIndex = characterCount);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000155B0 File Offset: 0x000137B0
		public void MoveGraphicalLineStart()
		{
			this.cursorIndex = (this.selectIndex = this.GetGraphicalLineStart((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex));
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000155F4 File Offset: 0x000137F4
		public void MoveGraphicalLineEnd()
		{
			this.cursorIndex = (this.selectIndex = this.GetGraphicalLineEnd((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex));
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00015638 File Offset: 0x00013838
		public void MoveTextStart()
		{
			this.selectIndex = (this.cursorIndex = 0);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00015658 File Offset: 0x00013858
		public void MoveTextEnd()
		{
			this.selectIndex = (this.cursorIndex = this.characterCount);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00015680 File Offset: 0x00013880
		public void MoveParagraphForward()
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorIndex = this.cursorIndex;
				this.textHandle.SelectToNextParagraph(ref cursorIndex);
				this.cursorIndex = (this.selectIndex = cursorIndex);
			}
			else
			{
				this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
				bool flag = this.cursorIndex < this.characterCount;
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex + 1));
				}
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00015720 File Offset: 0x00013920
		public void MoveParagraphBackward()
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				int cursorIndex = this.cursorIndex;
				this.textHandle.SelectToPreviousParagraph(ref cursorIndex);
				this.cursorIndex = (this.selectIndex = cursorIndex);
			}
			else
			{
				this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
				bool flag = this.cursorIndex > 1;
				if (flag)
				{
					this.selectIndex = (this.cursorIndex = this.textHandle.LastIndexOf('\n', this.cursorIndex - 2) + 1);
				}
				else
				{
					this.selectIndex = (this.cursorIndex = 0);
				}
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000157D8 File Offset: 0x000139D8
		public void MoveWordRight()
		{
			this.cursorIndex = ((this.cursorIndex > this.selectIndex) ? this.cursorIndex : this.selectIndex);
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				this.cursorIndex = (this.selectIndex = this.FindStartOfNextWord(this.cursorIndex));
			}
			else
			{
				this.cursorIndex = (this.selectIndex = this.FindNextSeperator(this.cursorIndex));
			}
			this.ClearCursorPos();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001585C File Offset: 0x00013A5C
		public void MoveToStartOfNextWord()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex != this.selectIndex;
			if (flag)
			{
				this.MoveRight();
			}
			else
			{
				this.cursorIndex = (this.selectIndex = this.FindStartOfNextWord(this.cursorIndex));
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000158B0 File Offset: 0x00013AB0
		public void MoveToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex != this.selectIndex;
			if (flag)
			{
				this.MoveLeft();
			}
			else
			{
				this.cursorIndex = (this.selectIndex = this.FindEndOfPreviousWord(this.cursorIndex));
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00015904 File Offset: 0x00013B04
		public void MoveWordLeft()
		{
			this.cursorIndex = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			bool useAdvancedText = this.textHandle.useAdvancedText;
			if (useAdvancedText)
			{
				this.cursorIndex = this.FindEndOfPreviousWord(this.cursorIndex);
			}
			else
			{
				this.cursorIndex = this.FindPrevSeperator(this.cursorIndex);
			}
			this.selectIndex = this.cursorIndex;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001597C File Offset: 0x00013B7C
		public void MouseDragSelectsWholeWords(bool on)
		{
			this.m_MouseDragSelectsWholeWords = on;
			this.m_DblClickInitPosStart = ((this.cursorIndex < this.selectIndex) ? this.cursorIndex : this.selectIndex);
			this.m_DblClickInitPosEnd = ((this.cursorIndex < this.selectIndex) ? this.selectIndex : this.cursorIndex);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000159D8 File Offset: 0x00013BD8
		public void ExpandSelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex < this.selectIndex;
			if (flag)
			{
				this.cursorIndex = this.GetGraphicalLineStart(this.cursorIndex);
			}
			else
			{
				int cursorIndex = this.cursorIndex;
				this.cursorIndex = this.GetGraphicalLineStart(this.selectIndex);
				this.selectIndex = cursorIndex;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00015A38 File Offset: 0x00013C38
		public void ExpandSelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex > this.selectIndex;
			if (flag)
			{
				this.cursorIndex = this.GetGraphicalLineEnd(this.cursorIndex);
			}
			else
			{
				int cursorIndex = this.cursorIndex;
				this.cursorIndex = this.GetGraphicalLineEnd(this.selectIndex);
				this.selectIndex = cursorIndex;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00015A98 File Offset: 0x00013C98
		public void DblClickSnap(TextEditor.DblClickSnapping snapping)
		{
			this.dblClickSnap = snapping;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00015AA4 File Offset: 0x00013CA4
		protected internal void MoveCursorToPosition_Internal(Vector2 cursorPosition, bool shift)
		{
			this.selectIndex = this.textHandle.GetCursorIndexFromPosition(cursorPosition, true);
			bool flag = !shift;
			if (flag)
			{
				this.cursorIndex = this.selectIndex;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00015AE0 File Offset: 0x00013CE0
		protected internal void MoveAltCursorToPosition(Vector2 cursorPosition)
		{
			bool flag = this.cursorIndex == 0 && this.selectIndex == this.characterCount;
			if (flag)
			{
				this.iAltCursorPos = -1;
			}
			else
			{
				int cursorIndexFromPosition = this.textHandle.GetCursorIndexFromPosition(cursorPosition, true);
				this.iAltCursorPos = Mathf.Min(this.characterCount, cursorIndexFromPosition);
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00015B38 File Offset: 0x00013D38
		protected internal bool IsOverSelection(Vector2 cursorPosition)
		{
			int cursorIndexFromPosition = this.textHandle.GetCursorIndexFromPosition(cursorPosition, true);
			return cursorIndexFromPosition < Mathf.Max(this.cursorIndex, this.selectIndex) && cursorIndexFromPosition > Mathf.Min(this.cursorIndex, this.selectIndex);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00015B84 File Offset: 0x00013D84
		public void SelectToPosition(Vector2 cursorPosition)
		{
			bool flag = this.characterCount == 0;
			if (!flag)
			{
				bool flag2 = !this.m_MouseDragSelectsWholeWords;
				if (flag2)
				{
					this.cursorIndex = this.textHandle.GetCursorIndexFromPosition(cursorPosition, true);
				}
				else
				{
					int cursorIndexFromPosition = this.textHandle.GetCursorIndexFromPosition(cursorPosition, true);
					bool flag3 = this.dblClickSnap == TextEditor.DblClickSnapping.WORDS;
					if (flag3)
					{
						bool flag4 = cursorIndexFromPosition <= this.m_DblClickInitPosStart;
						if (flag4)
						{
							bool useAdvancedText = this.textHandle.useAdvancedText;
							if (useAdvancedText)
							{
								this.selectIndex = Mathf.Max(this.selectIndex, this.cursorIndex);
								this.cursorIndex = this.textHandle.GetEndOfPreviousWord(cursorIndexFromPosition);
							}
							else
							{
								this.cursorIndex = this.FindEndOfClassification(cursorIndexFromPosition, TextSelectingUtilities.Direction.Backward);
								this.selectIndex = this.FindEndOfClassification(this.m_DblClickInitPosEnd - 1, TextSelectingUtilities.Direction.Forward);
							}
						}
						else
						{
							bool flag5 = cursorIndexFromPosition >= this.m_DblClickInitPosEnd;
							if (flag5)
							{
								bool useAdvancedText2 = this.textHandle.useAdvancedText;
								if (useAdvancedText2)
								{
									this.selectIndex = Mathf.Min(this.selectIndex, this.cursorIndex);
									this.cursorIndex = this.textHandle.GetStartOfNextWord(cursorIndexFromPosition - 1);
								}
								else
								{
									this.cursorIndex = this.FindEndOfClassification(cursorIndexFromPosition - 1, TextSelectingUtilities.Direction.Forward);
									this.selectIndex = this.FindEndOfClassification(this.m_DblClickInitPosStart + 1, TextSelectingUtilities.Direction.Backward);
								}
							}
							else
							{
								this.cursorIndex = this.m_DblClickInitPosStart;
								this.selectIndex = this.m_DblClickInitPosEnd;
							}
						}
					}
					else
					{
						bool flag6 = (!this.textHandle.useAdvancedText && cursorIndexFromPosition <= this.m_DblClickInitPosStart) || (this.textHandle.useAdvancedText && cursorIndexFromPosition < this.m_DblClickInitPosStart);
						if (flag6)
						{
							bool useAdvancedText3 = this.textHandle.useAdvancedText;
							if (useAdvancedText3)
							{
								int num = cursorIndexFromPosition;
								this.textHandle.SelectToStartOfParagraph(ref num);
								this.selectIndex = num;
							}
							else
							{
								bool flag7 = cursorIndexFromPosition > 0;
								if (flag7)
								{
									this.cursorIndex = this.textHandle.LastIndexOf('\n', Mathf.Max(0, cursorIndexFromPosition - 1)) + 1;
								}
								else
								{
									this.cursorIndex = 0;
								}
								this.selectIndex = this.textHandle.LastIndexOf('\n', Mathf.Min(this.characterCount - 1, this.m_DblClickInitPosEnd + 1));
							}
						}
						else
						{
							bool flag8 = cursorIndexFromPosition >= this.m_DblClickInitPosEnd;
							if (flag8)
							{
								bool useAdvancedText4 = this.textHandle.useAdvancedText;
								if (useAdvancedText4)
								{
									int num2 = cursorIndexFromPosition;
									this.textHandle.SelectToEndOfParagraph(ref num2);
									this.cursorIndex = num2;
								}
								else
								{
									bool flag9 = cursorIndexFromPosition < this.characterCount;
									if (flag9)
									{
										this.cursorIndex = this.IndexOfEndOfLine(cursorIndexFromPosition);
									}
									else
									{
										this.cursorIndex = this.characterCount;
									}
									this.selectIndex = this.textHandle.LastIndexOf('\n', Mathf.Max(0, this.m_DblClickInitPosEnd - 2)) + 1;
								}
							}
							else
							{
								bool useAdvancedText5 = this.textHandle.useAdvancedText;
								if (useAdvancedText5)
								{
									this.cursorIndex = this.m_DblClickInitPosEnd;
									this.selectIndex = this.m_DblClickInitPosStart;
								}
								else
								{
									this.cursorIndex = this.m_DblClickInitPosStart;
									this.selectIndex = this.m_DblClickInitPosEnd;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00015EC8 File Offset: 0x000140C8
		private int FindNextSeperator(int startPos)
		{
			int characterCount = this.characterCount;
			while (startPos < characterCount && this.ClassifyChar(startPos) > TextSelectingUtilities.CharacterType.LetterLike)
			{
				startPos = this.NextCodePointIndex(startPos);
			}
			while (startPos < characterCount && this.ClassifyChar(startPos) == TextSelectingUtilities.CharacterType.LetterLike)
			{
				startPos = this.NextCodePointIndex(startPos);
			}
			return startPos;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00015F24 File Offset: 0x00014124
		private int FindPrevSeperator(int startPos)
		{
			startPos = this.PreviousCodePointIndex(startPos);
			while (startPos > 0 && this.ClassifyChar(startPos) > TextSelectingUtilities.CharacterType.LetterLike)
			{
				startPos = this.PreviousCodePointIndex(startPos);
			}
			bool flag = startPos == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				while (startPos > 0 && this.ClassifyChar(startPos) == TextSelectingUtilities.CharacterType.LetterLike)
				{
					startPos = this.PreviousCodePointIndex(startPos);
				}
				bool flag2 = this.ClassifyChar(startPos) == TextSelectingUtilities.CharacterType.LetterLike;
				if (flag2)
				{
					num = startPos;
				}
				else
				{
					num = this.NextCodePointIndex(startPos);
				}
			}
			return num;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00015FA8 File Offset: 0x000141A8
		public int FindStartOfNextWord(int p)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.GetStartOfNextWord(p);
			}
			else
			{
				int characterCount = this.characterCount;
				bool flag = p == characterCount;
				if (flag)
				{
					num = p;
				}
				else
				{
					TextSelectingUtilities.CharacterType characterType = this.ClassifyChar(p);
					bool flag2 = characterType != TextSelectingUtilities.CharacterType.WhiteSpace;
					if (flag2)
					{
						p = this.NextCodePointIndex(p);
						while (p < characterCount && this.ClassifyChar(p) == characterType)
						{
							p = this.NextCodePointIndex(p);
						}
					}
					else
					{
						bool flag3 = this.m_TextElementInfos[p].character == 9U || this.m_TextElementInfos[p].character == 10U;
						if (flag3)
						{
							return this.NextCodePointIndex(p);
						}
					}
					bool flag4 = p == characterCount;
					if (flag4)
					{
						num = p;
					}
					else
					{
						bool flag5 = this.m_TextElementInfos[p].character == 32U;
						if (flag5)
						{
							while (p < characterCount && this.ClassifyChar(p) == TextSelectingUtilities.CharacterType.WhiteSpace)
							{
								p = this.NextCodePointIndex(p);
							}
						}
						else
						{
							bool flag6 = this.m_TextElementInfos[p].character == 9U || this.m_TextElementInfos[p].character == 10U;
							if (flag6)
							{
								return p;
							}
						}
						num = p;
					}
				}
			}
			return num;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00016104 File Offset: 0x00014304
		public int FindEndOfPreviousWord(int p)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.GetEndOfPreviousWord(p);
			}
			else
			{
				bool flag = p == 0;
				if (flag)
				{
					num = p;
				}
				else
				{
					p = this.PreviousCodePointIndex(p);
					while (p > 0 && this.m_TextElementInfos[p].character == 32U)
					{
						p = this.PreviousCodePointIndex(p);
					}
					TextSelectingUtilities.CharacterType characterType = this.ClassifyChar(p);
					bool flag2 = characterType != TextSelectingUtilities.CharacterType.WhiteSpace;
					if (flag2)
					{
						while (p > 0 && this.ClassifyChar(this.PreviousCodePointIndex(p)) == characterType)
						{
							p = this.PreviousCodePointIndex(p);
						}
					}
					num = p;
				}
			}
			return num;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000161BC File Offset: 0x000143BC
		private int FindEndOfClassification(int p, TextSelectingUtilities.Direction dir)
		{
			bool flag = this.characterCount == 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = p >= this.characterCount;
				if (flag2)
				{
					p = this.characterCount - 1;
				}
				TextSelectingUtilities.CharacterType characterType = this.ClassifyChar(p);
				bool flag3 = characterType == TextSelectingUtilities.CharacterType.NewLine;
				if (flag3)
				{
					num = p;
				}
				else
				{
					for (;;)
					{
						if (dir != TextSelectingUtilities.Direction.Forward)
						{
							if (dir == TextSelectingUtilities.Direction.Backward)
							{
								p = this.PreviousCodePointIndex(p);
								bool flag4 = p == 0;
								if (flag4)
								{
									break;
								}
							}
						}
						else
						{
							p = this.NextCodePointIndex(p);
							bool flag5 = p >= this.characterCount;
							if (flag5)
							{
								goto Block_8;
							}
						}
						if (this.ClassifyChar(p) != characterType)
						{
							goto Block_9;
						}
					}
					return (this.ClassifyChar(0) == characterType) ? 0 : this.NextCodePointIndex(0);
					Block_8:
					return this.characterCount;
					Block_9:
					bool flag6 = dir == TextSelectingUtilities.Direction.Forward;
					if (flag6)
					{
						num = p;
					}
					else
					{
						num = this.NextCodePointIndex(p);
					}
				}
			}
			return num;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000162A4 File Offset: 0x000144A4
		private int ClampTextIndex(int index)
		{
			return Mathf.Clamp(index, 0, this.characterCount);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000162C4 File Offset: 0x000144C4
		private int IndexOfEndOfLine(int startIndex)
		{
			int num = this.textHandle.IndexOf('\n', startIndex);
			return (num != -1) ? num : this.characterCount;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000162F4 File Offset: 0x000144F4
		public int PreviousCodePointIndex(int index)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.PreviousCodePointIndex(index);
			}
			else
			{
				bool flag = index > 0;
				if (flag)
				{
					index--;
				}
				num = index;
			}
			return num;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00016334 File Offset: 0x00014534
		public int NextCodePointIndex(int index)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.NextCodePointIndex(index);
			}
			else
			{
				bool flag = index < this.characterCount;
				if (flag)
				{
					index++;
				}
				num = index;
			}
			return num;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00016378 File Offset: 0x00014578
		private int GetGraphicalLineStart(int p)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.GetFirstCharacterIndexOnLine(p);
			}
			else
			{
				Vector2 cursorPositionFromStringIndexUsingLineHeight = this.textHandle.GetCursorPositionFromStringIndexUsingLineHeight(p, false, true);
				cursorPositionFromStringIndexUsingLineHeight.y -= 1f / GUIUtility.pixelsPerPoint;
				cursorPositionFromStringIndexUsingLineHeight.x = 0f;
				num = this.textHandle.GetCursorIndexFromPosition(cursorPositionFromStringIndexUsingLineHeight, true);
			}
			return num;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000163E8 File Offset: 0x000145E8
		private int GetGraphicalLineEnd(int p)
		{
			bool useAdvancedText = this.textHandle.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				num = this.textHandle.GetLastCharacterIndexOnLine(p);
			}
			else
			{
				Vector2 cursorPositionFromStringIndexUsingLineHeight = this.textHandle.GetCursorPositionFromStringIndexUsingLineHeight(p, false, true);
				cursorPositionFromStringIndexUsingLineHeight.y -= 1f / GUIUtility.pixelsPerPoint;
				cursorPositionFromStringIndexUsingLineHeight.x += 5000f;
				num = this.textHandle.GetCursorIndexFromPosition(cursorPositionFromStringIndexUsingLineHeight, true);
			}
			return num;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001645C File Offset: 0x0001465C
		public void Copy()
		{
			bool flag = this.selectIndex == this.cursorIndex;
			if (!flag)
			{
				GUIUtility.systemCopyBuffer = this.selectedText;
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001648C File Offset: 0x0001468C
		private TextSelectingUtilities.CharacterType ClassifyChar(int index)
		{
			char c = (char)this.m_TextElementInfos[index].character;
			bool flag = c == '\n';
			TextSelectingUtilities.CharacterType characterType;
			if (flag)
			{
				characterType = TextSelectingUtilities.CharacterType.NewLine;
			}
			else
			{
				bool flag2 = char.IsWhiteSpace(c);
				if (flag2)
				{
					characterType = TextSelectingUtilities.CharacterType.WhiteSpace;
				}
				else
				{
					bool flag3 = char.IsLetterOrDigit(c) || this.m_TextElementInfos[index].character == 39U;
					if (flag3)
					{
						characterType = TextSelectingUtilities.CharacterType.LetterLike;
					}
					else
					{
						characterType = TextSelectingUtilities.CharacterType.Symbol;
					}
				}
			}
			return characterType;
		}

		// Token: 0x040001AC RID: 428
		public TextEditor.DblClickSnapping dblClickSnap = TextEditor.DblClickSnapping.WORDS;

		// Token: 0x040001AD RID: 429
		public int iAltCursorPos = -1;

		// Token: 0x040001AE RID: 430
		public bool hasHorizontalCursorPos = false;

		// Token: 0x040001AF RID: 431
		private bool m_bJustSelected = false;

		// Token: 0x040001B0 RID: 432
		private bool m_MouseDragSelectsWholeWords = false;

		// Token: 0x040001B1 RID: 433
		private int m_DblClickInitPosStart = 0;

		// Token: 0x040001B2 RID: 434
		private int m_DblClickInitPosEnd = 0;

		// Token: 0x040001B3 RID: 435
		public TextHandle textHandle;

		// Token: 0x040001B4 RID: 436
		private const int kMoveDownHeight = 5;

		// Token: 0x040001B5 RID: 437
		private const char kNewLineChar = '\n';

		// Token: 0x040001B6 RID: 438
		private bool m_RevealCursor;

		// Token: 0x040001B7 RID: 439
		private int m_CursorIndex = 0;

		// Token: 0x040001B8 RID: 440
		internal int m_SelectIndex = 0;

		// Token: 0x040001B9 RID: 441
		private static Dictionary<Event, TextSelectOp> s_KeySelectOps;

		// Token: 0x040001BA RID: 442
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal Action OnCursorIndexChange;

		// Token: 0x040001BB RID: 443
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal Action OnSelectIndexChange;

		// Token: 0x040001BC RID: 444
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal Action OnRevealCursorChange;

		// Token: 0x0200004A RID: 74
		private enum CharacterType
		{
			// Token: 0x040001BE RID: 446
			LetterLike,
			// Token: 0x040001BF RID: 447
			Symbol,
			// Token: 0x040001C0 RID: 448
			Symbol2,
			// Token: 0x040001C1 RID: 449
			WhiteSpace,
			// Token: 0x040001C2 RID: 450
			NewLine
		}

		// Token: 0x0200004B RID: 75
		private enum Direction
		{
			// Token: 0x040001C4 RID: 452
			Forward,
			// Token: 0x040001C5 RID: 453
			Backward
		}
	}
}
