using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.AI.Navigation
{
	// Token: 0x02000005 RID: 5
	[ExecuteAlways]
	[DefaultExecutionOrder(-103)]
	[AddComponentMenu("Navigation/NavMesh Modifier", 32)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/NavMeshModifier.html")]
	public class NavMeshModifier : MonoBehaviour
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002872 File Offset: 0x00000A72
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000287A File Offset: 0x00000A7A
		public bool overrideArea
		{
			get
			{
				return this.m_OverrideArea;
			}
			set
			{
				this.m_OverrideArea = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002883 File Offset: 0x00000A83
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000288B File Offset: 0x00000A8B
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002894 File Offset: 0x00000A94
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000289C File Offset: 0x00000A9C
		public bool overrideGenerateLinks
		{
			get
			{
				return this.m_OverrideGenerateLinks;
			}
			set
			{
				this.m_OverrideGenerateLinks = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000028A5 File Offset: 0x00000AA5
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000028AD File Offset: 0x00000AAD
		public bool generateLinks
		{
			get
			{
				return this.m_GenerateLinks;
			}
			set
			{
				this.m_GenerateLinks = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000028B6 File Offset: 0x00000AB6
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000028BE File Offset: 0x00000ABE
		public bool ignoreFromBuild
		{
			get
			{
				return this.m_IgnoreFromBuild;
			}
			set
			{
				this.m_IgnoreFromBuild = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000028C7 File Offset: 0x00000AC7
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000028CF File Offset: 0x00000ACF
		public bool applyToChildren
		{
			get
			{
				return this.m_ApplyToChildren;
			}
			set
			{
				this.m_ApplyToChildren = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000028D8 File Offset: 0x00000AD8
		public static List<NavMeshModifier> activeModifiers
		{
			get
			{
				if (NavMeshModifier.s_RebuildNavMeshModifiers)
				{
					NavMeshModifier.s_NavMeshModifiers = NavMeshModifier.s_NavMeshModifiersSet.ToList<NavMeshModifier>();
					NavMeshModifier.s_RebuildNavMeshModifiers = false;
				}
				return NavMeshModifier.s_NavMeshModifiers;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028FB File Offset: 0x00000AFB
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void ClearNavMeshModifiers()
		{
			NavMeshModifier.s_NavMeshModifiers.Clear();
			NavMeshModifier.s_NavMeshModifiersSet.Clear();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002911 File Offset: 0x00000B11
		private void OnEnable()
		{
			this.RegisterModifier();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002919 File Offset: 0x00000B19
		private void OnDisable()
		{
			this.UnregisterModifier();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002921 File Offset: 0x00000B21
		private void RegisterModifier()
		{
			if (NavMeshModifier.s_NavMeshModifiersSet.Add(this))
			{
				NavMeshModifier.s_RebuildNavMeshModifiers = true;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002936 File Offset: 0x00000B36
		private void UnregisterModifier()
		{
			if (NavMeshModifier.s_NavMeshModifiersSet.Remove(this))
			{
				NavMeshModifier.s_RebuildNavMeshModifiers = true;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000294B File Offset: 0x00000B4B
		public bool AffectsAgentType(int agentTypeID)
		{
			return this.m_AffectedAgents.Count != 0 && (this.m_AffectedAgents[0] == -1 || this.m_AffectedAgents.IndexOf(agentTypeID) != -1);
		}

		// Token: 0x0400001A RID: 26
		[SerializeField]
		[HideInInspector]
		private byte m_SerializedVersion;

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private bool m_OverrideArea;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private int m_Area;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		private bool m_OverrideGenerateLinks;

		// Token: 0x0400001E RID: 30
		[SerializeField]
		private bool m_GenerateLinks;

		// Token: 0x0400001F RID: 31
		[SerializeField]
		private bool m_IgnoreFromBuild;

		// Token: 0x04000020 RID: 32
		[SerializeField]
		private bool m_ApplyToChildren = true;

		// Token: 0x04000021 RID: 33
		[SerializeField]
		private List<int> m_AffectedAgents = new List<int>(new int[] { -1 });

		// Token: 0x04000022 RID: 34
		private static bool s_RebuildNavMeshModifiers = true;

		// Token: 0x04000023 RID: 35
		private static List<NavMeshModifier> s_NavMeshModifiers = new List<NavMeshModifier>();

		// Token: 0x04000024 RID: 36
		private static readonly HashSet<NavMeshModifier> s_NavMeshModifiersSet = new HashSet<NavMeshModifier>();
	}
}
