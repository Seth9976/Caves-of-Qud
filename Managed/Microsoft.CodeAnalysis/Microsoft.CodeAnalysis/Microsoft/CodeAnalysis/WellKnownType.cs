using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000292 RID: 658
	internal enum WellKnownType
	{
		// Token: 0x04000C21 RID: 3105
		Unknown,
		// Token: 0x04000C22 RID: 3106
		First = 46,
		// Token: 0x04000C23 RID: 3107
		System_Math = 46,
		// Token: 0x04000C24 RID: 3108
		System_Array,
		// Token: 0x04000C25 RID: 3109
		System_Attribute,
		// Token: 0x04000C26 RID: 3110
		System_CLSCompliantAttribute,
		// Token: 0x04000C27 RID: 3111
		System_Convert,
		// Token: 0x04000C28 RID: 3112
		System_Exception,
		// Token: 0x04000C29 RID: 3113
		System_FlagsAttribute,
		// Token: 0x04000C2A RID: 3114
		System_FormattableString,
		// Token: 0x04000C2B RID: 3115
		System_Guid,
		// Token: 0x04000C2C RID: 3116
		System_IFormattable,
		// Token: 0x04000C2D RID: 3117
		System_RuntimeTypeHandle,
		// Token: 0x04000C2E RID: 3118
		System_RuntimeFieldHandle,
		// Token: 0x04000C2F RID: 3119
		System_RuntimeMethodHandle,
		// Token: 0x04000C30 RID: 3120
		System_MarshalByRefObject,
		// Token: 0x04000C31 RID: 3121
		System_Type,
		// Token: 0x04000C32 RID: 3122
		System_Reflection_AssemblyKeyFileAttribute,
		// Token: 0x04000C33 RID: 3123
		System_Reflection_AssemblyKeyNameAttribute,
		// Token: 0x04000C34 RID: 3124
		System_Reflection_MethodInfo,
		// Token: 0x04000C35 RID: 3125
		System_Reflection_ConstructorInfo,
		// Token: 0x04000C36 RID: 3126
		System_Reflection_MethodBase,
		// Token: 0x04000C37 RID: 3127
		System_Reflection_FieldInfo,
		// Token: 0x04000C38 RID: 3128
		System_Reflection_MemberInfo,
		// Token: 0x04000C39 RID: 3129
		System_Reflection_Missing,
		// Token: 0x04000C3A RID: 3130
		System_Runtime_CompilerServices_FormattableStringFactory,
		// Token: 0x04000C3B RID: 3131
		System_Runtime_CompilerServices_RuntimeHelpers,
		// Token: 0x04000C3C RID: 3132
		System_Runtime_ExceptionServices_ExceptionDispatchInfo,
		// Token: 0x04000C3D RID: 3133
		System_Runtime_InteropServices_StructLayoutAttribute,
		// Token: 0x04000C3E RID: 3134
		System_Runtime_InteropServices_UnknownWrapper,
		// Token: 0x04000C3F RID: 3135
		System_Runtime_InteropServices_DispatchWrapper,
		// Token: 0x04000C40 RID: 3136
		System_Runtime_InteropServices_CallingConvention,
		// Token: 0x04000C41 RID: 3137
		System_Runtime_InteropServices_ClassInterfaceAttribute,
		// Token: 0x04000C42 RID: 3138
		System_Runtime_InteropServices_ClassInterfaceType,
		// Token: 0x04000C43 RID: 3139
		System_Runtime_InteropServices_CoClassAttribute,
		// Token: 0x04000C44 RID: 3140
		System_Runtime_InteropServices_ComAwareEventInfo,
		// Token: 0x04000C45 RID: 3141
		System_Runtime_InteropServices_ComEventInterfaceAttribute,
		// Token: 0x04000C46 RID: 3142
		System_Runtime_InteropServices_ComInterfaceType,
		// Token: 0x04000C47 RID: 3143
		System_Runtime_InteropServices_ComSourceInterfacesAttribute,
		// Token: 0x04000C48 RID: 3144
		System_Runtime_InteropServices_ComVisibleAttribute,
		// Token: 0x04000C49 RID: 3145
		System_Runtime_InteropServices_DispIdAttribute,
		// Token: 0x04000C4A RID: 3146
		System_Runtime_InteropServices_GuidAttribute,
		// Token: 0x04000C4B RID: 3147
		System_Runtime_InteropServices_InterfaceTypeAttribute,
		// Token: 0x04000C4C RID: 3148
		System_Runtime_InteropServices_Marshal,
		// Token: 0x04000C4D RID: 3149
		System_Runtime_InteropServices_TypeIdentifierAttribute,
		// Token: 0x04000C4E RID: 3150
		System_Runtime_InteropServices_BestFitMappingAttribute,
		// Token: 0x04000C4F RID: 3151
		System_Runtime_InteropServices_DefaultParameterValueAttribute,
		// Token: 0x04000C50 RID: 3152
		System_Runtime_InteropServices_LCIDConversionAttribute,
		// Token: 0x04000C51 RID: 3153
		System_Runtime_InteropServices_UnmanagedFunctionPointerAttribute,
		// Token: 0x04000C52 RID: 3154
		System_Activator,
		// Token: 0x04000C53 RID: 3155
		System_Threading_Tasks_Task,
		// Token: 0x04000C54 RID: 3156
		System_Threading_Tasks_Task_T,
		// Token: 0x04000C55 RID: 3157
		System_Threading_Interlocked,
		// Token: 0x04000C56 RID: 3158
		System_Threading_Monitor,
		// Token: 0x04000C57 RID: 3159
		System_Threading_Thread,
		// Token: 0x04000C58 RID: 3160
		Microsoft_CSharp_RuntimeBinder_Binder,
		// Token: 0x04000C59 RID: 3161
		Microsoft_CSharp_RuntimeBinder_CSharpArgumentInfo,
		// Token: 0x04000C5A RID: 3162
		Microsoft_CSharp_RuntimeBinder_CSharpArgumentInfoFlags,
		// Token: 0x04000C5B RID: 3163
		Microsoft_CSharp_RuntimeBinder_CSharpBinderFlags,
		// Token: 0x04000C5C RID: 3164
		Microsoft_VisualBasic_CallType,
		// Token: 0x04000C5D RID: 3165
		Microsoft_VisualBasic_Embedded,
		// Token: 0x04000C5E RID: 3166
		Microsoft_VisualBasic_CompilerServices_Conversions,
		// Token: 0x04000C5F RID: 3167
		Microsoft_VisualBasic_CompilerServices_Operators,
		// Token: 0x04000C60 RID: 3168
		Microsoft_VisualBasic_CompilerServices_NewLateBinding,
		// Token: 0x04000C61 RID: 3169
		Microsoft_VisualBasic_CompilerServices_EmbeddedOperators,
		// Token: 0x04000C62 RID: 3170
		Microsoft_VisualBasic_CompilerServices_StandardModuleAttribute,
		// Token: 0x04000C63 RID: 3171
		Microsoft_VisualBasic_CompilerServices_Utils,
		// Token: 0x04000C64 RID: 3172
		Microsoft_VisualBasic_CompilerServices_LikeOperator,
		// Token: 0x04000C65 RID: 3173
		Microsoft_VisualBasic_CompilerServices_ProjectData,
		// Token: 0x04000C66 RID: 3174
		Microsoft_VisualBasic_CompilerServices_ObjectFlowControl,
		// Token: 0x04000C67 RID: 3175
		Microsoft_VisualBasic_CompilerServices_ObjectFlowControl_ForLoopControl,
		// Token: 0x04000C68 RID: 3176
		Microsoft_VisualBasic_CompilerServices_StaticLocalInitFlag,
		// Token: 0x04000C69 RID: 3177
		Microsoft_VisualBasic_CompilerServices_StringType,
		// Token: 0x04000C6A RID: 3178
		Microsoft_VisualBasic_CompilerServices_IncompleteInitialization,
		// Token: 0x04000C6B RID: 3179
		Microsoft_VisualBasic_CompilerServices_Versioned,
		// Token: 0x04000C6C RID: 3180
		Microsoft_VisualBasic_CompareMethod,
		// Token: 0x04000C6D RID: 3181
		Microsoft_VisualBasic_Strings,
		// Token: 0x04000C6E RID: 3182
		Microsoft_VisualBasic_ErrObject,
		// Token: 0x04000C6F RID: 3183
		Microsoft_VisualBasic_FileSystem,
		// Token: 0x04000C70 RID: 3184
		Microsoft_VisualBasic_ApplicationServices_ApplicationBase,
		// Token: 0x04000C71 RID: 3185
		Microsoft_VisualBasic_ApplicationServices_WindowsFormsApplicationBase,
		// Token: 0x04000C72 RID: 3186
		Microsoft_VisualBasic_Information,
		// Token: 0x04000C73 RID: 3187
		Microsoft_VisualBasic_Interaction,
		// Token: 0x04000C74 RID: 3188
		System_Func_T,
		// Token: 0x04000C75 RID: 3189
		System_Func_T2,
		// Token: 0x04000C76 RID: 3190
		System_Func_T3,
		// Token: 0x04000C77 RID: 3191
		System_Func_T4,
		// Token: 0x04000C78 RID: 3192
		System_Func_T5,
		// Token: 0x04000C79 RID: 3193
		System_Func_T6,
		// Token: 0x04000C7A RID: 3194
		System_Func_T7,
		// Token: 0x04000C7B RID: 3195
		System_Func_T8,
		// Token: 0x04000C7C RID: 3196
		System_Func_T9,
		// Token: 0x04000C7D RID: 3197
		System_Func_T10,
		// Token: 0x04000C7E RID: 3198
		System_Func_T11,
		// Token: 0x04000C7F RID: 3199
		System_Func_T12,
		// Token: 0x04000C80 RID: 3200
		System_Func_T13,
		// Token: 0x04000C81 RID: 3201
		System_Func_T14,
		// Token: 0x04000C82 RID: 3202
		System_Func_T15,
		// Token: 0x04000C83 RID: 3203
		System_Func_T16,
		// Token: 0x04000C84 RID: 3204
		System_Func_T17,
		// Token: 0x04000C85 RID: 3205
		System_Func_TMax = 143,
		// Token: 0x04000C86 RID: 3206
		System_Action,
		// Token: 0x04000C87 RID: 3207
		System_Action_T,
		// Token: 0x04000C88 RID: 3208
		System_Action_T2,
		// Token: 0x04000C89 RID: 3209
		System_Action_T3,
		// Token: 0x04000C8A RID: 3210
		System_Action_T4,
		// Token: 0x04000C8B RID: 3211
		System_Action_T5,
		// Token: 0x04000C8C RID: 3212
		System_Action_T6,
		// Token: 0x04000C8D RID: 3213
		System_Action_T7,
		// Token: 0x04000C8E RID: 3214
		System_Action_T8,
		// Token: 0x04000C8F RID: 3215
		System_Action_T9,
		// Token: 0x04000C90 RID: 3216
		System_Action_T10,
		// Token: 0x04000C91 RID: 3217
		System_Action_T11,
		// Token: 0x04000C92 RID: 3218
		System_Action_T12,
		// Token: 0x04000C93 RID: 3219
		System_Action_T13,
		// Token: 0x04000C94 RID: 3220
		System_Action_T14,
		// Token: 0x04000C95 RID: 3221
		System_Action_T15,
		// Token: 0x04000C96 RID: 3222
		System_Action_T16,
		// Token: 0x04000C97 RID: 3223
		System_Action_TMax = 160,
		// Token: 0x04000C98 RID: 3224
		System_AttributeUsageAttribute,
		// Token: 0x04000C99 RID: 3225
		System_ParamArrayAttribute,
		// Token: 0x04000C9A RID: 3226
		System_NonSerializedAttribute,
		// Token: 0x04000C9B RID: 3227
		System_STAThreadAttribute,
		// Token: 0x04000C9C RID: 3228
		System_Reflection_DefaultMemberAttribute,
		// Token: 0x04000C9D RID: 3229
		System_Runtime_CompilerServices_DateTimeConstantAttribute,
		// Token: 0x04000C9E RID: 3230
		System_Runtime_CompilerServices_DecimalConstantAttribute,
		// Token: 0x04000C9F RID: 3231
		System_Runtime_CompilerServices_IUnknownConstantAttribute,
		// Token: 0x04000CA0 RID: 3232
		System_Runtime_CompilerServices_IDispatchConstantAttribute,
		// Token: 0x04000CA1 RID: 3233
		System_Runtime_CompilerServices_ExtensionAttribute,
		// Token: 0x04000CA2 RID: 3234
		System_Runtime_CompilerServices_INotifyCompletion,
		// Token: 0x04000CA3 RID: 3235
		System_Runtime_CompilerServices_InternalsVisibleToAttribute,
		// Token: 0x04000CA4 RID: 3236
		System_Runtime_CompilerServices_CompilerGeneratedAttribute,
		// Token: 0x04000CA5 RID: 3237
		System_Runtime_CompilerServices_AccessedThroughPropertyAttribute,
		// Token: 0x04000CA6 RID: 3238
		System_Runtime_CompilerServices_CompilationRelaxationsAttribute,
		// Token: 0x04000CA7 RID: 3239
		System_Runtime_CompilerServices_RuntimeCompatibilityAttribute,
		// Token: 0x04000CA8 RID: 3240
		System_Runtime_CompilerServices_UnsafeValueTypeAttribute,
		// Token: 0x04000CA9 RID: 3241
		System_Runtime_CompilerServices_FixedBufferAttribute,
		// Token: 0x04000CAA RID: 3242
		System_Runtime_CompilerServices_DynamicAttribute,
		// Token: 0x04000CAB RID: 3243
		System_Runtime_CompilerServices_CallSiteBinder,
		// Token: 0x04000CAC RID: 3244
		System_Runtime_CompilerServices_CallSite,
		// Token: 0x04000CAD RID: 3245
		System_Runtime_CompilerServices_CallSite_T,
		// Token: 0x04000CAE RID: 3246
		System_Runtime_InteropServices_WindowsRuntime_EventRegistrationToken,
		// Token: 0x04000CAF RID: 3247
		System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_T,
		// Token: 0x04000CB0 RID: 3248
		System_Runtime_InteropServices_WindowsRuntime_WindowsRuntimeMarshal,
		// Token: 0x04000CB1 RID: 3249
		Windows_Foundation_IAsyncAction,
		// Token: 0x04000CB2 RID: 3250
		Windows_Foundation_IAsyncActionWithProgress_T,
		// Token: 0x04000CB3 RID: 3251
		Windows_Foundation_IAsyncOperation_T,
		// Token: 0x04000CB4 RID: 3252
		Windows_Foundation_IAsyncOperationWithProgress_T2,
		// Token: 0x04000CB5 RID: 3253
		System_Diagnostics_Debugger,
		// Token: 0x04000CB6 RID: 3254
		System_Diagnostics_DebuggerDisplayAttribute,
		// Token: 0x04000CB7 RID: 3255
		System_Diagnostics_DebuggerNonUserCodeAttribute,
		// Token: 0x04000CB8 RID: 3256
		System_Diagnostics_DebuggerHiddenAttribute,
		// Token: 0x04000CB9 RID: 3257
		System_Diagnostics_DebuggerBrowsableAttribute,
		// Token: 0x04000CBA RID: 3258
		System_Diagnostics_DebuggerStepThroughAttribute,
		// Token: 0x04000CBB RID: 3259
		System_Diagnostics_DebuggerBrowsableState,
		// Token: 0x04000CBC RID: 3260
		System_Diagnostics_DebuggableAttribute,
		// Token: 0x04000CBD RID: 3261
		System_Diagnostics_DebuggableAttribute__DebuggingModes,
		// Token: 0x04000CBE RID: 3262
		System_ComponentModel_DesignerSerializationVisibilityAttribute,
		// Token: 0x04000CBF RID: 3263
		System_IEquatable_T,
		// Token: 0x04000CC0 RID: 3264
		System_Collections_IList,
		// Token: 0x04000CC1 RID: 3265
		System_Collections_ICollection,
		// Token: 0x04000CC2 RID: 3266
		System_Collections_Generic_EqualityComparer_T,
		// Token: 0x04000CC3 RID: 3267
		System_Collections_Generic_List_T,
		// Token: 0x04000CC4 RID: 3268
		System_Collections_Generic_IDictionary_KV,
		// Token: 0x04000CC5 RID: 3269
		System_Collections_Generic_IReadOnlyDictionary_KV,
		// Token: 0x04000CC6 RID: 3270
		System_Collections_ObjectModel_Collection_T,
		// Token: 0x04000CC7 RID: 3271
		System_Collections_ObjectModel_ReadOnlyCollection_T,
		// Token: 0x04000CC8 RID: 3272
		System_Collections_Specialized_INotifyCollectionChanged,
		// Token: 0x04000CC9 RID: 3273
		System_ComponentModel_INotifyPropertyChanged,
		// Token: 0x04000CCA RID: 3274
		System_ComponentModel_EditorBrowsableAttribute,
		// Token: 0x04000CCB RID: 3275
		System_ComponentModel_EditorBrowsableState,
		// Token: 0x04000CCC RID: 3276
		System_Linq_Enumerable,
		// Token: 0x04000CCD RID: 3277
		System_Linq_Expressions_Expression,
		// Token: 0x04000CCE RID: 3278
		System_Linq_Expressions_Expression_T,
		// Token: 0x04000CCF RID: 3279
		System_Linq_Expressions_ParameterExpression,
		// Token: 0x04000CD0 RID: 3280
		System_Linq_Expressions_ElementInit,
		// Token: 0x04000CD1 RID: 3281
		System_Linq_Expressions_MemberBinding,
		// Token: 0x04000CD2 RID: 3282
		System_Linq_Expressions_ExpressionType,
		// Token: 0x04000CD3 RID: 3283
		System_Linq_IQueryable,
		// Token: 0x04000CD4 RID: 3284
		System_Linq_IQueryable_T,
		// Token: 0x04000CD5 RID: 3285
		System_Xml_Linq_Extensions,
		// Token: 0x04000CD6 RID: 3286
		System_Xml_Linq_XAttribute,
		// Token: 0x04000CD7 RID: 3287
		System_Xml_Linq_XCData,
		// Token: 0x04000CD8 RID: 3288
		System_Xml_Linq_XComment,
		// Token: 0x04000CD9 RID: 3289
		System_Xml_Linq_XContainer,
		// Token: 0x04000CDA RID: 3290
		System_Xml_Linq_XDeclaration,
		// Token: 0x04000CDB RID: 3291
		System_Xml_Linq_XDocument,
		// Token: 0x04000CDC RID: 3292
		System_Xml_Linq_XElement,
		// Token: 0x04000CDD RID: 3293
		System_Xml_Linq_XName,
		// Token: 0x04000CDE RID: 3294
		System_Xml_Linq_XNamespace,
		// Token: 0x04000CDF RID: 3295
		System_Xml_Linq_XObject,
		// Token: 0x04000CE0 RID: 3296
		System_Xml_Linq_XProcessingInstruction,
		// Token: 0x04000CE1 RID: 3297
		System_Security_UnverifiableCodeAttribute,
		// Token: 0x04000CE2 RID: 3298
		System_Security_Permissions_SecurityAction,
		// Token: 0x04000CE3 RID: 3299
		System_Security_Permissions_SecurityAttribute,
		// Token: 0x04000CE4 RID: 3300
		System_Security_Permissions_SecurityPermissionAttribute,
		// Token: 0x04000CE5 RID: 3301
		System_NotSupportedException,
		// Token: 0x04000CE6 RID: 3302
		System_Runtime_CompilerServices_ICriticalNotifyCompletion,
		// Token: 0x04000CE7 RID: 3303
		System_Runtime_CompilerServices_IAsyncStateMachine,
		// Token: 0x04000CE8 RID: 3304
		System_Runtime_CompilerServices_AsyncVoidMethodBuilder,
		// Token: 0x04000CE9 RID: 3305
		System_Runtime_CompilerServices_AsyncTaskMethodBuilder,
		// Token: 0x04000CEA RID: 3306
		System_Runtime_CompilerServices_AsyncTaskMethodBuilder_T,
		// Token: 0x04000CEB RID: 3307
		System_Runtime_CompilerServices_AsyncStateMachineAttribute,
		// Token: 0x04000CEC RID: 3308
		System_Runtime_CompilerServices_IteratorStateMachineAttribute,
		// Token: 0x04000CED RID: 3309
		System_Windows_Forms_Form,
		// Token: 0x04000CEE RID: 3310
		System_Windows_Forms_Application,
		// Token: 0x04000CEF RID: 3311
		System_Environment,
		// Token: 0x04000CF0 RID: 3312
		System_Runtime_GCLatencyMode,
		// Token: 0x04000CF1 RID: 3313
		System_IFormatProvider,
		// Token: 0x04000CF2 RID: 3314
		CSharp7Sentinel = 250,
		// Token: 0x04000CF3 RID: 3315
		System_ValueTuple,
		// Token: 0x04000CF4 RID: 3316
		System_ValueTuple_T1,
		// Token: 0x04000CF5 RID: 3317
		System_ValueTuple_T2,
		// Token: 0x04000CF6 RID: 3318
		System_ValueTuple_T3,
		// Token: 0x04000CF7 RID: 3319
		ExtSentinel,
		// Token: 0x04000CF8 RID: 3320
		System_ValueTuple_T4,
		// Token: 0x04000CF9 RID: 3321
		System_ValueTuple_T5,
		// Token: 0x04000CFA RID: 3322
		System_ValueTuple_T6,
		// Token: 0x04000CFB RID: 3323
		System_ValueTuple_T7,
		// Token: 0x04000CFC RID: 3324
		System_ValueTuple_TRest,
		// Token: 0x04000CFD RID: 3325
		System_Runtime_CompilerServices_TupleElementNamesAttribute,
		// Token: 0x04000CFE RID: 3326
		Microsoft_CodeAnalysis_Runtime_Instrumentation,
		// Token: 0x04000CFF RID: 3327
		System_Runtime_CompilerServices_NullableAttribute,
		// Token: 0x04000D00 RID: 3328
		System_Runtime_CompilerServices_NullableContextAttribute,
		// Token: 0x04000D01 RID: 3329
		System_Runtime_CompilerServices_NullablePublicOnlyAttribute,
		// Token: 0x04000D02 RID: 3330
		System_Runtime_CompilerServices_ReferenceAssemblyAttribute,
		// Token: 0x04000D03 RID: 3331
		System_Runtime_CompilerServices_IsReadOnlyAttribute,
		// Token: 0x04000D04 RID: 3332
		System_Runtime_CompilerServices_IsByRefLikeAttribute,
		// Token: 0x04000D05 RID: 3333
		System_Runtime_InteropServices_InAttribute,
		// Token: 0x04000D06 RID: 3334
		System_ObsoleteAttribute,
		// Token: 0x04000D07 RID: 3335
		System_Span_T,
		// Token: 0x04000D08 RID: 3336
		System_ReadOnlySpan_T,
		// Token: 0x04000D09 RID: 3337
		System_Runtime_InteropServices_UnmanagedType,
		// Token: 0x04000D0A RID: 3338
		System_Runtime_CompilerServices_IsUnmanagedAttribute,
		// Token: 0x04000D0B RID: 3339
		Microsoft_VisualBasic_Conversion,
		// Token: 0x04000D0C RID: 3340
		System_Runtime_CompilerServices_NonNullTypesAttribute,
		// Token: 0x04000D0D RID: 3341
		System_AttributeTargets,
		// Token: 0x04000D0E RID: 3342
		Microsoft_CodeAnalysis_EmbeddedAttribute,
		// Token: 0x04000D0F RID: 3343
		System_Runtime_CompilerServices_ITuple,
		// Token: 0x04000D10 RID: 3344
		System_Index,
		// Token: 0x04000D11 RID: 3345
		System_Range,
		// Token: 0x04000D12 RID: 3346
		System_Runtime_CompilerServices_AsyncIteratorStateMachineAttribute,
		// Token: 0x04000D13 RID: 3347
		System_IAsyncDisposable,
		// Token: 0x04000D14 RID: 3348
		System_Collections_Generic_IAsyncEnumerable_T,
		// Token: 0x04000D15 RID: 3349
		System_Collections_Generic_IAsyncEnumerator_T,
		// Token: 0x04000D16 RID: 3350
		System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T,
		// Token: 0x04000D17 RID: 3351
		System_Threading_Tasks_Sources_ValueTaskSourceStatus,
		// Token: 0x04000D18 RID: 3352
		System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags,
		// Token: 0x04000D19 RID: 3353
		System_Threading_Tasks_Sources_IValueTaskSource_T,
		// Token: 0x04000D1A RID: 3354
		System_Threading_Tasks_Sources_IValueTaskSource,
		// Token: 0x04000D1B RID: 3355
		System_Threading_Tasks_ValueTask_T,
		// Token: 0x04000D1C RID: 3356
		System_Threading_Tasks_ValueTask,
		// Token: 0x04000D1D RID: 3357
		System_Runtime_CompilerServices_AsyncIteratorMethodBuilder,
		// Token: 0x04000D1E RID: 3358
		System_Threading_CancellationToken,
		// Token: 0x04000D1F RID: 3359
		System_Threading_CancellationTokenSource,
		// Token: 0x04000D20 RID: 3360
		System_InvalidOperationException,
		// Token: 0x04000D21 RID: 3361
		System_Runtime_CompilerServices_SwitchExpressionException,
		// Token: 0x04000D22 RID: 3362
		System_Collections_Generic_IEqualityComparer_T,
		// Token: 0x04000D23 RID: 3363
		System_Runtime_CompilerServices_NativeIntegerAttribute,
		// Token: 0x04000D24 RID: 3364
		System_Runtime_CompilerServices_IsExternalInit,
		// Token: 0x04000D25 RID: 3365
		System_Runtime_InteropServices_OutAttribute,
		// Token: 0x04000D26 RID: 3366
		System_Text_StringBuilder,
		// Token: 0x04000D27 RID: 3367
		NextAvailable
	}
}
