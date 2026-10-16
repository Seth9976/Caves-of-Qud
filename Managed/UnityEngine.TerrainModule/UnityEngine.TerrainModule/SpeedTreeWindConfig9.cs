using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	internal struct SpeedTreeWindConfig9
	{
		// Token: 0x060000DC RID: 220 RVA: 0x0000306C File Offset: 0x0000126C
		public SpeedTreeWindConfig9()
		{
			this.strengthResponse = 5f;
			this.directionResponse = 2.5f;
			this.gustFrequency = 0f;
			this.gustStrengthMin = 0.5f;
			this.gustStrengthMax = 1f;
			this.gustDurationMin = 1f;
			this.gustDurationMax = 4f;
			this.gustRiseScalar = 1f;
			this.gustFallScalar = 1f;
			this.branch1StretchLimit = 1f;
			this.branch2StretchLimit = 1f;
			this.sharedHeightStart = 0f;
			this.independenceShared = 0f;
			this.independenceBranch1 = 0f;
			this.independenceBranch2 = 0f;
			this.independenceRipple = 0f;
			this.shimmerRipple = 0f;
			this.windIndependence = 0f;
			this.treeExtentX = 0f;
			this.treeExtentY = 0f;
			this.treeExtentZ = 0f;
			this.doShared = 0;
			this.doBranch1 = 0;
			this.doBranch2 = 0;
			this.doRipple = 0;
			this.doShimmer = 0;
			this.lodFade = 0;
			this.importScale = 1f;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003196 File Offset: 0x00001396
		public readonly bool IsWindEnabled
		{
			get
			{
				return this.doShared != 0 || this.doBranch1 != 0 || this.doBranch2 != 0 || this.doRipple != 0;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000031BC File Offset: 0x000013BC
		public static byte[] Serialize(SpeedTreeWindConfig9 config)
		{
			int num = Marshal.SizeOf<SpeedTreeWindConfig9>(config);
			byte[] array = new byte[num];
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				IntPtr intPtr = gchandle.AddrOfPinnedObject();
				Marshal.StructureToPtr<SpeedTreeWindConfig9>(config, intPtr, false);
			}
			finally
			{
				gchandle.Free();
			}
			return array;
		}

		// Token: 0x04000019 RID: 25
		public float strengthResponse;

		// Token: 0x0400001A RID: 26
		public float directionResponse;

		// Token: 0x0400001B RID: 27
		public float gustFrequency;

		// Token: 0x0400001C RID: 28
		public float gustStrengthMin;

		// Token: 0x0400001D RID: 29
		public float gustStrengthMax;

		// Token: 0x0400001E RID: 30
		public float gustDurationMin;

		// Token: 0x0400001F RID: 31
		public float gustDurationMax;

		// Token: 0x04000020 RID: 32
		public float gustRiseScalar;

		// Token: 0x04000021 RID: 33
		public float gustFallScalar;

		// Token: 0x04000022 RID: 34
		public float branch1StretchLimit;

		// Token: 0x04000023 RID: 35
		public float branch2StretchLimit;

		// Token: 0x04000024 RID: 36
		public float sharedHeightStart;

		// Token: 0x04000025 RID: 37
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<bendShared>e__FixedBuffer bendShared;

		// Token: 0x04000026 RID: 38
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<oscillationShared>e__FixedBuffer oscillationShared;

		// Token: 0x04000027 RID: 39
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<speedShared>e__FixedBuffer speedShared;

		// Token: 0x04000028 RID: 40
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<turbulenceShared>e__FixedBuffer turbulenceShared;

		// Token: 0x04000029 RID: 41
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<flexibilityShared>e__FixedBuffer flexibilityShared;

		// Token: 0x0400002A RID: 42
		public float independenceShared;

		// Token: 0x0400002B RID: 43
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<bendBranch1>e__FixedBuffer bendBranch1;

		// Token: 0x0400002C RID: 44
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<oscillationBranch1>e__FixedBuffer oscillationBranch1;

		// Token: 0x0400002D RID: 45
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<speedBranch1>e__FixedBuffer speedBranch1;

		// Token: 0x0400002E RID: 46
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<turbulenceBranch1>e__FixedBuffer turbulenceBranch1;

		// Token: 0x0400002F RID: 47
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<flexibilityBranch1>e__FixedBuffer flexibilityBranch1;

		// Token: 0x04000030 RID: 48
		public float independenceBranch1;

		// Token: 0x04000031 RID: 49
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<bendBranch2>e__FixedBuffer bendBranch2;

		// Token: 0x04000032 RID: 50
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<oscillationBranch2>e__FixedBuffer oscillationBranch2;

		// Token: 0x04000033 RID: 51
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<speedBranch2>e__FixedBuffer speedBranch2;

		// Token: 0x04000034 RID: 52
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<turbulenceBranch2>e__FixedBuffer turbulenceBranch2;

		// Token: 0x04000035 RID: 53
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<flexibilityBranch2>e__FixedBuffer flexibilityBranch2;

		// Token: 0x04000036 RID: 54
		public float independenceBranch2;

		// Token: 0x04000037 RID: 55
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<planarRipple>e__FixedBuffer planarRipple;

		// Token: 0x04000038 RID: 56
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<directionalRipple>e__FixedBuffer directionalRipple;

		// Token: 0x04000039 RID: 57
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<speedRipple>e__FixedBuffer speedRipple;

		// Token: 0x0400003A RID: 58
		[FixedBuffer(typeof(float), 20)]
		public SpeedTreeWindConfig9.<flexibilityRipple>e__FixedBuffer flexibilityRipple;

		// Token: 0x0400003B RID: 59
		public float independenceRipple;

		// Token: 0x0400003C RID: 60
		public float shimmerRipple;

		// Token: 0x0400003D RID: 61
		public float treeExtentX;

		// Token: 0x0400003E RID: 62
		public float treeExtentY;

		// Token: 0x0400003F RID: 63
		public float treeExtentZ;

		// Token: 0x04000040 RID: 64
		public float windIndependence;

		// Token: 0x04000041 RID: 65
		public int doShared;

		// Token: 0x04000042 RID: 66
		public int doBranch1;

		// Token: 0x04000043 RID: 67
		public int doBranch2;

		// Token: 0x04000044 RID: 68
		public int doRipple;

		// Token: 0x04000045 RID: 69
		public int doShimmer;

		// Token: 0x04000046 RID: 70
		public int lodFade;

		// Token: 0x04000047 RID: 71
		public float importScale;

		// Token: 0x02000009 RID: 9
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <bendBranch1>e__FixedBuffer
		{
			// Token: 0x04000048 RID: 72
			public float FixedElementField;
		}

		// Token: 0x0200000A RID: 10
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <bendBranch2>e__FixedBuffer
		{
			// Token: 0x04000049 RID: 73
			public float FixedElementField;
		}

		// Token: 0x0200000B RID: 11
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <bendShared>e__FixedBuffer
		{
			// Token: 0x0400004A RID: 74
			public float FixedElementField;
		}

		// Token: 0x0200000C RID: 12
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <directionalRipple>e__FixedBuffer
		{
			// Token: 0x0400004B RID: 75
			public float FixedElementField;
		}

		// Token: 0x0200000D RID: 13
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <flexibilityBranch1>e__FixedBuffer
		{
			// Token: 0x0400004C RID: 76
			public float FixedElementField;
		}

		// Token: 0x0200000E RID: 14
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <flexibilityBranch2>e__FixedBuffer
		{
			// Token: 0x0400004D RID: 77
			public float FixedElementField;
		}

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <flexibilityRipple>e__FixedBuffer
		{
			// Token: 0x0400004E RID: 78
			public float FixedElementField;
		}

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <flexibilityShared>e__FixedBuffer
		{
			// Token: 0x0400004F RID: 79
			public float FixedElementField;
		}

		// Token: 0x02000011 RID: 17
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <oscillationBranch1>e__FixedBuffer
		{
			// Token: 0x04000050 RID: 80
			public float FixedElementField;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <oscillationBranch2>e__FixedBuffer
		{
			// Token: 0x04000051 RID: 81
			public float FixedElementField;
		}

		// Token: 0x02000013 RID: 19
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <oscillationShared>e__FixedBuffer
		{
			// Token: 0x04000052 RID: 82
			public float FixedElementField;
		}

		// Token: 0x02000014 RID: 20
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <planarRipple>e__FixedBuffer
		{
			// Token: 0x04000053 RID: 83
			public float FixedElementField;
		}

		// Token: 0x02000015 RID: 21
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <speedBranch1>e__FixedBuffer
		{
			// Token: 0x04000054 RID: 84
			public float FixedElementField;
		}

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <speedBranch2>e__FixedBuffer
		{
			// Token: 0x04000055 RID: 85
			public float FixedElementField;
		}

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <speedRipple>e__FixedBuffer
		{
			// Token: 0x04000056 RID: 86
			public float FixedElementField;
		}

		// Token: 0x02000018 RID: 24
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <speedShared>e__FixedBuffer
		{
			// Token: 0x04000057 RID: 87
			public float FixedElementField;
		}

		// Token: 0x02000019 RID: 25
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <turbulenceBranch1>e__FixedBuffer
		{
			// Token: 0x04000058 RID: 88
			public float FixedElementField;
		}

		// Token: 0x0200001A RID: 26
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <turbulenceBranch2>e__FixedBuffer
		{
			// Token: 0x04000059 RID: 89
			public float FixedElementField;
		}

		// Token: 0x0200001B RID: 27
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 80)]
		public struct <turbulenceShared>e__FixedBuffer
		{
			// Token: 0x0400005A RID: 90
			public float FixedElementField;
		}
	}
}
