using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Unity.AI.Navigation
{
	// Token: 0x02000008 RID: 8
	[ExecuteAlways]
	[DefaultExecutionOrder(-102)]
	[AddComponentMenu("Navigation/NavMesh Surface", 30)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/NavMeshSurface.html")]
	public class NavMeshSurface : MonoBehaviour
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002ACC File Offset: 0x00000CCC
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002AD4 File Offset: 0x00000CD4
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002ADD File Offset: 0x00000CDD
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002AE5 File Offset: 0x00000CE5
		public CollectObjects collectObjects
		{
			get
			{
				return this.m_CollectObjects;
			}
			set
			{
				this.m_CollectObjects = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002AEE File Offset: 0x00000CEE
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002AF6 File Offset: 0x00000CF6
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002AFF File Offset: 0x00000CFF
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002B07 File Offset: 0x00000D07
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002B10 File Offset: 0x00000D10
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002B18 File Offset: 0x00000D18
		public LayerMask layerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002B21 File Offset: 0x00000D21
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002B29 File Offset: 0x00000D29
		public NavMeshCollectGeometry useGeometry
		{
			get
			{
				return this.m_UseGeometry;
			}
			set
			{
				this.m_UseGeometry = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002B32 File Offset: 0x00000D32
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002B3A File Offset: 0x00000D3A
		public int defaultArea
		{
			get
			{
				return this.m_DefaultArea;
			}
			set
			{
				this.m_DefaultArea = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002B43 File Offset: 0x00000D43
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002B4B File Offset: 0x00000D4B
		public bool ignoreNavMeshAgent
		{
			get
			{
				return this.m_IgnoreNavMeshAgent;
			}
			set
			{
				this.m_IgnoreNavMeshAgent = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002B54 File Offset: 0x00000D54
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002B5C File Offset: 0x00000D5C
		public bool ignoreNavMeshObstacle
		{
			get
			{
				return this.m_IgnoreNavMeshObstacle;
			}
			set
			{
				this.m_IgnoreNavMeshObstacle = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002B65 File Offset: 0x00000D65
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002B6D File Offset: 0x00000D6D
		public bool overrideTileSize
		{
			get
			{
				return this.m_OverrideTileSize;
			}
			set
			{
				this.m_OverrideTileSize = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002B76 File Offset: 0x00000D76
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002B7E File Offset: 0x00000D7E
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002B87 File Offset: 0x00000D87
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002B8F File Offset: 0x00000D8F
		public bool overrideVoxelSize
		{
			get
			{
				return this.m_OverrideVoxelSize;
			}
			set
			{
				this.m_OverrideVoxelSize = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002B98 File Offset: 0x00000D98
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002BA0 File Offset: 0x00000DA0
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002BA9 File Offset: 0x00000DA9
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002BB1 File Offset: 0x00000DB1
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002BBA File Offset: 0x00000DBA
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002BC2 File Offset: 0x00000DC2
		public bool buildHeightMesh
		{
			get
			{
				return this.m_BuildHeightMesh;
			}
			set
			{
				this.m_BuildHeightMesh = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002BCB File Offset: 0x00000DCB
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002BD3 File Offset: 0x00000DD3
		public NavMeshData navMeshData
		{
			get
			{
				return this.m_NavMeshData;
			}
			set
			{
				this.m_NavMeshData = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002BDC File Offset: 0x00000DDC
		internal NavMeshDataInstance navMeshDataInstance
		{
			get
			{
				return this.m_NavMeshDataInstance;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public static List<NavMeshSurface> activeSurfaces
		{
			get
			{
				return NavMeshSurface.s_NavMeshSurfaces;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002BEC File Offset: 0x00000DEC
		private Bounds GetInflatedBounds()
		{
			NavMeshBuildSettings settingsByID = NavMesh.GetSettingsByID(this.m_AgentTypeID);
			float num = ((settingsByID.agentTypeID != -1) ? settingsByID.agentRadius : 0f);
			Bounds bounds = new Bounds(this.center, this.size);
			bounds.Expand(new Vector3(num, 0f, num));
			return bounds;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002C45 File Offset: 0x00000E45
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void ClearNavMeshSurfaces()
		{
			NavMeshSurface.s_NavMeshSurfaces.Clear();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002C51 File Offset: 0x00000E51
		private void OnEnable()
		{
			NavMeshSurface.Register(this);
			this.AddData();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002C5F File Offset: 0x00000E5F
		private void OnDisable()
		{
			this.RemoveData();
			NavMeshSurface.Unregister(this);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002C70 File Offset: 0x00000E70
		public void AddData()
		{
			if (this.m_NavMeshDataInstance.valid)
			{
				return;
			}
			if (this.m_NavMeshData != null)
			{
				this.m_NavMeshDataInstance = NavMesh.AddNavMeshData(this.m_NavMeshData, base.transform.position, base.transform.rotation);
				this.m_NavMeshDataInstance.owner = this;
			}
			this.m_LastPosition = base.transform.position;
			this.m_LastRotation = base.transform.rotation;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002CEE File Offset: 0x00000EEE
		public void RemoveData()
		{
			this.m_NavMeshDataInstance.Remove();
			this.m_NavMeshDataInstance = default(NavMeshDataInstance);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002D08 File Offset: 0x00000F08
		public NavMeshBuildSettings GetBuildSettings()
		{
			NavMeshBuildSettings settingsByID = NavMesh.GetSettingsByID(this.m_AgentTypeID);
			if (settingsByID.agentTypeID == -1)
			{
				Debug.LogWarning("No build settings for agent type ID " + this.agentTypeID.ToString(), this);
				settingsByID.agentTypeID = this.m_AgentTypeID;
			}
			if (this.overrideTileSize)
			{
				settingsByID.overrideTileSize = true;
				settingsByID.tileSize = this.tileSize;
			}
			if (this.overrideVoxelSize)
			{
				settingsByID.overrideVoxelSize = true;
				settingsByID.voxelSize = this.voxelSize;
			}
			settingsByID.minRegionArea = this.minRegionArea;
			settingsByID.buildHeightMesh = this.buildHeightMesh;
			return settingsByID;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002DAC File Offset: 0x00000FAC
		public void BuildNavMesh()
		{
			List<NavMeshBuildSource> list = this.CollectSources();
			Bounds bounds = new Bounds(this.m_Center, NavMeshSurface.Abs(this.m_Size));
			if (this.m_CollectObjects != CollectObjects.Volume)
			{
				bounds = this.CalculateWorldBounds(list);
			}
			NavMeshData navMeshData = NavMeshBuilder.BuildNavMeshData(this.GetBuildSettings(), list, bounds, base.transform.position, base.transform.rotation);
			if (navMeshData != null)
			{
				navMeshData.name = base.gameObject.name;
				this.RemoveData();
				this.m_NavMeshData = navMeshData;
				if (base.isActiveAndEnabled)
				{
					this.AddData();
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002E44 File Offset: 0x00001044
		public AsyncOperation UpdateNavMesh(NavMeshData data)
		{
			List<NavMeshBuildSource> list = this.CollectSources();
			Bounds bounds = new Bounds(this.m_Center, NavMeshSurface.Abs(this.m_Size));
			if (this.m_CollectObjects != CollectObjects.Volume)
			{
				bounds = this.CalculateWorldBounds(list);
			}
			return NavMeshBuilder.UpdateNavMeshDataAsync(data, this.GetBuildSettings(), list, bounds);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002E90 File Offset: 0x00001090
		private static void Register(NavMeshSurface surface)
		{
			if (NavMeshSurface.s_NavMeshSurfaces.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshSurface.UpdateActive));
			}
			if (!NavMeshSurface.s_NavMeshSurfaces.Contains(surface))
			{
				NavMeshSurface.s_NavMeshSurfaces.Add(surface);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002EE1 File Offset: 0x000010E1
		private static void Unregister(NavMeshSurface surface)
		{
			NavMeshSurface.s_NavMeshSurfaces.Remove(surface);
			if (NavMeshSurface.s_NavMeshSurfaces.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshSurface.UpdateActive));
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002F1C File Offset: 0x0000111C
		private static void UpdateActive()
		{
			for (int i = 0; i < NavMeshSurface.s_NavMeshSurfaces.Count; i++)
			{
				NavMeshSurface.s_NavMeshSurfaces[i].UpdateDataIfTransformChanged();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002F50 File Offset: 0x00001150
		private void AppendModifierVolumes(ref List<NavMeshBuildSource> sources)
		{
			List<NavMeshModifierVolume> list;
			if (this.m_CollectObjects == CollectObjects.Children)
			{
				list = new List<NavMeshModifierVolume>(base.GetComponentsInChildren<NavMeshModifierVolume>());
				list.RemoveAll((NavMeshModifierVolume x) => !x.isActiveAndEnabled);
			}
			else
			{
				list = NavMeshModifierVolume.activeModifiers;
			}
			foreach (NavMeshModifierVolume navMeshModifierVolume in list)
			{
				if ((this.m_LayerMask & (1 << navMeshModifierVolume.gameObject.layer)) != 0 && navMeshModifierVolume.AffectsAgentType(this.m_AgentTypeID))
				{
					Vector3 vector = navMeshModifierVolume.transform.TransformPoint(navMeshModifierVolume.center);
					Vector3 lossyScale = navMeshModifierVolume.transform.lossyScale;
					Vector3 vector2 = new Vector3(navMeshModifierVolume.size.x * Mathf.Abs(lossyScale.x), navMeshModifierVolume.size.y * Mathf.Abs(lossyScale.y), navMeshModifierVolume.size.z * Mathf.Abs(lossyScale.z));
					NavMeshBuildSource navMeshBuildSource = default(NavMeshBuildSource);
					navMeshBuildSource.shape = NavMeshBuildSourceShape.ModifierBox;
					navMeshBuildSource.transform = Matrix4x4.TRS(vector, navMeshModifierVolume.transform.rotation, Vector3.one);
					navMeshBuildSource.size = vector2;
					navMeshBuildSource.area = navMeshModifierVolume.area;
					sources.Add(navMeshBuildSource);
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030D8 File Offset: 0x000012D8
		private List<NavMeshBuildSource> CollectSources()
		{
			List<NavMeshBuildSource> list = new List<NavMeshBuildSource>();
			List<NavMeshBuildMarkup> list2 = new List<NavMeshBuildMarkup>();
			List<NavMeshModifier> list3;
			if (this.m_CollectObjects == CollectObjects.Children)
			{
				list3 = new List<NavMeshModifier>(base.GetComponentsInChildren<NavMeshModifier>());
				list3.RemoveAll((NavMeshModifier x) => !x.isActiveAndEnabled);
			}
			else
			{
				list3 = NavMeshModifier.activeModifiers;
			}
			foreach (NavMeshModifier navMeshModifier in list3)
			{
				if ((this.m_LayerMask & (1 << navMeshModifier.gameObject.layer)) != 0 && navMeshModifier.AffectsAgentType(this.m_AgentTypeID))
				{
					list2.Add(new NavMeshBuildMarkup
					{
						root = navMeshModifier.transform,
						overrideArea = navMeshModifier.overrideArea,
						area = navMeshModifier.area,
						ignoreFromBuild = navMeshModifier.ignoreFromBuild,
						applyToChildren = navMeshModifier.applyToChildren,
						overrideGenerateLinks = navMeshModifier.overrideGenerateLinks,
						generateLinks = navMeshModifier.generateLinks
					});
				}
			}
			switch (this.m_CollectObjects)
			{
			default:
				this.CollectSourcesInHierarchy(null, this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, this.m_GenerateLinks, list2, false, list);
				break;
			case CollectObjects.Volume:
			{
				Bounds worldBounds = NavMeshSurface.GetWorldBounds(Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one), this.GetInflatedBounds());
				this.CollectSourcesInVolume(worldBounds, this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, this.m_GenerateLinks, list2, false, list);
				break;
			}
			case CollectObjects.Children:
				this.CollectSourcesInHierarchy(base.transform, this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, this.m_GenerateLinks, list2, false, list);
				break;
			case CollectObjects.MarkedWithModifier:
				this.CollectSourcesInHierarchy(null, this.m_LayerMask, this.m_UseGeometry, this.m_DefaultArea, this.m_GenerateLinks, list2, true, list);
				break;
			}
			if (this.m_IgnoreNavMeshAgent)
			{
				list.RemoveAll((NavMeshBuildSource x) => x.component != null && x.component.gameObject.GetComponent<NavMeshAgent>() != null);
			}
			if (this.m_IgnoreNavMeshObstacle)
			{
				list.RemoveAll((NavMeshBuildSource x) => x.component != null && x.component.gameObject.GetComponent<NavMeshObstacle>() != null);
			}
			this.AppendModifierVolumes(ref list);
			return list;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003374 File Offset: 0x00001574
		private static Vector3 Abs(Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000339C File Offset: 0x0000159C
		private static Bounds GetWorldBounds(Matrix4x4 mat, Bounds bounds)
		{
			Vector3 vector = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.right));
			Vector3 vector2 = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.up));
			Vector3 vector3 = NavMeshSurface.Abs(mat.MultiplyVector(Vector3.forward));
			Vector3 vector4 = mat.MultiplyPoint(bounds.center);
			Vector3 vector5 = vector * bounds.size.x + vector2 * bounds.size.y + vector3 * bounds.size.z;
			return new Bounds(vector4, vector5);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003434 File Offset: 0x00001634
		private Bounds CalculateWorldBounds(List<NavMeshBuildSource> sources)
		{
			Matrix4x4 inverse = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one).inverse;
			Bounds bounds = default(Bounds);
			foreach (NavMeshBuildSource navMeshBuildSource in sources)
			{
				switch (navMeshBuildSource.shape)
				{
				case NavMeshBuildSourceShape.Mesh:
				{
					Mesh mesh = navMeshBuildSource.sourceObject as Mesh;
					bounds.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, mesh.bounds));
					break;
				}
				case NavMeshBuildSourceShape.Terrain:
				{
					TerrainData terrainData = navMeshBuildSource.sourceObject as TerrainData;
					bounds.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, new Bounds(0.5f * terrainData.size, terrainData.size)));
					break;
				}
				case NavMeshBuildSourceShape.Box:
				case NavMeshBuildSourceShape.Sphere:
				case NavMeshBuildSourceShape.Capsule:
				case NavMeshBuildSourceShape.ModifierBox:
					bounds.Encapsulate(NavMeshSurface.GetWorldBounds(inverse * navMeshBuildSource.transform, new Bounds(Vector3.zero, navMeshBuildSource.size)));
					break;
				}
			}
			bounds.Expand(0.1f);
			return bounds;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000358C File Offset: 0x0000178C
		private bool HasTransformChanged()
		{
			return this.m_LastPosition != base.transform.position || this.m_LastRotation != base.transform.rotation;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000035C3 File Offset: 0x000017C3
		private void UpdateDataIfTransformChanged()
		{
			if (this.HasTransformChanged())
			{
				this.RemoveData();
				this.AddData();
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000035D9 File Offset: 0x000017D9
		private void CollectSourcesInVolume(Bounds includedWorldBounds, int includedLayerMask, NavMeshCollectGeometry geometry, int areaByDefault, bool generateLinksByDefault, List<NavMeshBuildMarkup> markups, bool includeOnlyMarkedObjects, List<NavMeshBuildSource> results)
		{
			NavMeshBuilder.CollectSources(includedWorldBounds, includedLayerMask, geometry, areaByDefault, generateLinksByDefault, markups, includeOnlyMarkedObjects, results);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000035ED File Offset: 0x000017ED
		private void CollectSourcesInHierarchy(Transform root, int includedLayerMask, NavMeshCollectGeometry geometry, int areaByDefault, bool generateLinksByDefault, List<NavMeshBuildMarkup> markups, bool includeOnlyMarkedObjects, List<NavMeshBuildSource> results)
		{
			NavMeshBuilder.CollectSources(root, includedLayerMask, geometry, areaByDefault, generateLinksByDefault, markups, includeOnlyMarkedObjects, results);
		}

		// Token: 0x04000030 RID: 48
		[SerializeField]
		[HideInInspector]
		private byte m_SerializedVersion;

		// Token: 0x04000031 RID: 49
		[SerializeField]
		private int m_AgentTypeID;

		// Token: 0x04000032 RID: 50
		[SerializeField]
		private CollectObjects m_CollectObjects;

		// Token: 0x04000033 RID: 51
		[SerializeField]
		private Vector3 m_Size = new Vector3(10f, 10f, 10f);

		// Token: 0x04000034 RID: 52
		[SerializeField]
		private Vector3 m_Center = new Vector3(0f, 2f, 0f);

		// Token: 0x04000035 RID: 53
		[SerializeField]
		private LayerMask m_LayerMask = -1;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		private NavMeshCollectGeometry m_UseGeometry;

		// Token: 0x04000037 RID: 55
		[SerializeField]
		private int m_DefaultArea;

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private bool m_GenerateLinks;

		// Token: 0x04000039 RID: 57
		[SerializeField]
		private bool m_IgnoreNavMeshAgent = true;

		// Token: 0x0400003A RID: 58
		[SerializeField]
		private bool m_IgnoreNavMeshObstacle = true;

		// Token: 0x0400003B RID: 59
		[SerializeField]
		private bool m_OverrideTileSize;

		// Token: 0x0400003C RID: 60
		[SerializeField]
		private int m_TileSize = 256;

		// Token: 0x0400003D RID: 61
		[SerializeField]
		private bool m_OverrideVoxelSize;

		// Token: 0x0400003E RID: 62
		[SerializeField]
		private float m_VoxelSize;

		// Token: 0x0400003F RID: 63
		[SerializeField]
		private float m_MinRegionArea = 2f;

		// Token: 0x04000040 RID: 64
		[FormerlySerializedAs("m_BakedNavMeshData")]
		[SerializeField]
		private NavMeshData m_NavMeshData;

		// Token: 0x04000041 RID: 65
		[SerializeField]
		private bool m_BuildHeightMesh;

		// Token: 0x04000042 RID: 66
		private NavMeshDataInstance m_NavMeshDataInstance;

		// Token: 0x04000043 RID: 67
		private Vector3 m_LastPosition = Vector3.zero;

		// Token: 0x04000044 RID: 68
		private Quaternion m_LastRotation = Quaternion.identity;

		// Token: 0x04000045 RID: 69
		private static readonly List<NavMeshSurface> s_NavMeshSurfaces = new List<NavMeshSurface>();
	}
}
