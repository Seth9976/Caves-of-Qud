using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200005F RID: 95
	[DebuggerDisplay("{settings.text}")]
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal class TextHandle
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x00022C50 File Offset: 0x00020E50
		~TextHandle()
		{
			this.RemoveTextInfoFromTemporaryCache();
			this.RemoveTextInfoFromPermanentCache();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00022C88 File Offset: 0x00020E88
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal static void InitThreadArrays()
		{
			bool flag = TextHandle.s_Settings != null && TextHandle.s_Generators != null && TextHandle.s_TextInfosCommon != null;
			if (!flag)
			{
				TextHandle.InitArray<TextGenerationSettings>(ref TextHandle.s_Settings, () => new TextGenerationSettings());
				TextHandle.InitArray<TextGenerator>(ref TextHandle.s_Generators, () => new TextGenerator());
				TextHandle.InitArray<TextInfo>(ref TextHandle.s_TextInfosCommon, () => new TextInfo());
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00022D34 File Offset: 0x00020F34
		internal static TextGenerationSettings[] settingsArray
		{
			get
			{
				bool flag = TextHandle.s_Settings == null;
				if (flag)
				{
					TextHandle.InitArray<TextGenerationSettings>(ref TextHandle.s_Settings, () => new TextGenerationSettings());
				}
				return TextHandle.s_Settings;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00022D84 File Offset: 0x00020F84
		internal static TextGenerator[] generators
		{
			get
			{
				bool flag = TextHandle.s_Generators == null;
				if (flag)
				{
					TextHandle.InitArray<TextGenerator>(ref TextHandle.s_Generators, () => new TextGenerator());
				}
				return TextHandle.s_Generators;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00022DD4 File Offset: 0x00020FD4
		internal static TextInfo[] textInfosCommon
		{
			get
			{
				bool flag = TextHandle.s_TextInfosCommon == null;
				if (flag)
				{
					TextHandle.InitArray<TextInfo>(ref TextHandle.s_TextInfosCommon, () => new TextInfo());
				}
				return TextHandle.s_TextInfosCommon;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00022E24 File Offset: 0x00021024
		private static void InitArray<T>(ref T[] array, Func<T> createInstance)
		{
			bool flag = array != null;
			if (!flag)
			{
				array = new T[JobsUtility.ThreadIndexCount];
				for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
				{
					array[i] = createInstance();
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00022E6D File Offset: 0x0002106D
		internal static TextInfo textInfoCommon
		{
			get
			{
				return TextHandle.textInfosCommon[JobsUtility.ThreadIndex];
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00022E7A File Offset: 0x0002107A
		private static TextGenerator generator
		{
			get
			{
				return TextHandle.generators[JobsUtility.ThreadIndex];
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00022E87 File Offset: 0x00021087
		internal static TextGenerationSettings settings
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
			get
			{
				return TextHandle.settingsArray[JobsUtility.ThreadIndex];
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00022E94 File Offset: 0x00021094
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00022E9C File Offset: 0x0002109C
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal Vector2 preferredSize { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00022EA5 File Offset: 0x000210A5
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00022EAD File Offset: 0x000210AD
		internal LinkedListNode<TextInfo> TextInfoNode { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00022EB6 File Offset: 0x000210B6
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00022EBE File Offset: 0x000210BE
		internal bool IsCachedPermanent { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00022EC7 File Offset: 0x000210C7
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00022ECF File Offset: 0x000210CF
		internal bool IsCachedTemporary { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00022ED8 File Offset: 0x000210D8
		internal bool useAdvancedText
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
			get
			{
				return this.IsAdvancedTextEnabledForElement();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00022EF0 File Offset: 0x000210F0
		internal int characterCount
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
			get
			{
				return this.useAdvancedText ? this.nativeSettings.text.Length : this.textInfo.characterCount;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00022F28 File Offset: 0x00021128
		public virtual void AddTextInfoToPermanentCache()
		{
			bool useAdvancedText = this.useAdvancedText;
			if (useAdvancedText)
			{
				throw new InvalidOperationException("Method is virtual and should be overriden in ATGTextHanle, the only valid handle for ATG");
			}
			TextHandle.s_PermanentCache.AddTextInfoToCache(this);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00022F5C File Offset: 0x0002115C
		public void AddTextInfoToTemporaryCache(int hashCode)
		{
			bool useAdvancedText = this.useAdvancedText;
			if (!useAdvancedText)
			{
				TextHandle.s_TemporaryCache.AddTextInfoToCache(this, hashCode);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00022F83 File Offset: 0x00021183
		public void RemoveTextInfoFromTemporaryCache()
		{
			TextHandle.s_TemporaryCache.RemoveTextInfoFromCache(this);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00022F94 File Offset: 0x00021194
		public void RemoveTextInfoFromPermanentCache()
		{
			bool flag = this.textGenerationInfo != IntPtr.Zero;
			if (flag)
			{
				TextGenerationInfo.Destroy(this.textGenerationInfo);
				this.textGenerationInfo = IntPtr.Zero;
			}
			else
			{
				TextHandle.s_PermanentCache.RemoveTextInfoFromCache(this);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00022FDF File Offset: 0x000211DF
		public static void UpdateCurrentFrame()
		{
			TextHandle.s_TemporaryCache.UpdateCurrentFrame();
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00022FF0 File Offset: 0x000211F0
		internal TextInfo textInfo
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
			get
			{
				bool flag = this.TextInfoNode == null;
				TextInfo textInfo;
				if (flag)
				{
					textInfo = TextHandle.textInfoCommon;
				}
				else
				{
					textInfo = this.TextInfoNode.Value;
				}
				return textInfo;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00023024 File Offset: 0x00021224
		internal bool IsTextInfoAllocated()
		{
			return this.textInfo != null;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0002303F File Offset: 0x0002123F
		public void SetDirty()
		{
			this.isDirty = true;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0002304C File Offset: 0x0002124C
		public bool IsDirty(int hashCode)
		{
			bool flag = this.m_PreviousGenerationSettingsHash == hashCode && !this.isDirty && (this.IsCachedTemporary || this.IsCachedPermanent);
			return !flag;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0002308C File Offset: 0x0002128C
		public float ComputeTextWidth(TextGenerationSettings tgs)
		{
			this.UpdatePreferredValues(tgs);
			return this.preferredSize.x;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000230B4 File Offset: 0x000212B4
		public float ComputeTextHeight(TextGenerationSettings tgs)
		{
			this.UpdatePreferredValues(tgs);
			return this.preferredSize.y;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000230D9 File Offset: 0x000212D9
		public virtual bool IsPlaceholder
		{
			get
			{
				return this.m_IsPlaceholder;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000230E1 File Offset: 0x000212E1
		protected void UpdatePreferredValues(TextGenerationSettings tgs)
		{
			this.preferredSize = TextHandle.generator.GetPreferredValues(tgs, TextHandle.textInfoCommon);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000230FC File Offset: 0x000212FC
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal TextInfo Update()
		{
			return this.UpdateWithHash(TextHandle.settings.GetHashCode());
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00023120 File Offset: 0x00021320
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal TextInfo UpdateWithHash(int hashCode)
		{
			this.m_ScreenRect = TextHandle.settings.screenRect;
			this.m_LineHeightDefault = TextHandle.GetLineHeightDefault(TextHandle.settings);
			this.m_IsPlaceholder = TextHandle.settings.isPlaceholder;
			bool flag = !this.IsDirty(hashCode);
			TextInfo textInfo;
			if (flag)
			{
				textInfo = this.textInfo;
			}
			else
			{
				bool flag2 = TextHandle.settings.fontAsset == null || TextHandle.settings.fontAsset.characterLookupTable == null;
				if (flag2)
				{
					Debug.LogWarning("Can't Generate Mesh, No Font Asset has been assigned.");
					textInfo = this.textInfo;
				}
				else
				{
					TextHandle.generator.GenerateText(TextHandle.settings, this.textInfo);
					this.m_PreviousGenerationSettingsHash = hashCode;
					this.isDirty = false;
					this.m_IsEllided = TextHandle.generator.isTextTruncated;
					textInfo = this.textInfo;
				}
			}
			return textInfo;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000231F4 File Offset: 0x000213F4
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal bool PrepareFontAsset()
		{
			bool flag = TextHandle.settings.fontAsset == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.IsDirty(TextHandle.settings.GetHashCode());
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = TextHandle.generator.PrepareFontAsset(TextHandle.settings);
					flag2 = flag4;
				}
			}
			return flag2;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0002324C File Offset: 0x0002144C
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal void UpdatePreferredSize()
		{
			bool flag = this.textInfo.characterCount <= 0;
			if (!flag)
			{
				float num = float.MinValue;
				float num2 = this.textInfo.textElementInfo[this.textInfo.characterCount - 1].descender;
				float num3 = 0f;
				for (int i = 0; i < this.textInfo.lineCount; i++)
				{
					LineInfo lineInfo = this.textInfo.lineInfo[i];
					num = Mathf.Max(num, this.textInfo.textElementInfo[lineInfo.firstVisibleCharacterIndex].ascender);
					num2 = Mathf.Min(num2, this.textInfo.textElementInfo[lineInfo.firstVisibleCharacterIndex].descender);
					num3 = (TextHandle.settings.isIMGUI ? Mathf.Max(num3, lineInfo.length) : Mathf.Max(num3, lineInfo.lineExtents.max.x - lineInfo.lineExtents.min.x));
				}
				float num4 = num - num2;
				num3 = (float)((int)(num3 * 100f + 1f)) / 100f;
				num4 = (float)((int)(num4 * 100f + 1f)) / 100f;
				this.preferredSize = new Vector2(num3, num4);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000233B0 File Offset: 0x000215B0
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static float ConvertPixelUnitsToTextCoreRelativeUnits(float fontSize, FontAsset fontAsset)
		{
			float num = 1f / (float)fontAsset.atlasPadding;
			float num2 = fontAsset.faceInfo.pointSize / fontSize;
			return num * num2;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000233E8 File Offset: 0x000215E8
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal static float GetLineHeightDefault(TextGenerationSettings settings)
		{
			bool flag = settings != null && settings.fontAsset != null;
			float num;
			if (flag)
			{
				num = settings.fontAsset.faceInfo.lineHeight / settings.fontAsset.faceInfo.pointSize * settings.fontSize;
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00023450 File Offset: 0x00021650
		public virtual Vector2 GetCursorPositionFromStringIndexUsingCharacterHeight(int index, bool inverseYAxis = true)
		{
			this.AddTextInfoToPermanentCache();
			return this.useAdvancedText ? TextSelectionService.GetCursorPositionFromLogicalIndex(this.textGenerationInfo, index) : this.textInfo.GetCursorPositionFromStringIndexUsingCharacterHeight(index, this.m_ScreenRect, this.m_LineHeightDefault, inverseYAxis);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00023498 File Offset: 0x00021698
		public Vector2 GetCursorPositionFromStringIndexUsingLineHeight(int index, bool useXAdvance = false, bool inverseYAxis = true)
		{
			this.AddTextInfoToPermanentCache();
			return this.useAdvancedText ? TextSelectionService.GetCursorPositionFromLogicalIndex(this.textGenerationInfo, index) : this.textInfo.GetCursorPositionFromStringIndexUsingLineHeight(index, this.m_ScreenRect, this.m_LineHeightDefault, useXAdvance, inverseYAxis);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000234E4 File Offset: 0x000216E4
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal Rect[] GetHighlightRectangles(int cursorIndex, int selectIndex)
		{
			bool flag = !this.useAdvancedText;
			Rect[] array;
			if (flag)
			{
				Debug.LogError("Cannot use GetHighlightRectangles while using Standard Text");
				array = new Rect[0];
			}
			else
			{
				array = TextSelectionService.GetHighlightRectangles(this.textGenerationInfo, cursorIndex, selectIndex);
			}
			return array;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00023528 File Offset: 0x00021728
		public int GetCursorIndexFromPosition(Vector2 position, bool inverseYAxis = true)
		{
			return this.useAdvancedText ? TextSelectionService.GetCursorLogicalIndexFromPosition(this.textGenerationInfo, position) : this.textInfo.GetCursorIndexFromPosition(position, this.m_ScreenRect, inverseYAxis);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00023564 File Offset: 0x00021764
		public int LineDownCharacterPosition(int originalLogicalPos)
		{
			return this.useAdvancedText ? TextSelectionService.LineDownCharacterPosition(this.textGenerationInfo, originalLogicalPos) : this.textInfo.LineDownCharacterPosition(originalLogicalPos);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00023598 File Offset: 0x00021798
		public int LineUpCharacterPosition(int originalLogicalPos)
		{
			return this.useAdvancedText ? TextSelectionService.LineUpCharacterPosition(this.textGenerationInfo, originalLogicalPos) : this.textInfo.LineUpCharacterPosition(originalLogicalPos);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000235CC File Offset: 0x000217CC
		public int FindWordIndex(int cursorIndex)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use FindWordIndex while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.FindWordIndex(cursorIndex);
			}
			return num;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00023604 File Offset: 0x00021804
		public int FindNearestLine(Vector2 position)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use FindNearestLine while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.FindNearestLine(position);
			}
			return num;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0002363C File Offset: 0x0002183C
		public int FindNearestCharacterOnLine(Vector2 position, int line, bool visibleOnly)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use FindNearestCharacterOnLine while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.FindNearestCharacterOnLine(position, line, visibleOnly);
			}
			return num;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00023678 File Offset: 0x00021878
		public int FindIntersectingLink(Vector3 position, bool inverseYAxis = true)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use FindIntersectingLink while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.FindIntersectingLink(position, this.m_ScreenRect, inverseYAxis);
			}
			return num;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000236B8 File Offset: 0x000218B8
		public int GetCorrespondingStringIndex(int index)
		{
			return this.useAdvancedText ? index : this.textInfo.GetCorrespondingStringIndex(index);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000236E4 File Offset: 0x000218E4
		public int GetCorrespondingCodePointIndex(int stringIndex)
		{
			return this.useAdvancedText ? stringIndex : this.textInfo.GetCorrespondingCodePointIndex(stringIndex);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00023710 File Offset: 0x00021910
		public LineInfo GetLineInfoFromCharacterIndex(int index)
		{
			bool useAdvancedText = this.useAdvancedText;
			LineInfo lineInfo;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use GetLineInfoFromCharacterIndex while using Advanced Text");
				lineInfo = default(LineInfo);
			}
			else
			{
				lineInfo = this.textInfo.GetLineInfoFromCharacterIndex(index);
			}
			return lineInfo;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00023750 File Offset: 0x00021950
		public int GetLineNumber(int index)
		{
			return this.useAdvancedText ? TextSelectionService.GetLineNumber(this.textGenerationInfo, index) : this.textInfo.GetLineNumber(index);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00023784 File Offset: 0x00021984
		public float GetLineHeight(int lineNumber)
		{
			return this.useAdvancedText ? TextSelectionService.GetLineHeight(this.textGenerationInfo, lineNumber) : this.textInfo.GetLineHeight(lineNumber);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000237B8 File Offset: 0x000219B8
		public float GetLineHeightFromCharacterIndex(int index)
		{
			return this.useAdvancedText ? TextSelectionService.GetCharacterHeightFromIndex(this.textGenerationInfo, index) : this.textInfo.GetLineHeightFromCharacterIndex(index);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000237EC File Offset: 0x000219EC
		public float GetCharacterHeightFromIndex(int index)
		{
			return this.useAdvancedText ? TextSelectionService.GetCharacterHeightFromIndex(this.textGenerationInfo, index) : this.textInfo.GetCharacterHeightFromIndex(index);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00023820 File Offset: 0x00021A20
		public string Substring(int startIndex, int length)
		{
			return this.useAdvancedText ? TextSelectionService.Substring(this.textGenerationInfo, startIndex, startIndex + length) : this.textInfo.Substring(startIndex, length);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00023858 File Offset: 0x00021A58
		public int PreviousCodePointIndex(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use PreviousCodePointIndex while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.PreviousCodePointIndex(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00023894 File Offset: 0x00021A94
		public int NextCodePointIndex(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use NextCodePointIndex while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.NextCodePointIndex(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000238D0 File Offset: 0x00021AD0
		public int GetStartOfNextWord(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use GetStartOfNextWord while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.GetStartOfNextWord(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0002390C File Offset: 0x00021B0C
		public int GetEndOfPreviousWord(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use GetEndOfPreviousWord while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.GetEndOfPreviousWord(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00023948 File Offset: 0x00021B48
		public int GetFirstCharacterIndexOnLine(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use GetFirstCharacterIndexOnLine while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.GetFirstCharacterIndexOnLine(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00023984 File Offset: 0x00021B84
		public int GetLastCharacterIndexOnLine(int currentIndex)
		{
			bool flag = !this.useAdvancedText;
			int num;
			if (flag)
			{
				Debug.LogError("Cannot use GetLastCharacterIndexOnLine while using Standard Text");
				num = 0;
			}
			else
			{
				num = TextSelectionService.GetLastCharacterIndexOnLine(this.textGenerationInfo, currentIndex);
			}
			return num;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000239C0 File Offset: 0x00021BC0
		public int IndexOf(char value, int startIndex)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use IndexOf while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.IndexOf(value, startIndex);
			}
			return num;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000239FC File Offset: 0x00021BFC
		public int LastIndexOf(char value, int startIndex)
		{
			bool useAdvancedText = this.useAdvancedText;
			int num;
			if (useAdvancedText)
			{
				Debug.LogError("Cannot use LastIndexOf while using Advanced Text");
				num = 0;
			}
			else
			{
				num = this.textInfo.LastIndexOf(value, startIndex);
			}
			return num;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00023A38 File Offset: 0x00021C38
		public void SelectCurrentWord(int index, ref int cursorIndex, ref int selectIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectCurrentWord while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectCurrentWord(this.textGenerationInfo, index, ref cursorIndex, ref selectIndex);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00023A70 File Offset: 0x00021C70
		public void SelectCurrentParagraph(ref int cursorIndex, ref int selectIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectCurrentParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectCurrentParagraph(this.textGenerationInfo, ref cursorIndex, ref selectIndex);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00023AA8 File Offset: 0x00021CA8
		public void SelectToPreviousParagraph(ref int cursorIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectToPreviousParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectToPreviousParagraph(this.textGenerationInfo, ref cursorIndex);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00023AE0 File Offset: 0x00021CE0
		public void SelectToNextParagraph(ref int cursorIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectToNextParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectToNextParagraph(this.textGenerationInfo, ref cursorIndex);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00023B18 File Offset: 0x00021D18
		public void SelectToStartOfParagraph(ref int cursorIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectToStartOfParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectToStartOfParagraph(this.textGenerationInfo, ref cursorIndex);
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00023B50 File Offset: 0x00021D50
		public void SelectToEndOfParagraph(ref int cursorIndex)
		{
			bool flag = !this.useAdvancedText;
			if (flag)
			{
				Debug.LogError("Cannot use SelectToEndOfParagraph while using Standard Text");
			}
			else
			{
				TextSelectionService.SelectToEndOfParagraph(this.textGenerationInfo, ref cursorIndex);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00023B88 File Offset: 0x00021D88
		internal virtual bool IsAdvancedTextEnabledForElement()
		{
			return false;
		}

		// Token: 0x0400035E RID: 862
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static TextHandleTemporaryCache s_TemporaryCache = new TextHandleTemporaryCache();

		// Token: 0x0400035F RID: 863
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static TextHandlePermanentCache s_PermanentCache = new TextHandlePermanentCache();

		// Token: 0x04000360 RID: 864
		private static TextGenerationSettings[] s_Settings;

		// Token: 0x04000361 RID: 865
		private static TextGenerator[] s_Generators;

		// Token: 0x04000362 RID: 866
		private static TextInfo[] s_TextInfosCommon;

		// Token: 0x04000363 RID: 867
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal NativeTextGenerationSettings nativeSettings = NativeTextGenerationSettings.Default;

		// Token: 0x04000365 RID: 869
		private Rect m_ScreenRect;

		// Token: 0x04000366 RID: 870
		private float m_LineHeightDefault;

		// Token: 0x04000367 RID: 871
		private bool m_IsPlaceholder;

		// Token: 0x04000368 RID: 872
		protected bool m_IsEllided;

		// Token: 0x04000369 RID: 873
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal IntPtr textGenerationInfo = IntPtr.Zero;

		// Token: 0x0400036D RID: 877
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal int m_PreviousGenerationSettingsHash;

		// Token: 0x0400036E RID: 878
		protected bool isDirty;
	}
}
