using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation
{
	// Token: 0x02000004 RID: 4
	[ExecuteAlways]
	[DefaultExecutionOrder(-101)]
	[AddComponentMenu("Navigation/NavMesh Link", 33)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/NavMeshLink.html")]
	public class NavMeshLink : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				if (value == this.m_AgentTypeID)
				{
					return;
				}
				this.m_AgentTypeID = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020DF File Offset: 0x000002DF
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020E7 File Offset: 0x000002E7
		public Vector3 startPoint
		{
			get
			{
				return this.m_StartPoint;
			}
			set
			{
				if (value == this.m_StartPoint)
				{
					return;
				}
				this.m_StartPoint = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002105 File Offset: 0x00000305
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000210D File Offset: 0x0000030D
		public Vector3 endPoint
		{
			get
			{
				return this.m_EndPoint;
			}
			set
			{
				if (value == this.m_EndPoint)
				{
					return;
				}
				this.m_EndPoint = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212B File Offset: 0x0000032B
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002133 File Offset: 0x00000333
		public Transform startTransform
		{
			get
			{
				return this.m_StartTransform;
			}
			set
			{
				if (value == this.m_StartTransform)
				{
					return;
				}
				this.m_StartTransform = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002151 File Offset: 0x00000351
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002159 File Offset: 0x00000359
		public Transform endTransform
		{
			get
			{
				return this.m_EndTransform;
			}
			set
			{
				if (value == this.m_EndTransform)
				{
					return;
				}
				this.m_EndTransform = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002177 File Offset: 0x00000377
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000217F File Offset: 0x0000037F
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				if (value.Equals(this.m_Width))
				{
					return;
				}
				this.m_Width = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000219E File Offset: 0x0000039E
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021B8 File Offset: 0x000003B8
		public float costModifier
		{
			get
			{
				if (!this.m_IsOverridingCost)
				{
					return -this.m_CostModifier;
				}
				return this.m_CostModifier;
			}
			set
			{
				bool flag = value >= 0f;
				if (value.Equals(this.costModifier) && flag == this.m_IsOverridingCost)
				{
					return;
				}
				this.m_IsOverridingCost = flag;
				this.m_CostModifier = Mathf.Abs(value);
				this.UpdateLink();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002203 File Offset: 0x00000403
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000220B File Offset: 0x0000040B
		public bool bidirectional
		{
			get
			{
				return this.m_Bidirectional;
			}
			set
			{
				if (value == this.m_Bidirectional)
				{
					return;
				}
				this.m_Bidirectional = value;
				this.UpdateLink();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002224 File Offset: 0x00000424
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000222C File Offset: 0x0000042C
		public bool autoUpdate
		{
			get
			{
				return this.m_AutoUpdatePosition;
			}
			set
			{
				if (value == this.m_AutoUpdatePosition)
				{
					return;
				}
				this.m_AutoUpdatePosition = value;
				if (this.m_AutoUpdatePosition)
				{
					NavMeshLink.AddTracking(this);
					return;
				}
				NavMeshLink.RemoveTracking(this);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002254 File Offset: 0x00000454
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000225C File Offset: 0x0000045C
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				if (value == this.m_Area)
				{
					return;
				}
				this.m_Area = value;
				this.UpdateLink();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002275 File Offset: 0x00000475
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000227D File Offset: 0x0000047D
		public bool activated
		{
			get
			{
				return this.m_Activated;
			}
			set
			{
				this.m_Activated = value;
				NavMesh.SetLinkActive(this.m_LinkInstance, this.m_Activated);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002297 File Offset: 0x00000497
		public bool occupied
		{
			get
			{
				return NavMesh.IsLinkOccupied(this.m_LinkInstance);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022A4 File Offset: 0x000004A4
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void ClearTrackedList()
		{
			NavMeshLink.s_Tracked.Clear();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022B0 File Offset: 0x000004B0
		private void UpgradeSerializedVersion()
		{
			if (this.m_SerializedVersion < 1)
			{
				this.m_SerializedVersion = 1;
				this.m_IsOverridingCost = this.m_CostModifier >= 0f;
				this.m_CostModifier = Mathf.Abs(this.m_CostModifier);
				if (this.m_StartTransform == base.gameObject.transform)
				{
					this.m_StartTransform = null;
				}
				if (this.m_EndTransform == base.gameObject.transform)
				{
					this.m_EndTransform = null;
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002332 File Offset: 0x00000532
		private void Awake()
		{
			this.UpgradeSerializedVersion();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000233A File Offset: 0x0000053A
		private void OnEnable()
		{
			this.AddLink();
			if (this.m_AutoUpdatePosition && NavMesh.IsLinkValid(this.m_LinkInstance))
			{
				NavMeshLink.AddTracking(this);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000235D File Offset: 0x0000055D
		private void OnDisable()
		{
			NavMeshLink.RemoveTracking(this);
			NavMesh.RemoveLink(this.m_LinkInstance);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002370 File Offset: 0x00000570
		public void UpdateLink()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			NavMesh.RemoveLink(this.m_LinkInstance);
			this.AddLink();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000238C File Offset: 0x0000058C
		private static void AddTracking(NavMeshLink link)
		{
			if (NavMeshLink.s_Tracked.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Combine(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshLink.UpdateTrackedInstances));
			}
			NavMeshLink.s_Tracked.Add(link);
			link.RecordEndpointTransforms();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023CB File Offset: 0x000005CB
		private static void RemoveTracking(NavMeshLink link)
		{
			NavMeshLink.s_Tracked.Remove(link);
			if (NavMeshLink.s_Tracked.Count == 0)
			{
				NavMesh.onPreUpdate = (NavMesh.OnNavMeshPreUpdate)Delegate.Remove(NavMesh.onPreUpdate, new NavMesh.OnNavMeshPreUpdate(NavMeshLink.UpdateTrackedInstances));
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002408 File Offset: 0x00000608
		internal void GetWorldPositions(out Vector3 worldStartPosition, out Vector3 worldEndPosition)
		{
			bool flag = this.m_StartTransform == null;
			bool flag2 = this.m_EndTransform == null;
			Matrix4x4 matrix4x = ((flag || flag2) ? this.LocalToWorldUnscaled() : Matrix4x4.identity);
			worldStartPosition = (flag ? matrix4x.MultiplyPoint3x4(this.m_StartPoint) : this.m_StartTransform.position);
			worldEndPosition = (flag2 ? matrix4x.MultiplyPoint3x4(this.m_EndPoint) : this.m_EndTransform.position);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002488 File Offset: 0x00000688
		internal void GetLocalPositions(out Vector3 localStartPosition, out Vector3 localEndPosition)
		{
			bool flag = this.m_StartTransform == null;
			bool flag2 = this.m_EndTransform == null;
			Matrix4x4 matrix4x = ((flag && flag2) ? Matrix4x4.identity : this.LocalToWorldUnscaled().inverse);
			localStartPosition = (flag ? this.m_StartPoint : matrix4x.MultiplyPoint3x4(this.m_StartTransform.position));
			localEndPosition = (flag2 ? this.m_EndPoint : matrix4x.MultiplyPoint3x4(this.m_EndTransform.position));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002510 File Offset: 0x00000710
		private void AddLink()
		{
			Vector3 vector;
			Vector3 vector2;
			this.GetLocalPositions(out vector, out vector2);
			NavMeshLinkData navMeshLinkData = new NavMeshLinkData
			{
				startPosition = vector,
				endPosition = vector2,
				width = this.m_Width,
				costModifier = this.costModifier,
				bidirectional = this.m_Bidirectional,
				area = this.m_Area,
				agentTypeID = this.m_AgentTypeID
			};
			this.m_LinkInstance = NavMesh.AddLink(navMeshLinkData, base.transform.position, base.transform.rotation);
			if (NavMesh.IsLinkValid(this.m_LinkInstance))
			{
				NavMesh.SetLinkOwner(this.m_LinkInstance, this);
				NavMesh.SetLinkActive(this.m_LinkInstance, this.m_Activated);
			}
			this.m_LastPosition = base.transform.position;
			this.m_LastRotation = base.transform.rotation;
			this.RecordEndpointTransforms();
			this.GetWorldPositions(out this.m_LastStartWorldPosition, out this.m_LastEndWorldPosition);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002608 File Offset: 0x00000808
		internal void RecordEndpointTransforms()
		{
			this.m_StartTransformWasEmpty = this.m_StartTransform == null;
			this.m_EndTransformWasEmpty = this.m_EndTransform == null;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002630 File Offset: 0x00000830
		internal bool HaveTransformsChanged()
		{
			bool flag = this.m_StartTransform == null;
			bool flag2 = this.m_EndTransform == null;
			if (flag && flag2 && this.m_StartTransformWasEmpty && this.m_EndTransformWasEmpty && base.transform.position == this.m_LastPosition && base.transform.rotation == this.m_LastRotation)
			{
				return false;
			}
			Matrix4x4 matrix4x = ((flag || flag2) ? this.LocalToWorldUnscaled() : Matrix4x4.identity);
			return (flag ? matrix4x.MultiplyPoint3x4(this.m_StartPoint) : this.m_StartTransform.position) != this.m_LastStartWorldPosition || (flag2 ? matrix4x.MultiplyPoint3x4(this.m_EndPoint) : this.m_EndTransform.position) != this.m_LastEndWorldPosition;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002705 File Offset: 0x00000905
		internal Matrix4x4 LocalToWorldUnscaled()
		{
			return Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002727 File Offset: 0x00000927
		private void OnDidApplyAnimationProperties()
		{
			this.UpdateLink();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002730 File Offset: 0x00000930
		private static void UpdateTrackedInstances()
		{
			foreach (NavMeshLink navMeshLink in NavMeshLink.s_Tracked)
			{
				if (navMeshLink.HaveTransformsChanged())
				{
					navMeshLink.UpdateLink();
				}
				navMeshLink.RecordEndpointTransforms();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002790 File Offset: 0x00000990
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002798 File Offset: 0x00000998
		[Obsolete("autoUpdatePositions has been deprecated. Use autoUpdate instead. (UnityUpgradable) -> autoUpdate")]
		public bool autoUpdatePositions
		{
			get
			{
				return this.autoUpdate;
			}
			set
			{
				this.autoUpdate = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000027A1 File Offset: 0x000009A1
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000027A9 File Offset: 0x000009A9
		[Obsolete("biDirectional has been deprecated. Use bidirectional instead. (UnityUpgradable) -> bidirectional")]
		public bool biDirectional
		{
			get
			{
				return this.bidirectional;
			}
			set
			{
				this.bidirectional = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000027B2 File Offset: 0x000009B2
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000027BA File Offset: 0x000009BA
		[Obsolete("costOverride has been deprecated. Use costModifier instead. (UnityUpgradable) -> costModifier")]
		public float costOverride
		{
			get
			{
				return this.costModifier;
			}
			set
			{
				this.costModifier = value;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027C3 File Offset: 0x000009C3
		[Obsolete("UpdatePositions() has been deprecated. Use UpdateLink() instead. (UnityUpgradable) -> UpdateLink(*)")]
		public void UpdatePositions()
		{
			this.UpdateLink();
		}

		// Token: 0x04000005 RID: 5
		[SerializeField]
		[HideInInspector]
		private byte m_SerializedVersion;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		private int m_AgentTypeID;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private Vector3 m_StartPoint = new Vector3(0f, 0f, -2.5f);

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private Vector3 m_EndPoint = new Vector3(0f, 0f, 2.5f);

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private Transform m_StartTransform;

		// Token: 0x0400000A RID: 10
		[SerializeField]
		private Transform m_EndTransform;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		private bool m_Activated = true;

		// Token: 0x0400000C RID: 12
		[SerializeField]
		private float m_Width;

		// Token: 0x0400000D RID: 13
		[SerializeField]
		[Min(0f)]
		private float m_CostModifier = -1f;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		private bool m_IsOverridingCost;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		private bool m_Bidirectional = true;

		// Token: 0x04000010 RID: 16
		[SerializeField]
		private bool m_AutoUpdatePosition;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		private int m_Area;

		// Token: 0x04000012 RID: 18
		private NavMeshLinkInstance m_LinkInstance;

		// Token: 0x04000013 RID: 19
		private bool m_StartTransformWasEmpty = true;

		// Token: 0x04000014 RID: 20
		private bool m_EndTransformWasEmpty = true;

		// Token: 0x04000015 RID: 21
		private Vector3 m_LastStartWorldPosition = Vector3.positiveInfinity;

		// Token: 0x04000016 RID: 22
		private Vector3 m_LastEndWorldPosition = Vector3.positiveInfinity;

		// Token: 0x04000017 RID: 23
		private Vector3 m_LastPosition = Vector3.positiveInfinity;

		// Token: 0x04000018 RID: 24
		private Quaternion m_LastRotation = Quaternion.identity;

		// Token: 0x04000019 RID: 25
		private static readonly List<NavMeshLink> s_Tracked = new List<NavMeshLink>();
	}
}
