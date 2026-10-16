using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000128 RID: 296
	[StructLayout(2)]
	public struct PropVariant
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x000136AC File Offset: 0x000118AC
		public static PropVariant FromLong(long value)
		{
			return new PropVariant
			{
				vt = 20,
				hVal = value
			};
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000136D4 File Offset: 0x000118D4
		private byte[] GetBlob()
		{
			byte[] array = new byte[this.blobVal.Length];
			Marshal.Copy(this.blobVal.Data, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00013708 File Offset: 0x00011908
		public T[] GetBlobAsArrayOf<T>()
		{
			int length = this.blobVal.Length;
			T t = (T)((object)Activator.CreateInstance(typeof(T)));
			int num = Marshal.SizeOf(t);
			if (length % num != 0)
			{
				throw new InvalidDataException(string.Format("Blob size {0} not a multiple of struct size {1}", length, num));
			}
			int num2 = length / num;
			T[] array = new T[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i] = (T)((object)Activator.CreateInstance(typeof(T)));
				Marshal.PtrToStructure(new IntPtr((long)this.blobVal.Data + (long)(i * num)), array[i]);
			}
			return array;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000137CC File Offset: 0x000119CC
		public VarEnum DataType
		{
			get
			{
				return this.vt;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000137D4 File Offset: 0x000119D4
		public object Value
		{
			get
			{
				VarEnum dataType = this.DataType;
				VarEnum varEnum = dataType;
				if (varEnum > 22)
				{
					if (varEnum <= 65)
					{
						if (varEnum == 31)
						{
							return Marshal.PtrToStringUni(this.pointerValue);
						}
						if (varEnum != 65)
						{
							goto IL_012F;
						}
					}
					else
					{
						if (varEnum == 72)
						{
							return (Guid)Marshal.PtrToStructure(this.pointerValue, typeof(Guid));
						}
						if (varEnum != 4113)
						{
							goto IL_012F;
						}
					}
					return this.GetBlob();
				}
				switch (varEnum)
				{
				case 2:
					return this.iVal;
				case 3:
					return this.lVal;
				default:
					if (varEnum != 11)
					{
						switch (varEnum)
						{
						case 16:
							return this.bVal;
						case 19:
							return this.ulVal;
						case 20:
							return this.hVal;
						case 21:
							return this.uhVal;
						case 22:
							return this.iVal;
						}
					}
					else
					{
						switch (this.boolVal)
						{
						case -1:
							return true;
						case 0:
							return false;
						default:
							throw new NotSupportedException("PropVariant VT_BOOL must be either -1 or 0");
						}
					}
					break;
				}
				IL_012F:
				throw new NotImplementedException("PropVariant " + dataType.ToString());
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001392A File Offset: 0x00011B2A
		public void Clear()
		{
			PropVariant.PropVariantClear(ref this);
		}

		// Token: 0x0600064E RID: 1614
		[DllImport("ole32.dll")]
		private static extern int PropVariantClear(ref PropVariant pvar);

		// Token: 0x040006FA RID: 1786
		[FieldOffset(0)]
		private short vt;

		// Token: 0x040006FB RID: 1787
		[FieldOffset(2)]
		private short wReserved1;

		// Token: 0x040006FC RID: 1788
		[FieldOffset(4)]
		private short wReserved2;

		// Token: 0x040006FD RID: 1789
		[FieldOffset(6)]
		private short wReserved3;

		// Token: 0x040006FE RID: 1790
		[FieldOffset(8)]
		private sbyte cVal;

		// Token: 0x040006FF RID: 1791
		[FieldOffset(8)]
		private byte bVal;

		// Token: 0x04000700 RID: 1792
		[FieldOffset(8)]
		private short iVal;

		// Token: 0x04000701 RID: 1793
		[FieldOffset(8)]
		private ushort uiVal;

		// Token: 0x04000702 RID: 1794
		[FieldOffset(8)]
		private int lVal;

		// Token: 0x04000703 RID: 1795
		[FieldOffset(8)]
		private uint ulVal;

		// Token: 0x04000704 RID: 1796
		[FieldOffset(8)]
		private int intVal;

		// Token: 0x04000705 RID: 1797
		[FieldOffset(8)]
		private uint uintVal;

		// Token: 0x04000706 RID: 1798
		[FieldOffset(8)]
		private long hVal;

		// Token: 0x04000707 RID: 1799
		[FieldOffset(8)]
		private long uhVal;

		// Token: 0x04000708 RID: 1800
		[FieldOffset(8)]
		private float fltVal;

		// Token: 0x04000709 RID: 1801
		[FieldOffset(8)]
		private double dblVal;

		// Token: 0x0400070A RID: 1802
		[FieldOffset(8)]
		private short boolVal;

		// Token: 0x0400070B RID: 1803
		[FieldOffset(8)]
		private int scode;

		// Token: 0x0400070C RID: 1804
		[FieldOffset(8)]
		private DateTime date;

		// Token: 0x0400070D RID: 1805
		[FieldOffset(8)]
		private FILETIME filetime;

		// Token: 0x0400070E RID: 1806
		[FieldOffset(8)]
		private Blob blobVal;

		// Token: 0x0400070F RID: 1807
		[FieldOffset(8)]
		private IntPtr pointerValue;
	}
}
