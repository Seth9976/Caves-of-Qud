using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000129 RID: 297
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CommandLineParser
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x0001F0CE File Offset: 0x0001D2CE
		internal CommandLineParser(CommonMessageProvider messageProvider, bool isScriptCommandLineParser)
		{
			this._messageProvider = messageProvider;
			this.IsScriptCommandLineParser = isScriptCommandLineParser;
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0001F0E4 File Offset: 0x0001D2E4
		internal CommonMessageProvider MessageProvider
		{
			get
			{
				return this._messageProvider;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000AD1 RID: 2769
		protected abstract string RegularFileExtension { get; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000AD2 RID: 2770
		protected abstract string ScriptFileExtension { get; }

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001F0EC File Offset: 0x0001D2EC
		internal virtual TextReader CreateTextFileReader(string fullPath)
		{
			return new StreamReader(new FileStream(fullPath, 3, 1, 1), true);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001F0FD File Offset: 0x0001D2FD
		internal virtual IEnumerable<string> EnumerateFiles([Nullable(2)] string directory, string fileNamePattern, SearchOption searchOption)
		{
			if (directory == null)
			{
				return SpecializedCollections.EmptyEnumerable<string>();
			}
			return Directory.EnumerateFiles(directory, fileNamePattern, searchOption);
		}

		// Token: 0x06000AD5 RID: 2773
		internal abstract CommandLineArguments CommonParse(IEnumerable<string> args, string baseDirectory, [Nullable(2)] string sdkDirectory, [Nullable(2)] string additionalReferenceDirectories);

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001F110 File Offset: 0x0001D310
		public CommandLineArguments Parse(IEnumerable<string> args, string baseDirectory, [Nullable(2)] string sdkDirectory, [Nullable(2)] string additionalReferenceDirectories)
		{
			return this.CommonParse(args, baseDirectory, sdkDirectory, additionalReferenceDirectories);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001F11D File Offset: 0x0001D31D
		private static bool IsOption(string arg)
		{
			return !string.IsNullOrEmpty(arg) && (arg.get_Chars(0) == '/' || arg.get_Chars(0) == '-');
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001F144 File Offset: 0x0001D344
		[NullableContext(2)]
		internal static bool TryParseOption([Nullable(1)] string arg, [NotNullWhen(true)] out string name, out string value)
		{
			if (!CommandLineParser.IsOption(arg))
			{
				name = null;
				value = null;
				return false;
			}
			if (arg == "-")
			{
				name = arg;
				value = null;
				return true;
			}
			int num = arg.IndexOf(':');
			if (arg.Length > 1 && arg.get_Chars(0) != '-')
			{
				int num2 = arg.IndexOf('/', 1);
				if (num2 > 0 && (num < 0 || num2 < num))
				{
					name = null;
					value = null;
					return false;
				}
			}
			if (num >= 0)
			{
				name = arg.Substring(1, num - 1);
				value = arg.Substring(num + 1);
			}
			else
			{
				name = arg.Substring(1);
				value = null;
			}
			name = name.ToLowerInvariant();
			return true;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		[return: Nullable(2)]
		internal ErrorLogOptions ParseErrorLogOptions(string arg, IList<Diagnostic> diagnostics, [Nullable(2)] string baseDirectory, out bool diagnosticAlreadyReported)
		{
			diagnosticAlreadyReported = false;
			IEnumerator<string> enumerator = CommandLineParser.ParseSeparatedStrings(arg, CommandLineParser.s_pathSeparators, 1).GetEnumerator();
			if (!enumerator.MoveNext() || string.IsNullOrEmpty(enumerator.Current))
			{
				return null;
			}
			string text = this.ParseGenericPathToFile(enumerator.Current, diagnostics, baseDirectory, true);
			if (text == null)
			{
				diagnosticAlreadyReported = true;
				return null;
			}
			SarifVersion sarifVersion = SarifVersion.Sarif1;
			if (enumerator.MoveNext() && !string.IsNullOrEmpty(enumerator.Current))
			{
				string text2 = enumerator.Current;
				string text3 = "version=";
				int length = text3.Length;
				if (text2.Length <= length || !text2.Substring(0, length).Equals(text3, 5) || !SarifVersionFacts.TryParse(text2.Substring(length), out sarifVersion))
				{
					return null;
				}
			}
			if (enumerator.MoveNext())
			{
				return null;
			}
			return new ErrorLogOptions(text, sarifVersion);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		[NullableContext(2)]
		internal static void ParseAndNormalizeFile([Nullable(1)] string unquoted, string baseDirectory, out string outputFileName, out string outputDirectory, [Nullable(1)] out string invalidPath)
		{
			outputFileName = null;
			outputDirectory = null;
			invalidPath = unquoted;
			string text = FileUtilities.ResolveRelativePath(unquoted, baseDirectory);
			if (text != null)
			{
				try
				{
					text = Path.GetFullPath(text);
					invalidPath = text;
					outputFileName = Path.GetFileName(text);
					outputDirectory = Path.GetDirectoryName(text);
				}
				catch (Exception)
				{
					text = null;
				}
				if (outputFileName != null)
				{
					outputFileName = CommandLineParser.RemoveTrailingSpacesAndDots(outputFileName);
				}
			}
			if (text == null || !MetadataHelpers.IsValidMetadataIdentifier(outputDirectory) || !MetadataHelpers.IsValidMetadataIdentifier(outputFileName))
			{
				outputFileName = null;
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001F324 File Offset: 0x0001D524
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		internal static string RemoveTrailingSpacesAndDots(string path)
		{
			if (path == null)
			{
				return path;
			}
			int length = path.Length;
			int i = length - 1;
			while (i >= 0)
			{
				char c = path.get_Chars(i);
				if (!char.IsWhiteSpace(c) && c != '.')
				{
					if (i != length - 1)
					{
						return path.Substring(0, i + 1);
					}
					return path;
				}
				else
				{
					i--;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001F378 File Offset: 0x0001D578
		[return: Nullable(new byte[] { 0, 0, 1, 1 })]
		protected ImmutableArray<KeyValuePair<string, string>> ParsePathMap(string pathMap, IList<Diagnostic> errors)
		{
			if (pathMap.IsEmpty())
			{
				return ImmutableArray<KeyValuePair<string, string>>.Empty;
			}
			ArrayBuilder<KeyValuePair<string, string>> instance = ArrayBuilder<KeyValuePair<string, string>>.GetInstance();
			foreach (string text in CommandLineParser.SplitWithDoubledSeparatorEscaping(pathMap, ','))
			{
				if (!text.IsEmpty())
				{
					string[] array2 = CommandLineParser.SplitWithDoubledSeparatorEscaping(text, '=');
					if (array2.Length != 2)
					{
						errors.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.ERR_InvalidPathMap, new object[] { text }));
					}
					else
					{
						string text2 = array2[0];
						string text3 = array2[1];
						if (text2.Length == 0 || text3.Length == 0)
						{
							errors.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.ERR_InvalidPathMap, new object[] { text }));
						}
						else
						{
							text2 = PathUtilities.EnsureTrailingSeparator(text2);
							text3 = PathUtilities.EnsureTrailingSeparator(text3);
							instance.Add(new KeyValuePair<string, string>(text2, text3));
						}
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001F46C File Offset: 0x0001D66C
		internal static string[] SplitWithDoubledSeparatorEscaping(string str, char separator)
		{
			if (str.Length == 0)
			{
				return Array.Empty<string>();
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
			PooledStringBuilder instance2 = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance2.Builder;
			int i = 0;
			while (i < str.Length)
			{
				char c = str.get_Chars(i++);
				if (c == separator)
				{
					if (i >= str.Length || str.get_Chars(i) != separator)
					{
						instance.Add(builder.ToString());
						builder.Clear();
						continue;
					}
					i++;
				}
				builder.Append(c);
			}
			instance.Add(builder.ToString());
			instance2.Free();
			return instance.ToArrayAndFree();
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001F508 File Offset: 0x0001D708
		[NullableContext(2)]
		internal void ParseOutputFile([Nullable(1)] string value, [Nullable(1)] IList<Diagnostic> errors, string baseDirectory, out string outputFileName, out string outputDirectory)
		{
			string text;
			CommandLineParser.ParseAndNormalizeFile(CommandLineParser.RemoveQuotesAndSlashes(value), baseDirectory, out outputFileName, out outputDirectory, out text);
			if (outputFileName == null || !MetadataHelpers.IsValidAssemblyOrModuleName(outputFileName))
			{
				errors.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.FTL_InvalidInputFileName, new object[] { text }));
				outputFileName = null;
				outputDirectory = baseDirectory;
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001F564 File Offset: 0x0001D764
		[return: Nullable(2)]
		internal string ParsePdbPath(string value, IList<Diagnostic> errors, [Nullable(2)] string baseDirectory)
		{
			string text = null;
			string text2;
			string text3;
			string text4;
			CommandLineParser.ParseAndNormalizeFile(CommandLineParser.RemoveQuotesAndSlashes(value), baseDirectory, out text2, out text3, out text4);
			if (text2 == null || PathUtilities.ChangeExtension(text2, null).Length == 0)
			{
				errors.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.FTL_InvalidInputFileName, new object[] { text4 }));
			}
			else
			{
				text = Path.ChangeExtension(Path.Combine(text3, text2), ".pdb");
			}
			return text;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
		[return: Nullable(2)]
		internal string ParseGenericPathToFile(string unquoted, IList<Diagnostic> errors, [Nullable(2)] string baseDirectory, bool generateDiagnostic = true)
		{
			string text = null;
			string text2;
			string text3;
			string text4;
			CommandLineParser.ParseAndNormalizeFile(unquoted, baseDirectory, out text2, out text3, out text4);
			if (string.IsNullOrWhiteSpace(text2))
			{
				if (generateDiagnostic)
				{
					errors.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.FTL_InvalidInputFileName, new object[] { text4 }));
				}
			}
			else
			{
				text = Path.Combine(text3, text2);
			}
			return text;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001F630 File Offset: 0x0001D830
		internal void FlattenArgs(IEnumerable<string> rawArguments, IList<Diagnostic> diagnostics, List<string> processedArgs, [Nullable(new byte[] { 2, 1 })] List<string> scriptArgsOpt, [Nullable(2)] string baseDirectory, [Nullable(new byte[] { 2, 1 })] List<string> responsePaths = null)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Stack<string> stack = new Stack<string>(Enumerable.Reverse<string>(rawArguments));
			while (stack.Count > 0)
			{
				string text = stack.Pop().TrimEnd(Array.Empty<char>());
				if (flag)
				{
					scriptArgsOpt.Add(text);
				}
				else
				{
					if (scriptArgsOpt != null)
					{
						if (flag2)
						{
							flag = true;
							scriptArgsOpt.Add(text);
							continue;
						}
						if (!flag3 && text == "--")
						{
							flag3 = true;
							processedArgs.Add(text);
							continue;
						}
					}
					if (!flag3 && text.StartsWith("@", 4))
					{
						string text2 = CommandLineParser.RemoveQuotesAndSlashes(text.Substring(1)).TrimEnd(null);
						string text3 = FileUtilities.ResolveRelativePath(text2, baseDirectory);
						if (text3 != null)
						{
							foreach (string text4 in Enumerable.Reverse<string>(this.ParseResponseFile(text3, diagnostics)))
							{
								if (!string.Equals(text4, "/noconfig", 5) && !string.Equals(text4, "-noconfig", 5))
								{
									stack.Push(text4);
								}
								else
								{
									diagnostics.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.WRN_NoConfigNotOnCommandLine));
								}
							}
							if (responsePaths != null)
							{
								string directoryName = PathUtilities.GetDirectoryName(text3);
								if (directoryName == null)
								{
									diagnostics.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.FTL_InvalidInputFileName, new object[] { text2 }));
								}
								else
								{
									responsePaths.Add(FileUtilities.NormalizeAbsolutePath(directoryName));
								}
							}
						}
						else
						{
							diagnostics.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.FTL_InvalidInputFileName, new object[] { text2 }));
						}
					}
					else
					{
						processedArgs.Add(text);
						flag2 |= flag3 || !CommandLineParser.IsOption(text);
					}
				}
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001F818 File Offset: 0x0001DA18
		[NullableContext(2)]
		internal static bool TryParseClientArgs([Nullable(1)] IEnumerable<string> args, [Nullable(new byte[] { 2, 1 })] out List<string> parsedArgs, out bool containsShared, out string keepAliveValue, out string pipeName, out string errorMessage)
		{
			containsShared = false;
			keepAliveValue = null;
			errorMessage = null;
			parsedArgs = null;
			pipeName = null;
			List<string> list = new List<string>();
			foreach (string text in args)
			{
				bool flag;
				string text2;
				if (CommandLineParser.<TryParseClientArgs>g__isClientArgsOption|26_0(text, "keepalive", out flag, out text2))
				{
					if (string.IsNullOrEmpty(text2))
					{
						errorMessage = CodeAnalysisResources.MissingKeepAlive;
						return false;
					}
					int num;
					if (!int.TryParse(text2, ref num))
					{
						errorMessage = CodeAnalysisResources.KeepAliveIsNotAnInteger;
						return false;
					}
					if (num < -1)
					{
						errorMessage = CodeAnalysisResources.KeepAliveIsTooSmall;
						return false;
					}
					keepAliveValue = text2;
				}
				else if (CommandLineParser.<TryParseClientArgs>g__isClientArgsOption|26_0(text, "shared", out flag, out text2))
				{
					if (flag)
					{
						if (string.IsNullOrEmpty(text2))
						{
							errorMessage = CodeAnalysisResources.SharedArgumentMissing;
							return false;
						}
						pipeName = text2;
					}
					containsShared = true;
				}
				else
				{
					list.Add(text);
				}
			}
			if (keepAliveValue != null && !containsShared)
			{
				errorMessage = CodeAnalysisResources.KeepAliveWithoutShared;
				return false;
			}
			parsedArgs = list;
			return true;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0001F924 File Offset: 0x0001DB24
		internal static string MismatchedVersionErrorText
		{
			get
			{
				return CodeAnalysisResources.MismatchedVersion;
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001F92C File Offset: 0x0001DB2C
		internal IEnumerable<string> ParseResponseFile(string fullPath, IList<Diagnostic> errors)
		{
			List<string> list = new List<string>();
			try
			{
				using (TextReader textReader = this.CreateTextFileReader(fullPath))
				{
					string text;
					while ((text = textReader.ReadLine()) != null)
					{
						list.Add(text);
					}
				}
			}
			catch (Exception)
			{
				errors.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.ERR_OpenResponseFile, new object[] { fullPath }));
				return SpecializedCollections.EmptyEnumerable<string>();
			}
			return CommandLineParser.ParseResponseLines(list);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001F9BC File Offset: 0x0001DBBC
		internal static IEnumerable<string> ParseResponseLines(IEnumerable<string> lines)
		{
			List<string> list = new List<string>();
			foreach (string text in lines)
			{
				list.AddRange(CommandLineParser.SplitCommandLineIntoArguments(text, true));
			}
			return list;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001FA14 File Offset: 0x0001DC14
		[NullableContext(2)]
		internal static void ParseResourceDescription([Nullable(1)] string resourceDescriptor, string baseDirectory, bool skipLeadingSeparators, out string filePath, out string fullPath, out string fileName, [Nullable(1)] out string resourceName, out string accessibility)
		{
			filePath = null;
			fullPath = null;
			fileName = null;
			resourceName = "";
			accessibility = null;
			string[] array = Enumerable.ToArray<string>(CommandLineParser.ParseSeparatedStrings(resourceDescriptor, CommandLineParser.s_resourceSeparators, 0));
			int num = 0;
			int num2 = array.Length;
			if (skipLeadingSeparators)
			{
				while (num < num2 && string.IsNullOrEmpty(array[num]))
				{
					num++;
				}
				num2 -= num;
			}
			if (num2 >= 1)
			{
				filePath = CommandLineParser.RemoveQuotesAndSlashes(array[num]);
			}
			if (num2 >= 2)
			{
				resourceName = CommandLineParser.RemoveQuotesAndSlashes(array[num + 1]);
			}
			if (num2 >= 3)
			{
				accessibility = CommandLineParser.RemoveQuotesAndSlashes(array[num + 2]);
			}
			if (RoslynString.IsNullOrWhiteSpace(filePath))
			{
				return;
			}
			fileName = PathUtilities.GetFileName(filePath, true);
			fullPath = FileUtilities.ResolveRelativePath(filePath, baseDirectory);
			if (RoslynString.IsNullOrWhiteSpace(resourceName))
			{
				resourceName = fileName;
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001FACB File Offset: 0x0001DCCB
		public static IEnumerable<string> SplitCommandLineIntoArguments(string commandLine, bool removeHashComments)
		{
			return CommandLineUtilities.SplitCommandLineIntoArguments(commandLine, removeHashComments);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		[NullableContext(2)]
		[return: NotNullIfNotNull("arg")]
		internal static string RemoveQuotesAndSlashes(string arg)
		{
			if (arg == null)
			{
				return arg;
			}
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			int i = 0;
			while (i < arg.Length)
			{
				char c = arg.get_Chars(i);
				if (c != '"')
				{
					if (c == '\\')
					{
						CommandLineParser.ProcessSlashes(builder, arg, ref i);
					}
					else
					{
						builder.Append(c);
						i++;
					}
				}
				else
				{
					i++;
				}
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001FB38 File Offset: 0x0001DD38
		internal static void ProcessSlashes(StringBuilder builder, string arg, ref int i)
		{
			int j = 0;
			while (i < arg.Length && arg.get_Chars(i) == '\\')
			{
				j++;
				i++;
			}
			if (i < arg.Length && arg.get_Chars(i) == '"')
			{
				while (j >= 2)
				{
					builder.Append('\\');
					j -= 2;
				}
				if (j == 1)
				{
					builder.Append('"');
				}
				i++;
				return;
			}
			while (j > 0)
			{
				builder.Append('\\');
				j--;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001FBB7 File Offset: 0x0001DDB7
		private static IEnumerable<string> Split([Nullable(2)] string str, Func<char, bool> splitHere)
		{
			if (str == null)
			{
				yield break;
			}
			int num = 0;
			int num2;
			for (int c = 0; c < str.Length; c = num2 + 1)
			{
				if (splitHere.Invoke(str.get_Chars(c)))
				{
					yield return str.Substring(num, c - num);
					num = c + 1;
				}
				num2 = c;
			}
			yield return str.Substring(num);
			yield break;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001FBCE File Offset: 0x0001DDCE
		internal static IEnumerable<string> ParseSeparatedPaths([Nullable(2)] string str)
		{
			return Enumerable.Select<string, string>(CommandLineParser.ParseSeparatedStrings(str, CommandLineParser.s_pathSeparators, 1), new Func<string, string>(CommandLineParser.RemoveQuotesAndSlashes));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
		internal static IEnumerable<string> ParseSeparatedStrings([Nullable(2)] string str, char[] separators, StringSplitOptions options = 0)
		{
			bool inQuotes = false;
			IEnumerable<string> enumerable = CommandLineParser.Split(str, delegate(char c)
			{
				if (c == '"')
				{
					inQuotes = !inQuotes;
				}
				return !inQuotes && separators.Contains(c, null);
			});
			if (options != 1)
			{
				return enumerable;
			}
			return Enumerable.Where<string>(enumerable, (string s) => s.Length > 0);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001FC51 File Offset: 0x0001DE51
		internal IEnumerable<string> ResolveRelativePaths(IEnumerable<string> paths, string baseDirectory, IList<Diagnostic> errors)
		{
			foreach (string text in paths)
			{
				string text2 = FileUtilities.ResolveRelativePath(text, baseDirectory);
				if (text2 == null)
				{
					errors.Add(Diagnostic.Create(this._messageProvider, this._messageProvider.FTL_InvalidInputFileName, new object[] { text }));
				}
				else
				{
					yield return text2;
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001FC78 File Offset: 0x0001DE78
		private protected CommandLineSourceFile ToCommandLineSourceFile(string resolvedPath, bool isInputRedirected = false)
		{
			string extension = PathUtilities.GetExtension(resolvedPath);
			bool flag = this.IsScriptCommandLineParser && !string.Equals(extension, this.RegularFileExtension, 5);
			return new CommandLineSourceFile(resolvedPath, flag, isInputRedirected);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001FCB1 File Offset: 0x0001DEB1
		internal IEnumerable<string> ParseFileArgument(string arg, [Nullable(2)] string baseDirectory, IList<Diagnostic> errors)
		{
			string text = CommandLineParser.RemoveQuotesAndSlashes(arg);
			if (text.IndexOfAny(CommandLineParser.s_wildcards) != -1)
			{
				foreach (string text2 in this.ExpandFileNamePattern(text, baseDirectory, 0, errors))
				{
					yield return text2;
				}
				IEnumerator<string> enumerator = null;
			}
			else
			{
				string text3 = FileUtilities.ResolveRelativePath(text, baseDirectory);
				if (text3 == null)
				{
					errors.Add(Diagnostic.Create(this.MessageProvider, this.MessageProvider.FTL_InvalidInputFileName, new object[] { text }));
				}
				else
				{
					yield return text3;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001FCD6 File Offset: 0x0001DED6
		private protected IEnumerable<string> ParseSeparatedFileArgument(string value, [Nullable(2)] string baseDirectory, IList<Diagnostic> errors)
		{
			foreach (string text in Enumerable.Where<string>(CommandLineParser.ParseSeparatedPaths(value), (string path) => !string.IsNullOrWhiteSpace(path)))
			{
				foreach (string text2 in this.ParseFileArgument(text, baseDirectory, errors))
				{
					yield return text2;
				}
				IEnumerator<string> enumerator2 = null;
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001FCFB File Offset: 0x0001DEFB
		internal IEnumerable<CommandLineSourceFile> ParseRecurseArgument(string arg, [Nullable(2)] string baseDirectory, IList<Diagnostic> errors)
		{
			foreach (string text in this.ExpandFileNamePattern(arg, baseDirectory, 1, errors))
			{
				yield return this.ToCommandLineSourceFile(text, false);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001FD20 File Offset: 0x0001DF20
		[return: Nullable(2)]
		internal static Encoding TryParseEncodingName(string arg)
		{
			long num;
			if (!string.IsNullOrWhiteSpace(arg) && long.TryParse(arg, 0, CultureInfo.InvariantCulture, ref num) && num > 0L)
			{
				try
				{
					return Encoding.GetEncoding((int)num);
				}
				catch (Exception)
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001FD6C File Offset: 0x0001DF6C
		internal static SourceHashAlgorithm TryParseHashAlgorithmName(string arg)
		{
			if (string.Equals("sha1", arg, 5))
			{
				return SourceHashAlgorithm.Sha1;
			}
			if (string.Equals("sha256", arg, 5))
			{
				return SourceHashAlgorithm.Sha256;
			}
			return SourceHashAlgorithm.None;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001FD8F File Offset: 0x0001DF8F
		private IEnumerable<string> ExpandFileNamePattern(string path, [Nullable(2)] string baseDirectory, SearchOption searchOption, IList<Diagnostic> errors)
		{
			string directoryName = PathUtilities.GetDirectoryName(path);
			string pattern = PathUtilities.GetFileName(path, true);
			string resolvedDirectoryPath = (string.IsNullOrEmpty(directoryName) ? baseDirectory : FileUtilities.ResolveRelativePath(directoryName, baseDirectory));
			IEnumerator<string> enumerator = null;
			try
			{
				bool yielded = false;
				pattern = pattern.Trim(CommandLineParser.s_searchPatternTrimChars);
				if (!string.Equals(pattern, ".", 4))
				{
					for (;;)
					{
						string text = null;
						try
						{
							if (enumerator == null)
							{
								enumerator = this.EnumerateFiles(resolvedDirectoryPath, pattern, searchOption).GetEnumerator();
							}
							if (!enumerator.MoveNext())
							{
								goto IL_016D;
							}
							text = enumerator.Current;
						}
						catch
						{
							text = null;
						}
						if (text != null)
						{
							text = FileUtilities.ResolveRelativePath(text, baseDirectory);
						}
						if (text == null)
						{
							break;
						}
						yielded = true;
						yield return text;
					}
					errors.Add(Diagnostic.Create(this.MessageProvider, this.MessageProvider.FTL_InvalidInputFileName, new object[] { path }));
				}
				IL_016D:
				if (!yielded)
				{
					if (searchOption == 1)
					{
						this.GenerateErrorForNoFilesFoundInRecurse(path, errors);
					}
					else
					{
						errors.Add(Diagnostic.Create(this.MessageProvider, this.MessageProvider.ERR_FileNotFound, new object[] { path }));
					}
				}
			}
			finally
			{
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000AF5 RID: 2805
		internal abstract void GenerateErrorForNoFilesFoundInRecurse(string path, IList<Diagnostic> errors);

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		internal ReportDiagnostic GetDiagnosticOptionsFromRulesetFile([Nullable(2)] string fullPath, out Dictionary<string, ReportDiagnostic> diagnosticOptions, IList<Diagnostic> diagnostics)
		{
			return RuleSet.GetDiagnosticOptionsFromRulesetFile(fullPath, out diagnosticOptions, diagnostics, this._messageProvider);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001FDCC File Offset: 0x0001DFCC
		[NullableContext(2)]
		internal static bool TryParseUInt64(string value, out ulong result)
		{
			result = 0UL;
			if (RoslynString.IsNullOrEmpty(value))
			{
				return false;
			}
			int num = 10;
			if (value.StartsWith("0x", 5))
			{
				num = 16;
			}
			else if (value.StartsWith("0", 5))
			{
				num = 8;
			}
			try
			{
				result = Convert.ToUInt64(value, num);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001FE34 File Offset: 0x0001E034
		[NullableContext(2)]
		internal static bool TryParseUInt16(string value, out ushort result)
		{
			result = 0;
			if (RoslynString.IsNullOrEmpty(value))
			{
				return false;
			}
			int num = 10;
			if (value.StartsWith("0x", 5))
			{
				num = 16;
			}
			else if (value.StartsWith("0", 5))
			{
				num = 8;
			}
			try
			{
				result = Convert.ToUInt16(value, num);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001FE98 File Offset: 0x0001E098
		internal static ImmutableDictionary<string, string> ParseFeatures(List<string> features)
		{
			ImmutableDictionary<string, string>.Builder builder = ImmutableDictionary.CreateBuilder<string, string>();
			CompilerOptionParseUtilities.ParseFeatures(builder, features);
			return builder.ToImmutable();
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001FEAB File Offset: 0x0001E0AB
		[return: Nullable(new byte[] { 0, 0, 1, 1 })]
		internal static ImmutableArray<KeyValuePair<string, string>> SortPathMap([Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<KeyValuePair<string, string>> pathMap)
		{
			return pathMap.Sort(([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<string, string> x, [Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<string, string> y) => -x.Key.Length.CompareTo(y.Key.Length));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001FF34 File Offset: 0x0001E134
		[CompilerGenerated]
		internal static bool <TryParseClientArgs>g__isClientArgsOption|26_0(string arg, string optionName, out bool hasValue, [Nullable(2)] out string optionValue)
		{
			hasValue = false;
			optionValue = null;
			if (arg.Length == 0 || (arg.get_Chars(0) != '/' && arg.get_Chars(0) != '-'))
			{
				return false;
			}
			arg = arg.Substring(1);
			if (!arg.StartsWith(optionName, 5))
			{
				return false;
			}
			if (arg.Length > optionName.Length)
			{
				if (arg.get_Chars(optionName.Length) != ':' && arg.get_Chars(optionName.Length) != '=')
				{
					return false;
				}
				hasValue = true;
				optionValue = arg.Substring(optionName.Length + 1).Trim(new char[] { '"' });
			}
			return true;
		}

		// Token: 0x04000319 RID: 793
		private readonly CommonMessageProvider _messageProvider;

		// Token: 0x0400031A RID: 794
		internal readonly bool IsScriptCommandLineParser;

		// Token: 0x0400031B RID: 795
		private static readonly char[] s_searchPatternTrimChars = new char[] { '\t', '\n', '\v', '\f', '\r', ' ', '\u0085', '\u00a0' };

		// Token: 0x0400031C RID: 796
		internal const string ErrorLogOptionFormat = "<file>[,version={1|1.0|2|2.1}]";

		// Token: 0x0400031D RID: 797
		private static readonly char[] s_resourceSeparators = new char[] { ',' };

		// Token: 0x0400031E RID: 798
		private static readonly char[] s_pathSeparators = new char[] { ';', ',' };

		// Token: 0x0400031F RID: 799
		private static readonly char[] s_wildcards = new char[] { '*', '?' };
	}
}
