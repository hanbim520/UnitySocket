using System;

namespace twp
{
	namespace protocol
	{
		public class c2ws
		{
			public const ushort kMSGIDX_REQ_ENTERGAME 							= (ushort)protocol.ERange.MSG_BASE_C2WS + 1;
			public const ushort kMSGIDX_ENTER_SCENE 								= (ushort)protocol.ERange.MSG_BASE_C2WS + 2;
			//map
			public const ushort kMSGIDX_REQ_MOVE_SCREEN						= (ushort)protocol.ERange.MSG_BASE_C2WS + 3;
			public const ushort kMSGIDX_REQ_INITIAL_MAP_POSITION			= (ushort)protocol.ERange.MSG_BASE_C2WS + 4;
			public const ushort kMSGIDX_REQ_MAP_INFO								= (ushort)protocol.ERange.MSG_BASE_C2WS + 5;
			//league
			public const ushort kMSGIDX_REQ_CREATE_LEAGUE						= (ushort)protocol.ERange.MSG_BASE_C2WS + 20;
			public const ushort kMSGIDX_REQ_APPLY_JOIN_IN_LEAGUE			= (ushort)protocol.ERange.MSG_BASE_C2WS + 21;
			public const ushort kMSGIDX_REQ_SEARCH_LEAGUE					= (ushort)protocol.ERange.MSG_BASE_C2WS + 22;
			public const ushort kMSGIDX_REQ_LEAGUE_INFO							= (ushort)protocol.ERange.MSG_BASE_C2WS + 23;
			

			 //
            // 请求进入游戏
            //
            public class ReqEnterGame : PackBaseCTS
            {
                public UInt32 char_idx;

                public ReqEnterGame()
                {
					header = kMSGIDX_REQ_ENTERGAME;
				}
				
				public new byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray();				
					bin.Put(base.ToBin());
					bin.Put(char_idx);
					return bin.GetData();
				}
				
            };

						 //
            // 首次进入场景
            //
            public class EnterScene : PackBaseCTS
            {
                public EnterScene()
				{
					header = kMSGIDX_ENTER_SCENE;
				}

				public new byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray();	
					bin.Put(base.ToBin());
					return bin.GetData();
				}
            };
		}
	}
}

