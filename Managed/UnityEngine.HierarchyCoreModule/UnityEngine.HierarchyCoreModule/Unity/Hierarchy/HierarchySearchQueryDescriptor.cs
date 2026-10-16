using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x0200002F RID: 47
	[RequiredByNativeCode]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchySearch.h")]
	[NativeAsStruct]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HierarchySearchQueryDescriptor
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005D37 File Offset: 0x00003F37
		public static HierarchySearchQueryDescriptor Empty
		{
			get
			{
				return HierarchySearchQueryDescriptor.s_Empty;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005D3E File Offset: 0x00003F3E
		public static HierarchySearchQueryDescriptor InvalidQuery
		{
			get
			{
				return HierarchySearchQueryDescriptor.s_InvalidQuery;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005D45 File Offset: 0x00003F45
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00005D4D File Offset: 0x00003F4D
		public HierarchySearchFilter[] SystemFilters { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005D56 File Offset: 0x00003F56
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00005D5E File Offset: 0x00003F5E
		public HierarchySearchFilter[] Filters { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00005D67 File Offset: 0x00003F67
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00005D6F File Offset: 0x00003F6F
		public string[] TextValues { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00005D78 File Offset: 0x00003F78
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00005D80 File Offset: 0x00003F80
		public bool Strict { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00005D89 File Offset: 0x00003F89
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00005D91 File Offset: 0x00003F91
		public bool Invalid { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00005D9A File Offset: 0x00003F9A
		public bool IsValid
		{
			get
			{
				return !this.Invalid && !this.IsEmpty;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public bool IsEmpty
		{
			get
			{
				return this.Filters.Length == 0 && this.TextValues.Length == 0 && this.SystemFilters.Length == 0;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00005DD1 File Offset: 0x00003FD1
		public bool IsSystemOnlyQuery
		{
			get
			{
				return this.SystemFilters.Length != 0 && this.Filters.Length == 0 && this.TextValues.Length == 0;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public unsafe HierarchySearchQueryDescriptor(HierarchySearchFilter[] filters = null, string[] textValues = null)
		{
			filters = filters ?? new HierarchySearchFilter[0];
			textValues = textValues ?? new string[0];
			this.Filters = HierarchySearchQueryDescriptor.Where<HierarchySearchFilter>(filters, (HierarchySearchFilter f) => !HierarchySearchQueryDescriptor.s_SystemFilters.Contains(f.Name));
			this.SystemFilters = HierarchySearchQueryDescriptor.Where<HierarchySearchFilter>(filters, (HierarchySearchFilter f) => HierarchySearchQueryDescriptor.s_SystemFilters.Contains(f.Name));
			this.TextValues = textValues;
			HierarchySearchFilter hierarchySearchFilter = *HierarchySearchFilter.Invalid;
			foreach (HierarchySearchFilter hierarchySearchFilter2 in this.SystemFilters)
			{
				bool flag = hierarchySearchFilter2.Name == "strict";
				if (flag)
				{
					hierarchySearchFilter = hierarchySearchFilter2;
					break;
				}
			}
			this.Invalid = false;
			this.Strict = !hierarchySearchFilter.IsValid || hierarchySearchFilter.Value == "true";
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005EF8 File Offset: 0x000040F8
		public HierarchySearchQueryDescriptor(HierarchySearchQueryDescriptor desc)
		{
			this.SystemFilters = new HierarchySearchFilter[desc.SystemFilters.Length];
			Array.Copy(desc.SystemFilters, this.SystemFilters, desc.SystemFilters.Length);
			this.Filters = new HierarchySearchFilter[desc.Filters.Length];
			Array.Copy(desc.Filters, this.Filters, desc.Filters.Length);
			this.TextValues = new string[desc.TextValues.Length];
			Array.Copy(desc.TextValues, this.TextValues, desc.TextValues.Length);
			this.Strict = desc.Strict;
			this.Invalid = desc.Invalid;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005FB4 File Offset: 0x000041B4
		public override string ToString()
		{
			return this.BuildQuery();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005FCC File Offset: 0x000041CC
		[VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal string BuildFilterQuery()
		{
			return string.Join<HierarchySearchFilter>(" ", this.Filters);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005FF0 File Offset: 0x000041F0
		internal string BuildSystemFilterQuery()
		{
			return string.Join<HierarchySearchFilter>(" ", this.SystemFilters);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006014 File Offset: 0x00004214
		internal string BuildTextQuery()
		{
			string[] array = new string[this.TextValues.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = HierarchySearchFilter.QuoteStringIfNeeded(this.TextValues[i]);
			}
			return string.Join(" ", array);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006064 File Offset: 0x00004264
		internal string BuildQuery()
		{
			string text = "";
			bool flag = this.SystemFilters.Length != 0;
			if (flag)
			{
				text += this.BuildSystemFilterQuery();
			}
			bool flag2 = this.Filters.Length != 0;
			if (flag2)
			{
				bool flag3 = text.Length > 0;
				if (flag3)
				{
					text += " ";
				}
				text += this.BuildFilterQuery();
			}
			bool flag4 = this.TextValues.Length != 0;
			if (flag4)
			{
				bool flag5 = text.Length > 0;
				if (flag5)
				{
					text += " ";
				}
				text += this.BuildTextQuery();
			}
			return text;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000610C File Offset: 0x0000430C
		private static T[] Where<T>(IEnumerable<T> src, Func<T, bool> pred)
		{
			int num = 0;
			foreach (T t in src)
			{
				bool flag = pred(t);
				if (flag)
				{
					num++;
				}
			}
			T[] array = new T[num];
			int num2 = 0;
			foreach (T t2 in src)
			{
				bool flag2 = pred(t2);
				if (flag2)
				{
					array[num2++] = t2;
				}
			}
			return array;
		}

		// Token: 0x04000086 RID: 134
		private static readonly HashSet<string> s_SystemFilters = new HashSet<string>(new string[] { "nodetype", "strict" });

		// Token: 0x04000087 RID: 135
		private static readonly HierarchySearchQueryDescriptor s_Empty = new HierarchySearchQueryDescriptor(null, null);

		// Token: 0x04000088 RID: 136
		private static readonly HierarchySearchQueryDescriptor s_InvalidQuery = new HierarchySearchQueryDescriptor(null, null)
		{
			Invalid = true
		};
	}
}
