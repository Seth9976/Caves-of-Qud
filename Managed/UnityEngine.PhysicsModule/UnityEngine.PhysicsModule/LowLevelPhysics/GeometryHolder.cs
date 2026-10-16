using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x02000060 RID: 96
	public struct GeometryHolder
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0000C048 File Offset: 0x0000A248
		private void SetGeometry<T>(T geometry) where T : struct, IGeometry
		{
			this.m_Type = (int)geometry.GeometryType;
			UnsafeUtility.CopyStructureToPtr<T>(ref geometry, UnsafeUtility.AddressOf<uint>(ref this.m_DataStart));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000C074 File Offset: 0x0000A274
		public T As<T>() where T : struct, IGeometry
		{
			T t = default(T);
			bool flag = t.GeometryType != (GeometryType)this.m_Type;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Unable to get geometry of type {0} from a geometry holder that stores {1}.", t.GeometryType, this.m_Type));
			}
			UnsafeUtility.CopyPtrToStructure<T>(UnsafeUtility.AddressOf<uint>(ref this.m_DataStart), out t);
			return t;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public static GeometryHolder Create<T>(T geometry) where T : struct, IGeometry
		{
			GeometryHolder geometryHolder = new GeometryHolder
			{
				m_DataStart = 0U,
				m_Type = -1,
				m_FakePointer0 = new IntPtr((long)((ulong)(-559038737))),
				m_FakePointer1 = new IntPtr((long)((ulong)(-559038737)))
			};
			geometryHolder.SetGeometry<T>(geometry);
			return geometryHolder;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0000C148 File Offset: 0x0000A348
		public GeometryType Type
		{
			get
			{
				return (GeometryType)this.m_Type;
			}
		}

		// Token: 0x04000193 RID: 403
		private int m_Type;

		// Token: 0x04000194 RID: 404
		private uint m_DataStart;

		// Token: 0x04000195 RID: 405
		private IntPtr m_FakePointer0;

		// Token: 0x04000196 RID: 406
		private IntPtr m_FakePointer1;

		// Token: 0x04000197 RID: 407
		[FixedBuffer(typeof(uint), 6)]
		private GeometryHolder.<m_Blob>e__FixedBuffer m_Blob;

		// Token: 0x02000061 RID: 97
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		public struct <m_Blob>e__FixedBuffer
		{
			// Token: 0x04000198 RID: 408
			public uint FixedElementField;
		}
	}
}
