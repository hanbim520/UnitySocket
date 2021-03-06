using System;

namespace twp
{
	namespace protocol
	{
		public class ws2c
		{
			public const ushort kMSGIDX_REP_ENTERGAME 							= (ushort)protocol.ERange.MSG_BASE_WS2C + 1;
			//map
			public const ushort kMSGIDX_REP_INITIAL_MAP_POSITION			= (ushort)protocol.ERange.MSG_BASE_WS2C + 2;
			public const ushort kMSGIDX_REP_MAP_INFO								= (ushort)protocol.ERange.MSG_BASE_WS2C + 3;
			public const ushort kMSGIDX_REP_UPDATE_MAP_INFO					= (ushort)protocol.ERange.MSG_BASE_WS2C + 4;
			//league
			public const ushort kMSGIDX_REP_CREATE_LEAGUE						= (ushort)protocol.ERange.MSG_BASE_WS2C + 20;
			public const ushort kMSGIDX_REP_APPLY_JOIN_IN_LEAGUE			= (ushort)protocol.ERange.MSG_BASE_WS2C + 21;
			public const ushort kMSGIDX_REP_SEARCH_LEAGUE						= (ushort)protocol.ERange.MSG_BASE_WS2C + 22;
			public const ushort kMSGIDX_REP_LEAGUE_INFO							= (ushort)protocol.ERange.MSG_BASE_WS2C + 23;
			
			public enum EEnterGameResult
			{
				E_FAILED_UNKNOWERROR	= 0,
				E_SUCCESS,
				E_FAILED_SERVERINTERNALERROR,
				E_FAILED_ACCOUNTNOTEXISTCHAR,
				E_FAILED_CHARNOTEXIST,
				E_FAILED_NOTSSLOADBEARING,
				E_FAILED_SCENENOTEXIST,
				E_FAILED_NEEDRENAME,
			}

			//已修改 2013-8-13
            // 进入游戏结果
            //
            public class RepEnterGame : PackBaseSTC
            {
                public enum Result
                {
                    E_FAILED_UNKNOWERROR = 0,
                    E_SUCCESS,
                    E_FAILED_SERVERINTERNALERROR,
                    E_FAILED_ACCOUNTNOTEXISTCHAR,	// 帐号中不存在此角色
                    E_FAILED_CHARNOTEXIST,			// 角色不存在
                    E_FAILED_NOTSSLOADBEARING,		// 没有SS承载场景
                    E_FAILED_SCENENOTEXIST,			// 场景不存在
                    E_FAILED_NEEDRENAME,			// 角色需要改名
                }
				public Result result;
            
                // ss服务器的id 给fep用 使用后 清0
                public UInt32 ss_idx;
                // 需要进入场景的角色id
                public UInt32 char_idx;

                public RepEnterGame()
				{
					header = kMSGIDX_REP_ENTERGAME;
					result = Result.E_FAILED_UNKNOWERROR;
				}
				
				public new void FromBin(NetSocket.ByteArray bin)
				{
					base.FromBin(bin);	
					
					int result_;bin.Get_(out result_);result = (Result)result_;
					bin.Get_(out ss_idx);
					bin.Get_(out char_idx);
				}

            };
		}
	}
}