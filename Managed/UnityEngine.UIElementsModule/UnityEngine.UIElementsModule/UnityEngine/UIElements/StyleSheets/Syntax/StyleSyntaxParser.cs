using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020006FB RID: 1787
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StyleSyntaxParser
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x000C8C80 File Offset: 0x000C6E80
		public Expression Parse(string syntax)
		{
			bool flag = string.IsNullOrEmpty(syntax);
			Expression expression;
			if (flag)
			{
				expression = null;
			}
			else
			{
				Expression expression2 = null;
				bool flag2 = !this.m_ParsedExpressionCache.TryGetValue(syntax, out expression2);
				if (flag2)
				{
					StyleSyntaxTokenizer styleSyntaxTokenizer = new StyleSyntaxTokenizer();
					styleSyntaxTokenizer.Tokenize(syntax);
					try
					{
						expression2 = this.ParseExpression(styleSyntaxTokenizer);
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
					this.m_ParsedExpressionCache[syntax] = expression2;
				}
				expression = expression2;
			}
			return expression;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000C8D04 File Offset: 0x000C6F04
		private Expression ParseExpression(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			while (!StyleSyntaxParser.IsExpressionEnd(styleSyntaxToken))
			{
				bool flag = styleSyntaxToken.type == StyleSyntaxTokenType.String || styleSyntaxToken.type == StyleSyntaxTokenType.LessThan;
				Expression expression;
				if (flag)
				{
					expression = this.ParseTerm(tokenizer);
				}
				else
				{
					bool flag2 = styleSyntaxToken.type == StyleSyntaxTokenType.OpenBracket;
					if (!flag2)
					{
						throw new Exception(string.Format("Unexpected token '{0}' in expression", styleSyntaxToken.type));
					}
					expression = this.ParseGroup(tokenizer);
				}
				this.m_ExpressionStack.Push(expression);
				ExpressionCombinator expressionCombinator = this.ParseCombinatorType(tokenizer);
				bool flag3 = expressionCombinator > ExpressionCombinator.None;
				if (flag3)
				{
					bool flag4 = this.m_CombinatorStack.Count > 0;
					if (flag4)
					{
						ExpressionCombinator expressionCombinator2 = this.m_CombinatorStack.Peek();
						int num = (int)expressionCombinator2;
						int num2 = (int)expressionCombinator;
						while (num > num2 && expressionCombinator2 != ExpressionCombinator.Group)
						{
							this.ProcessCombinatorStack();
							expressionCombinator2 = ((this.m_CombinatorStack.Count > 0) ? this.m_CombinatorStack.Peek() : ExpressionCombinator.None);
							num = (int)expressionCombinator2;
						}
					}
					this.m_CombinatorStack.Push(expressionCombinator);
				}
				styleSyntaxToken = tokenizer.current;
			}
			while (this.m_CombinatorStack.Count > 0)
			{
				ExpressionCombinator expressionCombinator3 = this.m_CombinatorStack.Peek();
				bool flag5 = expressionCombinator3 == ExpressionCombinator.Group;
				if (flag5)
				{
					this.m_CombinatorStack.Pop();
					break;
				}
				this.ProcessCombinatorStack();
			}
			return this.m_ExpressionStack.Pop();
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000C8E90 File Offset: 0x000C7090
		private void ProcessCombinatorStack()
		{
			ExpressionCombinator expressionCombinator = this.m_CombinatorStack.Pop();
			Expression expression = this.m_ExpressionStack.Pop();
			Expression expression2 = this.m_ExpressionStack.Pop();
			this.m_ProcessExpressionList.Clear();
			this.m_ProcessExpressionList.Add(expression2);
			this.m_ProcessExpressionList.Add(expression);
			while (this.m_CombinatorStack.Count > 0 && expressionCombinator == this.m_CombinatorStack.Peek())
			{
				Expression expression3 = this.m_ExpressionStack.Pop();
				this.m_ProcessExpressionList.Insert(0, expression3);
				this.m_CombinatorStack.Pop();
			}
			Expression expression4 = new Expression(ExpressionType.Combinator);
			expression4.combinator = expressionCombinator;
			expression4.subExpressions = this.m_ProcessExpressionList.ToArray();
			this.m_ExpressionStack.Push(expression4);
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000C8F68 File Offset: 0x000C7168
		private Expression ParseTerm(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken current = tokenizer.current;
			bool flag = current.type == StyleSyntaxTokenType.LessThan;
			Expression expression;
			if (flag)
			{
				expression = this.ParseDataType(tokenizer);
			}
			else
			{
				bool flag2 = current.type == StyleSyntaxTokenType.String;
				if (!flag2)
				{
					throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected term token", current.type));
				}
				expression = new Expression(ExpressionType.Keyword);
				expression.keyword = current.text.ToLower();
				tokenizer.MoveNext();
			}
			this.ParseMultiplier(tokenizer, ref expression.multiplier);
			return expression;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000C8FFC File Offset: 0x000C71FC
		private ExpressionCombinator ParseCombinatorType(StyleSyntaxTokenizer tokenizer)
		{
			ExpressionCombinator expressionCombinator = ExpressionCombinator.None;
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			while (!StyleSyntaxParser.IsExpressionEnd(styleSyntaxToken) && expressionCombinator == ExpressionCombinator.None)
			{
				StyleSyntaxToken styleSyntaxToken2 = tokenizer.PeekNext();
				switch (styleSyntaxToken.type)
				{
				case StyleSyntaxTokenType.Space:
				{
					bool flag = !StyleSyntaxParser.IsCombinator(styleSyntaxToken2) && styleSyntaxToken2.type != StyleSyntaxTokenType.CloseBracket;
					if (flag)
					{
						expressionCombinator = ExpressionCombinator.Juxtaposition;
					}
					break;
				}
				case StyleSyntaxTokenType.SingleBar:
					expressionCombinator = ExpressionCombinator.Or;
					break;
				case StyleSyntaxTokenType.DoubleBar:
					expressionCombinator = ExpressionCombinator.OrOr;
					break;
				case StyleSyntaxTokenType.DoubleAmpersand:
					expressionCombinator = ExpressionCombinator.AndAnd;
					break;
				default:
					throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected combinator token", styleSyntaxToken.type));
				}
				styleSyntaxToken = tokenizer.MoveNext();
			}
			StyleSyntaxParser.EatSpace(tokenizer);
			return expressionCombinator;
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000C90C0 File Offset: 0x000C72C0
		private Expression ParseGroup(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = styleSyntaxToken.type != StyleSyntaxTokenType.OpenBracket;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in group expression. Expected '[' token", styleSyntaxToken.type));
			}
			this.m_CombinatorStack.Push(ExpressionCombinator.Group);
			tokenizer.MoveNext();
			StyleSyntaxParser.EatSpace(tokenizer);
			Expression expression = this.ParseExpression(tokenizer);
			styleSyntaxToken = tokenizer.current;
			bool flag2 = styleSyntaxToken.type != StyleSyntaxTokenType.CloseBracket;
			if (flag2)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in group expression. Expected ']' token", styleSyntaxToken.type));
			}
			tokenizer.MoveNext();
			Expression expression2 = new Expression(ExpressionType.Combinator);
			expression2.combinator = ExpressionCombinator.Group;
			expression2.subExpressions = new Expression[] { expression };
			this.ParseMultiplier(tokenizer, ref expression2.multiplier);
			return expression2;
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000C9194 File Offset: 0x000C7394
		private Expression ParseDataType(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = styleSyntaxToken.type != StyleSyntaxTokenType.LessThan;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in data type expression. Expected '<' token", styleSyntaxToken.type));
			}
			styleSyntaxToken = tokenizer.MoveNext();
			StyleSyntaxTokenType type = styleSyntaxToken.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			Expression expression;
			if (styleSyntaxTokenType != StyleSyntaxTokenType.String)
			{
				if (styleSyntaxTokenType != StyleSyntaxTokenType.SingleQuote)
				{
					throw new Exception(string.Format("Unexpected token '{0}' in data type expression", styleSyntaxToken.type));
				}
				expression = this.ParseProperty(tokenizer);
			}
			else
			{
				string text;
				bool flag2 = StylePropertyCache.TryGetNonTerminalValue(styleSyntaxToken.text, out text);
				if (flag2)
				{
					expression = this.ParseNonTerminalValue(text);
				}
				else
				{
					DataType dataType = DataType.None;
					try
					{
						object obj = Enum.Parse(typeof(DataType), styleSyntaxToken.text.Replace("-", ""), true);
						bool flag3 = obj != null;
						if (flag3)
						{
							dataType = (DataType)obj;
						}
					}
					catch (Exception)
					{
						throw new Exception("Unknown data type '" + styleSyntaxToken.text + "'");
					}
					expression = new Expression(ExpressionType.Data);
					expression.dataType = dataType;
				}
				tokenizer.MoveNext();
			}
			styleSyntaxToken = tokenizer.current;
			bool flag4 = styleSyntaxToken.type != StyleSyntaxTokenType.GreaterThan;
			if (flag4)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in data type expression. Expected '>' token", styleSyntaxToken.type));
			}
			tokenizer.MoveNext();
			return expression;
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000C9310 File Offset: 0x000C7510
		private Expression ParseNonTerminalValue(string syntax)
		{
			Expression expression = null;
			bool flag = !this.m_ParsedExpressionCache.TryGetValue(syntax, out expression);
			if (flag)
			{
				this.m_CombinatorStack.Push(ExpressionCombinator.Group);
				expression = this.Parse(syntax);
			}
			return new Expression(ExpressionType.Combinator)
			{
				combinator = ExpressionCombinator.Group,
				subExpressions = new Expression[] { expression }
			};
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000C9370 File Offset: 0x000C7570
		private Expression ParseProperty(StyleSyntaxTokenizer tokenizer)
		{
			Expression expression = null;
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = styleSyntaxToken.type != StyleSyntaxTokenType.SingleQuote;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected ''' token", styleSyntaxToken.type));
			}
			styleSyntaxToken = tokenizer.MoveNext();
			bool flag2 = styleSyntaxToken.type != StyleSyntaxTokenType.String;
			if (flag2)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected 'string' token", styleSyntaxToken.type));
			}
			string text = styleSyntaxToken.text;
			string text2;
			bool flag3 = !StylePropertyCache.TryGetSyntax(text, out text2);
			if (flag3)
			{
				throw new Exception("Unknown property '" + text + "' <''> expression.");
			}
			bool flag4 = !this.m_ParsedExpressionCache.TryGetValue(text2, out expression);
			if (flag4)
			{
				this.m_CombinatorStack.Push(ExpressionCombinator.Group);
				expression = this.Parse(text2);
			}
			styleSyntaxToken = tokenizer.MoveNext();
			bool flag5 = styleSyntaxToken.type != StyleSyntaxTokenType.SingleQuote;
			if (flag5)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected ''' token", styleSyntaxToken.type));
			}
			styleSyntaxToken = tokenizer.MoveNext();
			bool flag6 = styleSyntaxToken.type != StyleSyntaxTokenType.GreaterThan;
			if (flag6)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected '>' token", styleSyntaxToken.type));
			}
			return new Expression(ExpressionType.Combinator)
			{
				combinator = ExpressionCombinator.Group,
				subExpressions = new Expression[] { expression }
			};
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000C94D8 File Offset: 0x000C76D8
		private void ParseMultiplier(StyleSyntaxTokenizer tokenizer, ref ExpressionMultiplier multiplier)
		{
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = StyleSyntaxParser.IsMultiplier(styleSyntaxToken);
			if (flag)
			{
				switch (styleSyntaxToken.type)
				{
				case StyleSyntaxTokenType.Asterisk:
					multiplier.type = ExpressionMultiplierType.ZeroOrMore;
					goto IL_00A1;
				case StyleSyntaxTokenType.Plus:
					multiplier.type = ExpressionMultiplierType.OneOrMore;
					goto IL_00A1;
				case StyleSyntaxTokenType.QuestionMark:
					multiplier.type = ExpressionMultiplierType.ZeroOrOne;
					goto IL_00A1;
				case StyleSyntaxTokenType.HashMark:
					multiplier.type = ExpressionMultiplierType.OneOrMoreComma;
					goto IL_00A1;
				case StyleSyntaxTokenType.ExclamationPoint:
					multiplier.type = ExpressionMultiplierType.GroupAtLeastOne;
					goto IL_00A1;
				case StyleSyntaxTokenType.OpenBrace:
					multiplier.type = ExpressionMultiplierType.Ranges;
					goto IL_00A1;
				}
				throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected multiplier token", styleSyntaxToken.type));
				IL_00A1:
				styleSyntaxToken = tokenizer.MoveNext();
			}
			bool flag2 = multiplier.type == ExpressionMultiplierType.Ranges;
			if (flag2)
			{
				this.ParseRanges(tokenizer, out multiplier.min, out multiplier.max);
			}
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000C95B4 File Offset: 0x000C77B4
		private void ParseRanges(StyleSyntaxTokenizer tokenizer, out int min, out int max)
		{
			min = -1;
			max = -1;
			StyleSyntaxToken styleSyntaxToken = tokenizer.current;
			bool flag = false;
			while (styleSyntaxToken.type != StyleSyntaxTokenType.CloseBrace)
			{
				StyleSyntaxTokenType type = styleSyntaxToken.type;
				StyleSyntaxTokenType styleSyntaxTokenType = type;
				if (styleSyntaxTokenType != StyleSyntaxTokenType.Number)
				{
					if (styleSyntaxTokenType != StyleSyntaxTokenType.Comma)
					{
						throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected ranges token", styleSyntaxToken.type));
					}
					flag = true;
				}
				else
				{
					bool flag2 = !flag;
					if (flag2)
					{
						min = styleSyntaxToken.number;
					}
					else
					{
						max = styleSyntaxToken.number;
					}
				}
				styleSyntaxToken = tokenizer.MoveNext();
			}
			bool flag3 = !flag;
			if (flag3)
			{
				max = min;
			}
			tokenizer.MoveNext();
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000C965C File Offset: 0x000C785C
		private static void EatSpace(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken current = tokenizer.current;
			bool flag = current.type == StyleSyntaxTokenType.Space;
			if (flag)
			{
				tokenizer.MoveNext();
			}
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000C9688 File Offset: 0x000C7888
		private static bool IsExpressionEnd(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType == StyleSyntaxTokenType.CloseBracket || styleSyntaxTokenType == StyleSyntaxTokenType.End;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000C96B8 File Offset: 0x000C78B8
		private static bool IsCombinator(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType - StyleSyntaxTokenType.Space <= 3;
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000C96E0 File Offset: 0x000C78E0
		private static bool IsMultiplier(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType - StyleSyntaxTokenType.Asterisk <= 4 || styleSyntaxTokenType == StyleSyntaxTokenType.OpenBrace;
		}

		// Token: 0x04001A6B RID: 6763
		private List<Expression> m_ProcessExpressionList = new List<Expression>();

		// Token: 0x04001A6C RID: 6764
		private Stack<Expression> m_ExpressionStack = new Stack<Expression>();

		// Token: 0x04001A6D RID: 6765
		private Stack<ExpressionCombinator> m_CombinatorStack = new Stack<ExpressionCombinator>();

		// Token: 0x04001A6E RID: 6766
		private Dictionary<string, Expression> m_ParsedExpressionCache = new Dictionary<string, Expression>();
	}
}
