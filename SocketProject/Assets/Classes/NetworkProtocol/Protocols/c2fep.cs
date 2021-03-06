using System;

namespace twp
{
	namespace protocol
	{
		public class c2fep
		{
			static public uint MAKE_PAIR32(uint major, uint minor)
			{
				return (uint)(major | minor << 16);
			}
			
			public const ushort kMSGIDX_REQ_LOGIN = (ushort)protocol.ERange.MSG_BASE_C2FEP+1;
			public const ushort kMSGIDX_REQ_ENCRYPT_INFO = (ushort)protocol.ERange.MSG_BASE_C2FEP+2;
			
						            //
            // 请求登陆
            // 
            public class ReqLogin : PacketBase
            {
                // 标志
                public byte[] signiture;//3
                // 客户端协议版本
                public uint version;
				// 客户端版本
				public byte[] client_version;//32 + 1

                // 登录数据长度
                public UInt16 data_len;
                // 登录数据(帐号 密码等 变长数据)
                public byte[] data; //twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH  300
                
                public ReqLogin()
                {
					header = kMSGIDX_REQ_LOGIN;
					
					signiture = new byte[3];
                    signiture[0] = System.Convert.ToByte('t');
                    signiture[1] = System.Convert.ToByte('w');
                    signiture[2] = System.Convert.ToByte('p');
					
                    version = twp.protocol.c2fep.MAKE_PAIR32((uint)EVersion.PROTOCOL_VERSION_MAJOR, (uint)EVersion.PROTOCOL_VERSION_MINOR);//(uint)((uint)EVersion.PROTOCOL_VERSION_MAJOR | (uint)EVersion.PROTOCOL_VERSION_MINOR << 16);//MAKE_PAIR32(PROTOCOL_VERSION_MAJOR, PROTOCOL_VERSION_MINOR);
					
					client_version = new byte[32 + 1];
					client_version[32] = System.Convert.ToByte('\0');
					
                    data_len = 0;
					
					data = new byte[(uint)twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH];
                   // memset(&data, 0, sizeof(data));
                }
				
				new public byte[] ToBin()
				{
					NetSocket.ByteArray data_ = new NetSocket.ByteArray();
					
					data_.Put(header);
					for (int i=0; i<3; ++i)
					{
						data_.Put(signiture[i]);
					}
					data_.Put(version);
					for( uint i = 0; i < 32 + 1; ++i)
					{
						data_.Put(client_version[i]);	
					}
					data_.Put(data_len);
					for(uint i = 0; i < data_len/*(int)twp.app.EDef.LIMIT_LOGIN_DATA_LENGTH*/; ++i)
					{
						data_.Put(data[i]);
					}
					
					return data_.GetData();
				}
				
				
				/*
                inline uint32 get_pak_length()const
                {
                    return (sizeof(ReqLogin) - LIMIT_LOGIN_DATA_LENGTH + data_len);
                }

				inline bool check_packet() { return true; }

				inline bool check_string()
				{
					client_version[32] = '\0'; 
					return true;
				}
				*/
            }
			
			public class ReqEncryptInfo : PacketBase
			{
				// 标志
				public byte [] signiture;
				// 客户端版本
				public uint version;
				// 特殊标记
				public uint flag;
				
				public ReqEncryptInfo()
				{
					header = kMSGIDX_REQ_ENCRYPT_INFO;
					
					signiture = new byte[3];
					signiture[0] = System.Convert.ToByte('t');
                    signiture[1] = System.Convert.ToByte('w');
                    signiture[2] = System.Convert.ToByte('p');
					version =  twp.protocol.c2fep.MAKE_PAIR32((uint)EVersion.PROTOCOL_VERSION_MAJOR, (uint)EVersion.PROTOCOL_VERSION_MINOR);//(uint)((uint)EVersion.PROTOCOL_VERSION_MAJOR | (uint)EVersion.PROTOCOL_VERSION_MINOR << 16);
						
					flag = 0xffefef12;
				}
				
				new public byte[] ToBin()
				{
					NetSocket.ByteArray data = new NetSocket.ByteArray();
					
					data.Put(header);
					for (int i=0; i<3; ++i)
					{
						data.Put(signiture[i]);
					}
					data.Put(version);
					data.Put(flag);
					return data.GetData();
				}
			}
		}
	}
}
