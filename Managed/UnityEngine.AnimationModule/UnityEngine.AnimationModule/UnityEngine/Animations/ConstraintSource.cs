using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200006C RID: 108
	[NativeType(CodegenOptions = CodegenOptions.Custom, Header = "Modules/Animation/Constraints/ConstraintSource.h", IntermediateScriptingStructName = "MonoConstraintSource")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[UsedByNativeCode]
	[Serializable]
	public struct ConstraintSource
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0000C508 File Offset: 0x0000A708
		public Transform sourceTransform
		{
			get
			{
				return this.m_SourceTransform;
			}
			set
			{
				this.m_SourceTransform = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000C514 File Offset: 0x0000A714
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0000C52C File Offset: 0x0000A72C
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		// Token: 0x04000194 RID: 404
		[NativeName("sourceTransform")]
		private Transform m_SourceTransform;

		// Token: 0x04000195 RID: 405
		[NativeName("weight")]
		private float m_Weight;
	}
}
