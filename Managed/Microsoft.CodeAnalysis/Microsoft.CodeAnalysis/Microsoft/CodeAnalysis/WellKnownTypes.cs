using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000293 RID: 659
	[NullableContext(1)]
	[Nullable(0)]
	internal static class WellKnownTypes
	{
		// Token: 0x06001B01 RID: 6913 RVA: 0x000513EC File Offset: 0x0004F5EC
		static WellKnownTypes()
		{
			for (int i = 0; i < WellKnownTypes.s_metadataNames.Length; i++)
			{
				string text = WellKnownTypes.s_metadataNames[i];
				WellKnownType wellKnownType = i + WellKnownType.First;
				WellKnownTypes.s_nameToTypeIdMap.Add(text, wellKnownType);
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00051EC8 File Offset: 0x000500C8
		[Conditional("DEBUG")]
		private static void AssertEnumAndTableInSync()
		{
			int i = 0;
			while (i < WellKnownTypes.s_metadataNames.Length)
			{
				string text = WellKnownTypes.s_metadataNames[i];
				WellKnownType wellKnownType = i + WellKnownType.First;
				string text2;
				if (wellKnownType <= WellKnownType.Microsoft_VisualBasic_CompilerServices_ObjectFlowControl_ForLoopControl)
				{
					if (wellKnownType != WellKnownType.First)
					{
						if (wellKnownType != WellKnownType.Microsoft_VisualBasic_CompilerServices_ObjectFlowControl_ForLoopControl)
						{
							goto IL_0057;
						}
						text2 = "Microsoft.VisualBasic.CompilerServices.ObjectFlowControl+ForLoopControl";
					}
					else
					{
						text2 = "System.Math";
					}
				}
				else if (wellKnownType != WellKnownType.System_IFormatProvider)
				{
					if (wellKnownType != WellKnownType.ExtSentinel)
					{
						goto IL_0057;
					}
					text2 = "";
				}
				else
				{
					text2 = "System.IFormatProvider";
				}
				IL_007D:
				int num = text.IndexOf('`');
				if (num >= 0)
				{
					text = text.Substring(0, num);
					text2 = text2.Substring(0, num);
				}
				i++;
				continue;
				IL_0057:
				text2 = wellKnownType.ToString().Replace("__", "+").Replace('_', '.');
				goto IL_007D;
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00051F86 File Offset: 0x00050186
		public static bool IsWellKnownType(this WellKnownType typeId)
		{
			return typeId >= WellKnownType.First && typeId < WellKnownType.NextAvailable;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00051F97 File Offset: 0x00050197
		public static bool IsValueTupleType(this WellKnownType typeId)
		{
			return typeId >= WellKnownType.System_ValueTuple && typeId <= WellKnownType.System_ValueTuple_TRest;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00051FAE File Offset: 0x000501AE
		public static bool IsValid(this WellKnownType typeId)
		{
			return typeId >= WellKnownType.First && typeId < WellKnownType.NextAvailable && typeId != WellKnownType.ExtSentinel;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00051FCA File Offset: 0x000501CA
		public static string GetMetadataName(this WellKnownType id)
		{
			return WellKnownTypes.s_metadataNames[id - WellKnownType.First];
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00051FD8 File Offset: 0x000501D8
		public static WellKnownType GetTypeFromMetadataName(string metadataName)
		{
			WellKnownType wellKnownType;
			if (WellKnownTypes.s_nameToTypeIdMap.TryGetValue(metadataName, ref wellKnownType))
			{
				return wellKnownType;
			}
			return WellKnownType.Unknown;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00051FF7 File Offset: 0x000501F7
		internal static WellKnownType GetWellKnownFunctionDelegate(int invokeArgumentCount)
		{
			if (invokeArgumentCount > 16)
			{
				return WellKnownType.Unknown;
			}
			return WellKnownType.System_Func_T + invokeArgumentCount;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00052004 File Offset: 0x00050204
		internal static WellKnownType GetWellKnownActionDelegate(int invokeArgumentCount)
		{
			if (invokeArgumentCount > 16)
			{
				return WellKnownType.Unknown;
			}
			return WellKnownType.System_Action + invokeArgumentCount;
		}

		// Token: 0x04000D28 RID: 3368
		internal const int Count = 257;

		// Token: 0x04000D29 RID: 3369
		private static readonly string[] s_metadataNames = new string[]
		{
			"System.Math", "System.Array", "System.Attribute", "System.CLSCompliantAttribute", "System.Convert", "System.Exception", "System.FlagsAttribute", "System.FormattableString", "System.Guid", "System.IFormattable",
			"System.RuntimeTypeHandle", "System.RuntimeFieldHandle", "System.RuntimeMethodHandle", "System.MarshalByRefObject", "System.Type", "System.Reflection.AssemblyKeyFileAttribute", "System.Reflection.AssemblyKeyNameAttribute", "System.Reflection.MethodInfo", "System.Reflection.ConstructorInfo", "System.Reflection.MethodBase",
			"System.Reflection.FieldInfo", "System.Reflection.MemberInfo", "System.Reflection.Missing", "System.Runtime.CompilerServices.FormattableStringFactory", "System.Runtime.CompilerServices.RuntimeHelpers", "System.Runtime.ExceptionServices.ExceptionDispatchInfo", "System.Runtime.InteropServices.StructLayoutAttribute", "System.Runtime.InteropServices.UnknownWrapper", "System.Runtime.InteropServices.DispatchWrapper", "System.Runtime.InteropServices.CallingConvention",
			"System.Runtime.InteropServices.ClassInterfaceAttribute", "System.Runtime.InteropServices.ClassInterfaceType", "System.Runtime.InteropServices.CoClassAttribute", "System.Runtime.InteropServices.ComAwareEventInfo", "System.Runtime.InteropServices.ComEventInterfaceAttribute", "System.Runtime.InteropServices.ComInterfaceType", "System.Runtime.InteropServices.ComSourceInterfacesAttribute", "System.Runtime.InteropServices.ComVisibleAttribute", "System.Runtime.InteropServices.DispIdAttribute", "System.Runtime.InteropServices.GuidAttribute",
			"System.Runtime.InteropServices.InterfaceTypeAttribute", "System.Runtime.InteropServices.Marshal", "System.Runtime.InteropServices.TypeIdentifierAttribute", "System.Runtime.InteropServices.BestFitMappingAttribute", "System.Runtime.InteropServices.DefaultParameterValueAttribute", "System.Runtime.InteropServices.LCIDConversionAttribute", "System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute", "System.Activator", "System.Threading.Tasks.Task", "System.Threading.Tasks.Task`1",
			"System.Threading.Interlocked", "System.Threading.Monitor", "System.Threading.Thread", "Microsoft.CSharp.RuntimeBinder.Binder", "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo", "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags", "Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags", "Microsoft.VisualBasic.CallType", "Microsoft.VisualBasic.Embedded", "Microsoft.VisualBasic.CompilerServices.Conversions",
			"Microsoft.VisualBasic.CompilerServices.Operators", "Microsoft.VisualBasic.CompilerServices.NewLateBinding", "Microsoft.VisualBasic.CompilerServices.EmbeddedOperators", "Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute", "Microsoft.VisualBasic.CompilerServices.Utils", "Microsoft.VisualBasic.CompilerServices.LikeOperator", "Microsoft.VisualBasic.CompilerServices.ProjectData", "Microsoft.VisualBasic.CompilerServices.ObjectFlowControl", "Microsoft.VisualBasic.CompilerServices.ObjectFlowControl+ForLoopControl", "Microsoft.VisualBasic.CompilerServices.StaticLocalInitFlag",
			"Microsoft.VisualBasic.CompilerServices.StringType", "Microsoft.VisualBasic.CompilerServices.IncompleteInitialization", "Microsoft.VisualBasic.CompilerServices.Versioned", "Microsoft.VisualBasic.CompareMethod", "Microsoft.VisualBasic.Strings", "Microsoft.VisualBasic.ErrObject", "Microsoft.VisualBasic.FileSystem", "Microsoft.VisualBasic.ApplicationServices.ApplicationBase", "Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase", "Microsoft.VisualBasic.Information",
			"Microsoft.VisualBasic.Interaction", "System.Func`1", "System.Func`2", "System.Func`3", "System.Func`4", "System.Func`5", "System.Func`6", "System.Func`7", "System.Func`8", "System.Func`9",
			"System.Func`10", "System.Func`11", "System.Func`12", "System.Func`13", "System.Func`14", "System.Func`15", "System.Func`16", "System.Func`17", "System.Action", "System.Action`1",
			"System.Action`2", "System.Action`3", "System.Action`4", "System.Action`5", "System.Action`6", "System.Action`7", "System.Action`8", "System.Action`9", "System.Action`10", "System.Action`11",
			"System.Action`12", "System.Action`13", "System.Action`14", "System.Action`15", "System.Action`16", "System.AttributeUsageAttribute", "System.ParamArrayAttribute", "System.NonSerializedAttribute", "System.STAThreadAttribute", "System.Reflection.DefaultMemberAttribute",
			"System.Runtime.CompilerServices.DateTimeConstantAttribute", "System.Runtime.CompilerServices.DecimalConstantAttribute", "System.Runtime.CompilerServices.IUnknownConstantAttribute", "System.Runtime.CompilerServices.IDispatchConstantAttribute", "System.Runtime.CompilerServices.ExtensionAttribute", "System.Runtime.CompilerServices.INotifyCompletion", "System.Runtime.CompilerServices.InternalsVisibleToAttribute", "System.Runtime.CompilerServices.CompilerGeneratedAttribute", "System.Runtime.CompilerServices.AccessedThroughPropertyAttribute", "System.Runtime.CompilerServices.CompilationRelaxationsAttribute",
			"System.Runtime.CompilerServices.RuntimeCompatibilityAttribute", "System.Runtime.CompilerServices.UnsafeValueTypeAttribute", "System.Runtime.CompilerServices.FixedBufferAttribute", "System.Runtime.CompilerServices.DynamicAttribute", "System.Runtime.CompilerServices.CallSiteBinder", "System.Runtime.CompilerServices.CallSite", "System.Runtime.CompilerServices.CallSite`1", "System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken", "System.Runtime.InteropServices.WindowsRuntime.EventRegistrationTokenTable`1", "System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMarshal",
			"Windows.Foundation.IAsyncAction", "Windows.Foundation.IAsyncActionWithProgress`1", "Windows.Foundation.IAsyncOperation`1", "Windows.Foundation.IAsyncOperationWithProgress`2", "System.Diagnostics.Debugger", "System.Diagnostics.DebuggerDisplayAttribute", "System.Diagnostics.DebuggerNonUserCodeAttribute", "System.Diagnostics.DebuggerHiddenAttribute", "System.Diagnostics.DebuggerBrowsableAttribute", "System.Diagnostics.DebuggerStepThroughAttribute",
			"System.Diagnostics.DebuggerBrowsableState", "System.Diagnostics.DebuggableAttribute", "System.Diagnostics.DebuggableAttribute+DebuggingModes", "System.ComponentModel.DesignerSerializationVisibilityAttribute", "System.IEquatable`1", "System.Collections.IList", "System.Collections.ICollection", "System.Collections.Generic.EqualityComparer`1", "System.Collections.Generic.List`1", "System.Collections.Generic.IDictionary`2",
			"System.Collections.Generic.IReadOnlyDictionary`2", "System.Collections.ObjectModel.Collection`1", "System.Collections.ObjectModel.ReadOnlyCollection`1", "System.Collections.Specialized.INotifyCollectionChanged", "System.ComponentModel.INotifyPropertyChanged", "System.ComponentModel.EditorBrowsableAttribute", "System.ComponentModel.EditorBrowsableState", "System.Linq.Enumerable", "System.Linq.Expressions.Expression", "System.Linq.Expressions.Expression`1",
			"System.Linq.Expressions.ParameterExpression", "System.Linq.Expressions.ElementInit", "System.Linq.Expressions.MemberBinding", "System.Linq.Expressions.ExpressionType", "System.Linq.IQueryable", "System.Linq.IQueryable`1", "System.Xml.Linq.Extensions", "System.Xml.Linq.XAttribute", "System.Xml.Linq.XCData", "System.Xml.Linq.XComment",
			"System.Xml.Linq.XContainer", "System.Xml.Linq.XDeclaration", "System.Xml.Linq.XDocument", "System.Xml.Linq.XElement", "System.Xml.Linq.XName", "System.Xml.Linq.XNamespace", "System.Xml.Linq.XObject", "System.Xml.Linq.XProcessingInstruction", "System.Security.UnverifiableCodeAttribute", "System.Security.Permissions.SecurityAction",
			"System.Security.Permissions.SecurityAttribute", "System.Security.Permissions.SecurityPermissionAttribute", "System.NotSupportedException", "System.Runtime.CompilerServices.ICriticalNotifyCompletion", "System.Runtime.CompilerServices.IAsyncStateMachine", "System.Runtime.CompilerServices.AsyncVoidMethodBuilder", "System.Runtime.CompilerServices.AsyncTaskMethodBuilder", "System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1", "System.Runtime.CompilerServices.AsyncStateMachineAttribute", "System.Runtime.CompilerServices.IteratorStateMachineAttribute",
			"System.Windows.Forms.Form", "System.Windows.Forms.Application", "System.Environment", "System.Runtime.GCLatencyMode", "System.IFormatProvider", "System.ValueTuple", "System.ValueTuple`1", "System.ValueTuple`2", "System.ValueTuple`3", "",
			"System.ValueTuple`4", "System.ValueTuple`5", "System.ValueTuple`6", "System.ValueTuple`7", "System.ValueTuple`8", "System.Runtime.CompilerServices.TupleElementNamesAttribute", "Microsoft.CodeAnalysis.Runtime.Instrumentation", "System.Runtime.CompilerServices.NullableAttribute", "System.Runtime.CompilerServices.NullableContextAttribute", "System.Runtime.CompilerServices.NullablePublicOnlyAttribute",
			"System.Runtime.CompilerServices.ReferenceAssemblyAttribute", "System.Runtime.CompilerServices.IsReadOnlyAttribute", "System.Runtime.CompilerServices.IsByRefLikeAttribute", "System.Runtime.InteropServices.InAttribute", "System.ObsoleteAttribute", "System.Span`1", "System.ReadOnlySpan`1", "System.Runtime.InteropServices.UnmanagedType", "System.Runtime.CompilerServices.IsUnmanagedAttribute", "Microsoft.VisualBasic.Conversion",
			"System.Runtime.CompilerServices.NonNullTypesAttribute", "System.AttributeTargets", "Microsoft.CodeAnalysis.EmbeddedAttribute", "System.Runtime.CompilerServices.ITuple", "System.Index", "System.Range", "System.Runtime.CompilerServices.AsyncIteratorStateMachineAttribute", "System.IAsyncDisposable", "System.Collections.Generic.IAsyncEnumerable`1", "System.Collections.Generic.IAsyncEnumerator`1",
			"System.Threading.Tasks.Sources.ManualResetValueTaskSourceCore`1", "System.Threading.Tasks.Sources.ValueTaskSourceStatus", "System.Threading.Tasks.Sources.ValueTaskSourceOnCompletedFlags", "System.Threading.Tasks.Sources.IValueTaskSource`1", "System.Threading.Tasks.Sources.IValueTaskSource", "System.Threading.Tasks.ValueTask`1", "System.Threading.Tasks.ValueTask", "System.Runtime.CompilerServices.AsyncIteratorMethodBuilder", "System.Threading.CancellationToken", "System.Threading.CancellationTokenSource",
			"System.InvalidOperationException", "System.Runtime.CompilerServices.SwitchExpressionException", "System.Collections.Generic.IEqualityComparer`1", "System.Runtime.CompilerServices.NativeIntegerAttribute", "System.Runtime.CompilerServices.IsExternalInit", "System.Runtime.InteropServices.OutAttribute", "System.Text.StringBuilder"
		};

		// Token: 0x04000D2A RID: 3370
		private static readonly Dictionary<string, WellKnownType> s_nameToTypeIdMap = new Dictionary<string, WellKnownType>(257);
	}
}
