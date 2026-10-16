using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000F6 RID: 246
	public enum Code
	{
		// Token: 0x04000424 RID: 1060
		Nop,
		// Token: 0x04000425 RID: 1061
		Break,
		// Token: 0x04000426 RID: 1062
		Ldarg_0,
		// Token: 0x04000427 RID: 1063
		Ldarg_1,
		// Token: 0x04000428 RID: 1064
		Ldarg_2,
		// Token: 0x04000429 RID: 1065
		Ldarg_3,
		// Token: 0x0400042A RID: 1066
		Ldloc_0,
		// Token: 0x0400042B RID: 1067
		Ldloc_1,
		// Token: 0x0400042C RID: 1068
		Ldloc_2,
		// Token: 0x0400042D RID: 1069
		Ldloc_3,
		// Token: 0x0400042E RID: 1070
		Stloc_0,
		// Token: 0x0400042F RID: 1071
		Stloc_1,
		// Token: 0x04000430 RID: 1072
		Stloc_2,
		// Token: 0x04000431 RID: 1073
		Stloc_3,
		// Token: 0x04000432 RID: 1074
		Ldarg_S,
		// Token: 0x04000433 RID: 1075
		Ldarga_S,
		// Token: 0x04000434 RID: 1076
		Starg_S,
		// Token: 0x04000435 RID: 1077
		Ldloc_S,
		// Token: 0x04000436 RID: 1078
		Ldloca_S,
		// Token: 0x04000437 RID: 1079
		Stloc_S,
		// Token: 0x04000438 RID: 1080
		Ldnull,
		// Token: 0x04000439 RID: 1081
		Ldc_I4_M1,
		// Token: 0x0400043A RID: 1082
		Ldc_I4_0,
		// Token: 0x0400043B RID: 1083
		Ldc_I4_1,
		// Token: 0x0400043C RID: 1084
		Ldc_I4_2,
		// Token: 0x0400043D RID: 1085
		Ldc_I4_3,
		// Token: 0x0400043E RID: 1086
		Ldc_I4_4,
		// Token: 0x0400043F RID: 1087
		Ldc_I4_5,
		// Token: 0x04000440 RID: 1088
		Ldc_I4_6,
		// Token: 0x04000441 RID: 1089
		Ldc_I4_7,
		// Token: 0x04000442 RID: 1090
		Ldc_I4_8,
		// Token: 0x04000443 RID: 1091
		Ldc_I4_S,
		// Token: 0x04000444 RID: 1092
		Ldc_I4,
		// Token: 0x04000445 RID: 1093
		Ldc_I8,
		// Token: 0x04000446 RID: 1094
		Ldc_R4,
		// Token: 0x04000447 RID: 1095
		Ldc_R8,
		// Token: 0x04000448 RID: 1096
		Dup,
		// Token: 0x04000449 RID: 1097
		Pop,
		// Token: 0x0400044A RID: 1098
		Jmp,
		// Token: 0x0400044B RID: 1099
		Call,
		// Token: 0x0400044C RID: 1100
		Calli,
		// Token: 0x0400044D RID: 1101
		Ret,
		// Token: 0x0400044E RID: 1102
		Br_S,
		// Token: 0x0400044F RID: 1103
		Brfalse_S,
		// Token: 0x04000450 RID: 1104
		Brtrue_S,
		// Token: 0x04000451 RID: 1105
		Beq_S,
		// Token: 0x04000452 RID: 1106
		Bge_S,
		// Token: 0x04000453 RID: 1107
		Bgt_S,
		// Token: 0x04000454 RID: 1108
		Ble_S,
		// Token: 0x04000455 RID: 1109
		Blt_S,
		// Token: 0x04000456 RID: 1110
		Bne_Un_S,
		// Token: 0x04000457 RID: 1111
		Bge_Un_S,
		// Token: 0x04000458 RID: 1112
		Bgt_Un_S,
		// Token: 0x04000459 RID: 1113
		Ble_Un_S,
		// Token: 0x0400045A RID: 1114
		Blt_Un_S,
		// Token: 0x0400045B RID: 1115
		Br,
		// Token: 0x0400045C RID: 1116
		Brfalse,
		// Token: 0x0400045D RID: 1117
		Brtrue,
		// Token: 0x0400045E RID: 1118
		Beq,
		// Token: 0x0400045F RID: 1119
		Bge,
		// Token: 0x04000460 RID: 1120
		Bgt,
		// Token: 0x04000461 RID: 1121
		Ble,
		// Token: 0x04000462 RID: 1122
		Blt,
		// Token: 0x04000463 RID: 1123
		Bne_Un,
		// Token: 0x04000464 RID: 1124
		Bge_Un,
		// Token: 0x04000465 RID: 1125
		Bgt_Un,
		// Token: 0x04000466 RID: 1126
		Ble_Un,
		// Token: 0x04000467 RID: 1127
		Blt_Un,
		// Token: 0x04000468 RID: 1128
		Switch,
		// Token: 0x04000469 RID: 1129
		Ldind_I1,
		// Token: 0x0400046A RID: 1130
		Ldind_U1,
		// Token: 0x0400046B RID: 1131
		Ldind_I2,
		// Token: 0x0400046C RID: 1132
		Ldind_U2,
		// Token: 0x0400046D RID: 1133
		Ldind_I4,
		// Token: 0x0400046E RID: 1134
		Ldind_U4,
		// Token: 0x0400046F RID: 1135
		Ldind_I8,
		// Token: 0x04000470 RID: 1136
		Ldind_I,
		// Token: 0x04000471 RID: 1137
		Ldind_R4,
		// Token: 0x04000472 RID: 1138
		Ldind_R8,
		// Token: 0x04000473 RID: 1139
		Ldind_Ref,
		// Token: 0x04000474 RID: 1140
		Stind_Ref,
		// Token: 0x04000475 RID: 1141
		Stind_I1,
		// Token: 0x04000476 RID: 1142
		Stind_I2,
		// Token: 0x04000477 RID: 1143
		Stind_I4,
		// Token: 0x04000478 RID: 1144
		Stind_I8,
		// Token: 0x04000479 RID: 1145
		Stind_R4,
		// Token: 0x0400047A RID: 1146
		Stind_R8,
		// Token: 0x0400047B RID: 1147
		Add,
		// Token: 0x0400047C RID: 1148
		Sub,
		// Token: 0x0400047D RID: 1149
		Mul,
		// Token: 0x0400047E RID: 1150
		Div,
		// Token: 0x0400047F RID: 1151
		Div_Un,
		// Token: 0x04000480 RID: 1152
		Rem,
		// Token: 0x04000481 RID: 1153
		Rem_Un,
		// Token: 0x04000482 RID: 1154
		And,
		// Token: 0x04000483 RID: 1155
		Or,
		// Token: 0x04000484 RID: 1156
		Xor,
		// Token: 0x04000485 RID: 1157
		Shl,
		// Token: 0x04000486 RID: 1158
		Shr,
		// Token: 0x04000487 RID: 1159
		Shr_Un,
		// Token: 0x04000488 RID: 1160
		Neg,
		// Token: 0x04000489 RID: 1161
		Not,
		// Token: 0x0400048A RID: 1162
		Conv_I1,
		// Token: 0x0400048B RID: 1163
		Conv_I2,
		// Token: 0x0400048C RID: 1164
		Conv_I4,
		// Token: 0x0400048D RID: 1165
		Conv_I8,
		// Token: 0x0400048E RID: 1166
		Conv_R4,
		// Token: 0x0400048F RID: 1167
		Conv_R8,
		// Token: 0x04000490 RID: 1168
		Conv_U4,
		// Token: 0x04000491 RID: 1169
		Conv_U8,
		// Token: 0x04000492 RID: 1170
		Callvirt,
		// Token: 0x04000493 RID: 1171
		Cpobj,
		// Token: 0x04000494 RID: 1172
		Ldobj,
		// Token: 0x04000495 RID: 1173
		Ldstr,
		// Token: 0x04000496 RID: 1174
		Newobj,
		// Token: 0x04000497 RID: 1175
		Castclass,
		// Token: 0x04000498 RID: 1176
		Isinst,
		// Token: 0x04000499 RID: 1177
		Conv_R_Un,
		// Token: 0x0400049A RID: 1178
		Unbox,
		// Token: 0x0400049B RID: 1179
		Throw,
		// Token: 0x0400049C RID: 1180
		Ldfld,
		// Token: 0x0400049D RID: 1181
		Ldflda,
		// Token: 0x0400049E RID: 1182
		Stfld,
		// Token: 0x0400049F RID: 1183
		Ldsfld,
		// Token: 0x040004A0 RID: 1184
		Ldsflda,
		// Token: 0x040004A1 RID: 1185
		Stsfld,
		// Token: 0x040004A2 RID: 1186
		Stobj,
		// Token: 0x040004A3 RID: 1187
		Conv_Ovf_I1_Un,
		// Token: 0x040004A4 RID: 1188
		Conv_Ovf_I2_Un,
		// Token: 0x040004A5 RID: 1189
		Conv_Ovf_I4_Un,
		// Token: 0x040004A6 RID: 1190
		Conv_Ovf_I8_Un,
		// Token: 0x040004A7 RID: 1191
		Conv_Ovf_U1_Un,
		// Token: 0x040004A8 RID: 1192
		Conv_Ovf_U2_Un,
		// Token: 0x040004A9 RID: 1193
		Conv_Ovf_U4_Un,
		// Token: 0x040004AA RID: 1194
		Conv_Ovf_U8_Un,
		// Token: 0x040004AB RID: 1195
		Conv_Ovf_I_Un,
		// Token: 0x040004AC RID: 1196
		Conv_Ovf_U_Un,
		// Token: 0x040004AD RID: 1197
		Box,
		// Token: 0x040004AE RID: 1198
		Newarr,
		// Token: 0x040004AF RID: 1199
		Ldlen,
		// Token: 0x040004B0 RID: 1200
		Ldelema,
		// Token: 0x040004B1 RID: 1201
		Ldelem_I1,
		// Token: 0x040004B2 RID: 1202
		Ldelem_U1,
		// Token: 0x040004B3 RID: 1203
		Ldelem_I2,
		// Token: 0x040004B4 RID: 1204
		Ldelem_U2,
		// Token: 0x040004B5 RID: 1205
		Ldelem_I4,
		// Token: 0x040004B6 RID: 1206
		Ldelem_U4,
		// Token: 0x040004B7 RID: 1207
		Ldelem_I8,
		// Token: 0x040004B8 RID: 1208
		Ldelem_I,
		// Token: 0x040004B9 RID: 1209
		Ldelem_R4,
		// Token: 0x040004BA RID: 1210
		Ldelem_R8,
		// Token: 0x040004BB RID: 1211
		Ldelem_Ref,
		// Token: 0x040004BC RID: 1212
		Stelem_I,
		// Token: 0x040004BD RID: 1213
		Stelem_I1,
		// Token: 0x040004BE RID: 1214
		Stelem_I2,
		// Token: 0x040004BF RID: 1215
		Stelem_I4,
		// Token: 0x040004C0 RID: 1216
		Stelem_I8,
		// Token: 0x040004C1 RID: 1217
		Stelem_R4,
		// Token: 0x040004C2 RID: 1218
		Stelem_R8,
		// Token: 0x040004C3 RID: 1219
		Stelem_Ref,
		// Token: 0x040004C4 RID: 1220
		Ldelem_Any,
		// Token: 0x040004C5 RID: 1221
		Stelem_Any,
		// Token: 0x040004C6 RID: 1222
		Unbox_Any,
		// Token: 0x040004C7 RID: 1223
		Conv_Ovf_I1,
		// Token: 0x040004C8 RID: 1224
		Conv_Ovf_U1,
		// Token: 0x040004C9 RID: 1225
		Conv_Ovf_I2,
		// Token: 0x040004CA RID: 1226
		Conv_Ovf_U2,
		// Token: 0x040004CB RID: 1227
		Conv_Ovf_I4,
		// Token: 0x040004CC RID: 1228
		Conv_Ovf_U4,
		// Token: 0x040004CD RID: 1229
		Conv_Ovf_I8,
		// Token: 0x040004CE RID: 1230
		Conv_Ovf_U8,
		// Token: 0x040004CF RID: 1231
		Refanyval,
		// Token: 0x040004D0 RID: 1232
		Ckfinite,
		// Token: 0x040004D1 RID: 1233
		Mkrefany,
		// Token: 0x040004D2 RID: 1234
		Ldtoken,
		// Token: 0x040004D3 RID: 1235
		Conv_U2,
		// Token: 0x040004D4 RID: 1236
		Conv_U1,
		// Token: 0x040004D5 RID: 1237
		Conv_I,
		// Token: 0x040004D6 RID: 1238
		Conv_Ovf_I,
		// Token: 0x040004D7 RID: 1239
		Conv_Ovf_U,
		// Token: 0x040004D8 RID: 1240
		Add_Ovf,
		// Token: 0x040004D9 RID: 1241
		Add_Ovf_Un,
		// Token: 0x040004DA RID: 1242
		Mul_Ovf,
		// Token: 0x040004DB RID: 1243
		Mul_Ovf_Un,
		// Token: 0x040004DC RID: 1244
		Sub_Ovf,
		// Token: 0x040004DD RID: 1245
		Sub_Ovf_Un,
		// Token: 0x040004DE RID: 1246
		Endfinally,
		// Token: 0x040004DF RID: 1247
		Leave,
		// Token: 0x040004E0 RID: 1248
		Leave_S,
		// Token: 0x040004E1 RID: 1249
		Stind_I,
		// Token: 0x040004E2 RID: 1250
		Conv_U,
		// Token: 0x040004E3 RID: 1251
		Arglist,
		// Token: 0x040004E4 RID: 1252
		Ceq,
		// Token: 0x040004E5 RID: 1253
		Cgt,
		// Token: 0x040004E6 RID: 1254
		Cgt_Un,
		// Token: 0x040004E7 RID: 1255
		Clt,
		// Token: 0x040004E8 RID: 1256
		Clt_Un,
		// Token: 0x040004E9 RID: 1257
		Ldftn,
		// Token: 0x040004EA RID: 1258
		Ldvirtftn,
		// Token: 0x040004EB RID: 1259
		Ldarg,
		// Token: 0x040004EC RID: 1260
		Ldarga,
		// Token: 0x040004ED RID: 1261
		Starg,
		// Token: 0x040004EE RID: 1262
		Ldloc,
		// Token: 0x040004EF RID: 1263
		Ldloca,
		// Token: 0x040004F0 RID: 1264
		Stloc,
		// Token: 0x040004F1 RID: 1265
		Localloc,
		// Token: 0x040004F2 RID: 1266
		Endfilter,
		// Token: 0x040004F3 RID: 1267
		Unaligned,
		// Token: 0x040004F4 RID: 1268
		Volatile,
		// Token: 0x040004F5 RID: 1269
		Tail,
		// Token: 0x040004F6 RID: 1270
		Initobj,
		// Token: 0x040004F7 RID: 1271
		Constrained,
		// Token: 0x040004F8 RID: 1272
		Cpblk,
		// Token: 0x040004F9 RID: 1273
		Initblk,
		// Token: 0x040004FA RID: 1274
		No,
		// Token: 0x040004FB RID: 1275
		Rethrow,
		// Token: 0x040004FC RID: 1276
		Sizeof,
		// Token: 0x040004FD RID: 1277
		Refanytype,
		// Token: 0x040004FE RID: 1278
		Readonly
	}
}
