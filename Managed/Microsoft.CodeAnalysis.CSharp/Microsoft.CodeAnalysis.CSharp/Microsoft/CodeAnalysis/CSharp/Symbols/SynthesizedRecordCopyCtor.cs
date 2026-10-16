using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000572 RID: 1394
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordCopyCtor : SynthesizedInstanceConstructor
	{
		// Token: 0x06007534 RID: 30004 RVA: 0x001B4ADC File Offset: 0x001B2CDC
		public SynthesizedRecordCopyCtor(SourceMemberContainerTypeSymbol containingType, int memberOffset)
			: base(containingType)
		{
			this._memberOffset = memberOffset;
			this.Parameters = ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(true, this.ContainingType, false), 0, RefKind.None, "original", default(ImmutableArray<CustomModifier>), null));
		}

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x06007535 RID: 30005 RVA: 0x001B4B26 File Offset: 0x001B2D26
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x06007536 RID: 30006 RVA: 0x001B4B2E File Offset: 0x001B2D2E
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				if (!this.ContainingType.IsSealed)
				{
					return Accessibility.Protected;
				}
				return Accessibility.Private;
			}
		}

		// Token: 0x06007537 RID: 30007 RVA: 0x001B4B40 File Offset: 0x001B2D40
		internal override LexicalSortKey GetLexicalSortKey()
		{
			return LexicalSortKey.GetSynthesizedMemberKey(this._memberOffset);
		}

		// Token: 0x06007538 RID: 30008 RVA: 0x001B4B50 File Offset: 0x001B2D50
		internal override void GenerateMethodBodyStatements(SyntheticBoundNodeFactory F, ArrayBuilder<BoundStatement> statements, BindingDiagnosticBag diagnostics)
		{
			BoundParameter boundParameter = F.Parameter(this.Parameters[0]);
			foreach (FieldSymbol fieldSymbol in this.ContainingType.GetFieldsToEmit())
			{
				if (!fieldSymbol.IsStatic)
				{
					statements.Add(F.Assignment(F.Field(F.This(), fieldSymbol), F.Field(boundParameter, fieldSymbol), false));
				}
			}
		}

		// Token: 0x06007539 RID: 30009 RVA: 0x001B4BDC File Offset: 0x001B2DDC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static MethodSymbol FindCopyConstructor(NamedTypeSymbol containingType, NamedTypeSymbol within, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			MethodSymbol methodSymbol = null;
			int num = -1;
			foreach (MethodSymbol methodSymbol2 in containingType.InstanceConstructors)
			{
				if (SynthesizedRecordCopyCtor.HasCopyConstructorSignature(methodSymbol2) && !methodSymbol2.HasUnsupportedMetadata && AccessCheck.IsSymbolAccessible(methodSymbol2, within, ref useSiteInfo, null))
				{
					if (methodSymbol == null && num < 0)
					{
						methodSymbol = methodSymbol2;
					}
					else
					{
						if (num < 0)
						{
							num = methodSymbol.CustomModifierCount();
						}
						int num2 = methodSymbol2.CustomModifierCount();
						if (num2 <= num)
						{
							if (num2 == num)
							{
								methodSymbol = null;
							}
							else
							{
								methodSymbol = methodSymbol2;
								num = num2;
							}
						}
					}
				}
			}
			return methodSymbol;
		}

		// Token: 0x0600753A RID: 30010 RVA: 0x001B4C64 File Offset: 0x001B2E64
		internal static bool IsCopyConstructor(Symbol member)
		{
			MethodSymbol methodSymbol = member as MethodSymbol;
			return methodSymbol != null && methodSymbol.MethodKind == MethodKind.Constructor && SynthesizedRecordCopyCtor.HasCopyConstructorSignature(methodSymbol);
		}

		// Token: 0x0600753B RID: 30011 RVA: 0x001B4C8C File Offset: 0x001B2E8C
		internal static bool HasCopyConstructorSignature(MethodSymbol member)
		{
			NamedTypeSymbol containingType = member.ContainingType;
			return member != null && !member.IsStatic && member.ParameterCount == 1 && member.Arity == 0 && member.Parameters[0].Type.Equals(containingType, TypeCompareKind.AllIgnoreOptions) && member.Parameters[0].RefKind == RefKind.None;
		}

		// Token: 0x04001C9A RID: 7322
		private readonly int _memberOffset;
	}
}
