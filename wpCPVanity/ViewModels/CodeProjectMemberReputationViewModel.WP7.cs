using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using wpCPVanity;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
    public partial class CodeProjectMemberReputationViewModel : CodeprojectBaseViewModel
    {

        public void Load()
        {
            Name = "Reputation";
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetField(ref isLoading, value, "IsLoading"); }
        }

        private BitmapImage memberReputationGraphImage;
        public BitmapImage MemberReputationGraphImage
        {
            get { return memberReputationGraphImage; }
            set { SetField(ref memberReputationGraphImage, value, "MemberReputationGraphImage"); }
        }

        public DataTemplate ItemDataTemplate
        {
            get
            {
                return App.Current.Resources["MemberReputationTemplate"] as DataTemplate;
            }
        }

        public void OnReputationGraphLoaded(byte[] graph)
        {
            IsLoading = false;

            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream ms = new MemoryStream(graph);
            bitmapImage.SetSource(ms);

            MemberReputationGraphImage = bitmapImage;
        }

        public override void OnLoad()
        {
            LoadMemberReputation(TaskScheduler.FromCurrentSynchronizationContext());
            IsLoading = true;
        }
    }
}
