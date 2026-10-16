using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200004B RID: 75
	internal enum ConversionKind : byte
	{
		// Token: 0x040000EF RID: 239
		UnsetConversionKind,
		// Token: 0x040000F0 RID: 240
		NoConversion,
		// Token: 0x040000F1 RID: 241
		Identity,
		// Token: 0x040000F2 RID: 242
		ImplicitNumeric,
		// Token: 0x040000F3 RID: 243
		ImplicitEnumeration,
		// Token: 0x040000F4 RID: 244
		ImplicitThrow,
		// Token: 0x040000F5 RID: 245
		ImplicitTupleLiteral,
		// Token: 0x040000F6 RID: 246
		ImplicitTuple,
		// Token: 0x040000F7 RID: 247
		ExplicitTupleLiteral,
		// Token: 0x040000F8 RID: 248
		ExplicitTuple,
		// Token: 0x040000F9 RID: 249
		ImplicitNullable,
		// Token: 0x040000FA RID: 250
		NullLiteral,
		// Token: 0x040000FB RID: 251
		ImplicitReference,
		// Token: 0x040000FC RID: 252
		Boxing,
		// Token: 0x040000FD RID: 253
		ImplicitPointerToVoid,
		// Token: 0x040000FE RID: 254
		ImplicitNullToPointer,
		// Token: 0x040000FF RID: 255
		ImplicitPointer,
		// Token: 0x04000100 RID: 256
		ImplicitDynamic,
		// Token: 0x04000101 RID: 257
		ExplicitDynamic,
		// Token: 0x04000102 RID: 258
		ImplicitConstant,
		// Token: 0x04000103 RID: 259
		ImplicitUserDefined,
		// Token: 0x04000104 RID: 260
		AnonymousFunction,
		// Token: 0x04000105 RID: 261
		MethodGroup,
		// Token: 0x04000106 RID: 262
		ExplicitNumeric,
		// Token: 0x04000107 RID: 263
		ExplicitEnumeration,
		// Token: 0x04000108 RID: 264
		ExplicitNullable,
		// Token: 0x04000109 RID: 265
		ExplicitReference,
		// Token: 0x0400010A RID: 266
		Unboxing,
		// Token: 0x0400010B RID: 267
		ExplicitUserDefined,
		// Token: 0x0400010C RID: 268
		ExplicitPointerToPointer,
		// Token: 0x0400010D RID: 269
		ExplicitIntegerToPointer,
		// Token: 0x0400010E RID: 270
		ExplicitPointerToInteger,
		// Token: 0x0400010F RID: 271
		IntPtr,
		// Token: 0x04000110 RID: 272
		InterpolatedString,
		// Token: 0x04000111 RID: 273
		SwitchExpression,
		// Token: 0x04000112 RID: 274
		ConditionalExpression,
		// Token: 0x04000113 RID: 275
		Deconstruction,
		// Token: 0x04000114 RID: 276
		StackAllocToPointerType,
		// Token: 0x04000115 RID: 277
		StackAllocToSpanType,
		// Token: 0x04000116 RID: 278
		PinnedObjectToPointer,
		// Token: 0x04000117 RID: 279
		DefaultLiteral,
		// Token: 0x04000118 RID: 280
		ObjectCreation
	}
}
