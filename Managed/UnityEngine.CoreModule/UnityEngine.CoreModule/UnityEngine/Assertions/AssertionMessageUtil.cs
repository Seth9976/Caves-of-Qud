using System;

namespace UnityEngine.Assertions
{
	// Token: 0x0200046D RID: 1133
	internal class AssertionMessageUtil
	{
		// Token: 0x06002CBE RID: 11454 RVA: 0x00054AD8 File Offset: 0x00052CD8
		public static string GetMessage(string failureMessage)
		{
			return UnityString.Format("{0} {1}", new object[] { "Assertion failure.", failureMessage });
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x00054B08 File Offset: 0x00052D08
		public static string GetMessage(string failureMessage, string expected)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("{0}{1}{2} {3}", new object[]
			{
				failureMessage,
				Environment.NewLine,
				"Expected:",
				expected
			}));
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x00054B48 File Offset: 0x00052D48
		public static string GetEqualityMessage(object actual, object expected, bool expectEqual)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Values are {0}equal.", new object[] { expectEqual ? "not " : "" }), UnityString.Format("{0} {2} {1}", new object[]
			{
				actual,
				expected,
				expectEqual ? "==" : "!="
			}));
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x00054BAC File Offset: 0x00052DAC
		public static string NullFailureMessage(object value, bool expectNull)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Value was {0}Null", new object[] { expectNull ? "not " : "" }), UnityString.Format("Value was {0}Null", new object[] { expectNull ? "" : "not " }));
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x00054C08 File Offset: 0x00052E08
		public static string BooleanFailureMessage(bool expected)
		{
			return AssertionMessageUtil.GetMessage("Value was " + (!expected).ToString(), expected.ToString());
		}

		// Token: 0x04000C57 RID: 3159
		private const string k_Expected = "Expected:";

		// Token: 0x04000C58 RID: 3160
		private const string k_AssertionFailed = "Assertion failure.";
	}
}
