using System;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000064 RID: 100
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal class TextInfo
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0002412E File Offset: 0x0002232E
		public void RemoveFromCache()
		{
			Action action = this.removedFromCache;
			if (action != null)
			{
				action();
			}
			this.removedFromCache = null;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0002414C File Offset: 0x0002234C
		public TextInfo()
		{
			this.textElementInfo = new TextElementInfo[4];
			this.wordInfo = new WordInfo[1];
			this.lineInfo = new LineInfo[1];
			this.linkInfo = Array.Empty<LinkInfo>();
			this.meshInfo = Array.Empty<MeshInfo>();
			this.materialCount = 0;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000241AC File Offset: 0x000223AC
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void Clear()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.spriteCount = 0;
			this.hasMultipleColors = false;
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].vertexCount = 0;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00024218 File Offset: 0x00022418
		internal void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00024250 File Offset: 0x00022450
		internal void ClearLineInfo()
		{
			bool flag = this.lineInfo == null;
			if (flag)
			{
				this.lineInfo = new LineInfo[1];
			}
			for (int i = 0; i < this.lineInfo.Length; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].ascender = TextInfo.s_InfinityVectorNegative.x;
				this.lineInfo[i].baseline = 0f;
				this.lineInfo[i].descender = TextInfo.s_InfinityVectorPositive.x;
				this.lineInfo[i].maxAdvance = 0f;
				this.lineInfo[i].marginLeft = 0f;
				this.lineInfo[i].marginRight = 0f;
				this.lineInfo[i].lineExtents.min = TextInfo.s_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TextInfo.s_InfinityVectorNegative;
				this.lineInfo[i].width = 0f;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000243B8 File Offset: 0x000225B8
		internal static void Resize<T>(ref T[] array, int size)
		{
			int num = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			Array.Resize<T>(ref array, num);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000243E8 File Offset: 0x000225E8
		internal static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			bool flag = size == array.Length;
			if (!flag)
			{
				Array.Resize<T>(ref array, size);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0002442C File Offset: 0x0002262C
		public virtual Vector2 GetCursorPositionFromStringIndexUsingCharacterHeight(int index, Rect screenRect, float lineHeight, bool inverseYAxis = true)
		{
			Vector2 vector = screenRect.position;
			bool flag = this.characterCount == 0;
			Vector2 vector2;
			if (flag)
			{
				vector2 = (inverseYAxis ? new Vector2(0f, lineHeight) : vector);
			}
			else
			{
				int num = ((index >= this.characterCount) ? (this.characterCount - 1) : index);
				TextElementInfo textElementInfo = this.textElementInfo[num];
				float descender = textElementInfo.descender;
				float num2 = ((index >= this.characterCount) ? textElementInfo.xAdvance : textElementInfo.origin);
				vector += (inverseYAxis ? new Vector2(num2, screenRect.height - descender) : new Vector2(num2, descender));
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000244D8 File Offset: 0x000226D8
		public Vector2 GetCursorPositionFromStringIndexUsingLineHeight(int index, Rect screenRect, float lineHeight, bool useXAdvance = false, bool inverseYAxis = true)
		{
			Vector2 position = screenRect.position;
			bool flag = this.characterCount == 0 || index < 0;
			Vector2 vector;
			if (flag)
			{
				vector = (inverseYAxis ? new Vector2(0f, lineHeight) : position);
			}
			else
			{
				int num = index;
				bool flag2 = index >= this.characterCount;
				if (flag2)
				{
					num = this.characterCount - 1;
					useXAdvance = true;
				}
				TextElementInfo textElementInfo = this.textElementInfo[num];
				LineInfo lineInfo = this.lineInfo[textElementInfo.lineNumber];
				float num2 = (useXAdvance ? textElementInfo.xAdvance : textElementInfo.origin);
				float num3 = (inverseYAxis ? (screenRect.height - lineInfo.descender) : lineInfo.descender);
				vector = position + new Vector2(num2, num3);
			}
			return vector;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000245A4 File Offset: 0x000227A4
		public int GetCursorIndexFromPosition(Vector2 position, Rect screenRect, bool inverseYAxis = true)
		{
			if (inverseYAxis)
			{
				position.y = screenRect.height - position.y;
			}
			int num = 0;
			bool flag = this.lineCount > 1;
			if (flag)
			{
				num = this.FindNearestLine(position);
			}
			int num2 = this.FindNearestCharacterOnLine(position, num, false);
			TextElementInfo textElementInfo = this.textElementInfo[num2];
			Vector3 bottomLeft = textElementInfo.bottomLeft;
			Vector3 topRight = textElementInfo.topRight;
			float num3 = (position.x - bottomLeft.x) / (topRight.x - bottomLeft.x);
			return (num3 < 0.5f || textElementInfo.character == 10U) ? num2 : (num2 + 1);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0002464C File Offset: 0x0002284C
		public int LineDownCharacterPosition(int originalPos)
		{
			bool flag = originalPos >= this.characterCount;
			int num;
			if (flag)
			{
				num = this.characterCount - 1;
			}
			else
			{
				TextElementInfo textElementInfo = this.textElementInfo[originalPos];
				int lineNumber = textElementInfo.lineNumber;
				bool flag2 = lineNumber + 1 >= this.lineCount;
				if (flag2)
				{
					num = this.characterCount - 1;
				}
				else
				{
					int lastCharacterIndex = this.lineInfo[lineNumber + 1].lastCharacterIndex;
					int num2 = -1;
					float num3 = float.PositiveInfinity;
					float num4 = 0f;
					int i = this.lineInfo[lineNumber + 1].firstCharacterIndex;
					while (i < lastCharacterIndex)
					{
						TextElementInfo textElementInfo2 = this.textElementInfo[i];
						float num5 = textElementInfo.origin - textElementInfo2.origin;
						float num6 = num5 / (textElementInfo2.xAdvance - textElementInfo2.origin);
						bool flag3 = num6 >= 0f && num6 <= 1f;
						if (flag3)
						{
							bool flag4 = num6 < 0.5f;
							if (flag4)
							{
								return i;
							}
							return i + 1;
						}
						else
						{
							num5 = Mathf.Abs(num5);
							bool flag5 = num5 < num3;
							if (flag5)
							{
								num2 = i;
								num3 = num5;
								num4 = num6;
							}
							i++;
						}
					}
					bool flag6 = num2 == -1;
					if (flag6)
					{
						num = lastCharacterIndex;
					}
					else
					{
						bool flag7 = num4 < 0.5f;
						if (flag7)
						{
							num = num2;
						}
						else
						{
							num = num2 + 1;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000247C4 File Offset: 0x000229C4
		public int LineUpCharacterPosition(int originalPos)
		{
			bool flag = originalPos >= this.characterCount;
			if (flag)
			{
				originalPos--;
			}
			TextElementInfo textElementInfo = this.textElementInfo[originalPos];
			int lineNumber = textElementInfo.lineNumber;
			bool flag2 = lineNumber - 1 < 0;
			int num;
			if (flag2)
			{
				num = 0;
			}
			else
			{
				int num2 = this.lineInfo[lineNumber].firstCharacterIndex - 1;
				int num3 = -1;
				float num4 = float.PositiveInfinity;
				float num5 = 0f;
				int i = this.lineInfo[lineNumber - 1].firstCharacterIndex;
				while (i < num2)
				{
					TextElementInfo textElementInfo2 = this.textElementInfo[i];
					float num6 = textElementInfo.origin - textElementInfo2.origin;
					float num7 = num6 / (textElementInfo2.xAdvance - textElementInfo2.origin);
					bool flag3 = num7 >= 0f && num7 <= 1f;
					if (flag3)
					{
						bool flag4 = num7 < 0.5f;
						if (flag4)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num6 = Mathf.Abs(num6);
						bool flag5 = num6 < num4;
						if (flag5)
						{
							num3 = i;
							num4 = num6;
							num5 = num7;
						}
						i++;
					}
				}
				bool flag6 = num3 == -1;
				if (flag6)
				{
					num = num2;
				}
				else
				{
					bool flag7 = num5 < 0.5f;
					if (flag7)
					{
						num = num3;
					}
					else
					{
						num = num3 + 1;
					}
				}
			}
			return num;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00024924 File Offset: 0x00022B24
		public int FindWordIndex(int cursorIndex)
		{
			for (int i = 0; i < this.wordCount; i++)
			{
				WordInfo wordInfo = this.wordInfo[i];
				bool flag = wordInfo.firstCharacterIndex <= cursorIndex && wordInfo.lastCharacterIndex >= cursorIndex;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00024980 File Offset: 0x00022B80
		public int FindNearestLine(Vector2 position)
		{
			float num = float.PositiveInfinity;
			int num2 = -1;
			for (int i = 0; i < this.lineCount; i++)
			{
				LineInfo lineInfo = this.lineInfo[i];
				float ascender = lineInfo.ascender;
				float descender = lineInfo.descender;
				bool flag = ascender > position.y && descender < position.y;
				if (flag)
				{
					return i;
				}
				float num3 = Mathf.Abs(ascender - position.y);
				float num4 = Mathf.Abs(descender - position.y);
				float num5 = Mathf.Min(num3, num4);
				bool flag2 = num5 < num;
				if (flag2)
				{
					num = num5;
					num2 = i;
				}
			}
			return num2;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00024A3C File Offset: 0x00022C3C
		public int FindNearestCharacterOnLine(Vector2 position, int line, bool visibleOnly)
		{
			bool flag = line >= this.lineInfo.Length || line < 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int firstCharacterIndex = this.lineInfo[line].firstCharacterIndex;
				int lastCharacterIndex = this.lineInfo[line].lastCharacterIndex;
				float num2 = float.PositiveInfinity;
				int num3 = lastCharacterIndex;
				for (int i = firstCharacterIndex; i <= lastCharacterIndex; i++)
				{
					TextElementInfo textElementInfo = this.textElementInfo[i];
					bool flag2 = visibleOnly && !textElementInfo.isVisible;
					if (!flag2)
					{
						bool flag3 = textElementInfo.character == 13U || textElementInfo.character == 10U;
						if (!flag3)
						{
							Vector3 bottomLeft = textElementInfo.bottomLeft;
							Vector3 vector = new Vector3(textElementInfo.bottomLeft.x, textElementInfo.topRight.y, 0f);
							Vector3 topRight = textElementInfo.topRight;
							Vector3 vector2 = new Vector3(textElementInfo.topRight.x, textElementInfo.bottomLeft.y, 0f);
							bool flag4 = TextInfo.PointIntersectRectangle(position, bottomLeft, vector, topRight, vector2);
							if (flag4)
							{
								num3 = i;
								break;
							}
							float num4 = TextInfo.DistanceToLine(bottomLeft, vector, position);
							float num5 = TextInfo.DistanceToLine(vector, topRight, position);
							float num6 = TextInfo.DistanceToLine(topRight, vector2, position);
							float num7 = TextInfo.DistanceToLine(vector2, bottomLeft, position);
							float num8 = ((num4 < num5) ? num4 : num5);
							num8 = ((num8 < num6) ? num8 : num6);
							num8 = ((num8 < num7) ? num8 : num7);
							bool flag5 = num2 > num8;
							if (flag5)
							{
								num2 = num8;
								num3 = i;
							}
						}
					}
				}
				num = num3;
			}
			return num;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00024C0C File Offset: 0x00022E0C
		public int FindIntersectingLink(Vector3 position, Rect screenRect, bool inverseYAxis = true)
		{
			if (inverseYAxis)
			{
				position.y = screenRect.height - position.y;
			}
			for (int i = 0; i < this.linkCount; i++)
			{
				LinkInfo linkInfo = this.linkInfo[i];
				bool flag = false;
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				Vector3 zero3 = Vector3.zero;
				Vector3 zero4 = Vector3.zero;
				for (int j = 0; j < linkInfo.linkTextLength; j++)
				{
					int num = linkInfo.linkTextfirstCharacterIndex + j;
					TextElementInfo textElementInfo = this.textElementInfo[num];
					int lineNumber = textElementInfo.lineNumber;
					bool flag2 = !flag;
					if (flag2)
					{
						flag = true;
						zero = new Vector3(textElementInfo.bottomLeft.x, textElementInfo.descender, 0f);
						zero2 = new Vector3(textElementInfo.bottomLeft.x, textElementInfo.ascender, 0f);
						bool flag3 = linkInfo.linkTextLength == 1;
						if (flag3)
						{
							flag = false;
							zero3 = new Vector3(textElementInfo.topRight.x, textElementInfo.descender, 0f);
							zero4 = new Vector3(textElementInfo.topRight.x, textElementInfo.ascender, 0f);
							bool flag4 = TextInfo.PointIntersectRectangle(position, zero, zero2, zero4, zero3);
							if (flag4)
							{
								return i;
							}
						}
					}
					bool flag5 = flag && j == linkInfo.linkTextLength - 1;
					if (flag5)
					{
						flag = false;
						zero3 = new Vector3(textElementInfo.topRight.x, textElementInfo.descender, 0f);
						zero4 = new Vector3(textElementInfo.topRight.x, textElementInfo.ascender, 0f);
						bool flag6 = TextInfo.PointIntersectRectangle(position, zero, zero2, zero4, zero3);
						if (flag6)
						{
							return i;
						}
					}
					else
					{
						bool flag7 = flag && lineNumber != this.textElementInfo[num + 1].lineNumber;
						if (flag7)
						{
							flag = false;
							zero3 = new Vector3(textElementInfo.topRight.x, textElementInfo.descender, 0f);
							zero4 = new Vector3(textElementInfo.topRight.x, textElementInfo.ascender, 0f);
							bool flag8 = TextInfo.PointIntersectRectangle(position, zero, zero2, zero4, zero3);
							if (flag8)
							{
								return i;
							}
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00024E84 File Offset: 0x00023084
		public int GetCorrespondingStringIndex(int index)
		{
			bool flag = index <= 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = this.textElementInfo[index - 1].index + this.textElementInfo[index - 1].stringLength;
			}
			return num;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00024ECC File Offset: 0x000230CC
		public int GetCorrespondingCodePointIndex(int stringIndex)
		{
			bool flag = stringIndex <= 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				for (int i = 0; i < this.characterCount; i++)
				{
					TextElementInfo textElementInfo = this.textElementInfo[i];
					bool flag2 = textElementInfo.index + textElementInfo.stringLength >= stringIndex;
					if (flag2)
					{
						return i + 1;
					}
				}
				num = this.characterCount;
			}
			return num;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00024F38 File Offset: 0x00023138
		public LineInfo GetLineInfoFromCharacterIndex(int index)
		{
			return this.lineInfo[this.GetLineNumber(index)];
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00024F5C File Offset: 0x0002315C
		private static bool PointIntersectRectangle(Vector3 m, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			Vector3 vector = Vector3.Cross(b - a, d - a);
			bool flag = vector == Vector3.zero;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Vector3 vector2 = b - a;
				Vector3 vector3 = m - a;
				Vector3 vector4 = c - b;
				Vector3 vector5 = m - b;
				float num = Vector3.Dot(vector2, vector3);
				float num2 = Vector3.Dot(vector4, vector5);
				flag2 = 0f <= num && num <= Vector3.Dot(vector2, vector2) && 0f <= num2 && num2 <= Vector3.Dot(vector4, vector4);
			}
			return flag2;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00025000 File Offset: 0x00023200
		private static float DistanceToLine(Vector3 a, Vector3 b, Vector3 point)
		{
			bool flag = a == b;
			float num;
			if (flag)
			{
				Vector3 vector = point - a;
				num = Vector3.Dot(vector, vector);
			}
			else
			{
				Vector3 vector2 = b - a;
				Vector3 vector3 = a - point;
				float num2 = Vector3.Dot(vector2, vector3);
				bool flag2 = num2 > 0f;
				if (flag2)
				{
					num = Vector3.Dot(vector3, vector3);
				}
				else
				{
					Vector3 vector4 = point - b;
					bool flag3 = Vector3.Dot(vector2, vector4) > 0f;
					if (flag3)
					{
						num = Vector3.Dot(vector4, vector4);
					}
					else
					{
						Vector3 vector5 = vector3 - vector2 * (num2 / Vector3.Dot(vector2, vector2));
						num = Vector3.Dot(vector5, vector5);
					}
				}
			}
			return num;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000250B0 File Offset: 0x000232B0
		public int GetLineNumber(int index)
		{
			bool flag = index <= 0;
			if (flag)
			{
				index = 0;
			}
			bool flag2 = index >= this.characterCount;
			if (flag2)
			{
				index = Mathf.Max(0, this.characterCount - 1);
			}
			return this.textElementInfo[index].lineNumber;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00025104 File Offset: 0x00023304
		public float GetLineHeight(int lineNumber)
		{
			bool flag = lineNumber <= 0;
			if (flag)
			{
				lineNumber = 0;
			}
			bool flag2 = lineNumber >= this.lineCount;
			if (flag2)
			{
				lineNumber = Mathf.Max(0, this.lineCount - 1);
			}
			return this.lineInfo[lineNumber].lineHeight;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00025158 File Offset: 0x00023358
		public float GetLineHeightFromCharacterIndex(int index)
		{
			bool flag = index <= 0;
			if (flag)
			{
				index = 0;
			}
			bool flag2 = index >= this.characterCount;
			if (flag2)
			{
				index = Mathf.Max(0, this.characterCount - 1);
			}
			return this.GetLineHeight(this.textElementInfo[index].lineNumber);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000251B0 File Offset: 0x000233B0
		public float GetCharacterHeightFromIndex(int index)
		{
			bool flag = index <= 0;
			if (flag)
			{
				index = 0;
			}
			bool flag2 = index >= this.characterCount;
			if (flag2)
			{
				index = Mathf.Max(0, this.characterCount - 1);
			}
			TextElementInfo textElementInfo = this.textElementInfo[index];
			return textElementInfo.ascender - textElementInfo.descender;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0002520C File Offset: 0x0002340C
		public string Substring(int startIndex, int length)
		{
			bool flag = startIndex < 0 || startIndex + length > this.characterCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = startIndex; i < startIndex + length; i++)
			{
				uint character = this.textElementInfo[i].character;
				bool flag2 = character >= 65536U && character <= 1114111U;
				if (flag2)
				{
					uint num = 55296U + (character - 65536U >> 10);
					uint num2 = 56320U + ((character - 65536U) & 1023U);
					stringBuilder.Append((char)num);
					stringBuilder.Append((char)num2);
				}
				else
				{
					stringBuilder.Append((char)character);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000252DC File Offset: 0x000234DC
		public int IndexOf(char value, int startIndex)
		{
			bool flag = startIndex < 0 || startIndex >= this.characterCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = startIndex; i < this.characterCount; i++)
			{
				bool flag2 = this.textElementInfo[i].character == (uint)value;
				if (flag2)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00025344 File Offset: 0x00023544
		public int LastIndexOf(char value, int startIndex)
		{
			bool flag = startIndex < 0 || startIndex >= this.characterCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = startIndex; i >= 0; i--)
			{
				bool flag2 = this.textElementInfo[i].character == (uint)value;
				if (flag2)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0400037F RID: 895
		private static Vector2 s_InfinityVectorPositive = new Vector2(32767f, 32767f);

		// Token: 0x04000380 RID: 896
		private static Vector2 s_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		// Token: 0x04000381 RID: 897
		public int characterCount;

		// Token: 0x04000382 RID: 898
		public int spriteCount;

		// Token: 0x04000383 RID: 899
		public int spaceCount;

		// Token: 0x04000384 RID: 900
		public int wordCount;

		// Token: 0x04000385 RID: 901
		public int linkCount;

		// Token: 0x04000386 RID: 902
		public int lineCount;

		// Token: 0x04000387 RID: 903
		public int materialCount;

		// Token: 0x04000388 RID: 904
		public TextElementInfo[] textElementInfo;

		// Token: 0x04000389 RID: 905
		public WordInfo[] wordInfo;

		// Token: 0x0400038A RID: 906
		public LinkInfo[] linkInfo;

		// Token: 0x0400038B RID: 907
		public LineInfo[] lineInfo;

		// Token: 0x0400038C RID: 908
		public MeshInfo[] meshInfo;

		// Token: 0x0400038D RID: 909
		public double lastTimeInCache;

		// Token: 0x0400038E RID: 910
		public Action removedFromCache;

		// Token: 0x0400038F RID: 911
		public bool hasMultipleColors = false;
	}
}
