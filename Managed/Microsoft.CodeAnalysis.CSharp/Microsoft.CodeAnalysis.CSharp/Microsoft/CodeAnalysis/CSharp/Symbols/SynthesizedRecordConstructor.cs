using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000571 RID: 1393
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordConstructor : SourceConstructorSymbolBase
	{
		// Token: 0x0600752B RID: 29995 RVA: 0x001B49E8 File Offset: 0x001B2BE8
		public SynthesizedRecordConstructor(SourceMemberContainerTypeSymbol containingType, TypeDeclarationSyntax syntax)
			: base(containingType, syntax.Identifier.GetLocation(), syntax, false)
		{
			base.MakeFlags(MethodKind.Constructor, containingType.IsAbstract ? DeclarationModifiers.Protected : DeclarationModifiers.Public, true, false, false, false);
		}

		// Token: 0x0600752C RID: 29996 RVA: 0x001B4A28 File Offset: 0x001B2C28
		internal RecordDeclarationSyntax GetSyntax()
		{
			return (RecordDeclarationSyntax)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
		}

		// Token: 0x0600752D RID: 29997 RVA: 0x001B4A4E File Offset: 0x001B2C4E
		protected override ParameterListSyntax GetParameterList()
		{
			return this.GetSyntax().ParameterList;
		}

		// Token: 0x0600752E RID: 29998 RVA: 0x001B4A5B File Offset: 0x001B2C5B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override CSharpSyntaxNode GetInitializer()
		{
			return this.GetSyntax().PrimaryConstructorBaseTypeIfClass;
		}

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x0600752F RID: 29999 RVA: 0x001B4A68 File Offset: 0x001B2C68
		protected override bool AllowRefOrOut
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x06007530 RID: 30000 RVA: 0x001B4A6B File Offset: 0x001B2C6B
		internal override bool IsExpressionBodied
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007531 RID: 30001 RVA: 0x001B4A6E File Offset: 0x001B2C6E
		internal override bool IsNullableAnalysisEnabled()
		{
			return ((SourceMemberContainerTypeSymbol)this.ContainingType).IsNullableEnabledForConstructorsAndInitializers(this.IsStatic);
		}

		// Token: 0x06007532 RID: 30002 RVA: 0x001B4A86 File Offset: 0x001B2C86
		protected override bool IsWithinExpressionOrBlockBody(int position, out int offset)
		{
			offset = -1;
			return false;
		}

		// Token: 0x06007533 RID: 30003 RVA: 0x001B4A8C File Offset: 0x001B2C8C
		internal override ExecutableCodeBinder TryGetBodyBinder([global::System.Runtime.CompilerServices.Nullable(2)] BinderFactory binderFactoryOpt = null, bool ignoreAccessibility = false)
		{
			TypeDeclarationSyntax syntax = this.GetSyntax();
			InMethodBinder recordConstructorInMethodBinder = (binderFactoryOpt ?? this.DeclaringCompilation.GetBinderFactory(syntax.SyntaxTree, false)).GetRecordConstructorInMethodBinder(this);
			return new ExecutableCodeBinder(this.SyntaxNode, this, recordConstructorInMethodBinder.WithAdditionalFlags(ignoreAccessibility ? BinderFlags.IgnoreAccessibility : BinderFlags.None), null);
		}
	}
}
