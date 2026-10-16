using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000003 RID: 3
	public abstract class GaussianWindow1d<T>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public float Sigma { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public int KernelSize
		{
			get
			{
				return this.m_kernel.Length;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		private void GenerateKernel(float sigma, int maxKernelRadius)
		{
			int num = Mathf.Min(maxKernelRadius, Mathf.FloorToInt(Mathf.Abs(sigma) * 2.5f));
			this.m_kernel = new float[2 * num + 1];
			this.m_kernelSum = 0f;
			if (num == 0)
			{
				this.m_kernelSum = (this.m_kernel[0] = 1f);
			}
			else
			{
				for (int i = -num; i <= num; i++)
				{
					this.m_kernel[i + num] = Mathf.Exp((float)(-(float)(i * i)) / (2f * sigma * sigma)) / Mathf.Sqrt(6.2831855f * sigma);
					this.m_kernelSum += this.m_kernel[i + num];
				}
			}
			this.Sigma = sigma;
		}

		// Token: 0x06000007 RID: 7
		protected abstract T Compute(int windowPos);

		// Token: 0x06000008 RID: 8 RVA: 0x0000218E File Offset: 0x0000038E
		public GaussianWindow1d(float sigma, int maxKernelRadius = 10)
		{
			this.GenerateKernel(sigma, maxKernelRadius);
			this.m_currentPos = 0;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A5 File Offset: 0x000003A5
		public void Reset()
		{
			this.m_data = null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AE File Offset: 0x000003AE
		public bool IsEmpty()
		{
			return this.m_data == null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021BC File Offset: 0x000003BC
		public void AddValue(T v)
		{
			if (this.m_data == null)
			{
				this.m_data = new T[this.KernelSize];
				for (int i = 0; i < this.KernelSize; i++)
				{
					this.m_data[i] = v;
				}
				this.m_currentPos = Mathf.Min(1, this.KernelSize - 1);
			}
			this.m_data[this.m_currentPos] = v;
			int num = this.m_currentPos + 1;
			this.m_currentPos = num;
			if (num == this.KernelSize)
			{
				this.m_currentPos = 0;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002246 File Offset: 0x00000446
		public T Filter(T v)
		{
			if (this.KernelSize < 3)
			{
				return v;
			}
			this.AddValue(v);
			return this.Value();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002260 File Offset: 0x00000460
		public T Value()
		{
			return this.Compute(this.m_currentPos);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000226E File Offset: 0x0000046E
		public int BufferLength
		{
			get
			{
				return this.m_data.Length;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002278 File Offset: 0x00000478
		public void SetBufferValue(int index, T value)
		{
			this.m_data[index] = value;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002287 File Offset: 0x00000487
		public T GetBufferValue(int index)
		{
			return this.m_data[index];
		}

		// Token: 0x04000001 RID: 1
		protected T[] m_data;

		// Token: 0x04000002 RID: 2
		protected float[] m_kernel;

		// Token: 0x04000003 RID: 3
		protected float m_kernelSum;

		// Token: 0x04000004 RID: 4
		protected int m_currentPos;
	}
}
