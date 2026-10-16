using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A7 RID: 679
	internal sealed class ConditionMethodExpression : ConditionExpression
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0007D305 File Offset: 0x0007B505
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0007D30C File Offset: 0x0007B50C
		public ConditionMethodExpression(string conditionMethodName, MethodInfo methodInfo, IEnumerable<ConditionExpression> methodParameters)
		{
			this.MethodInfo = methodInfo;
			this.conditionMethodName = conditionMethodName;
			this.MethodParameters = new List<ConditionExpression>(methodParameters).AsReadOnly();
			ParameterInfo[] parameters = this.MethodInfo.GetParameters();
			if (parameters.Length != 0 && parameters[0].ParameterType == typeof(LogEventInfo))
			{
				this.acceptsLogEvent = true;
			}
			int num = this.MethodParameters.Count;
			if (this.acceptsLogEvent)
			{
				num++;
			}
			int num2 = 0;
			int num3 = 0;
			ParameterInfo[] array = parameters;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IsOptional)
				{
					num3++;
				}
				else
				{
					num2++;
				}
			}
			if (num < num2 || num > parameters.Length)
			{
				string text;
				if (num3 > 0)
				{
					text = string.Format(CultureInfo.InvariantCulture, "Condition method '{0}' requires between {1} and {2} parameters, but passed {3}.", new object[] { conditionMethodName, num2, parameters.Length, num });
				}
				else
				{
					text = string.Format(CultureInfo.InvariantCulture, "Condition method '{0}' requires {1} parameters, but passed {2}.", conditionMethodName, num2, num);
				}
				ConditionMethodExpression.Logger.Error(text, Array.Empty<object>());
				throw new ConditionParseException(text);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0007D435 File Offset: 0x0007B635
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0007D43D File Offset: 0x0007B63D
		public MethodInfo MethodInfo { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0007D446 File Offset: 0x0007B646
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x0007D44E File Offset: 0x0007B64E
		public IList<ConditionExpression> MethodParameters { get; private set; }

		// Token: 0x060017F4 RID: 6132 RVA: 0x0007D458 File Offset: 0x0007B658
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.conditionMethodName);
			stringBuilder.Append("(");
			string text = string.Empty;
			foreach (ConditionExpression conditionExpression in this.MethodParameters)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(conditionExpression);
				text = ", ";
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0007D4EC File Offset: 0x0007B6EC
		protected override object EvaluateNode(LogEventInfo context)
		{
			int num = (this.acceptsLogEvent ? 1 : 0);
			object[] array = new object[this.MethodParameters.Count + num];
			int num2 = 0;
			foreach (ConditionExpression conditionExpression in this.MethodParameters)
			{
				array[num2++ + num] = conditionExpression.Evaluate(context);
			}
			if (this.acceptsLogEvent)
			{
				array[0] = context;
			}
			return this.MethodInfo.DeclaringType.InvokeMember(this.MethodInfo.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.OptionalParamBinding, null, null, array, CultureInfo.InvariantCulture);
		}

		// Token: 0x04000E3E RID: 3646
		private readonly bool acceptsLogEvent;

		// Token: 0x04000E3F RID: 3647
		private readonly string conditionMethodName;
	}
}
