using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000507 RID: 1287
	public struct BatchFilterSettings
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x00060A04 File Offset: 0x0005EC04
		// (set) Token: 0x060031E0 RID: 12768 RVA: 0x00060A0C File Offset: 0x0005EC0C
		public byte batchLayer
		{
			get
			{
				return this.m_batchLayer;
			}
			set
			{
				this.m_batchLayer = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x00060A15 File Offset: 0x0005EC15
		// (set) Token: 0x060031E2 RID: 12770 RVA: 0x00060A1D File Offset: 0x0005EC1D
		public MotionVectorGenerationMode motionMode
		{
			get
			{
				return (MotionVectorGenerationMode)this.m_motionMode;
			}
			set
			{
				this.m_motionMode = (byte)value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x00060A27 File Offset: 0x0005EC27
		// (set) Token: 0x060031E4 RID: 12772 RVA: 0x00060A2F File Offset: 0x0005EC2F
		public ShadowCastingMode shadowCastingMode
		{
			get
			{
				return (ShadowCastingMode)this.m_shadowMode;
			}
			set
			{
				this.m_shadowMode = (byte)value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x00060A39 File Offset: 0x0005EC39
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x00060A44 File Offset: 0x0005EC44
		public bool receiveShadows
		{
			get
			{
				return this.m_receiveShadows > 0;
			}
			set
			{
				this.m_receiveShadows = (value ? 1 : 0);
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x00060A54 File Offset: 0x0005EC54
		// (set) Token: 0x060031E8 RID: 12776 RVA: 0x00060A5F File Offset: 0x0005EC5F
		public bool staticShadowCaster
		{
			get
			{
				return this.m_staticShadowCaster > 0;
			}
			set
			{
				this.m_staticShadowCaster = (value ? 1 : 0);
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x00060A6F File Offset: 0x0005EC6F
		// (set) Token: 0x060031EA RID: 12778 RVA: 0x00060A7A File Offset: 0x0005EC7A
		public bool allDepthSorted
		{
			get
			{
				return this.m_allDepthSorted > 0;
			}
			set
			{
				this.m_allDepthSorted = (value ? 1 : 0);
			}
		}

		// Token: 0x060031EB RID: 12779
		[FreeFunction("BatchFilterSettings::DefaultCullingMask", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong DefaultCullingMask();

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x00060A8A File Offset: 0x0005EC8A
		// (set) Token: 0x060031ED RID: 12781 RVA: 0x00060AA1 File Offset: 0x0005ECA1
		public ulong sceneCullingMask
		{
			get
			{
				return (this.m_isSceneCullingMaskSet != 0) ? this.m_sceneCullingMask : BatchFilterSettings.DefaultCullingMask();
			}
			set
			{
				this.m_isSceneCullingMaskSet = 1;
				this.m_sceneCullingMask = value;
			}
		}

		// Token: 0x04000FD4 RID: 4052
		public uint renderingLayerMask;

		// Token: 0x04000FD5 RID: 4053
		public int rendererPriority;

		// Token: 0x04000FD6 RID: 4054
		private ulong m_sceneCullingMask;

		// Token: 0x04000FD7 RID: 4055
		public byte layer;

		// Token: 0x04000FD8 RID: 4056
		private byte m_batchLayer;

		// Token: 0x04000FD9 RID: 4057
		private byte m_motionMode;

		// Token: 0x04000FDA RID: 4058
		private byte m_shadowMode;

		// Token: 0x04000FDB RID: 4059
		private byte m_receiveShadows;

		// Token: 0x04000FDC RID: 4060
		private byte m_staticShadowCaster;

		// Token: 0x04000FDD RID: 4061
		private byte m_allDepthSorted;

		// Token: 0x04000FDE RID: 4062
		private byte m_isSceneCullingMaskSet;
	}
}
