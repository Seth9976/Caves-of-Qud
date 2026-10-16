using System;
using System.Collections.Generic;
using UnityEngine.UIElements.Layout;

namespace UnityEngine.UIElements
{
	// Token: 0x02000477 RID: 1143
	internal static class StyleValueExtensions
	{
		// Token: 0x06002401 RID: 9217 RVA: 0x0008428C File Offset: 0x0008248C
		internal static string DebugString<T>(this IStyleValue<T> styleValue)
		{
			return (styleValue.keyword != StyleKeyword.Undefined) ? string.Format("{0}", styleValue.keyword) : string.Format("{0}", styleValue.value);
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000842D4 File Offset: 0x000824D4
		internal static LayoutValue ToLayoutValue(this Length length)
		{
			bool flag = length.IsAuto();
			LayoutValue layoutValue;
			if (flag)
			{
				layoutValue = LayoutValue.Auto();
			}
			else
			{
				bool flag2 = length.IsNone();
				if (flag2)
				{
					layoutValue = float.NaN;
				}
				else
				{
					LengthUnit unit = length.unit;
					LengthUnit lengthUnit = unit;
					if (lengthUnit != LengthUnit.Pixel)
					{
						if (lengthUnit != LengthUnit.Percent)
						{
							Debug.LogAssertion(string.Format("Unexpected unit '{0}'", length.unit));
							layoutValue = float.NaN;
						}
						else
						{
							layoutValue = LayoutValue.Percent(length.value);
						}
					}
					else
					{
						layoutValue = LayoutValue.Point(length.value);
					}
				}
			}
			return layoutValue;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0008436C File Offset: 0x0008256C
		internal static Length ToLength(this StyleKeyword keyword)
		{
			StyleKeyword styleKeyword = keyword;
			StyleKeyword styleKeyword2 = styleKeyword;
			Length length;
			if (styleKeyword2 != StyleKeyword.Auto)
			{
				if (styleKeyword2 != StyleKeyword.None)
				{
					Debug.LogAssertion("Unexpected StyleKeyword '" + keyword.ToString() + "'");
					length = default(Length);
				}
				else
				{
					length = Length.None();
				}
			}
			else
			{
				length = Length.Auto();
			}
			return length;
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000843CC File Offset: 0x000825CC
		internal static Rotate ToRotate(this StyleKeyword keyword)
		{
			StyleKeyword styleKeyword = keyword;
			StyleKeyword styleKeyword2 = styleKeyword;
			Rotate rotate;
			if (styleKeyword2 != StyleKeyword.None)
			{
				Debug.LogAssertion("Unexpected StyleKeyword '" + keyword.ToString() + "'");
				rotate = default(Rotate);
			}
			else
			{
				rotate = Rotate.None();
			}
			return rotate;
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0008441C File Offset: 0x0008261C
		internal static Scale ToScale(this StyleKeyword keyword)
		{
			StyleKeyword styleKeyword = keyword;
			StyleKeyword styleKeyword2 = styleKeyword;
			Scale scale;
			if (styleKeyword2 != StyleKeyword.None)
			{
				Debug.LogAssertion("Unexpected StyleKeyword '" + keyword.ToString() + "'");
				scale = default(Scale);
			}
			else
			{
				scale = Scale.None();
			}
			return scale;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0008446C File Offset: 0x0008266C
		internal static Translate ToTranslate(this StyleKeyword keyword)
		{
			StyleKeyword styleKeyword = keyword;
			StyleKeyword styleKeyword2 = styleKeyword;
			Translate translate;
			if (styleKeyword2 != StyleKeyword.None)
			{
				Debug.LogAssertion("Unexpected StyleKeyword '" + keyword.ToString() + "'");
				translate = default(Translate);
			}
			else
			{
				translate = Translate.None();
			}
			return translate;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000844BC File Offset: 0x000826BC
		internal static Length ToLength(this StyleLength styleLength)
		{
			StyleKeyword keyword = styleLength.keyword;
			StyleKeyword styleKeyword = keyword;
			Length length;
			if (styleKeyword - StyleKeyword.Auto > 1)
			{
				length = styleLength.value;
			}
			else
			{
				length = styleLength.keyword.ToLength();
			}
			return length;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000844F6 File Offset: 0x000826F6
		internal static void CopyFrom<T>(this List<T> list, List<T> other)
		{
			list.Clear();
			list.AddRange(other);
		}
	}
}
