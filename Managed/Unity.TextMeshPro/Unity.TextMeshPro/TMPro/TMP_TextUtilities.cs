using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200006D RID: 109
	public static class TMP_TextUtilities
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x0002BC3C File Offset: 0x00029E3C
		public static int GetCursorIndexFromPosition(TMP_Text textComponent, Vector3 position, Camera camera)
		{
			int num = TMP_TextUtilities.FindNearestCharacter(textComponent, position, camera, false);
			RectTransform rectTransform = textComponent.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			TMP_CharacterInfo tmp_CharacterInfo = textComponent.textInfo.characterInfo[num];
			Vector3 vector = rectTransform.TransformPoint(tmp_CharacterInfo.bottomLeft);
			Vector3 vector2 = rectTransform.TransformPoint(tmp_CharacterInfo.topRight);
			if ((position.x - vector.x) / (vector2.x - vector.x) < 0.5f)
			{
				return num;
			}
			return num + 1;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002BCBC File Offset: 0x00029EBC
		public static int GetCursorIndexFromPosition(TMP_Text textComponent, Vector3 position, Camera camera, out CaretPosition cursor)
		{
			int num = TMP_TextUtilities.FindNearestLine(textComponent, position, camera);
			if (num == -1)
			{
				cursor = CaretPosition.Left;
				return 0;
			}
			int num2 = TMP_TextUtilities.FindNearestCharacterOnLine(textComponent, position, num, camera, false);
			if (textComponent.textInfo.lineInfo[num].characterCount == 1)
			{
				cursor = CaretPosition.Left;
				return num2;
			}
			RectTransform rectTransform = textComponent.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			TMP_CharacterInfo tmp_CharacterInfo = textComponent.textInfo.characterInfo[num2];
			Vector3 vector = rectTransform.TransformPoint(tmp_CharacterInfo.bottomLeft);
			Vector3 vector2 = rectTransform.TransformPoint(tmp_CharacterInfo.topRight);
			if ((position.x - vector.x) / (vector2.x - vector.x) < 0.5f)
			{
				cursor = CaretPosition.Left;
				return num2;
			}
			cursor = CaretPosition.Right;
			return num2;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0002BD74 File Offset: 0x00029F74
		public static int FindNearestLine(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			float num = float.PositiveInfinity;
			int num2 = -1;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.lineCount; i++)
			{
				TMP_LineInfo tmp_LineInfo = text.textInfo.lineInfo[i];
				float y = rectTransform.TransformPoint(new Vector3(0f, tmp_LineInfo.ascender, 0f)).y;
				float y2 = rectTransform.TransformPoint(new Vector3(0f, tmp_LineInfo.descender, 0f)).y;
				if (y > position.y && y2 < position.y)
				{
					return i;
				}
				float num3 = Mathf.Abs(y - position.y);
				float num4 = Mathf.Abs(y2 - position.y);
				float num5 = Mathf.Min(num3, num4);
				if (num5 < num)
				{
					num = num5;
					num2 = i;
				}
			}
			return num2;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0002BE60 File Offset: 0x0002A060
		public static int FindNearestCharacterOnLine(TMP_Text text, Vector3 position, int line, Camera camera, bool visibleOnly)
		{
			RectTransform rectTransform = text.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			int firstCharacterIndex = text.textInfo.lineInfo[line].firstCharacterIndex;
			int lastCharacterIndex = text.textInfo.lineInfo[line].lastCharacterIndex;
			float num = float.PositiveInfinity;
			int num2 = lastCharacterIndex;
			for (int i = firstCharacterIndex; i < lastCharacterIndex; i++)
			{
				TMP_CharacterInfo tmp_CharacterInfo = text.textInfo.characterInfo[i];
				if ((!visibleOnly || tmp_CharacterInfo.isVisible) && tmp_CharacterInfo.character != '\r')
				{
					Vector3 vector = rectTransform.TransformPoint(tmp_CharacterInfo.bottomLeft);
					Vector3 vector2 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.topRight.y, 0f));
					Vector3 vector3 = rectTransform.TransformPoint(tmp_CharacterInfo.topRight);
					Vector3 vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.bottomLeft.y, 0f));
					if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector3, vector4))
					{
						num2 = i;
						break;
					}
					float num3 = TMP_TextUtilities.DistanceToLine(vector, vector2, position);
					float num4 = TMP_TextUtilities.DistanceToLine(vector2, vector3, position);
					float num5 = TMP_TextUtilities.DistanceToLine(vector3, vector4, position);
					float num6 = TMP_TextUtilities.DistanceToLine(vector4, vector, position);
					float num7 = ((num3 < num4) ? num3 : num4);
					num7 = ((num7 < num5) ? num7 : num5);
					num7 = ((num7 < num6) ? num7 : num6);
					if (num > num7)
					{
						num = num7;
						num2 = i;
					}
				}
			}
			return num2;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002BFF0 File Offset: 0x0002A1F0
		public static bool IsIntersectingRectTransform(RectTransform rectTransform, Vector3 position, Camera camera)
		{
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			rectTransform.GetWorldCorners(TMP_TextUtilities.m_rectWorldCorners);
			return TMP_TextUtilities.PointIntersectRectangle(position, TMP_TextUtilities.m_rectWorldCorners[0], TMP_TextUtilities.m_rectWorldCorners[1], TMP_TextUtilities.m_rectWorldCorners[2], TMP_TextUtilities.m_rectWorldCorners[3]);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002C050 File Offset: 0x0002A250
		public static int FindIntersectingCharacter(TMP_Text text, Vector3 position, Camera camera, bool visibleOnly)
		{
			RectTransform rectTransform = text.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.characterCount; i++)
			{
				TMP_CharacterInfo tmp_CharacterInfo = text.textInfo.characterInfo[i];
				if (!visibleOnly || tmp_CharacterInfo.isVisible)
				{
					Vector3 vector = rectTransform.TransformPoint(tmp_CharacterInfo.bottomLeft);
					Vector3 vector2 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.topRight.y, 0f));
					Vector3 vector3 = rectTransform.TransformPoint(tmp_CharacterInfo.topRight);
					Vector3 vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.bottomLeft.y, 0f));
					if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector3, vector4))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002C12C File Offset: 0x0002A32C
		public static int FindNearestCharacter(TMP_Text text, Vector3 position, Camera camera, bool visibleOnly)
		{
			RectTransform rectTransform = text.rectTransform;
			float num = float.PositiveInfinity;
			int num2 = 0;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.characterCount; i++)
			{
				TMP_CharacterInfo tmp_CharacterInfo = text.textInfo.characterInfo[i];
				if (!visibleOnly || tmp_CharacterInfo.isVisible)
				{
					Vector3 vector = rectTransform.TransformPoint(tmp_CharacterInfo.bottomLeft);
					Vector3 vector2 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.topRight.y, 0f));
					Vector3 vector3 = rectTransform.TransformPoint(tmp_CharacterInfo.topRight);
					Vector3 vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.bottomLeft.y, 0f));
					if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector3, vector4))
					{
						return i;
					}
					float num3 = TMP_TextUtilities.DistanceToLine(vector, vector2, position);
					float num4 = TMP_TextUtilities.DistanceToLine(vector2, vector3, position);
					float num5 = TMP_TextUtilities.DistanceToLine(vector3, vector4, position);
					float num6 = TMP_TextUtilities.DistanceToLine(vector4, vector, position);
					float num7 = ((num3 < num4) ? num3 : num4);
					num7 = ((num7 < num5) ? num7 : num5);
					num7 = ((num7 < num6) ? num7 : num6);
					if (num > num7)
					{
						num = num7;
						num2 = i;
					}
				}
			}
			return num2;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002C280 File Offset: 0x0002A480
		public static int FindIntersectingWord(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.wordCount; i++)
			{
				TMP_WordInfo tmp_WordInfo = text.textInfo.wordInfo[i];
				bool flag = false;
				Vector3 vector = Vector3.zero;
				Vector3 vector2 = Vector3.zero;
				Vector3 vector3 = Vector3.zero;
				Vector3 vector4 = Vector3.zero;
				float num = float.NegativeInfinity;
				float num2 = float.PositiveInfinity;
				for (int j = 0; j < tmp_WordInfo.characterCount; j++)
				{
					int num3 = tmp_WordInfo.firstCharacterIndex + j;
					TMP_CharacterInfo tmp_CharacterInfo = text.textInfo.characterInfo[num3];
					int lineNumber = tmp_CharacterInfo.lineNumber;
					bool isVisible = tmp_CharacterInfo.isVisible;
					num = Mathf.Max(num, tmp_CharacterInfo.ascender);
					num2 = Mathf.Min(num2, tmp_CharacterInfo.descender);
					if (!flag && isVisible)
					{
						flag = true;
						vector = new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.descender, 0f);
						vector2 = new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.ascender, 0f);
						if (tmp_WordInfo.characterCount == 1)
						{
							flag = false;
							vector3 = new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f);
							vector4 = new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f);
							vector = rectTransform.TransformPoint(new Vector3(vector.x, num2, 0f));
							vector2 = rectTransform.TransformPoint(new Vector3(vector2.x, num, 0f));
							vector4 = rectTransform.TransformPoint(new Vector3(vector4.x, num, 0f));
							vector3 = rectTransform.TransformPoint(new Vector3(vector3.x, num2, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
							{
								return i;
							}
						}
					}
					if (flag && j == tmp_WordInfo.characterCount - 1)
					{
						flag = false;
						vector3 = new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f);
						vector4 = new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f);
						vector = rectTransform.TransformPoint(new Vector3(vector.x, num2, 0f));
						vector2 = rectTransform.TransformPoint(new Vector3(vector2.x, num, 0f));
						vector4 = rectTransform.TransformPoint(new Vector3(vector4.x, num, 0f));
						vector3 = rectTransform.TransformPoint(new Vector3(vector3.x, num2, 0f));
						if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
						{
							return i;
						}
					}
					else if (flag && lineNumber != text.textInfo.characterInfo[num3 + 1].lineNumber)
					{
						flag = false;
						vector3 = new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f);
						vector4 = new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f);
						vector = rectTransform.TransformPoint(new Vector3(vector.x, num2, 0f));
						vector2 = rectTransform.TransformPoint(new Vector3(vector2.x, num, 0f));
						vector4 = rectTransform.TransformPoint(new Vector3(vector4.x, num, 0f));
						vector3 = rectTransform.TransformPoint(new Vector3(vector3.x, num2, 0f));
						num = float.NegativeInfinity;
						num2 = float.PositiveInfinity;
						if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002C64C File Offset: 0x0002A84C
		public static int FindNearestWord(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			float num = float.PositiveInfinity;
			int num2 = 0;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.wordCount; i++)
			{
				TMP_WordInfo tmp_WordInfo = text.textInfo.wordInfo[i];
				bool flag = false;
				Vector3 vector = Vector3.zero;
				Vector3 vector2 = Vector3.zero;
				Vector3 vector3 = Vector3.zero;
				Vector3 vector4 = Vector3.zero;
				for (int j = 0; j < tmp_WordInfo.characterCount; j++)
				{
					int num3 = tmp_WordInfo.firstCharacterIndex + j;
					TMP_CharacterInfo tmp_CharacterInfo = text.textInfo.characterInfo[num3];
					int lineNumber = tmp_CharacterInfo.lineNumber;
					bool isVisible = tmp_CharacterInfo.isVisible;
					if (!flag && isVisible)
					{
						flag = true;
						vector = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.descender, 0f));
						vector2 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.ascender, 0f));
						if (tmp_WordInfo.characterCount == 1)
						{
							flag = false;
							vector3 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
							vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
							{
								return i;
							}
							float num4 = TMP_TextUtilities.DistanceToLine(vector, vector2, position);
							float num5 = TMP_TextUtilities.DistanceToLine(vector2, vector4, position);
							float num6 = TMP_TextUtilities.DistanceToLine(vector4, vector3, position);
							float num7 = TMP_TextUtilities.DistanceToLine(vector3, vector, position);
							float num8 = ((num4 < num5) ? num4 : num5);
							num8 = ((num8 < num6) ? num8 : num6);
							num8 = ((num8 < num7) ? num8 : num7);
							if (num > num8)
							{
								num = num8;
								num2 = i;
							}
						}
					}
					if (flag && j == tmp_WordInfo.characterCount - 1)
					{
						flag = false;
						vector3 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
						vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
						if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
						{
							return i;
						}
						float num9 = TMP_TextUtilities.DistanceToLine(vector, vector2, position);
						float num10 = TMP_TextUtilities.DistanceToLine(vector2, vector4, position);
						float num11 = TMP_TextUtilities.DistanceToLine(vector4, vector3, position);
						float num12 = TMP_TextUtilities.DistanceToLine(vector3, vector, position);
						float num13 = ((num9 < num10) ? num9 : num10);
						num13 = ((num13 < num11) ? num13 : num11);
						num13 = ((num13 < num12) ? num13 : num12);
						if (num > num13)
						{
							num = num13;
							num2 = i;
						}
					}
					else if (flag && lineNumber != text.textInfo.characterInfo[num3 + 1].lineNumber)
					{
						flag = false;
						vector3 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
						vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
						if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
						{
							return i;
						}
						float num14 = TMP_TextUtilities.DistanceToLine(vector, vector2, position);
						float num15 = TMP_TextUtilities.DistanceToLine(vector2, vector4, position);
						float num16 = TMP_TextUtilities.DistanceToLine(vector4, vector3, position);
						float num17 = TMP_TextUtilities.DistanceToLine(vector3, vector, position);
						float num18 = ((num14 < num15) ? num14 : num15);
						num18 = ((num18 < num16) ? num18 : num16);
						num18 = ((num18 < num17) ? num18 : num17);
						if (num > num18)
						{
							num = num18;
							num2 = i;
						}
					}
				}
			}
			return num2;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0002CA0C File Offset: 0x0002AC0C
		public static int FindIntersectingLine(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			int num = -1;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			for (int i = 0; i < text.textInfo.lineCount; i++)
			{
				TMP_LineInfo tmp_LineInfo = text.textInfo.lineInfo[i];
				float y = rectTransform.TransformPoint(new Vector3(0f, tmp_LineInfo.ascender, 0f)).y;
				float y2 = rectTransform.TransformPoint(new Vector3(0f, tmp_LineInfo.descender, 0f)).y;
				if (y > position.y && y2 < position.y)
				{
					return i;
				}
			}
			return num;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0002CAB8 File Offset: 0x0002ACB8
		public static int FindIntersectingLink(TMP_Text text, Vector3 position, Camera camera)
		{
			Transform transform = text.transform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(transform, position, camera, out position);
			for (int i = 0; i < text.textInfo.linkCount; i++)
			{
				TMP_LinkInfo tmp_LinkInfo = text.textInfo.linkInfo[i];
				bool flag = false;
				Vector3 vector = Vector3.zero;
				Vector3 vector2 = Vector3.zero;
				Vector3 vector3 = Vector3.zero;
				Vector3 vector4 = Vector3.zero;
				for (int j = 0; j < tmp_LinkInfo.linkTextLength; j++)
				{
					int num = tmp_LinkInfo.linkTextfirstCharacterIndex + j;
					TMP_CharacterInfo tmp_CharacterInfo = text.textInfo.characterInfo[num];
					int lineNumber = tmp_CharacterInfo.lineNumber;
					if (text.overflowMode != TextOverflowModes.Page || tmp_CharacterInfo.pageNumber + 1 == text.pageToDisplay)
					{
						if (!flag)
						{
							flag = true;
							vector = transform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.descender, 0f));
							vector2 = transform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.ascender, 0f));
							if (tmp_LinkInfo.linkTextLength == 1)
							{
								flag = false;
								vector3 = transform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
								vector4 = transform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
								if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
								{
									return i;
								}
							}
						}
						if (flag && j == tmp_LinkInfo.linkTextLength - 1)
						{
							flag = false;
							vector3 = transform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
							vector4 = transform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
							{
								return i;
							}
						}
						else if (flag && lineNumber != text.textInfo.characterInfo[num + 1].lineNumber)
						{
							flag = false;
							vector3 = transform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
							vector4 = transform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
							{
								return i;
							}
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002CD30 File Offset: 0x0002AF30
		public static int FindNearestLink(TMP_Text text, Vector3 position, Camera camera)
		{
			RectTransform rectTransform = text.rectTransform;
			TMP_TextUtilities.ScreenPointToWorldPointInRectangle(rectTransform, position, camera, out position);
			float num = float.PositiveInfinity;
			int num2 = 0;
			for (int i = 0; i < text.textInfo.linkCount; i++)
			{
				TMP_LinkInfo tmp_LinkInfo = text.textInfo.linkInfo[i];
				bool flag = false;
				Vector3 vector = Vector3.zero;
				Vector3 vector2 = Vector3.zero;
				Vector3 vector3 = Vector3.zero;
				Vector3 vector4 = Vector3.zero;
				for (int j = 0; j < tmp_LinkInfo.linkTextLength; j++)
				{
					int num3 = tmp_LinkInfo.linkTextfirstCharacterIndex + j;
					TMP_CharacterInfo tmp_CharacterInfo = text.textInfo.characterInfo[num3];
					int lineNumber = tmp_CharacterInfo.lineNumber;
					if (text.overflowMode != TextOverflowModes.Page || tmp_CharacterInfo.pageNumber + 1 == text.pageToDisplay)
					{
						if (!flag)
						{
							flag = true;
							vector = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.descender, 0f));
							vector2 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.bottomLeft.x, tmp_CharacterInfo.ascender, 0f));
							if (tmp_LinkInfo.linkTextLength == 1)
							{
								flag = false;
								vector3 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
								vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
								if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
								{
									return i;
								}
								float num4 = TMP_TextUtilities.DistanceToLine(vector, vector2, position);
								float num5 = TMP_TextUtilities.DistanceToLine(vector2, vector4, position);
								float num6 = TMP_TextUtilities.DistanceToLine(vector4, vector3, position);
								float num7 = TMP_TextUtilities.DistanceToLine(vector3, vector, position);
								float num8 = ((num4 < num5) ? num4 : num5);
								num8 = ((num8 < num6) ? num8 : num6);
								num8 = ((num8 < num7) ? num8 : num7);
								if (num > num8)
								{
									num = num8;
									num2 = i;
								}
							}
						}
						if (flag && j == tmp_LinkInfo.linkTextLength - 1)
						{
							flag = false;
							vector3 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
							vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
							{
								return i;
							}
							float num9 = TMP_TextUtilities.DistanceToLine(vector, vector2, position);
							float num10 = TMP_TextUtilities.DistanceToLine(vector2, vector4, position);
							float num11 = TMP_TextUtilities.DistanceToLine(vector4, vector3, position);
							float num12 = TMP_TextUtilities.DistanceToLine(vector3, vector, position);
							float num13 = ((num9 < num10) ? num9 : num10);
							num13 = ((num13 < num11) ? num13 : num11);
							num13 = ((num13 < num12) ? num13 : num12);
							if (num > num13)
							{
								num = num13;
								num2 = i;
							}
						}
						else if (flag && lineNumber != text.textInfo.characterInfo[num3 + 1].lineNumber)
						{
							flag = false;
							vector3 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.descender, 0f));
							vector4 = rectTransform.TransformPoint(new Vector3(tmp_CharacterInfo.topRight.x, tmp_CharacterInfo.ascender, 0f));
							if (TMP_TextUtilities.PointIntersectRectangle(position, vector, vector2, vector4, vector3))
							{
								return i;
							}
							float num14 = TMP_TextUtilities.DistanceToLine(vector, vector2, position);
							float num15 = TMP_TextUtilities.DistanceToLine(vector2, vector4, position);
							float num16 = TMP_TextUtilities.DistanceToLine(vector4, vector3, position);
							float num17 = TMP_TextUtilities.DistanceToLine(vector3, vector, position);
							float num18 = ((num14 < num15) ? num14 : num15);
							num18 = ((num18 < num16) ? num18 : num16);
							num18 = ((num18 < num17) ? num18 : num17);
							if (num > num18)
							{
								num = num18;
								num2 = i;
							}
						}
					}
				}
			}
			return num2;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002D100 File Offset: 0x0002B300
		private static bool PointIntersectRectangle(Vector3 m, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = m - a;
			Vector3 vector3 = c - b;
			Vector3 vector4 = m - b;
			float num = Vector3.Dot(vector, vector2);
			float num2 = Vector3.Dot(vector3, vector4);
			return 0f <= num && num <= Vector3.Dot(vector, vector) && 0f <= num2 && num2 <= Vector3.Dot(vector3, vector3);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0002D16C File Offset: 0x0002B36C
		public static bool ScreenPointToWorldPointInRectangle(Transform transform, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			float num;
			if (!new Plane(transform.rotation * Vector3.back, transform.position).Raycast(ray, out num))
			{
				return false;
			}
			worldPoint = ray.GetPoint(num);
			return true;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		private static bool IntersectLinePlane(TMP_TextUtilities.LineSegment line, Vector3 point, Vector3 normal, out Vector3 intersectingPoint)
		{
			intersectingPoint = Vector3.zero;
			Vector3 vector = line.Point2 - line.Point1;
			Vector3 vector2 = line.Point1 - point;
			float num = Vector3.Dot(normal, vector);
			float num2 = -Vector3.Dot(normal, vector2);
			if (Mathf.Abs(num) < Mathf.Epsilon)
			{
				return num2 == 0f;
			}
			float num3 = num2 / num;
			if (num3 < 0f || num3 > 1f)
			{
				return false;
			}
			intersectingPoint = line.Point1 + num3 * vector;
			return true;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0002D260 File Offset: 0x0002B460
		public static float DistanceToLine(Vector3 a, Vector3 b, Vector3 point)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = a - point;
			float num = Vector3.Dot(vector, vector2);
			if (num > 0f)
			{
				return Vector3.Dot(vector2, vector2);
			}
			Vector3 vector3 = point - b;
			if (Vector3.Dot(vector, vector3) > 0f)
			{
				return Vector3.Dot(vector3, vector3);
			}
			Vector3 vector4 = vector2 - vector * (num / Vector3.Dot(vector, vector));
			return Vector3.Dot(vector4, vector4);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002D2CE File Offset: 0x0002B4CE
		public static char ToLowerFast(char c)
		{
			if ((int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-".Length - 1)
			{
				return c;
			}
			return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-"[(int)c];
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0002D2EC File Offset: 0x0002B4EC
		public static char ToUpperFast(char c)
		{
			if ((int)c > "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)
			{
				return c;
			}
			return "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002D30A File Offset: 0x0002B50A
		internal static uint ToUpperASCIIFast(uint c)
		{
			if ((ulong)c > (ulong)((long)("-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-".Length - 1)))
			{
				return c;
			}
			return (uint)"-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-"[(int)c];
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002D32C File Offset: 0x0002B52C
		public static int GetHashCode(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)TMP_TextUtilities.ToUpperFast(s[i]);
			}
			return num;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002D36C File Offset: 0x0002B56C
		public static int GetSimpleHashCode(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (int)s[i];
			}
			return num;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002D39C File Offset: 0x0002B59C
		public static uint GetSimpleHashCodeLowercase(string s)
		{
			uint num = 5381U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)TMP_TextUtilities.ToLowerFast(s[i]);
			}
			return num;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		public static uint GetHashCodeCaseInSensitive(string s)
		{
			uint num = 0U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)TMP_TextUtilities.ToUpperFast(s[i]);
			}
			return num;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002D408 File Offset: 0x0002B608
		public static int HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (hex)
				{
				case 'a':
					return 10;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				}
				break;
			}
			return 15;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002D4D8 File Offset: 0x0002B6D8
		public static int StringHexToInt(string s)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				num += TMP_TextUtilities.HexToInt(s[i]) * (int)Mathf.Pow(16f, (float)(s.Length - 1 - i));
			}
			return num;
		}

		// Token: 0x040004DC RID: 1244
		private static Vector3[] m_rectWorldCorners = new Vector3[4];

		// Token: 0x040004DD RID: 1245
		private const string k_lookupStringL = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@abcdefghijklmnopqrstuvwxyz[-]^_`abcdefghijklmnopqrstuvwxyz{|}~-";

		// Token: 0x040004DE RID: 1246
		private const string k_lookupStringU = "-------------------------------- !-#$%&-()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[-]^_`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~-";

		// Token: 0x020000BB RID: 187
		private struct LineSegment
		{
			// Token: 0x0600070A RID: 1802 RVA: 0x000446E7 File Offset: 0x000428E7
			public LineSegment(Vector3 p1, Vector3 p2)
			{
				this.Point1 = p1;
				this.Point2 = p2;
			}

			// Token: 0x0400069A RID: 1690
			public Vector3 Point1;

			// Token: 0x0400069B RID: 1691
			public Vector3 Point2;
		}
	}
}
