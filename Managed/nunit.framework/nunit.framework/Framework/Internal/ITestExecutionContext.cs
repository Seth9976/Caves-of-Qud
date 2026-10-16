using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal.Execution;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200007D RID: 125
	public interface ITestExecutionContext
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600049C RID: 1180
		// (set) Token: 0x0600049D RID: 1181
		Test CurrentTest { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600049E RID: 1182
		// (set) Token: 0x0600049F RID: 1183
		DateTime StartTime { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004A0 RID: 1184
		// (set) Token: 0x060004A1 RID: 1185
		long StartTicks { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004A2 RID: 1186
		// (set) Token: 0x060004A3 RID: 1187
		TestResult CurrentResult { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004A4 RID: 1188
		TextWriter OutWriter { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004A5 RID: 1189
		// (set) Token: 0x060004A6 RID: 1190
		object TestObject { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004A7 RID: 1191
		// (set) Token: 0x060004A8 RID: 1192
		string WorkDirectory { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004A9 RID: 1193
		// (set) Token: 0x060004AA RID: 1194
		bool StopOnError { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004AB RID: 1195
		// (set) Token: 0x060004AC RID: 1196
		TestExecutionStatus ExecutionStatus { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004AD RID: 1197
		// (set) Token: 0x060004AE RID: 1198
		IWorkItemDispatcher Dispatcher { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004AF RID: 1199
		// (set) Token: 0x060004B0 RID: 1200
		ParallelScope ParallelScope { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004B1 RID: 1201
		string WorkerId { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004B2 RID: 1202
		Randomizer RandomGenerator { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004B3 RID: 1203
		// (set) Token: 0x060004B4 RID: 1204
		int TestCaseTimeout { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004B5 RID: 1205
		List<ITestAction> UpstreamActions { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004B6 RID: 1206
		// (set) Token: 0x060004B7 RID: 1207
		CultureInfo CurrentCulture { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004B8 RID: 1208
		// (set) Token: 0x060004B9 RID: 1209
		CultureInfo CurrentUICulture { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004BA RID: 1210
		ValueFormatter CurrentValueFormatter { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004BB RID: 1211
		// (set) Token: 0x060004BC RID: 1212
		bool IsSingleThreaded { get; set; }

		// Token: 0x060004BD RID: 1213
		void IncrementAssertCount();

		// Token: 0x060004BE RID: 1214
		void AddFormatter(ValueFormatterFactory formatterFactory);
	}
}
