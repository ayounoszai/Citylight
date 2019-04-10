using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citylight
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavPage : TabbedPage
    {
        public NavPage()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            //DiningPage.
        }
    }
}
