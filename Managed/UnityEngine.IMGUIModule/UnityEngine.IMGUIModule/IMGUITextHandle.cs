using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	internal class IMGUITextHandle : TextHandle
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x0000E499 File Offset: 0x0000C699
		internal static void EmptyCache()
		{
			GUIStyle.Internal_CleanupAllTextGenerator();
			IMGUITextHandle.textHandles.Clear();
			IMGUITextHandle.textHandlesTuple.Clear();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
		internal static void EmptyManagedCache()
		{
			IMGUITextHandle.textHandles.Clear();
			IMGUITextHandle.textHandlesTuple.Clear();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		internal static IMGUITextHandle GetTextHandle(GUIStyle style, Rect position, string content, Color32 textColor)
		{
			bool flag = false;
			IMGUITextHandle.ConvertGUIStyleToGenerationSettings(TextHandle.settings, style, textColor, content, position);
			return IMGUITextHandle.GetTextHandle(TextHandle.settings, false, ref flag);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000E50C File Offset: 0x0000C70C
		internal static IMGUITextHandle GetTextHandle(GUIStyle style, Rect position, string content, Color32 textColor, ref bool isCached)
		{
			IMGUITextHandle.ConvertGUIStyleToGenerationSettings(TextHandle.settings, style, textColor, content, position);
			return IMGUITextHandle.GetTextHandle(TextHandle.settings, true, ref isCached);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000E540 File Offset: 0x0000C740
		private static bool ShouldCleanup(float currentTime, float lastTime, float cleanupThreshold)
		{
			float num = currentTime - lastTime;
			return num > cleanupThreshold || num < 0f;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000E568 File Offset: 0x0000C768
		private static void ClearUnusedTextHandles()
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			while (IMGUITextHandle.textHandlesTuple.Count > 0)
			{
				IMGUITextHandle.TextHandleTuple textHandleTuple = IMGUITextHandle.textHandlesTuple.First<IMGUITextHandle.TextHandleTuple>();
				bool flag = IMGUITextHandle.ShouldCleanup(realtimeSinceStartup, textHandleTuple.lastTimeUsed, 5f);
				if (!flag)
				{
					break;
				}
				GUIStyle.Internal_DestroyTextGenerator(textHandleTuple.hashCode);
				IMGUITextHandle imguitextHandle;
				bool flag2 = IMGUITextHandle.textHandles.TryGetValue(textHandleTuple.hashCode, out imguitextHandle);
				if (flag2)
				{
					imguitextHandle.RemoveTextInfoFromPermanentCache();
				}
				IMGUITextHandle.textHandles.Remove(textHandleTuple.hashCode);
				IMGUITextHandle.textHandlesTuple.RemoveFirst();
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000E608 File Offset: 0x0000C808
		private static IMGUITextHandle GetTextHandle(TextGenerationSettings settings, bool isCalledFromNative, ref bool isCached)
		{
			isCached = false;
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			bool flag = IMGUITextHandle.ShouldCleanup(realtimeSinceStartup, IMGUITextHandle.lastCleanupTime, 30f) || IMGUITextHandle.newHandlesSinceCleanup > 500;
			if (flag)
			{
				IMGUITextHandle.ClearUnusedTextHandles();
				IMGUITextHandle.lastCleanupTime = realtimeSinceStartup;
				IMGUITextHandle.newHandlesSinceCleanup = 0;
			}
			int hashCode = settings.GetHashCode();
			IMGUITextHandle imguitextHandle;
			bool flag2 = IMGUITextHandle.textHandles.TryGetValue(hashCode, out imguitextHandle);
			IMGUITextHandle imguitextHandle2;
			if (flag2)
			{
				IMGUITextHandle.textHandlesTuple.Remove(imguitextHandle.tuple);
				IMGUITextHandle.textHandlesTuple.AddLast(imguitextHandle.tuple);
				isCached = !isCalledFromNative || imguitextHandle.isCachedOnNative;
				bool flag3 = !imguitextHandle.isCachedOnNative && isCalledFromNative;
				if (flag3)
				{
					imguitextHandle.UpdateWithHash(hashCode);
					imguitextHandle.UpdatePreferredSize();
					imguitextHandle.isCachedOnNative = true;
				}
				imguitextHandle2 = imguitextHandle;
			}
			else
			{
				IMGUITextHandle imguitextHandle3 = new IMGUITextHandle();
				IMGUITextHandle.TextHandleTuple textHandleTuple = new IMGUITextHandle.TextHandleTuple(realtimeSinceStartup, hashCode);
				LinkedListNode<IMGUITextHandle.TextHandleTuple> linkedListNode = new LinkedListNode<IMGUITextHandle.TextHandleTuple>(textHandleTuple);
				imguitextHandle3.tuple = linkedListNode;
				IMGUITextHandle.textHandles[hashCode] = imguitextHandle3;
				imguitextHandle3.UpdateWithHash(hashCode);
				imguitextHandle3.UpdatePreferredSize();
				IMGUITextHandle.textHandlesTuple.AddLast(linkedListNode);
				imguitextHandle3.isCachedOnNative = isCalledFromNative;
				IMGUITextHandle.newHandlesSinceCleanup++;
				imguitextHandle2 = imguitextHandle3;
			}
			return imguitextHandle2;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000E738 File Offset: 0x0000C938
		internal static float GetLineHeight(GUIStyle style)
		{
			IMGUITextHandle.ConvertGUIStyleToGenerationSettings(TextHandle.settings, style, Color.white, "", Rect.zero);
			return TextHandle.GetLineHeightDefault(TextHandle.settings);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E770 File Offset: 0x0000C970
		internal Vector2 GetPreferredSize()
		{
			return base.preferredSize;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E788 File Offset: 0x0000C988
		internal int GetNumCharactersThatFitWithinWidth(float width)
		{
			this.AddTextInfoToPermanentCache();
			int characterCount = base.textInfo.lineInfo[0].characterCount;
			float num = 0f;
			int i;
			for (i = 0; i < characterCount; i++)
			{
				num += base.textInfo.textElementInfo[i].xAdvance - base.textInfo.textElementInfo[i].origin;
				bool flag = num > width;
				if (flag)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E810 File Offset: 0x0000CA10
		public Rect[] GetHyperlinkRects(Rect content)
		{
			this.AddTextInfoToPermanentCache();
			List<Rect> list = new List<Rect>();
			for (int i = 0; i < base.textInfo.linkCount; i++)
			{
				Vector2 vector = base.GetCursorPositionFromStringIndexUsingLineHeight(base.textInfo.linkInfo[i].linkTextfirstCharacterIndex, false, true) + new Vector2(content.x, content.y);
				Vector2 vector2 = base.GetCursorPositionFromStringIndexUsingLineHeight(base.textInfo.linkInfo[i].linkTextLength + base.textInfo.linkInfo[i].linkTextfirstCharacterIndex, false, true) + new Vector2(content.x, content.y);
				float lineHeight = base.textInfo.lineInfo[0].lineHeight;
				bool flag = vector.y == vector2.y;
				if (flag)
				{
					list.Add(new Rect(vector.x, vector.y - lineHeight, vector2.x - vector.x, lineHeight));
				}
				else
				{
					list.Add(new Rect(vector.x, vector.y - lineHeight, base.textInfo.lineInfo[0].width - vector.x, lineHeight));
					list.Add(new Rect(content.x, vector.y, base.textInfo.lineInfo[0].width, vector2.y - vector.y - lineHeight));
					bool flag2 = vector2.x != 0f;
					if (flag2)
					{
						list.Add(new Rect(content.x, vector2.y - lineHeight, vector2.x, lineHeight));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		private static void ConvertGUIStyleToGenerationSettings(TextGenerationSettings settings, GUIStyle style, Color textColor, string text, Rect rect)
		{
			settings.textSettings = RuntimeTextSettings.defaultTextSettings;
			bool flag = settings.textSettings == null;
			if (!flag)
			{
				Font font = style.font;
				bool flag2 = !font;
				if (flag2)
				{
					font = GUIStyle.GetDefaultFont();
				}
				bool flag3 = style.fontSize > 0;
				if (flag3)
				{
					settings.fontSize = (float)style.fontSize;
				}
				else
				{
					bool flag4 = font;
					if (flag4)
					{
						settings.fontSize = (float)font.fontSize;
					}
					else
					{
						settings.fontSize = 13f;
					}
				}
				settings.fontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(style.fontStyle);
				settings.fontAsset = settings.textSettings.GetCachedFontAsset(font);
				bool flag5 = settings.fontAsset == null;
				if (!flag5)
				{
					settings.fontAsset.material.SetFloat("_Sharpness", 0.5f);
					settings.screenRect = new Rect(0f, 0f, rect.width, rect.height);
					settings.text = text;
					TextAnchor textAnchor = style.alignment;
					bool flag6 = style.imagePosition == ImagePosition.ImageAbove;
					if (flag6)
					{
						switch (style.alignment)
						{
						case TextAnchor.MiddleLeft:
						case TextAnchor.LowerLeft:
							textAnchor = TextAnchor.UpperLeft;
							break;
						case TextAnchor.MiddleCenter:
						case TextAnchor.LowerCenter:
							textAnchor = TextAnchor.UpperCenter;
							break;
						case TextAnchor.MiddleRight:
						case TextAnchor.LowerRight:
							textAnchor = TextAnchor.UpperRight;
							break;
						}
					}
					settings.textAlignment = TextGeneratorUtilities.LegacyAlignmentToNewAlignment(textAnchor);
					settings.overflowMode = IMGUITextHandle.LegacyClippingToNewOverflow(style.clipping);
					bool flag7 = rect.width > 0f && style.wordWrap;
					if (flag7)
					{
						settings.textWrappingMode = TextWrappingMode.PreserveWhitespace;
					}
					else
					{
						settings.textWrappingMode = TextWrappingMode.PreserveWhitespaceNoWrap;
					}
					settings.richText = style.richText;
					settings.parseControlCharacters = false;
					settings.isPlaceholder = false;
					settings.isRightToLeft = false;
					settings.characterSpacing = 0f;
					settings.wordSpacing = 0f;
					settings.paragraphSpacing = 0f;
					settings.color = textColor;
					settings.isIMGUI = true;
					settings.shouldConvertToLinearSpace = QualitySettings.activeColorSpace == ColorSpace.Linear;
				}
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000EC04 File Offset: 0x0000CE04
		private static TextOverflowMode LegacyClippingToNewOverflow(TextClipping clipping)
		{
			switch (clipping)
			{
			case TextClipping.Clip:
				return TextOverflowMode.Masking;
			case TextClipping.Ellipsis:
				return TextOverflowMode.Ellipsis;
			}
			return TextOverflowMode.Overflow;
		}

		// Token: 0x040000F9 RID: 249
		internal LinkedListNode<IMGUITextHandle.TextHandleTuple> tuple;

		// Token: 0x040000FA RID: 250
		private const float sFallbackFontSize = 13f;

		// Token: 0x040000FB RID: 251
		private const float sTimeToFlush = 5f;

		// Token: 0x040000FC RID: 252
		private const float sTimeBetweenCleanupRuns = 30f;

		// Token: 0x040000FD RID: 253
		private const int sNewHandlesBetweenCleanupRuns = 500;

		// Token: 0x040000FE RID: 254
		private static Dictionary<int, IMGUITextHandle> textHandles = new Dictionary<int, IMGUITextHandle>();

		// Token: 0x040000FF RID: 255
		private static LinkedList<IMGUITextHandle.TextHandleTuple> textHandlesTuple = new LinkedList<IMGUITextHandle.TextHandleTuple>();

		// Token: 0x04000100 RID: 256
		private static float lastCleanupTime;

		// Token: 0x04000101 RID: 257
		private static int newHandlesSinceCleanup = 0;

		// Token: 0x04000102 RID: 258
		internal bool isCachedOnNative = false;

		// Token: 0x02000037 RID: 55
		internal class TextHandleTuple
		{
			// Token: 0x06000460 RID: 1120 RVA: 0x0000EC63 File Offset: 0x0000CE63
			public TextHandleTuple(float lastTimeUsed, int hashCode)
			{
				this.hashCode = hashCode;
				this.lastTimeUsed = lastTimeUsed;
			}

			// Token: 0x04000103 RID: 259
			public float lastTimeUsed;

			// Token: 0x04000104 RID: 260
			public int hashCode;
		}
	}
}
