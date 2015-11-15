
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PorAka;
using Android.Support.V4.View;
using Android.Views.Animations;
using Android.Support.V4.App;

namespace PorAka
{
	public class OfertasFragment : BaseFragment
	{

		private SlidingTabScrollView mSlidingTabScrollView;
		private ViewPager mViewPager;

		public static OfertasFragment NewInstance ()
		{
			var fragment = new OfertasFragment ();
			//			var args = new Bundle ();
			//			args.GetBoolean ("TYPEUSER", typeUser);
			//			fragment.Arguments = args;
			return fragment;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			return inflater.Inflate(Resource.Layout.OfertasFragment, container, false);

			//return base.OnCreateView (inflater, container, savedInstanceState);
		}


		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
			mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
			mViewPager.Adapter = new SamplePagerAdapter(this.Activity);

			mSlidingTabScrollView.ViewPager = mViewPager;
		}

		public class SamplePagerAdapter : PagerAdapter
		{
			List<string> items = new List<string>();
			FragmentActivity Activity;
			public SamplePagerAdapter(FragmentActivity activity) : base()
			{
				items.Add("Ofertas publicadas(10)");
				items.Add("Mis Ofertas(05)");
				items.Add("Mis Ventas Hoy(04)");

				this.Activity=activity;
			
			}

			public override int Count
			{
				get { return items.Count; }
			}

			public override bool IsViewFromObject(View view, Java.Lang.Object obj)
			{
				return view == obj;
			}

			public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
			{

				View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
				container.AddView(view);

				var LVOfertas= view.FindViewById<ListView>(Resource.Id.LVOfertas);

				switch (position) {

				case 0:
					
					LVOfertas.Adapter = new ListQuotationAdapter (this.Activity, new List<Quotation>(){
						new Quotation{
							Id="",
							Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7\" - 32GB - Negro",
							UserName="Jhon Harol Diaz",
							Count=10,
							Details="",
							UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",
						   
						},
						new Quotation{
							Id="",
							Name="Celular Samsung Galaxy S6 Edge Plus - Pantalla 5.7\" - 32GB - Negro",
							UserName="Jhon Harol Diaz",
							Count=15,
							Details="",
							UrlImg="http://www.wbuscas.com/wp-content/uploads/2014/10/smartphone-motorola-moto-g-4.jpg",

						}
					});

					break;



				default:
					break;
				}


				return view;
			}

			public string GetHeaderTitle (int position)
			{
				return items[position];
			}

			public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
			{
				container.RemoveView((View)obj);
			}
		}

		public override Animation OnCreateAnimation (int transit, bool enter, int nextAnim)
		{
			return AnimationUtils.LoadAnimation (Activity,
				enter ? Android.Resource.Animation.SlideInLeft : Android.Resource.Animation.SlideOutRight);
		}
	}
}

