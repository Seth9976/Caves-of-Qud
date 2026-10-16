using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadingCache.h")]
	[UsedByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal static class AssetBundleLoadingCache
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000062 RID: 98
		// (set) Token: 0x06000063 RID: 99
		internal static extern uint maxBlocksPerFile
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000064 RID: 100
		// (set) Token: 0x06000065 RID: 101
		internal static extern uint blockCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000066 RID: 102
		internal static extern uint blockSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002DAC File Offset: 0x00000FAC
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002DCC File Offset: 0x00000FCC
		internal static uint memoryBudgetKB
		{
			get
			{
				return AssetBundleLoadingCache.blockCount * AssetBundleLoadingCache.blockSize;
			}
			set
			{
				uint num = Math.Max(value / AssetBundleLoadingCache.blockSize, 2U);
				uint num2 = Math.Max(AssetBundleLoadingCache.blockCount / 4U, 2U);
				bool flag = num != AssetBundleLoadingCache.blockCount || num2 != AssetBundleLoadingCache.maxBlocksPerFile;
				if (flag)
				{
					AssetBundleLoadingCache.blockCount = num;
					AssetBundleLoadingCache.maxBlocksPerFile = num2;
				}
			}
		}

		// Token: 0x04000010 RID: 16
		internal const int kMinAllowedBlockCount = 2;

		// Token: 0x04000011 RID: 17
		internal const int kMinAllowedMaxBlocksPerFile = 2;
	}
}
