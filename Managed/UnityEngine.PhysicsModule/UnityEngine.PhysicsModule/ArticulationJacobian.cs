using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public struct ArticulationJacobian
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002A20 File Offset: 0x00000C20
		public ArticulationJacobian(int rows, int cols)
		{
			this.rowsCount = rows;
			this.colsCount = cols;
			this.matrixData = new List<float>(rows * cols);
			for (int i = 0; i < rows * cols; i++)
			{
				this.matrixData.Add(0f);
			}
		}

		// Token: 0x1700002D RID: 45
		public float this[int row, int col]
		{
			get
			{
				bool flag = row < 0 || row >= this.rowsCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				bool flag2 = col < 0 || col >= this.colsCount;
				if (flag2)
				{
					throw new IndexOutOfRangeException();
				}
				return this.matrixData[row * this.colsCount + col];
			}
			set
			{
				bool flag = row < 0 || row >= this.rowsCount;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				bool flag2 = col < 0 || col >= this.colsCount;
				if (flag2)
				{
					throw new IndexOutOfRangeException();
				}
				this.matrixData[row * this.colsCount + col] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B34 File Offset: 0x00000D34
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002B4C File Offset: 0x00000D4C
		public int rows
		{
			get
			{
				return this.rowsCount;
			}
			set
			{
				this.rowsCount = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002B70 File Offset: 0x00000D70
		public int columns
		{
			get
			{
				return this.colsCount;
			}
			set
			{
				this.colsCount = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002B7C File Offset: 0x00000D7C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002B94 File Offset: 0x00000D94
		public List<float> elements
		{
			get
			{
				return this.matrixData;
			}
			set
			{
				this.matrixData = value;
			}
		}

		// Token: 0x0400004E RID: 78
		private int rowsCount;

		// Token: 0x0400004F RID: 79
		private int colsCount;

		// Token: 0x04000050 RID: 80
		private List<float> matrixData;
	}
}
