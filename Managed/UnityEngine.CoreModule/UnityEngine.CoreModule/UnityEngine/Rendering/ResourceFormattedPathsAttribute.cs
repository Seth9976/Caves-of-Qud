using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004D5 RID: 1237
	public sealed class ResourceFormattedPathsAttribute : ResourcePathsBaseAttribute
	{
		// Token: 0x06002E4C RID: 11852 RVA: 0x00057BB8 File Offset: 0x00055DB8
		public ResourceFormattedPathsAttribute(string pathFormat, int rangeMin, int rangeMax, SearchType location = SearchType.ProjectPath)
			: base(null, false, location)
		{
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x00057BC8 File Offset: 0x00055DC8
		private static string[] CreateFormattedPaths(string format, int rangeMin, int rangeMax)
		{
			string[] array = new string[rangeMax - rangeMin];
			int i = rangeMin;
			int num = 0;
			while (i < rangeMax)
			{
				array[num] = string.Format(format, i);
				i++;
				num++;
			}
			return array;
		}
	}
}
