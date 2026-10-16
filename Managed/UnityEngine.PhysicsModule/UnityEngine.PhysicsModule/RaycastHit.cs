using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200004F RID: 79
	[NativeHeader("Modules/Physics/RaycastHit.h")]
	[NativeHeader("Runtime/Interfaces/IRaycast.h")]
	[NativeHeader("PhysicsScriptingClasses.h")]
	[UsedByNativeCode]
	public struct RaycastHit
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public Collider collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public int colliderInstanceID
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000A9F4 File Offset: 0x00008BF4
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000AA0C File Offset: 0x00008C0C
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000AA18 File Offset: 0x00008C18
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000AA30 File Offset: 0x00008C30
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000AA3C File Offset: 0x00008C3C
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0000AA86 File Offset: 0x00008C86
		public Vector3 barycentricCoordinate
		{
			get
			{
				return new Vector3(1f - (this.m_UV.y + this.m_UV.x), this.m_UV.x, this.m_UV.y);
			}
			set
			{
				this.m_UV = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000AA98 File Offset: 0x00008C98
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000AABC File Offset: 0x00008CBC
		public int triangleIndex
		{
			get
			{
				return (int)this.m_FaceID;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		[NativeMethod("CalculateRaycastTexCoord", true, true)]
		private static Vector2 CalculateRaycastTexCoord(int colliderInstanceID, Vector2 uv, Vector3 pos, uint face, int textcoord)
		{
			Vector2 vector;
			RaycastHit.CalculateRaycastTexCoord_Injected(colliderInstanceID, ref uv, ref pos, face, textcoord, out vector);
			return vector;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		public Vector2 textureCoord
		{
			get
			{
				return RaycastHit.CalculateRaycastTexCoord(this.m_Collider, this.m_UV, this.m_Point, this.m_FaceID, 0);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000AB24 File Offset: 0x00008D24
		public Vector2 textureCoord2
		{
			get
			{
				return RaycastHit.CalculateRaycastTexCoord(this.m_Collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000AB54 File Offset: 0x00008D54
		public Transform transform
		{
			get
			{
				Rigidbody rigidbody = this.rigidbody;
				bool flag = rigidbody != null;
				Transform transform;
				if (flag)
				{
					transform = rigidbody.transform;
				}
				else
				{
					bool flag2 = this.collider != null;
					if (flag2)
					{
						transform = this.collider.transform;
					}
					else
					{
						transform = null;
					}
				}
				return transform;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public Rigidbody rigidbody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedRigidbody : null;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public ArticulationBody articulationBody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedArticulationBody : null;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000AC00 File Offset: 0x00008E00
		public Vector2 lightmapCoord
		{
			get
			{
				Vector2 vector = RaycastHit.CalculateRaycastTexCoord(this.m_Collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				bool flag = this.collider.GetComponent<Renderer>() != null;
				if (flag)
				{
					Vector4 lightmapScaleOffset = this.collider.GetComponent<Renderer>().lightmapScaleOffset;
					vector.x = vector.x * lightmapScaleOffset.x + lightmapScaleOffset.z;
					vector.y = vector.y * lightmapScaleOffset.y + lightmapScaleOffset.w;
				}
				return vector;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000AC90 File Offset: 0x00008E90
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use textureCoord2 instead. (UnityUpgradable) -> textureCoord2")]
		public Vector2 textureCoord1
		{
			get
			{
				return this.textureCoord2;
			}
		}

		// Token: 0x060005A1 RID: 1441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateRaycastTexCoord_Injected(int colliderInstanceID, [In] ref Vector2 uv, [In] ref Vector3 pos, uint face, int textcoord, out Vector2 ret);

		// Token: 0x0400014E RID: 334
		[NativeName("point")]
		internal Vector3 m_Point;

		// Token: 0x0400014F RID: 335
		[NativeName("normal")]
		internal Vector3 m_Normal;

		// Token: 0x04000150 RID: 336
		[NativeName("faceID")]
		internal uint m_FaceID;

		// Token: 0x04000151 RID: 337
		[NativeName("distance")]
		internal float m_Distance;

		// Token: 0x04000152 RID: 338
		[NativeName("uv")]
		internal Vector2 m_UV;

		// Token: 0x04000153 RID: 339
		[NativeName("collider")]
		internal int m_Collider;
	}
}
