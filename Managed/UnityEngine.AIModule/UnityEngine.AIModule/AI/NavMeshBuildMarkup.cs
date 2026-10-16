using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x02000020 RID: 32
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	public struct NavMeshBuildMarkup
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00004828 File Offset: 0x00002A28
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00004843 File Offset: 0x00002A43
		public bool overrideArea
		{
			get
			{
				return this.m_OverrideArea != 0;
			}
			set
			{
				this.m_OverrideArea = (value ? 1 : 0);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00004854 File Offset: 0x00002A54
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000486C File Offset: 0x00002A6C
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00004878 File Offset: 0x00002A78
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00004893 File Offset: 0x00002A93
		public bool overrideIgnore
		{
			get
			{
				return this.m_InheritIgnoreFromBuild == 0;
			}
			set
			{
				this.m_InheritIgnoreFromBuild = (value ? 0 : 1);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000048A4 File Offset: 0x00002AA4
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000048BF File Offset: 0x00002ABF
		public bool ignoreFromBuild
		{
			get
			{
				return this.m_IgnoreFromBuild != 0;
			}
			set
			{
				this.m_IgnoreFromBuild = (value ? 1 : 0);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000048D0 File Offset: 0x00002AD0
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000048EB File Offset: 0x00002AEB
		public bool overrideGenerateLinks
		{
			get
			{
				return this.m_OverrideGenerateLinks != 0;
			}
			set
			{
				this.m_OverrideGenerateLinks = (value ? 1 : 0);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000048FC File Offset: 0x00002AFC
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00004917 File Offset: 0x00002B17
		public bool generateLinks
		{
			get
			{
				return this.m_GenerateLinks != 0;
			}
			set
			{
				this.m_GenerateLinks = (value ? 1 : 0);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00004928 File Offset: 0x00002B28
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00004943 File Offset: 0x00002B43
		public bool applyToChildren
		{
			get
			{
				return this.m_IgnoreChildren == 0;
			}
			set
			{
				this.m_IgnoreChildren = (value ? 0 : 1);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00004954 File Offset: 0x00002B54
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00004971 File Offset: 0x00002B71
		public Transform root
		{
			get
			{
				return NavMeshBuildMarkup.InternalGetRootGO(this.m_InstanceID);
			}
			set
			{
				this.m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000498C File Offset: 0x00002B8C
		[StaticAccessor("NavMeshBuildMarkup", StaticAccessorType.DoubleColon)]
		private static Transform InternalGetRootGO(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Transform>(NavMeshBuildMarkup.InternalGetRootGO_Injected(instanceID));
		}

		// Token: 0x06000213 RID: 531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetRootGO_Injected(int instanceID);

		// Token: 0x04000060 RID: 96
		private int m_OverrideArea;

		// Token: 0x04000061 RID: 97
		private int m_Area;

		// Token: 0x04000062 RID: 98
		private int m_InheritIgnoreFromBuild;

		// Token: 0x04000063 RID: 99
		private int m_IgnoreFromBuild;

		// Token: 0x04000064 RID: 100
		private int m_OverrideGenerateLinks;

		// Token: 0x04000065 RID: 101
		private int m_GenerateLinks;

		// Token: 0x04000066 RID: 102
		private int m_InstanceID;

		// Token: 0x04000067 RID: 103
		private int m_IgnoreChildren;
	}
}
