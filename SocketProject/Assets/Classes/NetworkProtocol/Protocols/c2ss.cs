using System;
using twp.app.unit;
using twp.app;
using twp.app.chat;
using System.Runtime.InteropServices;

namespace twp
{
	namespace protocol
	{
		public class c2ss
		{							
			public const ushort kMSGIDX_SCENE_RES_LOADED = (ushort)protocol.ERange.MSG_BASE_C2SS + 1;
			public const ushort kMSGIDX_DEBUG_COMMAND = (ushort)protocol.ERange.MSG_BASE_C2SS + 2;
			///////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_CHAT = (ushort)protocol.ERange.MSG_BASE_C2SS + 3;
			public const ushort kMSGIDX_REQ_TRANSFER = (ushort)protocol.ERange.MSG_BASE_C2SS + 4;
			public const ushort kMSGIDX_REQ_CLIENT_SETTINGS = (ushort)protocol.ERange.MSG_BASE_C2SS + 10;
			public const ushort kMSGIDX_REQ_START_COMBAT = (ushort)protocol.ERange.MSG_BASE_C2SS + 11;

			///////////////////////////////////////////////////////////////////////
			// shop (40 - 59)
			////////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_SHOP_OPENSHOP = (ushort)protocol.ERange.MSG_BASE_C2SS + 40;// -- 打开商店
			public const ushort kMSGIDX_REQ_SHOP_BUYITEM = (ushort)protocol.ERange.MSG_BASE_C2SS + 41;// -- 购买商品
			public const ushort kMSGIDX_REQ_SHOP_SELLITEM = (ushort)protocol.ERange.MSG_BASE_C2SS + 42;// -- 出售商品
			
			////////////////////////////////////////////////////////////////////////
			// instance (60 - 79)
			///////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_COMBAT_OPERATION = (ushort)protocol.ERange.MSG_BASE_C2SS + 60;// -- 副本操作

			///////////////////////////////////////////////////////////////////////
			// area (80 - 89)
			///////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_AREA_LIST = (ushort)protocol.ERange.MSG_BASE_C2SS + 80;// -- 操作区域

			///////////////////////////////////////////////////////////////////////
			// mail (90 - 109)
			///////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_MAIL_LIST = (ushort)protocol.ERange.MSG_BASE_C2SS + 90;
			public const ushort kMSGIDX_REQ_READ_MAIL = (ushort)protocol.ERange.MSG_BASE_C2SS + 91;
			public const ushort kMSGIDX_REQ_SEND_MAIL = (ushort)protocol.ERange.MSG_BASE_C2SS + 92;
			public const ushort kMSGIDX_REQ_TAKE_MAIL = (ushort)protocol.ERange.MSG_BASE_C2SS + 93;
			public const ushort kMSGIDX_REQ_DELETE_MAIL = (ushort)protocol.ERange.MSG_BASE_C2SS + 94;
			public const ushort kMSGIDX_REQ_MAIL_TEST = (ushort)protocol.ERange.MSG_BASE_C2SS + 99;// -- 邮件测试接口

			///////////////////////////////////////////////////////////////////////
			// city (110 - 129)
			///////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_CITY_INFO = (ushort)protocol.ERange.MSG_BASE_C2SS + 110;
			public const ushort kMSGIDX_REQ_BUILD_OPERATION = (ushort)protocol.ERange.MSG_BASE_C2SS + 111;// -- 建筑操作
			public const ushort kMSGIDX_REQ_BUILD_MOVE = (ushort)protocol.ERange.MSG_BASE_C2SS + 112;// 请求建筑移动，或者批量移动
			///////////////////////////////////////////////////////////////////////
			// build effect (150 - 169)
			///////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_CITY_HERO_OPERATION = (ushort)protocol.ERange.MSG_BASE_C2SS + 150;

			//////////////////////////////////////////////////////////////////////////
			// 匹配操作  (170 - 179)
			//////////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_MATCH_OPERATE = (ushort)protocol.ERange.MSG_BASE_C2SS + 171;


			//////////////////////////////////////////////////////////////////////////
			// 好友操作（关系）(180 - 189)
			//////////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_RELATION_LIST = (ushort)protocol.ERange.MSG_BASE_C2SS + 181;// -- 查看关系列表
			public const ushort kMSGIDX_REQ_RELATION_OPERATION = (ushort)protocol.ERange.MSG_BASE_C2SS + 182;// -- 好友操作（添加,查看，删除，黑名单）

			//////////////////////////////////////////////////////////////////////////
			// city caches(190 - 199)
			//////////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_SHOW_CITY_DATE = (ushort)protocol.ERange.MSG_BASE_C2SS + 190;// -- 查看城市缓存数据
			
			//////////////////////////////////////////////////////////////////////////
			// test
			//////////////////////////////////////////////////////////////////////////
			public const ushort kMSGIDX_REQ_TEST_CREATE_BUILD = (ushort)protocol.ERange.MSG_BASE_C2SS + 700;    // -- 创建建筑
			public const ushort kMSGIDX_REQ_TEST_OPERATION_CITY = (ushort)protocol.ERange.MSG_BASE_C2SS + 701;  // -- 操作建筑


			//////////////////////////////////////////////////////////////////////////
			
			//
			// 载入场景完成
			//
			public class SceneResLoaded :  PackBaseCTS
			{
				public SceneResLoaded ()
				{
					header = kMSGIDX_SCENE_RES_LOADED;
				}

				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					return bin.GetData ();
				}
			};
			
  //TD-iOS
            // 调试命令
            //
            public class DebugCommand : PackBaseCTS
            {
                enum ELimit
				{ 
					CONTENT_LIMIT = 400
				};
				
				
                public UInt16 content_len;
                public byte[] content = new byte[(int)(ELimit.CONTENT_LIMIT+1)];    

                public DebugCommand()
                {
					header = kMSGIDX_DEBUG_COMMAND;
				}
				
				public new byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray();
					bin.Put(content_len);
					for (UInt16 i = 0; i < content_len; ++i) {
						bin.Put(content[i]);
					}
					return bin.GetData();
				}
          
            };
			//
			// 聊天(变长结构)
			//
			public class ReqChat :  PackBaseCTS
			{
				public ChatType chat_type;
				public UInt32 receive_idx;
				public string recriver_name;	// EUnitLimit.LIMIT_NAME_STR_LENGTH + 1
				public UInt16 chat_size;
				public string chat_txt; // ChatDefine.LIMIT_CHAT_TEXT_LENGTH + 1
				
				public ReqChat ()
				{
					header = kMSGIDX_REQ_CHAT;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					
					bin.Put ((UInt32)chat_type);
					bin.Put (receive_idx);
					byte[] charArray = new byte[(int)EUnitLimit.LIMIT_NAME_STR_LENGTH + 1];
					if (null != recriver_name) {
						byte[] nameArr = TextEncode.Convert.UTFString2ServerBin(recriver_name);

						for (int i = 0; i < charArray.Length && i < nameArr.Length; i++) {
							charArray [i] = nameArr [i];
						}
					}
					charArray [charArray.Length - 1] = (byte)'\0';
					bin.Put (charArray);
					
					if (null == chat_txt)
						return null;
					
					charArray = TextEncode.Convert.UTFString2ServerBin(chat_txt);
					/*
					charArray = new byte[(int)ChatDefine.LIMIT_CHAT_TEXT_LENGTH + 1];
					byte[] txtArr = System.Text.Encoding.UTF8.GetBytes(chat_txt);
					for ( int i = 0; i < charArray.Length && i < txtArr.Length; i++ )
					{
						charArray[i] = txtArr[i];
					}
					*/
					chat_size = (UInt16)(charArray.Length + 1 + bin.Length + 2);
					bin.Put (chat_size);
					bin.Put (charArray);
					bin.Put ((byte)'\0');
					
					return bin.GetData ();
				}
			};
			
			//
			// 请求场景传送
			//
			public class ReqTransfer : PackBaseCTS
			{
				// 目标场景类型id
				public UInt32 dst_scene_type_idx;
				// 目标场景实例id
				public UInt64 dst_scene_obj_idx;

				// 副本规则类型(创建副本时用到 暂时废弃)
				public twp.app.scene.CreateInstanceRule instance_create_rules = new twp.app.scene.CreateInstanceRule ();

				public ReqTransfer ()
				{
					header = kMSGIDX_REQ_TRANSFER;
					dst_scene_obj_idx = (UInt64)twp.app.scene.ELimit.INVALID_SCENE_OBJ_ID;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					
					bin.Put (dst_scene_type_idx);
					bin.Put (dst_scene_obj_idx);
					bin.Put (instance_create_rules.ToBin ());
					
					return bin.GetData ();
				}
				
			};
			
			///////////////////////////////////////////////////////////////////////
			// area (80 - 89)
			///////////////////////////////////////////////////////////////////////

			//
			// 区域操作
			//

			public class ReqAreaShowCityBase : PackBaseCTS
			{
				public byte area;//area 0 ~ 6 的地图?
				public ReqAreaShowCityBase ()
				{
					header = kMSGIDX_REQ_AREA_LIST;
				}

				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					bin.Put (area);
					return bin.GetData ();
				}
			};

			public class ReqAreaShowCityElem
			{
				public byte row;//row
				public byte col;//col
				public byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (row);
					bin.Put (col);
					return bin.GetData ();
				}
			};
			
			public class VarLenStruct_ReqAreaShowCityBase: ReqAreaShowCityBase
			{
				public UInt16 amount;
				public ReqAreaShowCityElem[] elem ;//= new ReqAreaShowCityElem[25];

				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					bin.Put (amount);
					for (UInt16 i = 0; i < amount; ++i) {
						//TDMacro.Assert (elem [i] != null, "ReqAreaShowCityElem[" + i + "] is null");
						bin.Put (elem [i].ToBin ());
					}
					return bin.GetData ();
				}
			}
 
			public class ReqAreaShowCityList : VarLenStruct_ReqAreaShowCityBase
			{

			};


			//////////////////////////////////////////////////////////////////////////
			// instance
			//////////////////////////////////////////////////////////////////////////

			//
			// 副本操作
			//

			// -- begin --

			public class CombatUnitMove //12
			{
				
				public twp.app.unit.UnitID unit_idx = new twp.app.unit.UnitID();
				public byte row; // 目前是格子
				public byte col;
				public twp.app.scene.MoveFlag flag;

				public byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (unit_idx.ToBin());
					bin.Put (row);
					bin.Put (col);
					
					bin.Put((byte)flag);
					
					bin.appendMargin(18-12);
					return bin.GetData ();
				}
			}; // 单位移动

				public class CombatUnitBorn//11
			{
				public twp.app.unit.UnitType type;
				public UInt32 sub_type;
				public UInt32 obj_idx;
				public byte row;
				public byte col;
				
				public byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put((byte)type);
					bin.Put(sub_type);
					bin.Put(obj_idx);
					bin.Put(row);
					bin.Put(col);
					bin.appendMargin(18-11);
					return bin.GetData();
				}
			}; // 单位出生

			public class CombatChangeAttackDest//18
			{
				
				public twp.app.unit.UnitID src_unit = new twp.app.unit.UnitID();
				public twp.app.unit.UnitID dest_unit = new twp.app.unit.UnitID();
				
				
				public byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put(src_unit.ToBin());
					bin.Put(dest_unit.ToBin());
					return bin.GetData();
				}

			}; // 改变攻击目标

			public class ReqCombatOperation : PackBaseCTS
			{
				
				//C# 实现内联体(未经调试)
				[StructLayout(LayoutKind.Explicit/*,Size=8*/)]  
				public class InnerUnion
				{  
					[FieldOffset(0)]
					public CombatUnitBorn unit_born; // 单位出生
					[FieldOffset(0)] 
					public CombatUnitMove unit_move; // 单位移动
					[FieldOffset(0)] 
					public CombatChangeAttackDest change_dest; // 单位改变攻击目标
					
					public byte[] ToBin (twp.app.scene.CombatOperationType type)
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray ();
						switch(type)
						{
							
						case twp.app.scene.CombatOperationType.COMBAT_BASIC_OPERATION_TYPE_UNIT_BORN:
						{
							bin.Put(unit_born.ToBin());
							break;
						}
						case twp.app.scene.CombatOperationType.COMBAT_BASIC_OPERATION_TYPE_UNIT_MOVE:
						{
							bin.Put(unit_move.ToBin());
							break;
						}
						case twp.app.scene.CombatOperationType.COMBAT_BASIC_OPERATION_TYPE_UNIT_CHAGNE_DEST:
						{
							//TDMacro.Assert(false, "COMBAT_BASIC_OPERATION_TYPE_APPLY_COMBAT  UnHandle......");
							bin.Put(change_dest.ToBin());
							
							break;
						}
						case twp.app.scene.CombatOperationType.COMBAT_BASIC_OPERATION_TYPE_OVER:
						{
							bin.appendMargin(18);
							break;
						}
						default:
						{
							//TDMacro.Assert(false, "Not Process CombatOperationType");
							break;
						}
							
						}
						
						return bin.GetData ();
					}
				}
				
				public twp.app.scene.CombatOperationType type;
				public InnerUnion innerUnion = new InnerUnion ();

				public ReqCombatOperation ()
				{
					header = kMSGIDX_REQ_COMBAT_OPERATION;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					bin.Put ((byte)type);
					bin.Put (innerUnion.ToBin (type));
					return bin.GetData ();
				} 
				
			};

			// -- end --


			// -- end --
			//请求城市信息
			public class ReqCityInfo : PackBaseCTS
			{
				public byte map_id;
				public UInt64 city_id;

				public ReqCityInfo ()
				{
					header = kMSGIDX_REQ_CITY_INFO;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					bin.Put (map_id);
					bin.Put (city_id);
					return bin.GetData ();
				}

			};
			
			//建筑请求操作
			public class ReqBuildOperation : PackBaseCTS
			{
				public twp.app.build.BuildOperationEvent eventType;//byte
				
				static UInt16 MaxSize = sizeof(twp.app.build.BuildsType) + sizeof(twp.app.build.SubType) + sizeof(byte) + sizeof(byte);
				[StructLayout(LayoutKind.Explicit)]
				public class InnerUnion
				{
					public class Add_build//添加建筑 4
					{
						public twp.app.build.BuildsType main_type; //建筑类型 1byte
						public twp.app.build.SubType sub_type;// + 1byte
						public byte pos_x; //1byte 行
						public byte pos_y; //1byte 列
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put ((byte)main_type);
							bin.Put ((byte)sub_type);
							bin.Put (pos_x);
							bin.Put (pos_y);
							bin.appendMargin ((byte)(MaxSize - sizeof(twp.app.build.BuildsType) - sizeof(twp.app.build.SubType) - sizeof(byte) - sizeof(byte)));
							return bin.GetData ();
						}
					}

					public class Del_build  //删除建筑 3
					{
						public twp.app.build.BuildsType main_type;//1byte
						public UInt16 build_id;//2byte

						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put ((byte)main_type);
							bin.Put (build_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(twp.app.build.BuildsType) - sizeof(UInt16)));
							return bin.GetData ();
						}
					}
					
					public class UpLevel_Build
					{//升级建筑 3
						public twp.app.build.BuildsType main_type;//1 byte
						public UInt16 build_idx; // 2 byte
						public byte level;//1 byte
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put ((byte)main_type);
							bin.Put (build_idx);
							bin.Put (level);
							bin.appendMargin ((byte)(MaxSize - sizeof(twp.app.build.BuildsType) - sizeof(UInt16) - sizeof(byte)));
							return bin.GetData ();
						}
					}

					[FieldOffset(0)]
					public Add_build add_build;
					[FieldOffset(0)]
					public Del_build del_build;
					[FieldOffset(0)]
					public UpLevel_Build uplevel_build;
					
					public byte[] ToBin (twp.app.build.BuildOperationEvent type)
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray ();
						switch (type) {
						case twp.app.build.BuildOperationEvent.ADD_BUILD:
							{
								bin.Put (add_build.ToBin ());
							}
							break;
						case twp.app.build.BuildOperationEvent.DEL_BUILD:
							{
								bin.Put (del_build.ToBin ());
							}
							break;
						case twp.app.build.BuildOperationEvent.LEVEL_BUILD:
							{
								bin.Put (uplevel_build.ToBin ());	
							}
							break;
						default:
							break;
						}
						return bin.GetData ();
					}
				}
				public InnerUnion innerUnion = new InnerUnion ();
				
				public ReqBuildOperation ()
				{
					header = kMSGIDX_REQ_BUILD_OPERATION;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					bin.Put ((byte)eventType);
					bin.Put (innerUnion.ToBin (eventType));
					
					return bin.GetData ();
				}
			};
			
			//
			// 建筑移动  
			//
			public class BuildMoveElem
			{
				public twp.app.build.BuildsType main_type;
				public UInt16 build_idx;
				public byte pos_x; //移动到x坐标
				public byte pos_y; //移动到y坐标
				public byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put ((byte)main_type);
					bin.Put (build_idx);
					bin.Put (pos_x);
					bin.Put (pos_y);
					return bin.GetData ();
				}
			};
			
			public class ReqBuildMoveBase : PackBaseCTS
			{
				public ReqBuildMoveBase ()
				{
					header = kMSGIDX_REQ_BUILD_MOVE;
				}
			};
			
			public class VarLenStruct_ReqBuildMoveBase : ReqBuildMoveBase
			{
				public UInt16 amount;
				public BuildMoveElem[] elems = new BuildMoveElem[64];
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					bin.Put (amount);
					for (UInt16 i = 0; i < amount; ++i) {
						bin.Put (elems [i].ToBin ());
					}
					return bin.GetData ();
				}
			}

			public class ReqBuildMove : VarLenStruct_ReqBuildMoveBase
			{
			};
			
			////////////////////////////////////////////////////////////////////
			// city cache
			////////////////////////////////////////////////////////////////////
			public class ReqShowCityData : PackBaseCTS
			{
				public byte area;
				public UInt64 city_idx;
				public twp.app.city.ShowCityCacheFlag flag;

				public ReqShowCityData ()
				{
					header = kMSGIDX_REQ_SHOW_CITY_DATE;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					bin.Put (area);
					bin.Put (city_idx);
					bin.Put ((UInt32)flag);
					return bin.GetData ();
				}
			};
			
			// 匹配操作
			public class ReqMatchOperation : PackBaseCTS
			{
				public ReqMatchOperation ()
				{
					header = kMSGIDX_REQ_MATCH_OPERATE;
				}
			};

			// 城市英雄操作
			public class ReqCityHeroOperation : PackBaseCTS
			{
				public twp.app.city.CityHeroOperate type;
				public byte     area_id;
				public UInt64     city_id;
				
				//inner
				static  byte MaxSize = sizeof(UInt64) + sizeof(UInt32) + sizeof(twp.app.hero.COST_TYPE);
				[StructLayout(LayoutKind.Explicit)]
				public class InnerUnion
				{

					//创建浏览英雄
					public class Browser
					{
						public UInt16 build_id;

						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (build_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt16)));
							return bin.GetData ();
						}
					}
					
					//召唤英雄
					public class Summon
					{
						public UInt16  build_id;

						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (build_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt16)));
							return bin.GetData ();
						}
					}
					
					//放弃浏览英雄
					public class GiveUp_Browser
					{
						public UInt16 build_id;

						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (build_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt16)));
							return bin.GetData ();
						}
					}

		
					public class Level_Up//升级英雄
					{
						public UInt64 hero_id;// 8 byte
						public twp.app.hero.COST_TYPE cost_type; //消耗资源类型 4 byte
						//
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (hero_id);
							bin.Put ((int)cost_type);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt64) - sizeof(twp.app.hero.COST_TYPE)));
							return bin.GetData ();
						}
					};
			
					public class Up_Skill //升级技能
					{
						public UInt64 hero_id; // 8 byte
						public UInt32 skill_id; // 4 byte
						public twp.app.hero.COST_TYPE cost_type; //消耗资源类型
						//
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (hero_id);
							bin.Put (skill_id);
							bin.Put ((int)cost_type);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt64) - sizeof(UInt32) - sizeof(twp.app.hero.COST_TYPE)));
							return bin.GetData ();
						}
					};
					
					public class Train_Time//购买次数
					{
						public UInt64 hero_id; // 8 byte
						//
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (hero_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt64)));
							return bin.GetData ();
						}
					};
					
					public class Fire_Hero //解雇英雄
					{
						public UInt64 hero_id;// 8 byte
						//
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (hero_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt64)));
							return bin.GetData ();
						}
					};
					
					public class Train_Hero//培养英雄
					{
						public UInt64 hero_id; 
						//
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (hero_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt64)));
							return bin.GetData ();
						}
					};
					
					public class Build_Add_Hero//建筑入驻英雄
					{
						public UInt64 hero_id;// 8
						public twp.app.build.BuildsType main_type; //主类型 0 放进英雄背包
						public UInt16 build_idx;

						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put (hero_id);
							bin.Put ((byte)main_type);
							bin.Put (build_idx);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt64) - sizeof(twp.app.build.BuildsType) - sizeof(UInt16)));
							return bin.GetData ();
						}
					};
			
					public class Hero_List//请求英雄列表
					{
						public twp.app.hero.HERO_LIST_TYPE hero_list_type;
						
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put ((int)hero_list_type);
							bin.appendMargin ((byte)(MaxSize - sizeof(twp.app.hero.HERO_LIST_TYPE)));
							return bin.GetData ();
						}
					};
		
					public class Build_Del_Hero//建筑移除英雄
					{
						public UInt64 hero_id;
      					public UInt16 build_id;
						
						public byte[] ToBin()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray();
							bin.Put (hero_id);
							bin.Put (build_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt64) - sizeof(UInt16)));
							return bin.GetData();
						}
					}
					
					public class Clean_Skill_Cd //清除升级技能cd
				     {
				      	public UInt64 hero_id; 
				      	public UInt32 skill_id;
						
						public byte[] ToBin()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray();
							bin.Put (hero_id);
							bin.Put (skill_id);
							bin.appendMargin ((byte)(MaxSize - sizeof(UInt64) - sizeof(UInt32)));
							return bin.GetData();
						}
				     };
					
				     public class Clean_Train_Cd //清除培养cd
				     {
				      	public UInt64 hero_id;
						
						public byte[] ToBin()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray();
							bin.Put (hero_id);
							bin.appendMargin((byte)(MaxSize - sizeof(UInt64)));
							return bin.GetData();
						}
				     }; 
					
					[FieldOffset(0)]
					public Browser browse;
					[FieldOffset(0)]
					public Summon summon;
					[FieldOffset(0)]
					public GiveUp_Browser giveup_browse;
					[FieldOffset(0)]
					public Level_Up level_up;
					[FieldOffset(0)]
					public Up_Skill up_skill;
					[FieldOffset(0)]
					public Train_Time train_time;
					[FieldOffset(0)]
					public Fire_Hero fire_hero;
					[FieldOffset(0)]
					public Train_Hero train_hero;
					[FieldOffset(0)]
					public Build_Add_Hero build_add_hero;
					[FieldOffset(0)]
					public Hero_List hero_list;
					[FieldOffset(0)]
					public Build_Del_Hero build_del_hero;
					[FieldOffset(0)]
					public Clean_Skill_Cd clean_skill_cd;
					[FieldOffset(0)]
					public Clean_Train_Cd clean_train_cd;
					
					public byte[] ToBin (twp.app.city.CityHeroOperate type)
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray ();
						switch (type) {
						case twp.app.city.CityHeroOperate.CITY_CALL_BROWSER_HERO://召唤英雄前，浏览英雄
							{
								bin.Put (browse.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_CALL_HERO://召唤英雄
							{
								bin.Put (summon.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_GIVE_UP_BROWSER_HERO://放弃浏览英雄
							{
								bin.Put (giveup_browse.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_LEVEL_UP_HERO://升级英雄
							{
								bin.Put (level_up.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_HERO_LEARN_SKILL:/* CITY_LEVEL_UP_HERO_SKILL*/ //英雄技能升级
							{
								bin.Put (up_skill.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_BUY_TRAIN_TIMES://购买次数
							{
								bin.Put (train_time.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_FIRE_HERO://解雇英雄
							{
								bin.Put (fire_hero.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_TRAIN_HERO://
							{
								bin.Put (train_hero.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_BUILD_ADD_HERO:// 建筑入驻英雄
							{
								bin.Put (build_add_hero.ToBin ());
							}
							break;
						case twp.app.city.CityHeroOperate.CITY_REQ_HERO_LIST:
						{
							bin.Put (hero_list.ToBin());
						}
							break;
						case twp.app.city.CityHeroOperate.CITY_BUILD_DEL_HERO:
						{
								bin.Put(build_del_hero.ToBin());
						}
							break;
						case twp.app.city.CityHeroOperate.CITY_HERO_CLEAN_SKILL_CD:
						{
							bin.Put(clean_skill_cd.ToBin());	
						}
							break;
						case twp.app.city.CityHeroOperate.CITY_HERO_CLEAN_TRAIN_CD://清除培养cd
						{
							bin.Put(clean_train_cd.ToBin());
						}
							break;
						default:
							break;
						}
						
						return bin.GetData ();
					}
				}
				public InnerUnion innerUnion = new InnerUnion ();
    
				public ReqCityHeroOperation ()
				{
					header = kMSGIDX_REQ_CITY_HERO_OPERATION;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());//10
					bin.Put ((byte)type);//1
					bin.Put (area_id);//1
					bin.Put (city_id);//8
					bin.Put (innerUnion.ToBin (type));//11
					
					return bin.GetData ();
				}
			};

			//////build --hero end---
			
		//
			// 打开商城
			//
			public class  ReqShopList : PackBaseCTS
			{
				public UInt32 shop_idx;
				
				public ReqShopList()
				{
					header = kMSGIDX_REQ_SHOP_OPENSHOP;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put (base.ToBin ());
					bin.Put(shop_idx);
					
					return bin.GetData();
				}
			};
			
			//
			// 购买物品
			//
			public class ReqShopItemBuy : PackBaseCTS
			{
				public UInt32	shop_idx;
				public UInt32	item_idx;			// 物品ID 
				public UInt32	item_count;		// 购买物品数量
				
				public enum BuyType
				{
					BT_BuyBuilding,
					BT_BuyResource,
				}
				public BuyType buyType;
				
				[StructLayout(LayoutKind.Explicit)]
				public class InnerUnion
				{
					public class BuyBuilding
					{
						public byte row;
						public byte col;
						
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put(row);
							bin.Put(col);
							
							return bin.GetData();
						}
					};
					
					public class BuyResource
					{
						public byte[] ToBin ()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.appendMargin(sizeof(byte) + sizeof(byte));
							
							return bin.GetData();
						}
					};
					
					[FieldOffset(0)]
					public BuyBuilding buy_build = new BuyBuilding();
					[FieldOffset(0)]
					public BuyResource buy_resource = new BuyResource();
					
					public byte[] ToBin (BuyType type)
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray ();
						switch( type )
						{
							case BuyType.BT_BuyBuilding:
								bin.Put(buy_build.ToBin());
								break;
							case BuyType.BT_BuyResource:
								bin.Put(buy_resource.ToBin());
								break;
							default:
								break;
						}
						
						return bin.GetData();
					}
				};
				
				public InnerUnion innerUnion = new InnerUnion ();
				
				public ReqShopItemBuy()
				{
					header = kMSGIDX_REQ_SHOP_BUYITEM;
				}
				
				public new byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put(base.ToBin());
					bin.Put(shop_idx);
					bin.Put(item_idx);
					bin.Put(item_count);
					bin.Put(innerUnion.ToBin(buyType));
					
					return bin.GetData();
				}
			};

			//
			// 出售物品
			//
			public class ReqShopItemSell : PackBaseCTS
			{
				public UInt32 shop_idx;
				public byte build_type;
				public byte build_sub_type;
				public UInt16 build_level;
				public UInt16 build_idx;
				
				public ReqShopItemSell()
				{
					header = kMSGIDX_REQ_SHOP_SELLITEM;
				}
				
				public new byte[] ToBin ()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put(base.ToBin());

					bin.Put(shop_idx);
					bin.Put(build_type);
					bin.Put(build_sub_type);
					bin.Put(build_level);
					bin.Put(build_idx);
					
					return bin.GetData();
				}
			}

			
			
			//////////////////////////////////////////////////////////////////////////
			//好友
			//////////////////////////////////////////////////////////////////////////
			
			//好友列表请求
			public class ReqRelationMemberList : PackBaseCTS
			{
				public ReqRelationMemberList()
				{
					header = kMSGIDX_REQ_RELATION_LIST;
				}
			}
			
			//操作好友
			public class ReqRelationMemberOperation : PackBaseCTS
			{
				twp.app.relation.OperationRelation operation_type = twp.app.relation.OperationRelation.OPERATION_RELATION_NULL;
				
				[StructLayout(LayoutKind.Explicit)]
				public class InnerUnion
				{
					public class Data
					{
						const int UNION_MAX_LEN = (int)twp.app.unit.EUnitLimit.LIMIT_NAME_STR_LENGTH+1;
						public UInt32 target_idx;
						
						public byte[] ToBin()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray ();
							bin.Put(target_idx);
							bin.appendMargin(UNION_MAX_LEN - sizeof(UInt32));
							
							return bin.GetData();
						}
					}
					
					public class AddName
					{
						string target_name = null; //twp::app::unit::LIMIT_NAME_STR_LENGTH+1
						
						public byte[] ToBin()
						{
							NetSocket.ByteArray bin = new NetSocket.ByteArray();
							bin.Put(TextEncode.Convert.UTFString2ServerBin(target_name));
							
							return bin.GetData();
						}
					}
					
					[FieldOffset(0)]
					public Data data;
					[FieldOffset(0)]
					public AddName add_name;
					
					public byte[] ToBin(twp.app.relation.OperationRelation ot)
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						
						switch(ot)
						{
							case twp.app.relation.OperationRelation.OPERATION_RELATION_ADD_ID:
							{
								bin.Put(data.ToBin());
								break;
							}
							case twp.app.relation.OperationRelation.OPERATION_RELATION_ADD_NAME:
							{
								bin.Put(add_name.ToBin());
								break;
							}
						}
						
						return bin.GetData();
					}
				}
				
				public InnerUnion innerUnion = new InnerUnion ();
				
				public ReqRelationMemberOperation()
				{
					header = kMSGIDX_REQ_RELATION_OPERATION;
				}
			
				public new byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray ();
					bin.Put(base.ToBin());
					bin.Put((byte)operation_type);
					bin.Put(innerUnion.ToBin(operation_type));
					
					return bin.GetData();
				}
			}
		}	
	}
}
