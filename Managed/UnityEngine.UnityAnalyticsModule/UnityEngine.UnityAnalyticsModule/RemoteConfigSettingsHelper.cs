using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	internal static class RemoteConfigSettingsHelper
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002EC0 File Offset: 0x000010C0
		internal unsafe static IntPtr GetSafeMap(IntPtr m, string key)
		{
			IntPtr safeMap_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				safeMap_Injected = RemoteConfigSettingsHelper.GetSafeMap_Injected(m, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return safeMap_Injected;
		}

		// Token: 0x06000064 RID: 100
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] GetSafeMapKeys(IntPtr m);

		// Token: 0x06000065 RID: 101 RVA: 0x00002F18 File Offset: 0x00001118
		internal static RemoteConfigSettingsHelper.Tag[] GetSafeMapTypes(IntPtr m)
		{
			RemoteConfigSettingsHelper.Tag[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RemoteConfigSettingsHelper.GetSafeMapTypes_Injected(m, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RemoteConfigSettingsHelper.Tag[] array;
				blittableArrayWrapper.Unmarshal<RemoteConfigSettingsHelper.Tag>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F4C File Offset: 0x0000114C
		internal unsafe static long GetSafeNumber(IntPtr m, string key, long defaultValue)
		{
			long safeNumber_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				safeNumber_Injected = RemoteConfigSettingsHelper.GetSafeNumber_Injected(m, ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return safeNumber_Injected;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FA4 File Offset: 0x000011A4
		internal unsafe static float GetSafeFloat(IntPtr m, string key, float defaultValue)
		{
			float safeFloat_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				safeFloat_Injected = RemoteConfigSettingsHelper.GetSafeFloat_Injected(m, ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return safeFloat_Injected;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002FFC File Offset: 0x000011FC
		internal unsafe static bool GetSafeBool(IntPtr m, string key, bool defaultValue)
		{
			bool safeBool_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				safeBool_Injected = RemoteConfigSettingsHelper.GetSafeBool_Injected(m, ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return safeBool_Injected;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003054 File Offset: 0x00001254
		internal unsafe static string GetSafeStringValue(IntPtr m, string key, string defaultValue)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(defaultValue, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = defaultValue.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				RemoteConfigSettingsHelper.GetSafeStringValue_Injected(m, ref managedSpanWrapper, ref managedSpanWrapper2, out managedSpanWrapper3);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				ManagedSpanWrapper managedSpanWrapper3;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper3);
			}
			return stringAndDispose;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000030E8 File Offset: 0x000012E8
		internal unsafe static IntPtr GetSafeArray(IntPtr m, string key)
		{
			IntPtr safeArray_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(key, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = key.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				safeArray_Injected = RemoteConfigSettingsHelper.GetSafeArray_Injected(m, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return safeArray_Injected;
		}

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeArraySize(IntPtr a);

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArrayArray(IntPtr a, long i);

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArrayMap(IntPtr a, long i);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RemoteConfigSettingsHelper.Tag GetSafeArrayType(IntPtr a, long i);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeNumberArray(IntPtr a, long i);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetSafeArrayFloat(IntPtr a, long i);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetSafeArrayBool(IntPtr a, long i);

		// Token: 0x06000072 RID: 114 RVA: 0x00003140 File Offset: 0x00001340
		internal static string GetSafeArrayStringValue(IntPtr a, long i)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				RemoteConfigSettingsHelper.GetSafeArrayStringValue_Injected(a, i, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003174 File Offset: 0x00001374
		public static IDictionary<string, object> GetDictionary(IntPtr m, string key)
		{
			bool flag = m == IntPtr.Zero;
			IDictionary<string, object> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(key);
				if (flag2)
				{
					m = RemoteConfigSettingsHelper.GetSafeMap(m, key);
					bool flag3 = m == IntPtr.Zero;
					if (flag3)
					{
						return null;
					}
				}
				dictionary = RemoteConfigSettingsHelper.GetDictionary(m);
			}
			return dictionary;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031CC File Offset: 0x000013CC
		internal static IDictionary<string, object> GetDictionary(IntPtr m)
		{
			bool flag = m == IntPtr.Zero;
			IDictionary<string, object> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				IDictionary<string, object> dictionary2 = new Dictionary<string, object>();
				RemoteConfigSettingsHelper.Tag[] safeMapTypes = RemoteConfigSettingsHelper.GetSafeMapTypes(m);
				string[] safeMapKeys = RemoteConfigSettingsHelper.GetSafeMapKeys(m);
				for (int i = 0; i < safeMapKeys.Length; i++)
				{
					RemoteConfigSettingsHelper.SetDictKeyType(m, dictionary2, safeMapKeys[i], safeMapTypes[i]);
				}
				dictionary = dictionary2;
			}
			return dictionary;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003234 File Offset: 0x00001434
		internal static object GetArrayArrayEntries(IntPtr a, long i)
		{
			return RemoteConfigSettingsHelper.GetArrayEntries(RemoteConfigSettingsHelper.GetSafeArrayArray(a, i));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003254 File Offset: 0x00001454
		internal static IDictionary<string, object> GetArrayMapEntries(IntPtr a, long i)
		{
			return RemoteConfigSettingsHelper.GetDictionary(RemoteConfigSettingsHelper.GetSafeArrayMap(a, i));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003274 File Offset: 0x00001474
		internal static T[] GetArrayEntriesType<T>(IntPtr a, long size, Func<IntPtr, long, T> f)
		{
			T[] array = new T[size];
			for (long num = 0L; num < size; num += 1L)
			{
				array[(int)(checked((IntPtr)num))] = f(a, num);
			}
			return array;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032B4 File Offset: 0x000014B4
		internal static object GetArrayEntries(IntPtr a)
		{
			long safeArraySize = RemoteConfigSettingsHelper.GetSafeArraySize(a);
			bool flag = safeArraySize == 0L;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				switch (RemoteConfigSettingsHelper.GetSafeArrayType(a, 0L))
				{
				case RemoteConfigSettingsHelper.Tag.kIntVal:
				case RemoteConfigSettingsHelper.Tag.kInt64Val:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<long>(a, safeArraySize, new Func<IntPtr, long, long>(RemoteConfigSettingsHelper.GetSafeNumberArray));
				case RemoteConfigSettingsHelper.Tag.kDoubleVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<float>(a, safeArraySize, new Func<IntPtr, long, float>(RemoteConfigSettingsHelper.GetSafeArrayFloat));
				case RemoteConfigSettingsHelper.Tag.kBoolVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<bool>(a, safeArraySize, new Func<IntPtr, long, bool>(RemoteConfigSettingsHelper.GetSafeArrayBool));
				case RemoteConfigSettingsHelper.Tag.kStringVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<string>(a, safeArraySize, new Func<IntPtr, long, string>(RemoteConfigSettingsHelper.GetSafeArrayStringValue));
				case RemoteConfigSettingsHelper.Tag.kArrayVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<object>(a, safeArraySize, new Func<IntPtr, long, object>(RemoteConfigSettingsHelper.GetArrayArrayEntries));
				case RemoteConfigSettingsHelper.Tag.kMapVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<IDictionary<string, object>>(a, safeArraySize, new Func<IntPtr, long, IDictionary<string, object>>(RemoteConfigSettingsHelper.GetArrayMapEntries));
				}
				obj = null;
			}
			return obj;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000033A0 File Offset: 0x000015A0
		internal static object GetMixedArrayEntries(IntPtr a)
		{
			long safeArraySize = RemoteConfigSettingsHelper.GetSafeArraySize(a);
			bool flag = safeArraySize == 0L;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				object[] array = new object[safeArraySize];
				for (long num = 0L; num < safeArraySize; num += 1L)
				{
					checked
					{
						switch (RemoteConfigSettingsHelper.GetSafeArrayType(a, num))
						{
						case RemoteConfigSettingsHelper.Tag.kIntVal:
						case RemoteConfigSettingsHelper.Tag.kInt64Val:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeNumberArray(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kDoubleVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayFloat(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kBoolVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayBool(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kStringVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayStringValue(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kArrayVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetArrayArrayEntries(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kMapVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetArrayMapEntries(a, num);
							break;
						}
					}
				}
				obj = array;
			}
			return obj;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000349C File Offset: 0x0000169C
		internal static void SetDictKeyType(IntPtr m, IDictionary<string, object> dict, string key, RemoteConfigSettingsHelper.Tag tag)
		{
			switch (tag)
			{
			case RemoteConfigSettingsHelper.Tag.kIntVal:
			case RemoteConfigSettingsHelper.Tag.kInt64Val:
				dict[key] = RemoteConfigSettingsHelper.GetSafeNumber(m, key, 0L);
				break;
			case RemoteConfigSettingsHelper.Tag.kDoubleVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeFloat(m, key, 0f);
				break;
			case RemoteConfigSettingsHelper.Tag.kBoolVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeBool(m, key, false);
				break;
			case RemoteConfigSettingsHelper.Tag.kStringVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeStringValue(m, key, "");
				break;
			case RemoteConfigSettingsHelper.Tag.kArrayVal:
				dict[key] = RemoteConfigSettingsHelper.GetArrayEntries(RemoteConfigSettingsHelper.GetSafeArray(m, key));
				break;
			case RemoteConfigSettingsHelper.Tag.kMixedArrayVal:
				dict[key] = RemoteConfigSettingsHelper.GetMixedArrayEntries(RemoteConfigSettingsHelper.GetSafeArray(m, key));
				break;
			case RemoteConfigSettingsHelper.Tag.kMapVal:
				dict[key] = RemoteConfigSettingsHelper.GetDictionary(RemoteConfigSettingsHelper.GetSafeMap(m, key));
				break;
			}
		}

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSafeMap_Injected(IntPtr m, ref ManagedSpanWrapper key);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSafeMapTypes_Injected(IntPtr m, out BlittableArrayWrapper ret);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetSafeNumber_Injected(IntPtr m, ref ManagedSpanWrapper key, long defaultValue);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetSafeFloat_Injected(IntPtr m, ref ManagedSpanWrapper key, float defaultValue);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetSafeBool_Injected(IntPtr m, ref ManagedSpanWrapper key, bool defaultValue);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSafeStringValue_Injected(IntPtr m, ref ManagedSpanWrapper key, ref ManagedSpanWrapper defaultValue, out ManagedSpanWrapper ret);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSafeArray_Injected(IntPtr m, ref ManagedSpanWrapper key);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSafeArrayStringValue_Injected(IntPtr a, long i, out ManagedSpanWrapper ret);

		// Token: 0x02000007 RID: 7
		[RequiredByNativeCode]
		internal enum Tag
		{
			// Token: 0x04000007 RID: 7
			kUnknown,
			// Token: 0x04000008 RID: 8
			kIntVal,
			// Token: 0x04000009 RID: 9
			kInt64Val,
			// Token: 0x0400000A RID: 10
			kUInt64Val,
			// Token: 0x0400000B RID: 11
			kDoubleVal,
			// Token: 0x0400000C RID: 12
			kBoolVal,
			// Token: 0x0400000D RID: 13
			kStringVal,
			// Token: 0x0400000E RID: 14
			kArrayVal,
			// Token: 0x0400000F RID: 15
			kMixedArrayVal,
			// Token: 0x04000010 RID: 16
			kMapVal,
			// Token: 0x04000011 RID: 17
			kMaxTags
		}
	}
}
