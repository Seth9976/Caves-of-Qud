using System;
using System.Collections.Immutable;
using System.Linq;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000514 RID: 1300
	internal sealed class SignatureOnlyParameterSymbol : ParameterSymbol
	{
		// Token: 0x06006D56 RID: 27990 RVA: 0x00192973 File Offset: 0x00190B73
		public SignatureOnlyParameterSymbol(TypeWithAnnotations type, ImmutableArray<CustomModifier> refCustomModifiers, bool isParams, RefKind refKind)
		{
			this._type = type;
			this._refCustomModifiers = refCustomModifiers;
			this._isParams = isParams;
			this._refKind = refKind;
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x06006D57 RID: 27991 RVA: 0x00192998 File Offset: 0x00190B98
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06006D58 RID: 27992 RVA: 0x001929A0 File Offset: 0x00190BA0
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._refCustomModifiers;
			}
		}

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x06006D59 RID: 27993 RVA: 0x001929A8 File Offset: 0x00190BA8
		public override bool IsParams
		{
			get
			{
				return this._isParams;
			}
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x06006D5A RID: 27994 RVA: 0x001929B0 File Offset: 0x00190BB0
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x06006D5B RID: 27995 RVA: 0x001929B8 File Offset: 0x00190BB8
		public override string Name
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x06006D5C RID: 27996 RVA: 0x001929BF File Offset: 0x00190BBF
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x06006D5D RID: 27997 RVA: 0x001929C2 File Offset: 0x00190BC2
		public override bool IsDiscard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x06006D5E RID: 27998 RVA: 0x001929C5 File Offset: 0x00190BC5
		internal override bool IsMetadataIn
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x06006D5F RID: 27999 RVA: 0x001929CC File Offset: 0x00190BCC
		internal override bool IsMetadataOut
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x06006D60 RID: 28000 RVA: 0x001929D3 File Offset: 0x00190BD3
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x06006D61 RID: 28001 RVA: 0x001929DA File Offset: 0x00190BDA
		public override int Ordinal
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06006D62 RID: 28002 RVA: 0x001929E1 File Offset: 0x00190BE1
		internal override bool IsMetadataOptional
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06006D63 RID: 28003 RVA: 0x001929E8 File Offset: 0x00190BE8
		internal override ConstantValue ExplicitDefaultConstantValue
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06006D64 RID: 28004 RVA: 0x001929EF File Offset: 0x00190BEF
		internal override bool IsIDispatchConstant
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x06006D65 RID: 28005 RVA: 0x001929F6 File Offset: 0x00190BF6
		internal override bool IsIUnknownConstant
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x06006D66 RID: 28006 RVA: 0x001929FD File Offset: 0x00190BFD
		internal override bool IsCallerFilePath
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x06006D67 RID: 28007 RVA: 0x00192A04 File Offset: 0x00190C04
		internal override bool IsCallerLineNumber
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x06006D68 RID: 28008 RVA: 0x00192A0B File Offset: 0x00190C0B
		internal override bool IsCallerMemberName
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x06006D69 RID: 28009 RVA: 0x00192A12 File Offset: 0x00190C12
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x06006D6A RID: 28010 RVA: 0x00192A19 File Offset: 0x00190C19
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x06006D6B RID: 28011 RVA: 0x00192A20 File Offset: 0x00190C20
		public override Symbol ContainingSymbol
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x06006D6C RID: 28012 RVA: 0x00192A27 File Offset: 0x00190C27
		public override ImmutableArray<Location> Locations
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x06006D6D RID: 28013 RVA: 0x00192A2E File Offset: 0x00190C2E
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x06006D6E RID: 28014 RVA: 0x00192A35 File Offset: 0x00190C35
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06006D6F RID: 28015 RVA: 0x00192A3C File Offset: 0x00190C3C
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06006D70 RID: 28016 RVA: 0x00192A44 File Offset: 0x00190C44
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (this == obj)
			{
				return true;
			}
			SignatureOnlyParameterSymbol signatureOnlyParameterSymbol = obj as SignatureOnlyParameterSymbol;
			return signatureOnlyParameterSymbol != null && TypeSymbol.Equals(this._type.Type, signatureOnlyParameterSymbol._type.Type, compareKind) && this._type.CustomModifiers.Equals(signatureOnlyParameterSymbol._type.CustomModifiers) && this._refCustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(signatureOnlyParameterSymbol._refCustomModifiers, null) && this._isParams == signatureOnlyParameterSymbol._isParams && this._refKind == signatureOnlyParameterSymbol._refKind;
		}

		// Token: 0x06006D71 RID: 28017 RVA: 0x00192AD4 File Offset: 0x00190CD4
		public override int GetHashCode()
		{
			return Hash.Combine(this._type.Type.GetHashCode(), Hash.Combine(Hash.CombineValues<CustomModifier>(this._type.CustomModifiers, int.MaxValue), Hash.Combine(this._isParams.GetHashCode(), this._refKind.GetHashCode())));
		}

		// Token: 0x04001AFF RID: 6911
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001B00 RID: 6912
		private readonly ImmutableArray<CustomModifier> _refCustomModifiers;

		// Token: 0x04001B01 RID: 6913
		private readonly bool _isParams;

		// Token: 0x04001B02 RID: 6914
		private readonly RefKind _refKind;
	}
}
