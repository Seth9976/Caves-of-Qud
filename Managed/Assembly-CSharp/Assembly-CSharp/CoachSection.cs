using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

// Token: 0x02000159 RID: 345
public class CoachSection
{
	// Token: 0x06000DB0 RID: 3504 RVA: 0x00058B14 File Offset: 0x00056D14
	public CoachSection(string N, bool bTrackGarbage)
	{
		this.Name = N;
		this.LogGarbage = bTrackGarbage;
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x00058B68 File Offset: 0x00056D68
	public long FinalizeTime()
	{
		long num = 0L;
		foreach (string text in this.Sections.Keys)
		{
			long num2 = this.Sections[text].FinalizeTime();
			num += num2;
		}
		return num + this.Timer.ElapsedMilliseconds;
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x00058BE0 File Offset: 0x00056DE0
	public void Log(StringBuilder SB)
	{
		if (SB.Length >= 0)
		{
			SB.Append('\n');
		}
		SB.Append('\t', this.Depth).Append(this.Name).Append(": ")
			.Append(this.FinalizeTime())
			.Append("ms");
		foreach (KeyValuePair<string, CoachSection> keyValuePair in this.Sections)
		{
			keyValuePair.Value.Log(SB);
		}
	}

	// Token: 0x04000981 RID: 2433
	public int Depth;

	// Token: 0x04000982 RID: 2434
	public CoachSection Parent;

	// Token: 0x04000983 RID: 2435
	public Stopwatch Timer = new Stopwatch();

	// Token: 0x04000984 RID: 2436
	public TimeSpan Time = TimeSpan.Zero;

	// Token: 0x04000985 RID: 2437
	public string Name = "";

	// Token: 0x04000986 RID: 2438
	public int Count = 1;

	// Token: 0x04000987 RID: 2439
	public bool LogGarbage;

	// Token: 0x04000988 RID: 2440
	public long StartGarbage;

	// Token: 0x04000989 RID: 2441
	public long TotalGarbage;

	// Token: 0x0400098A RID: 2442
	public Dictionary<string, CoachSection> Sections = new Dictionary<string, CoachSection>();
}
