using System;
using System.IO;
using System.Runtime.Serialization;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002ED RID: 749
	[Serializable]
	public sealed class SymbolsNotFoundException : FileNotFoundException
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x0003D338 File Offset: 0x0003B538
		public SymbolsNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00025B1E File Offset: 0x00023D1E
		private SymbolsNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
