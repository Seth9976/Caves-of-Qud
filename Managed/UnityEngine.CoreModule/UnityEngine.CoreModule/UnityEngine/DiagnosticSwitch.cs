using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200013A RID: 314
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[NativeHeader("Runtime/Utilities/DiagnosticSwitch.h")]
	[NativeAsStruct]
	[NativeClass("DiagnosticSwitch", "struct DiagnosticSwitch;")]
	[StructLayout(LayoutKind.Sequential)]
	internal class DiagnosticSwitch
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x0000FB37 File Offset: 0x0000DD37
		private DiagnosticSwitch(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0000FB48 File Offset: 0x0000DD48
		public string name
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					DiagnosticSwitch.get_name_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0000FB88 File Offset: 0x0000DD88
		public string description
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					DiagnosticSwitch.get_description_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		[NativeName("OwningModuleName")]
		public string owningModule
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					DiagnosticSwitch.get_owningModule_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0000FC08 File Offset: 0x0000DE08
		public DiagnosticSwitch.Flags flags
		{
			get
			{
				IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DiagnosticSwitch.get_flags_Injected(intPtr);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0000FC2A File Offset: 0x0000DE2A
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0000FC32 File Offset: 0x0000DE32
		public object value
		{
			get
			{
				return this.GetScriptingValue();
			}
			set
			{
				this.SetScriptingValue(value, false);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0000FC40 File Offset: 0x0000DE40
		[NativeName("ScriptingDefaultValue")]
		public object defaultValue
		{
			get
			{
				IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DiagnosticSwitch.get_defaultValue_Injected(intPtr);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0000FC64 File Offset: 0x0000DE64
		[NativeName("ScriptingMinValue")]
		public object minValue
		{
			get
			{
				IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DiagnosticSwitch.get_minValue_Injected(intPtr);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0000FC88 File Offset: 0x0000DE88
		[NativeName("ScriptingMaxValue")]
		public object maxValue
		{
			get
			{
				IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DiagnosticSwitch.get_maxValue_Injected(intPtr);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0000FCAA File Offset: 0x0000DEAA
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0000FCB2 File Offset: 0x0000DEB2
		public object persistentValue
		{
			get
			{
				return this.GetScriptingPersistentValue();
			}
			set
			{
				this.SetScriptingValue(value, true);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		[NativeName("ScriptingEnumInfo")]
		public EnumInfo enumInfo
		{
			get
			{
				IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DiagnosticSwitch.get_enumInfo_Injected(intPtr);
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0000FCE4 File Offset: 0x0000DEE4
		private object GetScriptingValue()
		{
			IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return DiagnosticSwitch.GetScriptingValue_Injected(intPtr);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0000FD08 File Offset: 0x0000DF08
		private object GetScriptingPersistentValue()
		{
			IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return DiagnosticSwitch.GetScriptingPersistentValue_Injected(intPtr);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		[NativeThrows]
		private void SetScriptingValue(object value, bool setPersistent)
		{
			IntPtr intPtr = DiagnosticSwitch.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			DiagnosticSwitch.SetScriptingValue_Injected(intPtr, value, setPersistent);
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0000FD50 File Offset: 0x0000DF50
		public bool isSetToDefault
		{
			get
			{
				return object.Equals(this.persistentValue, this.defaultValue);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0000FD63 File Offset: 0x0000DF63
		public bool needsRestart
		{
			get
			{
				return !object.Equals(this.value, this.persistentValue);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public override string ToString()
		{
			string text = ((this.value == null) ? "<null>" : this.value.ToString());
			return this.name + " = " + text;
		}

		// Token: 0x06000950 RID: 2384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_name_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000951 RID: 2385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_description_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000952 RID: 2386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_owningModule_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000953 RID: 2387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DiagnosticSwitch.Flags get_flags_Injected(IntPtr _unity_self);

		// Token: 0x06000954 RID: 2388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object get_defaultValue_Injected(IntPtr _unity_self);

		// Token: 0x06000955 RID: 2389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object get_minValue_Injected(IntPtr _unity_self);

		// Token: 0x06000956 RID: 2390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object get_maxValue_Injected(IntPtr _unity_self);

		// Token: 0x06000957 RID: 2391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EnumInfo get_enumInfo_Injected(IntPtr _unity_self);

		// Token: 0x06000958 RID: 2392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetScriptingValue_Injected(IntPtr _unity_self);

		// Token: 0x06000959 RID: 2393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetScriptingPersistentValue_Injected(IntPtr _unity_self);

		// Token: 0x0600095A RID: 2394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScriptingValue_Injected(IntPtr _unity_self, object value, bool setPersistent);

		// Token: 0x04000426 RID: 1062
		private IntPtr m_Ptr;

		// Token: 0x0200013B RID: 315
		[Flags]
		internal enum Flags
		{
			// Token: 0x04000428 RID: 1064
			None = 0,
			// Token: 0x04000429 RID: 1065
			CanChangeAfterEngineStart = 1,
			// Token: 0x0400042A RID: 1066
			PropagateToAssetImportWorkerProcess = 2
		}

		// Token: 0x0200013C RID: 316
		internal static class BindingsMarshaller
		{
			// Token: 0x0600095B RID: 2395 RVA: 0x0000FDBA File Offset: 0x0000DFBA
			public static IntPtr ConvertToNative(DiagnosticSwitch diagnosticSwitch)
			{
				return diagnosticSwitch.m_Ptr;
			}

			// Token: 0x0600095C RID: 2396 RVA: 0x0000FDC2 File Offset: 0x0000DFC2
			public static DiagnosticSwitch ConvertToManaged(IntPtr ptr)
			{
				return new DiagnosticSwitch(ptr);
			}
		}
	}
}
