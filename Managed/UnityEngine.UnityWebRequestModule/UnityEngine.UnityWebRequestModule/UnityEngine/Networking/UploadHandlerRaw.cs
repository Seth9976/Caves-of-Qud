using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x0200001B RID: 27
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerRaw.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UploadHandlerRaw : UploadHandler
	{
		// Token: 0x06000157 RID: 343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Create([Unmarshalled] UploadHandlerRaw self, byte* data, int dataLength);

		// Token: 0x06000158 RID: 344 RVA: 0x00005CAC File Offset: 0x00003EAC
		public UploadHandlerRaw(byte[] data)
			: this((data == null || data.Length == 0) ? default(NativeArray<byte>) : new NativeArray<byte>(data, Allocator.Persistent), true)
		{
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005CDC File Offset: 0x00003EDC
		public unsafe UploadHandlerRaw(NativeArray<byte> data, bool transferOwnership)
		{
			bool flag = !data.IsCreated || data.Length == 0;
			if (flag)
			{
				this.m_Ptr = UploadHandlerRaw.Create(this, null, 0);
			}
			else
			{
				if (transferOwnership)
				{
					this.m_Payload = data;
				}
				this.m_Ptr = UploadHandlerRaw.Create(this, (byte*)data.GetUnsafeReadOnlyPtr<byte>(), data.Length);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005D48 File Offset: 0x00003F48
		public unsafe UploadHandlerRaw(NativeArray<byte>.ReadOnly data)
		{
			bool flag = !data.IsCreated || data.Length == 0;
			if (flag)
			{
				this.m_Ptr = UploadHandlerRaw.Create(this, null, 0);
			}
			else
			{
				bool flag2 = data.Length == 0;
				if (flag2)
				{
					this.m_Ptr = UploadHandlerRaw.Create(this, null, 0);
				}
				else
				{
					this.m_Ptr = UploadHandlerRaw.Create(this, (byte*)data.GetUnsafeReadOnlyPtr<byte>(), data.Length);
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005DC4 File Offset: 0x00003FC4
		internal override byte[] GetData()
		{
			bool isCreated = this.m_Payload.IsCreated;
			byte[] array;
			if (isCreated)
			{
				array = this.m_Payload.ToArray();
			}
			else
			{
				array = null;
			}
			return array;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public override void Dispose()
		{
			bool isCreated = this.m_Payload.IsCreated;
			if (isCreated)
			{
				this.m_Payload.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x04000069 RID: 105
		private NativeArray<byte> m_Payload;

		// Token: 0x0200001C RID: 28
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600015D RID: 349 RVA: 0x00005CA2 File Offset: 0x00003EA2
			public static IntPtr ConvertToNative(UploadHandlerRaw uploadHandler)
			{
				return uploadHandler.m_Ptr;
			}
		}
	}
}
