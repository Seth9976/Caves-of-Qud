using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000217 RID: 535
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class AttributeData
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00043263 File Offset: 0x00041463
		public INamedTypeSymbol AttributeClass
		{
			get
			{
				return this.CommonAttributeClass;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001503 RID: 5379
		protected abstract INamedTypeSymbol CommonAttributeClass { get; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0004326B File Offset: 0x0004146B
		public IMethodSymbol AttributeConstructor
		{
			get
			{
				return this.CommonAttributeConstructor;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001505 RID: 5381
		protected abstract IMethodSymbol CommonAttributeConstructor { get; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00043273 File Offset: 0x00041473
		public SyntaxReference ApplicationSyntaxReference
		{
			get
			{
				return this.CommonApplicationSyntaxReference;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001507 RID: 5383
		protected abstract SyntaxReference CommonApplicationSyntaxReference { get; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0004327B File Offset: 0x0004147B
		[Nullable(0)]
		public ImmutableArray<TypedConstant> ConstructorArguments
		{
			[NullableContext(0)]
			get
			{
				return this.CommonConstructorArguments;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001509 RID: 5385
		[Nullable(0)]
		protected internal abstract ImmutableArray<TypedConstant> CommonConstructorArguments
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00043283 File Offset: 0x00041483
		[Nullable(new byte[] { 0, 0, 1 })]
		public ImmutableArray<KeyValuePair<string, TypedConstant>> NamedArguments
		{
			[return: Nullable(new byte[] { 0, 0, 1 })]
			get
			{
				return this.CommonNamedArguments;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600150B RID: 5387
		[Nullable(new byte[] { 0, 0, 1 })]
		protected internal abstract ImmutableArray<KeyValuePair<string, TypedConstant>> CommonNamedArguments
		{
			[return: Nullable(new byte[] { 0, 0, 1 })]
			get;
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0004328B File Offset: 0x0004148B
		internal virtual bool IsConditionallyOmitted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0004328E File Offset: 0x0004148E
		[MemberNotNullWhen(true, new string[] { "AttributeClass", "AttributeConstructor" })]
		internal virtual bool HasErrors
		{
			[MemberNotNullWhen(true, new string[] { "AttributeClass", "AttributeConstructor" })]
			get
			{
				return false;
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00043294 File Offset: 0x00041494
		[NullableContext(1)]
		internal static bool IsTargetEarlyAttribute(INamedTypeSymbolInternal attributeType, int attributeArgCount, AttributeDescription description)
		{
			ISymbolInternal containingSymbol = attributeType.ContainingSymbol;
			if (containingSymbol == null || containingSymbol.Kind != SymbolKind.Namespace)
			{
				return false;
			}
			int num = description.Signatures.Length;
			for (int i = 0; i < num; i++)
			{
				int parameterCount = description.GetParameterCount(i);
				if (attributeArgCount == parameterCount)
				{
					StringComparison stringComparison = (description.MatchIgnoringCase ? 5 : 4);
					return attributeType.Name.Equals(description.Name, stringComparison) && AttributeData.<IsTargetEarlyAttribute>g__namespaceMatch|25_0(attributeType.ContainingNamespace, description.Namespace, stringComparison);
				}
			}
			return false;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00043318 File Offset: 0x00041518
		internal T GetConstructorArgument<T>(int i, SpecialType specialType)
		{
			return this.CommonConstructorArguments[i].DecodeValue<T>(specialType);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0004333D File Offset: 0x0004153D
		internal T DecodeNamedArgument<T>([Nullable(1)] string name, SpecialType specialType, T defaultValue = default(T))
		{
			return AttributeData.DecodeNamedArgument<T>(this.CommonNamedArguments, name, specialType, defaultValue);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00043350 File Offset: 0x00041550
		private static T DecodeNamedArgument<T>([Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments, [Nullable(1)] string name, SpecialType specialType, T defaultValue = default(T))
		{
			int num = AttributeData.IndexOfNamedArgument(namedArguments, name);
			if (num < 0)
			{
				return defaultValue;
			}
			return namedArguments[num].Value.DecodeValue<T>(specialType);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00043384 File Offset: 0x00041584
		[NullableContext(1)]
		private static int IndexOfNamedArgument([Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments, string name)
		{
			for (int i = namedArguments.Length - 1; i >= 0; i--)
			{
				if (string.Equals(namedArguments[i].Key, name, 4))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x000433C4 File Offset: 0x000415C4
		[NullableContext(1)]
		internal ConstantValue DecodeDecimalConstantValue()
		{
			ImmutableArray<IParameterSymbol> parameters = this.AttributeConstructor.Parameters;
			ImmutableArray<TypedConstant> commonConstructorArguments = this.CommonConstructorArguments;
			byte b = commonConstructorArguments[0].DecodeValue<byte>(SpecialType.System_Byte);
			bool flag = commonConstructorArguments[1].DecodeValue<byte>(SpecialType.System_Byte) > 0;
			int num;
			int num2;
			int num3;
			if (parameters[2].Type.SpecialType == SpecialType.System_Int32)
			{
				num = commonConstructorArguments[2].DecodeValue<int>(SpecialType.System_Int32);
				num2 = commonConstructorArguments[3].DecodeValue<int>(SpecialType.System_Int32);
				num3 = commonConstructorArguments[4].DecodeValue<int>(SpecialType.System_Int32);
			}
			else
			{
				num = (int)commonConstructorArguments[2].DecodeValue<uint>(SpecialType.System_UInt32);
				num2 = (int)commonConstructorArguments[3].DecodeValue<uint>(SpecialType.System_UInt32);
				num3 = (int)commonConstructorArguments[4].DecodeValue<uint>(SpecialType.System_UInt32);
			}
			return ConstantValue.Create(new decimal(num3, num2, num, flag, b));
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x000434B8 File Offset: 0x000416B8
		[NullableContext(1)]
		internal ConstantValue DecodeDateTimeConstantValue()
		{
			long num = this.CommonConstructorArguments[0].DecodeValue<long>(SpecialType.System_Int64);
			long num2 = num;
			DateTime dateTime = DateTime.MinValue;
			if (num2 >= dateTime.Ticks)
			{
				long num3 = num;
				dateTime = DateTime.MaxValue;
				if (num3 <= dateTime.Ticks)
				{
					return ConstantValue.Create(new DateTime(num));
				}
			}
			return ConstantValue.Bad;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00043510 File Offset: 0x00041710
		[NullableContext(1)]
		internal ObsoleteAttributeData DecodeObsoleteAttribute(ObsoleteAttributeKind kind)
		{
			switch (kind)
			{
			case ObsoleteAttributeKind.Obsolete:
				return this.DecodeObsoleteAttribute();
			case ObsoleteAttributeKind.Deprecated:
				return this.DecodeDeprecatedAttribute();
			case ObsoleteAttributeKind.Experimental:
				return this.DecodeExperimentalAttribute();
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00043548 File Offset: 0x00041748
		[NullableContext(1)]
		private ObsoleteAttributeData DecodeObsoleteAttribute()
		{
			ImmutableArray<TypedConstant> commonConstructorArguments = this.CommonConstructorArguments;
			string text = null;
			bool flag = false;
			if (commonConstructorArguments.Length > 0)
			{
				TypedConstant typedConstant = commonConstructorArguments[0];
				text = (string)typedConstant.ValueInternal;
				if (commonConstructorArguments.Length == 2)
				{
					typedConstant = commonConstructorArguments[1];
					flag = (bool)typedConstant.ValueInternal;
				}
			}
			string text2 = null;
			string text3 = null;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in this.CommonNamedArguments)
			{
				TypedConstant typedConstant;
				string text4;
				keyValuePair.Deconstruct<string, TypedConstant>(out text4, out typedConstant);
				string text5 = text4;
				TypedConstant typedConstant2 = typedConstant;
				if (text2 == null && text5 == "DiagnosticId" && this.IsStringProperty("DiagnosticId"))
				{
					text2 = typedConstant2.ValueInternal as string;
				}
				else if (text3 == null && text5 == "UrlFormat" && this.IsStringProperty("UrlFormat"))
				{
					text3 = typedConstant2.ValueInternal as string;
				}
				if (text2 != null && text3 != null)
				{
					break;
				}
			}
			return new ObsoleteAttributeData(ObsoleteAttributeKind.Obsolete, text, flag, text2, text3);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0004364A File Offset: 0x0004184A
		[NullableContext(1)]
		private protected virtual bool IsStringProperty(string memberName)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00043654 File Offset: 0x00041854
		[NullableContext(1)]
		private ObsoleteAttributeData DecodeDeprecatedAttribute()
		{
			ImmutableArray<TypedConstant> commonConstructorArguments = this.CommonConstructorArguments;
			string text = null;
			bool flag = false;
			if (commonConstructorArguments.Length == 3 || commonConstructorArguments.Length == 4)
			{
				text = (string)commonConstructorArguments[0].ValueInternal;
				flag = (int)commonConstructorArguments[1].ValueInternal == 1;
			}
			return new ObsoleteAttributeData(ObsoleteAttributeKind.Deprecated, text, flag, null, null);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000436B9 File Offset: 0x000418B9
		[NullableContext(1)]
		private ObsoleteAttributeData DecodeExperimentalAttribute()
		{
			return ObsoleteAttributeData.Experimental;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000436C0 File Offset: 0x000418C0
		[NullableContext(1)]
		internal static void DecodeMethodImplAttribute<[Nullable(0)] T, [Nullable(0)] TAttributeSyntaxNode, [Nullable(0)] TAttributeData, [Nullable(2)] TAttributeLocation>([Nullable(new byte[] { 0, 1, 1, 1 })] ref DecodeWellKnownAttributeArguments<TAttributeSyntaxNode, TAttributeData, TAttributeLocation> arguments, CommonMessageProvider messageProvider) where T : CommonMethodWellKnownAttributeData, new() where TAttributeSyntaxNode : SyntaxNode where TAttributeData : AttributeData
		{
			TAttributeData attribute = arguments.Attribute;
			MethodImplOptions methodImplOptions;
			if (attribute.CommonConstructorArguments.Length == 1)
			{
				if (attribute.AttributeConstructor.Parameters[0].Type.SpecialType == SpecialType.System_Int16)
				{
					methodImplOptions = attribute.CommonConstructorArguments[0].DecodeValue<short>(SpecialType.System_Int16);
				}
				else
				{
					methodImplOptions = attribute.CommonConstructorArguments[0].DecodeValue<MethodImplOptions>(SpecialType.System_Enum);
				}
				if ((methodImplOptions & 3) != null)
				{
					messageProvider.ReportInvalidAttributeArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, 0, attribute);
					methodImplOptions &= -4;
				}
			}
			else
			{
				methodImplOptions = 0;
			}
			MethodImplAttributes methodImplAttributes = 0;
			int num = 1;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in attribute.CommonNamedArguments)
			{
				if (keyValuePair.Key == "MethodCodeType")
				{
					MethodImplAttributes methodImplAttributes2 = keyValuePair.Value.DecodeValue<int>(SpecialType.System_Enum);
					if (methodImplAttributes2 < 0 || methodImplAttributes2 > 3)
					{
						messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num, attribute.AttributeClass, "MethodCodeType");
					}
					else
					{
						methodImplAttributes = methodImplAttributes2;
					}
				}
				num++;
			}
			arguments.GetOrCreateData<T>().SetMethodImplementation(arguments.Index, methodImplOptions | methodImplAttributes);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00043830 File Offset: 0x00041A30
		[NullableContext(1)]
		internal static void DecodeStructLayoutAttribute<[Nullable(0)] TTypeWellKnownAttributeData, [Nullable(0)] TAttributeSyntaxNode, [Nullable(0)] TAttributeData, [Nullable(2)] TAttributeLocation>([Nullable(new byte[] { 0, 1, 1, 1 })] ref DecodeWellKnownAttributeArguments<TAttributeSyntaxNode, TAttributeData, TAttributeLocation> arguments, CharSet defaultCharSet, int defaultAutoLayoutSize, CommonMessageProvider messageProvider) where TTypeWellKnownAttributeData : CommonTypeWellKnownAttributeData, new() where TAttributeSyntaxNode : SyntaxNode where TAttributeData : AttributeData
		{
			TAttributeData attribute = arguments.Attribute;
			CharSet charSet = ((defaultCharSet != 1) ? defaultCharSet : 2);
			int? num = default(int?);
			int? num2 = default(int?);
			bool flag = false;
			LayoutKind layoutKind = attribute.CommonConstructorArguments[0].DecodeValue<LayoutKind>(SpecialType.System_Enum);
			if (layoutKind != null && layoutKind - 2 > 1)
			{
				messageProvider.ReportInvalidAttributeArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, 0, attribute);
				flag = true;
			}
			int num3 = 1;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in attribute.CommonNamedArguments)
			{
				string key = keyValuePair.Key;
				if (!(key == "CharSet"))
				{
					if (!(key == "Pack"))
					{
						if (key == "Size")
						{
							num = new int?(keyValuePair.Value.DecodeValue<int>(SpecialType.System_Int32));
							int? num4 = num;
							int num5 = 0;
							if ((num4.GetValueOrDefault() < num5) & (num4 != null))
							{
								messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num3, attribute.AttributeClass, keyValuePair.Key);
								flag = true;
							}
						}
					}
					else
					{
						num2 = new int?(keyValuePair.Value.DecodeValue<int>(SpecialType.System_Int32));
						int? num4 = num2;
						int num5 = 128;
						if (!((num4.GetValueOrDefault() > num5) & (num4 != null)))
						{
							num4 = num2 & (num2 - 1);
							num5 = 0;
							if ((num4.GetValueOrDefault() == num5) & (num4 != null))
							{
								goto IL_0270;
							}
						}
						messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num3, attribute.AttributeClass, keyValuePair.Key);
						flag = true;
					}
				}
				else
				{
					charSet = keyValuePair.Value.DecodeValue<CharSet>(SpecialType.System_Enum);
					if (charSet != 1)
					{
						if (charSet - 2 > 2)
						{
							messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num3, attribute.AttributeClass, keyValuePair.Key);
							flag = true;
						}
					}
					else
					{
						charSet = 2;
					}
				}
				IL_0270:
				num3++;
			}
			if (!flag)
			{
				if (layoutKind == 3 && num == null && num2 != null)
				{
					num = new int?(defaultAutoLayoutSize);
				}
				arguments.GetOrCreateData<TTypeWellKnownAttributeData>().SetStructLayout(new TypeLayout(layoutKind, num.GetValueOrDefault(), (byte)num2.GetValueOrDefault()), charSet);
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00043B0C File Offset: 0x00041D0C
		internal AttributeUsageInfo DecodeAttributeUsageAttribute()
		{
			return AttributeData.DecodeAttributeUsageAttribute(this.CommonConstructorArguments[0], this.CommonNamedArguments);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00043B34 File Offset: 0x00041D34
		internal static AttributeUsageInfo DecodeAttributeUsageAttribute(TypedConstant positionalArg, [Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<KeyValuePair<string, TypedConstant>> namedArgs)
		{
			AttributeTargets attributeTargets = (AttributeTargets)positionalArg.ValueInternal;
			bool flag = AttributeData.DecodeNamedArgument<bool>(namedArgs, "AllowMultiple", SpecialType.System_Boolean, false);
			bool flag2 = AttributeData.DecodeNamedArgument<bool>(namedArgs, "Inherited", SpecialType.System_Boolean, true);
			return new AttributeUsageInfo(attributeTargets, flag, flag2);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00043B70 File Offset: 0x00041D70
		[NullableContext(1)]
		[CompilerGenerated]
		internal static bool <IsTargetEarlyAttribute>g__namespaceMatch|25_0(INamespaceSymbolInternal container, string namespaceName, StringComparison options)
		{
			int num = namespaceName.Length;
			bool flag = false;
			while (!container.IsGlobalNamespace)
			{
				if (flag)
				{
					num--;
					if (num < 0 || namespaceName.get_Chars(num) != '.')
					{
						return false;
					}
				}
				else
				{
					flag = true;
				}
				string name = container.Name;
				int length = name.Length;
				num -= length;
				if (num < 0 || string.Compare(namespaceName, num, name, 0, length, options) != 0)
				{
					return false;
				}
				container = container.ContainingNamespace;
				if (container == null)
				{
					return false;
				}
			}
			return num == 0;
		}
	}
}
