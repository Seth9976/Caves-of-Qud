using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Serialization
{
	// Token: 0x02000358 RID: 856
	[NativeHeader("Runtime/Serialize/ManagedReferenceUtility.h")]
	public sealed class ManagedReferenceUtility
	{
		// Token: 0x0600287D RID: 10365 RVA: 0x0004E364 File Offset: 0x0004C564
		[NativeMethod("SetManagedReferenceIdForObject")]
		private static bool SetManagedReferenceIdForObjectInternal(Object obj, object scriptObj, long refId)
		{
			return ManagedReferenceUtility.SetManagedReferenceIdForObjectInternal_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), scriptObj, refId);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x0004E380 File Offset: 0x0004C580
		public static bool SetManagedReferenceIdForObject(Object obj, object scriptObj, long refId)
		{
			bool flag = scriptObj == null;
			bool flag2;
			if (flag)
			{
				flag2 = refId == -2L;
			}
			else
			{
				Type type = scriptObj.GetType();
				bool flag3 = type == typeof(Object) || type.IsSubclassOf(typeof(Object));
				if (flag3)
				{
					throw new InvalidOperationException("Cannot assign an object deriving from UnityEngine.Object to a managed reference. This is not supported.");
				}
				flag2 = ManagedReferenceUtility.SetManagedReferenceIdForObjectInternal(obj, scriptObj, refId);
			}
			return flag2;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
		[NativeMethod("GetManagedReferenceIdForObject")]
		private static long GetManagedReferenceIdForObjectInternal(Object obj, object scriptObj)
		{
			return ManagedReferenceUtility.GetManagedReferenceIdForObjectInternal_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), scriptObj);
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x0004E404 File Offset: 0x0004C604
		public static long GetManagedReferenceIdForObject(Object obj, object scriptObj)
		{
			return ManagedReferenceUtility.GetManagedReferenceIdForObjectInternal(obj, scriptObj);
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x0004E420 File Offset: 0x0004C620
		[NativeMethod("GetManagedReference")]
		private static object GetManagedReferenceInternal(Object obj, long id)
		{
			return ManagedReferenceUtility.GetManagedReferenceInternal_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), id);
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0004E43C File Offset: 0x0004C63C
		public static object GetManagedReference(Object obj, long id)
		{
			return ManagedReferenceUtility.GetManagedReferenceInternal(obj, id);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0004E458 File Offset: 0x0004C658
		[NativeMethod("GetManagedReferenceIds")]
		private static long[] GetManagedReferenceIdsForObjectInternal(Object obj)
		{
			long[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ManagedReferenceUtility.GetManagedReferenceIdsForObjectInternal_Injected(Object.MarshalledUnityObject.Marshal<Object>(obj), out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				long[] array;
				blittableArrayWrapper.Unmarshal<long>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0004E494 File Offset: 0x0004C694
		public static long[] GetManagedReferenceIds(Object obj)
		{
			return ManagedReferenceUtility.GetManagedReferenceIdsForObjectInternal(obj);
		}

		// Token: 0x06002886 RID: 10374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetManagedReferenceIdForObjectInternal_Injected(IntPtr obj, object scriptObj, long refId);

		// Token: 0x06002887 RID: 10375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetManagedReferenceIdForObjectInternal_Injected(IntPtr obj, object scriptObj);

		// Token: 0x06002888 RID: 10376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetManagedReferenceInternal_Injected(IntPtr obj, long id);

		// Token: 0x06002889 RID: 10377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetManagedReferenceIdsForObjectInternal_Injected(IntPtr obj, out BlittableArrayWrapper ret);

		// Token: 0x04000B55 RID: 2901
		public const long RefIdUnknown = -1L;

		// Token: 0x04000B56 RID: 2902
		public const long RefIdNull = -2L;
	}
}
