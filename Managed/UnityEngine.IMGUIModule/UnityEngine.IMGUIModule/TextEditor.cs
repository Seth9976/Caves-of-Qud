using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x02000047 RID: 71
	public class TextEditor
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00013549 File Offset: 0x00011749
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00013556 File Offset: 0x00011756
		public bool isMultiline
		{
			get
			{
				return this.m_TextEditing.multiline;
			}
			set
			{
				this.m_TextEditing.multiline = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00013564 File Offset: 0x00011764
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00013571 File Offset: 0x00011771
		public bool hasHorizontalCursor
		{
			get
			{
				return this.m_TextSelecting.hasHorizontalCursorPos;
			}
			set
			{
				this.m_TextSelecting.hasHorizontalCursorPos = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001357F File Offset: 0x0001177F
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0001358C File Offset: 0x0001178C
		public bool showCursor
		{
			get
			{
				return this.m_TextSelecting.revealCursor;
			}
			set
			{
				this.m_TextSelecting.revealCursor = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001359C File Offset: 0x0001179C
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000135B4 File Offset: 0x000117B4
		internal bool m_HasFocus
		{
			get
			{
				return this.focus;
			}
			set
			{
				this.focus = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000135BE File Offset: 0x000117BE
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x000135BE File Offset: 0x000117BE
		[Obsolete("Please use 'text' instead of 'content'", true)]
		public GUIContent content
		{
			get
			{
				throw new NotImplementedException("Please use 'text' instead of 'content'");
			}
			set
			{
				throw new NotImplementedException("Please use 'text' instead of 'content'");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000135CA File Offset: 0x000117CA
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x000135D8 File Offset: 0x000117D8
		public string text
		{
			get
			{
				return this.m_TextEditing.text;
			}
			set
			{
				string text = value ?? "";
				bool flag = this.m_TextEditing.text == text;
				if (!flag)
				{
					this.m_TextEditing.SetTextWithoutNotify(text);
					this.m_Content.SetTextWithoutNotify(text);
					this.textWithWhitespace = text;
					this.UpdateTextHandle();
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00013631 File Offset: 0x00011831
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0001364D File Offset: 0x0001184D
		internal string textWithWhitespace
		{
			get
			{
				return string.IsNullOrEmpty(this.m_TextWithWhitespace) ? GUIContent.k_ZeroWidthSpace : this.m_TextWithWhitespace;
			}
			set
			{
				this.m_TextWithWhitespace = value + GUIContent.k_ZeroWidthSpace;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00013660 File Offset: 0x00011860
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00013668 File Offset: 0x00011868
		public Rect position { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00013671 File Offset: 0x00011871
		internal virtual Rect localPosition
		{
			get
			{
				return this.style.padding.Remove(this.position);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00013689 File Offset: 0x00011889
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00013696 File Offset: 0x00011896
		public int cursorIndex
		{
			get
			{
				return this.m_TextSelecting.cursorIndex;
			}
			set
			{
				this.m_TextSelecting.cursorIndex = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000136A5 File Offset: 0x000118A5
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x000136B2 File Offset: 0x000118B2
		internal int stringCursorIndex
		{
			get
			{
				return this.m_TextEditing.stringCursorIndex;
			}
			set
			{
				this.m_TextEditing.stringCursorIndex = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000136C1 File Offset: 0x000118C1
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x000136CE File Offset: 0x000118CE
		public int selectIndex
		{
			get
			{
				return this.m_TextSelecting.selectIndex;
			}
			set
			{
				this.m_TextSelecting.selectIndex = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000136DD File Offset: 0x000118DD
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x000136EA File Offset: 0x000118EA
		internal int stringSelectIndex
		{
			get
			{
				return this.m_TextEditing.stringSelectIndex;
			}
			set
			{
				this.m_TextEditing.stringSelectIndex = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000136F9 File Offset: 0x000118F9
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x00013706 File Offset: 0x00011906
		public TextEditor.DblClickSnapping doubleClickSnapping
		{
			get
			{
				return this.m_TextSelecting.dblClickSnap;
			}
			set
			{
				this.m_TextSelecting.dblClickSnap = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00013714 File Offset: 0x00011914
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00013721 File Offset: 0x00011921
		public int altCursorPosition
		{
			get
			{
				return this.m_TextSelecting.iAltCursorPos;
			}
			set
			{
				this.m_TextSelecting.iAltCursorPos = value;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013730 File Offset: 0x00011930
		[RequiredByNativeCode]
		public TextEditor()
		{
			GUIStyle none = GUIStyle.none;
			this.m_TextHandle = IMGUITextHandle.GetTextHandle(none, this.position, this.textWithWhitespace, Color.white);
			this.m_TextHandle.AddTextInfoToPermanentCache();
			this.m_TextSelecting = new TextSelectingUtilities(this.m_TextHandle);
			this.m_TextEditing = new TextEditingUtilities(this.m_TextSelecting, this.m_TextHandle, this.m_Content.text);
			this.m_Content.OnTextChanged += this.OnContentTextChangedHandle;
			TextEditingUtilities textEditing = this.m_TextEditing;
			textEditing.OnTextChanged = (Action)Delegate.Combine(textEditing.OnTextChanged, new Action(this.OnTextChangedHandle));
			this.style = none;
			TextSelectingUtilities textSelecting = this.m_TextSelecting;
			textSelecting.OnCursorIndexChange = (Action)Delegate.Combine(textSelecting.OnCursorIndexChange, new Action(this.OnCursorIndexChange));
			TextSelectingUtilities textSelecting2 = this.m_TextSelecting;
			textSelecting2.OnSelectIndexChange = (Action)Delegate.Combine(textSelecting2.OnSelectIndexChange, new Action(this.OnSelectIndexChange));
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001387F File Offset: 0x00011A7F
		private void OnTextChangedHandle()
		{
			this.m_Content.SetTextWithoutNotify(this.text);
			this.textWithWhitespace = this.text;
			this.UpdateTextHandle();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000138A8 File Offset: 0x00011AA8
		private void OnContentTextChangedHandle()
		{
			this.text = this.m_Content.text;
			this.textWithWhitespace = this.text;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000138CA File Offset: 0x00011ACA
		public void OnFocus()
		{
			this.m_HasFocus = true;
			this.m_TextSelecting.OnFocus(true);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000138E2 File Offset: 0x00011AE2
		public void OnLostFocus()
		{
			this.m_HasFocus = false;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000138F0 File Offset: 0x00011AF0
		public bool HasClickedOnLink(Vector2 mousePosition, out string linkData)
		{
			Vector2 vector = mousePosition + this.scrollOffset;
			linkData = "";
			int num = this.m_TextHandle.FindIntersectingLink(vector - new Vector2(this.position.x, this.position.y), true);
			bool flag = num < 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				LinkInfo linkInfo = this.m_TextHandle.textInfo.linkInfo[num];
				bool flag3 = linkInfo.linkId != null && linkInfo.linkIdLength > 0;
				if (flag3)
				{
					linkData = new string(linkInfo.linkId);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000139A8 File Offset: 0x00011BA8
		public bool HasClickedOnHREF(Vector2 mousePosition, out string href)
		{
			Vector2 vector = mousePosition + this.scrollOffset;
			href = "";
			int num = this.m_TextHandle.FindIntersectingLink(vector - new Vector2(this.position.x, this.position.y), true);
			bool flag = num < 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				LinkInfo linkInfo = this.m_TextHandle.textInfo.linkInfo[num];
				bool flag3 = linkInfo.hashCode == 2535353;
				if (flag3)
				{
					bool flag4 = linkInfo.linkId != null && linkInfo.linkIdLength > 0;
					if (flag4)
					{
						href = new string(linkInfo.linkId);
						bool flag5 = !href.StartsWith("href");
						if (flag5)
						{
							return false;
						}
						bool flag6 = href.StartsWith("href=\"") || href.StartsWith("href='");
						if (flag6)
						{
							href = href.Substring(6, href.Length - 7);
						}
						else
						{
							href = href.Substring(5, href.Length - 6);
						}
						bool flag7 = Uri.IsWellFormedUriString(href, UriKind.Absolute);
						if (flag7)
						{
							return true;
						}
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013AF0 File Offset: 0x00011CF0
		public bool HandleKeyEvent(Event e)
		{
			return this.m_TextEditing.HandleKeyEvent(e) || this.m_TextSelecting.HandleKeyEvent(e);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013B1F File Offset: 0x00011D1F
		public bool DeleteLineBack()
		{
			return this.m_TextEditing.DeleteLineBack();
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00013B2C File Offset: 0x00011D2C
		public bool DeleteWordBack()
		{
			return this.m_TextEditing.DeleteWordBack();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00013B39 File Offset: 0x00011D39
		public bool DeleteWordForward()
		{
			return this.m_TextEditing.DeleteWordForward();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00013B46 File Offset: 0x00011D46
		public bool Delete()
		{
			return this.m_TextEditing.Delete();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00013B53 File Offset: 0x00011D53
		public bool CanPaste()
		{
			return this.m_TextEditing.CanPaste();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00013B60 File Offset: 0x00011D60
		public bool Backspace()
		{
			return this.m_TextEditing.Backspace();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00013B6D File Offset: 0x00011D6D
		public void SelectAll()
		{
			this.m_TextSelecting.SelectAll();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00013B7B File Offset: 0x00011D7B
		public void SelectNone()
		{
			this.m_TextSelecting.SelectNone();
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00013B89 File Offset: 0x00011D89
		public bool hasSelection
		{
			get
			{
				return this.m_TextSelecting.hasSelection;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00013B96 File Offset: 0x00011D96
		public string SelectedText
		{
			get
			{
				return this.m_TextSelecting.selectedText;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00013BA3 File Offset: 0x00011DA3
		public bool DeleteSelection()
		{
			return this.m_TextEditing.DeleteSelection();
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00013BB0 File Offset: 0x00011DB0
		public void ReplaceSelection(string replace)
		{
			this.m_TextEditing.ReplaceSelection(replace);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00013BBF File Offset: 0x00011DBF
		public void Insert(char c)
		{
			this.m_TextEditing.Insert(c);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00013BCE File Offset: 0x00011DCE
		public void MoveSelectionToAltCursor()
		{
			this.m_TextEditing.MoveSelectionToAltCursor();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00013BDC File Offset: 0x00011DDC
		public void MoveRight()
		{
			this.m_TextSelecting.MoveRight();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00013BEA File Offset: 0x00011DEA
		public void MoveLeft()
		{
			this.m_TextSelecting.MoveLeft();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public void MoveUp()
		{
			this.m_TextSelecting.MoveUp();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00013C06 File Offset: 0x00011E06
		public void MoveDown()
		{
			this.m_TextSelecting.MoveDown();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00013C14 File Offset: 0x00011E14
		public void MoveLineStart()
		{
			this.m_TextSelecting.MoveLineStart();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00013C22 File Offset: 0x00011E22
		public void MoveLineEnd()
		{
			this.m_TextSelecting.MoveLineEnd();
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00013C30 File Offset: 0x00011E30
		public void MoveGraphicalLineStart()
		{
			this.m_TextSelecting.MoveGraphicalLineStart();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00013C3E File Offset: 0x00011E3E
		public void MoveGraphicalLineEnd()
		{
			this.m_TextSelecting.MoveGraphicalLineEnd();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00013C4C File Offset: 0x00011E4C
		public void MoveTextStart()
		{
			this.m_TextSelecting.MoveTextStart();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00013C5A File Offset: 0x00011E5A
		public void MoveTextEnd()
		{
			this.m_TextSelecting.MoveTextEnd();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00013C68 File Offset: 0x00011E68
		public void MoveParagraphForward()
		{
			this.m_TextSelecting.MoveParagraphForward();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00013C76 File Offset: 0x00011E76
		public void MoveParagraphBackward()
		{
			this.m_TextSelecting.MoveParagraphBackward();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00013C84 File Offset: 0x00011E84
		public void MoveCursorToPosition(Vector2 cursorPosition)
		{
			this.MoveCursorToPosition_Internal(cursorPosition, Event.current.shift);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00013C99 File Offset: 0x00011E99
		protected internal void MoveCursorToPosition_Internal(Vector2 cursorPosition, bool shift)
		{
			this.m_TextSelecting.MoveCursorToPosition_Internal(this.GetLocalCursorPosition(cursorPosition), shift);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00013CAF File Offset: 0x00011EAF
		public void MoveAltCursorToPosition(Vector2 cursorPosition)
		{
			this.m_TextSelecting.MoveAltCursorToPosition(this.GetLocalCursorPosition(cursorPosition));
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00013CC4 File Offset: 0x00011EC4
		public bool IsOverSelection(Vector2 cursorPosition)
		{
			return this.m_TextSelecting.IsOverSelection(this.GetLocalCursorPosition(cursorPosition));
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public void SelectToPosition(Vector2 cursorPosition)
		{
			this.m_TextSelecting.SelectToPosition(this.GetLocalCursorPosition(cursorPosition));
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00013CF0 File Offset: 0x00011EF0
		private Vector2 GetLocalCursorPosition(Vector2 cursorPosition)
		{
			return cursorPosition - this.style.Internal_GetTextRectOffset(this.position, this.m_Content, new Vector2(this.m_TextHandle.preferredSize.x, (this.m_TextHandle.preferredSize.y > 0f) ? this.m_TextHandle.preferredSize.y : this.style.lineHeight)) + this.scrollOffset;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00013D73 File Offset: 0x00011F73
		public void SelectLeft()
		{
			this.m_TextSelecting.SelectLeft();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00013D81 File Offset: 0x00011F81
		public void SelectRight()
		{
			this.m_TextSelecting.SelectRight();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00013D8F File Offset: 0x00011F8F
		public void SelectUp()
		{
			this.m_TextSelecting.SelectUp();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00013D9D File Offset: 0x00011F9D
		public void SelectDown()
		{
			this.m_TextSelecting.SelectDown();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00013DAB File Offset: 0x00011FAB
		public void SelectTextEnd()
		{
			this.m_TextSelecting.SelectTextEnd();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00013DB9 File Offset: 0x00011FB9
		public void SelectTextStart()
		{
			this.m_TextSelecting.SelectTextStart();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013DC7 File Offset: 0x00011FC7
		public void MouseDragSelectsWholeWords(bool on)
		{
			this.m_TextSelecting.MouseDragSelectsWholeWords(on);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013DD6 File Offset: 0x00011FD6
		public void DblClickSnap(TextEditor.DblClickSnapping snapping)
		{
			this.m_TextSelecting.DblClickSnap(snapping);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013DE5 File Offset: 0x00011FE5
		public void MoveWordRight()
		{
			this.m_TextSelecting.MoveWordRight();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00013DF3 File Offset: 0x00011FF3
		public void MoveToStartOfNextWord()
		{
			this.m_TextSelecting.MoveToStartOfNextWord();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013E01 File Offset: 0x00012001
		public void MoveToEndOfPreviousWord()
		{
			this.m_TextSelecting.MoveToEndOfPreviousWord();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00013E0F File Offset: 0x0001200F
		public void SelectToStartOfNextWord()
		{
			this.m_TextSelecting.SelectToStartOfNextWord();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00013E1D File Offset: 0x0001201D
		public void SelectToEndOfPreviousWord()
		{
			this.m_TextSelecting.SelectToEndOfPreviousWord();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00013E2B File Offset: 0x0001202B
		public int FindStartOfNextWord(int p)
		{
			return this.m_TextSelecting.FindStartOfNextWord(p);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00013E39 File Offset: 0x00012039
		public void MoveWordLeft()
		{
			this.m_TextSelecting.MoveWordLeft();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00013E47 File Offset: 0x00012047
		public void SelectWordRight()
		{
			this.m_TextSelecting.SelectWordRight();
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00013E55 File Offset: 0x00012055
		public void SelectWordLeft()
		{
			this.m_TextSelecting.SelectWordLeft();
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013E63 File Offset: 0x00012063
		public void ExpandSelectGraphicalLineStart()
		{
			this.m_TextSelecting.ExpandSelectGraphicalLineStart();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00013E71 File Offset: 0x00012071
		public void ExpandSelectGraphicalLineEnd()
		{
			this.m_TextSelecting.ExpandSelectGraphicalLineEnd();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00013E7F File Offset: 0x0001207F
		public void SelectGraphicalLineStart()
		{
			this.m_TextSelecting.SelectGraphicalLineStart();
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00013E8D File Offset: 0x0001208D
		public void SelectGraphicalLineEnd()
		{
			this.m_TextSelecting.SelectGraphicalLineEnd();
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00013E9B File Offset: 0x0001209B
		public void SelectParagraphForward()
		{
			this.m_TextSelecting.SelectParagraphForward();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00013EA9 File Offset: 0x000120A9
		public void SelectParagraphBackward()
		{
			this.m_TextSelecting.SelectParagraphBackward();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00013EB7 File Offset: 0x000120B7
		public void SelectCurrentWord()
		{
			this.m_TextSelecting.SelectCurrentWord();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013EC5 File Offset: 0x000120C5
		public void SelectCurrentParagraph()
		{
			this.m_TextSelecting.SelectCurrentParagraph();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00013ED4 File Offset: 0x000120D4
		public void UpdateScrollOffsetIfNeeded(Event evt)
		{
			bool flag = evt.type != EventType.Repaint && evt.type != EventType.Layout;
			if (flag)
			{
				this.UpdateScrollOffset();
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00013F08 File Offset: 0x00012108
		internal void UpdateTextHandle()
		{
			this.m_TextHandle = IMGUITextHandle.GetTextHandle(this.style, this.style.padding.Remove(this.position), this.textWithWhitespace, Color.white);
			this.m_TextHandle.AddTextInfoToPermanentCache();
			this.m_TextEditing.textHandle = this.m_TextHandle;
			this.m_TextSelecting.textHandle = this.m_TextHandle;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00013F7C File Offset: 0x0001217C
		[VisibleToOtherModules]
		internal void UpdateScrollOffset()
		{
			float num = this.scrollOffset.x;
			float num2 = this.scrollOffset.y;
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.m_Content, this.m_TextSelecting.cursorIndexNoValidation);
			Rect rect = this.style.padding.Remove(this.position);
			Vector2 vector = this.graphicalCursorPos;
			vector.x -= (float)this.style.padding.left;
			vector.y -= (float)this.style.padding.top;
			Vector2 vector2 = (this.previousContentSize = this.style.GetPreferredSize(this.m_Content.textWithWhitespace, this.position));
			bool flag = vector2.x < rect.width;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool showCursor = this.showCursor;
				if (showCursor)
				{
					bool flag2 = vector.x > this.scrollOffset.x + rect.width - 1f;
					if (flag2)
					{
						num = vector.x - rect.width + 1f;
					}
					else
					{
						bool flag3 = vector.x < this.scrollOffset.x;
						if (flag3)
						{
							num = Mathf.Max(vector.x, 0f);
						}
						else
						{
							bool flag4 = this.previousContentSize.x != vector2.x && vector.x < rect.x + Math.Abs(vector2.x + 1f - rect.width);
							if (flag4)
							{
								num = Mathf.Max(rect.width - vector.x, 0f);
							}
						}
					}
				}
			}
			bool flag5 = Mathf.Round(vector2.y) <= Mathf.Round(rect.height) || rect.height == 0f;
			if (flag5)
			{
				num2 = 0f;
			}
			else
			{
				bool flag6 = this.showCursor && Math.Abs(this.lastCursorPos.y - vector.y) > 0.05f;
				if (flag6)
				{
					bool flag7 = vector.y + this.style.lineHeight > this.scrollOffset.y + rect.height;
					if (flag7)
					{
						num2 = vector.y - rect.height + this.style.lineHeight;
					}
					else
					{
						bool flag8 = vector.y < this.style.lineHeight + this.scrollOffset.y;
						if (flag8)
						{
							num2 = vector.y - this.style.lineHeight;
						}
					}
				}
			}
			bool flag9 = this.scrollOffset.x != num || this.scrollOffset.y != num2;
			if (flag9)
			{
				this.scrollOffset = new Vector2(num, (num2 < 0f) ? 0f : num2);
			}
			this.lastCursorPos = vector;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000142B0 File Offset: 0x000124B0
		public void DrawCursor(string newText)
		{
			string text = this.text;
			int cursorIndex = this.cursorIndex;
			bool flag = GUIUtility.compositionString.Length > 0;
			if (flag)
			{
				this.m_Content.text = newText.Substring(0, this.cursorIndex) + GUIUtility.compositionString + newText.Substring(this.selectIndex);
			}
			else
			{
				this.m_Content.text = newText;
			}
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(this.position, this.m_Content, cursorIndex) + new Vector2(0f, this.style.lineHeight);
			Vector2 contentOffset = this.style.contentOffset;
			this.style.contentOffset -= this.scrollOffset;
			this.style.Internal_clipOffset = this.scrollOffset;
			GUIUtility.compositionCursorPos = GUIClip.UnclipToWindow(this.graphicalCursorPos - this.scrollOffset);
			bool flag2 = GUIUtility.compositionString.Length > 0;
			if (flag2)
			{
				this.style.DrawWithTextSelection(this.position, this.m_Content, this.controlID, this.cursorIndex, this.cursorIndex + GUIUtility.compositionString.Length, true);
			}
			else
			{
				this.style.DrawWithTextSelection(this.position, this.m_Content, this.controlID, this.cursorIndex, this.selectIndex);
			}
			bool flag3 = this.m_TextSelecting.iAltCursorPos != -1;
			if (flag3)
			{
				this.style.DrawCursor(this.position, this.m_Content, this.controlID, this.m_TextSelecting.iAltCursorPos);
			}
			this.style.contentOffset = contentOffset;
			this.style.Internal_clipOffset = Vector2.zero;
			this.m_Content.text = text;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001448C File Offset: 0x0001268C
		public void SaveBackup()
		{
			this.oldText = this.text;
			this.oldPos = this.cursorIndex;
			this.oldSelectPos = this.selectIndex;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000144B3 File Offset: 0x000126B3
		public void Undo()
		{
			this.m_Content.text = this.oldText;
			this.cursorIndex = this.oldPos;
			this.selectIndex = this.oldSelectPos;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000144E4 File Offset: 0x000126E4
		public bool Cut()
		{
			bool flag = this.isPasswordField;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.m_TextEditing.Cut();
				this.UpdateTextHandle();
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00014518 File Offset: 0x00012718
		public void Copy()
		{
			bool flag = this.isPasswordField;
			if (!flag)
			{
				this.m_TextSelecting.Copy();
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00014540 File Offset: 0x00012740
		internal Rect[] GetHyperlinksRect()
		{
			Rect[] hyperlinkRects = this.style.GetHyperlinkRects(this.m_TextHandle, this.localPosition);
			for (int i = 0; i < hyperlinkRects.Length; i++)
			{
				Rect[] array = hyperlinkRects;
				int num = i;
				array[num].position = array[num].position - this.scrollOffset;
			}
			return hyperlinkRects;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001459B File Offset: 0x0001279B
		public bool Paste()
		{
			return this.m_TextEditing.Paste();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000145A8 File Offset: 0x000127A8
		public void DetectFocusChange()
		{
			this.OnDetectFocusChange();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000145B4 File Offset: 0x000127B4
		internal virtual void OnDetectFocusChange()
		{
			bool flag = this.m_HasFocus && this.controlID != GUIUtility.keyboardControl;
			if (flag)
			{
				this.OnLostFocus();
			}
			bool flag2 = !this.m_HasFocus && this.controlID == GUIUtility.keyboardControl;
			if (flag2)
			{
				this.OnFocus();
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001460B File Offset: 0x0001280B
		internal virtual void OnCursorIndexChange()
		{
			this.UpdateScrollOffset();
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001460B File Offset: 0x0001280B
		internal virtual void OnSelectIndexChange()
		{
			this.UpdateScrollOffset();
		}

		// Token: 0x04000193 RID: 403
		private readonly GUIContent m_Content = new GUIContent();

		// Token: 0x04000194 RID: 404
		private TextSelectingUtilities m_TextSelecting;

		// Token: 0x04000195 RID: 405
		internal TextEditingUtilities m_TextEditing;

		// Token: 0x04000196 RID: 406
		internal IMGUITextHandle m_TextHandle;

		// Token: 0x04000197 RID: 407
		public TouchScreenKeyboard keyboardOnScreen = null;

		// Token: 0x04000198 RID: 408
		public int controlID = 0;

		// Token: 0x04000199 RID: 409
		public GUIStyle style;

		// Token: 0x0400019A RID: 410
		[Obsolete("'multiline' has been deprecated. Changes to this member will not be observed. Use 'isMultiline' instead.", true)]
		public bool multiline;

		// Token: 0x0400019B RID: 411
		[Obsolete("'hasHorizontalCursorPos' has been deprecated. Changes to this member will not be observed. Use 'hasHorizontalCursor' instead.", true)]
		public bool hasHorizontalCursorPos = false;

		// Token: 0x0400019C RID: 412
		public bool isPasswordField = false;

		// Token: 0x0400019D RID: 413
		public Vector2 scrollOffset;

		// Token: 0x0400019E RID: 414
		[Obsolete("'revealCursor' has been deprecated. Changes to this member will not be observed. Use 'showCursor' instead.", true)]
		public bool revealCursor;

		// Token: 0x0400019F RID: 415
		private bool focus;

		// Token: 0x040001A0 RID: 416
		private string m_TextWithWhitespace;

		// Token: 0x040001A2 RID: 418
		public Vector2 graphicalCursorPos;

		// Token: 0x040001A3 RID: 419
		public Vector2 graphicalSelectCursorPos;

		// Token: 0x040001A4 RID: 420
		private Vector2 lastCursorPos = Vector2.zero;

		// Token: 0x040001A5 RID: 421
		private Vector2 previousContentSize = Vector2.zero;

		// Token: 0x040001A6 RID: 422
		private string oldText;

		// Token: 0x040001A7 RID: 423
		private int oldPos;

		// Token: 0x040001A8 RID: 424
		private int oldSelectPos;

		// Token: 0x02000048 RID: 72
		public enum DblClickSnapping : byte
		{
			// Token: 0x040001AA RID: 426
			WORDS,
			// Token: 0x040001AB RID: 427
			PARAGRAPHS
		}
	}
}
