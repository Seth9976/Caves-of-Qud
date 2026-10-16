using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("PhysicsJobOptions2D", "struct PhysicsJobOptions2D;")]
	[NativeHeader("Modules/Physics2D/Public/Physics2DSettings.h")]
	public struct PhysicsJobOptions2D
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000080CC File Offset: 0x000062CC
		// (set) Token: 0x0600028F RID: 655 RVA: 0x000080E4 File Offset: 0x000062E4
		public bool useMultithreading
		{
			get
			{
				return this.m_UseMultithreading;
			}
			set
			{
				this.m_UseMultithreading = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000080F0 File Offset: 0x000062F0
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00008108 File Offset: 0x00006308
		public bool useConsistencySorting
		{
			get
			{
				return this.m_UseConsistencySorting;
			}
			set
			{
				this.m_UseConsistencySorting = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00008114 File Offset: 0x00006314
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000812C File Offset: 0x0000632C
		public int interpolationPosesPerJob
		{
			get
			{
				return this.m_InterpolationPosesPerJob;
			}
			set
			{
				this.m_InterpolationPosesPerJob = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00008138 File Offset: 0x00006338
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00008150 File Offset: 0x00006350
		public int newContactsPerJob
		{
			get
			{
				return this.m_NewContactsPerJob;
			}
			set
			{
				this.m_NewContactsPerJob = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000815C File Offset: 0x0000635C
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00008174 File Offset: 0x00006374
		public int collideContactsPerJob
		{
			get
			{
				return this.m_CollideContactsPerJob;
			}
			set
			{
				this.m_CollideContactsPerJob = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00008180 File Offset: 0x00006380
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00008198 File Offset: 0x00006398
		public int clearFlagsPerJob
		{
			get
			{
				return this.m_ClearFlagsPerJob;
			}
			set
			{
				this.m_ClearFlagsPerJob = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000081A4 File Offset: 0x000063A4
		// (set) Token: 0x0600029B RID: 667 RVA: 0x000081BC File Offset: 0x000063BC
		public int clearBodyForcesPerJob
		{
			get
			{
				return this.m_ClearBodyForcesPerJob;
			}
			set
			{
				this.m_ClearBodyForcesPerJob = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000081C8 File Offset: 0x000063C8
		// (set) Token: 0x0600029D RID: 669 RVA: 0x000081E0 File Offset: 0x000063E0
		public int syncDiscreteFixturesPerJob
		{
			get
			{
				return this.m_SyncDiscreteFixturesPerJob;
			}
			set
			{
				this.m_SyncDiscreteFixturesPerJob = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000081EC File Offset: 0x000063EC
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00008204 File Offset: 0x00006404
		public int syncContinuousFixturesPerJob
		{
			get
			{
				return this.m_SyncContinuousFixturesPerJob;
			}
			set
			{
				this.m_SyncContinuousFixturesPerJob = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00008210 File Offset: 0x00006410
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00008228 File Offset: 0x00006428
		public int findNearestContactsPerJob
		{
			get
			{
				return this.m_FindNearestContactsPerJob;
			}
			set
			{
				this.m_FindNearestContactsPerJob = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00008234 File Offset: 0x00006434
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000824C File Offset: 0x0000644C
		public int updateTriggerContactsPerJob
		{
			get
			{
				return this.m_UpdateTriggerContactsPerJob;
			}
			set
			{
				this.m_UpdateTriggerContactsPerJob = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00008258 File Offset: 0x00006458
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x00008270 File Offset: 0x00006470
		public int islandSolverCostThreshold
		{
			get
			{
				return this.m_IslandSolverCostThreshold;
			}
			set
			{
				this.m_IslandSolverCostThreshold = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000827C File Offset: 0x0000647C
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00008294 File Offset: 0x00006494
		public int islandSolverBodyCostScale
		{
			get
			{
				return this.m_IslandSolverBodyCostScale;
			}
			set
			{
				this.m_IslandSolverBodyCostScale = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000082A0 File Offset: 0x000064A0
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x000082B8 File Offset: 0x000064B8
		public int islandSolverContactCostScale
		{
			get
			{
				return this.m_IslandSolverContactCostScale;
			}
			set
			{
				this.m_IslandSolverContactCostScale = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000082C4 File Offset: 0x000064C4
		// (set) Token: 0x060002AB RID: 683 RVA: 0x000082DC File Offset: 0x000064DC
		public int islandSolverJointCostScale
		{
			get
			{
				return this.m_IslandSolverJointCostScale;
			}
			set
			{
				this.m_IslandSolverJointCostScale = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000082E8 File Offset: 0x000064E8
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00008300 File Offset: 0x00006500
		public int islandSolverBodiesPerJob
		{
			get
			{
				return this.m_IslandSolverBodiesPerJob;
			}
			set
			{
				this.m_IslandSolverBodiesPerJob = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000830C File Offset: 0x0000650C
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00008324 File Offset: 0x00006524
		public int islandSolverContactsPerJob
		{
			get
			{
				return this.m_IslandSolverContactsPerJob;
			}
			set
			{
				this.m_IslandSolverContactsPerJob = value;
			}
		}

		// Token: 0x04000096 RID: 150
		private bool m_UseMultithreading;

		// Token: 0x04000097 RID: 151
		private bool m_UseConsistencySorting;

		// Token: 0x04000098 RID: 152
		private int m_InterpolationPosesPerJob;

		// Token: 0x04000099 RID: 153
		private int m_NewContactsPerJob;

		// Token: 0x0400009A RID: 154
		private int m_CollideContactsPerJob;

		// Token: 0x0400009B RID: 155
		private int m_ClearFlagsPerJob;

		// Token: 0x0400009C RID: 156
		private int m_ClearBodyForcesPerJob;

		// Token: 0x0400009D RID: 157
		private int m_SyncDiscreteFixturesPerJob;

		// Token: 0x0400009E RID: 158
		private int m_SyncContinuousFixturesPerJob;

		// Token: 0x0400009F RID: 159
		private int m_FindNearestContactsPerJob;

		// Token: 0x040000A0 RID: 160
		private int m_UpdateTriggerContactsPerJob;

		// Token: 0x040000A1 RID: 161
		private int m_IslandSolverCostThreshold;

		// Token: 0x040000A2 RID: 162
		private int m_IslandSolverBodyCostScale;

		// Token: 0x040000A3 RID: 163
		private int m_IslandSolverContactCostScale;

		// Token: 0x040000A4 RID: 164
		private int m_IslandSolverJointCostScale;

		// Token: 0x040000A5 RID: 165
		private int m_IslandSolverBodiesPerJob;

		// Token: 0x040000A6 RID: 166
		private int m_IslandSolverContactsPerJob;
	}
}
