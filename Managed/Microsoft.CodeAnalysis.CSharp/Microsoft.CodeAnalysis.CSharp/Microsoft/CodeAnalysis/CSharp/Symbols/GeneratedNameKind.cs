using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200056C RID: 1388
	internal enum GeneratedNameKind
	{
		// Token: 0x04001C70 RID: 7280
		None,
		// Token: 0x04001C71 RID: 7281
		ThisProxyField = 52,
		// Token: 0x04001C72 RID: 7282
		HoistedLocalField,
		// Token: 0x04001C73 RID: 7283
		DisplayClassLocalOrField = 56,
		// Token: 0x04001C74 RID: 7284
		LambdaMethod = 98,
		// Token: 0x04001C75 RID: 7285
		LambdaDisplayClass,
		// Token: 0x04001C76 RID: 7286
		StateMachineType,
		// Token: 0x04001C77 RID: 7287
		LocalFunction = 103,
		// Token: 0x04001C78 RID: 7288
		AwaiterField = 117,
		// Token: 0x04001C79 RID: 7289
		HoistedSynthesizedLocalField = 115,
		// Token: 0x04001C7A RID: 7290
		StateMachineStateField = 49,
		// Token: 0x04001C7B RID: 7291
		IteratorCurrentBackingField,
		// Token: 0x04001C7C RID: 7292
		StateMachineParameterProxyField,
		// Token: 0x04001C7D RID: 7293
		ReusableHoistedLocalField = 55,
		// Token: 0x04001C7E RID: 7294
		LambdaCacheField = 57,
		// Token: 0x04001C7F RID: 7295
		FixedBufferField = 101,
		// Token: 0x04001C80 RID: 7296
		AnonymousType,
		// Token: 0x04001C81 RID: 7297
		TransparentIdentifier = 104,
		// Token: 0x04001C82 RID: 7298
		AnonymousTypeField,
		// Token: 0x04001C83 RID: 7299
		AutoPropertyBackingField = 107,
		// Token: 0x04001C84 RID: 7300
		IteratorCurrentThreadIdField,
		// Token: 0x04001C85 RID: 7301
		IteratorFinallyMethod,
		// Token: 0x04001C86 RID: 7302
		BaseMethodWrapper,
		// Token: 0x04001C87 RID: 7303
		AsyncBuilderField = 116,
		// Token: 0x04001C88 RID: 7304
		DynamicCallSiteContainerType = 111,
		// Token: 0x04001C89 RID: 7305
		DynamicCallSiteField,
		// Token: 0x04001C8A RID: 7306
		AsyncIteratorPromiseOfValueOrEndBackingField = 118,
		// Token: 0x04001C8B RID: 7307
		DisposeModeField,
		// Token: 0x04001C8C RID: 7308
		CombinedTokensField,
		// Token: 0x04001C8D RID: 7309
		[Obsolete]
		Deprecated_OuterscopeLocals = 54,
		// Token: 0x04001C8E RID: 7310
		[Obsolete]
		Deprecated_IteratorInstance = 97,
		// Token: 0x04001C8F RID: 7311
		[Obsolete]
		Deprecated_InitializerLocal = 103,
		// Token: 0x04001C90 RID: 7312
		[Obsolete]
		Deprecated_AnonymousTypeTypeParameter = 106,
		// Token: 0x04001C91 RID: 7313
		[Obsolete]
		Deprecated_DynamicDelegate = 113,
		// Token: 0x04001C92 RID: 7314
		[Obsolete]
		Deprecated_ComrefCallLocal
	}
}
