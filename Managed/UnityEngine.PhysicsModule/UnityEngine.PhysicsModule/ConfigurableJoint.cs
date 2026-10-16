using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[NativeClass("Unity::ConfigurableJoint")]
	[RequireComponent(typeof(Rigidbody))]
	[NativeHeader("Modules/Physics/ConfigurableJoint.h")]
	public class ConfigurableJoint : Joint
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000053DC File Offset: 0x000035DC
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00005404 File Offset: 0x00003604
		public Vector3 secondaryAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ConfigurableJoint.get_secondaryAxis_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_secondaryAxis_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00005428 File Offset: 0x00003628
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000544C File Offset: 0x0000364C
		public ConfigurableJointMotion xMotion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_xMotion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_xMotion_Injected(intPtr, value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00005470 File Offset: 0x00003670
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00005494 File Offset: 0x00003694
		public ConfigurableJointMotion yMotion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_yMotion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_yMotion_Injected(intPtr, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000054B8 File Offset: 0x000036B8
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000054DC File Offset: 0x000036DC
		public ConfigurableJointMotion zMotion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_zMotion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_zMotion_Injected(intPtr, value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00005500 File Offset: 0x00003700
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00005524 File Offset: 0x00003724
		public ConfigurableJointMotion angularXMotion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_angularXMotion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularXMotion_Injected(intPtr, value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005548 File Offset: 0x00003748
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000556C File Offset: 0x0000376C
		public ConfigurableJointMotion angularYMotion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_angularYMotion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularYMotion_Injected(intPtr, value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00005590 File Offset: 0x00003790
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000055B4 File Offset: 0x000037B4
		public ConfigurableJointMotion angularZMotion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_angularZMotion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularZMotion_Injected(intPtr, value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000055D8 File Offset: 0x000037D8
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00005600 File Offset: 0x00003800
		public SoftJointLimitSpring linearLimitSpring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimitSpring softJointLimitSpring;
				ConfigurableJoint.get_linearLimitSpring_Injected(intPtr, out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_linearLimitSpring_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00005624 File Offset: 0x00003824
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000564C File Offset: 0x0000384C
		public SoftJointLimitSpring angularXLimitSpring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimitSpring softJointLimitSpring;
				ConfigurableJoint.get_angularXLimitSpring_Injected(intPtr, out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularXLimitSpring_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00005670 File Offset: 0x00003870
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00005698 File Offset: 0x00003898
		public SoftJointLimitSpring angularYZLimitSpring
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimitSpring softJointLimitSpring;
				ConfigurableJoint.get_angularYZLimitSpring_Injected(intPtr, out softJointLimitSpring);
				return softJointLimitSpring;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularYZLimitSpring_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000056BC File Offset: 0x000038BC
		// (set) Token: 0x06000241 RID: 577 RVA: 0x000056E4 File Offset: 0x000038E4
		public SoftJointLimit linearLimit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				ConfigurableJoint.get_linearLimit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_linearLimit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00005708 File Offset: 0x00003908
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00005730 File Offset: 0x00003930
		public SoftJointLimit lowAngularXLimit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				ConfigurableJoint.get_lowAngularXLimit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_lowAngularXLimit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00005754 File Offset: 0x00003954
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000577C File Offset: 0x0000397C
		public SoftJointLimit highAngularXLimit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				ConfigurableJoint.get_highAngularXLimit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_highAngularXLimit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000057A0 File Offset: 0x000039A0
		// (set) Token: 0x06000247 RID: 583 RVA: 0x000057C8 File Offset: 0x000039C8
		public SoftJointLimit angularYLimit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				ConfigurableJoint.get_angularYLimit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularYLimit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000057EC File Offset: 0x000039EC
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00005814 File Offset: 0x00003A14
		public SoftJointLimit angularZLimit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SoftJointLimit softJointLimit;
				ConfigurableJoint.get_angularZLimit_Injected(intPtr, out softJointLimit);
				return softJointLimit;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularZLimit_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00005838 File Offset: 0x00003A38
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00005860 File Offset: 0x00003A60
		public Vector3 targetPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ConfigurableJoint.get_targetPosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_targetPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00005884 File Offset: 0x00003A84
		// (set) Token: 0x0600024D RID: 589 RVA: 0x000058AC File Offset: 0x00003AAC
		public Vector3 targetVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ConfigurableJoint.get_targetVelocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_targetVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000058D0 File Offset: 0x00003AD0
		// (set) Token: 0x0600024F RID: 591 RVA: 0x000058F8 File Offset: 0x00003AF8
		public JointDrive xDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointDrive jointDrive;
				ConfigurableJoint.get_xDrive_Injected(intPtr, out jointDrive);
				return jointDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_xDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000591C File Offset: 0x00003B1C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00005944 File Offset: 0x00003B44
		public JointDrive yDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointDrive jointDrive;
				ConfigurableJoint.get_yDrive_Injected(intPtr, out jointDrive);
				return jointDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_yDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00005968 File Offset: 0x00003B68
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00005990 File Offset: 0x00003B90
		public JointDrive zDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointDrive jointDrive;
				ConfigurableJoint.get_zDrive_Injected(intPtr, out jointDrive);
				return jointDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_zDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000059B4 File Offset: 0x00003BB4
		// (set) Token: 0x06000255 RID: 597 RVA: 0x000059DC File Offset: 0x00003BDC
		public Quaternion targetRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				ConfigurableJoint.get_targetRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_targetRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00005A00 File Offset: 0x00003C00
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00005A28 File Offset: 0x00003C28
		public Vector3 targetAngularVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ConfigurableJoint.get_targetAngularVelocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_targetAngularVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00005A4C File Offset: 0x00003C4C
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00005A70 File Offset: 0x00003C70
		public RotationDriveMode rotationDriveMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_rotationDriveMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_rotationDriveMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00005A94 File Offset: 0x00003C94
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00005ABC File Offset: 0x00003CBC
		public JointDrive angularXDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointDrive jointDrive;
				ConfigurableJoint.get_angularXDrive_Injected(intPtr, out jointDrive);
				return jointDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularXDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00005AE0 File Offset: 0x00003CE0
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00005B08 File Offset: 0x00003D08
		public JointDrive angularYZDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointDrive jointDrive;
				ConfigurableJoint.get_angularYZDrive_Injected(intPtr, out jointDrive);
				return jointDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_angularYZDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00005B2C File Offset: 0x00003D2C
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00005B54 File Offset: 0x00003D54
		public JointDrive slerpDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				JointDrive jointDrive;
				ConfigurableJoint.get_slerpDrive_Injected(intPtr, out jointDrive);
				return jointDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_slerpDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00005B78 File Offset: 0x00003D78
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00005B9C File Offset: 0x00003D9C
		public JointProjectionMode projectionMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_projectionMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_projectionMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00005BC0 File Offset: 0x00003DC0
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public float projectionDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_projectionDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_projectionDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00005C08 File Offset: 0x00003E08
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00005C2C File Offset: 0x00003E2C
		public float projectionAngle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_projectionAngle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_projectionAngle_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00005C50 File Offset: 0x00003E50
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00005C74 File Offset: 0x00003E74
		public bool configuredInWorldSpace
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_configuredInWorldSpace_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_configuredInWorldSpace_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00005C98 File Offset: 0x00003E98
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00005CBC File Offset: 0x00003EBC
		public bool swapBodies
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ConfigurableJoint.get_swapBodies_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ConfigurableJoint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ConfigurableJoint.set_swapBodies_Injected(intPtr, value);
			}
		}

		// Token: 0x0600026B RID: 619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_secondaryAxis_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600026C RID: 620
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_secondaryAxis_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600026D RID: 621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ConfigurableJointMotion get_xMotion_Injected(IntPtr _unity_self);

		// Token: 0x0600026E RID: 622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_xMotion_Injected(IntPtr _unity_self, ConfigurableJointMotion value);

		// Token: 0x0600026F RID: 623
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ConfigurableJointMotion get_yMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000270 RID: 624
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_yMotion_Injected(IntPtr _unity_self, ConfigurableJointMotion value);

		// Token: 0x06000271 RID: 625
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ConfigurableJointMotion get_zMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000272 RID: 626
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_zMotion_Injected(IntPtr _unity_self, ConfigurableJointMotion value);

		// Token: 0x06000273 RID: 627
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ConfigurableJointMotion get_angularXMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000274 RID: 628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularXMotion_Injected(IntPtr _unity_self, ConfigurableJointMotion value);

		// Token: 0x06000275 RID: 629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ConfigurableJointMotion get_angularYMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000276 RID: 630
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularYMotion_Injected(IntPtr _unity_self, ConfigurableJointMotion value);

		// Token: 0x06000277 RID: 631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ConfigurableJointMotion get_angularZMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000278 RID: 632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularZMotion_Injected(IntPtr _unity_self, ConfigurableJointMotion value);

		// Token: 0x06000279 RID: 633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_linearLimitSpring_Injected(IntPtr _unity_self, out SoftJointLimitSpring ret);

		// Token: 0x0600027A RID: 634
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearLimitSpring_Injected(IntPtr _unity_self, [In] ref SoftJointLimitSpring value);

		// Token: 0x0600027B RID: 635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularXLimitSpring_Injected(IntPtr _unity_self, out SoftJointLimitSpring ret);

		// Token: 0x0600027C RID: 636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularXLimitSpring_Injected(IntPtr _unity_self, [In] ref SoftJointLimitSpring value);

		// Token: 0x0600027D RID: 637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularYZLimitSpring_Injected(IntPtr _unity_self, out SoftJointLimitSpring ret);

		// Token: 0x0600027E RID: 638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularYZLimitSpring_Injected(IntPtr _unity_self, [In] ref SoftJointLimitSpring value);

		// Token: 0x0600027F RID: 639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_linearLimit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x06000280 RID: 640
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearLimit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x06000281 RID: 641
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_lowAngularXLimit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x06000282 RID: 642
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lowAngularXLimit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x06000283 RID: 643
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_highAngularXLimit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x06000284 RID: 644
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_highAngularXLimit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x06000285 RID: 645
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularYLimit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x06000286 RID: 646
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularYLimit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x06000287 RID: 647
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularZLimit_Injected(IntPtr _unity_self, out SoftJointLimit ret);

		// Token: 0x06000288 RID: 648
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularZLimit_Injected(IntPtr _unity_self, [In] ref SoftJointLimit value);

		// Token: 0x06000289 RID: 649
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600028A RID: 650
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetPosition_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600028B RID: 651
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600028C RID: 652
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600028D RID: 653
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_xDrive_Injected(IntPtr _unity_self, out JointDrive ret);

		// Token: 0x0600028E RID: 654
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_xDrive_Injected(IntPtr _unity_self, [In] ref JointDrive value);

		// Token: 0x0600028F RID: 655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_yDrive_Injected(IntPtr _unity_self, out JointDrive ret);

		// Token: 0x06000290 RID: 656
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_yDrive_Injected(IntPtr _unity_self, [In] ref JointDrive value);

		// Token: 0x06000291 RID: 657
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_zDrive_Injected(IntPtr _unity_self, out JointDrive ret);

		// Token: 0x06000292 RID: 658
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_zDrive_Injected(IntPtr _unity_self, [In] ref JointDrive value);

		// Token: 0x06000293 RID: 659
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000294 RID: 660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x06000295 RID: 661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetAngularVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000296 RID: 662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetAngularVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000297 RID: 663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RotationDriveMode get_rotationDriveMode_Injected(IntPtr _unity_self);

		// Token: 0x06000298 RID: 664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationDriveMode_Injected(IntPtr _unity_self, RotationDriveMode value);

		// Token: 0x06000299 RID: 665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularXDrive_Injected(IntPtr _unity_self, out JointDrive ret);

		// Token: 0x0600029A RID: 666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularXDrive_Injected(IntPtr _unity_self, [In] ref JointDrive value);

		// Token: 0x0600029B RID: 667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularYZDrive_Injected(IntPtr _unity_self, out JointDrive ret);

		// Token: 0x0600029C RID: 668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularYZDrive_Injected(IntPtr _unity_self, [In] ref JointDrive value);

		// Token: 0x0600029D RID: 669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_slerpDrive_Injected(IntPtr _unity_self, out JointDrive ret);

		// Token: 0x0600029E RID: 670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_slerpDrive_Injected(IntPtr _unity_self, [In] ref JointDrive value);

		// Token: 0x0600029F RID: 671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern JointProjectionMode get_projectionMode_Injected(IntPtr _unity_self);

		// Token: 0x060002A0 RID: 672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_projectionMode_Injected(IntPtr _unity_self, JointProjectionMode value);

		// Token: 0x060002A1 RID: 673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_projectionDistance_Injected(IntPtr _unity_self);

		// Token: 0x060002A2 RID: 674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_projectionDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060002A3 RID: 675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_projectionAngle_Injected(IntPtr _unity_self);

		// Token: 0x060002A4 RID: 676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_projectionAngle_Injected(IntPtr _unity_self, float value);

		// Token: 0x060002A5 RID: 677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_configuredInWorldSpace_Injected(IntPtr _unity_self);

		// Token: 0x060002A6 RID: 678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_configuredInWorldSpace_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060002A7 RID: 679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_swapBodies_Injected(IntPtr _unity_self);

		// Token: 0x060002A8 RID: 680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_swapBodies_Injected(IntPtr _unity_self, bool value);
	}
}
