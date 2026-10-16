using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000073 RID: 115
	[NativeType(CodegenOptions.Custom, "UnityEngine::Animation::MonoGenericBinding")]
	[UsedByNativeCode]
	public readonly struct GenericBinding
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0000D129 File Offset: 0x0000B329
		public bool isObjectReference
		{
			get
			{
				return (this.m_Flags & Flags.kPPtr) == Flags.kPPtr;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0000D136 File Offset: 0x0000B336
		public bool isDiscrete
		{
			get
			{
				return (this.m_Flags & (Flags)3) > Flags.kNone;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0000D143 File Offset: 0x0000B343
		public bool isSerializeReference
		{
			get
			{
				return (this.m_Flags & Flags.kSerializeReference) == Flags.kSerializeReference;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0000D150 File Offset: 0x0000B350
		public uint transformPathHash
		{
			get
			{
				return this.m_Path;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0000D158 File Offset: 0x0000B358
		public uint propertyNameHash
		{
			get
			{
				return this.m_PropertyName;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0000D160 File Offset: 0x0000B360
		public int scriptInstanceID
		{
			get
			{
				return this.m_ScriptInstanceID;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0000D168 File Offset: 0x0000B368
		public int typeID
		{
			get
			{
				return this.m_TypeID;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0000D170 File Offset: 0x0000B370
		public byte customTypeID
		{
			get
			{
				return this.m_CustomType;
			}
		}

		// Token: 0x0400019D RID: 413
		private readonly uint m_Path;

		// Token: 0x0400019E RID: 414
		private readonly uint m_PropertyName;

		// Token: 0x0400019F RID: 415
		private readonly int m_ScriptInstanceID;

		// Token: 0x040001A0 RID: 416
		private readonly int m_TypeID;

		// Token: 0x040001A1 RID: 417
		private readonly byte m_CustomType;

		// Token: 0x040001A2 RID: 418
		internal readonly Flags m_Flags;
	}
}
