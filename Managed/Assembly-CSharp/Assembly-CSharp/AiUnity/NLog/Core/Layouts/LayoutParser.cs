using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Conditions;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.LayoutRenderers;
using AiUnity.NLog.Core.LayoutRenderers.Wrappers;

namespace AiUnity.NLog.Core.Layouts
{
	// Token: 0x0200023F RID: 575
	internal sealed class LayoutParser
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000754D0 File Offset: 0x000736D0
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000754D8 File Offset: 0x000736D8
		internal static LayoutRenderer[] CompileLayout(ConfigurationItemFactory configurationItemFactory, SimpleStringReader sr, bool isNested, out string text)
		{
			List<LayoutRenderer> list = new List<LayoutRenderer>();
			StringBuilder stringBuilder = new StringBuilder();
			int position = sr.Position;
			int num;
			while ((num = sr.Peek()) != -1 && (!isNested || (num != 125 && num != 58)))
			{
				sr.Read();
				if (num == 36 && sr.Peek() == 123)
				{
					if (stringBuilder.Length > 0)
					{
						list.Add(new LiteralLayoutRenderer(stringBuilder.ToString()));
						stringBuilder.Length = 0;
					}
					LayoutRenderer layoutRenderer = LayoutParser.ParseLayoutRenderer(configurationItemFactory, sr);
					if (LayoutParser.CanBeConvertedToLiteral(layoutRenderer))
					{
						layoutRenderer = LayoutParser.ConvertToLiteral(layoutRenderer);
					}
					list.Add(layoutRenderer);
				}
				else
				{
					stringBuilder.Append((char)num);
				}
			}
			if (stringBuilder.Length > 0)
			{
				list.Add(new LiteralLayoutRenderer(stringBuilder.ToString()));
				stringBuilder.Length = 0;
			}
			int position2 = sr.Position;
			LayoutParser.MergeLiterals(list);
			text = sr.Substring(position, position2);
			return list.ToArray();
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000755B8 File Offset: 0x000737B8
		private static string ParseLayoutRendererName(SimpleStringReader sr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = sr.Peek()) != -1 && num != 58 && num != 125)
			{
				stringBuilder.Append((char)num);
				sr.Read();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x000755F8 File Offset: 0x000737F8
		private static string ParseParameterName(SimpleStringReader sr)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			int num2;
			while ((num2 = sr.Peek()) != -1 && ((num2 != 61 && num2 != 125 && num2 != 58) || num != 0))
			{
				if (num2 == 36)
				{
					sr.Read();
					stringBuilder.Append('$');
					if (sr.Peek() == 123)
					{
						stringBuilder.Append('{');
						num++;
						sr.Read();
					}
				}
				else
				{
					if (num2 == 125)
					{
						num--;
					}
					if (num2 == 92)
					{
						sr.Read();
						stringBuilder.Append((char)sr.Read());
					}
					else
					{
						stringBuilder.Append((char)num2);
						sr.Read();
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000756A0 File Offset: 0x000738A0
		private static string ParseParameterValue(SimpleStringReader sr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = sr.Peek()) != -1 && num != 58 && num != 125)
			{
				if (num == 92)
				{
					sr.Read();
					char c = (char)sr.Peek();
					if (c <= '\\')
					{
						if (c <= '0')
						{
							if (c != '"')
							{
								if (c != '\'')
								{
									if (c == '0')
									{
										sr.Read();
										stringBuilder.Append('\0');
									}
								}
								else
								{
									sr.Read();
									stringBuilder.Append('\'');
								}
							}
							else
							{
								sr.Read();
								stringBuilder.Append('"');
							}
						}
						else if (c != ':')
						{
							if (c != 'U')
							{
								if (c == '\\')
								{
									sr.Read();
									stringBuilder.Append('\\');
								}
							}
							else
							{
								sr.Read();
								char unicode = LayoutParser.GetUnicode(sr, 8);
								stringBuilder.Append(unicode);
							}
						}
						else
						{
							sr.Read();
							stringBuilder.Append(':');
						}
					}
					else if (c <= 'f')
					{
						if (c != 'a')
						{
							if (c != 'b')
							{
								if (c == 'f')
								{
									sr.Read();
									stringBuilder.Append('\f');
								}
							}
							else
							{
								sr.Read();
								stringBuilder.Append('\b');
							}
						}
						else
						{
							sr.Read();
							stringBuilder.Append('\a');
						}
					}
					else
					{
						switch (c)
						{
						case 'n':
							sr.Read();
							stringBuilder.Append('\n');
							break;
						case 'o':
						case 'p':
						case 'q':
						case 's':
						case 'w':
							break;
						case 'r':
							sr.Read();
							stringBuilder.Append('\r');
							break;
						case 't':
							sr.Read();
							stringBuilder.Append('\t');
							break;
						case 'u':
						{
							sr.Read();
							char unicode2 = LayoutParser.GetUnicode(sr, 4);
							stringBuilder.Append(unicode2);
							break;
						}
						case 'v':
							sr.Read();
							stringBuilder.Append('\v');
							break;
						case 'x':
						{
							sr.Read();
							char unicode3 = LayoutParser.GetUnicode(sr, 4);
							stringBuilder.Append(unicode3);
							break;
						}
						default:
							if (c != '{')
							{
								if (c == '}')
								{
									sr.Read();
									stringBuilder.Append('}');
								}
							}
							else
							{
								sr.Read();
								stringBuilder.Append('{');
							}
							break;
						}
					}
				}
				else
				{
					stringBuilder.Append((char)num);
					sr.Read();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00075918 File Offset: 0x00073B18
		private static char GetUnicode(SimpleStringReader sr, int maxDigits)
		{
			int num = 0;
			for (int i = 0; i < maxDigits; i++)
			{
				int num2 = sr.Peek();
				if (num2 >= 48 && num2 <= 57)
				{
					num2 -= 48;
				}
				else if (num2 >= 97 && num2 <= 102)
				{
					num2 = num2 - 97 + 10;
				}
				else
				{
					if (num2 < 65 || num2 > 70)
					{
						break;
					}
					num2 = num2 - 65 + 10;
				}
				sr.Read();
				num = num * 16 + num2;
			}
			return (char)num;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00075984 File Offset: 0x00073B84
		private static LayoutRenderer ParseLayoutRenderer(ConfigurationItemFactory configurationItemFactory, SimpleStringReader sr)
		{
			int num = sr.Read();
			LayoutParser.Logger.Assert(num == 123, "'{' expected in layout specification", Array.Empty<object>());
			string text = LayoutParser.ParseLayoutRendererName(sr);
			LayoutRenderer layoutRenderer = configurationItemFactory.LayoutRenderers.CreateInstance(text);
			Dictionary<Type, LayoutRenderer> dictionary = new Dictionary<Type, LayoutRenderer>();
			List<LayoutRenderer> list = new List<LayoutRenderer>();
			num = sr.Read();
			while (num != -1 && num != 125)
			{
				string text2 = LayoutParser.ParseParameterName(sr).Trim();
				PropertyInfo propertyInfo2;
				if (sr.Peek() == 61)
				{
					sr.Read();
					LayoutRenderer layoutRenderer2 = layoutRenderer;
					PropertyInfo propertyInfo;
					Type type;
					if (!PropertyHelper.TryGetPropertyInfo(layoutRenderer, text2, out propertyInfo) && configurationItemFactory.AmbientProperties.TryGetDefinition(text2, out type))
					{
						LayoutRenderer layoutRenderer3;
						if (!dictionary.TryGetValue(type, out layoutRenderer3))
						{
							layoutRenderer3 = configurationItemFactory.AmbientProperties.CreateInstance(text2);
							dictionary[type] = layoutRenderer3;
							list.Add(layoutRenderer3);
						}
						if (!PropertyHelper.TryGetPropertyInfo(layoutRenderer3, text2, out propertyInfo))
						{
							propertyInfo = null;
						}
						else
						{
							layoutRenderer2 = layoutRenderer3;
						}
					}
					if (propertyInfo == null)
					{
						LayoutParser.ParseParameterValue(sr);
					}
					else if (typeof(Layout).IsAssignableFrom(propertyInfo.PropertyType))
					{
						SimpleLayout simpleLayout = new SimpleLayout();
						string text3;
						LayoutRenderer[] array = LayoutParser.CompileLayout(configurationItemFactory, sr, true, out text3);
						simpleLayout.SetRenderers(array, text3);
						propertyInfo.SetValue(layoutRenderer2, simpleLayout, null);
					}
					else if (typeof(ConditionExpression).IsAssignableFrom(propertyInfo.PropertyType))
					{
						ConditionExpression conditionExpression = ConditionParser.ParseExpression(sr, configurationItemFactory);
						propertyInfo.SetValue(layoutRenderer2, conditionExpression, null);
					}
					else
					{
						string text4 = LayoutParser.ParseParameterValue(sr);
						PropertyHelper.SetPropertyFromString(layoutRenderer2, text2, text4, configurationItemFactory);
					}
				}
				else if (PropertyHelper.TryGetPropertyInfo(layoutRenderer, string.Empty, out propertyInfo2))
				{
					if (typeof(SimpleLayout) == propertyInfo2.PropertyType)
					{
						propertyInfo2.SetValue(layoutRenderer, new SimpleLayout(text2), null);
					}
					else
					{
						string text5 = text2;
						PropertyHelper.SetPropertyFromString(layoutRenderer, propertyInfo2.Name, text5, configurationItemFactory);
					}
				}
				else
				{
					LayoutParser.Logger.Warn("{0} has no default property", new object[] { layoutRenderer.GetType().FullName });
				}
				num = sr.Read();
			}
			return LayoutParser.ApplyWrappers(configurationItemFactory, layoutRenderer, list);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00075B98 File Offset: 0x00073D98
		private static LayoutRenderer ApplyWrappers(ConfigurationItemFactory configurationItemFactory, LayoutRenderer lr, List<LayoutRenderer> orderedWrappers)
		{
			for (int i = orderedWrappers.Count - 1; i >= 0; i--)
			{
				WrapperLayoutRendererBase wrapperLayoutRendererBase = (WrapperLayoutRendererBase)orderedWrappers[i];
				LayoutParser.Logger.Trace("Wrapping {0} with {1}", new object[]
				{
					lr.GetType().Name,
					wrapperLayoutRendererBase.GetType().Name
				});
				if (LayoutParser.CanBeConvertedToLiteral(lr))
				{
					lr = LayoutParser.ConvertToLiteral(lr);
				}
				wrapperLayoutRendererBase.Inner = new SimpleLayout(new LayoutRenderer[] { lr }, string.Empty, configurationItemFactory);
				lr = wrapperLayoutRendererBase;
			}
			return lr;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00075C28 File Offset: 0x00073E28
		private static bool CanBeConvertedToLiteral(LayoutRenderer lr)
		{
			foreach (IRenderable renderable in ObjectGraphScanner.FindReachableObjects<IRenderable>(new object[] { lr }))
			{
				if (!(renderable.GetType() == typeof(SimpleLayout)) && !renderable.GetType().IsDefined(typeof(AppDomainFixedOutputAttribute), false))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00075C8C File Offset: 0x00073E8C
		private static void MergeLiterals(List<LayoutRenderer> list)
		{
			int num = 0;
			while (num + 1 < list.Count)
			{
				LiteralLayoutRenderer literalLayoutRenderer = list[num] as LiteralLayoutRenderer;
				LiteralLayoutRenderer literalLayoutRenderer2 = list[num + 1] as LiteralLayoutRenderer;
				if (literalLayoutRenderer != null && literalLayoutRenderer2 != null)
				{
					LiteralLayoutRenderer literalLayoutRenderer3 = literalLayoutRenderer;
					literalLayoutRenderer3.Text += literalLayoutRenderer2.Text;
					list.RemoveAt(num + 1);
				}
				else
				{
					num++;
				}
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00075CF0 File Offset: 0x00073EF0
		private static LayoutRenderer ConvertToLiteral(LayoutRenderer renderer)
		{
			return new LiteralLayoutRenderer(renderer.Render(LogEventInfo.CreateNullEvent()));
		}
	}
}
