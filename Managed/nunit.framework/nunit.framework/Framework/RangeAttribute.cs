using System;

namespace NUnit.Framework
{
	// Token: 0x0200002C RID: 44
	public class RangeAttribute : ValuesAttribute
	{
		// Token: 0x06000173 RID: 371 RVA: 0x0000462A File Offset: 0x0000282A
		public RangeAttribute(int from, int to)
			: this(from, to, (from > to) ? (-1) : 1)
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000463C File Offset: 0x0000283C
		public RangeAttribute(int from, int to, int step)
		{
			Guard.ArgumentValid((step > 0 && to >= from) || (step < 0 && to <= from), "Step must be positive with to >= from or negative with to <= from", "step");
			int num = (to - from) / step + 1;
			this.data = new object[num];
			int i = 0;
			int num2 = from;
			while (i < num)
			{
				this.data[i++] = num2;
				num2 += step;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000046AB File Offset: 0x000028AB
		[CLSCompliant(false)]
		public RangeAttribute(uint from, uint to)
			: this(from, to, 1U)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000046B8 File Offset: 0x000028B8
		[CLSCompliant(false)]
		public RangeAttribute(uint from, uint to, uint step)
		{
			Guard.ArgumentValid(step > 0U, "Step must be greater than zero", "step");
			Guard.ArgumentValid(to >= from, "Value of to must be greater than or equal to from", "to");
			uint num = (to - from) / step + 1U;
			this.data = new object[num];
			uint num2 = 0U;
			uint num3 = from;
			while (num2 < num)
			{
				this.data[(int)num2++] = num3;
				num3 += step;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004728 File Offset: 0x00002928
		public RangeAttribute(long from, long to)
			: this(from, to, (from > to) ? (-1L) : 1L)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000473C File Offset: 0x0000293C
		public RangeAttribute(long from, long to, long step)
		{
			Guard.ArgumentValid((step > 0L && to >= from) || (step < 0L && to <= from), "Step must be positive with to >= from or negative with to <= from", "step");
			long num = (to - from) / step + 1L;
			this.data = new object[num];
			int num2 = 0;
			long num3 = from;
			while ((long)num2 < num)
			{
				this.data[num2++] = num3;
				num3 += step;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000047B0 File Offset: 0x000029B0
		[CLSCompliant(false)]
		public RangeAttribute(ulong from, ulong to)
			: this(from, to, 1UL)
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000047BC File Offset: 0x000029BC
		[CLSCompliant(false)]
		public RangeAttribute(ulong from, ulong to, ulong step)
		{
			Guard.ArgumentValid(step > 0UL, "Step must be greater than zero", "step");
			Guard.ArgumentValid(to >= from, "Value of to must be greater than or equal to from", "to");
			ulong num = (to - from) / step + 1UL;
			this.data = new object[num];
			ulong num2 = 0UL;
			ulong num3 = from;
			while (num2 < num)
			{
				object[] data = this.data;
				ulong num4 = num2;
				num2 = num4 + 1UL;
				data[(int)(checked((IntPtr)num4))] = num3;
				num3 += step;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004834 File Offset: 0x00002A34
		public RangeAttribute(double from, double to, double step)
		{
			Guard.ArgumentValid((step > 0.0 && to >= from) || (step < 0.0 && to <= from), "Step must be positive with to >= from or negative with to <= from", "step");
			double num = Math.Abs(step);
			double num2 = num / 1000.0;
			int num3 = (int)(Math.Abs(to - from) / num + num2 + 1.0);
			this.data = new object[num3];
			int i = 0;
			double num4 = from;
			while (i < num3)
			{
				this.data[i++] = num4;
				num4 += step;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000048DC File Offset: 0x00002ADC
		public RangeAttribute(float from, float to, float step)
		{
			Guard.ArgumentValid((step > 0f && to >= from) || (step < 0f && to <= from), "Step must be positive with to >= from or negative with to <= from", "step");
			float num = Math.Abs(step);
			float num2 = num / 1000f;
			int num3 = (int)(Math.Abs(to - from) / num + num2 + 1f);
			this.data = new object[num3];
			int i = 0;
			float num4 = from;
			while (i < num3)
			{
				this.data[i++] = num4;
				num4 += step;
			}
		}
	}
}
