using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004AC RID: 1196
	internal abstract class CSharpCustomModifier : CustomModifier, ICustomModifier
	{
		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06006348 RID: 25416 RVA: 0x0017CEFC File Offset: 0x0017B0FC
		bool ICustomModifier.IsOptional
		{
			get
			{
				return this.IsOptional;
			}
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x0017CF04 File Offset: 0x0017B104
		ITypeReference ICustomModifier.GetModifier(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.ModifierSymbol, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, false);
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x0017CF2F File Offset: 0x0017B12F
		private CSharpCustomModifier(NamedTypeSymbol modifier)
		{
			this.modifier = modifier;
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x0600634B RID: 25419 RVA: 0x0017CF3E File Offset: 0x0017B13E
		public override INamedTypeSymbol Modifier
		{
			get
			{
				return this.modifier.GetPublicSymbol();
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x0600634C RID: 25420 RVA: 0x0017CF4B File Offset: 0x0017B14B
		public NamedTypeSymbol ModifierSymbol
		{
			get
			{
				return this.modifier;
			}
		}

		// Token: 0x0600634D RID: 25421
		public abstract override int GetHashCode();

		// Token: 0x0600634E RID: 25422
		public abstract override bool Equals(object obj);

		// Token: 0x0600634F RID: 25423 RVA: 0x0017CF53 File Offset: 0x0017B153
		internal static CustomModifier CreateOptional(NamedTypeSymbol modifier)
		{
			return new CSharpCustomModifier.OptionalCustomModifier(modifier);
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x0017CF5B File Offset: 0x0017B15B
		internal static CustomModifier CreateRequired(NamedTypeSymbol modifier)
		{
			return new CSharpCustomModifier.RequiredCustomModifier(modifier);
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x0017CF63 File Offset: 0x0017B163
		internal static ImmutableArray<CustomModifier> Convert(ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers)
		{
			if (customModifiers.IsDefault)
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
			return customModifiers.SelectAsArray<ModifierInfo<TypeSymbol>, CustomModifier>(new Func<ModifierInfo<TypeSymbol>, CustomModifier>(CSharpCustomModifier.Convert));
		}

		// Token: 0x06006352 RID: 25426 RVA: 0x0017CF88 File Offset: 0x0017B188
		private static CustomModifier Convert(ModifierInfo<TypeSymbol> customModifier)
		{
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)customModifier.Modifier;
			if (!customModifier.IsOptional)
			{
				return CSharpCustomModifier.CreateRequired(namedTypeSymbol);
			}
			return CSharpCustomModifier.CreateOptional(namedTypeSymbol);
		}

		// Token: 0x0400198D RID: 6541
		protected readonly NamedTypeSymbol modifier;

		// Token: 0x020008D0 RID: 2256
		private class OptionalCustomModifier : CSharpCustomModifier
		{
			// Token: 0x06008E29 RID: 36393 RVA: 0x001F5CF8 File Offset: 0x001F3EF8
			public OptionalCustomModifier(NamedTypeSymbol modifier)
				: base(modifier)
			{
			}

			// Token: 0x1700283C RID: 10300
			// (get) Token: 0x06008E2A RID: 36394 RVA: 0x001F5D01 File Offset: 0x001F3F01
			public override bool IsOptional
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008E2B RID: 36395 RVA: 0x001F5D04 File Offset: 0x001F3F04
			public override int GetHashCode()
			{
				return this.modifier.GetHashCode();
			}

			// Token: 0x06008E2C RID: 36396 RVA: 0x001F5D14 File Offset: 0x001F3F14
			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				CSharpCustomModifier.OptionalCustomModifier optionalCustomModifier = obj as CSharpCustomModifier.OptionalCustomModifier;
				return optionalCustomModifier != null && optionalCustomModifier.modifier.Equals(this.modifier);
			}
		}

		// Token: 0x020008D1 RID: 2257
		private class RequiredCustomModifier : CSharpCustomModifier
		{
			// Token: 0x06008E2D RID: 36397 RVA: 0x001F5D44 File Offset: 0x001F3F44
			public RequiredCustomModifier(NamedTypeSymbol modifier)
				: base(modifier)
			{
			}

			// Token: 0x1700283D RID: 10301
			// (get) Token: 0x06008E2E RID: 36398 RVA: 0x001F5D4D File Offset: 0x001F3F4D
			public override bool IsOptional
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008E2F RID: 36399 RVA: 0x001F5D50 File Offset: 0x001F3F50
			public override int GetHashCode()
			{
				return this.modifier.GetHashCode();
			}

			// Token: 0x06008E30 RID: 36400 RVA: 0x001F5D60 File Offset: 0x001F3F60
			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				CSharpCustomModifier.RequiredCustomModifier requiredCustomModifier = obj as CSharpCustomModifier.RequiredCustomModifier;
				return requiredCustomModifier != null && requiredCustomModifier.modifier.Equals(this.modifier);
			}
		}
	}
}
