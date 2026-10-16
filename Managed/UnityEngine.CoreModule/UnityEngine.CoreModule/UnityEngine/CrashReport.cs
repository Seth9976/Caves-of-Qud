using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000134 RID: 308
	[NativeHeader("Runtime/Export/CrashReport/CrashReport.bindings.h")]
	public sealed class CrashReport
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0000F170 File Offset: 0x0000D370
		private static int Compare(CrashReport c1, CrashReport c2)
		{
			long ticks = c1.time.Ticks;
			long ticks2 = c2.time.Ticks;
			bool flag = ticks > ticks2;
			int num;
			if (flag)
			{
				num = 1;
			}
			else
			{
				bool flag2 = ticks < ticks2;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		private static void PopulateReports()
		{
			object obj = CrashReport.reportsLock;
			lock (obj)
			{
				bool flag2 = CrashReport.internalReports != null;
				if (!flag2)
				{
					string[] reports = CrashReport.GetReports();
					CrashReport.internalReports = new List<CrashReport>(reports.Length);
					foreach (string text in reports)
					{
						double num;
						string reportData = CrashReport.GetReportData(text, out num);
						DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds(num);
						CrashReport.internalReports.Add(new CrashReport(text, dateTime, reportData));
					}
					CrashReport.internalReports.Sort(new Comparison<CrashReport>(CrashReport.Compare));
				}
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0000F28C File Offset: 0x0000D48C
		public static CrashReport[] reports
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				CrashReport[] array;
				lock (obj)
				{
					array = CrashReport.internalReports.ToArray();
				}
				return array;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public static CrashReport lastReport
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					bool flag2 = CrashReport.internalReports.Count > 0;
					if (flag2)
					{
						return CrashReport.internalReports[CrashReport.internalReports.Count - 1];
					}
				}
				return null;
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0000F350 File Offset: 0x0000D550
		public static void RemoveAll()
		{
			foreach (CrashReport crashReport in CrashReport.reports)
			{
				crashReport.Remove();
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0000F37E File Offset: 0x0000D57E
		private CrashReport(string id, DateTime time, string text)
		{
			this.id = id;
			this.time = time;
			this.text = text;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public void Remove()
		{
			bool flag = CrashReport.RemoveReport(this.id);
			if (flag)
			{
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					CrashReport.internalReports.Remove(this);
				}
			}
		}

		// Token: 0x060008F4 RID: 2292
		[FreeFunction(Name = "CrashReport_Bindings::GetReports", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetReports();

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		[FreeFunction(Name = "CrashReport_Bindings::GetReportData", IsThreadSafe = true)]
		private unsafe static string GetReportData(string id, out double secondsSinceUnixEpoch)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(id, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = id.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				CrashReport.GetReportData_Injected(ref managedSpanWrapper, out secondsSinceUnixEpoch, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000F460 File Offset: 0x0000D660
		[FreeFunction(Name = "CrashReport_Bindings::RemoveReport", IsThreadSafe = true)]
		private unsafe static bool RemoveReport(string id)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(id, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = id.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = CrashReport.RemoveReport_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060008F8 RID: 2296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetReportData_Injected(ref ManagedSpanWrapper id, out double secondsSinceUnixEpoch, out ManagedSpanWrapper ret);

		// Token: 0x060008F9 RID: 2297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveReport_Injected(ref ManagedSpanWrapper id);

		// Token: 0x04000413 RID: 1043
		private static List<CrashReport> internalReports;

		// Token: 0x04000414 RID: 1044
		private static object reportsLock = new object();

		// Token: 0x04000415 RID: 1045
		private readonly string id;

		// Token: 0x04000416 RID: 1046
		public readonly DateTime time;

		// Token: 0x04000417 RID: 1047
		public readonly string text;
	}
}
