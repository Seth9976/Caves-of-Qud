using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.CrashReportHandler
{
	// Token: 0x02000002 RID: 2
	[StaticAccessor("CrashReporting::CrashReporter::Get()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/CrashReporting/Public/CrashReporter.h")]
	public class CrashReportHandler
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private CrashReportHandler()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2
		// (set) Token: 0x06000003 RID: 3
		[NativeProperty("EnableCloudDiagnosticsReporting")]
		public static extern bool enableCaptureExceptions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		// (set) Token: 0x06000005 RID: 5
		[NativeThrows]
		public static extern uint logBufferSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000205C File Offset: 0x0000025C
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000208C File Offset: 0x0000028C
		[NativeThrows]
		internal unsafe static string installationIdentifier
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					CrashReportHandler.get_installationIdentifier_Injected(out managedSpanWrapper);
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
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					CrashReportHandler.set_installationIdentifier_Injected(ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E0 File Offset: 0x000002E0
		[NativeThrows]
		public unsafe static string GetUserMetadata(string key)
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
				CrashReportHandler.GetUserMetadata_Injected(ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002140 File Offset: 0x00000340
		[NativeThrows]
		public unsafe static void SetUserMetadata(string key, string value)
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
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = value.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				CrashReportHandler.SetUserMetadata_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_installationIdentifier_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_installationIdentifier_Injected(ref ManagedSpanWrapper value);

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetUserMetadata_Injected(ref ManagedSpanWrapper key, out ManagedSpanWrapper ret);

		// Token: 0x0600000D RID: 13
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetUserMetadata_Injected(ref ManagedSpanWrapper key, ref ManagedSpanWrapper value);
	}
}
