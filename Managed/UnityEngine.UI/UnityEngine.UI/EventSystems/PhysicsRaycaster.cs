using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000073 RID: 115
	[AddComponentMenu("Event/Physics Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class PhysicsRaycaster : BaseRaycaster
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0001BC29 File Offset: 0x00019E29
		protected PhysicsRaycaster()
		{
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001BC3D File Offset: 0x00019E3D
		public override Camera eventCamera
		{
			get
			{
				if (this.m_EventCamera == null)
				{
					this.m_EventCamera = base.GetComponent<Camera>();
				}
				if (this.m_EventCamera == null)
				{
					return Camera.main;
				}
				return this.m_EventCamera;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001BC73 File Offset: 0x00019E73
		public virtual int depth
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return 16777215;
				}
				return (int)this.eventCamera.depth;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001BC95 File Offset: 0x00019E95
		public int finalEventMask
		{
			get
			{
				if (!(this.eventCamera != null))
				{
					return -1;
				}
				return this.eventCamera.cullingMask & this.m_EventMask;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001BCBE File Offset: 0x00019EBE
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001BCC6 File Offset: 0x00019EC6
		public LayerMask eventMask
		{
			get
			{
				return this.m_EventMask;
			}
			set
			{
				this.m_EventMask = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001BCCF File Offset: 0x00019ECF
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001BCD7 File Offset: 0x00019ED7
		public int maxRayIntersections
		{
			get
			{
				return this.m_MaxRayIntersections;
			}
			set
			{
				this.m_MaxRayIntersections = value;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		protected bool ComputeRayAndDistance(PointerEventData eventData, ref Ray ray, ref int eventDisplayIndex, ref float distanceToClipPlane)
		{
			if (this.eventCamera == null)
			{
				return false;
			}
			Vector3 vector = MultipleDisplayUtilities.RelativeMouseAtScaled(eventData.position, eventData.displayIndex);
			if (vector != Vector3.zero)
			{
				eventDisplayIndex = (int)vector.z;
				if (eventDisplayIndex != this.eventCamera.targetDisplay)
				{
					return false;
				}
			}
			else
			{
				vector = eventData.position;
			}
			if (!this.eventCamera.pixelRect.Contains(vector))
			{
				return false;
			}
			ray = this.eventCamera.ScreenPointToRay(vector);
			float z = ray.direction.z;
			distanceToClipPlane = (Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane) / z));
			return true;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			Ray ray = default(Ray);
			int num = 0;
			float num2 = 0f;
			if (!this.ComputeRayAndDistance(eventData, ref ray, ref num, ref num2))
			{
				return;
			}
			int num3;
			if (this.m_MaxRayIntersections == 0)
			{
				if (ReflectionMethodsCache.Singleton.raycast3DAll == null)
				{
					return;
				}
				this.m_Hits = ReflectionMethodsCache.Singleton.raycast3DAll(ray, num2, this.finalEventMask);
				num3 = this.m_Hits.Length;
			}
			else
			{
				if (ReflectionMethodsCache.Singleton.getRaycastNonAlloc == null)
				{
					return;
				}
				if (this.m_LastMaxRayIntersections != this.m_MaxRayIntersections)
				{
					this.m_Hits = new RaycastHit[this.m_MaxRayIntersections];
					this.m_LastMaxRayIntersections = this.m_MaxRayIntersections;
				}
				num3 = ReflectionMethodsCache.Singleton.getRaycastNonAlloc(ray, this.m_Hits, num2, this.finalEventMask);
			}
			if (num3 != 0)
			{
				if (num3 > 1)
				{
					Array.Sort<RaycastHit>(this.m_Hits, 0, num3, PhysicsRaycaster.RaycastHitComparer.instance);
				}
				int i = 0;
				int num4 = num3;
				while (i < num4)
				{
					RaycastResult raycastResult = new RaycastResult
					{
						gameObject = this.m_Hits[i].collider.gameObject,
						module = this,
						distance = this.m_Hits[i].distance,
						worldPosition = this.m_Hits[i].point,
						worldNormal = this.m_Hits[i].normal,
						screenPosition = eventData.position,
						displayIndex = num,
						index = (float)resultAppendList.Count,
						sortingLayer = 0,
						sortingOrder = 0
					};
					resultAppendList.Add(raycastResult);
					i++;
				}
			}
		}

		// Token: 0x0400023F RID: 575
		protected const int kNoEventMaskSet = -1;

		// Token: 0x04000240 RID: 576
		protected Camera m_EventCamera;

		// Token: 0x04000241 RID: 577
		[SerializeField]
		protected LayerMask m_EventMask = -1;

		// Token: 0x04000242 RID: 578
		[SerializeField]
		protected int m_MaxRayIntersections;

		// Token: 0x04000243 RID: 579
		protected int m_LastMaxRayIntersections;

		// Token: 0x04000244 RID: 580
		private RaycastHit[] m_Hits;

		// Token: 0x020000CA RID: 202
		private class RaycastHitComparer : IComparer<RaycastHit>
		{
			// Token: 0x0600076C RID: 1900 RVA: 0x0001CE38 File Offset: 0x0001B038
			public int Compare(RaycastHit x, RaycastHit y)
			{
				return x.distance.CompareTo(y.distance);
			}

			// Token: 0x0400035D RID: 861
			public static PhysicsRaycaster.RaycastHitComparer instance = new PhysicsRaycaster.RaycastHitComparer();
		}
	}
}
