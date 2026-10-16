using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000002 RID: 2
public class CsvTable : IEnumerable<CsvTable.Row>, IEnumerable
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public CsvTable(string content, params string[] delimeter)
	{
		this.m_rows = new List<CsvTable.Row>();
		using (StringReader stringReader = new StringReader(content))
		{
			string[] array = stringReader.ReadLine().Split(delimeter, StringSplitOptions.None);
			for (string text = stringReader.ReadLine(); text != null; text = stringReader.ReadLine())
			{
				string[] array2 = text.Split(delimeter, StringSplitOptions.None);
				CsvTable.Row row = new CsvTable.Row(array, array2);
				this.m_rows.Add(row);
			}
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020D4 File Offset: 0x000002D4
	public IEnumerator<CsvTable.Row> GetEnumerator()
	{
		return new CsvTable.CsvEnumerator(new List<CsvTable.Row>(this.m_rows));
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000020E6 File Offset: 0x000002E6
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new CsvTable.CsvEnumerator(new List<CsvTable.Row>(this.m_rows));
	}

	// Token: 0x04000001 RID: 1
	private List<CsvTable.Row> m_rows;

	// Token: 0x02000017 RID: 23
	public struct Row
	{
		// Token: 0x1700001A RID: 26
		public string this[string header]
		{
			get
			{
				return this.m_values[header];
			}
			private set
			{
				this.m_values[header] = value;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000031C0 File Offset: 0x000013C0
		public Row(string[] headers, string[] elements)
		{
			this.m_values = new Dictionary<string, string>();
			for (int i = 0; i < headers.Length; i++)
			{
				this.m_values.Add(headers[i], elements[i]);
			}
		}

		// Token: 0x04000034 RID: 52
		private Dictionary<string, string> m_values;
	}

	// Token: 0x02000018 RID: 24
	private class CsvEnumerator : IEnumerator<CsvTable.Row>, IEnumerator, IDisposable
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000031F7 File Offset: 0x000013F7
		public CsvTable.Row Current
		{
			get
			{
				return this.m_rows[this.m_currentIndex];
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000320A File Offset: 0x0000140A
		object IEnumerator.Current
		{
			get
			{
				return this.m_rows[this.m_currentIndex];
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003222 File Offset: 0x00001422
		public void Dispose()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003224 File Offset: 0x00001424
		public bool MoveNext()
		{
			this.m_currentIndex++;
			return this.m_currentIndex < this.m_rows.Count;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003247 File Offset: 0x00001447
		public void Reset()
		{
			this.m_currentIndex = -1;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003250 File Offset: 0x00001450
		public CsvEnumerator(List<CsvTable.Row> rows)
		{
			this.m_rows = rows;
		}

		// Token: 0x04000035 RID: 53
		private List<CsvTable.Row> m_rows;

		// Token: 0x04000036 RID: 54
		private int m_currentIndex = -1;
	}
}
