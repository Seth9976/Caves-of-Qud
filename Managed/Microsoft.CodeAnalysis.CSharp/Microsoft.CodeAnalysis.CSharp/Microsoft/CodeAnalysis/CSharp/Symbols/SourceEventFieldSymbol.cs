using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000534 RID: 1332
	internal sealed class SourceEventFieldSymbol : SourceMemberFieldSymbolFromDeclarator
	{
		// Token: 0x06006FBE RID: 28606 RVA: 0x0019A7EF File Offset: 0x001989EF
		internal SourceEventFieldSymbol(SourceEventSymbol associatedEvent, VariableDeclaratorSyntax declaratorSyntax, BindingDiagnosticBag discardedDiagnostics)
			: base(associatedEvent.containingType, declaratorSyntax, (associatedEvent.Modifiers & ~(DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected)) | DeclarationModifiers.Private, true, discardedDiagnostics)
		{
			this._associatedEvent = associatedEvent;
		}

		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x06006FBF RID: 28607 RVA: 0x0019A819 File Offset: 0x00198A19
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x06006FC0 RID: 28608 RVA: 0x0019A81C File Offset: 0x00198A1C
		protected override IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return this._associatedEvent;
			}
		}

		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x06006FC1 RID: 28609 RVA: 0x0019A824 File Offset: 0x00198A24
		public override Symbol AssociatedSymbol
		{
			get
			{
				return this._associatedEvent;
			}
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x0019A82C File Offset: 0x00198A2C
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDebuggerBrowsableNeverAttribute());
		}

		// Token: 0x04001B61 RID: 7009
		private readonly SourceEventSymbol _associatedEvent;
	}
}
