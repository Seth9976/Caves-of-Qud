using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000076 RID: 118
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/MuscleHandle.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	public struct MuscleHandle
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0000DCC5 File Offset: 0x0000BEC5
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0000DCCD File Offset: 0x0000BECD
		public HumanPartDof humanPartDof { readonly get; private set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0000DCD6 File Offset: 0x0000BED6
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		public int dof { readonly get; private set; }

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000DCE7 File Offset: 0x0000BEE7
		public MuscleHandle(BodyDof bodyDof)
		{
			this.humanPartDof = HumanPartDof.Body;
			this.dof = (int)bodyDof;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000DCFA File Offset: 0x0000BEFA
		public MuscleHandle(HeadDof headDof)
		{
			this.humanPartDof = HumanPartDof.Head;
			this.dof = (int)headDof;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000DD10 File Offset: 0x0000BF10
		public MuscleHandle(HumanPartDof partDof, LegDof legDof)
		{
			bool flag = partDof != HumanPartDof.LeftLeg && partDof != HumanPartDof.RightLeg;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a leg, please use either HumanPartDof.LeftLeg or HumanPartDof.RightLeg.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)legDof;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public MuscleHandle(HumanPartDof partDof, ArmDof armDof)
		{
			bool flag = partDof != HumanPartDof.LeftArm && partDof != HumanPartDof.RightArm;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for an arm, please use either HumanPartDof.LeftArm or HumanPartDof.RightArm.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)armDof;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public MuscleHandle(HumanPartDof partDof, FingerDof fingerDof)
		{
			bool flag = partDof < HumanPartDof.LeftThumb || partDof > HumanPartDof.RightLittle;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a finger.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)fingerDof;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		public string name
		{
			get
			{
				return this.GetName();
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public static int muscleHandleCount
		{
			get
			{
				return MuscleHandle.GetMuscleHandleCount();
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		public unsafe static void GetMuscleHandles([NotNull] [Out] MuscleHandle[] muscleHandles)
		{
			if (muscleHandles == null)
			{
				ThrowHelper.ThrowArgumentNullException(muscleHandles, "muscleHandles");
			}
			try
			{
				fixed (MuscleHandle[] array = muscleHandles)
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					MuscleHandle.GetMuscleHandles_Injected(out blittableArrayWrapper);
				}
			}
			finally
			{
				MuscleHandle[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<MuscleHandle>(ref array);
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000DE50 File Offset: 0x0000C050
		private string GetName()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				MuscleHandle.GetName_Injected(ref this, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060007AB RID: 1963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMuscleHandleCount();

		// Token: 0x060007AC RID: 1964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMuscleHandles_Injected(out BlittableArrayWrapper muscleHandles);

		// Token: 0x060007AD RID: 1965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetName_Injected(ref MuscleHandle _unity_self, out ManagedSpanWrapper ret);
	}
}
