using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/Physics/PhysicsCollisionGeometry.h")]
	[NativeHeader("Modules/Physics/PhysXContactModification.h")]
	public struct ModifiableContactPair
	{
		// Token: 0x060003CD RID: 973
		[FreeFunction("Physics::PhysxCompatibility::TranslateTriangleIndex", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint TranslateTriangleIndex(IntPtr shapePtr, uint rawIndex);

		// Token: 0x060003CE RID: 974
		[FreeFunction("Physics::PhysxCompatibility::ResolveShapeToInstanceID", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ResolveShapeToInstanceID(IntPtr shapePtr);

		// Token: 0x060003CF RID: 975
		[FreeFunction("Physics::PhysxCompatibility::ResolveActorToInstanceID", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ResolveActorToInstanceID(IntPtr actorPtr);

		// Token: 0x060003D0 RID: 976 RVA: 0x00007CF4 File Offset: 0x00005EF4
		[FreeFunction("Physics::PhysxCompatibility::GetActorLinearVelocity", true)]
		internal static Vector3 GetActorLinearVelocity(IntPtr actorPtr)
		{
			Vector3 vector;
			ModifiableContactPair.GetActorLinearVelocity_Injected(actorPtr, out vector);
			return vector;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00007D0C File Offset: 0x00005F0C
		[FreeFunction("Physics::PhysxCompatibility::GetActorAngularVelocity", true)]
		internal static Vector3 GetActorAngularVelocity(IntPtr actorPtr)
		{
			Vector3 vector;
			ModifiableContactPair.GetActorAngularVelocity_Injected(actorPtr, out vector);
			return vector;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00007D22 File Offset: 0x00005F22
		public int colliderInstanceID
		{
			get
			{
				return ModifiableContactPair.ResolveShapeToInstanceID(this.shape);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00007D2F File Offset: 0x00005F2F
		public int otherColliderInstanceID
		{
			get
			{
				return ModifiableContactPair.ResolveShapeToInstanceID(this.otherShape);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00007D3C File Offset: 0x00005F3C
		public int bodyInstanceID
		{
			get
			{
				return ModifiableContactPair.ResolveActorToInstanceID(this.actor);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00007D49 File Offset: 0x00005F49
		public int otherBodyInstanceID
		{
			get
			{
				return ModifiableContactPair.ResolveActorToInstanceID(this.otherActor);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00007D56 File Offset: 0x00005F56
		public Vector3 bodyVelocity
		{
			get
			{
				return ModifiableContactPair.GetActorLinearVelocity(this.actor);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00007D63 File Offset: 0x00005F63
		public Vector3 bodyAngularVelocity
		{
			get
			{
				return ModifiableContactPair.GetActorAngularVelocity(this.actor);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00007D70 File Offset: 0x00005F70
		public Vector3 otherBodyVelocity
		{
			get
			{
				return ModifiableContactPair.GetActorLinearVelocity(this.otherActor);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00007D7D File Offset: 0x00005F7D
		public Vector3 otherBodyAngularVelocity
		{
			get
			{
				return ModifiableContactPair.GetActorAngularVelocity(this.otherActor);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00007D8A File Offset: 0x00005F8A
		public int contactCount
		{
			get
			{
				return this.numContacts;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00007D94 File Offset: 0x00005F94
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public unsafe ModifiableMassProperties massProperties
		{
			get
			{
				return this.GetContactPatch()->massProperties;
			}
			set
			{
				ModifiableContactPatch* contactPatch = this.GetContactPatch();
				contactPatch->massProperties = value;
				ModifiableContactPatch* ptr = contactPatch;
				ptr->internalFlags = ptr->internalFlags | 8;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00007DDC File Offset: 0x00005FDC
		public unsafe Vector3 GetPoint(int i)
		{
			return this.GetContact(i)->contact;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00007DFA File Offset: 0x00005FFA
		public unsafe void SetPoint(int i, Vector3 v)
		{
			this.GetContact(i)->contact = v;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00007E0C File Offset: 0x0000600C
		public unsafe Vector3 GetNormal(int i)
		{
			return this.GetContact(i)->normal;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00007E2A File Offset: 0x0000602A
		public unsafe void SetNormal(int i, Vector3 normal)
		{
			this.GetContact(i)->normal = normal;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = contactPatch->internalFlags | 64;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00007E4C File Offset: 0x0000604C
		public unsafe float GetSeparation(int i)
		{
			return this.GetContact(i)->separation;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00007E6A File Offset: 0x0000606A
		public unsafe void SetSeparation(int i, float separation)
		{
			this.GetContact(i)->separation = separation;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00007E7C File Offset: 0x0000607C
		public unsafe Vector3 GetTargetVelocity(int i)
		{
			return this.GetContact(i)->targetVelocity;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00007E9A File Offset: 0x0000609A
		public unsafe void SetTargetVelocity(int i, Vector3 velocity)
		{
			this.GetContact(i)->targetVelocity = velocity;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = contactPatch->internalFlags | 16;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00007EBC File Offset: 0x000060BC
		public unsafe float GetBounciness(int i)
		{
			return this.GetContact(i)->restitution;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00007EDA File Offset: 0x000060DA
		public unsafe void SetBounciness(int i, float bounciness)
		{
			this.GetContact(i)->restitution = bounciness;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = contactPatch->internalFlags | 64;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00007EFC File Offset: 0x000060FC
		public unsafe float GetStaticFriction(int i)
		{
			return this.GetContact(i)->staticFriction;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00007F1A File Offset: 0x0000611A
		public unsafe void SetStaticFriction(int i, float staticFriction)
		{
			this.GetContact(i)->staticFriction = staticFriction;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = contactPatch->internalFlags | 64;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00007F3C File Offset: 0x0000613C
		public unsafe float GetDynamicFriction(int i)
		{
			return this.GetContact(i)->dynamicFriction;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00007F5A File Offset: 0x0000615A
		public unsafe void SetDynamicFriction(int i, float dynamicFriction)
		{
			this.GetContact(i)->dynamicFriction = dynamicFriction;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = contactPatch->internalFlags | 64;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00007F7C File Offset: 0x0000617C
		public unsafe float GetMaxImpulse(int i)
		{
			return this.GetContact(i)->maxImpulse;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00007F9A File Offset: 0x0000619A
		public unsafe void SetMaxImpulse(int i, float value)
		{
			this.GetContact(i)->maxImpulse = value;
			ModifiableContactPatch* contactPatch = this.GetContactPatch();
			contactPatch->internalFlags = contactPatch->internalFlags | 32;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00007FBC File Offset: 0x000061BC
		public void IgnoreContact(int i)
		{
			this.SetMaxImpulse(i, 0f);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00007FCC File Offset: 0x000061CC
		public unsafe uint GetFaceIndex(int i)
		{
			bool flag = (this.GetContactPatch()->internalFlags & 1) > 0;
			uint num2;
			if (flag)
			{
				IntPtr intPtr = new IntPtr(this.contacts.ToInt64() + (long)(this.numContacts * sizeof(ModifiableContact)) + (long)((this.numContacts + i) * 4));
				uint num = *(uint*)(void*)intPtr;
				num2 = ModifiableContactPair.TranslateTriangleIndex(this.otherShape, num);
			}
			else
			{
				num2 = uint.MaxValue;
			}
			return num2;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00008038 File Offset: 0x00006238
		private unsafe ModifiableContact* GetContact(int index)
		{
			IntPtr intPtr = new IntPtr(this.contacts.ToInt64() + (long)(index * sizeof(ModifiableContact)));
			return (ModifiableContact*)(void*)intPtr;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000806C File Offset: 0x0000626C
		private unsafe ModifiableContactPatch* GetContactPatch()
		{
			IntPtr intPtr = new IntPtr(this.contacts.ToInt64() - (long)(this.numContacts * sizeof(ModifiableContactPatch)));
			return (ModifiableContactPatch*)(void*)intPtr;
		}

		// Token: 0x060003F1 RID: 1009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActorLinearVelocity_Injected(IntPtr actorPtr, out Vector3 ret);

		// Token: 0x060003F2 RID: 1010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActorAngularVelocity_Injected(IntPtr actorPtr, out Vector3 ret);

		// Token: 0x0400007E RID: 126
		private IntPtr actor;

		// Token: 0x0400007F RID: 127
		private IntPtr otherActor;

		// Token: 0x04000080 RID: 128
		private IntPtr shape;

		// Token: 0x04000081 RID: 129
		private IntPtr otherShape;

		// Token: 0x04000082 RID: 130
		public Quaternion rotation;

		// Token: 0x04000083 RID: 131
		public Vector3 position;

		// Token: 0x04000084 RID: 132
		public Quaternion otherRotation;

		// Token: 0x04000085 RID: 133
		public Vector3 otherPosition;

		// Token: 0x04000086 RID: 134
		private int numContacts;

		// Token: 0x04000087 RID: 135
		private IntPtr contacts;
	}
}
