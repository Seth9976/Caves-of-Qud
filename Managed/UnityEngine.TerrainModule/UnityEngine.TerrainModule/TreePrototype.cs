using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[UsedByNativeCode]
	[NativeAsStruct]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TreePrototype
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003410 File Offset: 0x00001610
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00003428 File Offset: 0x00001628
		public GameObject prefab
		{
			get
			{
				return this.m_Prefab;
			}
			set
			{
				this.m_Prefab = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003434 File Offset: 0x00001634
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000344C File Offset: 0x0000164C
		public float bendFactor
		{
			get
			{
				return this.m_BendFactor;
			}
			set
			{
				this.m_BendFactor = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003458 File Offset: 0x00001658
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00003470 File Offset: 0x00001670
		public int navMeshLod
		{
			get
			{
				return this.m_NavMeshLod;
			}
			set
			{
				this.m_NavMeshLod = value;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000347A File Offset: 0x0000167A
		public TreePrototype()
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003484 File Offset: 0x00001684
		public TreePrototype(TreePrototype other)
		{
			this.prefab = other.prefab;
			this.bendFactor = other.bendFactor;
			this.navMeshLod = other.navMeshLod;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000034B8 File Offset: 0x000016B8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TreePrototype);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000034D8 File Offset: 0x000016D8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000034F0 File Offset: 0x000016F0
		private bool Equals(TreePrototype other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = other == this;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = base.GetType() != other.GetType();
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						bool flag5 = this.prefab == other.prefab && this.bendFactor == other.bendFactor && this.navMeshLod == other.navMeshLod;
						flag2 = flag5;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003569 File Offset: 0x00001769
		internal bool Validate(out string errorMessage)
		{
			return TreePrototype.ValidateTreePrototype(this, out errorMessage);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003574 File Offset: 0x00001774
		[FreeFunction("TerrainDataScriptingInterface::ValidateTreePrototype")]
		internal static bool ValidateTreePrototype([NotNull] TreePrototype prototype, out string errorMessage)
		{
			if (prototype == null)
			{
				ThrowHelper.ThrowArgumentNullException(prototype, "prototype");
			}
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				flag = TreePrototype.ValidateTreePrototype_Injected(prototype, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				errorMessage = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x06000101 RID: 257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateTreePrototype_Injected(TreePrototype prototype, out ManagedSpanWrapper errorMessage);

		// Token: 0x0400005D RID: 93
		[NativeName("prefab")]
		internal GameObject m_Prefab;

		// Token: 0x0400005E RID: 94
		[NativeName("bendFactor")]
		internal float m_BendFactor;

		// Token: 0x0400005F RID: 95
		[NativeName("navMeshLod")]
		internal int m_NavMeshLod;
	}
}
