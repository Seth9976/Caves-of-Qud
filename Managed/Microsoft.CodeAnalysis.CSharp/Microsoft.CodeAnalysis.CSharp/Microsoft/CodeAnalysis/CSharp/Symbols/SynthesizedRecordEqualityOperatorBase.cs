using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000576 RID: 1398
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SynthesizedRecordEqualityOperatorBase : SourceUserDefinedOperatorSymbolBase
	{
		// Token: 0x06007550 RID: 30032 RVA: 0x001B5310 File Offset: 0x001B3510
		protected SynthesizedRecordEqualityOperatorBase(SourceMemberContainerTypeSymbol containingType, string name, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(MethodKind.UserDefinedOperator, name, containingType, containingType.Locations[0], (CSharpSyntaxNode)containingType.SyntaxReferences[0].GetSyntax(default(CancellationToken)), DeclarationModifiers.Static | DeclarationModifiers.Public, true, false, false, false, diagnostics)
		{
			this._memberOffset = memberOffset;
		}

		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x06007551 RID: 30033 RVA: 0x001B5367 File Offset: 0x001B3567
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x06007552 RID: 30034 RVA: 0x001B536C File Offset: 0x001B356C
		protected sealed override Location ReturnTypeLocation
		{
			get
			{
				return this.Locations[0];
			}
		}

		// Token: 0x06007553 RID: 30035 RVA: 0x001B5388 File Offset: 0x001B3588
		internal sealed override LexicalSortKey GetLexicalSortKey()
		{
			return LexicalSortKey.GetSynthesizedMemberKey(this._memberOffset);
		}

		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x06007554 RID: 30036 RVA: 0x001B5395 File Offset: 0x001B3595
		[global::System.Runtime.CompilerServices.Nullable(2)]
		protected sealed override SourceMemberMethodSymbol BoundAttributesSource
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06007555 RID: 30037 RVA: 0x001B5398 File Offset: 0x001B3598
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		internal sealed override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(default(SyntaxList<AttributeListSyntax>));
		}

		// Token: 0x06007556 RID: 30038 RVA: 0x001B53B3 File Offset: 0x001B35B3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public sealed override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x06007557 RID: 30039 RVA: 0x001B53B6 File Offset: 0x001B35B6
		internal sealed override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x06007558 RID: 30040 RVA: 0x001B53B9 File Offset: 0x001B35B9
		internal sealed override bool SynthesizesLoweredBoundBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007559 RID: 30041 RVA: 0x001B53BC File Offset: 0x001B35BC
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected sealed override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location returnTypeLocation = this.ReturnTypeLocation;
			NullableAnnotation nullableAnnotation = (this.ContainingType.IsRecordStruct ? NullableAnnotation.Oblivious : NullableAnnotation.Annotated);
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>>(TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_Boolean, returnTypeLocation, diagnostics), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray.Create<ParameterSymbol>(new SourceSimpleParameterSymbol(this, TypeWithAnnotations.Create(this.ContainingType, nullableAnnotation, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "left", this.Locations), new SourceSimpleParameterSymbol(this, TypeWithAnnotations.Create(this.ContainingType, nullableAnnotation, default(ImmutableArray<CustomModifier>)), 1, RefKind.None, "right", this.Locations)));
		}

		// Token: 0x0600755A RID: 30042 RVA: 0x001B5459 File Offset: 0x001B3659
		protected override int GetParameterCountFromSyntax()
		{
			return 2;
		}

		// Token: 0x04001C9F RID: 7327
		private readonly int _memberOffset;
	}
}
