using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Modules/AI/Public/NavMeshBuildSettings.h")]
	public struct NavMeshBuildSettings
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000049A4 File Offset: 0x00002BA4
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000049BC File Offset: 0x00002BBC
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000049C8 File Offset: 0x00002BC8
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000049E0 File Offset: 0x00002BE0
		public float agentRadius
		{
			get
			{
				return this.m_AgentRadius;
			}
			set
			{
				this.m_AgentRadius = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000049EC File Offset: 0x00002BEC
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00004A04 File Offset: 0x00002C04
		public float agentHeight
		{
			get
			{
				return this.m_AgentHeight;
			}
			set
			{
				this.m_AgentHeight = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00004A10 File Offset: 0x00002C10
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00004A28 File Offset: 0x00002C28
		public float agentSlope
		{
			get
			{
				return this.m_AgentSlope;
			}
			set
			{
				this.m_AgentSlope = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00004A34 File Offset: 0x00002C34
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00004A4C File Offset: 0x00002C4C
		public float agentClimb
		{
			get
			{
				return this.m_AgentClimb;
			}
			set
			{
				this.m_AgentClimb = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00004A58 File Offset: 0x00002C58
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00004A70 File Offset: 0x00002C70
		public float ledgeDropHeight
		{
			get
			{
				return this.m_LedgeDropHeight;
			}
			set
			{
				this.m_LedgeDropHeight = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00004A7C File Offset: 0x00002C7C
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00004A94 File Offset: 0x00002C94
		public float maxJumpAcrossDistance
		{
			get
			{
				return this.m_MaxJumpAcrossDistance;
			}
			set
			{
				this.m_MaxJumpAcrossDistance = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00004AA0 File Offset: 0x00002CA0
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public float minRegionArea
		{
			get
			{
				return this.m_MinRegionArea;
			}
			set
			{
				this.m_MinRegionArea = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00004AC4 File Offset: 0x00002CC4
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00004ADF File Offset: 0x00002CDF
		public bool overrideVoxelSize
		{
			get
			{
				return this.m_OverrideVoxelSize != 0;
			}
			set
			{
				this.m_OverrideVoxelSize = (value ? 1 : 0);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00004AF0 File Offset: 0x00002CF0
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00004B08 File Offset: 0x00002D08
		public float voxelSize
		{
			get
			{
				return this.m_VoxelSize;
			}
			set
			{
				this.m_VoxelSize = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00004B14 File Offset: 0x00002D14
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00004B2F File Offset: 0x00002D2F
		public bool overrideTileSize
		{
			get
			{
				return this.m_OverrideTileSize != 0;
			}
			set
			{
				this.m_OverrideTileSize = (value ? 1 : 0);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00004B40 File Offset: 0x00002D40
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00004B58 File Offset: 0x00002D58
		public int tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00004B64 File Offset: 0x00002D64
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00004B7C File Offset: 0x00002D7C
		public uint maxJobWorkers
		{
			get
			{
				return this.m_MaxJobWorkers;
			}
			set
			{
				this.m_MaxJobWorkers = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00004B88 File Offset: 0x00002D88
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00004BA3 File Offset: 0x00002DA3
		public bool preserveTilesOutsideBounds
		{
			get
			{
				return this.m_PreserveTilesOutsideBounds != 0;
			}
			set
			{
				this.m_PreserveTilesOutsideBounds = (value ? 1 : 0);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00004BB4 File Offset: 0x00002DB4
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00004BCF File Offset: 0x00002DCF
		public bool buildHeightMesh
		{
			get
			{
				return this.m_BuildHeightMesh != 0;
			}
			set
			{
				this.m_BuildHeightMesh = (value ? 1 : 0);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00004BE0 File Offset: 0x00002DE0
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public NavMeshBuildDebugSettings debug
		{
			get
			{
				return this.m_Debug;
			}
			set
			{
				this.m_Debug = value;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00004C04 File Offset: 0x00002E04
		public string[] ValidationReport(Bounds buildBounds)
		{
			return NavMeshBuildSettings.InternalValidationReport(this, buildBounds);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00004C24 File Offset: 0x00002E24
		[NativeHeader("Modules/AI/Public/NavMeshBuildSettings.h")]
		[FreeFunction]
		private static string[] InternalValidationReport(NavMeshBuildSettings buildSettings, Bounds buildBounds)
		{
			return NavMeshBuildSettings.InternalValidationReport_Injected(ref buildSettings, ref buildBounds);
		}

		// Token: 0x06000236 RID: 566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] InternalValidationReport_Injected([In] ref NavMeshBuildSettings buildSettings, [In] ref Bounds buildBounds);

		// Token: 0x04000068 RID: 104
		private int m_AgentTypeID;

		// Token: 0x04000069 RID: 105
		private float m_AgentRadius;

		// Token: 0x0400006A RID: 106
		private float m_AgentHeight;

		// Token: 0x0400006B RID: 107
		private float m_AgentSlope;

		// Token: 0x0400006C RID: 108
		private float m_AgentClimb;

		// Token: 0x0400006D RID: 109
		private float m_LedgeDropHeight;

		// Token: 0x0400006E RID: 110
		private float m_MaxJumpAcrossDistance;

		// Token: 0x0400006F RID: 111
		private float m_MinRegionArea;

		// Token: 0x04000070 RID: 112
		private int m_OverrideVoxelSize;

		// Token: 0x04000071 RID: 113
		private float m_VoxelSize;

		// Token: 0x04000072 RID: 114
		private int m_OverrideTileSize;

		// Token: 0x04000073 RID: 115
		private int m_TileSize;

		// Token: 0x04000074 RID: 116
		private int m_BuildHeightMesh;

		// Token: 0x04000075 RID: 117
		private uint m_MaxJobWorkers;

		// Token: 0x04000076 RID: 118
		private int m_PreserveTilesOutsideBounds;

		// Token: 0x04000077 RID: 119
		private NavMeshBuildDebugSettings m_Debug;
	}
}
