using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006ED RID: 1773
	internal abstract class BaseStyleMatcher
	{
		// Token: 0x06003321 RID: 13089
		protected abstract bool MatchKeyword(string keyword);

		// Token: 0x06003322 RID: 13090
		protected abstract bool MatchNumber();

		// Token: 0x06003323 RID: 13091
		protected abstract bool MatchInteger();

		// Token: 0x06003324 RID: 13092
		protected abstract bool MatchLength();

		// Token: 0x06003325 RID: 13093
		protected abstract bool MatchPercentage();

		// Token: 0x06003326 RID: 13094
		protected abstract bool MatchColor();

		// Token: 0x06003327 RID: 13095
		protected abstract bool MatchResource();

		// Token: 0x06003328 RID: 13096
		protected abstract bool MatchUrl();

		// Token: 0x06003329 RID: 13097
		protected abstract bool MatchTime();

		// Token: 0x0600332A RID: 13098
		protected abstract bool MatchAngle();

		// Token: 0x0600332B RID: 13099
		protected abstract bool MatchCustomIdent();

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600332C RID: 13100
		public abstract int valueCount { get; }

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600332D RID: 13101
		public abstract bool isCurrentVariable { get; }

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600332E RID: 13102
		public abstract bool isCurrentComma { get; }

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000C7393 File Offset: 0x000C5593
		public bool hasCurrent
		{
			get
			{
				return this.m_CurrentContext.valueIndex < this.valueCount;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000C73A8 File Offset: 0x000C55A8
		// (set) Token: 0x06003331 RID: 13105 RVA: 0x000C73B5 File Offset: 0x000C55B5
		public int currentIndex
		{
			get
			{
				return this.m_CurrentContext.valueIndex;
			}
			set
			{
				this.m_CurrentContext.valueIndex = value;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000C73C3 File Offset: 0x000C55C3
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x000C73D0 File Offset: 0x000C55D0
		public int matchedVariableCount
		{
			get
			{
				return this.m_CurrentContext.matchedVariableCount;
			}
			set
			{
				this.m_CurrentContext.matchedVariableCount = value;
			}
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000C73DE File Offset: 0x000C55DE
		protected void Initialize()
		{
			this.m_CurrentContext = default(BaseStyleMatcher.MatchContext);
			this.m_ContextStack.Clear();
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000C73FC File Offset: 0x000C55FC
		public void MoveNext()
		{
			bool flag = this.currentIndex + 1 <= this.valueCount;
			if (flag)
			{
				int currentIndex = this.currentIndex;
				this.currentIndex = currentIndex + 1;
			}
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000C7434 File Offset: 0x000C5634
		public void SaveContext()
		{
			this.m_ContextStack.Push(this.m_CurrentContext);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000C7449 File Offset: 0x000C5649
		public void RestoreContext()
		{
			this.m_CurrentContext = this.m_ContextStack.Pop();
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000C745D File Offset: 0x000C565D
		public void DropContext()
		{
			this.m_ContextStack.Pop();
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000C746C File Offset: 0x000C566C
		protected bool Match(Expression exp)
		{
			bool flag = exp.multiplier.type == ExpressionMultiplierType.None;
			bool flag2;
			if (flag)
			{
				flag2 = this.MatchExpression(exp);
			}
			else
			{
				Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.GroupAtLeastOne, "'!' multiplier in syntax expression is not supported");
				flag2 = this.MatchExpressionWithMultiplier(exp);
			}
			return flag2;
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000C74C8 File Offset: 0x000C56C8
		private bool MatchExpression(Expression exp)
		{
			bool flag = false;
			bool flag2 = exp.type == ExpressionType.Combinator;
			if (flag2)
			{
				flag = this.MatchCombinator(exp);
			}
			else
			{
				bool isCurrentVariable = this.isCurrentVariable;
				if (isCurrentVariable)
				{
					flag = true;
					int matchedVariableCount = this.matchedVariableCount;
					this.matchedVariableCount = matchedVariableCount + 1;
				}
				else
				{
					bool flag3 = exp.type == ExpressionType.Data;
					if (flag3)
					{
						flag = this.MatchDataType(exp);
					}
					else
					{
						bool flag4 = exp.type == ExpressionType.Keyword;
						if (flag4)
						{
							flag = this.MatchKeyword(exp.keyword);
						}
					}
				}
				bool flag5 = flag;
				if (flag5)
				{
					this.MoveNext();
				}
			}
			bool flag6 = !flag && !this.hasCurrent && this.matchedVariableCount > 0;
			if (flag6)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000C7584 File Offset: 0x000C5784
		private bool MatchExpressionWithMultiplier(Expression exp)
		{
			bool flag = exp.multiplier.type == ExpressionMultiplierType.OneOrMoreComma;
			bool flag2 = true;
			int min = exp.multiplier.min;
			int max = exp.multiplier.max;
			int num = 0;
			int num2 = 0;
			while (flag2 && this.hasCurrent && num2 < max)
			{
				flag2 = this.MatchExpression(exp);
				bool flag3 = flag2;
				if (flag3)
				{
					num++;
					bool flag4 = flag;
					if (flag4)
					{
						bool flag5 = !this.isCurrentComma;
						if (flag5)
						{
							break;
						}
						this.MoveNext();
					}
				}
				num2++;
			}
			flag2 = num >= min && num <= max;
			bool flag6 = !flag2 && num <= max && this.matchedVariableCount > 0;
			if (flag6)
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000C7650 File Offset: 0x000C5850
		private bool MatchGroup(Expression exp)
		{
			Debug.Assert(exp.subExpressions.Length == 1, "Group has invalid number of sub expressions");
			Expression expression = exp.subExpressions[0];
			return this.Match(expression);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000C7688 File Offset: 0x000C5888
		private bool MatchCombinator(Expression exp)
		{
			this.SaveContext();
			bool flag = false;
			switch (exp.combinator)
			{
			case ExpressionCombinator.Or:
				flag = this.MatchOr(exp);
				break;
			case ExpressionCombinator.OrOr:
				flag = this.MatchOrOr(exp);
				break;
			case ExpressionCombinator.AndAnd:
				flag = this.MatchAndAnd(exp);
				break;
			case ExpressionCombinator.Juxtaposition:
				flag = this.MatchJuxtaposition(exp);
				break;
			case ExpressionCombinator.Group:
				flag = this.MatchGroup(exp);
				break;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.DropContext();
			}
			else
			{
				this.RestoreContext();
			}
			return flag;
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000C7718 File Offset: 0x000C5918
		private bool MatchOr(Expression exp)
		{
			BaseStyleMatcher.MatchContext matchContext = default(BaseStyleMatcher.MatchContext);
			int num = 0;
			for (int i = 0; i < exp.subExpressions.Length; i++)
			{
				this.SaveContext();
				int currentIndex = this.currentIndex;
				bool flag = this.Match(exp.subExpressions[i]);
				int num2 = this.currentIndex - currentIndex;
				bool flag2 = flag && num2 > num;
				if (flag2)
				{
					num = num2;
					matchContext = this.m_CurrentContext;
				}
				this.RestoreContext();
			}
			bool flag3 = num > 0;
			bool flag4;
			if (flag3)
			{
				this.m_CurrentContext = matchContext;
				flag4 = true;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000C77B8 File Offset: 0x000C59B8
		private bool MatchOrOr(Expression exp)
		{
			int num = this.MatchMany(exp);
			return num > 0;
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000C77D8 File Offset: 0x000C59D8
		private bool MatchAndAnd(Expression exp)
		{
			int num = this.MatchMany(exp);
			int num2 = exp.subExpressions.Length;
			return num == num2;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000C7800 File Offset: 0x000C5A00
		private unsafe int MatchMany(Expression exp)
		{
			BaseStyleMatcher.MatchContext matchContext = default(BaseStyleMatcher.MatchContext);
			int num = 0;
			int num2 = -1;
			int num3 = exp.subExpressions.Length;
			int* ptr;
			checked
			{
				ptr = stackalloc int[unchecked((UIntPtr)num3) * 4];
			}
			do
			{
				this.SaveContext();
				num2++;
				for (int i = 0; i < num3; i++)
				{
					int num4 = ((num2 > 0) ? ((num2 + i) % num3) : i);
					ptr[i] = num4;
				}
				int num5 = this.MatchManyByOrder(exp, ptr);
				bool flag = num5 > num;
				if (flag)
				{
					num = num5;
					matchContext = this.m_CurrentContext;
				}
				this.RestoreContext();
			}
			while (num < num3 && num2 < num3);
			bool flag2 = num > 0;
			if (flag2)
			{
				this.m_CurrentContext = matchContext;
			}
			return num;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000C78C0 File Offset: 0x000C5AC0
		private unsafe int MatchManyByOrder(Expression exp, int* matchOrder)
		{
			int num = exp.subExpressions.Length;
			int* ptr;
			int num2;
			int num3;
			int num4;
			checked
			{
				ptr = stackalloc int[unchecked((UIntPtr)num) * 4];
				num2 = 0;
				num3 = 0;
				num4 = 0;
			}
			while (num4 < num && num2 + num3 < num)
			{
				int num5 = matchOrder[num4];
				bool flag = false;
				for (int i = 0; i < num2; i++)
				{
					bool flag2 = ptr[i] == num5;
					if (flag2)
					{
						flag = true;
						break;
					}
				}
				bool flag3 = false;
				bool flag4 = !flag;
				if (flag4)
				{
					flag3 = this.Match(exp.subExpressions[num5]);
				}
				bool flag5 = flag3;
				if (flag5)
				{
					bool flag6 = num3 == this.matchedVariableCount;
					if (flag6)
					{
						ptr[num2] = num5;
						num2++;
					}
					else
					{
						num3 = this.matchedVariableCount;
					}
					num4 = 0;
				}
				else
				{
					num4++;
				}
			}
			return num2 + num3;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000C79AC File Offset: 0x000C5BAC
		private bool MatchJuxtaposition(Expression exp)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < exp.subExpressions.Length)
			{
				flag = this.Match(exp.subExpressions[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000C79F0 File Offset: 0x000C5BF0
		private bool MatchDataType(Expression exp)
		{
			bool flag = false;
			bool hasCurrent = this.hasCurrent;
			if (hasCurrent)
			{
				switch (exp.dataType)
				{
				case DataType.Number:
					flag = this.MatchNumber();
					break;
				case DataType.Integer:
					flag = this.MatchInteger();
					break;
				case DataType.Length:
					flag = this.MatchLength();
					break;
				case DataType.Percentage:
					flag = this.MatchPercentage();
					break;
				case DataType.Color:
					flag = this.MatchColor();
					break;
				case DataType.Resource:
					flag = this.MatchResource();
					break;
				case DataType.Url:
					flag = this.MatchUrl();
					break;
				case DataType.Time:
					flag = this.MatchTime();
					break;
				case DataType.Angle:
					flag = this.MatchAngle();
					break;
				case DataType.CustomIdent:
					flag = this.MatchCustomIdent();
					break;
				}
			}
			return flag;
		}

		// Token: 0x04001A1E RID: 6686
		protected static readonly Regex s_CustomIdentRegex = new Regex("^-?[_a-z][_a-z0-9-]*", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04001A1F RID: 6687
		private Stack<BaseStyleMatcher.MatchContext> m_ContextStack = new Stack<BaseStyleMatcher.MatchContext>();

		// Token: 0x04001A20 RID: 6688
		private BaseStyleMatcher.MatchContext m_CurrentContext;

		// Token: 0x020006EE RID: 1774
		private struct MatchContext
		{
			// Token: 0x04001A21 RID: 6689
			public int valueIndex;

			// Token: 0x04001A22 RID: 6690
			public int matchedVariableCount;
		}
	}
}
