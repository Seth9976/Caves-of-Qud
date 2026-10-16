using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020005C9 RID: 1481
	public struct VisualElementStyleSheetSet : IEquatable<VisualElementStyleSheetSet>
	{
		// Token: 0x06002BB8 RID: 11192 RVA: 0x0009DDD0 File Offset: 0x0009BFD0
		internal VisualElementStyleSheetSet(VisualElement element)
		{
			this.m_Element = element;
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x0009DDDA File Offset: 0x0009BFDA
		public void Add(StyleSheet styleSheet)
		{
			this.Insert(this.count, styleSheet);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0009DDEC File Offset: 0x0009BFEC
		public void Insert(int index, StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList == null;
			if (flag2)
			{
				this.m_Element.styleSheetList = new List<StyleSheet>();
			}
			else
			{
				bool flag3 = this.m_Element.styleSheetList.Contains(styleSheet);
				if (flag3)
				{
					return;
				}
			}
			this.m_Element.styleSheetList.Insert(index, styleSheet);
			this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x0009DE70 File Offset: 0x0009C070
		public void Clear()
		{
			bool flag = this.m_Element.styleSheetList == null;
			if (!flag)
			{
				this.m_Element.styleSheetList = null;
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0009DEAC File Offset: 0x0009C0AC
		public bool Remove(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null && this.m_Element.styleSheetList.Remove(styleSheet);
			bool flag4;
			if (flag2)
			{
				bool flag3 = this.m_Element.styleSheetList.Count == 0;
				if (flag3)
				{
					this.m_Element.styleSheetList = null;
				}
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
				flag4 = true;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x0009DF34 File Offset: 0x0009C134
		internal void Swap(StyleSheet old, StyleSheet @new)
		{
			bool flag = old == null;
			if (flag)
			{
				throw new ArgumentNullException("old");
			}
			bool flag2 = @new == null;
			if (flag2)
			{
				throw new ArgumentNullException("new");
			}
			bool flag3 = this.m_Element.styleSheetList == null;
			if (!flag3)
			{
				int num = this.m_Element.styleSheetList.IndexOf(old);
				bool flag4 = num >= 0;
				if (flag4)
				{
					this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
					this.m_Element.styleSheetList[num] = @new;
				}
			}
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x0009DFC8 File Offset: 0x0009C1C8
		public bool Contains(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null;
			return flag2 && this.m_Element.styleSheetList.Contains(styleSheet);
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x0009E018 File Offset: 0x0009C218
		public int count
		{
			get
			{
				bool flag = this.m_Element.styleSheetList == null;
				int num;
				if (flag)
				{
					num = 0;
				}
				else
				{
					num = this.m_Element.styleSheetList.Count;
				}
				return num;
			}
		}

		// Token: 0x17000ADB RID: 2779
		public StyleSheet this[int index]
		{
			get
			{
				bool flag = this.m_Element.styleSheetList == null;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_Element.styleSheetList[index];
			}
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x0009E090 File Offset: 0x0009C290
		public bool Equals(VisualElementStyleSheetSet other)
		{
			return object.Equals(this.m_Element, other.m_Element);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x0009E0B4 File Offset: 0x0009C2B4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisualElementStyleSheetSet && this.Equals((VisualElementStyleSheetSet)obj);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x0009E0EC File Offset: 0x0009C2EC
		public override int GetHashCode()
		{
			return (this.m_Element != null) ? this.m_Element.GetHashCode() : 0;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x0009E114 File Offset: 0x0009C314
		public static bool operator ==(VisualElementStyleSheetSet left, VisualElementStyleSheetSet right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x0009E130 File Offset: 0x0009C330
		public static bool operator !=(VisualElementStyleSheetSet left, VisualElementStyleSheetSet right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040013E9 RID: 5097
		private readonly VisualElement m_Element;
	}
}
