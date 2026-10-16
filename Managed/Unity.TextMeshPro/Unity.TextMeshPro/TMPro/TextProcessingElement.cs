using System;
using System.Diagnostics;

namespace TMPro
{
	// Token: 0x02000068 RID: 104
	[DebuggerDisplay("{DebuggerDisplay()}")]
	internal struct TextProcessingElement
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0002B337 File Offset: 0x00029537
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0002B33F File Offset: 0x0002953F
		public TextProcessingElementType ElementType
		{
			get
			{
				return this.m_ElementType;
			}
			set
			{
				this.m_ElementType = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0002B348 File Offset: 0x00029548
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0002B350 File Offset: 0x00029550
		public int StartIndex
		{
			get
			{
				return this.m_StartIndex;
			}
			set
			{
				this.m_StartIndex = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0002B359 File Offset: 0x00029559
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0002B361 File Offset: 0x00029561
		public int Length
		{
			get
			{
				return this.m_Length;
			}
			set
			{
				this.m_Length = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0002B36A File Offset: 0x0002956A
		public CharacterElement CharacterElement
		{
			get
			{
				return this.m_CharacterElement;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0002B372 File Offset: 0x00029572
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0002B37A File Offset: 0x0002957A
		public MarkupElement MarkupElement
		{
			get
			{
				return this.m_MarkupElement;
			}
			set
			{
				this.m_MarkupElement = value;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0002B383 File Offset: 0x00029583
		public TextProcessingElement(TextProcessingElementType elementType, int startIndex, int length)
		{
			this.m_ElementType = elementType;
			this.m_StartIndex = startIndex;
			this.m_Length = length;
			this.m_CharacterElement = default(CharacterElement);
			this.m_MarkupElement = default(MarkupElement);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0002B3B2 File Offset: 0x000295B2
		public TextProcessingElement(TMP_TextElement textElement, int startIndex, int length)
		{
			this.m_ElementType = TextProcessingElementType.TextCharacterElement;
			this.m_StartIndex = startIndex;
			this.m_Length = length;
			this.m_CharacterElement = new CharacterElement(textElement);
			this.m_MarkupElement = default(MarkupElement);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0002B3E1 File Offset: 0x000295E1
		public TextProcessingElement(CharacterElement characterElement, int startIndex, int length)
		{
			this.m_ElementType = TextProcessingElementType.TextCharacterElement;
			this.m_StartIndex = startIndex;
			this.m_Length = length;
			this.m_CharacterElement = characterElement;
			this.m_MarkupElement = default(MarkupElement);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0002B40B File Offset: 0x0002960B
		public TextProcessingElement(MarkupElement markupElement)
		{
			this.m_ElementType = TextProcessingElementType.TextMarkupElement;
			this.m_StartIndex = markupElement.ValueStartIndex;
			this.m_Length = markupElement.ValueLength;
			this.m_CharacterElement = default(CharacterElement);
			this.m_MarkupElement = markupElement;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0002B444 File Offset: 0x00029644
		public static TextProcessingElement Undefined
		{
			get
			{
				return new TextProcessingElement
				{
					ElementType = TextProcessingElementType.Undefined
				};
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0002B464 File Offset: 0x00029664
		private string DebuggerDisplay()
		{
			if (this.m_ElementType != TextProcessingElementType.TextCharacterElement)
			{
				return string.Format("Markup = {0}", (MarkupTag)this.m_MarkupElement.NameHashCode);
			}
			return string.Format("Unicode ({0})   '{1}' ", this.m_CharacterElement.Unicode, (char)this.m_CharacterElement.Unicode);
		}

		// Token: 0x040004C0 RID: 1216
		private TextProcessingElementType m_ElementType;

		// Token: 0x040004C1 RID: 1217
		private int m_StartIndex;

		// Token: 0x040004C2 RID: 1218
		private int m_Length;

		// Token: 0x040004C3 RID: 1219
		private CharacterElement m_CharacterElement;

		// Token: 0x040004C4 RID: 1220
		private MarkupElement m_MarkupElement;
	}
}
