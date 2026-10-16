using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200002F RID: 47
	public interface IStat
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000092 RID: 146
		string StatID { get; }

		// Token: 0x06000093 RID: 147
		float GetProgressFloat(string name, OnStatFloatValueGet callback);

		// Token: 0x06000094 RID: 148
		int GetProgressInt(string name, OnStatIntValueGet callback);

		// Token: 0x06000095 RID: 149
		void SetStat(int value, OnStatSet callback);

		// Token: 0x06000096 RID: 150
		void SetStat(float value, OnStatSet callback);

		// Token: 0x06000097 RID: 151
		void AddStat(int increment, OnStatSet callback);

		// Token: 0x06000098 RID: 152
		void AddStat(float increment, OnStatSet callback);
	}
}
