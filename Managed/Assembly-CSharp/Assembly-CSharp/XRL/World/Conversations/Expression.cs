using System;
using System.Collections.Generic;
using XRL.Collections;

namespace XRL.World.Conversations
{
	// Token: 0x02000BED RID: 3053
	public static class Expression
	{
		// Token: 0x06007AB4 RID: 31412 RVA: 0x002E1618 File Offset: 0x002DF818
		public static Expression.Node CreateTree(List<Expression.Token> Tokens, IConversationElement Element, PredicateReceiver Receiver)
		{
			int num = 0;
			return Expression.CreateNode(Tokens, Element, Receiver, ref num);
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x002E1634 File Offset: 0x002DF834
		private static Expression.Node CreateNode(List<Expression.Token> Tokens, IConversationElement Element, PredicateReceiver Receiver, ref int Index)
		{
			if (Index >= Tokens.Count)
			{
				return null;
			}
			int num = Index;
			Index = num + 1;
			Expression.Token token = Tokens[num];
			if (token.Type == TokenType.Not)
			{
				return Expression.NotNode.Get(Expression.CreateNode(Tokens, Element, Receiver, ref Index));
			}
			if (token.Type == TokenType.And)
			{
				Expression.Node node = Expression.CreateNode(Tokens, Element, Receiver, ref Index);
				Expression.Node node2 = Expression.CreateNode(Tokens, Element, Receiver, ref Index);
				return Expression.AndNode.Get(node, node2);
			}
			if (token.Type == TokenType.Or)
			{
				Expression.Node node3 = Expression.CreateNode(Tokens, Element, Receiver, ref Index);
				Expression.Node node4 = Expression.CreateNode(Tokens, Element, Receiver, ref Index);
				return Expression.OrNode.Get(node3, node4);
			}
			return Expression.PredicateNode.Get(Element, Receiver, token.ToString());
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x002E16CE File Offset: 0x002DF8CE
		public static List<Expression.Token> Tokenize(string Value)
		{
			return Expression.Token.Tokenize(Value);
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x002E16D6 File Offset: 0x002DF8D6
		public static void Shunt(List<Expression.Token> Value)
		{
			Expression.Token.Shunt(Value);
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x002E16E0 File Offset: 0x002DF8E0
		public static bool? Evaluate(string Value, IConversationElement Element, PredicateReceiver Receiver)
		{
			List<Expression.Token> list = Expression.Tokenize(Value);
			bool? flag;
			if (list.IsNullOrEmpty<Expression.Token>())
			{
				flag = null;
				return flag;
			}
			Expression.Shunt(list);
			using (Expression.Node node = Expression.CreateTree(list, Element, Receiver))
			{
				flag = new bool?(node.Eval());
			}
			return flag;
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x002E1740 File Offset: 0x002DF940
		public static bool? Evaluate(string Value, IConversationElement Element, ActionReceiver Receiver)
		{
			List<Expression.Token> list = Expression.Tokenize(Value);
			if (list.IsNullOrEmpty<Expression.Token>())
			{
				return null;
			}
			foreach (Expression.Token token in list)
			{
				if (token.Type == TokenType.Literal)
				{
					Expression.LiteralToken literalToken = token as Expression.LiteralToken;
					if (literalToken != null)
					{
						Receiver(Element, literalToken.Value);
					}
				}
			}
			return new bool?(true);
		}

		// Token: 0x040037F1 RID: 14321
		public const byte PREC_SHIFT = 4;

		// Token: 0x040037F2 RID: 14322
		public const byte PREC_MASK = 7;

		// Token: 0x040037F3 RID: 14323
		public const byte RIGHT_ASSOC = 128;

		// Token: 0x02001C14 RID: 7188
		public class Token
		{
			// Token: 0x17000FC4 RID: 4036
			// (get) Token: 0x0600F006 RID: 61446 RVA: 0x005493C2 File Offset: 0x005475C2
			public byte Precedence
			{
				get
				{
					return (byte)((this.Type >> 4) & (TokenType)7);
				}
			}

			// Token: 0x17000FC5 RID: 4037
			// (get) Token: 0x0600F007 RID: 61447 RVA: 0x005493CF File Offset: 0x005475CF
			public bool LeftAssociative
			{
				get
				{
					return (this.Type & (TokenType)128) == (TokenType)0;
				}
			}

			// Token: 0x17000FC6 RID: 4038
			// (get) Token: 0x0600F008 RID: 61448 RVA: 0x005493E0 File Offset: 0x005475E0
			public bool RightAssociative
			{
				get
				{
					return (this.Type & (TokenType)128) > (TokenType)0;
				}
			}

			// Token: 0x0600F009 RID: 61449 RVA: 0x005493F4 File Offset: 0x005475F4
			public override string ToString()
			{
				TokenType type = this.Type;
				if (type <= TokenType.CloseGroup)
				{
					if (type == TokenType.OpenGroup)
					{
						return "(";
					}
					if (type == TokenType.CloseGroup)
					{
						return ")";
					}
				}
				else
				{
					if (type == TokenType.And)
					{
						return "AND";
					}
					if (type == TokenType.Or)
					{
						return "OR";
					}
					if (type == TokenType.Not)
					{
						return "NOT";
					}
				}
				return "";
			}

			// Token: 0x0600F00A RID: 61450 RVA: 0x00549450 File Offset: 0x00547650
			public Token(TokenType Type)
			{
				this.Type = Type;
			}

			// Token: 0x0600F00B RID: 61451 RVA: 0x00549460 File Offset: 0x00547660
			public static bool TryGetLiteral(string Value, int Start, int Length, out Expression.Token Token)
			{
				while (Length > 0)
				{
					if (Value[Start] != ' ')
					{
						break;
					}
					Start++;
					Length--;
				}
				while (Length > 0 && Value[Start + Length - 1] == ' ')
				{
					Length--;
				}
				if (Length <= 0)
				{
					Token = null;
					return false;
				}
				Token = Expression.Token.GetLiteral(Value.AsSpan(Start, Length));
				return true;
			}

			// Token: 0x0600F00C RID: 61452 RVA: 0x005494BC File Offset: 0x005476BC
			public static Expression.Token GetLiteral(ReadOnlySpan<char> Key)
			{
				Expression.Token token;
				if (!Expression.Token.Literals.TryGetValue(Key, out token))
				{
					string text = new string(Key);
					token = (Expression.Token.Literals[text] = new Expression.LiteralToken(text));
				}
				return token;
			}

			// Token: 0x0600F00D RID: 61453 RVA: 0x005494F4 File Offset: 0x005476F4
			public static List<Expression.Token> Tokenize(string Value)
			{
				RingDeque<Expression.Token> output = Expression.Token.Output;
				int num = 0;
				int num2 = Value.Length - 1;
				Expression.Token token = Expression.Token.NOT;
				int i = 0;
				int num3 = 0;
				while (i <= num2)
				{
					char c = Value[i];
					if (c == 'N' && i + 3 <= num2 && Value[i + 1] == 'O' && Value[i + 2] == 'T' && Value[i + 3] == ' ' && (i == 0 || Value[i - 1] == ' ' || Value[i - 1] == '('))
					{
						token = Expression.Token.NOT;
						num3 = 3;
						goto IL_013C;
					}
					if (c == '(')
					{
						token = Expression.Token.OPEN;
						goto IL_013C;
					}
					if (c == ')')
					{
						token = Expression.Token.CLOSE;
						goto IL_013C;
					}
					if (c == ' ' && i + 4 <= num2)
					{
						if (Value[i + 1] == 'A' && Value[i + 2] == 'N' && Value[i + 3] == 'D' && Value[i + 4] == ' ')
						{
							token = Expression.Token.AND;
							num3 = 4;
							goto IL_013C;
						}
						if (Value[i + 1] == 'O' && Value[i + 2] == 'R' && Value[i + 3] == ' ')
						{
							token = Expression.Token.OR;
							num3 = 3;
							goto IL_013C;
						}
					}
					IL_0169:
					i++;
					continue;
					IL_013C:
					Expression.Token token2;
					if (Expression.Token.TryGetLiteral(Value, num, i - num, out token2))
					{
						output.Enqueue(token2);
					}
					output.Enqueue(token);
					i += num3;
					num = i + 1;
					num3 = 0;
					goto IL_0169;
				}
				if (num == 0)
				{
					return null;
				}
				Expression.Token token3;
				if (Expression.Token.TryGetLiteral(Value, num, num2 - num + 1, out token3))
				{
					output.Enqueue(token3);
				}
				int count = output.Count;
				List<Expression.Token> list = new List<Expression.Token>(count);
				for (int j = 0; j < count; j++)
				{
					list.Add(output.Dequeue());
				}
				return list;
			}

			// Token: 0x0600F00E RID: 61454 RVA: 0x005496C8 File Offset: 0x005478C8
			public static void Shunt(List<Expression.Token> Value)
			{
				RingDeque<Expression.Token> output = Expression.Token.Output;
				RingDeque<Expression.Token> operators = Expression.Token.Operators;
				output.Clear();
				operators.Clear();
				for (int i = Value.Count - 1; i >= 0; i--)
				{
					Expression.Token token = Value[i];
					TokenType type = token.Type;
					if (type <= TokenType.CloseGroup)
					{
						if (type != TokenType.Literal)
						{
							if (type != TokenType.OpenGroup)
							{
								if (type == TokenType.CloseGroup)
								{
									operators.Enqueue(token);
								}
							}
							else
							{
								bool flag = false;
								Expression.Token token2;
								while (operators.TryEject(out token2))
								{
									if (token2.Type == TokenType.CloseGroup)
									{
										flag = true;
										break;
									}
									output.Enqueue(token2);
								}
								if (!flag)
								{
									throw new ArgumentException("Mismatched parentheses");
								}
							}
						}
						else
						{
							output.Enqueue(token);
						}
					}
					else if (type == TokenType.And || type == TokenType.Or || type == TokenType.Not)
					{
						while (operators.Count > 0 && operators.Last.Type != TokenType.CloseGroup && (operators.Last.Precedence < token.Precedence || (operators.Last.Precedence == token.Precedence && token.RightAssociative)))
						{
							output.Enqueue(operators.Eject());
						}
						operators.Enqueue(token);
					}
				}
				Expression.Token token3;
				while (operators.TryEject(out token3))
				{
					output.Enqueue(token3);
				}
				Value.Clear();
				Expression.Token token4;
				while (output.TryEject(out token4))
				{
					Value.Add(token4);
				}
			}

			// Token: 0x04006F25 RID: 28453
			public static readonly Expression.Token OPEN = new Expression.Token(TokenType.OpenGroup);

			// Token: 0x04006F26 RID: 28454
			public static readonly Expression.Token CLOSE = new Expression.Token(TokenType.CloseGroup);

			// Token: 0x04006F27 RID: 28455
			public static readonly Expression.Token AND = new Expression.Token(TokenType.And);

			// Token: 0x04006F28 RID: 28456
			public static readonly Expression.Token OR = new Expression.Token(TokenType.Or);

			// Token: 0x04006F29 RID: 28457
			public static readonly Expression.Token NOT = new Expression.Token(TokenType.Not);

			// Token: 0x04006F2A RID: 28458
			private static RingDeque<Expression.Token> Output = new RingDeque<Expression.Token>(16);

			// Token: 0x04006F2B RID: 28459
			private static RingDeque<Expression.Token> Operators = new RingDeque<Expression.Token>(16);

			// Token: 0x04006F2C RID: 28460
			private static StringMap<Expression.Token> Literals = new StringMap<Expression.Token>();

			// Token: 0x04006F2D RID: 28461
			public TokenType Type;
		}

		// Token: 0x02001C15 RID: 7189
		public class LiteralToken : Expression.Token
		{
			// Token: 0x0600F010 RID: 61456 RVA: 0x00549896 File Offset: 0x00547A96
			public LiteralToken(string Value)
				: base(TokenType.Literal)
			{
				this.Value = Value;
			}

			// Token: 0x0600F011 RID: 61457 RVA: 0x005498A6 File Offset: 0x00547AA6
			public override string ToString()
			{
				return this.Value;
			}

			// Token: 0x04006F2E RID: 28462
			public string Value;
		}

		// Token: 0x02001C16 RID: 7190
		public abstract class Node : IDisposable
		{
			// Token: 0x0600F012 RID: 61458
			public abstract bool Eval();

			// Token: 0x0600F013 RID: 61459
			public abstract void Dispose();
		}

		// Token: 0x02001C17 RID: 7191
		public class PredicateNode : Expression.Node
		{
			// Token: 0x0600F015 RID: 61461 RVA: 0x005498B8 File Offset: 0x00547AB8
			public static Expression.PredicateNode Get(IConversationElement Element, PredicateReceiver Receiver, string Value)
			{
				Expression.PredicateNode predicateNode;
				if (!Expression.PredicateNode.Pool.TryDequeue(out predicateNode))
				{
					predicateNode = new Expression.PredicateNode();
				}
				predicateNode.Element = Element;
				predicateNode.Receiver = Receiver;
				predicateNode.Value = Value;
				return predicateNode;
			}

			// Token: 0x0600F016 RID: 61462 RVA: 0x005498EF File Offset: 0x00547AEF
			public override bool Eval()
			{
				return this.Receiver(this.Element, this.Value);
			}

			// Token: 0x0600F017 RID: 61463 RVA: 0x00549908 File Offset: 0x00547B08
			public override void Dispose()
			{
				this.Value = null;
				Expression.PredicateNode.Pool.Enqueue(this);
			}

			// Token: 0x04006F2F RID: 28463
			private static RingDeque<Expression.PredicateNode> Pool = new RingDeque<Expression.PredicateNode>();

			// Token: 0x04006F30 RID: 28464
			public IConversationElement Element;

			// Token: 0x04006F31 RID: 28465
			public PredicateReceiver Receiver;

			// Token: 0x04006F32 RID: 28466
			public string Value;
		}

		// Token: 0x02001C18 RID: 7192
		public class NotNode : Expression.Node
		{
			// Token: 0x0600F01A RID: 61466 RVA: 0x00549930 File Offset: 0x00547B30
			public static Expression.NotNode Get(Expression.Node Operand)
			{
				Expression.NotNode notNode;
				if (!Expression.NotNode.Pool.TryDequeue(out notNode))
				{
					notNode = new Expression.NotNode();
				}
				notNode.Operand = Operand;
				return notNode;
			}

			// Token: 0x0600F01B RID: 61467 RVA: 0x00549959 File Offset: 0x00547B59
			public override bool Eval()
			{
				return !this.Operand.Eval();
			}

			// Token: 0x0600F01C RID: 61468 RVA: 0x00549969 File Offset: 0x00547B69
			public override void Dispose()
			{
				this.Operand.Dispose();
				this.Operand = null;
				Expression.NotNode.Pool.Enqueue(this);
			}

			// Token: 0x04006F33 RID: 28467
			private static RingDeque<Expression.NotNode> Pool = new RingDeque<Expression.NotNode>();

			// Token: 0x04006F34 RID: 28468
			public Expression.Node Operand;
		}

		// Token: 0x02001C19 RID: 7193
		public class AndNode : Expression.Node
		{
			// Token: 0x0600F01F RID: 61471 RVA: 0x0054999C File Offset: 0x00547B9C
			public static Expression.AndNode Get(Expression.Node Left, Expression.Node Right)
			{
				Expression.AndNode andNode;
				if (!Expression.AndNode.Pool.TryDequeue(out andNode))
				{
					andNode = new Expression.AndNode();
				}
				andNode.Left = Left;
				andNode.Right = Right;
				return andNode;
			}

			// Token: 0x0600F020 RID: 61472 RVA: 0x005499CC File Offset: 0x00547BCC
			public override bool Eval()
			{
				return this.Left.Eval() && this.Right.Eval();
			}

			// Token: 0x0600F021 RID: 61473 RVA: 0x005499E8 File Offset: 0x00547BE8
			public override void Dispose()
			{
				this.Left.Dispose();
				this.Left = null;
				this.Right.Dispose();
				this.Right = null;
				Expression.AndNode.Pool.Enqueue(this);
			}

			// Token: 0x04006F35 RID: 28469
			private static RingDeque<Expression.AndNode> Pool = new RingDeque<Expression.AndNode>();

			// Token: 0x04006F36 RID: 28470
			public Expression.Node Left;

			// Token: 0x04006F37 RID: 28471
			public Expression.Node Right;
		}

		// Token: 0x02001C1A RID: 7194
		public class OrNode : Expression.Node
		{
			// Token: 0x0600F024 RID: 61476 RVA: 0x00549A30 File Offset: 0x00547C30
			public static Expression.OrNode Get(Expression.Node Left, Expression.Node Right)
			{
				Expression.OrNode orNode;
				if (!Expression.OrNode.Pool.TryDequeue(out orNode))
				{
					orNode = new Expression.OrNode();
				}
				orNode.Left = Left;
				orNode.Right = Right;
				return orNode;
			}

			// Token: 0x0600F025 RID: 61477 RVA: 0x00549A60 File Offset: 0x00547C60
			public override bool Eval()
			{
				return this.Left.Eval() || this.Right.Eval();
			}

			// Token: 0x0600F026 RID: 61478 RVA: 0x00549A7C File Offset: 0x00547C7C
			public override void Dispose()
			{
				this.Left.Dispose();
				this.Left = null;
				this.Right.Dispose();
				this.Right = null;
				Expression.OrNode.Pool.Enqueue(this);
			}

			// Token: 0x04006F38 RID: 28472
			private static RingDeque<Expression.OrNode> Pool = new RingDeque<Expression.OrNode>();

			// Token: 0x04006F39 RID: 28473
			public Expression.Node Left;

			// Token: 0x04006F3A RID: 28474
			public Expression.Node Right;
		}
	}
}
