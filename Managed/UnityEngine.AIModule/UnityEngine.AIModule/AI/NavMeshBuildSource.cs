using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.AI
{
	// Token: 0x0200001F RID: 31
	[UsedByNativeCode]
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	public struct NavMeshBuildSource
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000046CC File Offset: 0x000028CC
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000046E4 File Offset: 0x000028E4
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000046F0 File Offset: 0x000028F0
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00004708 File Offset: 0x00002908
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00004714 File Offset: 0x00002914
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000472C File Offset: 0x0000292C
		public NavMeshBuildSourceShape shape
		{
			get
			{
				return this.m_Shape;
			}
			set
			{
				this.m_Shape = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00004738 File Offset: 0x00002938
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00004750 File Offset: 0x00002950
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000475C File Offset: 0x0000295C
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00004777 File Offset: 0x00002977
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00004788 File Offset: 0x00002988
		// (set) Token: 0x060001FB RID: 507 RVA: 0x000047A5 File Offset: 0x000029A5
		public Object sourceObject
		{
			get
			{
				return NavMeshBuildSource.InternalGetObject(this.m_InstanceID);
			}
			set
			{
				this.m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000047C0 File Offset: 0x000029C0
		// (set) Token: 0x060001FD RID: 509 RVA: 0x000047DD File Offset: 0x000029DD
		public Component component
		{
			get
			{
				return NavMeshBuildSource.InternalGetComponent(this.m_ComponentID);
			}
			set
			{
				this.m_ComponentID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000047F8 File Offset: 0x000029F8
		[StaticAccessor("NavMeshBuildSource", StaticAccessorType.DoubleColon)]
		private static Component InternalGetComponent(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Component>(NavMeshBuildSource.InternalGetComponent_Injected(instanceID));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00004810 File Offset: 0x00002A10
		[StaticAccessor("NavMeshBuildSource", StaticAccessorType.DoubleColon)]
		private static Object InternalGetObject(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(NavMeshBuildSource.InternalGetObject_Injected(instanceID));
		}

		// Token: 0x06000200 RID: 512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetComponent_Injected(int instanceID);

		// Token: 0x06000201 RID: 513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetObject_Injected(int instanceID);

		// Token: 0x04000059 RID: 89
		private Matrix4x4 m_Transform;

		// Token: 0x0400005A RID: 90
		private Vector3 m_Size;

		// Token: 0x0400005B RID: 91
		private NavMeshBuildSourceShape m_Shape;

		// Token: 0x0400005C RID: 92
		private int m_Area;

		// Token: 0x0400005D RID: 93
		private int m_InstanceID;

		// Token: 0x0400005E RID: 94
		private int m_ComponentID;

		// Token: 0x0400005F RID: 95
		private int m_GenerateLinks;
	}
}
