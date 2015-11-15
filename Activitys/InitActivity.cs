
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Content.Res;
using Android.Support.V4.App;

namespace PorAka
{
	[Activity (Label = "PorAkà")]			
	public class InitActivity : FragmentActivity
	{
		private DrawerLayout _drawer;
		private MyActionBarDrawerToggle _drawerToggle;
		private ListView _drawerList;

		private string _drawerTitle;
		private string _title;
		private List<ItamDrawer> _planetTitles = new List<ItamDrawer> ();
		private BaseFragment fragment = null;
	
		public bool IsDiscografiFragment = false;
		private ImageView ImgLogo;
		private TextView TxtEntrar;
		private TextView TxtContact;
		public readonly DatosUser _DatosUser = new DatosUser ();
		private Boolean IsLoginVendors = false;
		private Boolean IsLoginClients = false;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.InitActivity);
			ColorDrawable colorDrawable = new ColorDrawable (Color.ParseColor (Colores.BaseColo));

			ActionBar.SetBackgroundDrawable (colorDrawable); 
			ActionBar.SetIcon (Resource.Drawable.ic_action_icon);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
			ActionBar.SetHomeButtonEnabled (true);



			_title = _drawerTitle = Title;


			try {


				_drawer = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
				_drawerList = FindViewById<ListView> (Resource.Id.left_drawer);		
				LoadMenu (2);
				var inputView = this.LayoutInflater.Inflate (Resource.Layout.ProcessBarView, null);

				ImgLogo = inputView.FindViewById<ImageView> (Resource.Id.ImgLogo);
				TxtEntrar = inputView.FindViewById<TextView> (Resource.Id.TxtEntrar);
				TxtContact = inputView.FindViewById<TextView> (Resource.Id.TxtContact);

				_drawerList.AddHeaderView (inputView);

				_drawer.SetDrawerShadow (Resource.Drawable.drawer_shadow_dark, (int)GravityFlags.Left);

				_drawerList.ItemClick += (sender, args) => SelectItem (args.Position);

				_drawerToggle = new MyActionBarDrawerToggle (this, _drawer,
					Resource.Drawable.ic_drawer_light,
					Resource.String.DrawerOpen,
					Resource.String.DrawerClose);

				_drawerToggle.DrawerClosed += delegate {
					ActionBar.Title = _title;
					InvalidateOptionsMenu ();
				};

				_drawerToggle.DrawerOpened += delegate {
					ActionBar.Title = _drawerTitle;
					InvalidateOptionsMenu ();
					LoadUserDatos ();
				};

				_drawer.SetDrawerListener (_drawerToggle);

				if (bundle == null) {
					SelectItem (1);
				}

				LoadUserDatos ();

			} catch (Exception) {
				return;
			}


		

		}


		private void LoadUserDatos ()
		{


			var User = _DatosUser.GetDatosVendors ();



			if (User != null) {
				IsLoginVendors = true;
				TxtEntrar.Text = User.Name;
				TxtContact.Text = User.Contact;
				if (!string.IsNullOrEmpty (User.UrlImg)) {
					using (var img = new DownloadAsync ()) {
						img.DownloadHistory (ImgLogo, User.UrlImg);
					}
				}
				LoadMenu (1);

			} else {
				IsLoginVendors = false;

				var Clients = _DatosUser.GetDatosClients ();

				if (Clients != null) {
					IsLoginClients = true;
					TxtEntrar.Text = Clients.Name;
					TxtContact.Text = Clients.Email;

					if (!string.IsNullOrEmpty (Clients.UrlImg)) {
						using (var img = new DownloadAsync ()) {
							img.DownloadHistory (ImgLogo, Clients.UrlImg);
						}
					}

					LoadMenu (0);
				}
			}

		}




		private void LoadMenu (int typeUser)
		{
			_planetTitles.Clear ();

			_planetTitles.Add (new ItamDrawer { 
				ImgMenu = "https://dl.dropboxusercontent.com/u/71886694/Vonline/Media-Play-02-128.png",
				Name = "Inico PorAkà"

			});

			switch (typeUser) {

			case 0:

				_planetTitles.Add (new ItamDrawer { 
					ImgMenu = "https://dl.dropboxusercontent.com/u/71886694/Vonline/Media-Play-02-128.png",
					Name = "Mis Compras"

				});
				IsLoginClients = true;

				break;
			case 1:
				_planetTitles.Add (new ItamDrawer { 
					ImgMenu = "https://dl.dropboxusercontent.com/u/71886694/Vonline/Media-Play-02-128.png",
					Name = "Vender"

				});

				break;


			default:

				_planetTitles.Add (new ItamDrawer { 
					ImgMenu = "https://dl.dropboxusercontent.com/u/71886694/Vonline/Media-Play-02-128.png",
					Name = "Vender"

				});

				break;
			}



			_planetTitles.Add (new ItamDrawer { 
				ImgMenu = "https://dl.dropboxusercontent.com/u/71886694/Vonline/Media-Play-02-128.png",
				Name = "¿Como funciona?"

			});

			_planetTitles.Add (new ItamDrawer { 
				ImgMenu = "https://dl.dropboxusercontent.com/u/71886694/Vonline/Share.png",
				Name = "About"
			});

			_planetTitles.Add (new ItamDrawer { 
				ImgMenu = "https://dl.dropboxusercontent.com/u/71886694/Vonline/Cerrar.png",
				Name = "Carrar Sesion"
			});

			_drawerList.Adapter = new ItamDrawerAdapter (this, _planetTitles);
		
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			//	RegisterReceiver (_ServerBroadcast, new IntentFilter (StreamingService.ActionCompletion));

		}

		protected override void OnStop ()
		{
			base.OnStop ();

//			if (_ServerBroadcast != null)
//				UnregisterReceiver (_ServerBroadcast);
		}


		protected override void OnDestroy ()
		{
			base.OnDestroy ();

//			_ServerBroadcast.OnCompletionTrack -= (sender, e) => {};

	
		}


		private void SelectItem (int position)
		{

			//			var arguments = new Bundle();
			//			arguments.PutInt(PlanetFragment.ArgPlanetNumber, position);
			//			fragment.Arguments = arguments;

			switch (position) {
			case 0:


				if (IsLoginVendors) {
					var User = _DatosUser.GetDatosVendors ();

					fragment = RegisterCMFragment.NewInstance (User.Email, true);
				
				} else {
				
					fragment = LoginUserFragment.NewInstance (GoFragments.InitFragment);
				}

			
				break;
			case 1:

				var Userv = _DatosUser.GetDatosVendors ();

				if (Userv!=null) {

					fragment = OfertasFragment.NewInstance ();

					break;
				} else {

					fragment = InitFragment.NewInstance ("");
				}

				break;
			case 2:

				if (IsLoginClients) {
				
					fragment = MyShopFragment.NewInstance ();

					break;
				}


				if (IsLoginVendors) {
					
					fragment = OfertasFragment.NewInstance ();

					break;
				} else {

					fragment = LoginCMFragment.NewInstance ();
				}

		
				break;
			default:

				_DatosUser.SetDatosVendors (null);
				StopService (new Intent (this, typeof(NotificationServices)));
				Finish ();

				break;
			}


			try {
				
				this.SupportFragmentManager.BeginTransaction ()
					.Replace (Resource.Id.content_frame, fragment, "InitFragment")
					.Commit ();

				_drawerList.SetItemChecked (position, true);

				if (position != 0) {

					ActionBar.Title = _title = _planetTitles [position - 1].Name;
				} else {

					ActionBar.Title = _title = "Usuario";
				}



				_drawer.CloseDrawer (_drawerList);



			} catch (Exception) {
				return;
			}






		}



		public void SendAudioCommand (string action)
		{
			var intent = new Intent (action);
			this.StartService (intent);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			_drawerToggle.SyncState ();
		}

		public override void OnConfigurationChanged (Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			_drawerToggle.OnConfigurationChanged (newConfig);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.main, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnPrepareOptionsMenu (IMenu menu)
		{
			var drawerOpen = _drawer.IsDrawerOpen (_drawerList);
			menu.FindItem (Resource.Id.action_websearch).SetVisible (!drawerOpen);
			return base.OnPrepareOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (_drawerToggle.OnOptionsItemSelected (item))
				return true;

			switch (item.ItemId) {
			case Resource.Id.action_websearch:
				{
		
					return true;

				}
			
			default:
				return base.OnOptionsItemSelected (item);
			}
		}

	

		public override bool OnKeyDown (Keycode keyCode, KeyEvent e)
		{

			if (keyCode == Keycode.Back) {
	
				this.SupportFragmentManager.PopBackStack ();

				return true;
			}

			return base.OnKeyDown (keyCode, e);
		}
	}





}

