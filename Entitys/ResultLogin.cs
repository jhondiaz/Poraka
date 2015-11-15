using System;

namespace PorAka
{
	public class ResultLogin<T>
	{
		public string Msg {
			get;
			set;
		}

		public T dato {
			get;
			set;
		}
	}
}

