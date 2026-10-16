using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004CE RID: 1230
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct StyleSelectorPart
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x00088AB8 File Offset: 0x00086CB8
		// (set) Token: 0x060025B3 RID: 9651 RVA: 0x00088AD0 File Offset: 0x00086CD0
		public string value
		{
			get
			{
				return this.m_Value;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x00088ADC File Offset: 0x00086CDC
		// (set) Token: 0x060025B5 RID: 9653 RVA: 0x00088AF4 File Offset: 0x00086CF4
		public StyleSelectorType type
		{
			get
			{
				return this.m_Type;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00088B00 File Offset: 0x00086D00
		public override string ToString()
		{
			return UnityString.Format("[StyleSelectorPart: value={0}, type={1}]", new object[] { this.value, this.type });
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00088B3C File Offset: 0x00086D3C
		public static StyleSelectorPart CreateClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Class,
				m_Value = className
			};
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00088B68 File Offset: 0x00086D68
		public static StyleSelectorPart CreatePseudoClass(string className)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.PseudoClass,
				m_Value = className
			};
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00088B94 File Offset: 0x00086D94
		public static StyleSelectorPart CreateId(string Id)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.ID,
				m_Value = Id
			};
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00088BC0 File Offset: 0x00086DC0
		public static StyleSelectorPart CreateType(Type t)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Type,
				m_Value = t.Name
			};
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x00088BF0 File Offset: 0x00086DF0
		public static StyleSelectorPart CreateType(string typeName)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Type,
				m_Value = typeName
			};
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x00088C1C File Offset: 0x00086E1C
		public static StyleSelectorPart CreatePredicate(object predicate)
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Predicate,
				tempData = predicate
			};
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x00088C48 File Offset: 0x00086E48
		public static StyleSelectorPart CreateWildCard()
		{
			return new StyleSelectorPart
			{
				m_Type = StyleSelectorType.Wildcard
			};
		}

		// Token: 0x040010C8 RID: 4296
		[SerializeField]
		private string m_Value;

		// Token: 0x040010C9 RID: 4297
		[SerializeField]
		private StyleSelectorType m_Type;

		// Token: 0x040010CA RID: 4298
		internal object tempData;
	}
}
