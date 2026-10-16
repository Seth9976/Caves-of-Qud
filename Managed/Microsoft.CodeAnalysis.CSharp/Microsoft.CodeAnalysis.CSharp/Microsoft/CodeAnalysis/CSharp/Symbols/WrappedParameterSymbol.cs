using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005B4 RID: 1460
	internal abstract class WrappedParameterSymbol : ParameterSymbol
	{
		// Token: 0x06007AB7 RID: 31415 RVA: 0x001BFA45 File Offset: 0x001BDC45
		protected WrappedParameterSymbol(ParameterSymbol underlyingParameter)
		{
			this._underlyingParameter = underlyingParameter;
		}

		// Token: 0x1700233D RID: 9021
		// (get) Token: 0x06007AB8 RID: 31416 RVA: 0x001BFA54 File Offset: 0x001BDC54
		public ParameterSymbol UnderlyingParameter
		{
			get
			{
				return this._underlyingParameter;
			}
		}

		// Token: 0x1700233E RID: 9022
		// (get) Token: 0x06007AB9 RID: 31417 RVA: 0x001BFA5C File Offset: 0x001BDC5C
		public sealed override bool IsDiscard
		{
			get
			{
				return this._underlyingParameter.IsDiscard;
			}
		}

		// Token: 0x1700233F RID: 9023
		// (get) Token: 0x06007ABA RID: 31418 RVA: 0x001BFA69 File Offset: 0x001BDC69
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._underlyingParameter.TypeWithAnnotations;
			}
		}

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x06007ABB RID: 31419 RVA: 0x001BFA76 File Offset: 0x001BDC76
		public sealed override RefKind RefKind
		{
			get
			{
				return this._underlyingParameter.RefKind;
			}
		}

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x06007ABC RID: 31420 RVA: 0x001BFA83 File Offset: 0x001BDC83
		internal sealed override bool IsMetadataIn
		{
			get
			{
				return this._underlyingParameter.IsMetadataIn;
			}
		}

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x06007ABD RID: 31421 RVA: 0x001BFA90 File Offset: 0x001BDC90
		internal sealed override bool IsMetadataOut
		{
			get
			{
				return this._underlyingParameter.IsMetadataOut;
			}
		}

		// Token: 0x17002343 RID: 9027
		// (get) Token: 0x06007ABE RID: 31422 RVA: 0x001BFA9D File Offset: 0x001BDC9D
		public sealed override ImmutableArray<Location> Locations
		{
			get
			{
				return this._underlyingParameter.Locations;
			}
		}

		// Token: 0x17002344 RID: 9028
		// (get) Token: 0x06007ABF RID: 31423 RVA: 0x001BFAAA File Offset: 0x001BDCAA
		public sealed override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this._underlyingParameter.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x06007AC0 RID: 31424 RVA: 0x001BFAB7 File Offset: 0x001BDCB7
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this._underlyingParameter.GetAttributes();
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x001BFAC4 File Offset: 0x001BDCC4
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			this._underlyingParameter.AddSynthesizedAttributes(moduleBuilder, ref attributes);
		}

		// Token: 0x17002345 RID: 9029
		// (get) Token: 0x06007AC2 RID: 31426 RVA: 0x001BFAD3 File Offset: 0x001BDCD3
		internal sealed override ConstantValue ExplicitDefaultConstantValue
		{
			get
			{
				return this._underlyingParameter.ExplicitDefaultConstantValue;
			}
		}

		// Token: 0x17002346 RID: 9030
		// (get) Token: 0x06007AC3 RID: 31427 RVA: 0x001BFAE0 File Offset: 0x001BDCE0
		public override int Ordinal
		{
			get
			{
				return this._underlyingParameter.Ordinal;
			}
		}

		// Token: 0x17002347 RID: 9031
		// (get) Token: 0x06007AC4 RID: 31428 RVA: 0x001BFAED File Offset: 0x001BDCED
		public override bool IsParams
		{
			get
			{
				return this._underlyingParameter.IsParams;
			}
		}

		// Token: 0x17002348 RID: 9032
		// (get) Token: 0x06007AC5 RID: 31429 RVA: 0x001BFAFA File Offset: 0x001BDCFA
		internal override bool IsMetadataOptional
		{
			get
			{
				return this._underlyingParameter.IsMetadataOptional;
			}
		}

		// Token: 0x17002349 RID: 9033
		// (get) Token: 0x06007AC6 RID: 31430 RVA: 0x001BFB07 File Offset: 0x001BDD07
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingParameter.IsImplicitlyDeclared;
			}
		}

		// Token: 0x1700234A RID: 9034
		// (get) Token: 0x06007AC7 RID: 31431 RVA: 0x001BFB14 File Offset: 0x001BDD14
		public sealed override string Name
		{
			get
			{
				return this._underlyingParameter.Name;
			}
		}

		// Token: 0x1700234B RID: 9035
		// (get) Token: 0x06007AC8 RID: 31432 RVA: 0x001BFB21 File Offset: 0x001BDD21
		public sealed override string MetadataName
		{
			get
			{
				return this._underlyingParameter.MetadataName;
			}
		}

		// Token: 0x1700234C RID: 9036
		// (get) Token: 0x06007AC9 RID: 31433 RVA: 0x001BFB2E File Offset: 0x001BDD2E
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._underlyingParameter.RefCustomModifiers;
			}
		}

		// Token: 0x1700234D RID: 9037
		// (get) Token: 0x06007ACA RID: 31434 RVA: 0x001BFB3B File Offset: 0x001BDD3B
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return this._underlyingParameter.MarshallingInformation;
			}
		}

		// Token: 0x1700234E RID: 9038
		// (get) Token: 0x06007ACB RID: 31435 RVA: 0x001BFB48 File Offset: 0x001BDD48
		internal override UnmanagedType MarshallingType
		{
			get
			{
				return this._underlyingParameter.MarshallingType;
			}
		}

		// Token: 0x1700234F RID: 9039
		// (get) Token: 0x06007ACC RID: 31436 RVA: 0x001BFB55 File Offset: 0x001BDD55
		internal override bool IsIDispatchConstant
		{
			get
			{
				return this._underlyingParameter.IsIDispatchConstant;
			}
		}

		// Token: 0x17002350 RID: 9040
		// (get) Token: 0x06007ACD RID: 31437 RVA: 0x001BFB62 File Offset: 0x001BDD62
		internal override bool IsIUnknownConstant
		{
			get
			{
				return this._underlyingParameter.IsIUnknownConstant;
			}
		}

		// Token: 0x17002351 RID: 9041
		// (get) Token: 0x06007ACE RID: 31438 RVA: 0x001BFB6F File Offset: 0x001BDD6F
		internal override bool IsCallerLineNumber
		{
			get
			{
				return this._underlyingParameter.IsCallerLineNumber;
			}
		}

		// Token: 0x17002352 RID: 9042
		// (get) Token: 0x06007ACF RID: 31439 RVA: 0x001BFB7C File Offset: 0x001BDD7C
		internal override bool IsCallerFilePath
		{
			get
			{
				return this._underlyingParameter.IsCallerFilePath;
			}
		}

		// Token: 0x17002353 RID: 9043
		// (get) Token: 0x06007AD0 RID: 31440 RVA: 0x001BFB89 File Offset: 0x001BDD89
		internal override bool IsCallerMemberName
		{
			get
			{
				return this._underlyingParameter.IsCallerMemberName;
			}
		}

		// Token: 0x17002354 RID: 9044
		// (get) Token: 0x06007AD1 RID: 31441 RVA: 0x001BFB96 File Offset: 0x001BDD96
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return this._underlyingParameter.FlowAnalysisAnnotations;
			}
		}

		// Token: 0x17002355 RID: 9045
		// (get) Token: 0x06007AD2 RID: 31442 RVA: 0x001BFBA3 File Offset: 0x001BDDA3
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return this._underlyingParameter.NotNullIfParameterNotNull;
			}
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x001BFBB0 File Offset: 0x001BDDB0
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingParameter.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x04001D30 RID: 7472
		protected readonly ParameterSymbol _underlyingParameter;
	}
}
