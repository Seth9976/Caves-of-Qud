using System;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	internal struct SDFStyleScope : IDisposable
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		public SDFStyleScope(params GUIStyle[] styles)
		{
			this.m_Styles = styles;
			this.m_OriginalValues = new bool[styles.Length];
			for (int i = 0; i < styles.Length; i++)
			{
				this.m_OriginalValues[i] = styles[i].isSDF;
				styles[i].isSDF = true;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D8FC File Offset: 0x0000BAFC
		public void Dispose()
		{
			for (int i = 0; i < this.m_Styles.Length; i++)
			{
				this.m_Styles[i].isSDF = this.m_OriginalValues[i];
			}
		}

		// Token: 0x040000E9 RID: 233
		private GUIStyle[] m_Styles;

		// Token: 0x040000EA RID: 234
		private bool[] m_OriginalValues;
	}
}
