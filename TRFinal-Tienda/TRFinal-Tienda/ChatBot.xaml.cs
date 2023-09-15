using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRFinal_Tienda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatBot : ContentPage
	{
		public ChatBot ()
		{
			InitializeComponent ();
			web_chat.Source = "https://webchat.botframework.com/embed/bot-trabajofinal-bot?s=8LNV2OHaREU.evfazX18-L-fy9fygesjQ11VirKWRMiYUTO7yxd2iuo";
			BindingContext = this;

        }
	}
}