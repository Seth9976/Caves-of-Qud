using System;

namespace Unity.Properties
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class InvalidContainerTypeException : Exception
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002EF3 File Offset: 0x000010F3
		public Type Type { get; }

		// Token: 0x0600005C RID: 92 RVA: 0x00002EFB File Offset: 0x000010FB
		public InvalidContainerTypeException(Type type)
			: base(InvalidContainerTypeException.GetMessageForType(type))
		{
			this.Type = type;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F12 File Offset: 0x00001112
		public InvalidContainerTypeException(Type type, Exception inner)
			: base(InvalidContainerTypeException.GetMessageForType(type), inner)
		{
			this.Type = type;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F2C File Offset: 0x0000112C
		private static string GetMessageForType(Type type)
		{
			return string.Concat(new string[] { "Invalid container Type=[", type.Name, ".", type.Name, "]" });
		}
	}
}
