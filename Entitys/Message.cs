using System;

namespace PorAka
{
	public	class Message
	{
		public MessageType Type{ get; set; }

		public string Value{ get; set; }
	}

	public	enum MessageType
	{

		Notification,
		AlertOferta

	}
}

