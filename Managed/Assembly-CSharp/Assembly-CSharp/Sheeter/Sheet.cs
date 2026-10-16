using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XRL.Rules;

namespace Sheeter
{
	// Token: 0x02000314 RID: 788
	public class Sheet
	{
		// Token: 0x06001CED RID: 7405 RVA: 0x0008E045 File Offset: 0x0008C245
		public Sheet(string path)
		{
			File.CreateText(path).Dispose();
			this.file = new StreamWriter(path, false);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0008E070 File Offset: 0x0008C270
		public void finish()
		{
			this.file.Flush();
			this.file.Close();
			this.file.Dispose();
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0008E093 File Offset: 0x0008C293
		private void write(double val)
		{
			this.file.Write(val);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0008E0A1 File Offset: 0x0008C2A1
		private void write(float val)
		{
			this.file.Write(val);
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0008E0AF File Offset: 0x0008C2AF
		private void write(int val)
		{
			this.file.Write(val);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0008E0BD File Offset: 0x0008C2BD
		private void write(string val)
		{
			if (val != ",")
			{
				val = val.Replace(",", "").Replace("\"", "");
			}
			this.file.Write(val);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0008E0F9 File Offset: 0x0008C2F9
		public void writeColumn(string val)
		{
			if (this.nColumn != 0)
			{
				this.write(",");
			}
			this.nColumn++;
			this.write(val);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0008E123 File Offset: 0x0008C323
		public void writeColumn(int val)
		{
			if (this.nColumn != 0)
			{
				this.write(",");
			}
			this.nColumn++;
			this.write(val);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0008E150 File Offset: 0x0008C350
		public void writeStats(List<int> values)
		{
			if (this.nColumn != 0)
			{
				this.write(",");
			}
			this.nColumn += 3;
			this.write(values.Median<int>());
			this.write(",");
			this.write(values.Average());
			this.write(",");
			this.write(values.StandardDeviation());
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0008E1B8 File Offset: 0x0008C3B8
		public void writeShortStats(List<int> values)
		{
			if (this.nColumn != 0)
			{
				this.write(",");
			}
			this.nColumn++;
			this.write(values.Average());
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0008E1E8 File Offset: 0x0008C3E8
		public void writeDie(string die, int tier, int n = 100, Func<int, int> map = null)
		{
			List<int> list = new List<int>(n);
			if (die.Contains(',') || die.Contains("t"))
			{
				string[] array = die.Split(',', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Replace("(t)", (tier - 1).ToString());
					array[i] = array[i].Replace("(t-1)", (tier - 1).ToString());
					array[i] = array[i].Replace("(t+1)", (tier - 1).ToString());
				}
				for (int j = 0; j < n; j++)
				{
					int num = 0;
					for (int k = 0; k < array.Length; k++)
					{
						num += Stat.Roll(array[j], null);
					}
					list.Add(num);
				}
			}
			else
			{
				for (int l = 0; l < n; l++)
				{
					list.Add(Stat.Roll(die, null));
				}
			}
			if (map != null)
			{
				for (int m = 0; m < list.Count; m++)
				{
					list[m] = map(list[m]);
				}
			}
			if (this.nColumn != 0)
			{
				this.write(",");
			}
			this.nColumn += 4;
			this.write(die);
			this.write(",");
			this.write(list.Median<int>());
			this.write(",");
			this.write(list.Average());
			this.write(",");
			this.write(list.StandardDeviation());
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0008E377 File Offset: 0x0008C577
		public void endColumns()
		{
			this.write("\r\n");
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0008E384 File Offset: 0x0008C584
		public void endRow()
		{
			if (this.nColumn != this.columns.Count)
			{
				throw new Exception("You ended a row before you put all the columns in.");
			}
			this.write("\r\n");
			this.nColumn = 0;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0008E3B6 File Offset: 0x0008C5B6
		public void addColumn(string name)
		{
			if (this.columns.Count != 0)
			{
				this.write(",");
			}
			this.write(name);
			this.columns.Add(new SheetColumn(name));
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0008E3E8 File Offset: 0x0008C5E8
		public void addDieColumn(string name)
		{
			if (this.columns.Count != 0)
			{
				this.write(",");
			}
			this.columns.Add(new SheetColumn(name + "Value"));
			this.write(name + "Value");
			this.write(",");
			this.columns.Add(new SheetColumn(name + "Mean"));
			this.write(name + "Mean");
			this.write(",");
			this.columns.Add(new SheetColumn(name + "Median"));
			this.write(name + "Median");
			this.write(",");
			this.columns.Add(new SheetColumn(name + "SD"));
			this.write(name + "SD");
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0008E4E0 File Offset: 0x0008C6E0
		public void addStatColumn(string name)
		{
			if (this.columns.Count != 0)
			{
				this.write(",");
			}
			this.columns.Add(new SheetColumn(name + "Median"));
			this.write(name + "Median");
			this.write(",");
			this.columns.Add(new SheetColumn(name + "Mean"));
			this.write(name + "Mean");
			this.write(",");
			this.columns.Add(new SheetColumn(name + "SD"));
			this.write(name + "SD");
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0008E5A0 File Offset: 0x0008C7A0
		public void addShortStatColumn(string name)
		{
			if (this.columns.Count != 0)
			{
				this.write(",");
			}
			this.columns.Add(new SheetColumn(name + "Mean"));
			this.write(name + "Mean");
		}

		// Token: 0x04000F61 RID: 3937
		public StreamWriter file;

		// Token: 0x04000F62 RID: 3938
		public List<SheetColumn> columns = new List<SheetColumn>();

		// Token: 0x04000F63 RID: 3939
		private int nColumn;
	}
}
