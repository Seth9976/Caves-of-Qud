using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000515 RID: 1301
	internal sealed class SignatureOnlyPropertySymbol : PropertySymbol
	{
		// Token: 0x06006D72 RID: 28018 RVA: 0x00192B34 File Offset: 0x00190D34
		public SignatureOnlyPropertySymbol(string name, TypeSymbol containingType, ImmutableArray<ParameterSymbol> parameters, RefKind refKind, TypeWithAnnotations type, ImmutableArray<CustomModifier> refCustomModifiers, bool isStatic, ImmutableArray<PropertySymbol> explicitInterfaceImplementations)
		{
			this._refKind = refKind;
			this._type = type;
			this._refCustomModifiers = refCustomModifiers;
			this._isStatic = isStatic;
			this._parameters = parameters;
			this._explicitInterfaceImplementations = explicitInterfaceImplementations.NullToEmpty<PropertySymbol>();
			this._containingType = containingType;
			this._name = name;
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x06006D73 RID: 28019 RVA: 0x00192B89 File Offset: 0x00190D89
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x06006D74 RID: 28020 RVA: 0x00192B91 File Offset: 0x00190D91
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x06006D75 RID: 28021 RVA: 0x00192B99 File Offset: 0x00190D99
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._refCustomModifiers;
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x06006D76 RID: 28022 RVA: 0x00192BA1 File Offset: 0x00190DA1
		public override bool IsStatic
		{
			get
			{
				return this._isStatic;
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x06006D77 RID: 28023 RVA: 0x00192BA9 File Offset: 0x00190DA9
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x06006D78 RID: 28024 RVA: 0x00192BB1 File Offset: 0x00190DB1
		public override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return this._explicitInterfaceImplementations;
			}
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x06006D79 RID: 28025 RVA: 0x00192BB9 File Offset: 0x00190DB9
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x06006D7A RID: 28026 RVA: 0x00192BC1 File Offset: 0x00190DC1
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x06006D7B RID: 28027 RVA: 0x00192BC9 File Offset: 0x00190DC9
		internal override bool HasSpecialName
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x06006D7C RID: 28028 RVA: 0x00192BD0 File Offset: 0x00190DD0
		internal override CallingConvention CallingConvention
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x06006D7D RID: 28029 RVA: 0x00192BD7 File Offset: 0x00190DD7
		public override ImmutableArray<Location> Locations
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x06006D7E RID: 28030 RVA: 0x00192BDE File Offset: 0x00190DDE
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x06006D7F RID: 28031 RVA: 0x00192BE5 File Offset: 0x00190DE5
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x06006D80 RID: 28032 RVA: 0x00192BEC File Offset: 0x00190DEC
		public override bool IsVirtual
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x06006D81 RID: 28033 RVA: 0x00192BF3 File Offset: 0x00190DF3
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x06006D82 RID: 28034 RVA: 0x00192BF6 File Offset: 0x00190DF6
		public override bool IsAbstract
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x06006D83 RID: 28035 RVA: 0x00192BFD File Offset: 0x00190DFD
		public override bool IsSealed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x06006D84 RID: 28036 RVA: 0x00192C04 File Offset: 0x00190E04
		public override bool IsExtern
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x06006D85 RID: 28037 RVA: 0x00192C0B File Offset: 0x00190E0B
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x06006D86 RID: 28038 RVA: 0x00192C12 File Offset: 0x00190E12
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x06006D87 RID: 28039 RVA: 0x00192C19 File Offset: 0x00190E19
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x06006D88 RID: 28040 RVA: 0x00192C20 File Offset: 0x00190E20
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x06006D89 RID: 28041 RVA: 0x00192C27 File Offset: 0x00190E27
		public override MethodSymbol SetMethod
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x06006D8A RID: 28042 RVA: 0x00192C2E File Offset: 0x00190E2E
		public override MethodSymbol GetMethod
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x06006D8B RID: 28043 RVA: 0x00192C35 File Offset: 0x00190E35
		public override bool IsIndexer
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x04001B03 RID: 6915
		private readonly string _name;

		// Token: 0x04001B04 RID: 6916
		private readonly TypeSymbol _containingType;

		// Token: 0x04001B05 RID: 6917
		private readonly ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001B06 RID: 6918
		private readonly RefKind _refKind;

		// Token: 0x04001B07 RID: 6919
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001B08 RID: 6920
		private readonly ImmutableArray<CustomModifier> _refCustomModifiers;

		// Token: 0x04001B09 RID: 6921
		private readonly bool _isStatic;

		// Token: 0x04001B0A RID: 6922
		private readonly ImmutableArray<PropertySymbol> _explicitInterfaceImplementations;
	}
}
