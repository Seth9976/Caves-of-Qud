using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000005 RID: 5
	internal enum VFXExpressionOperation
	{
		// Token: 0x0400000B RID: 11
		None,
		// Token: 0x0400000C RID: 12
		Value,
		// Token: 0x0400000D RID: 13
		Combine2f,
		// Token: 0x0400000E RID: 14
		Combine3f,
		// Token: 0x0400000F RID: 15
		Combine4f,
		// Token: 0x04000010 RID: 16
		ExtractComponent,
		// Token: 0x04000011 RID: 17
		DeltaTime,
		// Token: 0x04000012 RID: 18
		TotalTime,
		// Token: 0x04000013 RID: 19
		SystemSeed,
		// Token: 0x04000014 RID: 20
		LocalToWorld,
		// Token: 0x04000015 RID: 21
		WorldToLocal,
		// Token: 0x04000016 RID: 22
		FrameIndex,
		// Token: 0x04000017 RID: 23
		PlayRate,
		// Token: 0x04000018 RID: 24
		UnscaledDeltaTime,
		// Token: 0x04000019 RID: 25
		ManagerMaxDeltaTime,
		// Token: 0x0400001A RID: 26
		ManagerFixedTimeStep,
		// Token: 0x0400001B RID: 27
		GameDeltaTime,
		// Token: 0x0400001C RID: 28
		GameUnscaledDeltaTime,
		// Token: 0x0400001D RID: 29
		GameSmoothDeltaTime,
		// Token: 0x0400001E RID: 30
		GameTotalTime,
		// Token: 0x0400001F RID: 31
		GameUnscaledTotalTime,
		// Token: 0x04000020 RID: 32
		GameTotalTimeSinceSceneLoad,
		// Token: 0x04000021 RID: 33
		GameTimeScale,
		// Token: 0x04000022 RID: 34
		Sin,
		// Token: 0x04000023 RID: 35
		Cos,
		// Token: 0x04000024 RID: 36
		Tan,
		// Token: 0x04000025 RID: 37
		ASin,
		// Token: 0x04000026 RID: 38
		ACos,
		// Token: 0x04000027 RID: 39
		ATan,
		// Token: 0x04000028 RID: 40
		Abs,
		// Token: 0x04000029 RID: 41
		Sign,
		// Token: 0x0400002A RID: 42
		Saturate,
		// Token: 0x0400002B RID: 43
		Ceil,
		// Token: 0x0400002C RID: 44
		Round,
		// Token: 0x0400002D RID: 45
		Frac,
		// Token: 0x0400002E RID: 46
		Floor,
		// Token: 0x0400002F RID: 47
		Log2,
		// Token: 0x04000030 RID: 48
		Mul,
		// Token: 0x04000031 RID: 49
		Divide,
		// Token: 0x04000032 RID: 50
		Add,
		// Token: 0x04000033 RID: 51
		Subtract,
		// Token: 0x04000034 RID: 52
		Min,
		// Token: 0x04000035 RID: 53
		Max,
		// Token: 0x04000036 RID: 54
		Pow,
		// Token: 0x04000037 RID: 55
		ATan2,
		// Token: 0x04000038 RID: 56
		TRSToMatrix,
		// Token: 0x04000039 RID: 57
		InverseMatrix,
		// Token: 0x0400003A RID: 58
		InverseTRSMatrix,
		// Token: 0x0400003B RID: 59
		TransposeMatrix,
		// Token: 0x0400003C RID: 60
		ExtractPositionFromMatrix,
		// Token: 0x0400003D RID: 61
		ExtractAnglesFromMatrix,
		// Token: 0x0400003E RID: 62
		ExtractScaleFromMatrix,
		// Token: 0x0400003F RID: 63
		TransformMatrix,
		// Token: 0x04000040 RID: 64
		TransformPos,
		// Token: 0x04000041 RID: 65
		TransformVec,
		// Token: 0x04000042 RID: 66
		TransformDir,
		// Token: 0x04000043 RID: 67
		TransformVector4,
		// Token: 0x04000044 RID: 68
		RowToMatrix,
		// Token: 0x04000045 RID: 69
		ColumnToMatrix,
		// Token: 0x04000046 RID: 70
		AxisToMatrix,
		// Token: 0x04000047 RID: 71
		MatrixToRow,
		// Token: 0x04000048 RID: 72
		MatrixToColumn,
		// Token: 0x04000049 RID: 73
		MatrixToAxis,
		// Token: 0x0400004A RID: 74
		SampleCurve,
		// Token: 0x0400004B RID: 75
		SampleGradient,
		// Token: 0x0400004C RID: 76
		SampleMeshVertexFloat,
		// Token: 0x0400004D RID: 77
		SampleMeshVertexFloat2,
		// Token: 0x0400004E RID: 78
		SampleMeshVertexFloat3,
		// Token: 0x0400004F RID: 79
		SampleMeshVertexFloat4,
		// Token: 0x04000050 RID: 80
		SampleMeshVertexColor,
		// Token: 0x04000051 RID: 81
		SampleMeshIndex,
		// Token: 0x04000052 RID: 82
		VertexBufferFromMesh,
		// Token: 0x04000053 RID: 83
		VertexBufferFromSkinnedMeshRenderer,
		// Token: 0x04000054 RID: 84
		IndexBufferFromMesh,
		// Token: 0x04000055 RID: 85
		MeshFromSkinnedMeshRenderer,
		// Token: 0x04000056 RID: 86
		RootBoneTransformFromSkinnedMeshRenderer,
		// Token: 0x04000057 RID: 87
		BakeCurve,
		// Token: 0x04000058 RID: 88
		BakeGradient,
		// Token: 0x04000059 RID: 89
		BitwiseLeftShift,
		// Token: 0x0400005A RID: 90
		BitwiseRightShift,
		// Token: 0x0400005B RID: 91
		BitwiseOr,
		// Token: 0x0400005C RID: 92
		BitwiseAnd,
		// Token: 0x0400005D RID: 93
		BitwiseXor,
		// Token: 0x0400005E RID: 94
		BitwiseComplement,
		// Token: 0x0400005F RID: 95
		CastUintToFloat,
		// Token: 0x04000060 RID: 96
		CastIntToFloat,
		// Token: 0x04000061 RID: 97
		CastFloatToUint,
		// Token: 0x04000062 RID: 98
		CastIntToUint,
		// Token: 0x04000063 RID: 99
		CastFloatToInt,
		// Token: 0x04000064 RID: 100
		CastUintToInt,
		// Token: 0x04000065 RID: 101
		CastIntToBool,
		// Token: 0x04000066 RID: 102
		CastUintToBool,
		// Token: 0x04000067 RID: 103
		CastFloatToBool,
		// Token: 0x04000068 RID: 104
		CastBoolToInt,
		// Token: 0x04000069 RID: 105
		CastBoolToUint,
		// Token: 0x0400006A RID: 106
		CastBoolToFloat,
		// Token: 0x0400006B RID: 107
		RGBtoHSV,
		// Token: 0x0400006C RID: 108
		HSVtoRGB,
		// Token: 0x0400006D RID: 109
		Condition,
		// Token: 0x0400006E RID: 110
		Branch,
		// Token: 0x0400006F RID: 111
		GenerateRandom,
		// Token: 0x04000070 RID: 112
		GenerateFixedRandom,
		// Token: 0x04000071 RID: 113
		ExtractMatrixFromMainCamera,
		// Token: 0x04000072 RID: 114
		ExtractFOVFromMainCamera,
		// Token: 0x04000073 RID: 115
		ExtractNearPlaneFromMainCamera,
		// Token: 0x04000074 RID: 116
		ExtractFarPlaneFromMainCamera,
		// Token: 0x04000075 RID: 117
		ExtractAspectRatioFromMainCamera,
		// Token: 0x04000076 RID: 118
		ExtractPixelDimensionsFromMainCamera,
		// Token: 0x04000077 RID: 119
		ExtractScaledPixelDimensionsFromMainCamera,
		// Token: 0x04000078 RID: 120
		ExtractLensShiftFromMainCamera,
		// Token: 0x04000079 RID: 121
		GetBufferFromMainCamera,
		// Token: 0x0400007A RID: 122
		IsMainCameraOrthographic,
		// Token: 0x0400007B RID: 123
		GetOrthographicSizeFromMainCamera,
		// Token: 0x0400007C RID: 124
		LogicalAnd,
		// Token: 0x0400007D RID: 125
		LogicalOr,
		// Token: 0x0400007E RID: 126
		LogicalNot,
		// Token: 0x0400007F RID: 127
		ValueNoise1D,
		// Token: 0x04000080 RID: 128
		ValueNoise2D,
		// Token: 0x04000081 RID: 129
		ValueNoise3D,
		// Token: 0x04000082 RID: 130
		ValueCurlNoise2D,
		// Token: 0x04000083 RID: 131
		ValueCurlNoise3D,
		// Token: 0x04000084 RID: 132
		PerlinNoise1D,
		// Token: 0x04000085 RID: 133
		PerlinNoise2D,
		// Token: 0x04000086 RID: 134
		PerlinNoise3D,
		// Token: 0x04000087 RID: 135
		PerlinCurlNoise2D,
		// Token: 0x04000088 RID: 136
		PerlinCurlNoise3D,
		// Token: 0x04000089 RID: 137
		CellularNoise1D,
		// Token: 0x0400008A RID: 138
		CellularNoise2D,
		// Token: 0x0400008B RID: 139
		CellularNoise3D,
		// Token: 0x0400008C RID: 140
		CellularCurlNoise2D,
		// Token: 0x0400008D RID: 141
		CellularCurlNoise3D,
		// Token: 0x0400008E RID: 142
		VoroNoise2D,
		// Token: 0x0400008F RID: 143
		MeshVertexCount,
		// Token: 0x04000090 RID: 144
		MeshChannelOffset,
		// Token: 0x04000091 RID: 145
		MeshChannelInfos,
		// Token: 0x04000092 RID: 146
		MeshVertexStride,
		// Token: 0x04000093 RID: 147
		MeshIndexCount,
		// Token: 0x04000094 RID: 148
		MeshIndexFormat,
		// Token: 0x04000095 RID: 149
		BufferStride,
		// Token: 0x04000096 RID: 150
		BufferCount,
		// Token: 0x04000097 RID: 151
		TextureWidth,
		// Token: 0x04000098 RID: 152
		TextureHeight,
		// Token: 0x04000099 RID: 153
		TextureDepth,
		// Token: 0x0400009A RID: 154
		TextureFormat,
		// Token: 0x0400009B RID: 155
		ReadEventAttribute,
		// Token: 0x0400009C RID: 156
		SpawnerStateNewLoop,
		// Token: 0x0400009D RID: 157
		SpawnerStateLoopState,
		// Token: 0x0400009E RID: 158
		SpawnerStateSpawnCount,
		// Token: 0x0400009F RID: 159
		SpawnerStateDeltaTime,
		// Token: 0x040000A0 RID: 160
		SpawnerStateTotalTime,
		// Token: 0x040000A1 RID: 161
		SpawnerStateDelayBeforeLoop,
		// Token: 0x040000A2 RID: 162
		SpawnerStateLoopDuration,
		// Token: 0x040000A3 RID: 163
		SpawnerStateDelayAfterLoop,
		// Token: 0x040000A4 RID: 164
		SpawnerStateLoopIndex,
		// Token: 0x040000A5 RID: 165
		SpawnerStateLoopCount
	}
}
