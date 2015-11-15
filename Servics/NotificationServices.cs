using System;
using Android.App;
using Android.OS;
using Android.Content;
using System.Threading;


namespace PorAka
{
	[Service]
	public class NotificationServices:Service
	{
		private Timer _TimerStartService;


		public NotificationServices ()
		{
		}


		public override void OnCreate ()
		{
			base.OnCreate ();

			StartForeground ();
			TimerStartService ();
		}
		private void StartForeground ()
		{

			var pendingIntent = PendingIntent.GetActivity (ApplicationContext, 0,
				new Intent (ApplicationContext, typeof(MainActivity)),
				PendingIntentFlags.UpdateCurrent);

			var notification = new Notification {
				TickerText = new Java.Lang.String ("PorAkÀ!"),
				Icon = Resource.Drawable.ic_action_icon
			};
			notification.Flags |= NotificationFlags.OngoingEvent;
			notification.SetLatestEventInfo (ApplicationContext,  "PorAkÀ.co","Es mas barato!", pendingIntent);
			StartForeground (101, notification);
		}

	
		public override IBinder OnBind (Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{






			return StartCommandResult.Sticky;
		}

		public override void OnDestroy ()
		{


			
			base.OnDestroy ();

		}

		private void TimerStartService ()
		{
			try {
				int Intervalo = (10000);

				if (_TimerStartService != null) {
					_TimerStartService.Dispose ();
				}

				_TimerStartService = new System.Threading.Timer (delegate {
					
				

				}, null, 0, Intervalo);


			} catch (Exception) {
				
			}



		}


	}
}
