using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using LaundryBear;
using UnityEngine;
using UnityEngine.UIElements;

namespace Platform.IO
{
	// Token: 0x02000334 RID: 820
	[NullableContext(1)]
	[Nullable(0)]
	public static class TestConductor
	{
		// Token: 0x06001EAE RID: 7854 RVA: 0x00096999 File Offset: 0x00094B99
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			ServiceLocator.ManagersInitializedEvent -= TestConductor.OnInitialized;
			ServiceLocator.ManagersInitializedEvent += TestConductor.OnInitialized;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000969C0 File Offset: 0x00094BC0
		private static bool ShouldTest()
		{
			if (!Singleton<ServiceLocator>.HasInstance)
			{
				return false;
			}
			if (ServiceLocator.IsInitialized != ServiceLocator.ServiceInitializationStatus.Ready)
			{
				return false;
			}
			PlatformService platformService;
			if (!ServiceLocator.TryGetService<PlatformService>(out platformService))
			{
				return false;
			}
			if (platformService.InitializationStatus != ServiceLocator.ServiceInitializationStatus.Ready)
			{
				global::UnityEngine.Debug.LogWarning(string.Format("PlatformService is not present but {0}. So we cannot test IO parity.", platformService.InitializationStatus));
				return false;
			}
			return false;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00096A14 File Offset: 0x00094C14
		private static async void OnInitialized()
		{
			await Awaitable.NextFrameAsync(default(CancellationToken));
			if (TestConductor.ShouldTest())
			{
				TestConductor.TestResults testResults;
				TestConductor.PerformParityTest(out testResults);
				if (TestConductor.ResultsDisplayContainerEl != null)
				{
					TestConductor.GenerateUI(TestConductor.ResultsDisplayContainerEl, testResults);
				}
				testResults.LogResults();
			}
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00096A44 File Offset: 0x00094C44
		private static void PerformParityTest(out TestConductor.TestResults results)
		{
			results = new TestConductor.TestResults();
			if (!Singleton<ServiceLocator>.HasInstance)
			{
				results.errorMsg = "ServiceLocator not initialized, please start the game and let it init before running";
				return;
			}
			TestGenerator.Snapshot snapshot;
			if (!TestConductor.LoadSnapshotBaseline(out snapshot))
			{
				results.errorMsg = "Baseline Snapshot not found at " + TestConductor.GetPathSnapshotBaseline() + " so testing cannot be performed";
				return;
			}
			Exception ex;
			TestGenerator.Snapshot snapshot2;
			if (!TestGenerator.GenerateTestSnapshot(new TestGenerator.TestDelegate(TestGenerator.GenerateSnapshotCandidate__GENERATED), out ex, out snapshot2))
			{
				results.errorMsg = string.Format("{0} should had an exception thrown at an unexpected point, the rest of the tests are invalidated\n\"{1}\"", "GenerateSnapshotCandidate__GENERATED", ex);
				return;
			}
			HashSet<string> hashSet = new HashSet<string>();
			hashSet.UnionWith(snapshot.tests.Keys);
			hashSet.UnionWith(snapshot2.tests.Keys);
			foreach (string text in hashSet)
			{
				TestConductor.TestResult testResult = new TestConductor.TestResult(text, snapshot.tests.GetValueOrDefault(text), snapshot2.tests.GetValueOrDefault(text));
				if (testResult.passed)
				{
					results.numberPassed++;
				}
				results.tests.Add(testResult);
			}
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00096B6C File Offset: 0x00094D6C
		private static void GenerateUI(VisualElement containerEl, TestConductor.TestResults results)
		{
			if (!string.IsNullOrEmpty(results.errorMsg))
			{
				containerEl.Add(new HelpBox(results.errorMsg, HelpBoxMessageType.Error));
				return;
			}
			if (results.AllPassed())
			{
				containerEl.Add(new HelpBox("All Tests passed", HelpBoxMessageType.Info));
			}
			else
			{
				containerEl.Add(new HelpBox(string.Format("Not all Tests passed:  ({0}/{1})", results.numberPassed, results.tests.Count), HelpBoxMessageType.Warning));
			}
			using (List<TestConductor.TestResult>.Enumerator enumerator = results.tests.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TestConductor.TestResult test = enumerator.Current;
					VisualElement visualElement = new VisualElement();
					visualElement.style.marginTop = 2f;
					visualElement.style.marginTop = 2f;
					visualElement.style.marginLeft = 2f;
					visualElement.style.flexDirection = FlexDirection.Row;
					visualElement.tooltip = test.Summary();
					VisualElement visualElement2 = new VisualElement();
					visualElement2.style.width = 32f;
					visualElement2.style.height = 20f;
					visualElement2.style.backgroundColor = (test.passed ? Color.green : Color.red);
					visualElement2.style.marginRight = 2f;
					Label label = new Label(test.Summary());
					Button button = new Button(delegate
					{
						global::UnityEngine.Debug.Log(test.Details(false));
					});
					button.text = "Log Results";
					button.tooltip = "This is the entire state of the test. Including full directory state.";
					Button button2 = new Button(delegate
					{
						global::UnityEngine.Debug.Log(test.Details(true));
					});
					button2.text = "Log Manual Hash";
					button2.tooltip = "This excludes full directory state";
					visualElement.Add(visualElement2);
					visualElement.Add(label);
					visualElement.Add(button);
					visualElement.Add(button2);
					containerEl.Add(visualElement);
				}
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00096DA4 File Offset: 0x00094FA4
		[NullableContext(2)]
		public static bool LoadSnapshotBaseline(out TestGenerator.Snapshot snapshot)
		{
			TestGenerator.Snapshot snapshotClosure = null;
			TestConductor.ExecuteAndDisplayIfErrored(delegate
			{
				string pathSnapshotBaseline = TestConductor.GetPathSnapshotBaseline();
				if (!TestConductor.Deserialize(File.ReadAllText(pathSnapshotBaseline), out snapshotClosure))
				{
					throw new Exception("Could not deserialize snapshot from " + pathSnapshotBaseline);
				}
			});
			snapshot = snapshotClosure;
			return snapshot != null;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00096DE0 File Offset: 0x00094FE0
		private static bool ExecuteAndDisplayIfErrored(Action action)
		{
			bool flag;
			try
			{
				action();
				flag = true;
			}
			catch (Exception ex)
			{
				global::UnityEngine.Debug.LogError(ex);
				if (TestConductor.ResultsDisplayContainerEl != null)
				{
					TestConductor.ResultsDisplayContainerEl.Add(new HelpBox(ex.Message, HelpBoxMessageType.Error));
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00096E30 File Offset: 0x00095030
		private static string Serialize(TestGenerator.Snapshot snapshot)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(snapshot.lastSaved.ToLongDateString());
			stringBuilder.Append("\n====\n====\n");
			foreach (KeyValuePair<string, string> keyValuePair in snapshot.tests)
			{
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append("::::\n");
				stringBuilder.Append(keyValuePair.Value);
				stringBuilder.Append("\n====\n====\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00096EDC File Offset: 0x000950DC
		private static bool Deserialize(string hashTxt, out TestGenerator.Snapshot snapshot)
		{
			snapshot = new TestGenerator.Snapshot();
			snapshot.tests = new Dictionary<string, string>();
			string[] array = hashTxt.Split("\n====\n====\n", StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 1)
			{
				return false;
			}
			DateTime dateTime;
			if (!DateTime.TryParse(array[0], out dateTime))
			{
				return false;
			}
			snapshot.lastSaved = dateTime;
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split("::::\n", 2, StringSplitOptions.None);
				if (array2.Length != 2)
				{
					global::UnityEngine.Debug.LogWarning(string.Format("Could not parse line {0} of hash text: {1}", i, array[i]));
				}
				else
				{
					string text = array2[0];
					string text2 = array2[1];
					snapshot.tests[text] = text2;
				}
			}
			return true;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00096F80 File Offset: 0x00095180
		private static string GetPathTestsFolder()
		{
			return Path.Combine(new string[]
			{
				Application.dataPath,
				"LaundryBear",
				"Tests"
			});
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00096FA5 File Offset: 0x000951A5
		private static string GetPathSnapshotFolder()
		{
			return Path.Combine(new string[]
			{
				Application.streamingAssetsPath,
				"ParityTests"
			});
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00096FC2 File Offset: 0x000951C2
		public static string GetPathSnapshotBaseline()
		{
			return Path.Combine(new string[]
			{
				TestConductor.GetPathSnapshotFolder(),
				"ParityTestSnapshotBaseline.txt"
			});
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00096FDF File Offset: 0x000951DF
		public static string GetPathSourceCandidate()
		{
			return Path.Combine(new string[]
			{
				TestConductor.GetPathTestsFolder(),
				"GenerateSnapshotsCandidate.cs"
			});
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00096FFC File Offset: 0x000951FC
		public static string GetPathSourceBaseline()
		{
			return Path.Combine(new string[]
			{
				TestConductor.GetPathTestsFolder(),
				"GenerateSnapshotsBaseline.cs"
			});
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00097019 File Offset: 0x00095219
		[Conditional("UNITY_EDITOR")]
		public static void PerformStaticTests()
		{
			TestConductor.TestMatch();
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00097020 File Offset: 0x00095220
		private static void TestMatch()
		{
			string text = "C:/Users/charl/Desktop/Development/Work/Freehold/Repos/caves-of-qud-console-fork-2/Assets/LaundryBear/Wrappers/Platform.IO.Tests.cs";
			string text2 = "a.json";
			string text3 = "amal/option.xml";
			string text4 = "amal/option.text";
			string text5 = "bam/option.txt";
			string text6 = "vabam/option.txt";
			string text7 = "*option*.xml";
			string text8 = "*option*";
			string text9 = "bam/option.txt";
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text, text7));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text2, text7));
			TestConductor.<TestMatch>g__assertTrue|21_0(Path.IsMatch(text3, text7));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text4, text7));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text5, text7));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text, text8));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text2, text8));
			TestConductor.<TestMatch>g__assertTrue|21_0(Path.IsMatch(text3, text8));
			TestConductor.<TestMatch>g__assertTrue|21_0(Path.IsMatch(text4, text8));
			TestConductor.<TestMatch>g__assertTrue|21_0(Path.IsMatch(text5, text8));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text, text9));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text2, text9));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text3, text9));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text4, text9));
			TestConductor.<TestMatch>g__assertTrue|21_0(Path.IsMatch(text5, text9));
			TestConductor.<TestMatch>g__assertFalse|21_1(Path.IsMatch(text6, text9));
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00097136 File Offset: 0x00095336
		[CompilerGenerated]
		internal static void <TestMatch>g__assertTrue|21_0(bool conditional)
		{
			if (!conditional)
			{
				global::UnityEngine.Debug.LogError("Assert True failed");
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00097145 File Offset: 0x00095345
		[CompilerGenerated]
		internal static void <TestMatch>g__assertFalse|21_1(bool conditional)
		{
			if (conditional)
			{
				global::UnityEngine.Debug.LogError("Assert False failed");
			}
		}

		// Token: 0x04000FFF RID: 4095
		[Nullable(2)]
		public static VisualElement ResultsDisplayContainerEl;

		// Token: 0x04001000 RID: 4096
		public const string ShouldTestInEditorKey = "Should Platform-System Parity Test";

		// Token: 0x04001001 RID: 4097
		private const string elementDelimeter = "\n====\n====\n";

		// Token: 0x04001002 RID: 4098
		private const string keyValueDelimiter = "::::\n";

		// Token: 0x0200172F RID: 5935
		[Nullable(0)]
		public class TestResult
		{
			// Token: 0x0600DCAD RID: 56493 RVA: 0x005094D8 File Offset: 0x005076D8
			public bool IsMissing(string test)
			{
				return string.IsNullOrEmpty(test);
			}

			// Token: 0x0600DCAE RID: 56494 RVA: 0x005094E0 File Offset: 0x005076E0
			public TestResult(string name, string baseline, string candidate)
			{
				this.name = name;
				this.baseline = baseline;
				this.candidate = candidate;
				this.passed = baseline == candidate && !this.IsMissing(baseline) && !this.IsMissing(candidate);
			}

			// Token: 0x0600DCAF RID: 56495 RVA: 0x00509544 File Offset: 0x00507744
			public string Details(bool includeOnlyManualHash = false)
			{
				TestConductor.TestResult.<>c__DisplayClass6_0 CS$<>8__locals1;
				CS$<>8__locals1.includeOnlyManualHash = includeOnlyManualHash;
				string text = string.Empty;
				string text2 = (this.passed ? "(Passed)" : "(Failed)");
				text = string.Concat(new string[] { text, "Test: ", this.name, " ", text2, "\n\n" });
				if (this.passed)
				{
					text += "Baseline & Candidate:\n";
					return text + TestConductor.TestResult.<Details>g__getTestText|6_0(this.baseline, ref CS$<>8__locals1);
				}
				if (this.IsMissing(this.baseline))
				{
					text += "Baseline: Missing";
				}
				else
				{
					text += "Baseline:\n";
					text += TestConductor.TestResult.<Details>g__getTestText|6_0(this.baseline, ref CS$<>8__locals1);
				}
				text += "\n\n";
				if (this.IsMissing(this.candidate))
				{
					text += "Candidate: Missing";
				}
				else
				{
					text += "Candidate:\n";
					text += TestConductor.TestResult.<Details>g__getTestText|6_0(this.candidate, ref CS$<>8__locals1);
				}
				return text;
			}

			// Token: 0x0600DCB0 RID: 56496 RVA: 0x00509658 File Offset: 0x00507858
			public string Summary()
			{
				string text = "passed";
				if (!this.passed)
				{
					text = "failed";
					if (this.IsMissing(this.baseline))
					{
						text += " (baseline snapshot missing)";
					}
					if (this.IsMissing(this.candidate))
					{
						text += " (candidate snapshot missing)";
					}
				}
				return "Test " + this.name + ": " + text;
			}

			// Token: 0x0600DCB1 RID: 56497 RVA: 0x005096C3 File Offset: 0x005078C3
			public override string ToString()
			{
				return this.Summary();
			}

			// Token: 0x0600DCB2 RID: 56498 RVA: 0x005096CC File Offset: 0x005078CC
			[CompilerGenerated]
			internal static string <Details>g__getTestText|6_0(string testSerialized, ref TestConductor.TestResult.<>c__DisplayClass6_0 A_1)
			{
				if (!A_1.includeOnlyManualHash)
				{
					return testSerialized;
				}
				int num = testSerialized.IndexOf("Directory State:", StringComparison.Ordinal);
				if (num == -1)
				{
					global::UnityEngine.Debug.LogWarning("Could not find directory state");
					return testSerialized;
				}
				return testSerialized.Substring(0, num);
			}

			// Token: 0x04005D94 RID: 23956
			public string name;

			// Token: 0x04005D95 RID: 23957
			public bool passed;

			// Token: 0x04005D96 RID: 23958
			public string baseline = string.Empty;

			// Token: 0x04005D97 RID: 23959
			public string candidate = string.Empty;
		}

		// Token: 0x02001730 RID: 5936
		[Nullable(0)]
		public class TestResults
		{
			// Token: 0x0600DCB3 RID: 56499 RVA: 0x00509708 File Offset: 0x00507908
			public bool AllPassed()
			{
				return this.numberPassed == this.tests.Count;
			}

			// Token: 0x0600DCB4 RID: 56500 RVA: 0x0050971D File Offset: 0x0050791D
			public int FailedTestsCount()
			{
				return this.tests.Count - this.numberPassed;
			}

			// Token: 0x0600DCB5 RID: 56501 RVA: 0x00509734 File Offset: 0x00507934
			public void LogResults()
			{
				this.LogSummary();
				string text = "\n========\n======\n=====\n===\n";
				foreach (TestConductor.TestResult testResult in this.tests)
				{
					if (!testResult.passed)
					{
						global::UnityEngine.Debug.LogError(testResult.Details(false) + text);
					}
				}
				foreach (TestConductor.TestResult testResult2 in this.tests)
				{
					if (testResult2.passed)
					{
						global::UnityEngine.Debug.Log(testResult2.Details(false) + text);
					}
				}
			}

			// Token: 0x0600DCB6 RID: 56502 RVA: 0x005097FC File Offset: 0x005079FC
			public void LogSummary()
			{
				if (!string.IsNullOrEmpty(this.errorMsg))
				{
					global::UnityEngine.Debug.LogError("IO Parity Test Could not be performed due to an error: " + this.errorMsg);
					return;
				}
				if (this.AllPassed())
				{
					global::UnityEngine.Debug.Log(string.Format("Passed all IO Parity Tests ({0})", this.tests.Count));
					return;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("Not all IO Parity Tests Passed: ({0}/{1})", this.numberPassed, this.tests.Count));
				foreach (TestConductor.TestResult testResult in this.tests)
				{
					if (!testResult.passed)
					{
						stringBuilder.Append(testResult.Summary());
						stringBuilder.Append('\n');
					}
				}
				global::UnityEngine.Debug.Log(stringBuilder.ToString());
			}

			// Token: 0x0600DCB7 RID: 56503 RVA: 0x005098F0 File Offset: 0x00507AF0
			public string Details()
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (TestConductor.TestResult testResult in this.tests)
				{
					stringBuilder.Append("\n\n========\n========\n\n");
					stringBuilder.Append(testResult.Details(false));
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04005D98 RID: 23960
			public List<TestConductor.TestResult> tests = new List<TestConductor.TestResult>();

			// Token: 0x04005D99 RID: 23961
			public string errorMsg = string.Empty;

			// Token: 0x04005D9A RID: 23962
			public int numberPassed;
		}
	}
}
