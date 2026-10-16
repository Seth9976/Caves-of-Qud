using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000738 RID: 1848
	internal class IcmpV6MessageTypes
	{
		// Token: 0x040022B2 RID: 8882
		public const int DestinationUnreachable = 1;

		// Token: 0x040022B3 RID: 8883
		public const int PacketTooBig = 2;

		// Token: 0x040022B4 RID: 8884
		public const int TimeExceeded = 3;

		// Token: 0x040022B5 RID: 8885
		public const int ParameterProblem = 4;

		// Token: 0x040022B6 RID: 8886
		public const int EchoRequest = 128;

		// Token: 0x040022B7 RID: 8887
		public const int EchoReply = 129;

		// Token: 0x040022B8 RID: 8888
		public const int GroupMembershipQuery = 130;

		// Token: 0x040022B9 RID: 8889
		public const int GroupMembershipReport = 131;

		// Token: 0x040022BA RID: 8890
		public const int GroupMembershipReduction = 132;

		// Token: 0x040022BB RID: 8891
		public const int RouterSolicitation = 133;

		// Token: 0x040022BC RID: 8892
		public const int RouterAdvertisement = 134;

		// Token: 0x040022BD RID: 8893
		public const int NeighborSolicitation = 135;

		// Token: 0x040022BE RID: 8894
		public const int NeighborAdvertisement = 136;

		// Token: 0x040022BF RID: 8895
		public const int Redirect = 137;

		// Token: 0x040022C0 RID: 8896
		public const int RouterRenumbering = 138;
	}
}
