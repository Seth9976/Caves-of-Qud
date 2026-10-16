using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200005F RID: 95
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemForceFieldManager.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemForceField.h")]
	public class ParticleSystemForceField : Behaviour
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x000081D0 File Offset: 0x000063D0
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x000081F4 File Offset: 0x000063F4
		[NativeName("ForceShape")]
		public ParticleSystemForceFieldShape shape
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemForceField.get_shape_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_shape_Injected(intPtr, value);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00008218 File Offset: 0x00006418
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0000823C File Offset: 0x0000643C
		public float startRange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemForceField.get_startRange_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_startRange_Injected(intPtr, value);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00008260 File Offset: 0x00006460
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x00008284 File Offset: 0x00006484
		public float endRange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemForceField.get_endRange_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_endRange_Injected(intPtr, value);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x000082A8 File Offset: 0x000064A8
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x000082CC File Offset: 0x000064CC
		public float length
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemForceField.get_length_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_length_Injected(intPtr, value);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x000082F0 File Offset: 0x000064F0
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x00008314 File Offset: 0x00006514
		public float gravityFocus
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemForceField.get_gravityFocus_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_gravityFocus_Injected(intPtr, value);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00008338 File Offset: 0x00006538
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x00008360 File Offset: 0x00006560
		public Vector2 rotationRandomness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				ParticleSystemForceField.get_rotationRandomness_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_rotationRandomness_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00008384 File Offset: 0x00006584
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x000083A8 File Offset: 0x000065A8
		public bool multiplyDragByParticleSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemForceField.get_multiplyDragByParticleSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_multiplyDragByParticleSize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x000083CC File Offset: 0x000065CC
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x000083F0 File Offset: 0x000065F0
		public bool multiplyDragByParticleVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemForceField.get_multiplyDragByParticleVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_multiplyDragByParticleVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00008414 File Offset: 0x00006614
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000843C File Offset: 0x0000663C
		public Texture3D vectorField
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture3D>(ParticleSystemForceField.get_vectorField_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_vectorField_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture3D>(value));
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00008464 File Offset: 0x00006664
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x00008471 File Offset: 0x00006671
		public ParticleSystem.MinMaxCurve directionX
		{
			get
			{
				return this.directionXBlittable;
			}
			set
			{
				this.directionXBlittable = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00008480 File Offset: 0x00006680
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x000084A8 File Offset: 0x000066A8
		[NativeName("DirectionX")]
		private ParticleSystem.MinMaxCurveBlittable directionXBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_directionXBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_directionXBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x000084CC File Offset: 0x000066CC
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x000084D9 File Offset: 0x000066D9
		public ParticleSystem.MinMaxCurve directionY
		{
			get
			{
				return this.directionYBlittable;
			}
			set
			{
				this.directionYBlittable = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000084E8 File Offset: 0x000066E8
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00008510 File Offset: 0x00006710
		[NativeName("DirectionY")]
		private ParticleSystem.MinMaxCurveBlittable directionYBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_directionYBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_directionYBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00008534 File Offset: 0x00006734
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00008541 File Offset: 0x00006741
		public ParticleSystem.MinMaxCurve directionZ
		{
			get
			{
				return this.directionZBlittable;
			}
			set
			{
				this.directionZBlittable = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00008550 File Offset: 0x00006750
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00008578 File Offset: 0x00006778
		[NativeName("DirectionZ")]
		private ParticleSystem.MinMaxCurveBlittable directionZBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_directionZBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_directionZBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000859C File Offset: 0x0000679C
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x000085A9 File Offset: 0x000067A9
		public ParticleSystem.MinMaxCurve gravity
		{
			get
			{
				return this.gravityBlittable;
			}
			set
			{
				this.gravityBlittable = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x000085B8 File Offset: 0x000067B8
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x000085E0 File Offset: 0x000067E0
		[NativeName("Gravity")]
		private ParticleSystem.MinMaxCurveBlittable gravityBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_gravityBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_gravityBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00008604 File Offset: 0x00006804
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00008611 File Offset: 0x00006811
		public ParticleSystem.MinMaxCurve rotationSpeed
		{
			get
			{
				return this.rotationSpeedBlittable;
			}
			set
			{
				this.rotationSpeedBlittable = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00008620 File Offset: 0x00006820
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00008648 File Offset: 0x00006848
		[NativeName("RotationSpeed")]
		private ParticleSystem.MinMaxCurveBlittable rotationSpeedBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_rotationSpeedBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_rotationSpeedBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000866C File Offset: 0x0000686C
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00008679 File Offset: 0x00006879
		public ParticleSystem.MinMaxCurve rotationAttraction
		{
			get
			{
				return this.rotationAttractionBlittable;
			}
			set
			{
				this.rotationAttractionBlittable = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00008688 File Offset: 0x00006888
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x000086B0 File Offset: 0x000068B0
		[NativeName("RotationAttraction")]
		private ParticleSystem.MinMaxCurveBlittable rotationAttractionBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_rotationAttractionBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_rotationAttractionBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x000086D4 File Offset: 0x000068D4
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x000086E1 File Offset: 0x000068E1
		public ParticleSystem.MinMaxCurve drag
		{
			get
			{
				return this.dragBlittable;
			}
			set
			{
				this.dragBlittable = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x000086F0 File Offset: 0x000068F0
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x00008718 File Offset: 0x00006918
		[NativeName("Drag")]
		private ParticleSystem.MinMaxCurveBlittable dragBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_dragBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_dragBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000873C File Offset: 0x0000693C
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x00008749 File Offset: 0x00006949
		public ParticleSystem.MinMaxCurve vectorFieldSpeed
		{
			get
			{
				return this.vectorFieldSpeedBlittable;
			}
			set
			{
				this.vectorFieldSpeedBlittable = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00008758 File Offset: 0x00006958
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x00008780 File Offset: 0x00006980
		[NativeName("VectorFieldSpeed")]
		private ParticleSystem.MinMaxCurveBlittable vectorFieldSpeedBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_vectorFieldSpeedBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_vectorFieldSpeedBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x000087A4 File Offset: 0x000069A4
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x000087B1 File Offset: 0x000069B1
		public ParticleSystem.MinMaxCurve vectorFieldAttraction
		{
			get
			{
				return this.vectorFieldAttractionBlittable;
			}
			set
			{
				this.vectorFieldAttractionBlittable = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000087C0 File Offset: 0x000069C0
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x000087E8 File Offset: 0x000069E8
		[NativeName("VectorFieldAttraction")]
		private ParticleSystem.MinMaxCurveBlittable vectorFieldAttractionBlittable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystemForceField.get_vectorFieldAttractionBlittable_Injected(intPtr, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemForceField.set_vectorFieldAttractionBlittable_Injected(intPtr, ref value);
			}
		}

		// Token: 0x0600069F RID: 1695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParticleSystemForceFieldShape get_shape_Injected(IntPtr _unity_self);

		// Token: 0x060006A0 RID: 1696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shape_Injected(IntPtr _unity_self, ParticleSystemForceFieldShape value);

		// Token: 0x060006A1 RID: 1697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_startRange_Injected(IntPtr _unity_self);

		// Token: 0x060006A2 RID: 1698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startRange_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006A3 RID: 1699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_endRange_Injected(IntPtr _unity_self);

		// Token: 0x060006A4 RID: 1700
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_endRange_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006A5 RID: 1701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_length_Injected(IntPtr _unity_self);

		// Token: 0x060006A6 RID: 1702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_length_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006A7 RID: 1703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_gravityFocus_Injected(IntPtr _unity_self);

		// Token: 0x060006A8 RID: 1704
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gravityFocus_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006A9 RID: 1705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationRandomness_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060006AA RID: 1706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationRandomness_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060006AB RID: 1707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_multiplyDragByParticleSize_Injected(IntPtr _unity_self);

		// Token: 0x060006AC RID: 1708
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_multiplyDragByParticleSize_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006AD RID: 1709
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_multiplyDragByParticleVelocity_Injected(IntPtr _unity_self);

		// Token: 0x060006AE RID: 1710
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_multiplyDragByParticleVelocity_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060006AF RID: 1711
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_vectorField_Injected(IntPtr _unity_self);

		// Token: 0x060006B0 RID: 1712
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vectorField_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060006B1 RID: 1713
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_directionXBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006B2 RID: 1714
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_directionXBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

		// Token: 0x060006B3 RID: 1715
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_directionYBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006B4 RID: 1716
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_directionYBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

		// Token: 0x060006B5 RID: 1717
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_directionZBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006B6 RID: 1718
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_directionZBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

		// Token: 0x060006B7 RID: 1719
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_gravityBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006B8 RID: 1720
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gravityBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

		// Token: 0x060006B9 RID: 1721
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationSpeedBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006BA RID: 1722
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationSpeedBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

		// Token: 0x060006BB RID: 1723
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationAttractionBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006BC RID: 1724
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationAttractionBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

		// Token: 0x060006BD RID: 1725
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_dragBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006BE RID: 1726
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dragBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

		// Token: 0x060006BF RID: 1727
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_vectorFieldSpeedBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006C0 RID: 1728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vectorFieldSpeedBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

		// Token: 0x060006C1 RID: 1729
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_vectorFieldAttractionBlittable_Injected(IntPtr _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

		// Token: 0x060006C2 RID: 1730
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vectorFieldAttractionBlittable_Injected(IntPtr _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);
	}
}
