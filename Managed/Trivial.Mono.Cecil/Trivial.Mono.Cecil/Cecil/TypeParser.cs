using System;
using System.Text;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000C1 RID: 193
	internal class TypeParser
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x00018935 File Offset: 0x00016B35
		private TypeParser(string fullname)
		{
			this.fullname = fullname;
			this.length = fullname.Length;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00018950 File Offset: 0x00016B50
		private TypeParser.Type ParseType(bool fq_name)
		{
			TypeParser.Type type = new TypeParser.Type();
			type.type_fullname = this.ParsePart();
			type.nested_names = this.ParseNestedNames();
			if (TypeParser.TryGetArity(type))
			{
				type.generic_arguments = this.ParseGenericArguments(type.arity);
			}
			type.specs = this.ParseSpecs();
			if (fq_name)
			{
				type.assembly = this.ParseAssemblyName();
			}
			return type;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000189B4 File Offset: 0x00016BB4
		private static bool TryGetArity(TypeParser.Type type)
		{
			int num = 0;
			TypeParser.TryAddArity(type.type_fullname, ref num);
			string[] nested_names = type.nested_names;
			if (!nested_names.IsNullOrEmpty<string>())
			{
				for (int i = 0; i < nested_names.Length; i++)
				{
					TypeParser.TryAddArity(nested_names[i], ref num);
				}
			}
			type.arity = num;
			return num > 0;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00018A04 File Offset: 0x00016C04
		private static bool TryGetArity(string name, out int arity)
		{
			arity = 0;
			int num = name.LastIndexOf('`');
			return num != -1 && TypeParser.ParseInt32(name.Substring(num + 1), out arity);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00018A32 File Offset: 0x00016C32
		private static bool ParseInt32(string value, out int result)
		{
			return int.TryParse(value, ref result);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00018A3C File Offset: 0x00016C3C
		private static void TryAddArity(string name, ref int arity)
		{
			int num;
			if (TypeParser.TryGetArity(name, out num))
			{
				arity += num;
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00018A5C File Offset: 0x00016C5C
		private string ParsePart()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (this.position < this.length && !TypeParser.IsDelimiter(this.fullname.get_Chars(this.position)))
			{
				if (this.fullname.get_Chars(this.position) == '\\')
				{
					this.position++;
				}
				StringBuilder stringBuilder2 = stringBuilder;
				string text = this.fullname;
				int num = this.position;
				this.position = num + 1;
				stringBuilder2.Append(text.get_Chars(num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00018AE3 File Offset: 0x00016CE3
		private static bool IsDelimiter(char chr)
		{
			return "+,[]*&".IndexOf(chr) != -1;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00018AF6 File Offset: 0x00016CF6
		private void TryParseWhiteSpace()
		{
			while (this.position < this.length && char.IsWhiteSpace(this.fullname.get_Chars(this.position)))
			{
				this.position++;
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00018B30 File Offset: 0x00016D30
		private string[] ParseNestedNames()
		{
			string[] array = null;
			while (this.TryParse('+'))
			{
				TypeParser.Add<string>(ref array, this.ParsePart());
			}
			return array;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00018B59 File Offset: 0x00016D59
		private bool TryParse(char chr)
		{
			if (this.position < this.length && this.fullname.get_Chars(this.position) == chr)
			{
				this.position++;
				return true;
			}
			return false;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00018B8E File Offset: 0x00016D8E
		private static void Add<T>(ref T[] array, T item)
		{
			array = array.Add(item);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00018B9C File Offset: 0x00016D9C
		private int[] ParseSpecs()
		{
			int[] array = null;
			while (this.position < this.length)
			{
				char c = this.fullname.get_Chars(this.position);
				if (c != '&')
				{
					if (c != '*')
					{
						if (c != '[')
						{
							return array;
						}
						this.position++;
						c = this.fullname.get_Chars(this.position);
						if (c != '*')
						{
							if (c == ']')
							{
								this.position++;
								TypeParser.Add<int>(ref array, -3);
							}
							else
							{
								int num = 1;
								while (this.TryParse(','))
								{
									num++;
								}
								TypeParser.Add<int>(ref array, num);
								this.TryParse(']');
							}
						}
						else
						{
							this.position++;
							TypeParser.Add<int>(ref array, 1);
						}
					}
					else
					{
						this.position++;
						TypeParser.Add<int>(ref array, -1);
					}
				}
				else
				{
					this.position++;
					TypeParser.Add<int>(ref array, -2);
				}
			}
			return array;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00018CA4 File Offset: 0x00016EA4
		private TypeParser.Type[] ParseGenericArguments(int arity)
		{
			TypeParser.Type[] array = null;
			if (this.position == this.length || this.fullname.get_Chars(this.position) != '[')
			{
				return array;
			}
			this.TryParse('[');
			for (int i = 0; i < arity; i++)
			{
				bool flag = this.TryParse('[');
				TypeParser.Add<TypeParser.Type>(ref array, this.ParseType(flag));
				if (flag)
				{
					this.TryParse(']');
				}
				this.TryParse(',');
				this.TryParseWhiteSpace();
			}
			this.TryParse(']');
			return array;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00018D2C File Offset: 0x00016F2C
		private string ParseAssemblyName()
		{
			if (!this.TryParse(','))
			{
				return string.Empty;
			}
			this.TryParseWhiteSpace();
			int num = this.position;
			while (this.position < this.length)
			{
				char c = this.fullname.get_Chars(this.position);
				if (c == '[' || c == ']')
				{
					break;
				}
				this.position++;
			}
			return this.fullname.Substring(num, this.position - num);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00018DA4 File Offset: 0x00016FA4
		public static TypeReference ParseType(ModuleDefinition module, string fullname)
		{
			if (string.IsNullOrEmpty(fullname))
			{
				return null;
			}
			TypeParser typeParser = new TypeParser(fullname);
			return TypeParser.GetTypeReference(module, typeParser.ParseType(true));
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00018DD0 File Offset: 0x00016FD0
		private static TypeReference GetTypeReference(ModuleDefinition module, TypeParser.Type type_info)
		{
			TypeReference typeReference;
			if (!TypeParser.TryGetDefinition(module, type_info, out typeReference))
			{
				typeReference = TypeParser.CreateReference(type_info, module, TypeParser.GetMetadataScope(module, type_info));
			}
			return TypeParser.CreateSpecs(typeReference, type_info);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00018E00 File Offset: 0x00017000
		private static TypeReference CreateSpecs(TypeReference type, TypeParser.Type type_info)
		{
			type = TypeParser.TryCreateGenericInstanceType(type, type_info);
			int[] specs = type_info.specs;
			if (specs.IsNullOrEmpty<int>())
			{
				return type;
			}
			for (int i = 0; i < specs.Length; i++)
			{
				switch (specs[i])
				{
				case -3:
					type = new ArrayType(type);
					break;
				case -2:
					type = new ByReferenceType(type);
					break;
				case -1:
					type = new PointerType(type);
					break;
				default:
				{
					ArrayType arrayType = new ArrayType(type);
					arrayType.Dimensions.Clear();
					for (int j = 0; j < specs[i]; j++)
					{
						arrayType.Dimensions.Add(default(ArrayDimension));
					}
					type = arrayType;
					break;
				}
				}
			}
			return type;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00018EAC File Offset: 0x000170AC
		private static TypeReference TryCreateGenericInstanceType(TypeReference type, TypeParser.Type type_info)
		{
			TypeParser.Type[] generic_arguments = type_info.generic_arguments;
			if (generic_arguments.IsNullOrEmpty<TypeParser.Type>())
			{
				return type;
			}
			GenericInstanceType genericInstanceType = new GenericInstanceType(type);
			Collection<TypeReference> genericArguments = genericInstanceType.GenericArguments;
			for (int i = 0; i < generic_arguments.Length; i++)
			{
				genericArguments.Add(TypeParser.GetTypeReference(type.Module, generic_arguments[i]));
			}
			return genericInstanceType;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00018EFC File Offset: 0x000170FC
		public static void SplitFullName(string fullname, out string @namespace, out string name)
		{
			int num = fullname.LastIndexOf('.');
			if (num == -1)
			{
				@namespace = string.Empty;
				name = fullname;
				return;
			}
			@namespace = fullname.Substring(0, num);
			name = fullname.Substring(num + 1);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00018F38 File Offset: 0x00017138
		private static TypeReference CreateReference(TypeParser.Type type_info, ModuleDefinition module, IMetadataScope scope)
		{
			string text;
			string text2;
			TypeParser.SplitFullName(type_info.type_fullname, out text, out text2);
			TypeReference typeReference = new TypeReference(text, text2, module, scope);
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference);
			TypeParser.AdjustGenericParameters(typeReference);
			string[] nested_names = type_info.nested_names;
			if (nested_names.IsNullOrEmpty<string>())
			{
				return typeReference;
			}
			for (int i = 0; i < nested_names.Length; i++)
			{
				typeReference = new TypeReference(string.Empty, nested_names[i], module, null)
				{
					DeclaringType = typeReference
				};
				TypeParser.AdjustGenericParameters(typeReference);
			}
			return typeReference;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00018FAC File Offset: 0x000171AC
		private static void AdjustGenericParameters(TypeReference type)
		{
			int num;
			if (TypeParser.TryGetArity(type.Name, out num))
			{
				for (int i = 0; i < num; i++)
				{
					type.GenericParameters.Add(new GenericParameter(type));
				}
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00018FE8 File Offset: 0x000171E8
		private static IMetadataScope GetMetadataScope(ModuleDefinition module, TypeParser.Type type_info)
		{
			if (string.IsNullOrEmpty(type_info.assembly))
			{
				return module.TypeSystem.CoreLibrary;
			}
			AssemblyNameReference assemblyNameReference = AssemblyNameReference.Parse(type_info.assembly);
			AssemblyNameReference assemblyNameReference2;
			if (!module.TryGetAssemblyNameReference(assemblyNameReference, out assemblyNameReference2))
			{
				return assemblyNameReference;
			}
			return assemblyNameReference2;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00019028 File Offset: 0x00017228
		private static bool TryGetDefinition(ModuleDefinition module, TypeParser.Type type_info, out TypeReference type)
		{
			type = null;
			if (!TypeParser.TryCurrentModule(module, type_info))
			{
				return false;
			}
			TypeDefinition typeDefinition = module.GetType(type_info.type_fullname);
			if (typeDefinition == null)
			{
				return false;
			}
			string[] nested_names = type_info.nested_names;
			if (!nested_names.IsNullOrEmpty<string>())
			{
				for (int i = 0; i < nested_names.Length; i++)
				{
					TypeDefinition nestedType = typeDefinition.GetNestedType(nested_names[i]);
					if (nestedType == null)
					{
						return false;
					}
					typeDefinition = nestedType;
				}
			}
			type = typeDefinition;
			return true;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00019087 File Offset: 0x00017287
		private static bool TryCurrentModule(ModuleDefinition module, TypeParser.Type type_info)
		{
			return string.IsNullOrEmpty(type_info.assembly) || (module.assembly != null && module.assembly.Name.FullName == type_info.assembly);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000190C0 File Offset: 0x000172C0
		public static string ToParseable(TypeReference type)
		{
			if (type == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			TypeParser.AppendType(type, stringBuilder, true, true);
			return stringBuilder.ToString();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000190E8 File Offset: 0x000172E8
		private static void AppendNamePart(string part, StringBuilder name)
		{
			for (int i = 0; i < part.Length; i++)
			{
				char c = part.get_Chars(i);
				if (TypeParser.IsDelimiter(c))
				{
					name.Append('\\');
				}
				name.Append(c);
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001912C File Offset: 0x0001732C
		private static void AppendType(TypeReference type, StringBuilder name, bool fq_name, bool top_level)
		{
			TypeReference elementType = type.GetElementType();
			TypeReference declaringType = elementType.DeclaringType;
			if (declaringType != null)
			{
				TypeParser.AppendType(declaringType, name, false, top_level);
				name.Append('+');
			}
			string @namespace = type.Namespace;
			if (!string.IsNullOrEmpty(@namespace))
			{
				TypeParser.AppendNamePart(@namespace, name);
				name.Append('.');
			}
			TypeParser.AppendNamePart(elementType.Name, name);
			if (fq_name)
			{
				if (type.IsTypeSpecification())
				{
					TypeParser.AppendTypeSpecification((TypeSpecification)type, name);
				}
				if (TypeParser.RequiresFullyQualifiedName(type, top_level))
				{
					name.Append(", ");
					name.Append(TypeParser.GetScopeFullName(type));
				}
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000191C0 File Offset: 0x000173C0
		private static string GetScopeFullName(TypeReference type)
		{
			IMetadataScope scope = type.Scope;
			MetadataScopeType metadataScopeType = scope.MetadataScopeType;
			if (metadataScopeType == MetadataScopeType.AssemblyNameReference)
			{
				return ((AssemblyNameReference)scope).FullName;
			}
			if (metadataScopeType != MetadataScopeType.ModuleDefinition)
			{
				throw new ArgumentException();
			}
			return ((ModuleDefinition)scope).Assembly.Name.FullName;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001920C File Offset: 0x0001740C
		private static void AppendTypeSpecification(TypeSpecification type, StringBuilder name)
		{
			if (type.ElementType.IsTypeSpecification())
			{
				TypeParser.AppendTypeSpecification((TypeSpecification)type.ElementType, name);
			}
			ElementType etype = type.etype;
			switch (etype)
			{
			case ElementType.Ptr:
				name.Append('*');
				return;
			case ElementType.ByRef:
				name.Append('&');
				return;
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.Var:
				return;
			case ElementType.Array:
				break;
			case ElementType.GenericInst:
			{
				Collection<TypeReference> genericArguments = ((GenericInstanceType)type).GenericArguments;
				name.Append('[');
				for (int i = 0; i < genericArguments.Count; i++)
				{
					if (i > 0)
					{
						name.Append(',');
					}
					TypeReference typeReference = genericArguments[i];
					bool flag = typeReference.Scope != typeReference.Module;
					if (flag)
					{
						name.Append('[');
					}
					TypeParser.AppendType(typeReference, name, true, false);
					if (flag)
					{
						name.Append(']');
					}
				}
				name.Append(']');
				return;
			}
			default:
				if (etype != ElementType.SzArray)
				{
					return;
				}
				break;
			}
			ArrayType arrayType = (ArrayType)type;
			if (arrayType.IsVector)
			{
				name.Append("[]");
				return;
			}
			name.Append('[');
			for (int j = 1; j < arrayType.Rank; j++)
			{
				name.Append(',');
			}
			name.Append(']');
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001934A File Offset: 0x0001754A
		private static bool RequiresFullyQualifiedName(TypeReference type, bool top_level)
		{
			return type.Scope != type.Module && (!(type.Scope.Name == "mscorlib") || !top_level);
		}

		// Token: 0x040002E5 RID: 741
		private readonly string fullname;

		// Token: 0x040002E6 RID: 742
		private readonly int length;

		// Token: 0x040002E7 RID: 743
		private int position;

		// Token: 0x0200014A RID: 330
		private class Type
		{
			// Token: 0x04000746 RID: 1862
			public const int Ptr = -1;

			// Token: 0x04000747 RID: 1863
			public const int ByRef = -2;

			// Token: 0x04000748 RID: 1864
			public const int SzArray = -3;

			// Token: 0x04000749 RID: 1865
			public string type_fullname;

			// Token: 0x0400074A RID: 1866
			public string[] nested_names;

			// Token: 0x0400074B RID: 1867
			public int arity;

			// Token: 0x0400074C RID: 1868
			public int[] specs;

			// Token: 0x0400074D RID: 1869
			public TypeParser.Type[] generic_arguments;

			// Token: 0x0400074E RID: 1870
			public string assembly;
		}
	}
}
