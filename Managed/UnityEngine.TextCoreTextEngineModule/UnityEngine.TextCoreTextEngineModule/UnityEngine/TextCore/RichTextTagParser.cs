using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;

namespace UnityEngine.TextCore
{
	// Token: 0x0200000D RID: 13
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static class RichTextTagParser
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002748 File Offset: 0x00000948
		private unsafe static bool tagMatch(ReadOnlySpan<char> tagCandidate, [global::System.Runtime.CompilerServices.Nullable(1)] string tagName)
		{
			return tagCandidate.StartsWith<char>(tagName.AsSpan()) && (tagCandidate.Length == tagName.Length || (!char.IsLetter((char)(*tagCandidate[tagName.Length])) && *tagCandidate[tagName.Length] != 45));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000027AC File Offset: 0x000009AC
		private unsafe static bool SpanToEnum(ReadOnlySpan<char> tagCandidate, out RichTextTagParser.TagType tagType, [global::System.Runtime.CompilerServices.Nullable(2)] out string error, out ReadOnlySpan<char> attribute)
		{
			for (int i = 0; i < RichTextTagParser.TagsInfo.Length; i++)
			{
				string name = RichTextTagParser.TagsInfo[i].name;
				bool flag = RichTextTagParser.tagMatch(tagCandidate, name);
				if (flag)
				{
					tagType = RichTextTagParser.TagsInfo[i].TagType;
					error = null;
					attribute = tagCandidate.Slice(name.Length);
					return true;
				}
			}
			bool flag2 = tagCandidate.Length > 4 && *tagCandidate[0] == 35;
			if (flag2)
			{
				tagType = RichTextTagParser.TagType.Color;
				error = null;
				attribute = tagCandidate;
				return true;
			}
			error = "Unknown tag: " + tagCandidate.ToString();
			tagType = RichTextTagParser.TagType.Unknown;
			attribute = null;
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002878 File Offset: 0x00000A78
		[NullableContext(1)]
		internal unsafe static List<RichTextTagParser.Tag> FindTags(string inputStr, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] List<RichTextTagParser.ParseError> errors = null)
		{
			char[] array = inputStr.ToCharArray();
			List<RichTextTagParser.Tag> list = new List<RichTextTagParser.Tag>();
			int num = 0;
			for (;;)
			{
				int num2 = Array.IndexOf<char>(array, '<', num);
				bool flag = num2 == -1;
				if (flag)
				{
					break;
				}
				int num3 = Array.IndexOf<char>(array, '>', num2);
				bool flag2 = num3 == -1;
				if (flag2)
				{
					break;
				}
				bool flag3 = array.Length > num2 + 1 && array[num2 + 1] == '/';
				bool flag4 = num3 == num2 + 1;
				if (flag4)
				{
					if (errors != null)
					{
						errors.Add(new RichTextTagParser.ParseError("Empty tag", num2));
					}
					num = num3 + 1;
				}
				else
				{
					num = num3 + 1;
					bool flag5 = !flag3;
					if (flag5)
					{
						Span<char> span = array.AsSpan<char>(num2 + 1, num3 - num2 - 1);
						RichTextTagParser.TagType tagType;
						string text;
						ReadOnlySpan<char> readOnlySpan;
						bool flag6 = RichTextTagParser.SpanToEnum(span, out tagType, out text, out readOnlySpan);
						if (flag6)
						{
							RichTextTagParser.TagValue tagValue = null;
							bool flag7 = tagType == RichTextTagParser.TagType.Color;
							if (flag7)
							{
								bool flag8 = readOnlySpan.Length >= 2 && *readOnlySpan[0] == 61;
								if (flag8)
								{
									readOnlySpan = readOnlySpan.Slice(1);
								}
								bool flag9 = readOnlySpan.Length >= 4 && *readOnlySpan[0] == 34 && *readOnlySpan[readOnlySpan.Length - 1] == 34;
								if (flag9)
								{
									ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Slice(1, readOnlySpan.Length - 2);
									Color color;
									ColorUtility.TryParseHtmlString(readOnlySpan2.ToString(), out color);
									tagValue = new RichTextTagParser.TagValue(color);
								}
								else
								{
									Color color2;
									ColorUtility.TryParseHtmlString(readOnlySpan.ToString(), out color2);
									tagValue = new RichTextTagParser.TagValue(color2);
								}
								bool flag10 = tagValue == null;
								if (flag10)
								{
									if (errors != null)
									{
										errors.Add(new RichTextTagParser.ParseError("Invalid color value", num2));
									}
									num = num2 + 1;
									continue;
								}
							}
							bool flag11 = tagType == RichTextTagParser.TagType.Link || tagType == RichTextTagParser.TagType.Hyperlink;
							if (flag11)
							{
								bool flag12 = tagType == RichTextTagParser.TagType.Hyperlink && readOnlySpan.StartsWith<char>(" href=");
								if (flag12)
								{
									readOnlySpan = readOnlySpan.Slice(" href=".Length);
								}
								bool flag13 = readOnlySpan.Length >= 1 && *readOnlySpan[0] == 61;
								if (flag13)
								{
									readOnlySpan = readOnlySpan.Slice(1);
								}
								bool flag14 = readOnlySpan.Length >= 2 && *readOnlySpan[0] == 34 && *readOnlySpan[readOnlySpan.Length - 1] == 34;
								if (flag14)
								{
									ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Slice(1, readOnlySpan.Length - 2);
									tagValue = new RichTextTagParser.TagValue(readOnlySpan2.ToString());
								}
								else
								{
									tagValue = new RichTextTagParser.TagValue(readOnlySpan.ToString());
								}
							}
							list.Add(new RichTextTagParser.Tag
							{
								tagType = tagType,
								start = num2,
								end = num3,
								isClosing = flag3,
								value = tagValue
							});
							bool flag15 = tagType == RichTextTagParser.TagType.NoParse;
							if (flag15)
							{
								bool flag16 = (num2 = array.AsSpan<char>(num).IndexOf<char>("</noparse>")) == -1;
								if (flag16)
								{
									break;
								}
								num2 += num;
								num3 = num2 + "</noparse>".Length;
								list.Add(new RichTextTagParser.Tag
								{
									tagType = RichTextTagParser.TagType.NoParse,
									start = num2,
									end = num3,
									isClosing = true
								});
								num = num3 + 1;
							}
						}
						else
						{
							bool flag17 = text != null;
							if (flag17)
							{
								if (errors != null)
								{
									errors.Add(new RichTextTagParser.ParseError(text, num2));
								}
							}
							num = num2 + 1;
						}
					}
					else
					{
						ReadOnlySpan<char> readOnlySpan2;
						RichTextTagParser.TagType tagType2;
						string text2;
						bool flag18 = RichTextTagParser.SpanToEnum(array.AsSpan<char>(num2 + 2, num3 - num2 - 2), out tagType2, out text2, out readOnlySpan2);
						if (flag18)
						{
							list.Add(new RichTextTagParser.Tag
							{
								tagType = tagType2,
								start = num2,
								end = num3,
								isClosing = flag3
							});
						}
						else
						{
							bool flag19 = text2 != null;
							if (flag19)
							{
								if (errors != null)
								{
									errors.Add(new RichTextTagParser.ParseError(text2, num2));
								}
							}
							num = num2 + 1;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002CB0 File Offset: 0x00000EB0
		[NullableContext(1)]
		internal unsafe static List<RichTextTagParser.Tag> PickResultingTags(List<RichTextTagParser.Tag> allTags, string input, int atPosition, [global::System.Runtime.CompilerServices.Nullable(2)] List<RichTextTagParser.Tag> applicableTags = null)
		{
			bool flag = applicableTags == null;
			if (flag)
			{
				applicableTags = new List<RichTextTagParser.Tag>();
			}
			else
			{
				applicableTags.Clear();
			}
			int num = 0;
			Debug.Assert(string.IsNullOrEmpty(input) || (atPosition < input.Length && atPosition >= 0), "Invalid position");
			Debug.Assert(num <= atPosition && num >= 0, "Invalid starting position");
			int num2 = 0;
			foreach (RichTextTagParser.Tag tag in allTags)
			{
				Debug.Assert(tag.start >= num2, "Tags are not sorted");
				num2 = tag.end + 1;
			}
			foreach (RichTextTagParser.Tag tag2 in applicableTags)
			{
				Debug.Assert(tag2.end <= num, "Tag end pass the point where we should start parsing");
				Debug.Assert(allTags.Contains(tag2));
			}
			int count = allTags.Count;
			Span<int?> span2;
			Span<int?> span3;
			int num4;
			checked
			{
				Span<int?> span = new Span<int?>(stackalloc byte[unchecked((UIntPtr)count) * (UIntPtr)sizeof(int?)], count);
				span2 = span;
				int num3 = RichTextTagParser.TagsInfo.Length;
				span = new Span<int?>(stackalloc byte[unchecked((UIntPtr)num3) * (UIntPtr)sizeof(int?)], num3);
				span3 = span;
				num4 = -1;
			}
			foreach (RichTextTagParser.Tag tag3 in allTags)
			{
				num4++;
				bool flag2 = tag3.end < num;
				if (!flag2)
				{
					bool flag3 = tag3.tagType == RichTextTagParser.TagType.NoParse;
					if (!flag3)
					{
						bool flag4 = tag3.start > atPosition;
						if (flag4)
						{
							break;
						}
						bool isClosing = tag3.isClosing;
						if (isClosing)
						{
							bool flag5 = span3[(int)tag3.tagType] != null;
							if (flag5)
							{
								bool flag6 = span2[num4] != null;
								if (flag6)
								{
									*span3[(int)tag3.tagType] = *span2[num4];
								}
								else
								{
									*span3[(int)tag3.tagType] = null;
								}
							}
						}
						else
						{
							int? num5 = *span3[(int)tag3.tagType];
							bool flag7 = num5 != null;
							if (flag7)
							{
								*span2[num4] = num5;
							}
							*span3[(int)tag3.tagType] = new int?(num4);
						}
					}
				}
			}
			int num6 = 0;
			foreach (RichTextTagParser.Tag tag4 in allTags)
			{
				int? num7 = *span3[(int)tag4.tagType];
				bool flag8 = num7 != null && num6 == num7.Value;
				if (flag8)
				{
					applicableTags.Add(tag4);
				}
				num6++;
			}
			return applicableTags;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00003038 File Offset: 0x00001238
		[NullableContext(1)]
		internal static RichTextTagParser.Segment[] GenerateSegments(string input, List<RichTextTagParser.Tag> tags)
		{
			List<RichTextTagParser.Segment> list = new List<RichTextTagParser.Segment>();
			int num = 0;
			for (int i = 0; i < tags.Count; i++)
			{
				Debug.Assert(tags[i].start >= num);
				bool flag = tags[i].start > num;
				if (flag)
				{
					list.Add(new RichTextTagParser.Segment
					{
						start = num,
						end = tags[i].start - 1
					});
				}
				num = tags[i].end + 1;
			}
			bool flag2 = num < input.Length;
			if (flag2)
			{
				list.Add(new RichTextTagParser.Segment
				{
					start = num,
					end = input.Length - 1
				});
			}
			return list.ToArray();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003118 File Offset: 0x00001318
		[NullableContext(1)]
		internal static void ApplyStateToSegment(string input, List<RichTextTagParser.Tag> tags, RichTextTagParser.Segment[] segments)
		{
			for (int i = 0; i < segments.Length; i++)
			{
				segments[i].tags = RichTextTagParser.PickResultingTags(tags, input, segments[i].start, null);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000315C File Offset: 0x0000135C
		[NullableContext(1)]
		private static int AddLink(RichTextTagParser.TagType type, string value, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] List<ValueTuple<int, RichTextTagParser.TagType, string>> links)
		{
			foreach (ValueTuple<int, RichTextTagParser.TagType, string> valueTuple in links)
			{
				int item = valueTuple.Item1;
				RichTextTagParser.TagType item2 = valueTuple.Item2;
				string item3 = valueTuple.Item3;
				bool flag = type == item2 && value == item3;
				if (flag)
				{
					return item;
				}
			}
			int count = links.Count;
			links.Add(new ValueTuple<int, RichTextTagParser.TagType, string>(count, type, value));
			return count;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000031F4 File Offset: 0x000013F4
		private static TextSpan CreateTextSpan(RichTextTagParser.Segment segment, ref NativeTextGenerationSettings tgs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] List<ValueTuple<int, RichTextTagParser.TagType, string>> links, Color hyperlinkColor)
		{
			TextSpan textSpan = tgs.CreateTextSpan();
			bool flag = segment.tags == null;
			TextSpan textSpan2;
			if (flag)
			{
				textSpan2 = textSpan;
			}
			else
			{
				for (int i = 0; i < segment.tags.Count; i++)
				{
					switch (segment.tags[i].tagType)
					{
					case RichTextTagParser.TagType.Hyperlink:
					{
						RichTextTagParser.TagType tagType = RichTextTagParser.TagType.Hyperlink;
						RichTextTagParser.TagValue value = segment.tags[i].value;
						textSpan.linkID = RichTextTagParser.AddLink(tagType, ((value != null) ? value.StringValue : null) ?? "", links);
						textSpan.color = hyperlinkColor;
						textSpan.fontStyle |= FontStyles.Underline;
						break;
					}
					case RichTextTagParser.TagType.AllCaps:
					case RichTextTagParser.TagType.Uppercase:
						textSpan.fontStyle |= FontStyles.UpperCase;
						break;
					case RichTextTagParser.TagType.Bold:
						textSpan.fontWeight = TextFontWeight.Bold;
						break;
					case RichTextTagParser.TagType.Color:
						textSpan.color = segment.tags[i].value.ColorValue;
						break;
					case RichTextTagParser.TagType.Italic:
						textSpan.fontStyle |= FontStyles.Italic;
						break;
					case RichTextTagParser.TagType.Link:
					{
						RichTextTagParser.TagType tagType2 = RichTextTagParser.TagType.Link;
						RichTextTagParser.TagValue value2 = segment.tags[i].value;
						textSpan.linkID = RichTextTagParser.AddLink(tagType2, ((value2 != null) ? value2.StringValue : null) ?? "", links);
						break;
					}
					case RichTextTagParser.TagType.Lowercase:
					case RichTextTagParser.TagType.SmallCaps:
						textSpan.fontStyle |= FontStyles.LowerCase;
						break;
					case RichTextTagParser.TagType.Mark:
						textSpan.fontStyle |= FontStyles.Highlight;
						break;
					case RichTextTagParser.TagType.NoParse:
					case RichTextTagParser.TagType.Unknown:
						throw new InvalidOperationException("Invalid tag type" + segment.tags[i].tagType.ToString());
					case RichTextTagParser.TagType.Strikethrough:
						textSpan.fontStyle |= FontStyles.Strikethrough;
						break;
					case RichTextTagParser.TagType.Subscript:
						textSpan.fontStyle |= FontStyles.Subscript;
						break;
					case RichTextTagParser.TagType.Superscript:
						textSpan.fontStyle |= FontStyles.Superscript;
						break;
					case RichTextTagParser.TagType.Underline:
						textSpan.fontStyle |= FontStyles.Underline;
						break;
					}
				}
				textSpan2 = textSpan;
			}
			return textSpan2;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000348C File Offset: 0x0000168C
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void CreateTextGenerationSettingsArray(ref NativeTextGenerationSettings tgs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] List<ValueTuple<int, RichTextTagParser.TagType, string>> links, Color hyperlinkColor)
		{
			links.Clear();
			List<RichTextTagParser.Tag> list = RichTextTagParser.FindTags(tgs.text, null);
			RichTextTagParser.Segment[] array = RichTextTagParser.GenerateSegments(tgs.text, list);
			RichTextTagParser.ApplyStateToSegment(tgs.text, list, array);
			StringBuilder stringBuilder = new StringBuilder(tgs.text.Length);
			tgs.textSpans = new TextSpan[array.Length];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				RichTextTagParser.Segment segment = array[i];
				string text = tgs.text.Substring(segment.start, segment.end + 1 - segment.start);
				TextSpan textSpan = RichTextTagParser.CreateTextSpan(segment, ref tgs, links, hyperlinkColor);
				textSpan.startIndex = num;
				textSpan.length = text.Length;
				tgs.textSpans[i] = textSpan;
				stringBuilder.Append(text);
				num += text.Length;
			}
			tgs.text = stringBuilder.ToString();
		}

		// Token: 0x04000034 RID: 52
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal static readonly RichTextTagParser.TagTypeInfo[] TagsInfo = new RichTextTagParser.TagTypeInfo[]
		{
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Hyperlink, "a", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Align, "align", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.AllCaps, "allcaps", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Alpha, "alpha", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Bold, "b", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Br, "br", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Color, "color", RichTextTagParser.TagValueType.ColorValue, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.CSpace, "cspace", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Font, "font", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.FontWeight, "font-weight", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Italic, "i", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Indent, "indent", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.LineHeight, "line-height", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.LineIndent, "line-indent", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Link, "link", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Lowercase, "lowercase", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Mark, "mark", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Mspace, "mspace", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.NoBr, "nobr", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.NoParse, "noparse", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Strikethrough, "s", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Size, "size", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.SmallCaps, "smallcaps", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Space, "space", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Sprite, "sprite", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Style, "style", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Subscript, "sub", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Superscript, "sup", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Underline, "u", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels),
			new RichTextTagParser.TagTypeInfo(RichTextTagParser.TagType.Uppercase, "uppercase", RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType.Pixels)
		};

		// Token: 0x0200000E RID: 14
		public enum TagType
		{
			// Token: 0x04000036 RID: 54
			Hyperlink,
			// Token: 0x04000037 RID: 55
			Align,
			// Token: 0x04000038 RID: 56
			AllCaps,
			// Token: 0x04000039 RID: 57
			Alpha,
			// Token: 0x0400003A RID: 58
			Bold,
			// Token: 0x0400003B RID: 59
			Br,
			// Token: 0x0400003C RID: 60
			Color,
			// Token: 0x0400003D RID: 61
			CSpace,
			// Token: 0x0400003E RID: 62
			Font,
			// Token: 0x0400003F RID: 63
			FontWeight,
			// Token: 0x04000040 RID: 64
			Italic,
			// Token: 0x04000041 RID: 65
			Indent,
			// Token: 0x04000042 RID: 66
			LineHeight,
			// Token: 0x04000043 RID: 67
			LineIndent,
			// Token: 0x04000044 RID: 68
			Link,
			// Token: 0x04000045 RID: 69
			Lowercase,
			// Token: 0x04000046 RID: 70
			Mark,
			// Token: 0x04000047 RID: 71
			Mspace,
			// Token: 0x04000048 RID: 72
			NoBr,
			// Token: 0x04000049 RID: 73
			NoParse,
			// Token: 0x0400004A RID: 74
			Strikethrough,
			// Token: 0x0400004B RID: 75
			Size,
			// Token: 0x0400004C RID: 76
			SmallCaps,
			// Token: 0x0400004D RID: 77
			Space,
			// Token: 0x0400004E RID: 78
			Sprite,
			// Token: 0x0400004F RID: 79
			Style,
			// Token: 0x04000050 RID: 80
			Subscript,
			// Token: 0x04000051 RID: 81
			Superscript,
			// Token: 0x04000052 RID: 82
			Underline,
			// Token: 0x04000053 RID: 83
			Uppercase,
			// Token: 0x04000054 RID: 84
			Unknown
		}

		// Token: 0x0200000F RID: 15
		[global::System.Runtime.CompilerServices.Nullable(0)]
		[NullableContext(1)]
		internal class TagTypeInfo : IEquatable<RichTextTagParser.TagTypeInfo>
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000019 RID: 25 RVA: 0x000037AB File Offset: 0x000019AB
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[CompilerGenerated]
				get
				{
					return typeof(RichTextTagParser.TagTypeInfo);
				}
			}

			// Token: 0x0600001A RID: 26 RVA: 0x000037B7 File Offset: 0x000019B7
			internal TagTypeInfo(RichTextTagParser.TagType tagType, string name, RichTextTagParser.TagValueType valueType = RichTextTagParser.TagValueType.None, RichTextTagParser.TagUnitType unitType = RichTextTagParser.TagUnitType.Pixels)
			{
				this.TagType = tagType;
				this.name = name;
				this.valueType = valueType;
				this.unitType = unitType;
			}

			// Token: 0x0600001B RID: 27 RVA: 0x000037E0 File Offset: 0x000019E0
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("TagTypeInfo");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x0600001C RID: 28 RVA: 0x0000382C File Offset: 0x00001A2C
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("TagType = ");
				builder.Append(this.TagType.ToString());
				builder.Append(", name = ");
				builder.Append(this.name);
				builder.Append(", valueType = ");
				builder.Append(this.valueType.ToString());
				builder.Append(", unitType = ");
				builder.Append(this.unitType.ToString());
				return true;
			}

			// Token: 0x0600001D RID: 29 RVA: 0x000038C4 File Offset: 0x00001AC4
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(RichTextTagParser.TagTypeInfo left, RichTextTagParser.TagTypeInfo right)
			{
				return !(left == right);
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000038D0 File Offset: 0x00001AD0
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(RichTextTagParser.TagTypeInfo left, RichTextTagParser.TagTypeInfo right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x0600001F RID: 31 RVA: 0x000038E8 File Offset: 0x00001AE8
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<RichTextTagParser.TagType>.Default.GetHashCode(this.TagType)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.name)) * -1521134295 + EqualityComparer<RichTextTagParser.TagValueType>.Default.GetHashCode(this.valueType)) * -1521134295 + EqualityComparer<RichTextTagParser.TagUnitType>.Default.GetHashCode(this.unitType);
			}

			// Token: 0x06000020 RID: 32 RVA: 0x00003961 File Offset: 0x00001B61
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as RichTextTagParser.TagTypeInfo);
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00003970 File Offset: 0x00001B70
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(RichTextTagParser.TagTypeInfo other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<RichTextTagParser.TagType>.Default.Equals(this.TagType, other.TagType) && EqualityComparer<string>.Default.Equals(this.name, other.name) && EqualityComparer<RichTextTagParser.TagValueType>.Default.Equals(this.valueType, other.valueType) && EqualityComparer<RichTextTagParser.TagUnitType>.Default.Equals(this.unitType, other.unitType));
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00003A03 File Offset: 0x00001C03
			[CompilerGenerated]
			protected TagTypeInfo(RichTextTagParser.TagTypeInfo original)
			{
				this.TagType = original.TagType;
				this.name = original.name;
				this.valueType = original.valueType;
				this.unitType = original.unitType;
			}

			// Token: 0x04000055 RID: 85
			public RichTextTagParser.TagType TagType;

			// Token: 0x04000056 RID: 86
			public string name;

			// Token: 0x04000057 RID: 87
			public RichTextTagParser.TagValueType valueType;

			// Token: 0x04000058 RID: 88
			public RichTextTagParser.TagUnitType unitType;
		}

		// Token: 0x02000010 RID: 16
		internal enum TagValueType
		{
			// Token: 0x0400005A RID: 90
			None,
			// Token: 0x0400005B RID: 91
			NumericalValue,
			// Token: 0x0400005C RID: 92
			StringValue,
			// Token: 0x0400005D RID: 93
			ColorValue = 4
		}

		// Token: 0x02000011 RID: 17
		internal enum TagUnitType
		{
			// Token: 0x0400005F RID: 95
			Pixels,
			// Token: 0x04000060 RID: 96
			FontUnits,
			// Token: 0x04000061 RID: 97
			Percentage
		}

		// Token: 0x02000012 RID: 18
		[NullableContext(2)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class TagValue : IEquatable<RichTextTagParser.TagValue>
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000024 RID: 36 RVA: 0x00003A3C File Offset: 0x00001C3C
			[global::System.Runtime.CompilerServices.Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[CompilerGenerated]
				[NullableContext(1)]
				get
				{
					return typeof(RichTextTagParser.TagValue);
				}
			}

			// Token: 0x06000025 RID: 37 RVA: 0x00003A48 File Offset: 0x00001C48
			internal TagValue(float value)
			{
				this.type = RichTextTagParser.TagValueType.NumericalValue;
				this.m_numericalValue = value;
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00003A60 File Offset: 0x00001C60
			internal TagValue(Color value)
			{
				this.type = RichTextTagParser.TagValueType.ColorValue;
				this.m_colorValue = value;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00003A78 File Offset: 0x00001C78
			[NullableContext(1)]
			internal TagValue(string value)
			{
				this.type = RichTextTagParser.TagValueType.StringValue;
				this.m_stringValue = value;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000028 RID: 40 RVA: 0x00003A90 File Offset: 0x00001C90
			internal string StringValue
			{
				get
				{
					bool flag = this.type != RichTextTagParser.TagValueType.StringValue;
					if (flag)
					{
						throw new InvalidOperationException("Not a string value");
					}
					return this.m_stringValue;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00003AC4 File Offset: 0x00001CC4
			internal float NumericalValue
			{
				get
				{
					bool flag = this.type != RichTextTagParser.TagValueType.NumericalValue;
					if (flag)
					{
						throw new InvalidOperationException("Not a numerical value");
					}
					return this.m_numericalValue;
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600002A RID: 42 RVA: 0x00003AF8 File Offset: 0x00001CF8
			internal Color ColorValue
			{
				get
				{
					bool flag = this.type != RichTextTagParser.TagValueType.ColorValue;
					if (flag)
					{
						throw new InvalidOperationException("Not a color value");
					}
					return this.m_colorValue;
				}
			}

			// Token: 0x0600002B RID: 43 RVA: 0x00003B2C File Offset: 0x00001D2C
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("TagValue");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00003B78 File Offset: 0x00001D78
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				return false;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00003B7B File Offset: 0x00001D7B
			[CompilerGenerated]
			public static bool operator !=(RichTextTagParser.TagValue left, RichTextTagParser.TagValue right)
			{
				return !(left == right);
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00003B87 File Offset: 0x00001D87
			[CompilerGenerated]
			public static bool operator ==(RichTextTagParser.TagValue left, RichTextTagParser.TagValue right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00003BA0 File Offset: 0x00001DA0
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<RichTextTagParser.TagValueType>.Default.GetHashCode(this.type)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.m_stringValue)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.m_numericalValue)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.m_colorValue);
			}

			// Token: 0x06000030 RID: 48 RVA: 0x00003C19 File Offset: 0x00001E19
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as RichTextTagParser.TagValue);
			}

			// Token: 0x06000031 RID: 49 RVA: 0x00003C28 File Offset: 0x00001E28
			[CompilerGenerated]
			public virtual bool Equals(RichTextTagParser.TagValue other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<RichTextTagParser.TagValueType>.Default.Equals(this.type, other.type) && EqualityComparer<string>.Default.Equals(this.m_stringValue, other.m_stringValue) && EqualityComparer<float>.Default.Equals(this.m_numericalValue, other.m_numericalValue) && EqualityComparer<Color>.Default.Equals(this.m_colorValue, other.m_colorValue));
			}

			// Token: 0x06000033 RID: 51 RVA: 0x00003CBB File Offset: 0x00001EBB
			[CompilerGenerated]
			protected TagValue([global::System.Runtime.CompilerServices.Nullable(1)] RichTextTagParser.TagValue original)
			{
				this.type = original.type;
				this.m_stringValue = original.m_stringValue;
				this.m_numericalValue = original.m_numericalValue;
				this.m_colorValue = original.m_colorValue;
			}

			// Token: 0x04000062 RID: 98
			internal RichTextTagParser.TagValueType type;

			// Token: 0x04000063 RID: 99
			private string m_stringValue;

			// Token: 0x04000064 RID: 100
			private float m_numericalValue;

			// Token: 0x04000065 RID: 101
			private Color m_colorValue;
		}

		// Token: 0x02000013 RID: 19
		internal struct Tag
		{
			// Token: 0x04000066 RID: 102
			public RichTextTagParser.TagType tagType;

			// Token: 0x04000067 RID: 103
			public bool isClosing;

			// Token: 0x04000068 RID: 104
			public int start;

			// Token: 0x04000069 RID: 105
			public int end;

			// Token: 0x0400006A RID: 106
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public RichTextTagParser.TagValue value;
		}

		// Token: 0x02000014 RID: 20
		public struct Segment
		{
			// Token: 0x0400006B RID: 107
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public List<RichTextTagParser.Tag> tags;

			// Token: 0x0400006C RID: 108
			public int start;

			// Token: 0x0400006D RID: 109
			public int end;
		}

		// Token: 0x02000015 RID: 21
		[NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class ParseError : IEquatable<RichTextTagParser.ParseError>
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000034 RID: 52 RVA: 0x00003CF4 File Offset: 0x00001EF4
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[CompilerGenerated]
				get
				{
					return typeof(RichTextTagParser.ParseError);
				}
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00003D00 File Offset: 0x00001F00
			internal ParseError(string message, int position)
			{
				this.message = message;
				this.position = position;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00003D18 File Offset: 0x00001F18
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("ParseError");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00003D64 File Offset: 0x00001F64
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("position = ");
				builder.Append(this.position.ToString());
				builder.Append(", message = ");
				builder.Append(this.message);
				return true;
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00003DB4 File Offset: 0x00001FB4
			[CompilerGenerated]
			[NullableContext(2)]
			public static bool operator !=(RichTextTagParser.ParseError left, RichTextTagParser.ParseError right)
			{
				return !(left == right);
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00003DC0 File Offset: 0x00001FC0
			[CompilerGenerated]
			[NullableContext(2)]
			public static bool operator ==(RichTextTagParser.ParseError left, RichTextTagParser.ParseError right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00003DD6 File Offset: 0x00001FD6
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.position)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.message);
			}

			// Token: 0x0600003B RID: 59 RVA: 0x00003E16 File Offset: 0x00002016
			[CompilerGenerated]
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as RichTextTagParser.ParseError);
			}

			// Token: 0x0600003C RID: 60 RVA: 0x00003E24 File Offset: 0x00002024
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(RichTextTagParser.ParseError other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.position, other.position) && EqualityComparer<string>.Default.Equals(this.message, other.message));
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00003E87 File Offset: 0x00002087
			[CompilerGenerated]
			protected ParseError(RichTextTagParser.ParseError original)
			{
				this.position = original.position;
				this.message = original.message;
			}

			// Token: 0x0400006E RID: 110
			public readonly int position;

			// Token: 0x0400006F RID: 111
			public readonly string message;
		}
	}
}
