using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000091 RID: 145
	[AddComponentMenu("Event/Tracked Device Raycaster")]
	[RequireComponent(typeof(Canvas))]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/TrackedInputDevices.html#tracked-device-raycaster")]
	public class TrackedDeviceRaycaster : BaseRaycaster
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0003EAB4 File Offset: 0x0003CCB4
		public override Camera eventCamera
		{
			get
			{
				Canvas canvas = this.canvas;
				if (!(canvas != null))
				{
					return null;
				}
				return canvas.worldCamera;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0003EAD9 File Offset: 0x0003CCD9
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x0003EAE1 File Offset: 0x0003CCE1
		public LayerMask blockingMask
		{
			get
			{
				return this.m_BlockingMask;
			}
			set
			{
				this.m_BlockingMask = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0003EAEA File Offset: 0x0003CCEA
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0003EAF2 File Offset: 0x0003CCF2
		public bool checkFor3DOcclusion
		{
			get
			{
				return this.m_CheckFor3DOcclusion;
			}
			set
			{
				this.m_CheckFor3DOcclusion = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0003EAFB File Offset: 0x0003CCFB
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0003EB03 File Offset: 0x0003CD03
		public bool checkFor2DOcclusion
		{
			get
			{
				return this.m_CheckFor2DOcclusion;
			}
			set
			{
				this.m_CheckFor2DOcclusion = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0003EB0C File Offset: 0x0003CD0C
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0003EB14 File Offset: 0x0003CD14
		public bool ignoreReversedGraphics
		{
			get
			{
				return this.m_IgnoreReversedGraphics;
			}
			set
			{
				this.m_IgnoreReversedGraphics = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0003EB1D File Offset: 0x0003CD1D
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0003EB25 File Offset: 0x0003CD25
		public float maxDistance
		{
			get
			{
				return this.m_MaxDistance;
			}
			set
			{
				this.m_MaxDistance = value;
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0003EB2E File Offset: 0x0003CD2E
		protected override void OnEnable()
		{
			base.OnEnable();
			TrackedDeviceRaycaster.s_Instances.AppendWithCapacity(this, 10);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0003EB44 File Offset: 0x0003CD44
		protected override void OnDisable()
		{
			int num = TrackedDeviceRaycaster.s_Instances.IndexOfReference(this);
			if (num != -1)
			{
				TrackedDeviceRaycaster.s_Instances.RemoveAtByMovingTailWithCapacity(num);
			}
			base.OnDisable();
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0003EB74 File Offset: 0x0003CD74
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			ExtendedPointerEventData extendedPointerEventData = eventData as ExtendedPointerEventData;
			if (extendedPointerEventData != null && extendedPointerEventData.pointerType == UIPointerType.Tracked)
			{
				this.PerformRaycast(extendedPointerEventData, resultAppendList);
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0003EB9C File Offset: 0x0003CD9C
		internal void PerformRaycast(ExtendedPointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			if (this.eventCamera == null)
			{
				return;
			}
			Ray ray = new Ray(eventData.trackedDevicePosition, eventData.trackedDeviceOrientation * Vector3.forward);
			float num = this.m_MaxDistance;
			RaycastHit raycastHit;
			if (this.m_CheckFor3DOcclusion && Physics.Raycast(ray, out raycastHit, num, this.m_BlockingMask))
			{
				num = raycastHit.distance;
			}
			if (this.m_CheckFor2DOcclusion)
			{
				float num2 = num;
				RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(ray, num2, this.m_BlockingMask);
				if (rayIntersection.collider != null)
				{
					num = rayIntersection.distance;
				}
			}
			this.m_RaycastResultsCache.Clear();
			this.SortedRaycastGraphics(this.canvas, ray, this.m_RaycastResultsCache);
			for (int i = 0; i < this.m_RaycastResultsCache.Count; i++)
			{
				bool flag = true;
				TrackedDeviceRaycaster.RaycastHitData raycastHitData = this.m_RaycastResultsCache[i];
				GameObject gameObject = raycastHitData.graphic.gameObject;
				if (this.m_IgnoreReversedGraphics)
				{
					Vector3 direction = ray.direction;
					Vector3 vector = gameObject.transform.rotation * Vector3.forward;
					flag = Vector3.Dot(direction, vector) > 0f;
				}
				flag &= raycastHitData.distance < num;
				if (flag)
				{
					RaycastResult raycastResult = new RaycastResult
					{
						gameObject = gameObject,
						module = this,
						distance = raycastHitData.distance,
						index = (float)resultAppendList.Count,
						depth = raycastHitData.graphic.depth,
						worldPosition = raycastHitData.worldHitPosition,
						screenPosition = raycastHitData.screenPosition
					};
					resultAppendList.Add(raycastResult);
				}
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003ED60 File Offset: 0x0003CF60
		private void SortedRaycastGraphics(Canvas canvas, Ray ray, List<TrackedDeviceRaycaster.RaycastHitData> results)
		{
			IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(canvas);
			TrackedDeviceRaycaster.s_SortedGraphics.Clear();
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				Graphic graphic = graphicsForCanvas[i];
				Vector3 vector;
				float num;
				if (graphic.depth != -1 && TrackedDeviceRaycaster.RayIntersectsRectTransform(graphic.rectTransform, ray, out vector, out num))
				{
					Vector2 vector2 = this.eventCamera.WorldToScreenPoint(vector);
					if (graphic.Raycast(vector2, this.eventCamera))
					{
						TrackedDeviceRaycaster.s_SortedGraphics.Add(new TrackedDeviceRaycaster.RaycastHitData(graphic, vector, vector2, num));
					}
				}
			}
			TrackedDeviceRaycaster.s_SortedGraphics.Sort((TrackedDeviceRaycaster.RaycastHitData g1, TrackedDeviceRaycaster.RaycastHitData g2) => g2.graphic.depth.CompareTo(g1.graphic.depth));
			results.AddRange(TrackedDeviceRaycaster.s_SortedGraphics);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003EE20 File Offset: 0x0003D020
		private static bool RayIntersectsRectTransform(RectTransform transform, Ray ray, out Vector3 worldPosition, out float distance)
		{
			Vector3[] array = new Vector3[4];
			transform.GetWorldCorners(array);
			Plane plane = new Plane(array[0], array[1], array[2]);
			float num;
			if (plane.Raycast(ray, out num))
			{
				Vector3 point = ray.GetPoint(num);
				Vector3 vector = array[3] - array[0];
				Vector3 vector2 = array[1] - array[0];
				float num2 = Vector3.Dot(point - array[0], vector);
				if (Vector3.Dot(point - array[0], vector2) >= 0f && num2 >= 0f)
				{
					Vector3 vector3 = array[1] - array[2];
					Vector3 vector4 = array[3] - array[2];
					float num3 = Vector3.Dot(point - array[2], vector3);
					float num4 = Vector3.Dot(point - array[2], vector4);
					if (num3 >= 0f && num4 >= 0f)
					{
						worldPosition = point;
						distance = num;
						return true;
					}
				}
			}
			worldPosition = Vector3.zero;
			distance = 0f;
			return false;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0003EF56 File Offset: 0x0003D156
		private Canvas canvas
		{
			get
			{
				if (this.m_Canvas != null)
				{
					return this.m_Canvas;
				}
				this.m_Canvas = base.GetComponent<Canvas>();
				return this.m_Canvas;
			}
		}

		// Token: 0x0400043E RID: 1086
		[NonSerialized]
		private List<TrackedDeviceRaycaster.RaycastHitData> m_RaycastResultsCache = new List<TrackedDeviceRaycaster.RaycastHitData>();

		// Token: 0x0400043F RID: 1087
		internal static InlinedArray<TrackedDeviceRaycaster> s_Instances;

		// Token: 0x04000440 RID: 1088
		private static readonly List<TrackedDeviceRaycaster.RaycastHitData> s_SortedGraphics = new List<TrackedDeviceRaycaster.RaycastHitData>();

		// Token: 0x04000441 RID: 1089
		[FormerlySerializedAs("ignoreReversedGraphics")]
		[SerializeField]
		private bool m_IgnoreReversedGraphics;

		// Token: 0x04000442 RID: 1090
		[FormerlySerializedAs("checkFor2DOcclusion")]
		[SerializeField]
		private bool m_CheckFor2DOcclusion;

		// Token: 0x04000443 RID: 1091
		[FormerlySerializedAs("checkFor3DOcclusion")]
		[SerializeField]
		private bool m_CheckFor3DOcclusion;

		// Token: 0x04000444 RID: 1092
		[Tooltip("Maximum distance (in 3D world space) that rays are traced to find a hit.")]
		[SerializeField]
		private float m_MaxDistance = 1000f;

		// Token: 0x04000445 RID: 1093
		[SerializeField]
		private LayerMask m_BlockingMask;

		// Token: 0x04000446 RID: 1094
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x020001D9 RID: 473
		private struct RaycastHitData
		{
			// Token: 0x060014B7 RID: 5303 RVA: 0x0006017A File Offset: 0x0005E37A
			public RaycastHitData(Graphic graphic, Vector3 worldHitPosition, Vector2 screenPosition, float distance)
			{
				this.graphic = graphic;
				this.worldHitPosition = worldHitPosition;
				this.screenPosition = screenPosition;
				this.distance = distance;
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00060199 File Offset: 0x0005E399
			public readonly Graphic graphic { get; }

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x060014B9 RID: 5305 RVA: 0x000601A1 File Offset: 0x0005E3A1
			public readonly Vector3 worldHitPosition { get; }

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x060014BA RID: 5306 RVA: 0x000601A9 File Offset: 0x0005E3A9
			public readonly Vector2 screenPosition { get; }

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x060014BB RID: 5307 RVA: 0x000601B1 File Offset: 0x0005E3B1
			public readonly float distance { get; }
		}
	}
}
