using System;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000609 RID: 1545
	internal class ArgInfo
	{
		// Token: 0x06003A78 RID: 14968 RVA: 0x000CCEF8 File Offset: 0x000CB0F8
		public ArgInfo(MethodBase method, ArgInfoType type)
		{
			this._method = method;
			ParameterInfo[] parameters = this._method.GetParameters();
			this._paramMap = new int[parameters.Length];
			this._inoutArgCount = 0;
			if (type == ArgInfoType.In)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (!parameters[i].ParameterType.IsByRef)
					{
						int[] paramMap = this._paramMap;
						int num = this._inoutArgCount;
						this._inoutArgCount = num + 1;
						paramMap[num] = i;
					}
				}
				return;
			}
			for (int j = 0; j < parameters.Length; j++)
			{
				if (parameters[j].ParameterType.IsByRef || parameters[j].IsOut)
				{
					int[] paramMap2 = this._paramMap;
					int num = this._inoutArgCount;
					this._inoutArgCount = num + 1;
					paramMap2[num] = j;
				}
			}
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000CCFAD File Offset: 0x000CB1AD
		public int GetInOutArgIndex(int inoutArgNum)
		{
			return this._paramMap[inoutArgNum];
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000CCFB7 File Offset: 0x000CB1B7
		public virtual string GetInOutArgName(int index)
		{
			return this._method.GetParameters()[this._paramMap[index]].Name;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000CCFD2 File Offset: 0x000CB1D2
		public int GetInOutArgCount()
		{
			return this._inoutArgCount;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000CCFDC File Offset: 0x000CB1DC
		public object[] GetInOutArgs(object[] args)
		{
			object[] array = new object[this._inoutArgCount];
			for (int i = 0; i < this._inoutArgCount; i++)
			{
				array[i] = args[this._paramMap[i]];
			}
			return array;
		}

		// Token: 0x0400265C RID: 9820
		private int[] _paramMap;

		// Token: 0x0400265D RID: 9821
		private int _inoutArgCount;

		// Token: 0x0400265E RID: 9822
		private MethodBase _method;
	}
}
