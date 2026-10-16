using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D4 RID: 468
	internal class AncestorFilter
	{
		// Token: 0x06000E5D RID: 3677 RVA: 0x000407B7 File Offset: 0x0003E9B7
		private void AddHash(int hash)
		{
			this.m_HashStack.Push(hash);
			this.m_CountingBloomFilter.InsertHash((uint)hash);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000407D4 File Offset: 0x0003E9D4
		public unsafe bool IsCandidate(StyleComplexSelector complexSel)
		{
			int i = 0;
			while (i < 4)
			{
				bool flag = *((ref complexSel.ancestorHashes.hashes.FixedElementField) + (IntPtr)i * 4) == 0;
				bool flag2;
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					bool flag3 = !this.m_CountingBloomFilter.ContainsHash((uint)(*((ref complexSel.ancestorHashes.hashes.FixedElementField) + (IntPtr)i * 4)));
					if (!flag3)
					{
						i++;
						continue;
					}
					flag2 = false;
				}
				return flag2;
			}
			return true;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00040848 File Offset: 0x0003EA48
		public void PushElement(VisualElement element)
		{
			int count = this.m_HashStack.Count;
			this.AddHash(element.typeName.GetHashCode() * 13);
			bool flag = !string.IsNullOrEmpty(element.name);
			if (flag)
			{
				this.AddHash(element.name.GetHashCode() * 17);
			}
			List<string> classesForIteration = element.GetClassesForIteration();
			for (int i = 0; i < classesForIteration.Count; i++)
			{
				this.AddHash(classesForIteration[i].GetHashCode() * 19);
			}
			this.m_HashStack.Push(this.m_HashStack.Count - count);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000408F0 File Offset: 0x0003EAF0
		public void PopElement()
		{
			int i = this.m_HashStack.Peek();
			this.m_HashStack.Pop();
			while (i > 0)
			{
				int num = this.m_HashStack.Peek();
				this.m_CountingBloomFilter.RemoveHash((uint)num);
				this.m_HashStack.Pop();
				i--;
			}
		}

		// Token: 0x040008ED RID: 2285
		private CountingBloomFilter m_CountingBloomFilter;

		// Token: 0x040008EE RID: 2286
		private Stack<int> m_HashStack = new Stack<int>(100);
	}
}
