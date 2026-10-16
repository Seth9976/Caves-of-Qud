using System;

namespace NAudio.Wave
{
	// Token: 0x0200016A RID: 362
	internal enum AcmMetrics
	{
		// Token: 0x040007F3 RID: 2035
		CountDrivers = 1,
		// Token: 0x040007F4 RID: 2036
		CountCodecs,
		// Token: 0x040007F5 RID: 2037
		CountConverters,
		// Token: 0x040007F6 RID: 2038
		CountFilters,
		// Token: 0x040007F7 RID: 2039
		CountDisabled,
		// Token: 0x040007F8 RID: 2040
		CountHardware,
		// Token: 0x040007F9 RID: 2041
		CountLocalDrivers = 20,
		// Token: 0x040007FA RID: 2042
		CountLocalCodecs,
		// Token: 0x040007FB RID: 2043
		CountLocalConverters,
		// Token: 0x040007FC RID: 2044
		CountLocalFilters,
		// Token: 0x040007FD RID: 2045
		CountLocalDisabled,
		// Token: 0x040007FE RID: 2046
		HardwareWaveInput = 30,
		// Token: 0x040007FF RID: 2047
		HardwareWaveOutput,
		// Token: 0x04000800 RID: 2048
		MaxSizeFormat = 50,
		// Token: 0x04000801 RID: 2049
		MaxSizeFilter,
		// Token: 0x04000802 RID: 2050
		DriverSupport = 100,
		// Token: 0x04000803 RID: 2051
		DriverPriority
	}
}
