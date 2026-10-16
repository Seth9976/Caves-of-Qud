using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[MovedFrom("UnityEditor")]
	[ExcludeFromPreset]
	[NativeClass("LocalizationAsset")]
	[NativeHeader("Modules/Localization/Public/LocalizationAsset.bindings.h")]
	[NativeHeader("Modules/Localization/Public/LocalizationAsset.h")]
	public sealed class LocalizationAsset : Object
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public LocalizationAsset()
		{
			LocalizationAsset.Internal_CreateInstance(this);
		}

		// Token: 0x06000002 RID: 2
		[FreeFunction("Internal_CreateInstance")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstance([Writable] LocalizationAsset locAsset);

		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		[NativeMethod("StoreLocalizedString")]
		public unsafe void SetLocalizedString(string original, string localized)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LocalizationAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(original, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = original.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(localized, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = localized.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				LocalizationAsset.SetLocalizedString_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		[NativeMethod("GetLocalized")]
		public unsafe string GetLocalizedString(string original)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LocalizationAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(original, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = original.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				LocalizationAsset.GetLocalizedString_Injected(intPtr, ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000216C File Offset: 0x0000036C
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000021AC File Offset: 0x000003AC
		public unsafe string localeIsoCode
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LocalizationAsset>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					LocalizationAsset.get_localeIsoCode_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LocalizationAsset>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					LocalizationAsset.set_localeIsoCode_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002210 File Offset: 0x00000410
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002234 File Offset: 0x00000434
		public bool isEditorAsset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LocalizationAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LocalizationAsset.get_isEditorAsset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LocalizationAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LocalizationAsset.set_isEditorAsset_Injected(intPtr, value);
			}
		}

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalizedString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper original, ref ManagedSpanWrapper localized);

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalizedString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper original, out ManagedSpanWrapper ret);

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localeIsoCode_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localeIsoCode_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x0600000D RID: 13
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isEditorAsset_Injected(IntPtr _unity_self);

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isEditorAsset_Injected(IntPtr _unity_self, bool value);
	}
}
