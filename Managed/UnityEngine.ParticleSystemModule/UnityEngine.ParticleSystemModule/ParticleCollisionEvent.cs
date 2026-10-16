using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200005D RID: 93
	[RequiredByNativeCode(Optional = true)]
	public struct ParticleCollisionEvent
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00007DDC File Offset: 0x00005FDC
		public Vector3 intersection
		{
			get
			{
				return this.m_Intersection;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00007DF4 File Offset: 0x00005FF4
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00007E0C File Offset: 0x0000600C
		public Vector3 velocity
		{
			get
			{
				return this.m_Velocity;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00007E24 File Offset: 0x00006024
		public Component colliderComponent
		{
			get
			{
				return ParticleCollisionEvent.InstanceIDToColliderComponent(this.m_ColliderInstanceID);
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00007E44 File Offset: 0x00006044
		[FreeFunction(Name = "ParticleSystemScriptBindings::InstanceIDToColliderComponent")]
		private static Component InstanceIDToColliderComponent(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Component>(ParticleCollisionEvent.InstanceIDToColliderComponent_Injected(instanceID));
		}

		// Token: 0x06000658 RID: 1624
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InstanceIDToColliderComponent_Injected(int instanceID);

		// Token: 0x04000195 RID: 405
		internal Vector3 m_Intersection;

		// Token: 0x04000196 RID: 406
		internal Vector3 m_Normal;

		// Token: 0x04000197 RID: 407
		internal Vector3 m_Velocity;

		// Token: 0x04000198 RID: 408
		internal int m_ColliderInstanceID;
	}
}
