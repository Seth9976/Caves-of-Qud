using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace XRL.Tests
{
	// Token: 0x020005E2 RID: 1506
	public class VersionTests
	{
		// Token: 0x060040BF RID: 16575 RVA: 0x00140831 File Offset: 0x0013EA31
		[SetUp]
		public void Setup()
		{
			this.Builder.Clear();
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x00140840 File Offset: 0x0013EA40
		[TestCase("7.5")]
		[TestCase("0.14.0")]
		[TestCase("1.8.106")]
		[TestCase("2.0.214.66")]
		public void TestParse(string Version)
		{
			Version version = new Version(Version);
			Version version2 = new Version(Version);
			Assert.True(version == version2, "System.Version == XRL.Version", Array.Empty<object>());
			int num = Version.Count<char>((char x) => x == '.') + 1;
			Assert.AreEqual(Version, version.ToString(num), "Input == ToString()", Array.Empty<object>());
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x001408B8 File Offset: 0x0013EAB8
		[TestCase("8000", "*", true)]
		[TestCase("1.0", "1.0.*", true)]
		[TestCase("1.0", "=1", true)]
		[TestCase("1.1", "=1.0", false)]
		[TestCase("1.1", "1.0.*", false)]
		[TestCase("5.0.700.345345", "2.0 - *", true)]
		[TestCase("7.5", ">7.4", true)]
		[TestCase("7.4.12", ">7.4", false)]
		[TestCase("7.4.12", ">7.4.2 <7.5", true)]
		[TestCase("40.8.89", ">37 <=40.8", true)]
		[TestCase("40.8.12", ">0.* >1.1.1.1 <40.8", false)]
		[TestCase("40.8.66", "^40.8", true)]
		[TestCase("41.0.0", "^40.8", false)]
		[TestCase("0.0.1.67", "^0.0.1", true)]
		[TestCase("0.0.2.4", "^0.0.1", false)]
		[TestCase("2.0.214.66", "<2.0.4 || 2.0.214.66 - 2.0.215", true)]
		[TestCase("2.0.11", ">2.0.11 || 2.0.7.67 - 2.0.10.*", false)]
		[TestCase("2.0.10.7", "2.0.7.67 - 2.0.10.7", true)]
		public void TestEqualsSemantic(string Version, string Semantic, bool Expected)
		{
			Version version = new Version(Version);
			bool flag = version.EqualsSemantic(Semantic);
			StringBuilder builder = this.Builder;
			foreach (ReadOnlySpan<char> readOnlySpan in Semantic.DelimitedBy("||"))
			{
				ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Trim();
				if (readOnlySpan2.IndexOf('-') != -1)
				{
					builder.Append("\n\"").Append(readOnlySpan2).Append("\" : ")
						.Append(version.EqualsSemantic(readOnlySpan2));
				}
				else
				{
					foreach (ReadOnlySpan<char> readOnlySpan3 in readOnlySpan2.DelimitedBy(' '))
					{
						ReadOnlySpan<char> readOnlySpan4 = readOnlySpan3.Trim();
						builder.Append("\n\"").Append(readOnlySpan4).Append("\" : ")
							.Append(version.EqualsSemantic(readOnlySpan4));
					}
				}
			}
			string text = builder.ToString();
			Assert.AreEqual(Expected, flag, text, Array.Empty<object>());
			Assert.Pass(text);
		}

		// Token: 0x04001FC1 RID: 8129
		private StringBuilder Builder = new StringBuilder();
	}
}
