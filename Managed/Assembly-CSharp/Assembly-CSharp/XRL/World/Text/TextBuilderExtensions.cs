using System;

namespace XRL.World.Text
{
	// Token: 0x02001464 RID: 5220
	public static class TextBuilderExtensions
	{
		// Token: 0x0600CD27 RID: 52519 RVA: 0x004A5D44 File Offset: 0x004A3F44
		public static void AppendTo(this string String, TextBuilder Builder)
		{
			Builder.Append(String);
		}

		// Token: 0x0600CD28 RID: 52520 RVA: 0x004A5D4E File Offset: 0x004A3F4E
		public static void CompoundTo(this string String, TextBuilder Builder)
		{
			Builder.Compound(String);
		}

		// Token: 0x0600CD29 RID: 52521 RVA: 0x004A5D58 File Offset: 0x004A3F58
		public static void CompoundTo(this string String, TextBuilder Builder, string With)
		{
			Builder.Compound(String, With);
		}
	}
}
