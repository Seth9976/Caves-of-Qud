using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	// Token: 0x020001FF RID: 511
	public static class HashUtilities
	{
		// Token: 0x06001B36 RID: 6966 RVA: 0x0002FDC8 File Offset: 0x0002DFC8
		public unsafe static void AppendHash(ref Hash128 inHash, ref Hash128 outHash)
		{
			fixed (Hash128* ptr = &outHash)
			{
				Hash128* ptr2 = ptr;
				fixed (Hash128* ptr3 = &inHash)
				{
					Hash128* ptr4 = ptr3;
					HashUnsafeUtilities.ComputeHash128((void*)ptr4, (ulong)((long)sizeof(Hash128)), ptr2);
				}
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0002FDFC File Offset: 0x0002DFFC
		public unsafe static void QuantisedMatrixHash(ref Matrix4x4 value, ref Hash128 hash)
		{
			fixed (Hash128* ptr = &hash)
			{
				Hash128* ptr2 = ptr;
				int* ptr3 = stackalloc int[(UIntPtr)64];
				for (int i = 0; i < 16; i++)
				{
					ptr3[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128((void*)ptr3, 64UL, ptr2);
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0002FE58 File Offset: 0x0002E058
		public unsafe static void QuantisedVectorHash(ref Vector3 value, ref Hash128 hash)
		{
			fixed (Hash128* ptr = &hash)
			{
				Hash128* ptr2 = ptr;
				int* ptr3 = stackalloc int[(UIntPtr)12];
				for (int i = 0; i < 3; i++)
				{
					ptr3[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128((void*)ptr3, 12UL, ptr2);
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0002FEB0 File Offset: 0x0002E0B0
		public unsafe static void ComputeHash128<T>(ref T value, ref Hash128 hash) where T : struct
		{
			void* ptr = UnsafeUtility.AddressOf<T>(ref value);
			ulong num = (ulong)((long)UnsafeUtility.SizeOf<T>());
			Hash128* ptr2 = (Hash128*)UnsafeUtility.AddressOf<Hash128>(ref hash);
			HashUnsafeUtilities.ComputeHash128(ptr, num, ptr2);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0002FEDC File Offset: 0x0002E0DC
		public unsafe static void ComputeHash128(byte[] value, ref Hash128 hash)
		{
			fixed (byte* ptr = &value[0])
			{
				byte* ptr2 = ptr;
				ulong num = (ulong)((long)value.Length);
				Hash128* ptr3 = (Hash128*)UnsafeUtility.AddressOf<Hash128>(ref hash);
				HashUnsafeUtilities.ComputeHash128((void*)ptr2, num, ptr3);
			}
		}
	}
}
