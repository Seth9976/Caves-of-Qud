using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004AB RID: 1195
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class CSharpAttributeData : AttributeData, ICustomAttribute
	{
		// Token: 0x0600631C RID: 25372 RVA: 0x0017BD7C File Offset: 0x00179F7C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		ImmutableArray<IMetadataExpression> ICustomAttribute.GetArguments(EmitContext context)
		{
			ImmutableArray<TypedConstant> commonConstructorArguments = this.CommonConstructorArguments;
			if (commonConstructorArguments.IsEmpty)
			{
				return ImmutableArray<IMetadataExpression>.Empty;
			}
			ArrayBuilder<IMetadataExpression> instance = ArrayBuilder<IMetadataExpression>.GetInstance();
			foreach (TypedConstant typedConstant in commonConstructorArguments)
			{
				instance.Add(this.CreateMetadataExpression(typedConstant, context));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x0017BDD4 File Offset: 0x00179FD4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		IMethodReference ICustomAttribute.Constructor(EmitContext context, bool reportDiagnostics)
		{
			if (this.AttributeConstructor.IsDefaultValueTypeConstructor())
			{
				if (reportDiagnostics)
				{
					DiagnosticBag diagnostics = context.Diagnostics;
					ErrorCode errorCode = ErrorCode.ERR_NotAnAttributeClass;
					SyntaxNode syntaxNode = context.SyntaxNode;
					diagnostics.Add(errorCode, ((syntaxNode != null) ? syntaxNode.Location : null) ?? NoLocation.Singleton, new object[] { this.AttributeClass });
				}
				return null;
			}
			return ((PEModuleBuilder)context.Module).Translate(this.AttributeConstructor, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, null, false);
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x0017BE58 File Offset: 0x0017A058
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		ImmutableArray<IMetadataNamedArgument> ICustomAttribute.GetNamedArguments(EmitContext context)
		{
			ImmutableArray<KeyValuePair<string, TypedConstant>> commonNamedArguments = this.CommonNamedArguments;
			if (commonNamedArguments.IsEmpty)
			{
				return ImmutableArray<IMetadataNamedArgument>.Empty;
			}
			ArrayBuilder<IMetadataNamedArgument> instance = ArrayBuilder<IMetadataNamedArgument>.GetInstance();
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in commonNamedArguments)
			{
				instance.Add(this.CreateMetadataNamedArgument(keyValuePair.Key, keyValuePair.Value, context));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x0600631F RID: 25375 RVA: 0x0017BEC0 File Offset: 0x0017A0C0
		int ICustomAttribute.ArgumentCount
		{
			get
			{
				return this.CommonConstructorArguments.Length;
			}
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06006320 RID: 25376 RVA: 0x0017BEDC File Offset: 0x0017A0DC
		ushort ICustomAttribute.NamedArgumentCount
		{
			get
			{
				return (ushort)this.CommonNamedArguments.Length;
			}
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x0017BEF8 File Offset: 0x0017A0F8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		ITypeReference ICustomAttribute.GetType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AttributeClass, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, false);
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06006322 RID: 25378 RVA: 0x0017BF24 File Offset: 0x0017A124
		bool ICustomAttribute.AllowMultiple
		{
			get
			{
				return this.AttributeClass.GetAttributeUsageInfo().AllowMultiple;
			}
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x0017BF44 File Offset: 0x0017A144
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private IMetadataExpression CreateMetadataExpression(TypedConstant argument, EmitContext context)
		{
			if (argument.IsNull)
			{
				return CSharpAttributeData.CreateMetadataConstant(argument.TypeInternal, null, context);
			}
			TypedConstantKind kind = argument.Kind;
			if (kind == TypedConstantKind.Type)
			{
				return CSharpAttributeData.CreateType(argument, context);
			}
			if (kind == TypedConstantKind.Array)
			{
				return this.CreateMetadataArray(argument, context);
			}
			return CSharpAttributeData.CreateMetadataConstant(argument.TypeInternal, argument.ValueInternal, context);
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x0017BFA0 File Offset: 0x0017A1A0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private MetadataCreateArray CreateMetadataArray(TypedConstant argument, EmitContext context)
		{
			ImmutableArray<TypedConstant> values = argument.Values;
			IArrayTypeReference arrayTypeReference = ((PEModuleBuilder)context.Module).Translate((ArrayTypeSymbol)argument.TypeInternal);
			if (values.Length == 0)
			{
				return new MetadataCreateArray(arrayTypeReference, arrayTypeReference.GetElementType(context), ImmutableArray<IMetadataExpression>.Empty);
			}
			IMetadataExpression[] array = new IMetadataExpression[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = this.CreateMetadataExpression(values[i], context);
			}
			return new MetadataCreateArray(arrayTypeReference, arrayTypeReference.GetElementType(context), array.AsImmutableOrNull<IMetadataExpression>());
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x0017C034 File Offset: 0x0017A234
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static MetadataTypeOf CreateType(TypedConstant argument, EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)context.SyntaxNode;
			DiagnosticBag diagnostics = context.Diagnostics;
			return new MetadataTypeOf(pemoduleBuilder.Translate((TypeSymbol)argument.ValueInternal, csharpSyntaxNode, diagnostics), pemoduleBuilder.Translate((TypeSymbol)argument.TypeInternal, csharpSyntaxNode, diagnostics));
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x0017C08D File Offset: 0x0017A28D
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static MetadataConstant CreateMetadataConstant(ITypeSymbolInternal type, object value, EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).CreateConstant((TypeSymbol)type, value, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x0017C0B8 File Offset: 0x0017A2B8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private IMetadataNamedArgument CreateMetadataNamedArgument(string name, TypedConstant argument, EmitContext context)
		{
			Symbol symbol = this.LookupName(name);
			IMetadataExpression metadataExpression = this.CreateMetadataExpression(argument, context);
			FieldSymbol fieldSymbol = symbol as FieldSymbol;
			TypeSymbol typeSymbol;
			if (fieldSymbol != null)
			{
				typeSymbol = fieldSymbol.Type;
			}
			else
			{
				typeSymbol = ((PropertySymbol)symbol).Type;
			}
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			return new MetadataNamedArgument(symbol, pemoduleBuilder.Translate(typeSymbol, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics), metadataExpression);
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x0017C124 File Offset: 0x0017A324
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private Symbol LookupName(string name)
		{
			for (NamedTypeSymbol namedTypeSymbol = this.AttributeClass; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics)
			{
				foreach (Symbol symbol in namedTypeSymbol.GetMembers(name))
				{
					if (symbol.DeclaredAccessibility == Accessibility.Public)
					{
						return symbol;
					}
				}
			}
			return null;
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06006329 RID: 25385
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new abstract NamedTypeSymbol AttributeClass
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x0600632A RID: 25386
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new abstract MethodSymbol AttributeConstructor
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x0600632B RID: 25387
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new abstract SyntaxReference ApplicationSyntaxReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x0600632C RID: 25388 RVA: 0x0017C173 File Offset: 0x0017A373
		[MemberNotNullWhen(true, new string[] { "AttributeClass", "AttributeConstructor" })]
		internal override bool HasErrors
		{
			[MemberNotNullWhen(true, new string[] { "AttributeClass", "AttributeConstructor" })]
			get
			{
				return base.HasErrors;
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x0600632D RID: 25389 RVA: 0x0017C17D File Offset: 0x0017A37D
		public new IEnumerable<TypedConstant> ConstructorArguments
		{
			get
			{
				return this.CommonConstructorArguments;
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x0600632E RID: 25390 RVA: 0x0017C18A File Offset: 0x0017A38A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })]
		public new IEnumerable<KeyValuePair<string, TypedConstant>> NamedArguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })]
			get
			{
				return this.CommonNamedArguments;
			}
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x0017C197 File Offset: 0x0017A397
		internal virtual bool IsTargetAttribute(string namespaceName, string typeName)
		{
			return this.AttributeClass.Name.Equals(typeName) && (!this.AttributeClass.IsErrorType() || this.AttributeClass is MissingMetadataTypeSymbol) && this.AttributeClass.HasNameQualifier(namespaceName);
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x0017C1D6 File Offset: 0x0017A3D6
		internal bool IsTargetAttribute(Symbol targetSymbol, AttributeDescription description)
		{
			return this.GetTargetAttributeSignatureIndex(targetSymbol, description) != -1;
		}

		// Token: 0x06006331 RID: 25393
		internal abstract int GetTargetAttributeSignatureIndex(Symbol targetSymbol, AttributeDescription description);

		// Token: 0x06006332 RID: 25394 RVA: 0x0017C1E8 File Offset: 0x0017A3E8
		internal static bool IsTargetEarlyAttribute(NamedTypeSymbol attributeType, AttributeSyntax attributeSyntax, AttributeDescription description)
		{
			int num;
			if (attributeSyntax.ArgumentList == null)
			{
				num = 0;
			}
			else
			{
				num = Enumerable.Count<AttributeArgumentSyntax>(attributeSyntax.ArgumentList.Arguments, (AttributeArgumentSyntax arg) => arg.NameEquals == null);
			}
			int num2 = num;
			return AttributeData.IsTargetEarlyAttribute(attributeType, num2, description);
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x0017C240 File Offset: 0x0017A440
		internal bool IsSecurityAttribute(CSharpCompilation compilation)
		{
			if (this._lazyIsSecurityAttribute == ThreeState.Unknown)
			{
				NamedTypeSymbol wellKnownType = compilation.GetWellKnownType(WellKnownType.System_Security_Permissions_SecurityAttribute);
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				this._lazyIsSecurityAttribute = this.AttributeClass.IsDerivedFrom(wellKnownType, TypeCompareKind.ConsiderEverything, ref discarded).ToThreeState();
			}
			return this._lazyIsSecurityAttribute.Value();
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x0017C28C File Offset: 0x0017A48C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override string ToString()
		{
			if (this.AttributeClass == null)
			{
				return base.ToString();
			}
			string text = this.AttributeClass.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat);
			if (!this.CommonConstructorArguments.Any<TypedConstant>() & !this.CommonNamedArguments.Any<KeyValuePair<string, TypedConstant>>())
			{
				return text;
			}
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			builder.Append(text);
			builder.Append("(");
			bool flag = true;
			foreach (TypedConstant typedConstant in this.CommonConstructorArguments)
			{
				if (!flag)
				{
					builder.Append(", ");
				}
				builder.Append(typedConstant.ToCSharpString());
				flag = false;
			}
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in this.CommonNamedArguments)
			{
				if (!flag)
				{
					builder.Append(", ");
				}
				builder.Append(keyValuePair.Key);
				builder.Append(" = ");
				builder.Append(keyValuePair.Value.ToCSharpString());
				flag = false;
			}
			builder.Append(")");
			return instance.ToStringAndFree();
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x06006335 RID: 25397 RVA: 0x0017C3B9 File Offset: 0x0017A5B9
		[global::System.Runtime.CompilerServices.Nullable(2)]
		protected override INamedTypeSymbol CommonAttributeClass
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.AttributeClass.GetPublicSymbol();
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06006336 RID: 25398 RVA: 0x0017C3C6 File Offset: 0x0017A5C6
		[global::System.Runtime.CompilerServices.Nullable(2)]
		protected override IMethodSymbol CommonAttributeConstructor
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.AttributeConstructor.GetPublicSymbol();
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06006337 RID: 25399 RVA: 0x0017C3D3 File Offset: 0x0017A5D3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		protected override SyntaxReference CommonApplicationSyntaxReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ApplicationSyntaxReference;
			}
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x0017C3DC File Offset: 0x0017A5DC
		internal void DecodeSecurityAttribute<[global::System.Runtime.CompilerServices.Nullable(0)] T>(Symbol targetSymbol, CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments) where T : WellKnownAttributeData, ISecurityAttributeTarget, new()
		{
			bool flag;
			DeclarativeSecurityAction declarativeSecurityAction = this.DecodeSecurityAttributeAction(targetSymbol, compilation, arguments.AttributeSyntaxOpt, out flag, (BindingDiagnosticBag)arguments.Diagnostics);
			if (!flag)
			{
				SecurityWellKnownAttributeData orCreateData = arguments.GetOrCreateData<T>().GetOrCreateData();
				orCreateData.SetSecurityAttribute(arguments.Index, declarativeSecurityAction, arguments.AttributesCount);
				if (this.IsTargetAttribute(targetSymbol, AttributeDescription.PermissionSetAttribute))
				{
					string text = this.DecodePermissionSetAttribute(compilation, arguments.AttributeSyntaxOpt, (BindingDiagnosticBag)arguments.Diagnostics);
					if (text != null)
					{
						orCreateData.SetPathForPermissionSetAttributeFixup(arguments.Index, text, arguments.AttributesCount);
					}
				}
			}
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x0017C46C File Offset: 0x0017A66C
		internal static void DecodeSkipLocalsInitAttribute<[global::System.Runtime.CompilerServices.Nullable(0)] T>(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments) where T : WellKnownAttributeData, ISkipLocalsInitAttributeTarget, new()
		{
			arguments.GetOrCreateData<T>().HasSkipLocalsInitAttribute = true;
			if (!compilation.Options.AllowUnsafe)
			{
				((BindingDiagnosticBag)arguments.Diagnostics).Add(ErrorCode.ERR_IllegalUnsafe, arguments.AttributeSyntaxOpt.Location);
			}
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x0017C4B8 File Offset: 0x0017A6B8
		internal static void DecodeMemberNotNullAttribute<[global::System.Runtime.CompilerServices.Nullable(0)] T>(TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments) where T : WellKnownAttributeData, IMemberNotNullAttributeTarget, new()
		{
			TypedConstant typedConstant = arguments.Attribute.CommonConstructorArguments[0];
			if (typedConstant.IsNull)
			{
				return;
			}
			if (typedConstant.Kind != TypedConstantKind.Array)
			{
				string text = typedConstant.DecodeValue<string>(SpecialType.System_String);
				if (text != null)
				{
					arguments.GetOrCreateData<T>().AddNotNullMember(text);
					CSharpAttributeData.ReportBadNotNullMemberIfNeeded(type, arguments, text);
					return;
				}
			}
			else
			{
				ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
				foreach (TypedConstant typedConstant2 in typedConstant.Values)
				{
					string text2 = typedConstant2.DecodeValue<string>(SpecialType.System_String);
					if (text2 != null)
					{
						instance.Add(text2);
						CSharpAttributeData.ReportBadNotNullMemberIfNeeded(type, arguments, text2);
					}
				}
				arguments.GetOrCreateData<T>().AddNotNullMember(instance);
				instance.Free();
			}
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x0017C584 File Offset: 0x0017A784
		private static void ReportBadNotNullMemberIfNeeded(TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments, string memberName)
		{
			foreach (Symbol symbol in type.GetMembers(memberName))
			{
				if (symbol.Kind == SymbolKind.Field || symbol.Kind == SymbolKind.Property)
				{
					return;
				}
			}
			((BindingDiagnosticBag)arguments.Diagnostics).Add(ErrorCode.WRN_MemberNotNullBadMember, arguments.AttributeSyntaxOpt.Location, new object[] { memberName });
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x0017C5F8 File Offset: 0x0017A7F8
		internal static void DecodeMemberNotNullWhenAttribute<[global::System.Runtime.CompilerServices.Nullable(0)] T>(TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments) where T : WellKnownAttributeData, IMemberNotNullAttributeTarget, new()
		{
			TypedConstant typedConstant = arguments.Attribute.CommonConstructorArguments[1];
			if (typedConstant.IsNull)
			{
				return;
			}
			bool flag = arguments.Attribute.CommonConstructorArguments[0].DecodeValue<bool>(SpecialType.System_Boolean);
			if (typedConstant.Kind != TypedConstantKind.Array)
			{
				string text = typedConstant.DecodeValue<string>(SpecialType.System_String);
				if (text != null)
				{
					arguments.GetOrCreateData<T>().AddNotNullWhenMember(flag, text);
					CSharpAttributeData.ReportBadNotNullMemberIfNeeded(type, arguments, text);
					return;
				}
			}
			else
			{
				ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
				foreach (TypedConstant typedConstant2 in typedConstant.Values)
				{
					string text2 = typedConstant2.DecodeValue<string>(SpecialType.System_String);
					if (text2 != null)
					{
						instance.Add(text2);
						CSharpAttributeData.ReportBadNotNullMemberIfNeeded(type, arguments, text2);
					}
				}
				arguments.GetOrCreateData<T>().AddNotNullWhenMember(flag, instance);
				instance.Free();
			}
		}

		// Token: 0x0600633D RID: 25405 RVA: 0x0017C6EC File Offset: 0x0017A8EC
		private DeclarativeSecurityAction DecodeSecurityAttributeAction(Symbol targetSymbol, CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeSyntax nodeOpt, out bool hasErrors, BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<TypedConstant> commonConstructorArguments = this.CommonConstructorArguments;
			if (!commonConstructorArguments.Any<TypedConstant>())
			{
				if (this.IsTargetAttribute(targetSymbol, AttributeDescription.HostProtectionAttribute))
				{
					hasErrors = false;
					return DeclarativeSecurityAction.LinkDemand;
				}
			}
			else
			{
				TypedConstant typedConstant = commonConstructorArguments.First<TypedConstant>();
				TypeSymbol typeSymbol = (TypeSymbol)typedConstant.TypeInternal;
				if (typeSymbol != null && typeSymbol.Equals(compilation.GetWellKnownType(WellKnownType.System_Security_Permissions_SecurityAction)))
				{
					return this.DecodeSecurityAction(typedConstant, targetSymbol, nodeOpt, diagnostics, out hasErrors);
				}
			}
			diagnostics.Add(ErrorCode.ERR_SecurityAttributeMissingAction, (nodeOpt != null) ? nodeOpt.Name.Location : NoLocation.Singleton);
			hasErrors = true;
			return DeclarativeSecurityAction.None;
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x0017C77C File Offset: 0x0017A97C
		private DeclarativeSecurityAction DecodeSecurityAction(TypedConstant typedValue, Symbol targetSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeSyntax nodeOpt, BindingDiagnosticBag diagnostics, out bool hasErrors)
		{
			int num = (int)typedValue.ValueInternal;
			bool flag;
			switch (num)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				flag = false;
				break;
			case 6:
			case 7:
				if (this.IsTargetAttribute(targetSymbol, AttributeDescription.PrincipalPermissionAttribute))
				{
					object obj;
					Location securityAttributeActionSyntaxLocation = CSharpAttributeData.GetSecurityAttributeActionSyntaxLocation(nodeOpt, typedValue, out obj);
					diagnostics.Add(ErrorCode.ERR_PrincipalPermissionInvalidAction, securityAttributeActionSyntaxLocation, new object[] { obj });
					hasErrors = true;
					return DeclarativeSecurityAction.None;
				}
				flag = false;
				break;
			case 8:
			case 9:
			case 10:
				flag = true;
				break;
			default:
			{
				object obj2;
				Location securityAttributeActionSyntaxLocation2 = CSharpAttributeData.GetSecurityAttributeActionSyntaxLocation(nodeOpt, typedValue, out obj2);
				diagnostics.Add(ErrorCode.ERR_SecurityAttributeInvalidAction, securityAttributeActionSyntaxLocation2, new object[]
				{
					(nodeOpt != null) ? nodeOpt.GetErrorDisplayName() : "",
					obj2
				});
				hasErrors = true;
				return DeclarativeSecurityAction.None;
			}
			}
			if (flag)
			{
				if (targetSymbol.Kind == SymbolKind.NamedType || targetSymbol.Kind == SymbolKind.Method)
				{
					object obj3;
					Location securityAttributeActionSyntaxLocation3 = CSharpAttributeData.GetSecurityAttributeActionSyntaxLocation(nodeOpt, typedValue, out obj3);
					diagnostics.Add(ErrorCode.ERR_SecurityAttributeInvalidActionTypeOrMethod, securityAttributeActionSyntaxLocation3, new object[] { obj3 });
					hasErrors = true;
					return DeclarativeSecurityAction.None;
				}
			}
			else if (targetSymbol.Kind == SymbolKind.Assembly)
			{
				object obj4;
				Location securityAttributeActionSyntaxLocation4 = CSharpAttributeData.GetSecurityAttributeActionSyntaxLocation(nodeOpt, typedValue, out obj4);
				diagnostics.Add(ErrorCode.ERR_SecurityAttributeInvalidActionAssembly, securityAttributeActionSyntaxLocation4, new object[] { obj4 });
				hasErrors = true;
				return DeclarativeSecurityAction.None;
			}
			hasErrors = false;
			return (DeclarativeSecurityAction)num;
		}

		// Token: 0x0600633F RID: 25407 RVA: 0x0017C8C4 File Offset: 0x0017AAC4
		private static Location GetSecurityAttributeActionSyntaxLocation([global::System.Runtime.CompilerServices.Nullable(2)] AttributeSyntax nodeOpt, TypedConstant typedValue, out object displayString)
		{
			if (nodeOpt == null)
			{
				displayString = "";
				return NoLocation.Singleton;
			}
			AttributeArgumentListSyntax argumentList = nodeOpt.ArgumentList;
			if (argumentList == null || argumentList.Arguments.IsEmpty<AttributeArgumentSyntax>())
			{
				displayString = FormattableStringFactory.Create("{0}", new object[] { typedValue.ValueInternal });
				return nodeOpt.Location;
			}
			AttributeArgumentSyntax attributeArgumentSyntax = argumentList.Arguments[0];
			displayString = attributeArgumentSyntax.ToString();
			return attributeArgumentSyntax.Location;
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x0017C940 File Offset: 0x0017AB40
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private string DecodePermissionSetAttribute(CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeSyntax nodeOpt, BindingDiagnosticBag diagnostics)
		{
			string text = null;
			ImmutableArray<KeyValuePair<string, TypedConstant>> commonNamedArguments = this.CommonNamedArguments;
			if (commonNamedArguments.Length == 1)
			{
				KeyValuePair<string, TypedConstant> keyValuePair = commonNamedArguments[0];
				NamedTypeSymbol attributeClass = this.AttributeClass;
				string text2 = "File";
				string text3 = "Hex";
				if (keyValuePair.Key == text2 && CSharpAttributeData.PermissionSetAttributeTypeHasRequiredProperty(attributeClass, text2))
				{
					string text4 = (string)keyValuePair.Value.ValueInternal;
					XmlReferenceResolver xmlReferenceResolver = compilation.Options.XmlReferenceResolver;
					text = ((xmlReferenceResolver != null && text4 != null) ? xmlReferenceResolver.ResolveReference(text4, null) : null);
					if (text == null)
					{
						Location location;
						if (nodeOpt == null)
						{
							location = null;
						}
						else
						{
							AttributeArgumentSyntax namedArgumentSyntax = nodeOpt.GetNamedArgumentSyntax(text2);
							location = ((namedArgumentSyntax != null) ? namedArgumentSyntax.Location : null);
						}
						Location location2 = location ?? NoLocation.Singleton;
						diagnostics.Add(ErrorCode.ERR_PermissionSetAttributeInvalidFile, location2, new object[]
						{
							text4 ?? "<null>",
							text2
						});
					}
					else if (!CSharpAttributeData.PermissionSetAttributeTypeHasRequiredProperty(attributeClass, text3))
					{
						return null;
					}
				}
			}
			return text;
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x0017CA3C File Offset: 0x0017AC3C
		private static bool PermissionSetAttributeTypeHasRequiredProperty(NamedTypeSymbol permissionSetType, string propName)
		{
			ImmutableArray<Symbol> members = permissionSetType.GetMembers(propName);
			if (members.Length == 1 && members[0].Kind == SymbolKind.Property)
			{
				PropertySymbol propertySymbol = (PropertySymbol)members[0];
				if (propertySymbol.TypeWithAnnotations.HasType && propertySymbol.Type.SpecialType == SpecialType.System_String && propertySymbol.DeclaredAccessibility == Accessibility.Public && propertySymbol.GetMemberArity() == 0 && propertySymbol.SetMethod != null && propertySymbol.SetMethod.DeclaredAccessibility == Accessibility.Public)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x0017CAC4 File Offset: 0x0017ACC4
		internal void DecodeClassInterfaceAttribute([global::System.Runtime.CompilerServices.Nullable(2)] AttributeSyntax nodeOpt, BindingDiagnosticBag diagnostics)
		{
			TypedConstant typedConstant = this.CommonConstructorArguments[0];
			ClassInterfaceType classInterfaceType = ((typedConstant.Kind == TypedConstantKind.Enum) ? typedConstant.DecodeValue<ClassInterfaceType>(SpecialType.System_Enum) : typedConstant.DecodeValue<short>(SpecialType.System_Int16));
			if (classInterfaceType > 2)
			{
				Location attributeArgumentSyntaxLocation = this.GetAttributeArgumentSyntaxLocation(0, nodeOpt);
				diagnostics.Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntaxLocation, new object[] { (nodeOpt != null) ? nodeOpt.GetErrorDisplayName() : "" });
			}
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x0017CB34 File Offset: 0x0017AD34
		internal void DecodeInterfaceTypeAttribute(AttributeSyntax node, BindingDiagnosticBag diagnostics)
		{
			TypedConstant typedConstant = this.CommonConstructorArguments[0];
			ComInterfaceType comInterfaceType = ((typedConstant.Kind == TypedConstantKind.Enum) ? typedConstant.DecodeValue<ComInterfaceType>(SpecialType.System_Enum) : typedConstant.DecodeValue<short>(SpecialType.System_Int16));
			if (comInterfaceType > 3)
			{
				CSharpSyntaxNode attributeArgumentSyntax = this.GetAttributeArgumentSyntax(0, node);
				diagnostics.Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntax.Location, new object[] { node.GetErrorDisplayName() });
			}
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x0017CBA0 File Offset: 0x0017ADA0
		internal string DecodeGuidAttribute([global::System.Runtime.CompilerServices.Nullable(2)] AttributeSyntax nodeOpt, BindingDiagnosticBag diagnostics)
		{
			string text = (string)this.CommonConstructorArguments[0].ValueInternal;
			Guid guid;
			if (!Guid.TryParseExact(text, "D", ref guid))
			{
				Location attributeArgumentSyntaxLocation = this.GetAttributeArgumentSyntaxLocation(0, nodeOpt);
				diagnostics.Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntaxLocation, new object[] { (nodeOpt != null) ? nodeOpt.GetErrorDisplayName() : "" });
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x0017CC14 File Offset: 0x0017AE14
		private protected sealed override bool IsStringProperty(string memberName)
		{
			if (this.AttributeClass != null)
			{
				foreach (Symbol symbol in this.AttributeClass.GetMembers(memberName))
				{
					PropertySymbol propertySymbol = symbol as PropertySymbol;
					if (propertySymbol != null)
					{
						TypeSymbol type = propertySymbol.Type;
						if (type != null && type.SpecialType == SpecialType.System_String)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x0017CC70 File Offset: 0x0017AE70
		internal bool ShouldEmitAttribute(Symbol target, bool isReturnType, bool emittingAssemblyAttributesInNetModule)
		{
			if (this.HasErrors)
			{
				throw ExceptionUtilities.Unreachable;
			}
			if (this.IsConditionallyOmitted)
			{
				return false;
			}
			switch (target.Kind)
			{
			case SymbolKind.Assembly:
				if ((!emittingAssemblyAttributesInNetModule && (this.IsTargetAttribute(target, AttributeDescription.AssemblyCultureAttribute) || this.IsTargetAttribute(target, AttributeDescription.AssemblyVersionAttribute) || this.IsTargetAttribute(target, AttributeDescription.AssemblyFlagsAttribute) || this.IsTargetAttribute(target, AttributeDescription.AssemblyAlgorithmIdAttribute))) || this.IsTargetAttribute(target, AttributeDescription.TypeForwardedToAttribute) || this.IsSecurityAttribute(target.DeclaringCompilation))
				{
					return false;
				}
				break;
			case SymbolKind.Event:
				if (this.IsTargetAttribute(target, AttributeDescription.SpecialNameAttribute))
				{
					return false;
				}
				break;
			case SymbolKind.Field:
				if (this.IsTargetAttribute(target, AttributeDescription.SpecialNameAttribute) || this.IsTargetAttribute(target, AttributeDescription.NonSerializedAttribute) || this.IsTargetAttribute(target, AttributeDescription.FieldOffsetAttribute) || this.IsTargetAttribute(target, AttributeDescription.MarshalAsAttribute))
				{
					return false;
				}
				break;
			case SymbolKind.Method:
				if (isReturnType)
				{
					if (this.IsTargetAttribute(target, AttributeDescription.MarshalAsAttribute))
					{
						return false;
					}
				}
				else if (this.IsTargetAttribute(target, AttributeDescription.SpecialNameAttribute) || this.IsTargetAttribute(target, AttributeDescription.MethodImplAttribute) || this.IsTargetAttribute(target, AttributeDescription.DllImportAttribute) || this.IsTargetAttribute(target, AttributeDescription.PreserveSigAttribute) || this.IsTargetAttribute(target, AttributeDescription.DynamicSecurityMethodAttribute) || this.IsSecurityAttribute(target.DeclaringCompilation))
				{
					return false;
				}
				break;
			case SymbolKind.NamedType:
				if (this.IsTargetAttribute(target, AttributeDescription.SpecialNameAttribute) || this.IsTargetAttribute(target, AttributeDescription.ComImportAttribute) || this.IsTargetAttribute(target, AttributeDescription.SerializableAttribute) || this.IsTargetAttribute(target, AttributeDescription.StructLayoutAttribute) || this.IsTargetAttribute(target, AttributeDescription.WindowsRuntimeImportAttribute) || this.IsSecurityAttribute(target.DeclaringCompilation))
				{
					return false;
				}
				break;
			case SymbolKind.Parameter:
				if (this.IsTargetAttribute(target, AttributeDescription.OptionalAttribute) || this.IsTargetAttribute(target, AttributeDescription.DefaultParameterValueAttribute) || this.IsTargetAttribute(target, AttributeDescription.InAttribute) || this.IsTargetAttribute(target, AttributeDescription.OutAttribute) || this.IsTargetAttribute(target, AttributeDescription.MarshalAsAttribute))
				{
					return false;
				}
				break;
			case SymbolKind.Property:
				if (this.IsTargetAttribute(target, AttributeDescription.IndexerNameAttribute) || this.IsTargetAttribute(target, AttributeDescription.SpecialNameAttribute) || this.IsTargetAttribute(target, AttributeDescription.DisallowNullAttribute) || this.IsTargetAttribute(target, AttributeDescription.AllowNullAttribute) || this.IsTargetAttribute(target, AttributeDescription.MaybeNullAttribute) || this.IsTargetAttribute(target, AttributeDescription.NotNullAttribute))
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x0400198C RID: 6540
		private ThreeState _lazyIsSecurityAttribute;
	}
}
