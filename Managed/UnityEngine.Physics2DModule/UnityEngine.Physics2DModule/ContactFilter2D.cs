using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("ContactFilter", "struct ContactFilter;")]
	[Serializable]
	public struct ContactFilter2D
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000771C File Offset: 0x0000591C
		public ContactFilter2D NoFilter()
		{
			this.useTriggers = true;
			this.useLayerMask = false;
			this.layerMask = -1;
			this.useDepth = false;
			this.useOutsideDepth = false;
			this.minDepth = float.NegativeInfinity;
			this.maxDepth = float.PositiveInfinity;
			this.useNormalAngle = false;
			this.useOutsideNormalAngle = false;
			this.minNormalAngle = 0f;
			this.maxNormalAngle = 359.9999f;
			return this;
		}

		// Token: 0x06000241 RID: 577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CheckConsistency();

		// Token: 0x06000242 RID: 578 RVA: 0x00007796 File Offset: 0x00005996
		public void ClearLayerMask()
		{
			this.useLayerMask = false;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000077A0 File Offset: 0x000059A0
		public void SetLayerMask(LayerMask layerMask)
		{
			this.layerMask = layerMask;
			this.useLayerMask = true;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000077B1 File Offset: 0x000059B1
		public void ClearDepth()
		{
			this.useDepth = false;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000077BB File Offset: 0x000059BB
		public void SetDepth(float minDepth, float maxDepth)
		{
			this.minDepth = minDepth;
			this.maxDepth = maxDepth;
			this.useDepth = true;
			this.CheckConsistency();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000077DA File Offset: 0x000059DA
		public void ClearNormalAngle()
		{
			this.useNormalAngle = false;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000077E4 File Offset: 0x000059E4
		public void SetNormalAngle(float minNormalAngle, float maxNormalAngle)
		{
			this.minNormalAngle = minNormalAngle;
			this.maxNormalAngle = maxNormalAngle;
			this.useNormalAngle = true;
			this.CheckConsistency();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007804 File Offset: 0x00005A04
		public bool isFiltering
		{
			get
			{
				return !this.useTriggers || this.useLayerMask || this.useDepth || this.useNormalAngle;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007838 File Offset: 0x00005A38
		public bool IsFilteringTrigger(Collider2D collider)
		{
			return !this.useTriggers && collider.isTrigger;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000785C File Offset: 0x00005A5C
		public bool IsFilteringLayerMask(GameObject obj)
		{
			return this.useLayerMask && (this.layerMask & (1 << obj.layer)) == 0;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007894 File Offset: 0x00005A94
		public bool IsFilteringDepth(GameObject obj)
		{
			bool flag = !this.useDepth;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.minDepth > this.maxDepth;
				if (flag3)
				{
					float num = this.minDepth;
					this.minDepth = this.maxDepth;
					this.maxDepth = num;
				}
				float z = obj.transform.position.z;
				bool flag4 = z < this.minDepth || z > this.maxDepth;
				bool flag5 = this.useOutsideDepth;
				if (flag5)
				{
					flag2 = !flag4;
				}
				else
				{
					flag2 = flag4;
				}
			}
			return flag2;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007924 File Offset: 0x00005B24
		public bool IsFilteringNormalAngle(Vector2 normal)
		{
			return ContactFilter2D.IsFilteringNormalAngle_Injected(ref this, ref normal);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000793C File Offset: 0x00005B3C
		public bool IsFilteringNormalAngle(float angle)
		{
			return this.IsFilteringNormalAngleUsingAngle(angle);
		}

		// Token: 0x0600024E RID: 590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsFilteringNormalAngleUsingAngle(float angle);

		// Token: 0x0600024F RID: 591 RVA: 0x00007958 File Offset: 0x00005B58
		internal static ContactFilter2D CreateLegacyFilter(int layerMask, float minDepth, float maxDepth)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter2D.SetLayerMask(layerMask);
			contactFilter2D.SetDepth(minDepth, maxDepth);
			return contactFilter2D;
		}

		// Token: 0x06000250 RID: 592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFilteringNormalAngle_Injected(ref ContactFilter2D _unity_self, [In] ref Vector2 normal);

		// Token: 0x04000065 RID: 101
		[NativeName("m_UseTriggers")]
		public bool useTriggers;

		// Token: 0x04000066 RID: 102
		[NativeName("m_UseLayerMask")]
		public bool useLayerMask;

		// Token: 0x04000067 RID: 103
		[NativeName("m_UseDepth")]
		public bool useDepth;

		// Token: 0x04000068 RID: 104
		[NativeName("m_UseOutsideDepth")]
		public bool useOutsideDepth;

		// Token: 0x04000069 RID: 105
		[NativeName("m_UseNormalAngle")]
		public bool useNormalAngle;

		// Token: 0x0400006A RID: 106
		[NativeName("m_UseOutsideNormalAngle")]
		public bool useOutsideNormalAngle;

		// Token: 0x0400006B RID: 107
		[NativeName("m_LayerMask")]
		public LayerMask layerMask;

		// Token: 0x0400006C RID: 108
		[NativeName("m_MinDepth")]
		public float minDepth;

		// Token: 0x0400006D RID: 109
		[NativeName("m_MaxDepth")]
		public float maxDepth;

		// Token: 0x0400006E RID: 110
		[NativeName("m_MinNormalAngle")]
		public float minNormalAngle;

		// Token: 0x0400006F RID: 111
		[NativeName("m_MaxNormalAngle")]
		public float maxNormalAngle;

		// Token: 0x04000070 RID: 112
		public const float NormalAngleUpperLimit = 359.9999f;
	}
}
