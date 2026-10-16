using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Animation/AnimatorControllerParameter.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	[UsedByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoAnimatorControllerParameter")]
	[NativeAsStruct]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimatorControllerParameter
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00006598 File Offset: 0x00004798
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000065B0 File Offset: 0x000047B0
		public int nameHash
		{
			get
			{
				return Animator.StringToHash(this.m_Name);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000065D0 File Offset: 0x000047D0
		// (set) Token: 0x060002BF RID: 703 RVA: 0x000065E8 File Offset: 0x000047E8
		public AnimatorControllerParameterType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000065F4 File Offset: 0x000047F4
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000660C File Offset: 0x0000480C
		public float defaultFloat
		{
			get
			{
				return this.m_DefaultFloat;
			}
			set
			{
				this.m_DefaultFloat = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00006618 File Offset: 0x00004818
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00006630 File Offset: 0x00004830
		public int defaultInt
		{
			get
			{
				return this.m_DefaultInt;
			}
			set
			{
				this.m_DefaultInt = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000663C File Offset: 0x0000483C
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00006654 File Offset: 0x00004854
		public bool defaultBool
		{
			get
			{
				return this.m_DefaultBool;
			}
			set
			{
				this.m_DefaultBool = value;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00006660 File Offset: 0x00004860
		public override bool Equals(object o)
		{
			AnimatorControllerParameter animatorControllerParameter = o as AnimatorControllerParameter;
			return animatorControllerParameter != null && this.m_Name == animatorControllerParameter.m_Name && this.m_Type == animatorControllerParameter.m_Type && this.m_DefaultFloat == animatorControllerParameter.m_DefaultFloat && this.m_DefaultInt == animatorControllerParameter.m_DefaultInt && this.m_DefaultBool == animatorControllerParameter.m_DefaultBool;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000066CC File Offset: 0x000048CC
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x04000079 RID: 121
		internal string m_Name = "";

		// Token: 0x0400007A RID: 122
		internal AnimatorControllerParameterType m_Type;

		// Token: 0x0400007B RID: 123
		internal float m_DefaultFloat;

		// Token: 0x0400007C RID: 124
		internal int m_DefaultInt;

		// Token: 0x0400007D RID: 125
		internal bool m_DefaultBool;
	}
}
