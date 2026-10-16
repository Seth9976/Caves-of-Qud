using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004DB RID: 1243
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StyleVariableContext
	{
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x0008990C File Offset: 0x00087B0C
		public List<StyleVariable> variables
		{
			get
			{
				return this.m_Variables;
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00089914 File Offset: 0x00087B14
		public void Add(StyleVariable sv)
		{
			StyleVariableContext.<>c__DisplayClass7_0 CS$<>8__locals1;
			CS$<>8__locals1.hash = sv.GetHashCode();
			int num = this.m_SortedHash.BinarySearch(CS$<>8__locals1.hash);
			bool flag = num >= 0;
			if (flag)
			{
				int i = this.m_Variables.Count - 1;
				bool flag2 = this.m_UnsortedHash[i] == CS$<>8__locals1.hash;
				if (flag2)
				{
					return;
				}
				for (i--; i >= 0; i--)
				{
					bool flag3 = this.m_UnsortedHash[i] == CS$<>8__locals1.hash;
					if (flag3)
					{
						this.m_VariableHash ^= StyleVariableContext.<Add>g__ComputeOrderSensitiveHash|7_0(i, ref CS$<>8__locals1);
						this.m_Variables.RemoveAt(i);
						this.m_UnsortedHash.RemoveAt(i);
						break;
					}
				}
			}
			else
			{
				this.m_SortedHash.Insert(~num, CS$<>8__locals1.hash);
			}
			this.m_VariableHash ^= StyleVariableContext.<Add>g__ComputeOrderSensitiveHash|7_0(this.m_Variables.Count, ref CS$<>8__locals1);
			this.m_Variables.Add(sv);
			this.m_UnsortedHash.Add(CS$<>8__locals1.hash);
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00089A40 File Offset: 0x00087C40
		public void AddInitialRange(StyleVariableContext other)
		{
			bool flag = other.m_Variables.Count > 0;
			if (flag)
			{
				Debug.Assert(this.m_Variables.Count == 0);
				this.m_VariableHash = other.m_VariableHash;
				this.m_Variables.AddRange(other.m_Variables);
				this.m_SortedHash.AddRange(other.m_SortedHash);
				this.m_UnsortedHash.AddRange(other.m_UnsortedHash);
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x00089AB8 File Offset: 0x00087CB8
		public void Clear()
		{
			bool flag = this.m_Variables.Count > 0;
			if (flag)
			{
				this.m_Variables.Clear();
				this.m_VariableHash = 0;
				this.m_SortedHash.Clear();
				this.m_UnsortedHash.Clear();
			}
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x00089B05 File Offset: 0x00087D05
		public StyleVariableContext()
		{
			this.m_Variables = new List<StyleVariable>();
			this.m_VariableHash = 0;
			this.m_SortedHash = new List<int>();
			this.m_UnsortedHash = new List<int>();
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00089B38 File Offset: 0x00087D38
		public StyleVariableContext(StyleVariableContext other)
		{
			this.m_Variables = new List<StyleVariable>(other.m_Variables);
			this.m_VariableHash = other.m_VariableHash;
			this.m_SortedHash = new List<int>(other.m_SortedHash);
			this.m_UnsortedHash = new List<int>(other.m_UnsortedHash);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00089B8C File Offset: 0x00087D8C
		public bool TryFindVariable(string name, out StyleVariable v)
		{
			for (int i = this.m_Variables.Count - 1; i >= 0; i--)
			{
				bool flag = this.m_Variables[i].name == name;
				if (flag)
				{
					v = this.m_Variables[i];
					return true;
				}
			}
			v = default(StyleVariable);
			return false;
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00089BFC File Offset: 0x00087DFC
		public int GetVariableHash()
		{
			return this.m_VariableHash;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00089C20 File Offset: 0x00087E20
		[CompilerGenerated]
		internal static int <Add>g__ComputeOrderSensitiveHash|7_0(int index, ref StyleVariableContext.<>c__DisplayClass7_0 A_1)
		{
			return (index + 1) * A_1.hash;
		}

		// Token: 0x04001119 RID: 4377
		public static readonly StyleVariableContext none = new StyleVariableContext();

		// Token: 0x0400111A RID: 4378
		private int m_VariableHash;

		// Token: 0x0400111B RID: 4379
		private List<StyleVariable> m_Variables;

		// Token: 0x0400111C RID: 4380
		private List<int> m_SortedHash;

		// Token: 0x0400111D RID: 4381
		private List<int> m_UnsortedHash;
	}
}
