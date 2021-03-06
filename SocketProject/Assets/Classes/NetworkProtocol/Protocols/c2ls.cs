using System;

namespace twp
{
	namespace protocol
	{
		public class c2ls
		{
			public const ushort kMSGIDX_REQ_CHARACTER_LIST = (ushort)protocol.ERange.MSG_BASE_C2LS + 1;
			public const ushort kMSGIDX_REQ_CREATE_CHARACTER = (ushort)protocol.ERange.MSG_BASE_C2LS + 2;
			
			            //
            // 请求角色列表信息
            //
            public class ReqCharacterList : PackBaseCTS
            {
                public ReqCharacterList()
                {
					header = kMSGIDX_REQ_CHARACTER_LIST;
				}
				
				public new byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray();
					bin.Put(base.ToBin());
					
					return bin.GetData();
				}
            };
			
			 //
            // 请求创建角色
            //
            public class ReqCreateCharacter : PackBaseCTS
            {
                public twp.app.unit.CharacterCreationParam char_data = new twp.app.unit.CharacterCreationParam();
            
                public ReqCreateCharacter()
                {
					header = kMSGIDX_REQ_CREATE_CHARACTER;	
				}
				
				public new byte[] ToBin()
				{
					NetSocket.ByteArray bin = new NetSocket.ByteArray();
					
					bin.Put(base.ToBin());
					bin.Put(char_data.ToBin());
					
					return bin.GetData();
				}

            };
		}
	}
}