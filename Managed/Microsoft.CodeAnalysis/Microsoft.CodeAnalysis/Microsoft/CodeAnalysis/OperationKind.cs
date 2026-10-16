using System;
using System.ComponentModel;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000190 RID: 400
	public enum OperationKind
	{
		// Token: 0x0400048D RID: 1165
		None,
		// Token: 0x0400048E RID: 1166
		Invalid,
		// Token: 0x0400048F RID: 1167
		Block,
		// Token: 0x04000490 RID: 1168
		VariableDeclarationGroup,
		// Token: 0x04000491 RID: 1169
		Switch,
		// Token: 0x04000492 RID: 1170
		Loop,
		// Token: 0x04000493 RID: 1171
		Labeled,
		// Token: 0x04000494 RID: 1172
		Branch,
		// Token: 0x04000495 RID: 1173
		Empty,
		// Token: 0x04000496 RID: 1174
		Return,
		// Token: 0x04000497 RID: 1175
		YieldBreak,
		// Token: 0x04000498 RID: 1176
		Lock,
		// Token: 0x04000499 RID: 1177
		Try,
		// Token: 0x0400049A RID: 1178
		Using,
		// Token: 0x0400049B RID: 1179
		YieldReturn,
		// Token: 0x0400049C RID: 1180
		ExpressionStatement,
		// Token: 0x0400049D RID: 1181
		LocalFunction,
		// Token: 0x0400049E RID: 1182
		Stop,
		// Token: 0x0400049F RID: 1183
		End,
		// Token: 0x040004A0 RID: 1184
		RaiseEvent,
		// Token: 0x040004A1 RID: 1185
		Literal,
		// Token: 0x040004A2 RID: 1186
		Conversion,
		// Token: 0x040004A3 RID: 1187
		Invocation,
		// Token: 0x040004A4 RID: 1188
		ArrayElementReference,
		// Token: 0x040004A5 RID: 1189
		LocalReference,
		// Token: 0x040004A6 RID: 1190
		ParameterReference,
		// Token: 0x040004A7 RID: 1191
		FieldReference,
		// Token: 0x040004A8 RID: 1192
		MethodReference,
		// Token: 0x040004A9 RID: 1193
		PropertyReference,
		// Token: 0x040004AA RID: 1194
		EventReference = 30,
		// Token: 0x040004AB RID: 1195
		Unary,
		// Token: 0x040004AC RID: 1196
		[EditorBrowsable(1)]
		UnaryOperator = 31,
		// Token: 0x040004AD RID: 1197
		Binary,
		// Token: 0x040004AE RID: 1198
		[EditorBrowsable(1)]
		BinaryOperator = 32,
		// Token: 0x040004AF RID: 1199
		Conditional,
		// Token: 0x040004B0 RID: 1200
		Coalesce,
		// Token: 0x040004B1 RID: 1201
		AnonymousFunction,
		// Token: 0x040004B2 RID: 1202
		ObjectCreation,
		// Token: 0x040004B3 RID: 1203
		TypeParameterObjectCreation,
		// Token: 0x040004B4 RID: 1204
		ArrayCreation,
		// Token: 0x040004B5 RID: 1205
		InstanceReference,
		// Token: 0x040004B6 RID: 1206
		IsType,
		// Token: 0x040004B7 RID: 1207
		Await,
		// Token: 0x040004B8 RID: 1208
		SimpleAssignment,
		// Token: 0x040004B9 RID: 1209
		CompoundAssignment,
		// Token: 0x040004BA RID: 1210
		Parenthesized,
		// Token: 0x040004BB RID: 1211
		EventAssignment,
		// Token: 0x040004BC RID: 1212
		ConditionalAccess,
		// Token: 0x040004BD RID: 1213
		ConditionalAccessInstance,
		// Token: 0x040004BE RID: 1214
		InterpolatedString,
		// Token: 0x040004BF RID: 1215
		AnonymousObjectCreation,
		// Token: 0x040004C0 RID: 1216
		ObjectOrCollectionInitializer,
		// Token: 0x040004C1 RID: 1217
		MemberInitializer,
		// Token: 0x040004C2 RID: 1218
		[Obsolete("ICollectionElementInitializerOperation has been replaced with IInvocationOperation and IDynamicInvocationOperation", true)]
		CollectionElementInitializer,
		// Token: 0x040004C3 RID: 1219
		NameOf,
		// Token: 0x040004C4 RID: 1220
		Tuple,
		// Token: 0x040004C5 RID: 1221
		DynamicObjectCreation,
		// Token: 0x040004C6 RID: 1222
		DynamicMemberReference,
		// Token: 0x040004C7 RID: 1223
		DynamicInvocation,
		// Token: 0x040004C8 RID: 1224
		DynamicIndexerAccess,
		// Token: 0x040004C9 RID: 1225
		TranslatedQuery,
		// Token: 0x040004CA RID: 1226
		DelegateCreation,
		// Token: 0x040004CB RID: 1227
		DefaultValue,
		// Token: 0x040004CC RID: 1228
		TypeOf,
		// Token: 0x040004CD RID: 1229
		SizeOf,
		// Token: 0x040004CE RID: 1230
		AddressOf,
		// Token: 0x040004CF RID: 1231
		IsPattern,
		// Token: 0x040004D0 RID: 1232
		Increment,
		// Token: 0x040004D1 RID: 1233
		Throw,
		// Token: 0x040004D2 RID: 1234
		Decrement,
		// Token: 0x040004D3 RID: 1235
		DeconstructionAssignment,
		// Token: 0x040004D4 RID: 1236
		DeclarationExpression,
		// Token: 0x040004D5 RID: 1237
		OmittedArgument,
		// Token: 0x040004D6 RID: 1238
		FieldInitializer,
		// Token: 0x040004D7 RID: 1239
		VariableInitializer,
		// Token: 0x040004D8 RID: 1240
		PropertyInitializer,
		// Token: 0x040004D9 RID: 1241
		ParameterInitializer,
		// Token: 0x040004DA RID: 1242
		ArrayInitializer,
		// Token: 0x040004DB RID: 1243
		VariableDeclarator,
		// Token: 0x040004DC RID: 1244
		VariableDeclaration,
		// Token: 0x040004DD RID: 1245
		Argument,
		// Token: 0x040004DE RID: 1246
		CatchClause,
		// Token: 0x040004DF RID: 1247
		SwitchCase,
		// Token: 0x040004E0 RID: 1248
		CaseClause,
		// Token: 0x040004E1 RID: 1249
		InterpolatedStringText,
		// Token: 0x040004E2 RID: 1250
		Interpolation,
		// Token: 0x040004E3 RID: 1251
		ConstantPattern,
		// Token: 0x040004E4 RID: 1252
		DeclarationPattern,
		// Token: 0x040004E5 RID: 1253
		TupleBinary,
		// Token: 0x040004E6 RID: 1254
		[EditorBrowsable(1)]
		TupleBinaryOperator = 87,
		// Token: 0x040004E7 RID: 1255
		MethodBody,
		// Token: 0x040004E8 RID: 1256
		[EditorBrowsable(1)]
		MethodBodyOperation = 88,
		// Token: 0x040004E9 RID: 1257
		ConstructorBody,
		// Token: 0x040004EA RID: 1258
		[EditorBrowsable(1)]
		ConstructorBodyOperation = 89,
		// Token: 0x040004EB RID: 1259
		Discard,
		// Token: 0x040004EC RID: 1260
		FlowCapture,
		// Token: 0x040004ED RID: 1261
		FlowCaptureReference,
		// Token: 0x040004EE RID: 1262
		IsNull,
		// Token: 0x040004EF RID: 1263
		CaughtException,
		// Token: 0x040004F0 RID: 1264
		StaticLocalInitializationSemaphore,
		// Token: 0x040004F1 RID: 1265
		FlowAnonymousFunction,
		// Token: 0x040004F2 RID: 1266
		CoalesceAssignment,
		// Token: 0x040004F3 RID: 1267
		Range = 99,
		// Token: 0x040004F4 RID: 1268
		ReDim = 101,
		// Token: 0x040004F5 RID: 1269
		ReDimClause,
		// Token: 0x040004F6 RID: 1270
		RecursivePattern,
		// Token: 0x040004F7 RID: 1271
		DiscardPattern,
		// Token: 0x040004F8 RID: 1272
		SwitchExpression,
		// Token: 0x040004F9 RID: 1273
		SwitchExpressionArm,
		// Token: 0x040004FA RID: 1274
		PropertySubpattern,
		// Token: 0x040004FB RID: 1275
		UsingDeclaration,
		// Token: 0x040004FC RID: 1276
		NegatedPattern,
		// Token: 0x040004FD RID: 1277
		BinaryPattern,
		// Token: 0x040004FE RID: 1278
		TypePattern,
		// Token: 0x040004FF RID: 1279
		RelationalPattern,
		// Token: 0x04000500 RID: 1280
		With
	}
}
