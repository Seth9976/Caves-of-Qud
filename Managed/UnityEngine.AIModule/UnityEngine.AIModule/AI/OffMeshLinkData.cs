using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000E RID: 14
	[MovedFrom("UnityEngine")]
	[NativeHeader("Modules/AI/Components/OffMeshLink.bindings.h")]
	public struct OffMeshLinkData
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000386A File Offset: 0x00001A6A
		public bool valid
		{
			get
			{
				return this.m_Valid != 0;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003875 File Offset: 0x00001A75
		public bool activated
		{
			get
			{
				return this.m_Activated != 0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003880 File Offset: 0x00001A80
		public OffMeshLinkType linkType
		{
			get
			{
				return this.m_LinkType;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003888 File Offset: 0x00001A88
		public Vector3 startPos
		{
			get
			{
				return this.m_StartPos;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003890 File Offset: 0x00001A90
		public Vector3 endPos
		{
			get
			{
				return this.m_EndPos;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003898 File Offset: 0x00001A98
		public Object owner
		{
			get
			{
				return OffMeshLinkData.GetLinkOwnerInternal(this.m_InstanceID);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000038A8 File Offset: 0x00001AA8
		[FreeFunction("OffMeshLinkScriptBindings::GetLinkOwnerInternal")]
		private static Object GetLinkOwnerInternal(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(OffMeshLinkData.GetLinkOwnerInternal_Injected(instanceID));
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000038C0 File Offset: 0x00001AC0
		[Obsolete("offMeshLink has been deprecated. Use 'owner' instead.")]
		public OffMeshLink offMeshLink
		{
			get
			{
				return OffMeshLinkData.GetOffMeshLinkInternal(this.m_InstanceID);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000038D0 File Offset: 0x00001AD0
		[FreeFunction("OffMeshLinkScriptBindings::GetOffMeshLinkInternal")]
		private static OffMeshLink GetOffMeshLinkInternal(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<OffMeshLink>(OffMeshLinkData.GetOffMeshLinkInternal_Injected(instanceID));
		}

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetLinkOwnerInternal_Injected(int instanceID);

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetOffMeshLinkInternal_Injected(int instanceID);

		// Token: 0x04000022 RID: 34
		internal int m_Valid;

		// Token: 0x04000023 RID: 35
		internal int m_Activated;

		// Token: 0x04000024 RID: 36
		internal int m_InstanceID;

		// Token: 0x04000025 RID: 37
		internal OffMeshLinkType m_LinkType;

		// Token: 0x04000026 RID: 38
		internal Vector3 m_StartPos;

		// Token: 0x04000027 RID: 39
		internal Vector3 m_EndPos;
	}
}
