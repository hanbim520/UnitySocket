using System;
using System.Runtime;
using System.Runtime.InteropServices;
using UnityEngine;

namespace twp
{
	namespace app
	{
		namespace unit
		{
			public class TDCONST
			{
				public const UInt32 MIN_CITY_ID = 1000000;
				public const UInt32 MAP_FLAGS = 1000000;
				public const UInt32 MIN_CHAR_ID = 1000000;
				public const UInt32 POS_FLAGS = 1000;
		#if false
		//#define SET_CITY_ID(srv_realm, srv_cluster, map_id) srv_realm * MIN_CITY_ID  //创建城市id 
		//#define SET_CHAR_ID(srv_realm, srv_cluster) srv_realm * MIN_CHAR_ID
		//#define SET_MAP_AND_POSITION_FLAG(x, y, idx) (y) + (x)*POS_FLAGS + (idx)*MAP_FLAGS  //保存地图和坐标
		//#define GET_FLAGS_MAP_ID(flags) (flags) / MAP_FLAGS							//获得地图id
		//#define GET_FLAGS_X(flags) ((flags) % MAP_FLAGS) / POS_FLAGS						//获得x坐标
		//#define GET_FLAGS_Y(flags) (((flags) % MAP_FLAGS) / POS_FLAGS) % POS_FLAGS				//获得y坐标
		//#define GET_X(pos, weight) (pos) % (weight)									//一维坐标转换
		//#define GET_Y(pos, height) (pos) % (height)									//二维坐标转换
		#endif
				public const UInt32 MAX_MAP_NAME = 32;
				public const UInt32 MAX_MAP_DESCRIBE = 1024;
				public const UInt32 MAX_CITY_NAME = 32;
				public const UInt32 MAIN_CITY_MAP_ID = 1	;	//固定主城的id
			}

			public enum EUnitLimit
			{
				//////////////////////////////////////////////////////////////////////////
				// 系统保留id
				//////////////////////////////////////////////////////////////////////////
				// 游戏内系统unit id
				UNIT_INTERNAL_SYSTEM_ID = 1,
				// 后台系统unit id(gm kf等)
				UNIT_GM_SYSTEM_ID = 2,
				// 游戏事件unit id
				UNIT_GAME_EVENT_ID = 3,
				//////////////////////////////////////////////////////////////////////////

				// 角色最小id
				ID_MIN_CHARACTER = 1,
				ID_MIN_FRESH_CITY = (int)TDCONST.MIN_CITY_ID,		//城市的最小id

				INVALID_CHARINDEX = 0,			// 角色索引的非法值
				INVALID_UNIT_OBJ_ID  = 0,		// 非法unit对象id
				INVALID_UNIT_TYPE_ID  = 0,		// 非法unit类型id
				LIMIT_NAME_STR_LENGTH = 16,		// 角色名称长度限制 bytes
				LIMIT_NAME_STR_MIN_LENGTH = 4,	// 角色名称最小长度限制  bytes
				LIMIT_CHARACTER_NUM = 1,		// 每账号角色数量限制
				LIMIT_CHARACTER_LEVEL = 300,    // 角色最大等级
				LIMIT_GUILD_STR_LENGTH = 16,	// 公会名字最大长度
				LIMIT_SIGN_NAME_STR_LENGTH = 32,// 签名最大长度
				
				
	
				

				//league
				ID_MIN_LEAGUE = 1000000,
				MAX_LEAGUE_NAME = 32,
				MAX_LEAGUE_NOTICE = 256,
				MAX_LEAGUE_NUMBER = 50,
				CREATE_LEAGUE_COPPER = 40000,
				
				// 阵营id 进攻方
				CAMP_ATTACKER_ID = 0,
				CAMP_DEFENDER_ID,
				CAMP_OBSERVER_ID,
				MAX_CAMP_COUNT,
			};
			
			public enum UnitType
			{
				UNITTYPE_PLAYER,    // 玩家
				UNITTYPE_MONSTER,   // 怪物
				UNITTYPE_SYSTEM,    // system

				UNITTYPE_TRAP,            // 各种塔
				UNITTYPE_CRAFTSMAN_HOUSE, // 工匠屋
				UNITTYPE_WALL,            // 城墙
				UNITTYPE_ALLIANCE_HALL,   // 联盟大厅
				UNITTYPE_TRADE_CENTRALITY,// 贸易中心
				UNITTYPE_HERO_PALACE,     // 英雄殿堂
				UNITTYPE_ENLIST_TOWER,    // 招募塔
				UNITTYPE_MAIN_CITY,       // 主城
				UNITTYPE_FARMLANG,        // 农田
				UNITTYPE_SHOP,            // 商铺
				UNITTYPE_BARN,            // 粮仓
				UNITTYPE_EXCHEUQER,       // 金库
				UNITTYPE_RESIDENT,        // 民居

				UNITTYPE_COUNT,
				// 非法类型
				UNITTYPE_INVALID = 0xff,
			};
			
			public class UnitID //9
			{
				public class Player //4
				{
					public  UInt32 char_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out char_idx);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(char_idx);
						bin.appendMargin(8-4);
						return bin.GetData();
					}
				}
				
				public class Monster // 8
				{
					public UInt32 type_idx;
					public UInt32 obj_idx;
					
					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out type_idx);
						bin.Get_ (out obj_idx);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(type_idx);
						bin.Put(obj_idx);
						return bin.GetData();
					}
				}
				
				public class System //4
				{
					public UInt32 system_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out system_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(system_idx);
						bin.appendMargin(8-4);
						return bin.GetData();
					}
				}
				
				public class Trap // 8
				{
					public UInt32 type_idx;
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out type_idx);
						bin.Get_ (out obj_idx);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(type_idx);
						bin.Put(obj_idx);
						return bin.GetData();
					}
				}
				
				public class Wall //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Craftsman //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Alliance //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Trade_Centralit //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Hero_Palace //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Enlist_Tower//4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Main_City //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Farmlage //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Shop //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Barn //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Excheuqer //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				public class Resident //4
				{
					public UInt32 obj_idx;

					public void FromBin (NetSocket.ByteArray bin)
					{
						bin.Get_ (out obj_idx);
						bin.Move(8-4);
					}
					public byte[] ToBin()
					{
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						bin.Put(obj_idx);
						bin.appendMargin(8-4);
						return  bin.GetData();
					}
				}
				
				
				[StructLayout(LayoutKind.Explicit/*,Size=8*/)]  
				public class InnerUnion
				{
					[FieldOffset(0)] 
					public Player  player = new Player();
					
					[FieldOffset(0)] 
					public Monster  monster = new Monster();
					
					[FieldOffset(0)] 
					public System system = new System();

					// 各种塔
					[FieldOffset(0)] 
					public Trap trap = new Trap();

					// 城墙
					[FieldOffset(0)] 
					public Wall wall = new Wall();
					// 工匠屋
					[FieldOffset(0)] 
					public Craftsman craftsman = new Craftsman();

					// 联盟大厅
					[FieldOffset(0)] 
					public Alliance alliance = new Alliance();

					// 贸易中心
					[FieldOffset(0)] 
					public Trade_Centralit trade_centralit = new Trade_Centralit();

					// 英雄殿堂
					[FieldOffset(0)] 
					public Hero_Palace hero_palace = new Hero_Palace();
					
					[FieldOffset(0)] 
					public Enlist_Tower enlist_tower = new Enlist_Tower();

					// 主城
					[FieldOffset(0)] 
					public Main_City main_city = new Main_City();

					// 农田
					[FieldOffset(0)]  
					public Farmlage farmlage = new Farmlage();

					// 商铺
					[FieldOffset(0)]  
					public Shop shop = new Shop();

					// 粮仓
					[FieldOffset(0)] 
					public Barn barn = new Barn();

					// 金库
					[FieldOffset(0)] 
					public Excheuqer excheuqer = new Excheuqer();

					// 民居
					[FieldOffset(0)] 
					public Resident resident = new Resident();
					
					public void FromBin(twp.app.unit.UnitType type, NetSocket.ByteArray bin)
					{
						switch(type)
						{
						case twp.app.unit.UnitType.UNITTYPE_PLAYER:
						{
							player.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_MONSTER:
						{
							monster.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_SYSTEM:
						{
							system.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_TRAP:
						{
							trap.FromBin(bin);
							
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_WALL:
						{
							wall.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_CRAFTSMAN_HOUSE:
						{
							craftsman.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_ALLIANCE_HALL:
						{
							alliance.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_TRADE_CENTRALITY:
						{
							trade_centralit.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_HERO_PALACE:
						{
							hero_palace.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_ENLIST_TOWER:
						{
							enlist_tower.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_MAIN_CITY:
						{
							main_city.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_FARMLANG:
						{
							farmlage.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_SHOP:
						{
							shop.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_BARN:
						{
							barn.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_EXCHEUQER:
						{
							excheuqer.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_RESIDENT:
						{
							resident.FromBin(bin);
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_INVALID:
						{
							bin.Move(8);
							break;
						}
						default:
						{
							Debug.LogError("Unhandle unity type = " + type);
							break;
						}
							
						}
					}
					
					public byte[] ToBin(twp.app.unit.UnitType type)
					{
						
						NetSocket.ByteArray bin = new NetSocket.ByteArray();
						
						switch(type)
						{
						case twp.app.unit.UnitType.UNITTYPE_PLAYER:
						{
							bin.Put(player.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_MONSTER:
						{
							bin.Put(monster.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_SYSTEM:
						{
							bin.Put(system.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_TRAP:
						{
							bin.Put(trap.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_WALL:
						{
							bin.Put(wall.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_CRAFTSMAN_HOUSE:
						{
							bin.Put(craftsman.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_ALLIANCE_HALL:
						{
							bin.Put(alliance.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_TRADE_CENTRALITY:
						{
							bin.Put(trade_centralit.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_HERO_PALACE:
						{
							bin.Put(hero_palace.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_ENLIST_TOWER:
						{
							bin.Put(enlist_tower.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_MAIN_CITY:
						{
							bin.Put(main_city.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_FARMLANG:
						{
							bin.Put(farmlage.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_SHOP:
						{
							bin.Put(shop.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_BARN:
						{
							bin.Put(barn.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_EXCHEUQER:
						{
							bin.Put(excheuqer.ToBin());
							break;
						}
						case twp.app.unit.UnitType.UNITTYPE_RESIDENT:
						{
							bin.Put(resident.ToBin());
							break;
						}
						default:
						{
							Debug.LogError("Unhandle unity type = " + type);
							break;
						}
							
						}
						return bin.GetData();
					}
				}
				
				public twp.app.unit.UnitType type;
				
				public InnerUnion innerUnion = new InnerUnion();
				
				public void FromBin(NetSocket.ByteArray bin)
				{
					byte temp;
					bin.Get_(out temp);
					type = (twp.app.unit.UnitType)temp;
					innerUnion.FromBin(type, bin);
				}
				
				public byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray();
					bin.Put((byte)type);
					bin.Put(innerUnion.ToBin(type));
					return bin.GetData();
				}
				
			};


			//
			// unit属性修改类型，的定义
			//
			public enum UnitModType
			{
				// 角色类型
				UMT_char_type,
				// 等级
				UMT_level,
				// 角色当前标记
				UMT_flags,
				//// 玩家被屏蔽操作标志位
				//UMT_operate_flags,
				// 金币
				UMT_gold,
				// 铜
				UMT_copper,
				// 银
				UMT_silver,
				// 粮食
				UMT_food,
				// 最大粮食
				UMT_max_food,
				// 人口
				UMT_population,
				// 最大人口
				UMT_max_population,
				// 掠夺次数
				UMT_plunder_count,
				// 被掠夺次数
				UMT_exploit_count,
				// 战斗积分
				UMT_battle_integral,
				// 区域id
				UMT_area_idx,
				// 最后副本通关id
				UMT_last_passed_instance_type_idx,
				// 最后查看城市ID
				UMT_last_citys_idx,
				// 最后一次每日重置的时间
				UMT_last_daily_reset_time,
				// 最后一次每日重置的时间
				UMT_last_weekly_reset_time,
				// 角色最大属性数量
				UMT_CHARACTER_MAX_COUNT,
				//////////////////////////////////////////////////////////////////////////
				// 以下为其他unit 使用
				//*************************************
				// - 怪物
				//*************************************
				// 怪物子类型
				UMT_type_idx,
				// 血量
				UMT_hp,
				// 最大血量
				UMT_max_hp,
				// 攻击力
				UMT_attack,
				// 防御力
				UMT_defense,
				// 攻击半径
				UMT_attack_radius,
				// 视野半径
				UMT_view_radius,
				// 追击半径
				UMT_pursue_radius,
				// 周身半径
				UMT_collise_radius,
				//*************************************
				// - 炮塔
				//*************************************
				// unit属性最大值
				UMT_UNIT_MAX_COUNT,
				// 非法属性
				UMT_INVALID = 0xff,
			};

		}
	}
}