using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000478 RID: 1144
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Graphics/Texture.h")]
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	public struct AsyncGPUReadbackRequest
	{
		// Token: 0x06002D17 RID: 11543
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Update();

		// Token: 0x06002D18 RID: 11544
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WaitForCompletion();

		// Token: 0x06002D19 RID: 11545 RVA: 0x00055448 File Offset: 0x00053648
		public unsafe NativeArray<T> GetData<T>(int layer = 0) where T : struct
		{
			bool flag = !this.done || this.hasError;
			if (flag)
			{
				throw new InvalidOperationException("Cannot access the data as it is not available");
			}
			bool flag2 = layer < 0 || layer >= this.layerCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Layer index is out of range {0} / {1}", layer, this.layerCount));
			}
			int num = UnsafeUtility.SizeOf<T>();
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataRaw(layer), this.layerDataSize / num, Allocator.None);
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000554D8 File Offset: 0x000536D8
		public bool done
		{
			get
			{
				return this.IsDone();
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000554F0 File Offset: 0x000536F0
		public bool hasError
		{
			get
			{
				return this.HasError();
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x00055508 File Offset: 0x00053708
		public int layerCount
		{
			get
			{
				return this.GetLayerCount();
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x00055520 File Offset: 0x00053720
		public int layerDataSize
		{
			get
			{
				return this.GetLayerDataSize();
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x00055538 File Offset: 0x00053738
		public int width
		{
			get
			{
				return this.GetWidth();
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x00055550 File Offset: 0x00053750
		public int height
		{
			get
			{
				return this.GetHeight();
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x00055568 File Offset: 0x00053768
		public int depth
		{
			get
			{
				return this.GetDepth();
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x00055580 File Offset: 0x00053780
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x00055598 File Offset: 0x00053798
		public bool forcePlayerLoopUpdate
		{
			get
			{
				return this.GetForcePlayerLoopUpdate();
			}
			set
			{
				this.SetForcePlayerLoopUpdate(value);
			}
		}

		// Token: 0x06002D23 RID: 11555
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsDone();

		// Token: 0x06002D24 RID: 11556
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasError();

		// Token: 0x06002D25 RID: 11557
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetLayerCount();

		// Token: 0x06002D26 RID: 11558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetLayerDataSize();

		// Token: 0x06002D27 RID: 11559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetWidth();

		// Token: 0x06002D28 RID: 11560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetHeight();

		// Token: 0x06002D29 RID: 11561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetDepth();

		// Token: 0x06002D2A RID: 11562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetForcePlayerLoopUpdate();

		// Token: 0x06002D2B RID: 11563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetForcePlayerLoopUpdate(bool b);

		// Token: 0x06002D2C RID: 11564
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetScriptingCallback(Action<AsyncGPUReadbackRequest> callback);

		// Token: 0x06002D2D RID: 11565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetDataRaw(int layer);

		// Token: 0x06002D2E RID: 11566 RVA: 0x000555A3 File Offset: 0x000537A3
		[RequiredByNativeCode]
		private static void InvokeCallback(Action<AsyncGPUReadbackRequest> callback, AsyncGPUReadbackRequest obj)
		{
			callback(obj);
		}

		// Token: 0x04000C81 RID: 3201
		internal IntPtr m_Ptr;

		// Token: 0x04000C82 RID: 3202
		internal int m_Version;
	}
}
