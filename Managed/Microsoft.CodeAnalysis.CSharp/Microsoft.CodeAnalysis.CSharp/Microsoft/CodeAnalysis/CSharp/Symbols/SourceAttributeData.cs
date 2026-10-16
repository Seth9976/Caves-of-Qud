using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C5 RID: 1221
	internal class SourceAttributeData : CSharpAttributeData
	{
		// Token: 0x06006845 RID: 26693 RVA: 0x00187CB4 File Offset: 0x00185EB4
		internal SourceAttributeData([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxReference applicationNode, [global::System.Runtime.CompilerServices.Nullable(1)] NamedTypeSymbol attributeClass, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol attributeConstructor, ImmutableArray<TypedConstant> constructorArguments, ImmutableArray<int> constructorArgumentsSourceIndices, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments, bool hasErrors, bool isConditionallyOmitted)
		{
			this._attributeClass = attributeClass;
			this._attributeConstructor = attributeConstructor;
			this._constructorArguments = constructorArguments;
			this._constructorArgumentsSourceIndices = constructorArgumentsSourceIndices;
			this._namedArguments = namedArguments;
			this._isConditionallyOmitted = isConditionallyOmitted;
			this._hasErrors = hasErrors;
			this._applicationNode = applicationNode;
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x00187D04 File Offset: 0x00185F04
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal SourceAttributeData(SyntaxReference applicationNode, NamedTypeSymbol attributeClass, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol attributeConstructor, bool hasErrors)
			: this(applicationNode, attributeClass, attributeConstructor, ImmutableArray<TypedConstant>.Empty, default(ImmutableArray<int>), ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty, hasErrors, false)
		{
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x06006847 RID: 26695 RVA: 0x00187D30 File Offset: 0x00185F30
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override NamedTypeSymbol AttributeClass
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._attributeClass;
			}
		}

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x06006848 RID: 26696 RVA: 0x00187D38 File Offset: 0x00185F38
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override MethodSymbol AttributeConstructor
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._attributeConstructor;
			}
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x06006849 RID: 26697 RVA: 0x00187D40 File Offset: 0x00185F40
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxReference ApplicationSyntaxReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._applicationNode;
			}
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x0600684A RID: 26698 RVA: 0x00187D48 File Offset: 0x00185F48
		internal ImmutableArray<int> ConstructorArgumentsSourceIndices
		{
			get
			{
				return this._constructorArgumentsSourceIndices;
			}
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x00187D50 File Offset: 0x00185F50
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal CSharpSyntaxNode GetAttributeArgumentSyntax(int parameterIndex, AttributeSyntax attributeSyntax)
		{
			if (this._constructorArgumentsSourceIndices.IsDefault)
			{
				return attributeSyntax.ArgumentList.Arguments[parameterIndex];
			}
			int num = this._constructorArgumentsSourceIndices[parameterIndex];
			if (num == -1)
			{
				return attributeSyntax.Name;
			}
			return attributeSyntax.ArgumentList.Arguments[num];
		}

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x0600684C RID: 26700 RVA: 0x00187DB1 File Offset: 0x00185FB1
		internal override bool IsConditionallyOmitted
		{
			get
			{
				return this._isConditionallyOmitted;
			}
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x00187DB9 File Offset: 0x00185FB9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal SourceAttributeData WithOmittedCondition(bool isConditionallyOmitted)
		{
			if (this.IsConditionallyOmitted == isConditionallyOmitted)
			{
				return this;
			}
			return new SourceAttributeData(this.ApplicationSyntaxReference, this.AttributeClass, this.AttributeConstructor, this.CommonConstructorArguments, this.ConstructorArgumentsSourceIndices, this.CommonNamedArguments, this.HasErrors, isConditionallyOmitted);
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x0600684E RID: 26702 RVA: 0x00187DF6 File Offset: 0x00185FF6
		[MemberNotNullWhen(true, new string[] { "AttributeClass", "AttributeConstructor" })]
		internal override bool HasErrors
		{
			[MemberNotNullWhen(true, new string[] { "AttributeClass", "AttributeConstructor" })]
			get
			{
				return this._hasErrors;
			}
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x0600684F RID: 26703 RVA: 0x00187DFE File Offset: 0x00185FFE
		protected internal sealed override ImmutableArray<TypedConstant> CommonConstructorArguments
		{
			get
			{
				return this._constructorArguments;
			}
		}

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06006850 RID: 26704 RVA: 0x00187E06 File Offset: 0x00186006
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected internal sealed override ImmutableArray<KeyValuePair<string, TypedConstant>> CommonNamedArguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
			get
			{
				return this._namedArguments;
			}
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x00187E10 File Offset: 0x00186010
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override int GetTargetAttributeSignatureIndex(Symbol targetSymbol, AttributeDescription description)
		{
			SourceAttributeData.<>c__DisplayClass28_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.targetSymbol = targetSymbol;
			if (!this.IsTargetAttribute(description.Namespace, description.Name))
			{
				return -1;
			}
			MethodSymbol attributeConstructor = this.AttributeConstructor;
			if (attributeConstructor == null)
			{
				return -1;
			}
			TypeSymbol typeSymbol = null;
			ImmutableArray<ParameterSymbol> parameters = attributeConstructor.Parameters;
			for (int i = 0; i < description.Signatures.Length; i++)
			{
				byte[] array = description.Signatures[i];
				if (this.<GetTargetAttributeSignatureIndex>g__matches|28_0(array, parameters, ref typeSymbol, ref CS$<>8__locals1))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x00187E8D File Offset: 0x0018608D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal virtual TypeSymbol GetSystemType(Symbol targetSymbol)
		{
			return targetSymbol.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Type);
		}

		// Token: 0x06006853 RID: 26707 RVA: 0x00187E9C File Offset: 0x0018609C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		private bool <GetTargetAttributeSignatureIndex>g__matches|28_0(byte[] targetSignature, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters, [global::System.Runtime.CompilerServices.Nullable(2)] ref TypeSymbol lazySystemType, ref SourceAttributeData.<>c__DisplayClass28_0 A_4)
		{
			if (targetSignature[0] != 32)
			{
				return false;
			}
			if ((int)targetSignature[1] != parameters.Length)
			{
				return false;
			}
			if (targetSignature[2] != 1)
			{
				return false;
			}
			int num = 0;
			for (int i = 3; i < targetSignature.Length; i++)
			{
				if (num >= parameters.Length)
				{
					return false;
				}
				TypeSymbol type = parameters[num].Type;
				SpecialType specialType = type.SpecialType;
				byte b = targetSignature[i];
				if (b == 64)
				{
					i++;
					if (type.Kind != SymbolKind.NamedType && type.Kind != SymbolKind.ErrorType)
					{
						return false;
					}
					NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)type;
					AttributeDescription.TypeHandleTargetInfo typeHandleTargetInfo = AttributeDescription.TypeHandleTargets[(int)targetSignature[i]];
					if (!string.Equals(namedTypeSymbol.MetadataName, typeHandleTargetInfo.Name, 4) || !namedTypeSymbol.HasNameQualifier(typeHandleTargetInfo.Namespace))
					{
						return false;
					}
					b = (byte)typeHandleTargetInfo.Underlying;
					if (type.IsEnumType())
					{
						specialType = type.GetEnumUnderlyingType().SpecialType;
					}
				}
				else if (b != 29 && type.IsArray())
				{
					if (targetSignature[i - 1] != 29)
					{
						return false;
					}
					specialType = ((ArrayTypeSymbol)type).ElementType.SpecialType;
				}
				switch (b)
				{
				case 2:
					if (specialType != SpecialType.System_Boolean)
					{
						return false;
					}
					num++;
					break;
				case 3:
					if (specialType != SpecialType.System_Char)
					{
						return false;
					}
					num++;
					break;
				case 4:
					if (specialType != SpecialType.System_SByte)
					{
						return false;
					}
					num++;
					break;
				case 5:
					if (specialType != SpecialType.System_Byte)
					{
						return false;
					}
					num++;
					break;
				case 6:
					if (specialType != SpecialType.System_Int16)
					{
						return false;
					}
					num++;
					break;
				case 7:
					if (specialType != SpecialType.System_UInt16)
					{
						return false;
					}
					num++;
					break;
				case 8:
					if (specialType != SpecialType.System_Int32)
					{
						return false;
					}
					num++;
					break;
				case 9:
					if (specialType != SpecialType.System_UInt32)
					{
						return false;
					}
					num++;
					break;
				case 10:
					if (specialType != SpecialType.System_Int64)
					{
						return false;
					}
					num++;
					break;
				case 11:
					if (specialType != SpecialType.System_UInt64)
					{
						return false;
					}
					num++;
					break;
				case 12:
					if (specialType != SpecialType.System_Single)
					{
						return false;
					}
					num++;
					break;
				case 13:
					if (specialType != SpecialType.System_Double)
					{
						return false;
					}
					num++;
					break;
				case 14:
					if (specialType != SpecialType.System_String)
					{
						return false;
					}
					num++;
					break;
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
				case 23:
				case 24:
				case 25:
				case 26:
				case 27:
					return false;
				case 28:
					if (specialType != SpecialType.System_Object)
					{
						return false;
					}
					num++;
					break;
				case 29:
					if (!type.IsArray())
					{
						return false;
					}
					break;
				default:
					if (b != 80)
					{
						return false;
					}
					if (lazySystemType == null)
					{
						lazySystemType = this.GetSystemType(A_4.targetSymbol);
					}
					if (!TypeSymbol.Equals(type, lazySystemType, TypeCompareKind.ConsiderEverything))
					{
						return false;
					}
					num++;
					break;
				}
			}
			return true;
		}

		// Token: 0x040019CF RID: 6607
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly NamedTypeSymbol _attributeClass;

		// Token: 0x040019D0 RID: 6608
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly MethodSymbol _attributeConstructor;

		// Token: 0x040019D1 RID: 6609
		private readonly ImmutableArray<TypedConstant> _constructorArguments;

		// Token: 0x040019D2 RID: 6610
		private readonly ImmutableArray<int> _constructorArgumentsSourceIndices;

		// Token: 0x040019D3 RID: 6611
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		private readonly ImmutableArray<KeyValuePair<string, TypedConstant>> _namedArguments;

		// Token: 0x040019D4 RID: 6612
		private readonly bool _isConditionallyOmitted;

		// Token: 0x040019D5 RID: 6613
		private readonly bool _hasErrors;

		// Token: 0x040019D6 RID: 6614
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly SyntaxReference _applicationNode;
	}
}
