using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000140 RID: 320
	[MovedFrom(true, "UnityEditor", "UnityEditor", null)]
	public class ExpressionEvaluator
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x0000FE70 File Offset: 0x0000E070
		public static bool Evaluate<T>(string expression, out T value)
		{
			ExpressionEvaluator.Expression expression2;
			return ExpressionEvaluator.Evaluate<T>(expression, out value, out expression2);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0000FE8C File Offset: 0x0000E08C
		internal static bool Evaluate<T>(string expression, out T value, out ExpressionEvaluator.Expression delayed)
		{
			value = default(T);
			delayed = null;
			bool flag = ExpressionEvaluator.TryParse<T>(expression, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				ExpressionEvaluator.Expression expression2 = new ExpressionEvaluator.Expression(expression);
				bool hasVariables = expression2.hasVariables;
				if (hasVariables)
				{
					value = default(T);
					delayed = expression2;
					flag2 = false;
				}
				else
				{
					flag2 = ExpressionEvaluator.EvaluateTokens<T>(expression2.rpnTokens, ref value, 0, 1);
				}
			}
			return flag2;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000FEE5 File Offset: 0x0000E0E5
		internal static void SetRandomState(uint state)
		{
			ExpressionEvaluator.s_Random = new ExpressionEvaluator.PcgRandom((ulong)state, 0UL);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		private unsafe static bool EvaluateTokens<T>(string[] tokens, ref T value, int index, int count)
		{
			bool flag = false;
			bool flag2 = typeof(T) == typeof(float);
			if (flag2)
			{
				double num = (double)(*UnsafeUtility.As<T, float>(ref value));
				flag = ExpressionEvaluator.EvaluateDouble(tokens, ref num, index, count);
				float num2 = (float)num;
				value = *UnsafeUtility.As<float, T>(ref num2);
			}
			else
			{
				bool flag3 = typeof(T) == typeof(int);
				if (flag3)
				{
					double num3 = (double)(*UnsafeUtility.As<T, int>(ref value));
					flag = ExpressionEvaluator.EvaluateDouble(tokens, ref num3, index, count);
					int num4 = (int)num3;
					value = *UnsafeUtility.As<int, T>(ref num4);
				}
				else
				{
					bool flag4 = typeof(T) == typeof(long);
					if (flag4)
					{
						double num5 = (double)(*UnsafeUtility.As<T, long>(ref value));
						flag = ExpressionEvaluator.EvaluateDouble(tokens, ref num5, index, count);
						long num6 = (long)num5;
						value = *UnsafeUtility.As<long, T>(ref num6);
					}
					else
					{
						bool flag5 = typeof(T) == typeof(ulong);
						if (flag5)
						{
							double num7 = *UnsafeUtility.As<T, ulong>(ref value);
							flag = ExpressionEvaluator.EvaluateDouble(tokens, ref num7, index, count);
							bool flag6 = num7 < 0.0;
							if (flag6)
							{
								num7 = 0.0;
							}
							ulong num8 = (ulong)num7;
							value = *UnsafeUtility.As<ulong, T>(ref num8);
						}
						else
						{
							bool flag7 = typeof(T) == typeof(double);
							if (flag7)
							{
								double num9 = *UnsafeUtility.As<T, double>(ref value);
								flag = ExpressionEvaluator.EvaluateDouble(tokens, ref num9, index, count);
								value = *UnsafeUtility.As<double, T>(ref num9);
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x000100B8 File Offset: 0x0000E2B8
		private static bool EvaluateDouble(string[] tokens, ref double value, int index, int count)
		{
			Stack<string> stack = new Stack<string>();
			foreach (string text in tokens)
			{
				bool flag = ExpressionEvaluator.IsOperator(text);
				if (flag)
				{
					ExpressionEvaluator.Operator @operator = ExpressionEvaluator.TokenToOperator(text);
					List<double> list = new List<double>();
					bool flag2 = true;
					while (stack.Count > 0 && !ExpressionEvaluator.IsCommand(stack.Peek()) && list.Count < @operator.inputs)
					{
						double num;
						flag2 &= ExpressionEvaluator.TryParse<double>(stack.Pop(), out num);
						list.Add(num);
					}
					list.Reverse();
					bool flag3 = flag2 && list.Count == @operator.inputs;
					if (!flag3)
					{
						return false;
					}
					stack.Push(ExpressionEvaluator.EvaluateOp(list.ToArray(), @operator.op, index, count).ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					bool flag4 = ExpressionEvaluator.IsVariable(text);
					if (flag4)
					{
						stack.Push((text == "#") ? index.ToString() : value.ToString(CultureInfo.InvariantCulture));
					}
					else
					{
						stack.Push(text);
					}
				}
			}
			bool flag5 = stack.Count == 1;
			if (flag5)
			{
				bool flag6 = ExpressionEvaluator.TryParse<double>(stack.Pop(), out value);
				if (flag6)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00010228 File Offset: 0x0000E428
		private static string[] InfixToRPN(string[] tokens)
		{
			Stack<string> stack = new Stack<string>();
			Queue<string> queue = new Queue<string>();
			foreach (string text in tokens)
			{
				bool flag = ExpressionEvaluator.IsCommand(text);
				if (flag)
				{
					char c = text[0];
					bool flag2 = c == '(';
					if (flag2)
					{
						stack.Push(text);
					}
					else
					{
						bool flag3 = c == ')';
						if (flag3)
						{
							while (stack.Count > 0 && stack.Peek() != "(")
							{
								queue.Enqueue(stack.Pop());
							}
							bool flag4 = stack.Count > 0;
							if (flag4)
							{
								stack.Pop();
							}
							bool flag5 = stack.Count > 0 && ExpressionEvaluator.IsDelayedFunction(stack.Peek());
							if (flag5)
							{
								queue.Enqueue(stack.Pop());
							}
						}
						else
						{
							bool flag6 = c == ',';
							if (flag6)
							{
								while (stack.Count > 0 && stack.Peek() != "(")
								{
									queue.Enqueue(stack.Pop());
								}
							}
							else
							{
								ExpressionEvaluator.Operator @operator = ExpressionEvaluator.TokenToOperator(text);
								while (ExpressionEvaluator.NeedToPop(stack, @operator))
								{
									queue.Enqueue(stack.Pop());
								}
								stack.Push(text);
							}
						}
					}
				}
				else
				{
					bool flag7 = ExpressionEvaluator.IsDelayedFunction(text);
					if (flag7)
					{
						stack.Push(text);
					}
					else
					{
						queue.Enqueue(text);
					}
				}
			}
			while (stack.Count > 0)
			{
				queue.Enqueue(stack.Pop());
			}
			return queue.ToArray();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000103DC File Offset: 0x0000E5DC
		private static bool NeedToPop(Stack<string> operatorStack, ExpressionEvaluator.Operator newOperator)
		{
			bool flag = operatorStack.Count > 0 && newOperator != null;
			if (flag)
			{
				ExpressionEvaluator.Operator @operator = ExpressionEvaluator.TokenToOperator(operatorStack.Peek());
				bool flag2 = @operator != null;
				if (flag2)
				{
					bool flag3 = (newOperator.associativity == ExpressionEvaluator.Associativity.Left && newOperator.precedence <= @operator.precedence) || (newOperator.associativity == ExpressionEvaluator.Associativity.Right && newOperator.precedence < @operator.precedence);
					if (flag3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001045C File Offset: 0x0000E65C
		private static string[] ExpressionToTokens(string expression, out bool hasVariables)
		{
			hasVariables = false;
			List<string> list = new List<string>();
			string text = "";
			foreach (char c in expression)
			{
				bool flag = ExpressionEvaluator.IsCommand(c.ToString());
				if (flag)
				{
					bool flag2 = text.Length > 0;
					if (flag2)
					{
						list.Add(text);
					}
					list.Add(c.ToString());
					text = "";
				}
				else
				{
					bool flag3 = c != ' ';
					if (flag3)
					{
						text += c.ToString();
					}
					else
					{
						bool flag4 = text.Length > 0;
						if (flag4)
						{
							list.Add(text);
						}
						text = "";
					}
				}
			}
			bool flag5 = text.Length > 0;
			if (flag5)
			{
				list.Add(text);
			}
			hasVariables = list.Any<string>((string f) => ExpressionEvaluator.IsVariable(f) || ExpressionEvaluator.IsDelayedFunction(f));
			return list.ToArray();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001056C File Offset: 0x0000E76C
		private static bool IsCommand(string token)
		{
			bool flag = token.Length == 1;
			if (flag)
			{
				char c = token[0];
				bool flag2 = c == '(' || c == ')' || c == ',';
				if (flag2)
				{
					return true;
				}
			}
			return ExpressionEvaluator.IsOperator(token);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000105B8 File Offset: 0x0000E7B8
		private static bool IsVariable(string token)
		{
			bool flag = token.Length == 1;
			bool flag2;
			if (flag)
			{
				char c = token[0];
				flag2 = c == 'x' || c == 'v' || c == 'f' || c == '#';
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000105FC File Offset: 0x0000E7FC
		private static bool IsDelayedFunction(string token)
		{
			ExpressionEvaluator.Operator @operator = ExpressionEvaluator.TokenToOperator(token);
			bool flag = @operator != null;
			if (flag)
			{
				bool flag2 = @operator.op == ExpressionEvaluator.Op.Rand || @operator.op == ExpressionEvaluator.Op.Linear;
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00010640 File Offset: 0x0000E840
		private static bool IsOperator(string token)
		{
			return ExpressionEvaluator.s_Operators.ContainsKey(token);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00010660 File Offset: 0x0000E860
		private static ExpressionEvaluator.Operator TokenToOperator(string token)
		{
			ExpressionEvaluator.Operator @operator;
			return ExpressionEvaluator.s_Operators.TryGetValue(token, out @operator) ? @operator : null;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00010688 File Offset: 0x0000E888
		private static string PreFormatExpression(string expression)
		{
			string text = expression.Trim();
			bool flag = text.Length == 0;
			string text2;
			if (flag)
			{
				text2 = text;
			}
			else
			{
				char c = text[text.Length - 1];
				bool flag2 = ExpressionEvaluator.IsOperator(c.ToString());
				if (flag2)
				{
					text = text.TrimEnd(c);
				}
				bool flag3 = text.Length >= 2 && text[1] == '=';
				if (flag3)
				{
					char c2 = text[0];
					string text3 = text.Substring(2);
					bool flag4 = c2 == '+';
					if (flag4)
					{
						text = "x+(" + text3 + ")";
					}
					bool flag5 = c2 == '-';
					if (flag5)
					{
						text = "x-(" + text3 + ")";
					}
					bool flag6 = c2 == '*';
					if (flag6)
					{
						text = "x*(" + text3 + ")";
					}
					bool flag7 = c2 == '/';
					if (flag7)
					{
						text = "x/(" + text3 + ")";
					}
				}
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00010790 File Offset: 0x0000E990
		private static string[] FixUnaryOperators(string[] tokens)
		{
			bool flag = tokens.Length == 0;
			string[] array;
			if (flag)
			{
				array = tokens;
			}
			else
			{
				bool flag2 = tokens[0] == "-";
				if (flag2)
				{
					tokens[0] = "_";
				}
				for (int i = 1; i < tokens.Length - 1; i++)
				{
					string text = tokens[i];
					string text2 = tokens[i - 1];
					bool flag3 = text == "-" && ExpressionEvaluator.IsCommand(text2) && text2 != ")";
					if (flag3)
					{
						tokens[i] = "_";
					}
				}
				array = tokens;
			}
			return array;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00010824 File Offset: 0x0000EA24
		private static double EvaluateOp(double[] values, ExpressionEvaluator.Op op, int index, int count)
		{
			double num = ((values.Length >= 1) ? values[0] : 0.0);
			double num2 = ((values.Length >= 2) ? values[1] : 0.0);
			double num3;
			switch (op)
			{
			case ExpressionEvaluator.Op.Add:
				num3 = num + num2;
				break;
			case ExpressionEvaluator.Op.Sub:
				num3 = num - num2;
				break;
			case ExpressionEvaluator.Op.Mul:
				num3 = num * num2;
				break;
			case ExpressionEvaluator.Op.Div:
				num3 = num / num2;
				break;
			case ExpressionEvaluator.Op.Mod:
				num3 = num % num2;
				break;
			case ExpressionEvaluator.Op.Neg:
				num3 = -num;
				break;
			case ExpressionEvaluator.Op.Pow:
				num3 = Math.Pow(num, num2);
				break;
			case ExpressionEvaluator.Op.Sqrt:
				num3 = ((num <= 0.0) ? 0.0 : Math.Sqrt(num));
				break;
			case ExpressionEvaluator.Op.Sin:
				num3 = Math.Sin(num);
				break;
			case ExpressionEvaluator.Op.Cos:
				num3 = Math.Cos(num);
				break;
			case ExpressionEvaluator.Op.Tan:
				num3 = Math.Tan(num);
				break;
			case ExpressionEvaluator.Op.Floor:
				num3 = Math.Floor(num);
				break;
			case ExpressionEvaluator.Op.Ceil:
				num3 = Math.Ceiling(num);
				break;
			case ExpressionEvaluator.Op.Round:
				num3 = Math.Round(num);
				break;
			case ExpressionEvaluator.Op.Rand:
			{
				uint num4 = ExpressionEvaluator.s_Random.GetUInt() & 16777215U;
				double num5 = num4 / 16777215.0;
				num3 = num + num5 * (num2 - num);
				break;
			}
			case ExpressionEvaluator.Op.Linear:
			{
				bool flag = count < 1;
				if (flag)
				{
					count = 1;
				}
				double num6 = ((count < 2) ? 0.5 : ((double)index / (double)(count - 1)));
				num3 = num + num6 * (num2 - num);
				break;
			}
			default:
				num3 = 0.0;
				break;
			}
			return num3;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000109D0 File Offset: 0x0000EBD0
		private static bool TryParse<T>(string expression, out T result)
		{
			expression = expression.Replace(',', '.');
			string text = expression.ToLowerInvariant();
			bool flag = text.Length > 1 && char.IsDigit(text[text.Length - 2]);
			if (flag)
			{
				char[] array = new char[] { 'f', 'd', 'l' };
				text = text.TrimEnd(array);
			}
			bool flag2 = false;
			result = default(T);
			bool flag3 = text.Length == 0;
			bool flag4;
			if (flag3)
			{
				flag4 = true;
			}
			else
			{
				bool flag5 = typeof(T) == typeof(float);
				if (flag5)
				{
					bool flag6 = text == "pi";
					if (flag6)
					{
						flag2 = true;
						result = (T)((object)3.1415927f);
					}
					else
					{
						float num;
						flag2 = float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out num);
						result = (T)((object)num);
					}
				}
				else
				{
					bool flag7 = typeof(T) == typeof(int);
					if (flag7)
					{
						int num2;
						flag2 = int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out num2);
						result = (T)((object)num2);
					}
					else
					{
						bool flag8 = typeof(T) == typeof(double);
						if (flag8)
						{
							bool flag9 = text == "pi";
							if (flag9)
							{
								flag2 = true;
								result = (T)((object)3.141592653589793);
							}
							else
							{
								double num3;
								flag2 = double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out num3);
								result = (T)((object)num3);
							}
						}
						else
						{
							bool flag10 = typeof(T) == typeof(long);
							if (flag10)
							{
								long num4;
								flag2 = long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out num4);
								result = (T)((object)num4);
							}
							else
							{
								bool flag11 = typeof(T) == typeof(ulong);
								if (flag11)
								{
									ulong num5;
									flag2 = ulong.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out num5);
									result = (T)((object)num5);
								}
							}
						}
					}
				}
				flag4 = flag2;
			}
			return flag4;
		}

		// Token: 0x0400042E RID: 1070
		private static ExpressionEvaluator.PcgRandom s_Random = new ExpressionEvaluator.PcgRandom(0UL, 0UL);

		// Token: 0x0400042F RID: 1071
		private static Dictionary<string, ExpressionEvaluator.Operator> s_Operators = new Dictionary<string, ExpressionEvaluator.Operator>
		{
			{
				"-",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Sub, 2, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"+",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Add, 2, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"/",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Div, 3, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"*",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Mul, 3, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"%",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Mod, 3, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"^",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Pow, 5, 2, ExpressionEvaluator.Associativity.Right)
			},
			{
				"_",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Neg, 5, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"sqrt",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Sqrt, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"cos",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Cos, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"sin",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Sin, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"tan",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Tan, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"floor",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Floor, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"ceil",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Ceil, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"round",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Round, 4, 1, ExpressionEvaluator.Associativity.Left)
			},
			{
				"R",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Rand, 4, 2, ExpressionEvaluator.Associativity.Left)
			},
			{
				"L",
				new ExpressionEvaluator.Operator(ExpressionEvaluator.Op.Linear, 4, 2, ExpressionEvaluator.Associativity.Left)
			}
		};

		// Token: 0x02000141 RID: 321
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal class Expression
		{
			// Token: 0x06000977 RID: 2423 RVA: 0x00010DB0 File Offset: 0x0000EFB0
			internal Expression(string expression)
			{
				expression = ExpressionEvaluator.PreFormatExpression(expression);
				string[] array = ExpressionEvaluator.ExpressionToTokens(expression, out this.hasVariables);
				array = ExpressionEvaluator.FixUnaryOperators(array);
				this.rpnTokens = ExpressionEvaluator.InfixToRPN(array);
			}

			// Token: 0x06000978 RID: 2424 RVA: 0x00010DF0 File Offset: 0x0000EFF0
			public bool Evaluate<T>(ref T value, int index = 0, int count = 1)
			{
				return ExpressionEvaluator.EvaluateTokens<T>(this.rpnTokens, ref value, index, count);
			}

			// Token: 0x06000979 RID: 2425 RVA: 0x00010E10 File Offset: 0x0000F010
			public override bool Equals(object obj)
			{
				ExpressionEvaluator.Expression expression = obj as ExpressionEvaluator.Expression;
				bool flag = expression != null;
				return flag && this.rpnTokens.SequenceEqual<string>(expression.rpnTokens);
			}

			// Token: 0x0600097A RID: 2426 RVA: 0x00010E48 File Offset: 0x0000F048
			public override int GetHashCode()
			{
				return this.rpnTokens.GetHashCode();
			}

			// Token: 0x0600097B RID: 2427 RVA: 0x00010E68 File Offset: 0x0000F068
			public override string ToString()
			{
				return string.Join(" ", this.rpnTokens);
			}

			// Token: 0x04000430 RID: 1072
			internal readonly string[] rpnTokens;

			// Token: 0x04000431 RID: 1073
			internal readonly bool hasVariables;
		}

		// Token: 0x02000142 RID: 322
		private struct PcgRandom
		{
			// Token: 0x0600097C RID: 2428 RVA: 0x00010E8A File Offset: 0x0000F08A
			public PcgRandom(ulong state = 0UL, ulong sequence = 0UL)
			{
				this.increment = (sequence << 1) | 1UL;
				this.state = 0UL;
				this.Step();
				this.state += state;
				this.Step();
			}

			// Token: 0x0600097D RID: 2429 RVA: 0x00010EC0 File Offset: 0x0000F0C0
			public uint GetUInt()
			{
				ulong num = this.state;
				this.Step();
				return ExpressionEvaluator.PcgRandom.XshRr(num);
			}

			// Token: 0x0600097E RID: 2430 RVA: 0x00010EE6 File Offset: 0x0000F0E6
			private static uint RotateRight(uint v, int rot)
			{
				return (v >> rot) | (v << -rot);
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x00010EF9 File Offset: 0x0000F0F9
			private static uint XshRr(ulong s)
			{
				return ExpressionEvaluator.PcgRandom.RotateRight((uint)(((s >> 18) ^ s) >> 27), (int)(s >> 59));
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x00010F0F File Offset: 0x0000F10F
			private void Step()
			{
				this.state = this.state * 6364136223846793005UL + this.increment;
			}

			// Token: 0x04000432 RID: 1074
			private readonly ulong increment;

			// Token: 0x04000433 RID: 1075
			private ulong state;

			// Token: 0x04000434 RID: 1076
			private const ulong Multiplier64 = 6364136223846793005UL;
		}

		// Token: 0x02000143 RID: 323
		private enum Op
		{
			// Token: 0x04000436 RID: 1078
			Add,
			// Token: 0x04000437 RID: 1079
			Sub,
			// Token: 0x04000438 RID: 1080
			Mul,
			// Token: 0x04000439 RID: 1081
			Div,
			// Token: 0x0400043A RID: 1082
			Mod,
			// Token: 0x0400043B RID: 1083
			Neg,
			// Token: 0x0400043C RID: 1084
			Pow,
			// Token: 0x0400043D RID: 1085
			Sqrt,
			// Token: 0x0400043E RID: 1086
			Sin,
			// Token: 0x0400043F RID: 1087
			Cos,
			// Token: 0x04000440 RID: 1088
			Tan,
			// Token: 0x04000441 RID: 1089
			Floor,
			// Token: 0x04000442 RID: 1090
			Ceil,
			// Token: 0x04000443 RID: 1091
			Round,
			// Token: 0x04000444 RID: 1092
			Rand,
			// Token: 0x04000445 RID: 1093
			Linear
		}

		// Token: 0x02000144 RID: 324
		private enum Associativity
		{
			// Token: 0x04000447 RID: 1095
			Left,
			// Token: 0x04000448 RID: 1096
			Right
		}

		// Token: 0x02000145 RID: 325
		private class Operator
		{
			// Token: 0x06000981 RID: 2433 RVA: 0x00010F2F File Offset: 0x0000F12F
			public Operator(ExpressionEvaluator.Op op, int precedence, int inputs, ExpressionEvaluator.Associativity associativity)
			{
				this.op = op;
				this.precedence = precedence;
				this.inputs = inputs;
				this.associativity = associativity;
			}

			// Token: 0x04000449 RID: 1097
			public readonly ExpressionEvaluator.Op op;

			// Token: 0x0400044A RID: 1098
			public readonly int precedence;

			// Token: 0x0400044B RID: 1099
			public readonly ExpressionEvaluator.Associativity associativity;

			// Token: 0x0400044C RID: 1100
			public readonly int inputs;
		}
	}
}
