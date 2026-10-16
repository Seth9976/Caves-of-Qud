using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000124 RID: 292
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AnalyzerConfig
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0001D7FD File Offset: 0x0001B9FD
		internal static ImmutableHashSet<string> ReservedKeys { get; } = ImmutableHashSet.CreateRange<string>(AnalyzerConfig.Section.PropertiesKeyComparer, new string[] { "root", "indent_style", "indent_size", "tab_width", "end_of_line", "charset", "trim_trailing_whitespace", "insert_final_newline" });

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001D804 File Offset: 0x0001BA04
		internal static ImmutableHashSet<string> ReservedValues { get; } = ImmutableHashSet.CreateRange<string>(CaseInsensitiveComparison.Comparer, new string[] { "unset" });

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0001D80B File Offset: 0x0001BA0B
		internal AnalyzerConfig.Section GlobalSection { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001D813 File Offset: 0x0001BA13
		internal string NormalizedDirectory { get; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0001D81B File Offset: 0x0001BA1B
		internal string PathToFile { get; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0001D823 File Offset: 0x0001BA23
		internal static Comparer<AnalyzerConfig> DirectoryLengthComparer { get; } = Comparer<AnalyzerConfig>.Create((AnalyzerConfig e1, AnalyzerConfig e2) => e1.NormalizedDirectory.Length.CompareTo(e2.NormalizedDirectory.Length));

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0001D82A File Offset: 0x0001BA2A
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<AnalyzerConfig.Section> NamedSections
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0001D834 File Offset: 0x0001BA34
		internal bool IsRoot
		{
			get
			{
				string text;
				return this.GlobalSection.Properties.TryGetValue("root", out text) && text == "true";
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0001D867 File Offset: 0x0001BA67
		internal bool IsGlobal
		{
			get
			{
				return this._hasGlobalFileName || this.GlobalSection.Properties.ContainsKey("is_global");
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001D888 File Offset: 0x0001BA88
		internal int GlobalLevel
		{
			get
			{
				string text;
				int num;
				if (this.GlobalSection.Properties.TryGetValue("global_level", out text) && int.TryParse(text, ref num))
				{
					return num;
				}
				if (this._hasGlobalFileName)
				{
					return 100;
				}
				return 0;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001D8C8 File Offset: 0x0001BAC8
		private AnalyzerConfig(AnalyzerConfig.Section globalSection, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AnalyzerConfig.Section> namedSections, string pathToFile)
		{
			this.GlobalSection = globalSection;
			this.NamedSections = namedSections;
			this.PathToFile = pathToFile;
			this._hasGlobalFileName = Path.GetFileName(pathToFile).Equals(".globalconfig", 5);
			string text = Path.GetDirectoryName(pathToFile) ?? pathToFile;
			this.NormalizedDirectory = PathUtilities.NormalizeWithForwardSlash(text);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001D91F File Offset: 0x0001BB1F
		public static AnalyzerConfig Parse(string text, [Nullable(2)] string pathToFile)
		{
			return AnalyzerConfig.Parse(SourceText.From(text, null, SourceHashAlgorithm.Sha1), pathToFile);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001D930 File Offset: 0x0001BB30
		public static AnalyzerConfig Parse(SourceText text, [Nullable(2)] string pathToFile)
		{
			if (pathToFile == null || !Path.IsPathRooted(pathToFile) || string.IsNullOrEmpty(Path.GetFileName(pathToFile)))
			{
				throw new ArgumentException("Must be an absolute path to an editorconfig file", "pathToFile");
			}
			AnalyzerConfig.<>c__DisplayClass35_0 CS$<>8__locals1;
			CS$<>8__locals1.globalSection = null;
			CS$<>8__locals1.namedSectionBuilder = ImmutableArray.CreateBuilder<AnalyzerConfig.Section>();
			CS$<>8__locals1.activeSectionProperties = ImmutableDictionary.CreateBuilder<string, string>(AnalyzerConfig.Section.PropertiesKeyComparer);
			CS$<>8__locals1.activeSectionName = "";
			foreach (TextLine textLine in text.Lines)
			{
				string text2 = textLine.ToString();
				if (!string.IsNullOrWhiteSpace(text2) && !AnalyzerConfig.IsComment(text2))
				{
					MatchCollection matchCollection = AnalyzerConfig.s_sectionMatcher.Matches(text2);
					if (matchCollection.Count > 0 && matchCollection[0].Groups.Count > 0)
					{
						AnalyzerConfig.<Parse>g__addNewSection|35_0(ref CS$<>8__locals1);
						string value = matchCollection[0].Groups[1].Value;
						CS$<>8__locals1.activeSectionName = value;
						CS$<>8__locals1.activeSectionProperties = ImmutableDictionary.CreateBuilder<string, string>(AnalyzerConfig.Section.PropertiesKeyComparer);
					}
					else
					{
						MatchCollection matchCollection2 = AnalyzerConfig.s_propertyMatcher.Matches(text2);
						if (matchCollection2.Count > 0 && matchCollection2[0].Groups.Count > 1)
						{
							string text3 = matchCollection2[0].Groups[1].Value;
							string text4 = matchCollection2[0].Groups[2].Value;
							text3 = CaseInsensitiveComparison.ToLower(text3);
							if (AnalyzerConfig.ReservedKeys.Contains(text3) || AnalyzerConfig.ReservedValues.Contains(text4))
							{
								text4 = CaseInsensitiveComparison.ToLower(text4);
							}
							CS$<>8__locals1.activeSectionProperties[text3] = text4 ?? "";
						}
					}
				}
			}
			AnalyzerConfig.<Parse>g__addNewSection|35_0(ref CS$<>8__locals1);
			return new AnalyzerConfig(CS$<>8__locals1.globalSection, CS$<>8__locals1.namedSectionBuilder.ToImmutable(), pathToFile);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001DB40 File Offset: 0x0001BD40
		private static bool IsComment(string line)
		{
			for (int i = 0; i < line.Length; i++)
			{
				char c = line.get_Chars(i);
				if (!char.IsWhiteSpace(c))
				{
					return c == '#' || c == ';';
				}
			}
			return false;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001DB80 File Offset: 0x0001BD80
		internal static AnalyzerConfig.SectionNameMatcher? TryCreateSectionNameMatcher(string sectionName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('^');
			if (!sectionName.Contains("/"))
			{
				stringBuilder.Append(".*/");
			}
			else if (sectionName.get_Chars(0) != '/')
			{
				stringBuilder.Append('/');
			}
			AnalyzerConfig.SectionNameLexer sectionNameLexer = new AnalyzerConfig.SectionNameLexer(sectionName);
			ArrayBuilder<ValueTuple<int, int>> instance = ArrayBuilder<ValueTuple<int, int>>.GetInstance();
			if (!AnalyzerConfig.TryCompilePathList(ref sectionNameLexer, stringBuilder, false, instance))
			{
				instance.Free();
				return default(AnalyzerConfig.SectionNameMatcher?);
			}
			stringBuilder.Append('$');
			return new AnalyzerConfig.SectionNameMatcher?(new AnalyzerConfig.SectionNameMatcher(new Regex(stringBuilder.ToString(), 8), instance.ToImmutableAndFree()));
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001DC1C File Offset: 0x0001BE1C
		internal static bool IsAbsoluteEditorConfigPath(string sectionName)
		{
			AnalyzerConfig.SectionNameLexer sectionNameLexer = new AnalyzerConfig.SectionNameLexer(sectionName);
			bool flag = false;
			int num = 0;
			while (!sectionNameLexer.IsDone)
			{
				if (sectionNameLexer.Lex() != AnalyzerConfig.TokenKind.SimpleCharacter)
				{
					return false;
				}
				char c = sectionNameLexer.EatCurrentCharacter();
				if (num == 0)
				{
					if (c == '/')
					{
						flag = true;
					}
					else if (Path.DirectorySeparatorChar == '/')
					{
						return false;
					}
				}
				else if (!flag && Path.DirectorySeparatorChar == '\\')
				{
					if (num == 1 && c != ':')
					{
						return false;
					}
					if (num == 2)
					{
						if (c != '/')
						{
							return false;
						}
						flag = true;
					}
				}
				num++;
			}
			return flag;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001DC94 File Offset: 0x0001BE94
		private static bool TryCompilePathList(ref AnalyzerConfig.SectionNameLexer lexer, StringBuilder sb, bool parsingChoice, [TupleElementNames(new string[] { "minValue", "maxValue" })] [Nullable(new byte[] { 1, 0 })] ArrayBuilder<ValueTuple<int, int>> numberRangePairs)
		{
			while (!lexer.IsDone)
			{
				AnalyzerConfig.TokenKind tokenKind = lexer.Lex();
				switch (tokenKind)
				{
				case AnalyzerConfig.TokenKind.BadToken:
					return false;
				case AnalyzerConfig.TokenKind.SimpleCharacter:
					sb.Append(Regex.Escape(lexer.EatCurrentCharacter().ToString()));
					continue;
				case AnalyzerConfig.TokenKind.Star:
					sb.Append("[^/]*");
					continue;
				case AnalyzerConfig.TokenKind.StarStar:
					sb.Append(".*");
					continue;
				case AnalyzerConfig.TokenKind.Question:
					sb.Append('.');
					continue;
				case AnalyzerConfig.TokenKind.OpenCurly:
				{
					int position = lexer.Position;
					lexer.Position = position - 1;
					ValueTuple<string, string>? valueTuple = AnalyzerConfig.TryParseNumberRange(ref lexer);
					if (valueTuple == null)
					{
						if (!AnalyzerConfig.TryCompileChoice(ref lexer, sb, numberRangePairs))
						{
							return false;
						}
						continue;
					}
					else
					{
						ValueTuple<string, string> valueOrDefault = valueTuple.GetValueOrDefault();
						string item = valueOrDefault.Item1;
						string item2 = valueOrDefault.Item2;
						int num;
						int num2;
						if (int.TryParse(item, ref num) && int.TryParse(item2, ref num2))
						{
							ValueTuple<int, int> valueTuple2 = ((num < num2) ? new ValueTuple<int, int>(num, num2) : new ValueTuple<int, int>(num2, num));
							numberRangePairs.Add(valueTuple2);
							sb.Append("(-?[0-9]+)");
							continue;
						}
						return false;
					}
					break;
				}
				case AnalyzerConfig.TokenKind.CloseCurly:
					return parsingChoice;
				case AnalyzerConfig.TokenKind.Comma:
					return parsingChoice;
				case AnalyzerConfig.TokenKind.OpenBracket:
					sb.Append('[');
					if (!AnalyzerConfig.TryCompileCharacterClass(ref lexer, sb))
					{
						return false;
					}
					continue;
				}
				throw ExceptionUtilities.UnexpectedValue(tokenKind);
			}
			return !parsingChoice;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
		private static bool TryCompileCharacterClass(ref AnalyzerConfig.SectionNameLexer lexer, StringBuilder sb)
		{
			if (!lexer.IsDone && lexer.CurrentCharacter == '!')
			{
				sb.Append('^');
				int position = lexer.Position;
				lexer.Position = position + 1;
			}
			while (!lexer.IsDone)
			{
				char c = lexer.EatCurrentCharacter();
				if (c != '-')
				{
					if (c != '\\')
					{
						if (c == ']')
						{
							sb.Append(c);
							return true;
						}
						sb.Append(Regex.Escape(c.ToString()));
					}
					else
					{
						if (lexer.IsDone)
						{
							return false;
						}
						sb.Append('\\');
						sb.Append(lexer.EatCurrentCharacter());
					}
				}
				else
				{
					sb.Append(c);
				}
			}
			return false;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001DE9C File Offset: 0x0001C09C
		private static bool TryCompileChoice(ref AnalyzerConfig.SectionNameLexer lexer, StringBuilder sb, [Nullable(new byte[] { 1, 0 })] ArrayBuilder<ValueTuple<int, int>> numberRangePairs)
		{
			if (lexer.Lex() != AnalyzerConfig.TokenKind.OpenCurly)
			{
				return false;
			}
			sb.Append("(?:");
			while (AnalyzerConfig.TryCompilePathList(ref lexer, sb, true, numberRangePairs))
			{
				char c = lexer[lexer.Position - 1];
				if (c == ',')
				{
					sb.Append("|");
				}
				else
				{
					if (c == '}')
					{
						sb.Append(")");
						return true;
					}
					throw ExceptionUtilities.UnexpectedValue(c);
				}
			}
			return false;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001DF10 File Offset: 0x0001C110
		[return: TupleElementNames(new string[] { "numStart", "numEnd" })]
		[return: Nullable(new byte[] { 0, 1, 1 })]
		private static ValueTuple<string, string>? TryParseNumberRange(ref AnalyzerConfig.SectionNameLexer lexer)
		{
			int position = lexer.Position;
			if (lexer.Lex() != AnalyzerConfig.TokenKind.OpenCurly)
			{
				lexer.Position = position;
				return default(ValueTuple<string, string>?);
			}
			string text = lexer.TryLexNumber();
			if (text == null)
			{
				lexer.Position = position;
				return default(ValueTuple<string, string>?);
			}
			char c;
			if (!lexer.TryEatCurrentCharacter(out c) || c != '.' || !lexer.TryEatCurrentCharacter(out c) || c != '.')
			{
				lexer.Position = position;
				return default(ValueTuple<string, string>?);
			}
			string text2 = lexer.TryLexNumber();
			if (text2 == null || lexer.IsDone || lexer.Lex() != AnalyzerConfig.TokenKind.CloseCurly)
			{
				lexer.Position = position;
				return default(ValueTuple<string, string>?);
			}
			return new ValueTuple<string, string>?(new ValueTuple<string, string>(text, text2));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001E080 File Offset: 0x0001C280
		[CompilerGenerated]
		internal static void <Parse>g__addNewSection|35_0(ref AnalyzerConfig.<>c__DisplayClass35_0 A_0)
		{
			AnalyzerConfig.Section section = new AnalyzerConfig.Section(A_0.activeSectionName, A_0.activeSectionProperties.ToImmutable());
			if (A_0.activeSectionName == "")
			{
				A_0.globalSection = section;
				return;
			}
			A_0.namedSectionBuilder.Add(section);
		}

		// Token: 0x040002CF RID: 719
		private static readonly Regex s_sectionMatcher = new Regex("^\\s*\\[(([^#;]|\\\\#|\\\\;)+)\\]\\s*([#;].*)?$", 8);

		// Token: 0x040002D0 RID: 720
		private static readonly Regex s_propertyMatcher = new Regex("^\\s*([\\w\\.\\-_]+)\\s*[=:]\\s*(.*?)\\s*([#;].*)?$", 8);

		// Token: 0x040002D1 RID: 721
		internal const string GlobalKey = "is_global";

		// Token: 0x040002D2 RID: 722
		internal const string GlobalLevelKey = "global_level";

		// Token: 0x040002D3 RID: 723
		internal const string UserGlobalConfigName = ".globalconfig";

		// Token: 0x040002DB RID: 731
		private readonly bool _hasGlobalFileName;

		// Token: 0x02000556 RID: 1366
		[Nullable(0)]
		internal sealed class Section
		{
			// Token: 0x17000ED2 RID: 3794
			// (get) Token: 0x060033FC RID: 13308 RVA: 0x00092D8B File Offset: 0x00090F8B
			public static StringComparison NameComparer { get; } = 4;

			// Token: 0x17000ED3 RID: 3795
			// (get) Token: 0x060033FD RID: 13309 RVA: 0x00092D92 File Offset: 0x00090F92
			public static IEqualityComparer<string> NameEqualityComparer { get; } = StringComparer.Ordinal;

			// Token: 0x17000ED4 RID: 3796
			// (get) Token: 0x060033FE RID: 13310 RVA: 0x00092D99 File Offset: 0x00090F99
			public static StringComparer PropertiesKeyComparer { get; } = CaseInsensitiveComparison.Comparer;

			// Token: 0x060033FF RID: 13311 RVA: 0x00092DA0 File Offset: 0x00090FA0
			public Section(string name, ImmutableDictionary<string, string> properties)
			{
				this.Name = name;
				this.Properties = properties;
			}

			// Token: 0x17000ED5 RID: 3797
			// (get) Token: 0x06003400 RID: 13312 RVA: 0x00092DB6 File Offset: 0x00090FB6
			public string Name { get; }

			// Token: 0x17000ED6 RID: 3798
			// (get) Token: 0x06003401 RID: 13313 RVA: 0x00092DBE File Offset: 0x00090FBE
			public ImmutableDictionary<string, string> Properties { get; }
		}

		// Token: 0x02000557 RID: 1367
		[Nullable(0)]
		internal readonly struct SectionNameMatcher
		{
			// Token: 0x17000ED7 RID: 3799
			// (get) Token: 0x06003403 RID: 13315 RVA: 0x00092DE2 File Offset: 0x00090FE2
			internal Regex Regex { get; }

			// Token: 0x06003404 RID: 13316 RVA: 0x00092DEA File Offset: 0x00090FEA
			[NullableContext(0)]
			internal SectionNameMatcher([Nullable(1)] Regex regex, [TupleElementNames(new string[] { "minValue", "maxValue" })] ImmutableArray<ValueTuple<int, int>> numberRangePairs)
			{
				this.Regex = regex;
				this._numberRangePairs = numberRangePairs;
			}

			// Token: 0x06003405 RID: 13317 RVA: 0x00092DFC File Offset: 0x00090FFC
			public bool IsMatch(string s)
			{
				if (this._numberRangePairs.IsEmpty)
				{
					return this.Regex.IsMatch(s);
				}
				Match match = this.Regex.Match(s);
				if (!match.Success)
				{
					return false;
				}
				for (int i = 0; i < this._numberRangePairs.Length; i++)
				{
					ValueTuple<int, int> valueTuple = this._numberRangePairs[i];
					int item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					int num;
					if (!int.TryParse(match.Groups[i + 1].Value, ref num) || num < item || num > item2)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0400152C RID: 5420
			[TupleElementNames(new string[] { "minValue", "maxValue" })]
			[Nullable(0)]
			private readonly ImmutableArray<ValueTuple<int, int>> _numberRangePairs;
		}

		// Token: 0x02000558 RID: 1368
		[Nullable(0)]
		private struct SectionNameLexer
		{
			// Token: 0x17000ED8 RID: 3800
			// (get) Token: 0x06003406 RID: 13318 RVA: 0x00092E9D File Offset: 0x0009109D
			// (set) Token: 0x06003407 RID: 13319 RVA: 0x00092EA5 File Offset: 0x000910A5
			public int Position { readonly get; set; }

			// Token: 0x06003408 RID: 13320 RVA: 0x00092EAE File Offset: 0x000910AE
			public SectionNameLexer(string sectionName)
			{
				this._sectionName = sectionName;
				this.Position = 0;
			}

			// Token: 0x17000ED9 RID: 3801
			// (get) Token: 0x06003409 RID: 13321 RVA: 0x00092EBE File Offset: 0x000910BE
			public bool IsDone
			{
				get
				{
					return this.Position >= this._sectionName.Length;
				}
			}

			// Token: 0x0600340A RID: 13322 RVA: 0x00092ED8 File Offset: 0x000910D8
			public AnalyzerConfig.TokenKind Lex()
			{
				int position = this.Position;
				char c = this._sectionName.get_Chars(this.Position);
				if (c <= '?')
				{
					if (c != '*')
					{
						if (c == ',')
						{
							int num = this.Position;
							this.Position = num + 1;
							return AnalyzerConfig.TokenKind.Comma;
						}
						if (c == '?')
						{
							int num = this.Position;
							this.Position = num + 1;
							return AnalyzerConfig.TokenKind.Question;
						}
					}
					else
					{
						int num2 = this.Position + 1;
						if (num2 < this._sectionName.Length && this._sectionName.get_Chars(num2) == '*')
						{
							this.Position += 2;
							return AnalyzerConfig.TokenKind.StarStar;
						}
						int num = this.Position;
						this.Position = num + 1;
						return AnalyzerConfig.TokenKind.Star;
					}
				}
				else if (c <= '\\')
				{
					if (c == '[')
					{
						int num = this.Position;
						this.Position = num + 1;
						return AnalyzerConfig.TokenKind.OpenBracket;
					}
					if (c == '\\')
					{
						int num = this.Position;
						this.Position = num + 1;
						if (this.IsDone)
						{
							return AnalyzerConfig.TokenKind.BadToken;
						}
						return AnalyzerConfig.TokenKind.SimpleCharacter;
					}
				}
				else
				{
					if (c == '{')
					{
						int num = this.Position;
						this.Position = num + 1;
						return AnalyzerConfig.TokenKind.OpenCurly;
					}
					if (c == '}')
					{
						int num = this.Position;
						this.Position = num + 1;
						return AnalyzerConfig.TokenKind.CloseCurly;
					}
				}
				return AnalyzerConfig.TokenKind.SimpleCharacter;
			}

			// Token: 0x17000EDA RID: 3802
			// (get) Token: 0x0600340B RID: 13323 RVA: 0x00093007 File Offset: 0x00091207
			public char CurrentCharacter
			{
				get
				{
					return this._sectionName.get_Chars(this.Position);
				}
			}

			// Token: 0x0600340C RID: 13324 RVA: 0x0009301C File Offset: 0x0009121C
			public char EatCurrentCharacter()
			{
				string sectionName = this._sectionName;
				int position = this.Position;
				this.Position = position + 1;
				return sectionName.get_Chars(position);
			}

			// Token: 0x0600340D RID: 13325 RVA: 0x00093045 File Offset: 0x00091245
			public bool TryEatCurrentCharacter(out char nextChar)
			{
				if (this.IsDone)
				{
					nextChar = '\0';
					return false;
				}
				nextChar = this.EatCurrentCharacter();
				return true;
			}

			// Token: 0x17000EDB RID: 3803
			public char this[int position]
			{
				get
				{
					return this._sectionName.get_Chars(position);
				}
			}

			// Token: 0x0600340F RID: 13327 RVA: 0x0009306C File Offset: 0x0009126C
			[NullableContext(2)]
			public string TryLexNumber()
			{
				bool flag = true;
				StringBuilder stringBuilder = new StringBuilder();
				while (!this.IsDone)
				{
					char currentCharacter = this.CurrentCharacter;
					if (flag && currentCharacter == '-')
					{
						int num = this.Position;
						this.Position = num + 1;
						stringBuilder.Append('-');
					}
					else
					{
						if (!char.IsDigit(currentCharacter))
						{
							break;
						}
						int num = this.Position;
						this.Position = num + 1;
						stringBuilder.Append(currentCharacter);
					}
					flag = false;
				}
				string text = stringBuilder.ToString();
				if (text.Length != 0 && !(text == "-"))
				{
					return text;
				}
				return null;
			}

			// Token: 0x0400152E RID: 5422
			private readonly string _sectionName;
		}

		// Token: 0x02000559 RID: 1369
		[NullableContext(0)]
		private enum TokenKind
		{
			// Token: 0x04001531 RID: 5425
			BadToken,
			// Token: 0x04001532 RID: 5426
			SimpleCharacter,
			// Token: 0x04001533 RID: 5427
			Star,
			// Token: 0x04001534 RID: 5428
			StarStar,
			// Token: 0x04001535 RID: 5429
			Question,
			// Token: 0x04001536 RID: 5430
			OpenCurly,
			// Token: 0x04001537 RID: 5431
			CloseCurly,
			// Token: 0x04001538 RID: 5432
			Comma,
			// Token: 0x04001539 RID: 5433
			DoubleDot,
			// Token: 0x0400153A RID: 5434
			OpenBracket
		}
	}
}
