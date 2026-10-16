using System;
using System.Collections.Generic;
using System.Reflection;
using AiUnity.Common.Extensions;
using AiUnity.Common.Log;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Layouts;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002AD RID: 685
	public class ConditionParser
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x0007D796 File Offset: 0x0007B996
		private ConditionParser(SimpleStringReader stringReader, ConfigurationItemFactory configurationItemFactory)
		{
			this.configurationItemFactory = configurationItemFactory;
			this.tokenizer = new ConditionTokenizer(stringReader);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0007D7B1 File Offset: 0x0007B9B1
		public static ConditionExpression ParseExpression(string expressionText)
		{
			return ConditionParser.ParseExpression(expressionText, ConfigurationItemFactory.Default);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0007D7C0 File Offset: 0x0007B9C0
		public static ConditionExpression ParseExpression(string expressionText, ConfigurationItemFactory configurationItemFactories)
		{
			if (expressionText == null)
			{
				return null;
			}
			ConditionParser conditionParser = new ConditionParser(new SimpleStringReader(expressionText), configurationItemFactories);
			ConditionExpression conditionExpression = conditionParser.ParseExpression();
			if (!conditionParser.tokenizer.IsEOF())
			{
				throw new ConditionParseException("Unexpected token: " + conditionParser.tokenizer.TokenValue);
			}
			return conditionExpression;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0007D80D File Offset: 0x0007BA0D
		internal static ConditionExpression ParseExpression(SimpleStringReader stringReader, ConfigurationItemFactory configurationItemFactories)
		{
			return new ConditionParser(stringReader, configurationItemFactories).ParseExpression();
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0007D81C File Offset: 0x0007BA1C
		private ConditionMethodExpression ParsePredicate(string functionName)
		{
			List<ConditionExpression> list = new List<ConditionExpression>();
			while (!this.tokenizer.IsEOF() && this.tokenizer.TokenType != ConditionTokenType.RightParen)
			{
				list.Add(this.ParseExpression());
				if (this.tokenizer.TokenType != ConditionTokenType.Comma)
				{
					break;
				}
				this.tokenizer.GetNextToken();
			}
			this.tokenizer.Expect(ConditionTokenType.RightParen);
			ConditionMethodExpression conditionMethodExpression;
			try
			{
				MethodInfo methodInfo = this.configurationItemFactory.ConditionMethods.CreateInstance(functionName);
				conditionMethodExpression = new ConditionMethodExpression(functionName, methodInfo, list);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				throw new ConditionParseException("Cannot resolve function '" + functionName + "'", ex);
			}
			return conditionMethodExpression;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0007D8D0 File Offset: 0x0007BAD0
		private ConditionExpression ParseLiteralExpression()
		{
			if (this.tokenizer.IsToken(ConditionTokenType.LeftParen))
			{
				this.tokenizer.GetNextToken();
				ConditionExpression conditionExpression = this.ParseExpression();
				this.tokenizer.Expect(ConditionTokenType.RightParen);
				return conditionExpression;
			}
			if (this.tokenizer.IsToken(ConditionTokenType.Minus))
			{
				this.tokenizer.GetNextToken();
				if (!this.tokenizer.IsNumber())
				{
					throw new ConditionParseException("Number expected, got " + this.tokenizer.TokenType.ToString());
				}
				string tokenValue = this.tokenizer.TokenValue;
				this.tokenizer.GetNextToken();
				if (tokenValue.IndexOf('.') >= 0)
				{
					return new ConditionLiteralExpression(-double.Parse(tokenValue));
				}
				return new ConditionLiteralExpression(-int.Parse(tokenValue));
			}
			else if (this.tokenizer.IsNumber())
			{
				string tokenValue2 = this.tokenizer.TokenValue;
				this.tokenizer.GetNextToken();
				if (tokenValue2.IndexOf('.') >= 0)
				{
					return new ConditionLiteralExpression(double.Parse(tokenValue2));
				}
				return new ConditionLiteralExpression(int.Parse(tokenValue2));
			}
			else
			{
				if (this.tokenizer.TokenType == ConditionTokenType.String)
				{
					ConditionExpression conditionExpression2 = new ConditionLayoutExpression(Layout.FromString(this.tokenizer.StringTokenValue, this.configurationItemFactory));
					this.tokenizer.GetNextToken();
					return conditionExpression2;
				}
				if (this.tokenizer.TokenType == ConditionTokenType.Keyword)
				{
					string text = this.tokenizer.EatKeyword();
					if (string.Compare(text, "level", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLevelExpression();
					}
					if (string.Compare(text, "logger", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLoggerNameExpression();
					}
					if (string.Compare(text, "message", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionMessageExpression();
					}
					if (string.Compare(text, "loglevel", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.tokenizer.Expect(ConditionTokenType.Dot);
						return new ConditionLiteralExpression(this.tokenizer.EatKeyword().ToEnum((LogLevels)0));
					}
					if (string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLiteralExpression(true);
					}
					if (string.Compare(text, "false", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLiteralExpression(false);
					}
					if (string.Compare(text, "null", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLiteralExpression(null);
					}
					if (this.tokenizer.TokenType == ConditionTokenType.LeftParen)
					{
						this.tokenizer.GetNextToken();
						return this.ParsePredicate(text);
					}
				}
				throw new ConditionParseException("Unexpected token: " + this.tokenizer.TokenValue);
			}
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0007DB40 File Offset: 0x0007BD40
		private ConditionExpression ParseBooleanRelation()
		{
			ConditionExpression conditionExpression = this.ParseLiteralExpression();
			if (this.tokenizer.IsToken(ConditionTokenType.EqualTo))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Equal);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.NotEqual))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.NotEqual);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.LessThan))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Less);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.GreaterThan))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Greater);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.LessThanOrEqualTo))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.LessOrEqual);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.GreaterThanOrEqualTo))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.GreaterOrEqual);
			}
			return conditionExpression;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0007DC43 File Offset: 0x0007BE43
		private ConditionExpression ParseBooleanPredicate()
		{
			if (this.tokenizer.IsKeyword("not") || this.tokenizer.IsToken(ConditionTokenType.Not))
			{
				this.tokenizer.GetNextToken();
				return new ConditionNotExpression(this.ParseBooleanPredicate());
			}
			return this.ParseBooleanRelation();
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0007DC84 File Offset: 0x0007BE84
		private ConditionExpression ParseBooleanAnd()
		{
			ConditionExpression conditionExpression = this.ParseBooleanPredicate();
			while (this.tokenizer.IsKeyword("and") || this.tokenizer.IsToken(ConditionTokenType.And))
			{
				this.tokenizer.GetNextToken();
				conditionExpression = new ConditionAndExpression(conditionExpression, this.ParseBooleanPredicate());
			}
			return conditionExpression;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0007DCD4 File Offset: 0x0007BED4
		private ConditionExpression ParseBooleanOr()
		{
			ConditionExpression conditionExpression = this.ParseBooleanAnd();
			while (this.tokenizer.IsKeyword("or") || this.tokenizer.IsToken(ConditionTokenType.Or))
			{
				this.tokenizer.GetNextToken();
				conditionExpression = new ConditionOrExpression(conditionExpression, this.ParseBooleanAnd());
			}
			return conditionExpression;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0007DD24 File Offset: 0x0007BF24
		private ConditionExpression ParseBooleanExpression()
		{
			return this.ParseBooleanOr();
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0007DD2C File Offset: 0x0007BF2C
		private ConditionExpression ParseExpression()
		{
			return this.ParseBooleanExpression();
		}

		// Token: 0x04000E47 RID: 3655
		private readonly ConditionTokenizer tokenizer;

		// Token: 0x04000E48 RID: 3656
		private readonly ConfigurationItemFactory configurationItemFactory;
	}
}
