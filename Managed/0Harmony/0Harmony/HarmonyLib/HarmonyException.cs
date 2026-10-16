using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace HarmonyLib
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	public class HarmonyException : Exception
	{
		// Token: 0x060001DA RID: 474 RVA: 0x0000BC3C File Offset: 0x00009E3C
		internal HarmonyException()
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000BC44 File Offset: 0x00009E44
		internal HarmonyException(string message)
			: base(message)
		{
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000BC4D File Offset: 0x00009E4D
		internal HarmonyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000BC57 File Offset: 0x00009E57
		protected HarmonyException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000BC64 File Offset: 0x00009E64
		internal HarmonyException(Exception innerException, Dictionary<int, CodeInstruction> instructions, int errorOffset)
			: base("IL Compile Error", innerException)
		{
			this.instructions = instructions;
			this.errorOffset = errorOffset;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000BC80 File Offset: 0x00009E80
		internal static Exception Create(Exception ex, Dictionary<int, CodeInstruction> finalInstructions)
		{
			Match match = Regex.Match(ex.Message.TrimEnd(Array.Empty<char>()), "Reason: Invalid IL code in.+: IL_(\\d{4}): (.+)$");
			if (!match.Success)
			{
				return ex;
			}
			int num = int.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
			Regex.Replace(match.Groups[2].Value, " {2,}", " ");
			HarmonyException ex2 = ex as HarmonyException;
			if (ex2 != null)
			{
				ex2.instructions = finalInstructions;
				ex2.errorOffset = num;
				return ex2;
			}
			return new HarmonyException(ex, finalInstructions, num);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000BD11 File Offset: 0x00009F11
		public List<KeyValuePair<int, CodeInstruction>> GetInstructionsWithOffsets()
		{
			return this.instructions.OrderBy<KeyValuePair<int, CodeInstruction>, int>((KeyValuePair<int, CodeInstruction> ins) => ins.Key).ToList<KeyValuePair<int, CodeInstruction>>();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000BD44 File Offset: 0x00009F44
		public List<CodeInstruction> GetInstructions()
		{
			return (from ins in this.instructions
				orderby ins.Key
				select ins.Value).ToList<CodeInstruction>();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public int GetErrorOffset()
		{
			return this.errorOffset;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public int GetErrorIndex()
		{
			CodeInstruction codeInstruction;
			if (this.instructions.TryGetValue(this.errorOffset, out codeInstruction))
			{
				return this.GetInstructions().IndexOf(codeInstruction);
			}
			return -1;
		}

		// Token: 0x0400012E RID: 302
		private Dictionary<int, CodeInstruction> instructions;

		// Token: 0x0400012F RID: 303
		private int errorOffset;
	}
}
