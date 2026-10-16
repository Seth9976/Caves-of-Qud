using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000256 RID: 598
	public enum MethodKind
	{
		// Token: 0x04000927 RID: 2343
		AnonymousFunction,
		// Token: 0x04000928 RID: 2344
		LambdaMethod = 0,
		// Token: 0x04000929 RID: 2345
		Constructor,
		// Token: 0x0400092A RID: 2346
		Conversion,
		// Token: 0x0400092B RID: 2347
		DelegateInvoke,
		// Token: 0x0400092C RID: 2348
		Destructor,
		// Token: 0x0400092D RID: 2349
		EventAdd,
		// Token: 0x0400092E RID: 2350
		EventRaise,
		// Token: 0x0400092F RID: 2351
		EventRemove,
		// Token: 0x04000930 RID: 2352
		ExplicitInterfaceImplementation,
		// Token: 0x04000931 RID: 2353
		UserDefinedOperator,
		// Token: 0x04000932 RID: 2354
		Ordinary,
		// Token: 0x04000933 RID: 2355
		PropertyGet,
		// Token: 0x04000934 RID: 2356
		PropertySet,
		// Token: 0x04000935 RID: 2357
		ReducedExtension,
		// Token: 0x04000936 RID: 2358
		StaticConstructor,
		// Token: 0x04000937 RID: 2359
		SharedConstructor = 14,
		// Token: 0x04000938 RID: 2360
		BuiltinOperator,
		// Token: 0x04000939 RID: 2361
		DeclareMethod,
		// Token: 0x0400093A RID: 2362
		LocalFunction,
		// Token: 0x0400093B RID: 2363
		FunctionPointerSignature
	}
}
