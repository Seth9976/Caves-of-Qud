using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200013B RID: 315
	internal sealed class LazyMissingNonNullTypesContextDiagnosticInfo : LazyDiagnosticInfo
	{
		// Token: 0x06001916 RID: 6422 RVA: 0x0008733F File Offset: 0x0008553F
		private LazyMissingNonNullTypesContextDiagnosticInfo(TypeWithAnnotations type, DiagnosticInfo info)
		{
			this._type = type;
			this._info = info;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00087358 File Offset: 0x00085558
		public static void AddAll(bool isNullableEnabled, bool isGeneratedCode, TypeWithAnnotations type, Location location, DiagnosticBag diagnostics)
		{
			ArrayBuilder<DiagnosticInfo> instance = ArrayBuilder<DiagnosticInfo>.GetInstance();
			LazyMissingNonNullTypesContextDiagnosticInfo.GetRawDiagnosticInfos(isNullableEnabled, isGeneratedCode, (CSharpSyntaxTree)location.SourceTree, instance);
			foreach (DiagnosticInfo diagnosticInfo in instance)
			{
				diagnostics.Add(new LazyMissingNonNullTypesContextDiagnosticInfo(type, diagnosticInfo), location);
			}
			instance.Free();
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000873B0 File Offset: 0x000855B0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static void GetRawDiagnosticInfos(bool isNullableEnabled, bool isGeneratedCode, CSharpSyntaxTree tree, ArrayBuilder<DiagnosticInfo> infos)
		{
			CSDiagnosticInfo featureAvailabilityDiagnosticInfo = MessageID.IDS_FeatureNullableReferenceTypes.GetFeatureAvailabilityDiagnosticInfo(tree.Options);
			if (featureAvailabilityDiagnosticInfo != null)
			{
				infos.Add(featureAvailabilityDiagnosticInfo);
			}
			if (!isNullableEnabled && (featureAvailabilityDiagnosticInfo == null || featureAvailabilityDiagnosticInfo.Severity != DiagnosticSeverity.Error))
			{
				ErrorCode errorCode = (isGeneratedCode ? ErrorCode.WRN_MissingNonNullTypesContextForAnnotationInGeneratedCode : ErrorCode.WRN_MissingNonNullTypesContextForAnnotation);
				infos.Add(new CSDiagnosticInfo(errorCode));
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0008740B File Offset: 0x0008560B
		private static bool IsNullableReference(TypeSymbol type)
		{
			return type == null || (!type.IsValueType && !type.IsErrorType());
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00087425 File Offset: 0x00085625
		protected override DiagnosticInfo ResolveInfo()
		{
			if (!LazyMissingNonNullTypesContextDiagnosticInfo.IsNullableReference(this._type.Type))
			{
				return null;
			}
			return this._info;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00087441 File Offset: 0x00085641
		public static void ReportNullableReferenceTypesIfNeeded(bool isNullableEnabled, bool isGeneratedCode, TypeWithAnnotations type, Location location, DiagnosticBag diagnostics)
		{
			if (LazyMissingNonNullTypesContextDiagnosticInfo.IsNullableReference(type.Type))
			{
				LazyMissingNonNullTypesContextDiagnosticInfo.ReportNullableReferenceTypesIfNeeded(isNullableEnabled, isGeneratedCode, location, diagnostics);
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0008745C File Offset: 0x0008565C
		public static void ReportNullableReferenceTypesIfNeeded(bool isNullableEnabled, bool isGeneratedCode, Location location, DiagnosticBag diagnostics)
		{
			ArrayBuilder<DiagnosticInfo> instance = ArrayBuilder<DiagnosticInfo>.GetInstance();
			LazyMissingNonNullTypesContextDiagnosticInfo.GetRawDiagnosticInfos(isNullableEnabled, isGeneratedCode, (CSharpSyntaxTree)location.SourceTree, instance);
			foreach (DiagnosticInfo diagnosticInfo in instance)
			{
				diagnostics.Add(diagnosticInfo, location);
			}
			instance.Free();
		}

		// Token: 0x04000D07 RID: 3335
		private readonly TypeWithAnnotations _type;

		// Token: 0x04000D08 RID: 3336
		private readonly DiagnosticInfo _info;
	}
}
