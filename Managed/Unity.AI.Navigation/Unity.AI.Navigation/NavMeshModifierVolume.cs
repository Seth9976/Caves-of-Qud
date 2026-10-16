using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.AI.Navigation
{
	// Token: 0x02000006 RID: 6
	[ExecuteAlways]
	[AddComponentMenu("Navigation/NavMesh Modifier Volume", 31)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/NavMeshModifierVolume.html")]
	public class NavMeshModifierVolume : MonoBehaviour
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000029BF File Offset: 0x00000BBF
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000029C7 File Offset: 0x00000BC7
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000029D0 File Offset: 0x00000BD0
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000029D8 File Offset: 0x00000BD8
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000029E1 File Offset: 0x00000BE1
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000029E9 File Offset: 0x00000BE9
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000029F2 File Offset: 0x00000BF2
		public static List<NavMeshModifierVolume> activeModifiers
		{
			get
			{
				return NavMeshModifierVolume.s_NavMeshModifiers;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029F9 File Offset: 0x00000BF9
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void ClearNavMeshModifiers()
		{
			NavMeshModifierVolume.s_NavMeshModifiers.Clear();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A05 File Offset: 0x00000C05
		private void OnEnable()
		{
			if (!NavMeshModifierVolume.s_NavMeshModifiers.Contains(this))
			{
				NavMeshModifierVolume.s_NavMeshModifiers.Add(this);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A1F File Offset: 0x00000C1F
		private void OnDisable()
		{
			NavMeshModifierVolume.s_NavMeshModifiers.Remove(this);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A2D File Offset: 0x00000C2D
		public bool AffectsAgentType(int agentTypeID)
		{
			return this.m_AffectedAgents.Count != 0 && (this.m_AffectedAgents[0] == -1 || this.m_AffectedAgents.IndexOf(agentTypeID) != -1);
		}

		// Token: 0x04000025 RID: 37
		[SerializeField]
		[HideInInspector]
		private byte m_SerializedVersion;

		// Token: 0x04000026 RID: 38
		[SerializeField]
		private Vector3 m_Size = new Vector3(4f, 3f, 4f);

		// Token: 0x04000027 RID: 39
		[SerializeField]
		private Vector3 m_Center = new Vector3(0f, 1f, 0f);

		// Token: 0x04000028 RID: 40
		[SerializeField]
		private int m_Area;

		// Token: 0x04000029 RID: 41
		[SerializeField]
		private List<int> m_AffectedAgents = new List<int>(new int[] { -1 });

		// Token: 0x0400002A RID: 42
		private static readonly List<NavMeshModifierVolume> s_NavMeshModifiers = new List<NavMeshModifierVolume>();
	}
}
