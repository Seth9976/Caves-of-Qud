using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x0200002E RID: 46
	[RequiredByNativeCode]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchySearch.h")]
	[Serializable]
	public struct HierarchySearchFilter
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000059B3 File Offset: 0x00003BB3
		public static readonly ref HierarchySearchFilter Invalid
		{
			get
			{
				return ref HierarchySearchFilter.s_Invalid;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000059BA File Offset: 0x00003BBA
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.Name);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000059CA File Offset: 0x00003BCA
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000059D2 File Offset: 0x00003BD2
		public string Name { readonly get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000059DB File Offset: 0x00003BDB
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000059E3 File Offset: 0x00003BE3
		public string Value { readonly get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000059EC File Offset: 0x00003BEC
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x000059F4 File Offset: 0x00003BF4
		public float NumValue { readonly get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000059FD File Offset: 0x00003BFD
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00005A05 File Offset: 0x00003C05
		public HierarchySearchFilterOperator Op { readonly get; set; }

		// Token: 0x060001D4 RID: 468 RVA: 0x00005A10 File Offset: 0x00003C10
		public static string ToString(HierarchySearchFilterOperator op)
		{
			string text;
			switch (op)
			{
			case HierarchySearchFilterOperator.Equal:
				text = "=";
				break;
			case HierarchySearchFilterOperator.Contains:
				text = ":";
				break;
			case HierarchySearchFilterOperator.Greater:
				text = ">";
				break;
			case HierarchySearchFilterOperator.GreaterOrEqual:
				text = ">=";
				break;
			case HierarchySearchFilterOperator.Lesser:
				text = "<";
				break;
			case HierarchySearchFilterOperator.LesserOrEqual:
				text = "<=";
				break;
			case HierarchySearchFilterOperator.NotEqual:
				text = "!=";
				break;
			case HierarchySearchFilterOperator.Not:
				text = "-";
				break;
			default:
				throw new NotImplementedException(string.Format("Cannot convert {0} to string", op));
			}
			return text;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public static HierarchySearchFilterOperator ToOp(string op)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(op);
			if (num <= 957132539U)
			{
				if (num <= 671913016U)
				{
					if (num != 284975636U)
					{
						if (num == 671913016U)
						{
							if (op == "-")
							{
								return HierarchySearchFilterOperator.Not;
							}
						}
					}
					else if (op == ">=")
					{
						return HierarchySearchFilterOperator.GreaterOrEqual;
					}
				}
				else if (num != 940354920U)
				{
					if (num == 957132539U)
					{
						if (op == "<")
						{
							return HierarchySearchFilterOperator.Lesser;
						}
					}
				}
				else if (op == "=")
				{
					return HierarchySearchFilterOperator.Equal;
				}
			}
			else if (num <= 1057798253U)
			{
				if (num != 990687777U)
				{
					if (num == 1057798253U)
					{
						if (op == ":")
						{
							return HierarchySearchFilterOperator.Contains;
						}
					}
				}
				else if (op == ">")
				{
					return HierarchySearchFilterOperator.Greater;
				}
			}
			else if (num != 2428715011U)
			{
				if (num == 2499223986U)
				{
					if (op == "<=")
					{
						return HierarchySearchFilterOperator.LesserOrEqual;
					}
				}
			}
			else if (op == "!=")
			{
				return HierarchySearchFilterOperator.NotEqual;
			}
			throw new NotImplementedException("Cannot convert " + op + " to SearchFilterOperator");
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005BEC File Offset: 0x00003DEC
		public override string ToString()
		{
			string text = (float.IsNaN(this.NumValue) ? this.Value : this.NumValue.ToString());
			return this.Name + HierarchySearchFilter.ToString(this.Op) + HierarchySearchFilter.QuoteStringIfNeeded(text);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005C40 File Offset: 0x00003E40
		[VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal static HierarchySearchFilter CreateFilter(string name, string op, string value)
		{
			return HierarchySearchFilter.CreateFilter(name, HierarchySearchFilter.ToOp(op), value);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005C60 File Offset: 0x00003E60
		internal static HierarchySearchFilter CreateFilter(string name, HierarchySearchFilterOperator op, string str)
		{
			string text = str;
			float num = float.NaN;
			try
			{
				num = Convert.ToSingle(str);
				text = null;
			}
			catch (Exception)
			{
			}
			return new HierarchySearchFilter
			{
				Name = name,
				Op = op,
				Value = text,
				NumValue = num
			};
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005CCC File Offset: 0x00003ECC
		internal static string QuoteStringIfNeeded(string s)
		{
			bool flag = s.Length > 0 && s.IndexOfAny(HierarchySearchFilter.s_WhiteSpaces) != -1 && s[0] != '"';
			string text;
			if (flag)
			{
				text = "\"" + s + "\"";
			}
			else
			{
				text = s;
			}
			return text;
		}

		// Token: 0x04000080 RID: 128
		private static readonly char[] s_WhiteSpaces = new char[] { ' ', '\t', '\n' };

		// Token: 0x04000081 RID: 129
		private static readonly HierarchySearchFilter s_Invalid;
	}
}
