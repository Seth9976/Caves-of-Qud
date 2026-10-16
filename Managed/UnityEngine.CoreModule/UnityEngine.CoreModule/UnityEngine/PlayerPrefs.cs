using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200022C RID: 556
	[NativeHeader("Runtime/Utilities/PlayerPrefs.h")]
	public class PlayerPrefs
	{
		// Token: 0x06001E33 RID: 7731 RVA: 0x0003A15C File Offset: 0x0003835C
		[NativeMethod("SetInt")]
		private unsafe static bool TrySetInt(string key, int value)
		{
			bool flag;
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
				flag = PlayerPrefs.TrySetInt_Injected(ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0003A1B4 File Offset: 0x000383B4
		[NativeMethod("SetFloat")]
		private unsafe static bool TrySetFloat(string key, float value)
		{
			bool flag;
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
				flag = PlayerPrefs.TrySetFloat_Injected(ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0003A20C File Offset: 0x0003840C
		[NativeMethod("SetString")]
		private unsafe static bool TrySetSetString(string key, string value)
		{
			bool flag;
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
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = value.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				flag = PlayerPrefs.TrySetSetString_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0003A298 File Offset: 0x00038498
		public static void SetInt(string key, int value)
		{
			bool flag = !PlayerPrefs.TrySetInt(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0003A2C0 File Offset: 0x000384C0
		public unsafe static int GetInt(string key, int defaultValue)
		{
			int int_Injected;
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
				int_Injected = PlayerPrefs.GetInt_Injected(ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return int_Injected;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0003A318 File Offset: 0x00038518
		public static int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key, 0);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0003A334 File Offset: 0x00038534
		public static void SetFloat(string key, float value)
		{
			bool flag = !PlayerPrefs.TrySetFloat(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0003A35C File Offset: 0x0003855C
		public unsafe static float GetFloat(string key, float defaultValue)
		{
			float float_Injected;
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
				float_Injected = PlayerPrefs.GetFloat_Injected(ref managedSpanWrapper, defaultValue);
			}
			finally
			{
				char* ptr = null;
			}
			return float_Injected;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0003A3B4 File Offset: 0x000385B4
		public static float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key, 0f);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0003A3D4 File Offset: 0x000385D4
		public static void SetString(string key, string value)
		{
			bool flag = !PlayerPrefs.TrySetSetString(key, value);
			if (flag)
			{
				throw new PlayerPrefsException("Could not store preference value");
			}
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0003A3FC File Offset: 0x000385FC
		public unsafe static string GetString(string key, string defaultValue)
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
				PlayerPrefs.GetString_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, out managedSpanWrapper3);
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

		// Token: 0x06001E3E RID: 7742 RVA: 0x0003A490 File Offset: 0x00038690
		public static string GetString(string key)
		{
			return PlayerPrefs.GetString(key, "");
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0003A4B0 File Offset: 0x000386B0
		public unsafe static bool HasKey(string key)
		{
			bool flag;
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
				flag = PlayerPrefs.HasKey_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0003A508 File Offset: 0x00038708
		public unsafe static void DeleteKey(string key)
		{
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
				PlayerPrefs.DeleteKey_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001E41 RID: 7745
		[NativeMethod("DeleteAllWithCallback")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteAll();

		// Token: 0x06001E42 RID: 7746
		[NativeMethod("Sync")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Save();

		// Token: 0x06001E44 RID: 7748
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetInt_Injected(ref ManagedSpanWrapper key, int value);

		// Token: 0x06001E45 RID: 7749
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetFloat_Injected(ref ManagedSpanWrapper key, float value);

		// Token: 0x06001E46 RID: 7750
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TrySetSetString_Injected(ref ManagedSpanWrapper key, ref ManagedSpanWrapper value);

		// Token: 0x06001E47 RID: 7751
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInt_Injected(ref ManagedSpanWrapper key, int defaultValue);

		// Token: 0x06001E48 RID: 7752
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloat_Injected(ref ManagedSpanWrapper key, float defaultValue);

		// Token: 0x06001E49 RID: 7753
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetString_Injected(ref ManagedSpanWrapper key, ref ManagedSpanWrapper defaultValue, out ManagedSpanWrapper ret);

		// Token: 0x06001E4A RID: 7754
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasKey_Injected(ref ManagedSpanWrapper key);

		// Token: 0x06001E4B RID: 7755
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DeleteKey_Injected(ref ManagedSpanWrapper key);
	}
}
