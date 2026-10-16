using System;

namespace UnityEngine.Bindings
{
	// Token: 0x0200036F RID: 879
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	[VisibleToOtherModules]
	internal ref struct OutStringMarshaller
	{
		// Token: 0x060028CF RID: 10447 RVA: 0x0004ECB4 File Offset: 0x0004CEB4
		public unsafe static string GetStringAndDispose(ManagedSpanWrapper managedSpan)
		{
			bool flag = managedSpan.length == 0;
			string text;
			if (flag)
			{
				text = ((managedSpan.begin == null) ? null : string.Empty);
			}
			else
			{
				string text2 = new string((char*)managedSpan.begin, 0, managedSpan.length);
				BindingsAllocator.Free(managedSpan.begin);
				text = text2;
			}
			return text;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x0004ED0C File Offset: 0x0004CF0C
		public static void UpdateStringAndDispose(ManagedSpanWrapper inSpanWrapper, ManagedSpanWrapper outSpanWrapper, ref string outString)
		{
			bool flag = inSpanWrapper.begin != outSpanWrapper.begin;
			if (flag)
			{
				outString = OutStringMarshaller.GetStringAndDispose(outSpanWrapper);
			}
		}
	}
}
