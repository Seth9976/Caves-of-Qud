using System;
using NUnit.Framework;
using XRL.World.Text;

namespace ConsoleLib.Console
{
	// Token: 0x02001550 RID: 5456
	public class MarkupSmokeTest
	{
		// Token: 0x0600D4C9 RID: 54473 RVA: 0x004F164C File Offset: 0x004EF84C
		[TestCase("no markup", "no markup")]
		[TestCase("&ykeeps starting y", "&ykeeps starting y")]
		[TestCase("{{y|keeps starting y}}", "&ykeeps starting y")]
		[TestCase("small {{g|mossy}} tube", "&ysmall &gmossy&y tube")]
		[TestCase("{{|small {{g|mossy}} tube}}", "small &gmossy&y tube")]
		[TestCase("{{c|&Kwant black}}", "&Kwant black")]
		[TestCase("{{y|want grey {{Y|white}} grey &Kblack}}", "&ywant grey &Ywhite&y grey &Kblack")]
		[TestCase("{{Y-y-Y-Y-Y-Y-Y-Y sequence|Nursling of Nuptial}}", "&YN&yu&Yrsling &yo&Yf Nupti&ya&Yl")]
		[TestCase("{{zetachrome|&Kwant bl&Kack zeta", "&Kwa&Mn&Yt &Cb&cl&Kac&Ck &Yz&Met&ma")]
		public void Transform(string input, string expected)
		{
			Assert.AreEqual(expected, Markup.Transform(input, false));
			using (TextBuilder textBuilder = TextBuilder.Get(input))
			{
				using (TextBuilder textBuilder2 = TextBuilder.Get(expected.Length))
				{
					ZMarkup.Transform(textBuilder, textBuilder2, false);
					Assert.AreEqual(expected, textBuilder2.ToString());
				}
			}
		}

		// Token: 0x0600D4CA RID: 54474 RVA: 0x004F16C4 File Offset: 0x004EF8C4
		[TestCase("no markup", "no markup")]
		[TestCase("&ykeeps starting y", "&ykeeps starting y")]
		[TestCase("{{y|keeps starting y}}", "keeps starting y")]
		[TestCase("small {{g|mossy}} tube", "small mossy tube")]
		[TestCase("{{|small {{g|mossy}} tube}}", "small mossy tube")]
		[TestCase("{{c|&Kwant black}}", "&Kwant black")]
		[TestCase("{{y|want grey {{Y|white}} grey &Kblack}}", "want grey white grey &Kblack")]
		[TestCase("{{zetachrome|&Kwant bl&Kack zeta", "&Kwant bl&Kack zeta")]
		public void Strip(string input, string expected)
		{
			Assert.AreEqual(expected, Markup.Strip(input));
		}
	}
}
