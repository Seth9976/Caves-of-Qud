using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics2D/Public/Rigidbody2D.h")]
	public sealed class Rigidbody2D : Component
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00008330 File Offset: 0x00006530
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00008358 File Offset: 0x00006558
		public Vector2 position
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Rigidbody2D.get_position_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_position_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000837C File Offset: 0x0000657C
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000083A0 File Offset: 0x000065A0
		public float rotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_rotation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_rotation_Injected(intPtr, value);
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000083C3 File Offset: 0x000065C3
		public void SetRotation(float angle)
		{
			this.SetRotation_Angle(angle);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000083D0 File Offset: 0x000065D0
		[NativeMethod("SetRotation")]
		private void SetRotation_Angle(float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.SetRotation_Angle_Injected(intPtr, angle);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000083F3 File Offset: 0x000065F3
		public void SetRotation(Quaternion rotation)
		{
			this.SetRotation_Quaternion(rotation);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008400 File Offset: 0x00006600
		[NativeMethod("SetRotation")]
		private void SetRotation_Quaternion(Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.SetRotation_Quaternion_Injected(intPtr, ref rotation);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008424 File Offset: 0x00006624
		public void MovePosition(Vector2 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.MovePosition_Injected(intPtr, ref position);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008448 File Offset: 0x00006648
		public void MoveRotation(float angle)
		{
			this.MoveRotation_Angle(angle);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008454 File Offset: 0x00006654
		[NativeMethod("MoveRotation")]
		private void MoveRotation_Angle(float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.MoveRotation_Angle_Injected(intPtr, angle);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008477 File Offset: 0x00006677
		public void MoveRotation(Quaternion rotation)
		{
			this.MoveRotation_Quaternion(rotation);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008484 File Offset: 0x00006684
		[NativeMethod("MoveRotation")]
		private void MoveRotation_Quaternion(Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.MoveRotation_Quaternion_Injected(intPtr, ref rotation);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000084A8 File Offset: 0x000066A8
		[NativeMethod("MovePositionAndRotation")]
		public void MovePositionAndRotation(Vector2 position, float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.MovePositionAndRotation_Injected(intPtr, ref position, angle);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000084CD File Offset: 0x000066CD
		public void MovePositionAndRotation(Vector2 position, Quaternion rotation)
		{
			this.MovePositionAndRotation_Quaternion(position, rotation);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000084DC File Offset: 0x000066DC
		[NativeMethod("MovePositionAndRotation")]
		private void MovePositionAndRotation_Quaternion(Vector2 position, Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.MovePositionAndRotation_Quaternion_Injected(intPtr, ref position, ref rotation);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008504 File Offset: 0x00006704
		public Rigidbody2D.SlideResults Slide(Vector2 velocity, float deltaTime, Rigidbody2D.SlideMovement slideMovement)
		{
			bool flag = deltaTime < 0f;
			if (flag)
			{
				throw new ArgumentException(string.Format("Time cannot be negative. It is {0}.", deltaTime), "deltaTime");
			}
			bool flag2 = Mathf.Approximately(deltaTime, 0f);
			Rigidbody2D.SlideResults slideResults;
			if (flag2)
			{
				slideResults = new Rigidbody2D.SlideResults
				{
					position = (slideMovement.useStartPosition ? slideMovement.startPosition : this.position),
					remainingVelocity = velocity
				};
			}
			else
			{
				bool flag3 = slideMovement.useSimulationMove && this.bodyType == RigidbodyType2D.Static;
				if (flag3)
				{
					throw new ArgumentException(string.Format("Cannot use simulation move when the body type is Static. It is {0}.", slideMovement.useSimulationMove), "SlideMovement.useSimulationMove");
				}
				bool flag4 = slideMovement.useNoMove && slideMovement.useSimulationMove;
				if (flag4)
				{
					throw new ArgumentException(string.Format("Cannot use no move and simulation move at the same time; the two are conflicting options. It is {0}.", slideMovement.useNoMove), "SlideMovement.useNoMove");
				}
				bool flag5 = slideMovement.maxIterations < 1;
				if (flag5)
				{
					throw new ArgumentException(string.Format("Maximum Iterations must be greater than zero. It is {0}.", slideMovement.maxIterations), "SlideMovement.maxIterations");
				}
				bool flag6 = !float.IsFinite(slideMovement.surfaceSlideAngle) || slideMovement.surfaceSlideAngle < 0f || slideMovement.surfaceSlideAngle > 90f;
				if (flag6)
				{
					throw new ArgumentException(string.Format("Surface Slide Angle must be in the range of 0 to 90 degrees. It is {0}.", slideMovement.surfaceSlideAngle), "SlideMovement.surfaceSlideAngle");
				}
				bool flag7 = !float.IsFinite(slideMovement.gravitySlipAngle) || slideMovement.gravitySlipAngle < 0f || slideMovement.gravitySlipAngle > 90f;
				if (flag7)
				{
					throw new ArgumentException(string.Format("Gravity Slip Angle must be in the range of 0 to 90 degrees. It is {0}.", slideMovement.gravitySlipAngle), "SlideMovement.gravitySlipAngle");
				}
				bool flag8 = !float.IsFinite(slideMovement.surfaceUp.x) || !float.IsFinite(slideMovement.surfaceUp.y);
				if (flag8)
				{
					throw new ArgumentException(string.Format("Surface Up is invalid. It is {0}.", slideMovement.surfaceUp), "SlideMovement.surfaceUp");
				}
				bool flag9 = !float.IsFinite(slideMovement.surfaceAnchor.x) || !float.IsFinite(slideMovement.surfaceAnchor.y);
				if (flag9)
				{
					throw new ArgumentException(string.Format("Surface Anchor is invalid. It is {0}.", slideMovement.surfaceAnchor), "SlideMovement.surfaceAnchor");
				}
				bool flag10 = !float.IsFinite(slideMovement.gravity.x) || !float.IsFinite(slideMovement.gravity.y);
				if (flag10)
				{
					throw new ArgumentException(string.Format("Gravity is invalid. It is {0}.", slideMovement.gravity), "SlideMovement.gravity");
				}
				bool flag11 = !float.IsFinite(slideMovement.startPosition.x) || !float.IsFinite(slideMovement.startPosition.y);
				if (flag11)
				{
					throw new ArgumentException(string.Format("Start Position is invalid. It is {0}.", slideMovement.gravity), "SlideMovement.startPosition");
				}
				bool flag12 = slideMovement.selectedCollider && slideMovement.selectedCollider.attachedRigidbody != this;
				if (flag12)
				{
					throw new ArgumentException(string.Format("Selected Collider must be attached to the Slide Rigidbody2D. It is {0}.", slideMovement.selectedCollider), "SlideMovement.selectedCollider");
				}
				slideResults = this.Slide_Internal(velocity, deltaTime, slideMovement);
			}
			return slideResults;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008868 File Offset: 0x00006A68
		[NativeMethod("Slide")]
		private Rigidbody2D.SlideResults Slide_Internal(Vector2 velocity, float deltaTime, Rigidbody2D.SlideMovement slideMovement)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.SlideResults slideResults;
			Rigidbody2D.Slide_Internal_Injected(intPtr, ref velocity, deltaTime, ref slideMovement, out slideResults);
			return slideResults;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00008894 File Offset: 0x00006A94
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x000088BC File Offset: 0x00006ABC
		public Vector2 linearVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Rigidbody2D.get_linearVelocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_linearVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000088E0 File Offset: 0x00006AE0
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00008904 File Offset: 0x00006B04
		public float linearVelocityX
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_linearVelocityX_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_linearVelocityX_Injected(intPtr, value);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00008928 File Offset: 0x00006B28
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000894C File Offset: 0x00006B4C
		public float linearVelocityY
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_linearVelocityY_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_linearVelocityY_Injected(intPtr, value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00008970 File Offset: 0x00006B70
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00008994 File Offset: 0x00006B94
		public float angularVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_angularVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_angularVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000089B8 File Offset: 0x00006BB8
		// (set) Token: 0x060002CB RID: 715 RVA: 0x000089DC File Offset: 0x00006BDC
		public bool useAutoMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_useAutoMass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_useAutoMass_Injected(intPtr, value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00008A00 File Offset: 0x00006C00
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00008A24 File Offset: 0x00006C24
		public float mass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_mass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_mass_Injected(intPtr, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00008A48 File Offset: 0x00006C48
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00008A70 File Offset: 0x00006C70
		[NativeMethod("Material")]
		public PhysicsMaterial2D sharedMaterial
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<PhysicsMaterial2D>(Rigidbody2D.get_sharedMaterial_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_sharedMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<PhysicsMaterial2D>(value));
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00008A98 File Offset: 0x00006C98
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public Vector2 centerOfMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Rigidbody2D.get_centerOfMass_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_centerOfMass_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public Vector2 worldCenterOfMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Rigidbody2D.get_worldCenterOfMass_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00008B0C File Offset: 0x00006D0C
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00008B30 File Offset: 0x00006D30
		public float inertia
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_inertia_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_inertia_Injected(intPtr, value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00008B54 File Offset: 0x00006D54
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00008B78 File Offset: 0x00006D78
		public float linearDamping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_linearDamping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_linearDamping_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00008B9C File Offset: 0x00006D9C
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00008BC0 File Offset: 0x00006DC0
		public float angularDamping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_angularDamping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_angularDamping_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00008BE4 File Offset: 0x00006DE4
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00008C08 File Offset: 0x00006E08
		public float gravityScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_gravityScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_gravityScale_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00008C2C File Offset: 0x00006E2C
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00008C50 File Offset: 0x00006E50
		public RigidbodyType2D bodyType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_bodyType_Injected(intPtr);
			}
			[NativeMethod("SetBodyType_Binding")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_bodyType_Injected(intPtr, value);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00008C74 File Offset: 0x00006E74
		internal void SetDragBehaviour(bool dragged)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.SetDragBehaviour_Injected(intPtr, dragged);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00008C98 File Offset: 0x00006E98
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00008CBC File Offset: 0x00006EBC
		public bool useFullKinematicContacts
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_useFullKinematicContacts_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_useFullKinematicContacts_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00008CE0 File Offset: 0x00006EE0
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00008D04 File Offset: 0x00006F04
		public bool freezeRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_freezeRotation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_freezeRotation_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00008D28 File Offset: 0x00006F28
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00008D4C File Offset: 0x00006F4C
		public RigidbodyConstraints2D constraints
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_constraints_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_constraints_Injected(intPtr, value);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008D70 File Offset: 0x00006F70
		public bool IsSleeping()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.IsSleeping_Injected(intPtr);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008D94 File Offset: 0x00006F94
		public bool IsAwake()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.IsAwake_Injected(intPtr);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public void Sleep()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.Sleep_Injected(intPtr);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008DDC File Offset: 0x00006FDC
		[NativeMethod("Wake")]
		public void WakeUp()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.WakeUp_Injected(intPtr);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00008E00 File Offset: 0x00007000
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00008E24 File Offset: 0x00007024
		public bool simulated
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_simulated_Injected(intPtr);
			}
			[NativeMethod("SetSimulated_Binding")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_simulated_Injected(intPtr, value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00008E48 File Offset: 0x00007048
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00008E6C File Offset: 0x0000706C
		public RigidbodyInterpolation2D interpolation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_interpolation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_interpolation_Injected(intPtr, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00008E90 File Offset: 0x00007090
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00008EB4 File Offset: 0x000070B4
		public RigidbodySleepMode2D sleepMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_sleepMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_sleepMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00008ED8 File Offset: 0x000070D8
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00008EFC File Offset: 0x000070FC
		public CollisionDetectionMode2D collisionDetectionMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_collisionDetectionMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_collisionDetectionMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00008F20 File Offset: 0x00007120
		public int attachedColliderCount
		{
			get
			{
				return this.GetAttachedColliderCount_Internal(true);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008F3C File Offset: 0x0000713C
		[NativeMethod("GetAttachedColliderCount")]
		private int GetAttachedColliderCount_Internal(bool findTriggers)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.GetAttachedColliderCount_Internal_Injected(intPtr, findTriggers);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00008F60 File Offset: 0x00007160
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00008F88 File Offset: 0x00007188
		public Vector2 totalForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Rigidbody2D.get_totalForce_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_totalForce_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00008FAC File Offset: 0x000071AC
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00008FD0 File Offset: 0x000071D0
		public float totalTorque
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_totalTorque_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_totalTorque_Injected(intPtr, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00008FF4 File Offset: 0x000071F4
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000901C File Offset: 0x0000721C
		public LayerMask excludeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Rigidbody2D.get_excludeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_excludeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00009040 File Offset: 0x00007240
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00009068 File Offset: 0x00007268
		public LayerMask includeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Rigidbody2D.get_includeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_includeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000908C File Offset: 0x0000728C
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Rigidbody2D.get_localToWorldMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000090B4 File Offset: 0x000072B4
		public bool IsTouching([NotNull] Collider2D collider)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return Rigidbody2D.IsTouching_Injected(intPtr, intPtr2);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000090FC File Offset: 0x000072FC
		public bool IsTouching(Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter_Internal(collider, contactFilter);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009118 File Offset: 0x00007318
		[NativeMethod("IsTouching")]
		private bool IsTouching_OtherColliderWithFilter_Internal([NotNull] Collider2D collider, ContactFilter2D contactFilter)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return Rigidbody2D.IsTouching_OtherColliderWithFilter_Internal_Injected(intPtr, intPtr2, ref contactFilter);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009160 File Offset: 0x00007360
		public bool IsTouching(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter_Internal(contactFilter);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000917C File Offset: 0x0000737C
		[NativeMethod("IsTouching")]
		private bool IsTouching_AnyColliderWithFilter_Internal(ContactFilter2D contactFilter)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.IsTouching_AnyColliderWithFilter_Internal_Injected(intPtr, ref contactFilter);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000091A0 File Offset: 0x000073A0
		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			return this.IsTouchingLayers(-1);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000091BC File Offset: 0x000073BC
		public bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask = -1)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.SetLayerMask(layerMask);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			return this.IsTouching(contactFilter2D);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000091F8 File Offset: 0x000073F8
		public bool OverlapPoint(Vector2 point)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.OverlapPoint_Injected(intPtr, ref point);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000921C File Offset: 0x0000741C
		public ColliderDistance2D Distance(Collider2D collider)
		{
			bool flag = collider == null;
			if (flag)
			{
				throw new ArgumentNullException("Collider cannot be null.");
			}
			bool flag2 = collider.attachedRigidbody == this;
			if (flag2)
			{
				throw new ArgumentException("The collider cannot be attached to the Rigidbody2D being searched.");
			}
			return this.Distance_Internal(collider);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009268 File Offset: 0x00007468
		public ColliderDistance2D Distance(Vector2 thisPosition, float thisAngle, Collider2D collider, Vector2 position, float angle)
		{
			bool flag = !collider.attachedRigidbody;
			if (flag)
			{
				throw new InvalidOperationException("Cannot perform a Collider Distance at a specific position and angle if the Collider is not attached to a Rigidbody2D.");
			}
			return this.DistanceFrom_Internal(thisPosition, thisAngle, collider, position, angle);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000092A4 File Offset: 0x000074A4
		[NativeMethod("Distance")]
		private ColliderDistance2D Distance_Internal([NotNull] Collider2D collider)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			ColliderDistance2D colliderDistance2D;
			Rigidbody2D.Distance_Internal_Injected(intPtr, intPtr2, out colliderDistance2D);
			return colliderDistance2D;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000092F0 File Offset: 0x000074F0
		[NativeMethod("DistanceFrom")]
		private ColliderDistance2D DistanceFrom_Internal(Vector2 thisPosition, float thisAngle, [NotNull] Collider2D collider, Vector2 position, float angle)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			ColliderDistance2D colliderDistance2D;
			Rigidbody2D.DistanceFrom_Internal_Injected(intPtr, ref thisPosition, thisAngle, intPtr2, ref position, angle, out colliderDistance2D);
			return colliderDistance2D;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009340 File Offset: 0x00007540
		public Vector2 ClosestPoint(Vector2 position)
		{
			return Physics2D.ClosestPoint(position, this);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009359 File Offset: 0x00007559
		[ExcludeFromDocs]
		public void AddForce(Vector2 force)
		{
			this.AddForce_Internal(force, ForceMode2D.Force);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009365 File Offset: 0x00007565
		public void AddForce(Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode = ForceMode2D.Force)
		{
			this.AddForce_Internal(force, mode);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009371 File Offset: 0x00007571
		public void AddForceX(float force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode = ForceMode2D.Force)
		{
			this.AddForce_Internal(new Vector2(force, 0f), mode);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00009387 File Offset: 0x00007587
		public void AddForceY(float force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode = ForceMode2D.Force)
		{
			this.AddForce_Internal(new Vector2(0f, force), mode);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000093A0 File Offset: 0x000075A0
		[NativeMethod("AddForce")]
		private void AddForce_Internal(Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.AddForce_Internal_Injected(intPtr, ref force, mode);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000093C5 File Offset: 0x000075C5
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector2 relativeForce)
		{
			this.AddRelativeForce_Internal(relativeForce, ForceMode2D.Force);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000093D1 File Offset: 0x000075D1
		public void AddRelativeForce(Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode = ForceMode2D.Force)
		{
			this.AddRelativeForce_Internal(relativeForce, mode);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000093DD File Offset: 0x000075DD
		public void AddRelativeForceX(float force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode = ForceMode2D.Force)
		{
			this.AddRelativeForce_Internal(new Vector2(force, 0f), mode);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000093F3 File Offset: 0x000075F3
		public void AddRelativeForceY(float force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode = ForceMode2D.Force)
		{
			this.AddRelativeForce_Internal(new Vector2(0f, force), mode);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000940C File Offset: 0x0000760C
		[NativeMethod("AddRelativeForce")]
		private void AddRelativeForce_Internal(Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.AddRelativeForce_Internal_Injected(intPtr, ref relativeForce, mode);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009431 File Offset: 0x00007631
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector2 force, Vector2 position)
		{
			this.AddForceAtPosition(force, position, ForceMode2D.Force);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009440 File Offset: 0x00007640
		public void AddForceAtPosition(Vector2 force, Vector2 position, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.AddForceAtPosition_Injected(intPtr, ref force, ref position, mode);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009467 File Offset: 0x00007667
		[ExcludeFromDocs]
		public void AddTorque(float torque)
		{
			this.AddTorque(torque, ForceMode2D.Force);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009474 File Offset: 0x00007674
		public void AddTorque(float torque, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.AddTorque_Injected(intPtr, torque, mode);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009498 File Offset: 0x00007698
		public Vector2 GetPoint(Vector2 point)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Rigidbody2D.GetPoint_Injected(intPtr, ref point, out vector);
			return vector;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000094C0 File Offset: 0x000076C0
		public Vector2 GetRelativePoint(Vector2 relativePoint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Rigidbody2D.GetRelativePoint_Injected(intPtr, ref relativePoint, out vector);
			return vector;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000094E8 File Offset: 0x000076E8
		public Vector2 GetVector(Vector2 vector)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector2;
			Rigidbody2D.GetVector_Injected(intPtr, ref vector, out vector2);
			return vector2;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00009510 File Offset: 0x00007710
		public Vector2 GetRelativeVector(Vector2 relativeVector)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Rigidbody2D.GetRelativeVector_Injected(intPtr, ref relativeVector, out vector);
			return vector;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00009538 File Offset: 0x00007738
		public Vector2 GetPointVelocity(Vector2 point)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Rigidbody2D.GetPointVelocity_Injected(intPtr, ref point, out vector);
			return vector;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00009560 File Offset: 0x00007760
		public Vector2 GetRelativePointVelocity(Vector2 relativePoint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Rigidbody2D.GetRelativePointVelocity_Injected(intPtr, ref relativePoint, out vector);
			return vector;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00009588 File Offset: 0x00007788
		public int GetContacts(ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000095B0 File Offset: 0x000077B0
		public int GetContacts(List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000095D8 File Offset: 0x000077D8
		public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000095F4 File Offset: 0x000077F4
		public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009610 File Offset: 0x00007810
		public int GetContacts(Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00009638 File Offset: 0x00007838
		public int GetContacts(List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00009660 File Offset: 0x00007860
		public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000967C File Offset: 0x0000787C
		public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00009698 File Offset: 0x00007898
		[ExcludeFromDocs]
		public int GetAttachedColliders([Out] Collider2D[] results)
		{
			return this.GetAttachedCollidersArray_Internal(results, true);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000096B4 File Offset: 0x000078B4
		[ExcludeFromDocs]
		public int GetAttachedColliders(List<Collider2D> results)
		{
			return this.GetAttachedCollidersList_Internal(results, true);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000096D0 File Offset: 0x000078D0
		public int GetAttachedColliders([Out] Collider2D[] results, [DefaultValue("true")] bool findTriggers = true)
		{
			return this.GetAttachedCollidersArray_Internal(results, findTriggers);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000096EC File Offset: 0x000078EC
		public int GetAttachedColliders(List<Collider2D> results, [DefaultValue("true")] bool findTriggers = true)
		{
			return this.GetAttachedCollidersList_Internal(results, findTriggers);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00009708 File Offset: 0x00007908
		public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup)
		{
			return this.GetShapes_Internal(ref physicsShapeGroup.m_GroupState);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00009728 File Offset: 0x00007928
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results)
		{
			return this.CastArray_Internal(direction, float.PositiveInfinity, false, results);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00009748 File Offset: 0x00007948
		public int Cast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.CastArray_Internal(direction, distance, false, results);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00009764 File Offset: 0x00007964
		public int Cast(Vector2 direction, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.CastList_Internal(direction, distance, false, results);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00009780 File Offset: 0x00007980
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.CastFilteredArray_Internal(direction, float.PositiveInfinity, false, contactFilter, results);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000097A4 File Offset: 0x000079A4
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.CastFilteredArray_Internal(direction, distance, false, contactFilter, results);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000097C4 File Offset: 0x000079C4
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.CastFilteredList_Internal(direction, distance, false, contactFilter, results);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000097E4 File Offset: 0x000079E4
		public int Cast(Vector2 position, float angle, Vector2 direction, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.CastFrom_Internal(position, angle, direction, distance, false, results);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00009804 File Offset: 0x00007A04
		public int Cast(Vector2 position, float angle, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.CastFromFiltered_Internal(position, angle, direction, distance, false, contactFilter, results);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00009828 File Offset: 0x00007A28
		public int Overlap(ContactFilter2D contactFilter, [Out] Collider2D[] results)
		{
			return this.OverlapArray_Internal(contactFilter, results);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00009844 File Offset: 0x00007A44
		public int Overlap(List<Collider2D> results)
		{
			return this.OverlapList_Internal(results);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009860 File Offset: 0x00007A60
		public int Overlap(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return this.OverlapFilteredList_Internal(contactFilter, results);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000987C File Offset: 0x00007A7C
		public int Overlap(Vector2 position, float angle, List<Collider2D> results)
		{
			return this.OverlapFromList_Internal(position, angle, results);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00009898 File Offset: 0x00007A98
		public int Overlap(Vector2 position, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return this.OverlapFromFilteredList_Internal(position, angle, contactFilter, results);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000098B8 File Offset: 0x00007AB8
		[NativeMethod("GetAttachedCollidersArray_Binding")]
		private int GetAttachedCollidersArray_Internal([Unmarshalled] [NotNull] Collider2D[] results, bool findTriggers)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.GetAttachedCollidersArray_Internal_Injected(intPtr, results, findTriggers);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000098EC File Offset: 0x00007AEC
		[NativeMethod("GetAttachedCollidersList_Binding")]
		private int GetAttachedCollidersList_Internal([NotNull] List<Collider2D> results, bool findTriggers)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.GetAttachedCollidersList_Internal_Injected(intPtr, results, findTriggers);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009920 File Offset: 0x00007B20
		[NativeMethod("GetShapes_Binding")]
		private int GetShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.GetShapes_Internal_Injected(intPtr, ref physicsShapeGroupState);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009944 File Offset: 0x00007B44
		[NativeMethod("CastArray_Binding")]
		private unsafe int CastArray_Internal(Vector2 direction, float distance, bool checkIgnoreColliders, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = Rigidbody2D.CastArray_Internal_Injected(intPtr, ref direction, distance, checkIgnoreColliders, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000099A4 File Offset: 0x00007BA4
		[NativeMethod("CastList_Binding")]
		private unsafe int CastList_Internal(Vector2 direction, float distance, bool checkIgnoreColliders, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Rigidbody2D.CastList_Internal_Injected(intPtr, ref direction, distance, checkIgnoreColliders, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009A2C File Offset: 0x00007C2C
		[NativeMethod("CastFilteredArray_Binding")]
		private unsafe int CastFilteredArray_Internal(Vector2 direction, float distance, bool checkIgnoreColliders, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = Rigidbody2D.CastFilteredArray_Internal_Injected(intPtr, ref direction, distance, checkIgnoreColliders, ref contactFilter, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009A8C File Offset: 0x00007C8C
		[NativeMethod("CastFilteredList_Binding")]
		private unsafe int CastFilteredList_Internal(Vector2 direction, float distance, bool checkIgnoreColliders, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Rigidbody2D.CastFilteredList_Internal_Injected(intPtr, ref direction, distance, checkIgnoreColliders, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009B18 File Offset: 0x00007D18
		[NativeMethod("CastFrom_Binding")]
		private unsafe int CastFrom_Internal(Vector2 position, float angle, Vector2 direction, float distance, bool checkIgnoreColliders, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Rigidbody2D.CastFrom_Internal_Injected(intPtr, ref position, angle, ref direction, distance, checkIgnoreColliders, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009BA4 File Offset: 0x00007DA4
		[NativeMethod("CastFromFiltered_Binding")]
		private unsafe int CastFromFiltered_Internal(Vector2 position, float angle, Vector2 direction, float distance, bool checkIgnoreColliders, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Rigidbody2D.CastFromFiltered_Internal_Injected(intPtr, ref position, angle, ref direction, distance, checkIgnoreColliders, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009C34 File Offset: 0x00007E34
		[NativeMethod("OverlapArray_Binding")]
		private int OverlapArray_Internal(ContactFilter2D contactFilter, [NotNull] [Unmarshalled] Collider2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.OverlapArray_Internal_Injected(intPtr, ref contactFilter, results);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00009C68 File Offset: 0x00007E68
		[NativeMethod("OverlapList_Binding")]
		private int OverlapList_Internal([NotNull] List<Collider2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.OverlapList_Internal_Injected(intPtr, results);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009C9C File Offset: 0x00007E9C
		[NativeMethod("OverlapFilteredList_Binding")]
		private int OverlapFilteredList_Internal(ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.OverlapFilteredList_Internal_Injected(intPtr, ref contactFilter, results);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009CD0 File Offset: 0x00007ED0
		[NativeMethod("OverlapFromList_Binding")]
		private int OverlapFromList_Internal(Vector2 position, float angle, [NotNull] List<Collider2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.OverlapFromList_Internal_Injected(intPtr, ref position, angle, results);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009D08 File Offset: 0x00007F08
		[NativeMethod("OverlapFromFilteredList_Binding")]
		private int OverlapFromFilteredList_Internal(Vector2 position, float angle, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody2D.OverlapFromFilteredList_Internal_Injected(intPtr, ref position, angle, ref contactFilter, results);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00009D44 File Offset: 0x00007F44
		[Obsolete("OverlapCollider has been renamed to Overlap (UnityUpgradable) -> Overlap(*)", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public int OverlapCollider(ContactFilter2D contactFilter, [Out] Collider2D[] results)
		{
			return this.Overlap(contactFilter, results);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00009D60 File Offset: 0x00007F60
		[Obsolete("OverlapCollider has been renamed to Overlap (UnityUpgradable) -> Overlap(*)", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return this.Overlap(contactFilter, results);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00009D7C File Offset: 0x00007F7C
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00005DAF File Offset: 0x00003FAF
		[Obsolete("Rigidbody2D.fixedAngle is deprecated. Use Rigidbody2D.constraints instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public bool fixedAngle
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00009D90 File Offset: 0x00007F90
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00009DAB File Offset: 0x00007FAB
		[Obsolete("Please use Rigidbody2D.bodyType instead.", false)]
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool isKinematic
		{
			get
			{
				return this.bodyType == RigidbodyType2D.Kinematic;
			}
			set
			{
				this.bodyType = (value ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00009DBC File Offset: 0x00007FBC
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00009DC4 File Offset: 0x00007FC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody2D.linearDamping instead. (UnityUpgradable) -> linearDamping", false)]
		[ExcludeFromDocs]
		public float drag
		{
			get
			{
				return this.linearDamping;
			}
			set
			{
				this.linearDamping = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00009DCE File Offset: 0x00007FCE
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00009DD6 File Offset: 0x00007FD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody2D.angularDamping instead. (UnityUpgradable) -> angularDamping", false)]
		[ExcludeFromDocs]
		public float angularDrag
		{
			get
			{
				return this.angularDamping;
			}
			set
			{
				this.angularDamping = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009DE0 File Offset: 0x00007FE0
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00009DE8 File Offset: 0x00007FE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody2D.linearVelocity instead. (UnityUpgradable) -> linearVelocity", false)]
		[ExcludeFromDocs]
		public Vector2 velocity
		{
			get
			{
				return this.linearVelocity;
			}
			set
			{
				this.linearVelocity = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00009DF2 File Offset: 0x00007FF2
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00009DFA File Offset: 0x00007FFA
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody2D.linearVelocityX instead. (UnityUpgradable) -> linearVelocityX", false)]
		public float velocityX
		{
			get
			{
				return this.linearVelocityX;
			}
			set
			{
				this.linearVelocityX = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00009E04 File Offset: 0x00008004
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00009E0C File Offset: 0x0000800C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody2D.linearVelocityY instead. (UnityUpgradable) -> linearVelocityY", false)]
		[ExcludeFromDocs]
		public float velocityY
		{
			get
			{
				return this.linearVelocityY;
			}
			set
			{
				this.linearVelocityY = value;
			}
		}

		// Token: 0x06000355 RID: 853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_position_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000356 RID: 854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_position_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000357 RID: 855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_rotation_Injected(IntPtr _unity_self);

		// Token: 0x06000358 RID: 856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotation_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000359 RID: 857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotation_Angle_Injected(IntPtr _unity_self, float angle);

		// Token: 0x0600035A RID: 858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotation_Quaternion_Injected(IntPtr _unity_self, [In] ref Quaternion rotation);

		// Token: 0x0600035B RID: 859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MovePosition_Injected(IntPtr _unity_self, [In] ref Vector2 position);

		// Token: 0x0600035C RID: 860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveRotation_Angle_Injected(IntPtr _unity_self, float angle);

		// Token: 0x0600035D RID: 861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveRotation_Quaternion_Injected(IntPtr _unity_self, [In] ref Quaternion rotation);

		// Token: 0x0600035E RID: 862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MovePositionAndRotation_Injected(IntPtr _unity_self, [In] ref Vector2 position, float angle);

		// Token: 0x0600035F RID: 863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MovePositionAndRotation_Quaternion_Injected(IntPtr _unity_self, [In] ref Vector2 position, [In] ref Quaternion rotation);

		// Token: 0x06000360 RID: 864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slide_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 velocity, float deltaTime, [In] ref Rigidbody2D.SlideMovement slideMovement, out Rigidbody2D.SlideResults ret);

		// Token: 0x06000361 RID: 865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_linearVelocity_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000362 RID: 866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearVelocity_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000363 RID: 867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_linearVelocityX_Injected(IntPtr _unity_self);

		// Token: 0x06000364 RID: 868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearVelocityX_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000365 RID: 869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_linearVelocityY_Injected(IntPtr _unity_self);

		// Token: 0x06000366 RID: 870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearVelocityY_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000367 RID: 871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularVelocity_Injected(IntPtr _unity_self);

		// Token: 0x06000368 RID: 872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularVelocity_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000369 RID: 873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useAutoMass_Injected(IntPtr _unity_self);

		// Token: 0x0600036A RID: 874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useAutoMass_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600036B RID: 875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_mass_Injected(IntPtr _unity_self);

		// Token: 0x0600036C RID: 876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mass_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600036D RID: 877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sharedMaterial_Injected(IntPtr _unity_self);

		// Token: 0x0600036E RID: 878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sharedMaterial_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600036F RID: 879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_centerOfMass_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000370 RID: 880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_centerOfMass_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000371 RID: 881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldCenterOfMass_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000372 RID: 882
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_inertia_Injected(IntPtr _unity_self);

		// Token: 0x06000373 RID: 883
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_inertia_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000374 RID: 884
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_linearDamping_Injected(IntPtr _unity_self);

		// Token: 0x06000375 RID: 885
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearDamping_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000376 RID: 886
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularDamping_Injected(IntPtr _unity_self);

		// Token: 0x06000377 RID: 887
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularDamping_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000378 RID: 888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_gravityScale_Injected(IntPtr _unity_self);

		// Token: 0x06000379 RID: 889
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gravityScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600037A RID: 890
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RigidbodyType2D get_bodyType_Injected(IntPtr _unity_self);

		// Token: 0x0600037B RID: 891
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bodyType_Injected(IntPtr _unity_self, RigidbodyType2D value);

		// Token: 0x0600037C RID: 892
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDragBehaviour_Injected(IntPtr _unity_self, bool dragged);

		// Token: 0x0600037D RID: 893
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useFullKinematicContacts_Injected(IntPtr _unity_self);

		// Token: 0x0600037E RID: 894
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useFullKinematicContacts_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600037F RID: 895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_freezeRotation_Injected(IntPtr _unity_self);

		// Token: 0x06000380 RID: 896
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_freezeRotation_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000381 RID: 897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RigidbodyConstraints2D get_constraints_Injected(IntPtr _unity_self);

		// Token: 0x06000382 RID: 898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_constraints_Injected(IntPtr _unity_self, RigidbodyConstraints2D value);

		// Token: 0x06000383 RID: 899
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsSleeping_Injected(IntPtr _unity_self);

		// Token: 0x06000384 RID: 900
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAwake_Injected(IntPtr _unity_self);

		// Token: 0x06000385 RID: 901
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Sleep_Injected(IntPtr _unity_self);

		// Token: 0x06000386 RID: 902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WakeUp_Injected(IntPtr _unity_self);

		// Token: 0x06000387 RID: 903
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_simulated_Injected(IntPtr _unity_self);

		// Token: 0x06000388 RID: 904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_simulated_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000389 RID: 905
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RigidbodyInterpolation2D get_interpolation_Injected(IntPtr _unity_self);

		// Token: 0x0600038A RID: 906
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_interpolation_Injected(IntPtr _unity_self, RigidbodyInterpolation2D value);

		// Token: 0x0600038B RID: 907
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RigidbodySleepMode2D get_sleepMode_Injected(IntPtr _unity_self);

		// Token: 0x0600038C RID: 908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sleepMode_Injected(IntPtr _unity_self, RigidbodySleepMode2D value);

		// Token: 0x0600038D RID: 909
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CollisionDetectionMode2D get_collisionDetectionMode_Injected(IntPtr _unity_self);

		// Token: 0x0600038E RID: 910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_collisionDetectionMode_Injected(IntPtr _unity_self, CollisionDetectionMode2D value);

		// Token: 0x0600038F RID: 911
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAttachedColliderCount_Internal_Injected(IntPtr _unity_self, bool findTriggers);

		// Token: 0x06000390 RID: 912
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_totalForce_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000391 RID: 913
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_totalForce_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000392 RID: 914
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_totalTorque_Injected(IntPtr _unity_self);

		// Token: 0x06000393 RID: 915
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_totalTorque_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000394 RID: 916
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_excludeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000395 RID: 917
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_excludeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000396 RID: 918
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_includeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000397 RID: 919
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_includeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000398 RID: 920
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localToWorldMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06000399 RID: 921
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_Injected(IntPtr _unity_self, IntPtr collider);

		// Token: 0x0600039A RID: 922
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_OtherColliderWithFilter_Internal_Injected(IntPtr _unity_self, IntPtr collider, [In] ref ContactFilter2D contactFilter);

		// Token: 0x0600039B RID: 923
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_AnyColliderWithFilter_Internal_Injected(IntPtr _unity_self, [In] ref ContactFilter2D contactFilter);

		// Token: 0x0600039C RID: 924
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool OverlapPoint_Injected(IntPtr _unity_self, [In] ref Vector2 point);

		// Token: 0x0600039D RID: 925
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Distance_Internal_Injected(IntPtr _unity_self, IntPtr collider, out ColliderDistance2D ret);

		// Token: 0x0600039E RID: 926
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DistanceFrom_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 thisPosition, float thisAngle, IntPtr collider, [In] ref Vector2 position, float angle, out ColliderDistance2D ret);

		// Token: 0x0600039F RID: 927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddForce_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060003A0 RID: 928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddRelativeForce_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060003A1 RID: 929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddForceAtPosition_Injected(IntPtr _unity_self, [In] ref Vector2 force, [In] ref Vector2 position, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060003A2 RID: 930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddTorque_Injected(IntPtr _unity_self, float torque, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x060003A3 RID: 931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPoint_Injected(IntPtr _unity_self, [In] ref Vector2 point, out Vector2 ret);

		// Token: 0x060003A4 RID: 932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRelativePoint_Injected(IntPtr _unity_self, [In] ref Vector2 relativePoint, out Vector2 ret);

		// Token: 0x060003A5 RID: 933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector_Injected(IntPtr _unity_self, [In] ref Vector2 vector, out Vector2 ret);

		// Token: 0x060003A6 RID: 934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRelativeVector_Injected(IntPtr _unity_self, [In] ref Vector2 relativeVector, out Vector2 ret);

		// Token: 0x060003A7 RID: 935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPointVelocity_Injected(IntPtr _unity_self, [In] ref Vector2 point, out Vector2 ret);

		// Token: 0x060003A8 RID: 936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRelativePointVelocity_Injected(IntPtr _unity_self, [In] ref Vector2 relativePoint, out Vector2 ret);

		// Token: 0x060003A9 RID: 937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAttachedCollidersArray_Internal_Injected(IntPtr _unity_self, Collider2D[] results, bool findTriggers);

		// Token: 0x060003AA RID: 938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAttachedCollidersList_Internal_Injected(IntPtr _unity_self, List<Collider2D> results, bool findTriggers);

		// Token: 0x060003AB RID: 939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetShapes_Internal_Injected(IntPtr _unity_self, ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState);

		// Token: 0x060003AC RID: 940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastArray_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, bool checkIgnoreColliders, ref ManagedSpanWrapper results);

		// Token: 0x060003AD RID: 941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastList_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, bool checkIgnoreColliders, ref BlittableListWrapper results);

		// Token: 0x060003AE RID: 942
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastFilteredArray_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, bool checkIgnoreColliders, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x060003AF RID: 943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastFilteredList_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, bool checkIgnoreColliders, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x060003B0 RID: 944
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastFrom_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 position, float angle, [In] ref Vector2 direction, float distance, bool checkIgnoreColliders, ref BlittableListWrapper results);

		// Token: 0x060003B1 RID: 945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastFromFiltered_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 position, float angle, [In] ref Vector2 direction, float distance, bool checkIgnoreColliders, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x060003B2 RID: 946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapArray_Internal_Injected(IntPtr _unity_self, [In] ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x060003B3 RID: 947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapList_Internal_Injected(IntPtr _unity_self, List<Collider2D> results);

		// Token: 0x060003B4 RID: 948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapFilteredList_Internal_Injected(IntPtr _unity_self, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x060003B5 RID: 949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapFromList_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 position, float angle, List<Collider2D> results);

		// Token: 0x060003B6 RID: 950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapFromFilteredList_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 position, float angle, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x02000023 RID: 35
		[NativeHeader(Header = "Modules/Physics2D/Public/Rigidbody2D.h")]
		[Serializable]
		public struct SlideMovement
		{
			// Token: 0x060003B7 RID: 951 RVA: 0x00009E20 File Offset: 0x00008020
			public SlideMovement()
			{
				this.maxIterations = 3;
				this.surfaceSlideAngle = 90f;
				this.gravitySlipAngle = 90f;
				this.surfaceUp = Vector2.up;
				this.surfaceAnchor = Vector2.down;
				this.gravity = new Vector2(0f, -9.81f);
				this.startPosition = Vector2.zero;
				this.selectedCollider = null;
				this.useStartPosition = false;
				this.useNoMove = false;
				this.useSimulationMove = false;
				this.useAttachedTriggers = false;
				this.useLayerMask = false;
				this.layerMask = -1;
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x00009EC5 File Offset: 0x000080C5
			// (set) Token: 0x060003B9 RID: 953 RVA: 0x00009ECD File Offset: 0x000080CD
			public int maxIterations { readonly get; set; }

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060003BA RID: 954 RVA: 0x00009ED6 File Offset: 0x000080D6
			// (set) Token: 0x060003BB RID: 955 RVA: 0x00009EDE File Offset: 0x000080DE
			public float surfaceSlideAngle { readonly get; set; }

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060003BC RID: 956 RVA: 0x00009EE7 File Offset: 0x000080E7
			// (set) Token: 0x060003BD RID: 957 RVA: 0x00009EEF File Offset: 0x000080EF
			public float gravitySlipAngle { readonly get; set; }

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060003BE RID: 958 RVA: 0x00009EF8 File Offset: 0x000080F8
			// (set) Token: 0x060003BF RID: 959 RVA: 0x00009F00 File Offset: 0x00008100
			public Vector2 surfaceUp { readonly get; set; }

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x00009F09 File Offset: 0x00008109
			// (set) Token: 0x060003C1 RID: 961 RVA: 0x00009F11 File Offset: 0x00008111
			public Vector2 surfaceAnchor { readonly get; set; }

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x00009F1A File Offset: 0x0000811A
			// (set) Token: 0x060003C3 RID: 963 RVA: 0x00009F22 File Offset: 0x00008122
			public Vector2 gravity { readonly get; set; }

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060003C4 RID: 964 RVA: 0x00009F2B File Offset: 0x0000812B
			// (set) Token: 0x060003C5 RID: 965 RVA: 0x00009F33 File Offset: 0x00008133
			public Vector2 startPosition { readonly get; set; }

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060003C6 RID: 966 RVA: 0x00009F3C File Offset: 0x0000813C
			// (set) Token: 0x060003C7 RID: 967 RVA: 0x00009F44 File Offset: 0x00008144
			public Collider2D selectedCollider { readonly get; set; }

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x00009F4D File Offset: 0x0000814D
			// (set) Token: 0x060003C9 RID: 969 RVA: 0x00009F55 File Offset: 0x00008155
			public LayerMask layerMask { readonly get; set; }

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060003CA RID: 970 RVA: 0x00009F5E File Offset: 0x0000815E
			// (set) Token: 0x060003CB RID: 971 RVA: 0x00009F66 File Offset: 0x00008166
			public bool useLayerMask { readonly get; set; }

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060003CC RID: 972 RVA: 0x00009F6F File Offset: 0x0000816F
			// (set) Token: 0x060003CD RID: 973 RVA: 0x00009F77 File Offset: 0x00008177
			public bool useStartPosition { readonly get; set; }

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060003CE RID: 974 RVA: 0x00009F80 File Offset: 0x00008180
			// (set) Token: 0x060003CF RID: 975 RVA: 0x00009F88 File Offset: 0x00008188
			public bool useNoMove { readonly get; set; }

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060003D0 RID: 976 RVA: 0x00009F91 File Offset: 0x00008191
			// (set) Token: 0x060003D1 RID: 977 RVA: 0x00009F99 File Offset: 0x00008199
			public bool useSimulationMove { readonly get; set; }

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060003D2 RID: 978 RVA: 0x00009FA2 File Offset: 0x000081A2
			// (set) Token: 0x060003D3 RID: 979 RVA: 0x00009FAA File Offset: 0x000081AA
			public bool useAttachedTriggers { readonly get; set; }

			// Token: 0x060003D4 RID: 980 RVA: 0x00009FB3 File Offset: 0x000081B3
			public void SetLayerMask(LayerMask mask)
			{
				this.layerMask = mask;
				this.useLayerMask = true;
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x00009FC6 File Offset: 0x000081C6
			public void SetStartPosition(Vector2 position)
			{
				this.startPosition = position;
				this.useStartPosition = true;
			}
		}

		// Token: 0x02000024 RID: 36
		[NativeHeader(Header = "Modules/Physics2D/Public/Rigidbody2D.h")]
		[Serializable]
		public struct SlideResults
		{
			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060003D6 RID: 982 RVA: 0x00009FD9 File Offset: 0x000081D9
			// (set) Token: 0x060003D7 RID: 983 RVA: 0x00009FE1 File Offset: 0x000081E1
			public Vector2 remainingVelocity { readonly get; set; }

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060003D8 RID: 984 RVA: 0x00009FEA File Offset: 0x000081EA
			// (set) Token: 0x060003D9 RID: 985 RVA: 0x00009FF2 File Offset: 0x000081F2
			public Vector2 position { readonly get; set; }

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060003DA RID: 986 RVA: 0x00009FFB File Offset: 0x000081FB
			// (set) Token: 0x060003DB RID: 987 RVA: 0x0000A003 File Offset: 0x00008203
			public int iterationsUsed { readonly get; set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060003DC RID: 988 RVA: 0x0000A00C File Offset: 0x0000820C
			// (set) Token: 0x060003DD RID: 989 RVA: 0x0000A014 File Offset: 0x00008214
			public RaycastHit2D slideHit { readonly get; set; }

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0000A01D File Offset: 0x0000821D
			// (set) Token: 0x060003DF RID: 991 RVA: 0x0000A025 File Offset: 0x00008225
			public RaycastHit2D surfaceHit { readonly get; set; }
		}
	}
}
