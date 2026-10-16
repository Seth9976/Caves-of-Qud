using System;
using System.ComponentModel;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoSkeletonBone")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct SkeletonBone
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00006E80 File Offset: 0x00005080
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("transformModified is no longer used and has been deprecated.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int transformModified
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x0400011A RID: 282
		[NativeName("m_Name")]
		public string name;

		// Token: 0x0400011B RID: 283
		[NativeName("m_ParentName")]
		internal string parentName;

		// Token: 0x0400011C RID: 284
		[NativeName("m_Position")]
		public Vector3 position;

		// Token: 0x0400011D RID: 285
		[NativeName("m_Rotation")]
		public Quaternion rotation;

		// Token: 0x0400011E RID: 286
		[NativeName("m_Scale")]
		public Vector3 scale;
	}
}
