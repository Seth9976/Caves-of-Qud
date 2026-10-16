using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[AttributeUsage(AttributeTargets.Method)]
	public class GUITargetAttribute : Attribute
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000D939 File Offset: 0x0000BB39
		public GUITargetAttribute()
		{
			this.displayMask = -1;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D94A File Offset: 0x0000BB4A
		public GUITargetAttribute(int displayIndex)
		{
			this.displayMask = 1 << displayIndex;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D960 File Offset: 0x0000BB60
		public GUITargetAttribute(int displayIndex, int displayIndex1)
		{
			this.displayMask = (1 << displayIndex) | (1 << displayIndex1);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D980 File Offset: 0x0000BB80
		public GUITargetAttribute(int displayIndex, int displayIndex1, params int[] displayIndexList)
		{
			this.displayMask = (1 << displayIndex) | (1 << displayIndex1);
			for (int i = 0; i < displayIndexList.Length; i++)
			{
				this.displayMask |= 1 << displayIndexList[i];
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		[RequiredByNativeCode]
		private static int GetGUITargetAttrValue(Type klass, string methodName)
		{
			MethodInfo method = klass.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			bool flag = method != null;
			if (flag)
			{
				object[] customAttributes = method.GetCustomAttributes(true);
				bool flag2 = customAttributes != null;
				if (flag2)
				{
					for (int i = 0; i < customAttributes.Length; i++)
					{
						bool flag3 = customAttributes[i].GetType() != typeof(GUITargetAttribute);
						if (!flag3)
						{
							GUITargetAttribute guitargetAttribute = customAttributes[i] as GUITargetAttribute;
							return guitargetAttribute.displayMask;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x040000EB RID: 235
		internal int displayMask;
	}
}
