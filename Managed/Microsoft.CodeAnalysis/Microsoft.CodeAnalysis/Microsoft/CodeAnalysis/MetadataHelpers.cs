using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A4 RID: 420
	internal static class MetadataHelpers
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x00032728 File Offset: 0x00030928
		internal static MetadataHelpers.AssemblyQualifiedTypeName DecodeTypeName(string s)
		{
			MetadataHelpers.SerializedTypeDecoder serializedTypeDecoder = new MetadataHelpers.SerializedTypeDecoder(s);
			return serializedTypeDecoder.DecodeTypeName(false, false);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00032746 File Offset: 0x00030946
		internal static string GetAritySuffix(int arity)
		{
			if (arity > 9)
			{
				return "`" + arity.ToString(CultureInfo.InvariantCulture);
			}
			return MetadataHelpers.s_aritySuffixesOneToNine[arity - 1];
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003276D File Offset: 0x0003096D
		internal static string ComposeAritySuffixedMetadataName(string name, int arity)
		{
			if (arity != 0)
			{
				return name + MetadataHelpers.GetAritySuffix(arity);
			}
			return name;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00032780 File Offset: 0x00030980
		internal static int InferTypeArityFromMetadataName(string emittedTypeName)
		{
			int num;
			return (int)MetadataHelpers.InferTypeArityFromMetadataName(emittedTypeName, out num);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00032798 File Offset: 0x00030998
		private static short InferTypeArityFromMetadataName(string emittedTypeName, out int suffixStartsAt)
		{
			int length = emittedTypeName.Length;
			int num = length;
			while (num >= 1 && emittedTypeName.get_Chars(num - 1) != '`')
			{
				num--;
			}
			if (num < 2 || length - num == 0 || length - num > 22)
			{
				suffixStartsAt = -1;
				return 0;
			}
			string text = emittedTypeName.Substring(num);
			int num2;
			if (!int.TryParse(text, 0, CultureInfo.InvariantCulture, ref num2) || num2 < 0 || num2 > 32767 || text != num2.ToString())
			{
				suffixStartsAt = -1;
				return 0;
			}
			suffixStartsAt = num - 1;
			return (short)num2;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003281C File Offset: 0x00030A1C
		internal static string InferTypeArityAndUnmangleMetadataName(string emittedTypeName, out short arity)
		{
			int num;
			arity = MetadataHelpers.InferTypeArityFromMetadataName(emittedTypeName, out num);
			if (arity == 0)
			{
				return emittedTypeName;
			}
			return emittedTypeName.Substring(0, num);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00032844 File Offset: 0x00030A44
		internal static string UnmangleMetadataNameForArity(string emittedTypeName, int arity)
		{
			int num;
			if (arity == (int)MetadataHelpers.InferTypeArityFromMetadataName(emittedTypeName, out num))
			{
				return emittedTypeName.Substring(0, num);
			}
			return emittedTypeName;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00032868 File Offset: 0x00030A68
		internal static ImmutableArray<string> SplitQualifiedName(string name)
		{
			if (name.Length == 0)
			{
				return ImmutableArray<string>.Empty;
			}
			int i = 0;
			for (int j = 0; j < name.Length; j++)
			{
				if (name.get_Chars(j) == '.')
				{
					i++;
				}
			}
			if (i != 0)
			{
				ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(i + 1);
				int num = 0;
				int num2 = 0;
				while (i > 0)
				{
					if (name.get_Chars(num2) == '.')
					{
						int num3 = num2 - num;
						if (num3 == 6 && num == 0 && name.StartsWith("System", 4))
						{
							instance.Add("System");
						}
						else
						{
							instance.Add(name.Substring(num, num3));
						}
						i--;
						num = num2 + 1;
					}
					num2++;
				}
				instance.Add(name.Substring(num));
				return instance.ToImmutableAndFree();
			}
			if (!(name == "System"))
			{
				return ImmutableArray.Create<string>(name);
			}
			return MetadataHelpers.s_splitQualifiedNameSystem;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00032944 File Offset: 0x00030B44
		internal static string SplitQualifiedName(string pstrName, out string qualifier)
		{
			int num = 0;
			int num2 = -1;
			for (int i = 0; i < pstrName.Length; i++)
			{
				char c = pstrName.get_Chars(i);
				if (c != '.')
				{
					if (c != '<')
					{
						if (c == '>')
						{
							num--;
						}
					}
					else
					{
						num++;
					}
				}
				else if (num == 0 && (i == 0 || num2 < i - 1))
				{
					num2 = i;
				}
			}
			if (num2 < 0)
			{
				qualifier = string.Empty;
				return pstrName;
			}
			if (num2 == 6 && pstrName.StartsWith("System", 4))
			{
				qualifier = "System";
			}
			else
			{
				qualifier = pstrName.Substring(0, num2);
			}
			return pstrName.Substring(num2 + 1);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000329D4 File Offset: 0x00030BD4
		internal static string BuildQualifiedName(string qualifier, string name)
		{
			if (!string.IsNullOrEmpty(qualifier))
			{
				return qualifier + "." + name;
			}
			return name;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000329EC File Offset: 0x00030BEC
		public static void GetInfoForImmediateNamespaceMembers(bool isGlobalNamespace, int namespaceNameLength, IEnumerable<IGrouping<string, TypeDefinitionHandle>> typesByNS, StringComparer nameComparer, out IEnumerable<IGrouping<string, TypeDefinitionHandle>> types, out IEnumerable<KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>> namespaces)
		{
			List<IGrouping<string, TypeDefinitionHandle>> list = new List<IGrouping<string, TypeDefinitionHandle>>();
			List<KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>> list2 = new List<KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>>();
			bool flag = false;
			IEnumerator<IGrouping<string, TypeDefinitionHandle>> enumerator = typesByNS.GetEnumerator();
			using (enumerator)
			{
				if (enumerator.MoveNext())
				{
					IGrouping<string, TypeDefinitionHandle> grouping = enumerator.Current;
					string text = null;
					List<IGrouping<string, TypeDefinitionHandle>> list3 = null;
					while (grouping.Key.Length == namespaceNameLength)
					{
						list.Add(grouping);
						if (!enumerator.MoveNext())
						{
							goto IL_00F0;
						}
						grouping = enumerator.Current;
					}
					if (!isGlobalNamespace)
					{
						namespaceNameLength++;
					}
					do
					{
						grouping = enumerator.Current;
						string text2 = MetadataHelpers.ExtractSimpleNameOfChildNamespace(namespaceNameLength, grouping.Key);
						int num = nameComparer.Compare(text, text2);
						if (num == 0)
						{
							list3.Add(grouping);
						}
						else
						{
							if (num > 0)
							{
								flag = true;
							}
							if (list3 != null)
							{
								list2.Add(new KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>(text, list3));
							}
							list3 = new List<IGrouping<string, TypeDefinitionHandle>>();
							text = text2;
							list3.Add(grouping);
						}
					}
					while (enumerator.MoveNext());
					if (list3 != null)
					{
						list2.Add(new KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>(text, list3));
					}
				}
			}
			IL_00F0:
			types = list;
			if (flag)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(list2.Count, nameComparer);
				for (int i = list2.Count - 1; i >= 0; i--)
				{
					dictionary[list2[i].Key] = i;
				}
				if (dictionary.Count != list2.Count)
				{
					for (int j = 1; j < list2.Count; j++)
					{
						KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>> keyValuePair = list2[j];
						int num2 = dictionary[keyValuePair.Key];
						if (num2 != j)
						{
							KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>> keyValuePair2 = list2[num2];
							list2[num2] = KeyValuePairUtil.Create<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>(keyValuePair2.Key, Enumerable.Concat<IGrouping<string, TypeDefinitionHandle>>(keyValuePair2.Value, keyValuePair.Value));
							list2[j] = default(KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>);
						}
					}
					list2.RemoveAll((KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>> pair) => pair.Key == null);
				}
			}
			namespaces = list2;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00032BF8 File Offset: 0x00030DF8
		private static string ExtractSimpleNameOfChildNamespace(int parentNamespaceNameLength, string fullName)
		{
			int num = fullName.IndexOf('.', parentNamespaceNameLength);
			if (num < 0)
			{
				return fullName.Substring(parentNamespaceNameLength);
			}
			return fullName.Substring(parentNamespaceNameLength, num - parentNamespaceNameLength);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00032C25 File Offset: 0x00030E25
		internal static bool IsValidMetadataIdentifier(string str)
		{
			return !string.IsNullOrEmpty(str) && str.IsValidUnicodeString() && str.IndexOf('\0') == -1;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00032C43 File Offset: 0x00030E43
		internal static bool IsValidUnicodeString(string str)
		{
			return str == null || str.IsValidUnicodeString();
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00032C50 File Offset: 0x00030E50
		internal static bool IsValidAssemblyOrModuleName(string name)
		{
			return MetadataHelpers.GetAssemblyOrModuleNameErrorArgumentResourceName(name) == null;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00032C5C File Offset: 0x00030E5C
		internal static void CheckAssemblyOrModuleName(string name, CommonMessageProvider messageProvider, int code, DiagnosticBag diagnostics)
		{
			string assemblyOrModuleNameErrorArgumentResourceName = MetadataHelpers.GetAssemblyOrModuleNameErrorArgumentResourceName(name);
			if (assemblyOrModuleNameErrorArgumentResourceName != null)
			{
				diagnostics.Add(messageProvider.CreateDiagnostic(code, Location.None, new object[]
				{
					new CodeAnalysisResourcesLocalizableErrorArgument(assemblyOrModuleNameErrorArgumentResourceName)
				}));
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00032C9C File Offset: 0x00030E9C
		internal static void CheckAssemblyOrModuleName(string name, CommonMessageProvider messageProvider, int code, ArrayBuilder<Diagnostic> builder)
		{
			string assemblyOrModuleNameErrorArgumentResourceName = MetadataHelpers.GetAssemblyOrModuleNameErrorArgumentResourceName(name);
			if (assemblyOrModuleNameErrorArgumentResourceName != null)
			{
				builder.Add(messageProvider.CreateDiagnostic(code, Location.None, new object[]
				{
					new CodeAnalysisResourcesLocalizableErrorArgument(assemblyOrModuleNameErrorArgumentResourceName)
				}));
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00032CD9 File Offset: 0x00030ED9
		private static string GetAssemblyOrModuleNameErrorArgumentResourceName(string name)
		{
			if (name == null)
			{
				return "NameCannotBeNull";
			}
			if (name.Length == 0)
			{
				return "NameCannotBeEmpty";
			}
			if (char.IsWhiteSpace(name.get_Chars(0)))
			{
				return "NameCannotStartWithWhitespace";
			}
			if (!MetadataHelpers.IsValidMetadataFileName(name))
			{
				return "NameContainsInvalidCharacter";
			}
			return null;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00032D15 File Offset: 0x00030F15
		internal static bool IsValidMetadataFileName(string name)
		{
			return FileNameUtilities.IsFileName(name) && MetadataHelpers.IsValidMetadataIdentifier(name);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00032D27 File Offset: 0x00030F27
		internal static bool SplitNameEqualsFullyQualifiedName(string namespaceName, string typeName, string fullyQualified)
		{
			return fullyQualified.Length == namespaceName.Length + typeName.Length + 1 && fullyQualified.get_Chars(namespaceName.Length) == '.' && fullyQualified.StartsWith(namespaceName, 4) && fullyQualified.EndsWith(typeName, 4);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00032D64 File Offset: 0x00030F64
		internal static bool IsValidPublicKey(ImmutableArray<byte> bytes)
		{
			return CryptoBlobParser.IsValidPublicKey(bytes);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00032D6C File Offset: 0x00030F6C
		internal static string MangleForTypeNameIfNeeded(string moduleName)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			builder.Append(moduleName);
			builder.Replace("Q", "QQ");
			builder.Replace("_", "Q_");
			builder.Replace('.', '_');
			return instance.ToStringAndFree();
		}

		// Token: 0x0400052A RID: 1322
		public const char DotDelimiter = '.';

		// Token: 0x0400052B RID: 1323
		public const string DotDelimiterString = ".";

		// Token: 0x0400052C RID: 1324
		public const char GenericTypeNameManglingChar = '`';

		// Token: 0x0400052D RID: 1325
		private const string GenericTypeNameManglingString = "`";

		// Token: 0x0400052E RID: 1326
		public const int MaxStringLengthForParamSize = 22;

		// Token: 0x0400052F RID: 1327
		public const int MaxStringLengthForIntToStringConversion = 22;

		// Token: 0x04000530 RID: 1328
		public const string SystemString = "System";

		// Token: 0x04000531 RID: 1329
		public const char MangledNameRegionStartChar = '<';

		// Token: 0x04000532 RID: 1330
		public const char MangledNameRegionEndChar = '>';

		// Token: 0x04000533 RID: 1331
		private static readonly string[] s_aritySuffixesOneToNine = new string[] { "`1", "`2", "`3", "`4", "`5", "`6", "`7", "`8", "`9" };

		// Token: 0x04000534 RID: 1332
		private static readonly ImmutableArray<string> s_splitQualifiedNameSystem = ImmutableArray.Create<string>("System");

		// Token: 0x020005B4 RID: 1460
		internal struct AssemblyQualifiedTypeName
		{
			// Token: 0x06003605 RID: 13829 RVA: 0x0009847A File Offset: 0x0009667A
			internal AssemblyQualifiedTypeName(string topLevelType, string[] nestedTypes, MetadataHelpers.AssemblyQualifiedTypeName[] typeArguments, int pointerCount, int[] arrayRanks, string assemblyName)
			{
				this.TopLevelType = topLevelType;
				this.NestedTypes = nestedTypes;
				this.TypeArguments = typeArguments;
				this.PointerCount = pointerCount;
				this.ArrayRanks = arrayRanks;
				this.AssemblyName = assemblyName;
			}

			// Token: 0x0400166A RID: 5738
			internal readonly string TopLevelType;

			// Token: 0x0400166B RID: 5739
			internal readonly string[] NestedTypes;

			// Token: 0x0400166C RID: 5740
			internal readonly MetadataHelpers.AssemblyQualifiedTypeName[] TypeArguments;

			// Token: 0x0400166D RID: 5741
			internal readonly int PointerCount;

			// Token: 0x0400166E RID: 5742
			internal readonly int[] ArrayRanks;

			// Token: 0x0400166F RID: 5743
			internal readonly string AssemblyName;
		}

		// Token: 0x020005B5 RID: 1461
		private struct SerializedTypeDecoder
		{
			// Token: 0x06003606 RID: 13830 RVA: 0x000984A9 File Offset: 0x000966A9
			internal SerializedTypeDecoder(string s)
			{
				this._input = s;
				this._offset = 0;
			}

			// Token: 0x06003607 RID: 13831 RVA: 0x000984B9 File Offset: 0x000966B9
			private void Advance()
			{
				if (!this.EndOfInput)
				{
					this._offset++;
				}
			}

			// Token: 0x06003608 RID: 13832 RVA: 0x000984D1 File Offset: 0x000966D1
			private void AdvanceTo(int i)
			{
				if (i <= this._input.Length)
				{
					this._offset = i;
				}
			}

			// Token: 0x17000F49 RID: 3913
			// (get) Token: 0x06003609 RID: 13833 RVA: 0x000984E8 File Offset: 0x000966E8
			private bool EndOfInput
			{
				get
				{
					return this._offset >= this._input.Length;
				}
			}

			// Token: 0x17000F4A RID: 3914
			// (get) Token: 0x0600360A RID: 13834 RVA: 0x00098500 File Offset: 0x00096700
			private int Offset
			{
				get
				{
					return this._offset;
				}
			}

			// Token: 0x17000F4B RID: 3915
			// (get) Token: 0x0600360B RID: 13835 RVA: 0x00098508 File Offset: 0x00096708
			private char Current
			{
				get
				{
					return this._input.get_Chars(this._offset);
				}
			}

			// Token: 0x0600360C RID: 13836 RVA: 0x0009851C File Offset: 0x0009671C
			internal MetadataHelpers.AssemblyQualifiedTypeName DecodeTypeName(bool isTypeArgument = false, bool isTypeArgumentWithAssemblyName = false)
			{
				string text = null;
				ArrayBuilder<string> arrayBuilder = null;
				MetadataHelpers.AssemblyQualifiedTypeName[] array = null;
				int num = 0;
				ArrayBuilder<int> arrayBuilder2 = null;
				string text2 = null;
				bool flag = true;
				bool flag2 = false;
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				StringBuilder builder = instance.Builder;
				while (!this.EndOfInput)
				{
					int num2 = this._input.IndexOfAny(MetadataHelpers.SerializedTypeDecoder.s_typeNameDelimiters, this._offset);
					if (num2 < 0)
					{
						builder.Append(this.DecodeGenericName(this._input.Length));
						break;
					}
					char c = this._input.get_Chars(num2);
					string text3 = this.DecodeGenericName(num2);
					flag2 = flag2 || text3.IndexOf('`') >= 0;
					builder.Append(text3);
					switch (c)
					{
					case '*':
						if (arrayBuilder2 != null)
						{
							builder.Append(c);
						}
						else
						{
							num++;
						}
						this.Advance();
						break;
					case '+':
						if (arrayBuilder2 != null || num > 0)
						{
							builder.Append(c);
						}
						else
						{
							MetadataHelpers.SerializedTypeDecoder.HandleDecodedTypeName(builder.ToString(), flag, ref text, ref arrayBuilder);
							builder.Clear();
							flag = false;
						}
						this.Advance();
						break;
					case ',':
						if (!isTypeArgument || isTypeArgumentWithAssemblyName)
						{
							this.Advance();
							if (!this.EndOfInput && char.IsWhiteSpace(this.Current))
							{
								this.Advance();
							}
							text2 = this.DecodeAssemblyName(isTypeArgumentWithAssemblyName);
							goto IL_01BE;
						}
						goto IL_01BE;
					default:
						if (c != '[')
						{
							if (c != ']')
							{
								throw ExceptionUtilities.UnexpectedValue(c);
							}
							if (isTypeArgument)
							{
								goto IL_01BE;
							}
							builder.Append(c);
							this.Advance();
						}
						else if (flag2 && array == null)
						{
							this.Advance();
							if (arrayBuilder2 != null || num > 0)
							{
								builder.Append(c);
							}
							else
							{
								array = this.DecodeTypeArguments();
							}
						}
						else
						{
							this.DecodeArrayShape(builder, ref arrayBuilder2);
						}
						break;
					}
				}
				IL_01BE:
				MetadataHelpers.SerializedTypeDecoder.HandleDecodedTypeName(builder.ToString(), flag, ref text, ref arrayBuilder);
				instance.Free();
				return new MetadataHelpers.AssemblyQualifiedTypeName(text, (arrayBuilder != null) ? arrayBuilder.ToArrayAndFree() : null, array, num, (arrayBuilder2 != null) ? arrayBuilder2.ToArrayAndFree() : null, text2);
			}

			// Token: 0x0600360D RID: 13837 RVA: 0x00098724 File Offset: 0x00096924
			private static void HandleDecodedTypeName(string decodedTypeName, bool decodingTopLevelType, ref string topLevelType, ref ArrayBuilder<string> nestedTypesBuilder)
			{
				if (decodedTypeName.Length != 0)
				{
					if (decodingTopLevelType)
					{
						topLevelType = decodedTypeName;
						return;
					}
					if (nestedTypesBuilder == null)
					{
						nestedTypesBuilder = ArrayBuilder<string>.GetInstance();
					}
					nestedTypesBuilder.Add(decodedTypeName);
				}
			}

			// Token: 0x0600360E RID: 13838 RVA: 0x00098748 File Offset: 0x00096948
			private string DecodeGenericName(int i)
			{
				if (i - this._offset == 0)
				{
					return string.Empty;
				}
				int offset = this._offset;
				this.AdvanceTo(i);
				return this._input.Substring(offset, this._offset - offset);
			}

			// Token: 0x0600360F RID: 13839 RVA: 0x00098788 File Offset: 0x00096988
			private MetadataHelpers.AssemblyQualifiedTypeName[] DecodeTypeArguments()
			{
				if (this.EndOfInput)
				{
					return null;
				}
				ArrayBuilder<MetadataHelpers.AssemblyQualifiedTypeName> instance = ArrayBuilder<MetadataHelpers.AssemblyQualifiedTypeName>.GetInstance();
				while (!this.EndOfInput)
				{
					instance.Add(this.DecodeTypeArgument());
					if (!this.EndOfInput)
					{
						char c = this.Current;
						if (c != ',')
						{
							if (c != ']')
							{
								throw ExceptionUtilities.UnexpectedValue(this.EndOfInput);
							}
							this.Advance();
							return instance.ToArrayAndFree();
						}
						else
						{
							this.Advance();
							if (!this.EndOfInput && char.IsWhiteSpace(this.Current))
							{
								this.Advance();
							}
						}
					}
				}
				return instance.ToArrayAndFree();
			}

			// Token: 0x06003610 RID: 13840 RVA: 0x00098820 File Offset: 0x00096A20
			private MetadataHelpers.AssemblyQualifiedTypeName DecodeTypeArgument()
			{
				bool flag = false;
				if (this.Current == '[')
				{
					flag = true;
					this.Advance();
				}
				MetadataHelpers.AssemblyQualifiedTypeName assemblyQualifiedTypeName = this.DecodeTypeName(true, flag);
				if (flag && !this.EndOfInput && this.Current == ']')
				{
					this.Advance();
				}
				return assemblyQualifiedTypeName;
			}

			// Token: 0x06003611 RID: 13841 RVA: 0x00098864 File Offset: 0x00096A64
			private string DecodeAssemblyName(bool isTypeArgumentWithAssemblyName)
			{
				if (this.EndOfInput)
				{
					return null;
				}
				int num;
				if (isTypeArgumentWithAssemblyName)
				{
					num = this._input.IndexOf(']', this._offset);
					if (num < 0)
					{
						num = this._input.Length;
					}
				}
				else
				{
					num = this._input.Length;
				}
				string text = this._input.Substring(this._offset, num - this._offset);
				this.AdvanceTo(num);
				return text;
			}

			// Token: 0x06003612 RID: 13842 RVA: 0x000988D0 File Offset: 0x00096AD0
			private void DecodeArrayShape(StringBuilder typeNameBuilder, ref ArrayBuilder<int> arrayRanksBuilder)
			{
				int offset = this._offset;
				int num = 1;
				bool flag = false;
				this.Advance();
				while (!this.EndOfInput)
				{
					char c = this.Current;
					if (c != '*')
					{
						if (c == ',')
						{
							num++;
							this.Advance();
							continue;
						}
						if (c == ']')
						{
							if (arrayRanksBuilder == null)
							{
								arrayRanksBuilder = ArrayBuilder<int>.GetInstance();
							}
							arrayRanksBuilder.Add((num == 1 && !flag) ? 0 : num);
							this.Advance();
							return;
						}
					}
					else if (num == 1)
					{
						this.Advance();
						if (this.Current != ']')
						{
							typeNameBuilder.Append(this._input.Substring(offset, this._offset - offset));
							return;
						}
						flag = true;
						continue;
					}
					this.Advance();
					typeNameBuilder.Append(this._input.Substring(offset, this._offset - offset));
					return;
				}
				typeNameBuilder.Append(this._input.Substring(offset, this._offset - offset));
			}

			// Token: 0x04001670 RID: 5744
			private static readonly char[] s_typeNameDelimiters = new char[] { '+', ',', '[', ']', '*' };

			// Token: 0x04001671 RID: 5745
			private readonly string _input;

			// Token: 0x04001672 RID: 5746
			private int _offset;
		}
	}
}
