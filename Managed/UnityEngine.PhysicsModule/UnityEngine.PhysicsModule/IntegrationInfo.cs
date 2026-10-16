using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	[StructLayout(LayoutKind.Explicit)]
	public struct IntegrationInfo
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00008853 File Offset: 0x00006A53
		public readonly uint id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000885C File Offset: 0x00006A5C
		public unsafe string name
		{
			get
			{
				fixed (byte* ptr = &this.m_Name.FixedElementField)
				{
					byte* ptr2 = ptr;
					return Marshal.PtrToStringAnsi(new IntPtr((void*)ptr2));
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00008888 File Offset: 0x00006A88
		public unsafe string description
		{
			get
			{
				fixed (byte* ptr = &this.m_Desc.FixedElementField)
				{
					byte* ptr2 = ptr;
					return Marshal.PtrToStringAnsi(new IntPtr((void*)ptr2));
				}
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000088B4 File Offset: 0x00006AB4
		public bool isFallback
		{
			get
			{
				return this.id == 3737844653U;
			}
		}

		// Token: 0x040000C2 RID: 194
		internal const uint k_InvalidID = 0U;

		// Token: 0x040000C3 RID: 195
		internal const uint k_FallbackIntegrationId = 3737844653U;

		// Token: 0x040000C4 RID: 196
		[FieldOffset(0)]
		private readonly uint m_Id;

		// Token: 0x040000C5 RID: 197
		[FixedBuffer(typeof(ushort), 3)]
		[FieldOffset(4)]
		private IntegrationInfo.<m_IntegrationVersion>e__FixedBuffer m_IntegrationVersion;

		// Token: 0x040000C6 RID: 198
		[FixedBuffer(typeof(ushort), 3)]
		[FieldOffset(10)]
		private IntegrationInfo.<m_SdkVersion>e__FixedBuffer m_SdkVersion;

		// Token: 0x040000C7 RID: 199
		[FieldOffset(16)]
		private readonly IntegrationInfo.SupportedUnityFeatures m_Features;

		// Token: 0x040000C8 RID: 200
		[FixedBuffer(typeof(byte), 16)]
		[FieldOffset(20)]
		private IntegrationInfo.<m_Name>e__FixedBuffer m_Name;

		// Token: 0x040000C9 RID: 201
		[FixedBuffer(typeof(byte), 220)]
		[FieldOffset(36)]
		private IntegrationInfo.<m_Desc>e__FixedBuffer m_Desc;

		// Token: 0x02000034 RID: 52
		[Flags]
		internal enum SupportedUnityFeatures
		{
			// Token: 0x040000CB RID: 203
			None = 0,
			// Token: 0x040000CC RID: 204
			DynamicsSupport = 2,
			// Token: 0x040000CD RID: 205
			SDKVisualDebuggerSupport = 4,
			// Token: 0x040000CE RID: 206
			ArticulationSupport = 8,
			// Token: 0x040000CF RID: 207
			ImmediateModeSupport = 16,
			// Token: 0x040000D0 RID: 208
			VehicleSupport = 32,
			// Token: 0x040000D1 RID: 209
			CharacterControllerSupport = 64
		}

		// Token: 0x02000035 RID: 53
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 220)]
		public struct <m_Desc>e__FixedBuffer
		{
			// Token: 0x040000D2 RID: 210
			public byte FixedElementField;
		}

		// Token: 0x02000036 RID: 54
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 6)]
		public struct <m_IntegrationVersion>e__FixedBuffer
		{
			// Token: 0x040000D3 RID: 211
			public ushort FixedElementField;
		}

		// Token: 0x02000037 RID: 55
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <m_Name>e__FixedBuffer
		{
			// Token: 0x040000D4 RID: 212
			public byte FixedElementField;
		}

		// Token: 0x02000038 RID: 56
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 6)]
		public struct <m_SdkVersion>e__FixedBuffer
		{
			// Token: 0x040000D5 RID: 213
			public ushort FixedElementField;
		}
	}
}
