using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.DedicatedServer
{
	// Token: 0x02000465 RID: 1125
	[NativeHeader("Runtime/Export/DedicatedServer/Arguments.bindings.h")]
	[StaticAccessor("DedicatedServerBindings", StaticAccessorType.DoubleColon)]
	public static class Arguments
	{
		// Token: 0x06002C4E RID: 11342 RVA: 0x000539D0 File Offset: 0x00051BD0
		[NativeConditional("PLATFORM_SERVER")]
		[FreeFunction("DedicatedServerBindings::GetBoolArgument")]
		internal unsafe static bool GetBoolArgument(string arg)
		{
			bool boolArgument_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(arg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = arg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				boolArgument_Injected = Arguments.GetBoolArgument_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return boolArgument_Injected;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x00053A28 File Offset: 0x00051C28
		[FreeFunction("DedicatedServerBindings::GetIntArgument")]
		[NativeConditional("PLATFORM_SERVER")]
		internal unsafe static bool GetIntArgument(string arg, out int intValue)
		{
			bool intArgument_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(arg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = arg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intArgument_Injected = Arguments.GetIntArgument_Injected(ref managedSpanWrapper, out intValue);
			}
			finally
			{
				char* ptr = null;
			}
			return intArgument_Injected;
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x00053A80 File Offset: 0x00051C80
		[FreeFunction("DedicatedServerBindings::GetStringArgument")]
		[NativeConditional("PLATFORM_SERVER")]
		internal unsafe static bool GetStringArgument(string arg, out string stringValue)
		{
			bool stringArgument_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(arg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = arg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				stringArgument_Injected = Arguments.GetStringArgument_Injected(ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringValue = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringArgument_Injected;
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x00053AE4 File Offset: 0x00051CE4
		[NativeConditional("PLATFORM_SERVER")]
		[FreeFunction("DedicatedServerBindings::SetBoolArgument")]
		internal unsafe static void SetBoolArgument(string arg)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(arg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = arg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Arguments.SetBoolArgument_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x00053B38 File Offset: 0x00051D38
		[NativeConditional("PLATFORM_SERVER")]
		[FreeFunction("DedicatedServerBindings::SetIntArgument")]
		internal unsafe static void SetIntArgument(string arg, int intValue)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(arg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = arg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Arguments.SetIntArgument_Injected(ref managedSpanWrapper, intValue);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x00053B90 File Offset: 0x00051D90
		[FreeFunction("DedicatedServerBindings::SetStringArgument")]
		[NativeConditional("PLATFORM_SERVER")]
		internal unsafe static void SetStringArgument(string arg, string stringValue)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(arg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = arg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(stringValue, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = stringValue.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				Arguments.SetStringArgument_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x00053C18 File Offset: 0x00051E18
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x00053C4C File Offset: 0x00051E4C
		public static int? Port
		{
			get
			{
				int num;
				bool intArgument = Arguments.GetIntArgument("port", out num);
				int? num2;
				if (intArgument)
				{
					num2 = new int?(num);
				}
				else
				{
					num2 = null;
				}
				return num2;
			}
			set
			{
				int valueOrDefault = value.GetValueOrDefault();
				Arguments.SetIntArgument("port", valueOrDefault);
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x00053C70 File Offset: 0x00051E70
		// (set) Token: 0x06002C57 RID: 11351 RVA: 0x00053CA4 File Offset: 0x00051EA4
		public static int? TargetFramerate
		{
			get
			{
				int num;
				bool intArgument = Arguments.GetIntArgument("framerate", out num);
				int? num2;
				if (intArgument)
				{
					num2 = new int?(num);
				}
				else
				{
					num2 = null;
				}
				return num2;
			}
			set
			{
				int valueOrDefault = value.GetValueOrDefault();
				Arguments.SetIntArgument("framerate", valueOrDefault);
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x00053CC8 File Offset: 0x00051EC8
		// (set) Token: 0x06002C59 RID: 11353 RVA: 0x00053CFC File Offset: 0x00051EFC
		public static int? LogLevel
		{
			get
			{
				int num;
				bool intArgument = Arguments.GetIntArgument("loglevel", out num);
				int? num2;
				if (intArgument)
				{
					num2 = new int?(num);
				}
				else
				{
					num2 = null;
				}
				return num2;
			}
			set
			{
				int valueOrDefault = value.GetValueOrDefault();
				Arguments.SetIntArgument("loglevel", valueOrDefault);
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x00053D20 File Offset: 0x00051F20
		// (set) Token: 0x06002C5B RID: 11355 RVA: 0x00053D60 File Offset: 0x00051F60
		public static string LogPath
		{
			get
			{
				string text;
				bool stringArgument = Arguments.GetStringArgument("logpath", out text);
				string text2;
				if (stringArgument)
				{
					text2 = text;
				}
				else
				{
					bool stringArgument2 = Arguments.GetStringArgument("logfile", out text);
					if (stringArgument2)
					{
						text2 = Path.GetDirectoryName(text);
					}
					else
					{
						text2 = null;
					}
				}
				return text2;
			}
			set
			{
				Arguments.SetStringArgument("logpath", value);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x00053D70 File Offset: 0x00051F70
		// (set) Token: 0x06002C5D RID: 11357 RVA: 0x00053DA4 File Offset: 0x00051FA4
		public static int? QueryPort
		{
			get
			{
				int num;
				bool intArgument = Arguments.GetIntArgument("queryport", out num);
				int? num2;
				if (intArgument)
				{
					num2 = new int?(num);
				}
				else
				{
					num2 = null;
				}
				return num2;
			}
			set
			{
				int valueOrDefault = value.GetValueOrDefault();
				Arguments.SetIntArgument("queryport", valueOrDefault);
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x00053DC8 File Offset: 0x00051FC8
		// (set) Token: 0x06002C5F RID: 11359 RVA: 0x00053DEF File Offset: 0x00051FEF
		public static string QueryType
		{
			get
			{
				string text;
				bool stringArgument = Arguments.GetStringArgument("querytype", out text);
				string text2;
				if (stringArgument)
				{
					text2 = text;
				}
				else
				{
					text2 = null;
				}
				return text2;
			}
			set
			{
				Arguments.SetStringArgument("querytype", value);
			}
		}

		// Token: 0x06002C60 RID: 11360
		[NativeConditional("PLATFORM_SERVER")]
		[FreeFunction("DedicatedServerBindings::SetArgumentErrorPolicy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetArgumentErrorPolicy(Arguments.ArgumentErrorPolicy policy);

		// Token: 0x06002C61 RID: 11361
		[NativeConditional("PLATFORM_SERVER")]
		[FreeFunction("DedicatedServerBindings::GetArgumentErrorPolicy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Arguments.ArgumentErrorPolicy GetArgumentErrorPolicy();

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x00053E00 File Offset: 0x00052000
		// (set) Token: 0x06002C63 RID: 11363 RVA: 0x00053E17 File Offset: 0x00052017
		public static Arguments.ArgumentErrorPolicy ErrorPolicy
		{
			get
			{
				return Arguments.GetArgumentErrorPolicy();
			}
			set
			{
				Arguments.SetArgumentErrorPolicy(value);
			}
		}

		// Token: 0x06002C64 RID: 11364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolArgument_Injected(ref ManagedSpanWrapper arg);

		// Token: 0x06002C65 RID: 11365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIntArgument_Injected(ref ManagedSpanWrapper arg, out int intValue);

		// Token: 0x06002C66 RID: 11366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetStringArgument_Injected(ref ManagedSpanWrapper arg, out ManagedSpanWrapper stringValue);

		// Token: 0x06002C67 RID: 11367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolArgument_Injected(ref ManagedSpanWrapper arg);

		// Token: 0x06002C68 RID: 11368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntArgument_Injected(ref ManagedSpanWrapper arg, int intValue);

		// Token: 0x06002C69 RID: 11369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStringArgument_Injected(ref ManagedSpanWrapper arg, ref ManagedSpanWrapper stringValue);

		// Token: 0x02000466 RID: 1126
		public enum ArgumentErrorPolicy
		{
			// Token: 0x04000C48 RID: 3144
			Ignore,
			// Token: 0x04000C49 RID: 3145
			Warn,
			// Token: 0x04000C4A RID: 3146
			Fatal
		}
	}
}
