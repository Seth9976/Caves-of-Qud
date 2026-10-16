using System;
using System.ComponentModel;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200059A RID: 1434
	public enum GraphicsFormat
	{
		// Token: 0x04001317 RID: 4887
		None,
		// Token: 0x04001318 RID: 4888
		R8_SRGB,
		// Token: 0x04001319 RID: 4889
		R8G8_SRGB,
		// Token: 0x0400131A RID: 4890
		R8G8B8_SRGB,
		// Token: 0x0400131B RID: 4891
		R8G8B8A8_SRGB,
		// Token: 0x0400131C RID: 4892
		R8_UNorm,
		// Token: 0x0400131D RID: 4893
		R8G8_UNorm,
		// Token: 0x0400131E RID: 4894
		R8G8B8_UNorm,
		// Token: 0x0400131F RID: 4895
		R8G8B8A8_UNorm,
		// Token: 0x04001320 RID: 4896
		R8_SNorm,
		// Token: 0x04001321 RID: 4897
		R8G8_SNorm,
		// Token: 0x04001322 RID: 4898
		R8G8B8_SNorm,
		// Token: 0x04001323 RID: 4899
		R8G8B8A8_SNorm,
		// Token: 0x04001324 RID: 4900
		R8_UInt,
		// Token: 0x04001325 RID: 4901
		R8G8_UInt,
		// Token: 0x04001326 RID: 4902
		R8G8B8_UInt,
		// Token: 0x04001327 RID: 4903
		R8G8B8A8_UInt,
		// Token: 0x04001328 RID: 4904
		R8_SInt,
		// Token: 0x04001329 RID: 4905
		R8G8_SInt,
		// Token: 0x0400132A RID: 4906
		R8G8B8_SInt,
		// Token: 0x0400132B RID: 4907
		R8G8B8A8_SInt,
		// Token: 0x0400132C RID: 4908
		R16_UNorm,
		// Token: 0x0400132D RID: 4909
		R16G16_UNorm,
		// Token: 0x0400132E RID: 4910
		R16G16B16_UNorm,
		// Token: 0x0400132F RID: 4911
		R16G16B16A16_UNorm,
		// Token: 0x04001330 RID: 4912
		R16_SNorm,
		// Token: 0x04001331 RID: 4913
		R16G16_SNorm,
		// Token: 0x04001332 RID: 4914
		R16G16B16_SNorm,
		// Token: 0x04001333 RID: 4915
		R16G16B16A16_SNorm,
		// Token: 0x04001334 RID: 4916
		R16_UInt,
		// Token: 0x04001335 RID: 4917
		R16G16_UInt,
		// Token: 0x04001336 RID: 4918
		R16G16B16_UInt,
		// Token: 0x04001337 RID: 4919
		R16G16B16A16_UInt,
		// Token: 0x04001338 RID: 4920
		R16_SInt,
		// Token: 0x04001339 RID: 4921
		R16G16_SInt,
		// Token: 0x0400133A RID: 4922
		R16G16B16_SInt,
		// Token: 0x0400133B RID: 4923
		R16G16B16A16_SInt,
		// Token: 0x0400133C RID: 4924
		R32_UInt,
		// Token: 0x0400133D RID: 4925
		R32G32_UInt,
		// Token: 0x0400133E RID: 4926
		R32G32B32_UInt,
		// Token: 0x0400133F RID: 4927
		R32G32B32A32_UInt,
		// Token: 0x04001340 RID: 4928
		R32_SInt,
		// Token: 0x04001341 RID: 4929
		R32G32_SInt,
		// Token: 0x04001342 RID: 4930
		R32G32B32_SInt,
		// Token: 0x04001343 RID: 4931
		R32G32B32A32_SInt,
		// Token: 0x04001344 RID: 4932
		R16_SFloat,
		// Token: 0x04001345 RID: 4933
		R16G16_SFloat,
		// Token: 0x04001346 RID: 4934
		R16G16B16_SFloat,
		// Token: 0x04001347 RID: 4935
		R16G16B16A16_SFloat,
		// Token: 0x04001348 RID: 4936
		R32_SFloat,
		// Token: 0x04001349 RID: 4937
		R32G32_SFloat,
		// Token: 0x0400134A RID: 4938
		R32G32B32_SFloat,
		// Token: 0x0400134B RID: 4939
		R32G32B32A32_SFloat,
		// Token: 0x0400134C RID: 4940
		B8G8R8_SRGB = 56,
		// Token: 0x0400134D RID: 4941
		B8G8R8A8_SRGB,
		// Token: 0x0400134E RID: 4942
		B8G8R8_UNorm,
		// Token: 0x0400134F RID: 4943
		B8G8R8A8_UNorm,
		// Token: 0x04001350 RID: 4944
		B8G8R8_SNorm,
		// Token: 0x04001351 RID: 4945
		B8G8R8A8_SNorm,
		// Token: 0x04001352 RID: 4946
		B8G8R8_UInt,
		// Token: 0x04001353 RID: 4947
		B8G8R8A8_UInt,
		// Token: 0x04001354 RID: 4948
		B8G8R8_SInt,
		// Token: 0x04001355 RID: 4949
		B8G8R8A8_SInt,
		// Token: 0x04001356 RID: 4950
		R4G4B4A4_UNormPack16,
		// Token: 0x04001357 RID: 4951
		B4G4R4A4_UNormPack16,
		// Token: 0x04001358 RID: 4952
		R5G6B5_UNormPack16,
		// Token: 0x04001359 RID: 4953
		B5G6R5_UNormPack16,
		// Token: 0x0400135A RID: 4954
		R5G5B5A1_UNormPack16,
		// Token: 0x0400135B RID: 4955
		B5G5R5A1_UNormPack16,
		// Token: 0x0400135C RID: 4956
		A1R5G5B5_UNormPack16,
		// Token: 0x0400135D RID: 4957
		E5B9G9R9_UFloatPack32,
		// Token: 0x0400135E RID: 4958
		B10G11R11_UFloatPack32,
		// Token: 0x0400135F RID: 4959
		A2B10G10R10_UNormPack32,
		// Token: 0x04001360 RID: 4960
		A2B10G10R10_UIntPack32,
		// Token: 0x04001361 RID: 4961
		A2B10G10R10_SIntPack32,
		// Token: 0x04001362 RID: 4962
		A2R10G10B10_UNormPack32,
		// Token: 0x04001363 RID: 4963
		A2R10G10B10_UIntPack32,
		// Token: 0x04001364 RID: 4964
		A2R10G10B10_SIntPack32,
		// Token: 0x04001365 RID: 4965
		A2R10G10B10_XRSRGBPack32,
		// Token: 0x04001366 RID: 4966
		A2R10G10B10_XRUNormPack32,
		// Token: 0x04001367 RID: 4967
		R10G10B10_XRSRGBPack32,
		// Token: 0x04001368 RID: 4968
		R10G10B10_XRUNormPack32,
		// Token: 0x04001369 RID: 4969
		A10R10G10B10_XRSRGBPack32,
		// Token: 0x0400136A RID: 4970
		A10R10G10B10_XRUNormPack32,
		// Token: 0x0400136B RID: 4971
		D16_UNorm = 90,
		// Token: 0x0400136C RID: 4972
		D24_UNorm,
		// Token: 0x0400136D RID: 4973
		D24_UNorm_S8_UInt,
		// Token: 0x0400136E RID: 4974
		D32_SFloat,
		// Token: 0x0400136F RID: 4975
		D32_SFloat_S8_UInt,
		// Token: 0x04001370 RID: 4976
		S8_UInt,
		// Token: 0x04001371 RID: 4977
		[Obsolete("Enum member GraphicsFormat.RGB_DXT1_SRGB has been deprecated. Use GraphicsFormat.RGBA_DXT1_SRGB instead (UnityUpgradable) -> RGBA_DXT1_SRGB", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		RGB_DXT1_SRGB,
		// Token: 0x04001372 RID: 4978
		RGBA_DXT1_SRGB = 96,
		// Token: 0x04001373 RID: 4979
		[Obsolete("Enum member GraphicsFormat.RGB_DXT1_UNorm has been deprecated. Use GraphicsFormat.RGBA_DXT1_UNorm instead (UnityUpgradable) -> RGBA_DXT1_UNorm", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		RGB_DXT1_UNorm,
		// Token: 0x04001374 RID: 4980
		RGBA_DXT1_UNorm = 97,
		// Token: 0x04001375 RID: 4981
		RGBA_DXT3_SRGB,
		// Token: 0x04001376 RID: 4982
		RGBA_DXT3_UNorm,
		// Token: 0x04001377 RID: 4983
		RGBA_DXT5_SRGB,
		// Token: 0x04001378 RID: 4984
		RGBA_DXT5_UNorm,
		// Token: 0x04001379 RID: 4985
		R_BC4_UNorm,
		// Token: 0x0400137A RID: 4986
		R_BC4_SNorm,
		// Token: 0x0400137B RID: 4987
		RG_BC5_UNorm,
		// Token: 0x0400137C RID: 4988
		RG_BC5_SNorm,
		// Token: 0x0400137D RID: 4989
		RGB_BC6H_UFloat,
		// Token: 0x0400137E RID: 4990
		RGB_BC6H_SFloat,
		// Token: 0x0400137F RID: 4991
		RGBA_BC7_SRGB,
		// Token: 0x04001380 RID: 4992
		RGBA_BC7_UNorm,
		// Token: 0x04001381 RID: 4993
		RGB_PVRTC_2Bpp_SRGB,
		// Token: 0x04001382 RID: 4994
		RGB_PVRTC_2Bpp_UNorm,
		// Token: 0x04001383 RID: 4995
		RGB_PVRTC_4Bpp_SRGB,
		// Token: 0x04001384 RID: 4996
		RGB_PVRTC_4Bpp_UNorm,
		// Token: 0x04001385 RID: 4997
		RGBA_PVRTC_2Bpp_SRGB,
		// Token: 0x04001386 RID: 4998
		RGBA_PVRTC_2Bpp_UNorm,
		// Token: 0x04001387 RID: 4999
		RGBA_PVRTC_4Bpp_SRGB,
		// Token: 0x04001388 RID: 5000
		RGBA_PVRTC_4Bpp_UNorm,
		// Token: 0x04001389 RID: 5001
		RGB_ETC_UNorm,
		// Token: 0x0400138A RID: 5002
		RGB_ETC2_SRGB,
		// Token: 0x0400138B RID: 5003
		RGB_ETC2_UNorm,
		// Token: 0x0400138C RID: 5004
		RGB_A1_ETC2_SRGB,
		// Token: 0x0400138D RID: 5005
		RGB_A1_ETC2_UNorm,
		// Token: 0x0400138E RID: 5006
		RGBA_ETC2_SRGB,
		// Token: 0x0400138F RID: 5007
		RGBA_ETC2_UNorm,
		// Token: 0x04001390 RID: 5008
		R_EAC_UNorm,
		// Token: 0x04001391 RID: 5009
		R_EAC_SNorm,
		// Token: 0x04001392 RID: 5010
		RG_EAC_UNorm,
		// Token: 0x04001393 RID: 5011
		RG_EAC_SNorm,
		// Token: 0x04001394 RID: 5012
		RGBA_ASTC4X4_SRGB,
		// Token: 0x04001395 RID: 5013
		RGBA_ASTC4X4_UNorm,
		// Token: 0x04001396 RID: 5014
		RGBA_ASTC5X5_SRGB,
		// Token: 0x04001397 RID: 5015
		RGBA_ASTC5X5_UNorm,
		// Token: 0x04001398 RID: 5016
		RGBA_ASTC6X6_SRGB,
		// Token: 0x04001399 RID: 5017
		RGBA_ASTC6X6_UNorm,
		// Token: 0x0400139A RID: 5018
		RGBA_ASTC8X8_SRGB,
		// Token: 0x0400139B RID: 5019
		RGBA_ASTC8X8_UNorm,
		// Token: 0x0400139C RID: 5020
		RGBA_ASTC10X10_SRGB,
		// Token: 0x0400139D RID: 5021
		RGBA_ASTC10X10_UNorm,
		// Token: 0x0400139E RID: 5022
		RGBA_ASTC12X12_SRGB,
		// Token: 0x0400139F RID: 5023
		RGBA_ASTC12X12_UNorm,
		// Token: 0x040013A0 RID: 5024
		YUV2,
		// Token: 0x040013A1 RID: 5025
		[Obsolete("Enum member GraphicsFormat.DepthAuto is obsolete. Use GraphicsFormat.None as a color format to indicate depth only rendering and DefaultFormat to get the default depth buffer format.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		DepthAuto,
		// Token: 0x040013A2 RID: 5026
		[Obsolete("Enum member GraphicsFormat.ShadowAuto is obsolete. Use GraphicsFormat.None as a color format to indicate depth only rendering, DefaultFormat to get the default shadow buffer format and ShadowSamplingMode.CompareDepths to enable shadowmap sampling.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ShadowAuto,
		// Token: 0x040013A3 RID: 5027
		[Obsolete("Enum member GraphicsFormat.VideoAuto is obsolete. Use DefaultFormat instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		VideoAuto,
		// Token: 0x040013A4 RID: 5028
		RGBA_ASTC4X4_UFloat,
		// Token: 0x040013A5 RID: 5029
		RGBA_ASTC5X5_UFloat,
		// Token: 0x040013A6 RID: 5030
		RGBA_ASTC6X6_UFloat,
		// Token: 0x040013A7 RID: 5031
		RGBA_ASTC8X8_UFloat,
		// Token: 0x040013A8 RID: 5032
		RGBA_ASTC10X10_UFloat,
		// Token: 0x040013A9 RID: 5033
		RGBA_ASTC12X12_UFloat,
		// Token: 0x040013AA RID: 5034
		D16_UNorm_S8_UInt
	}
}
