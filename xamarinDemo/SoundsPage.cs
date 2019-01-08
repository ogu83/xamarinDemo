using System.Collections.ObjectModel;
using System.Reflection;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using xamarinDemo.ViewModels;

namespace xamarinDemo
{
    public class SoundsPage : ContentPage
    {
        ISimpleAudioPlayer player;

        public SoundsPage()
        {
            player =  Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

            var header = new Label
            {
                Text = "Sounds"
            };

            var items = new ObservableCollection<SoundItem>();
            items.Add(new SoundItem() { Name = "Imperial March", Path="imperial_march.mp3" });
            items.Add(new SoundItem() { Name = "Cantina Song", Path = "star-wars-cantina-song.mp3" });
            items.Add(new SoundItem() { Name = "Theme Song", Path = "star-wars-theme-song.mp3" });

            var picker = new Picker
            {
                ItemsSource = items,
                ItemDisplayBinding = new Binding("Name")
            };

            var button = new Button
            {
                Text = "Abspielen",
            };

            button.Clicked += (sender, e) => 
            {
                var item = picker.SelectedItem as SoundItem;
                if (item != null)
                {
                    if (player.IsPlaying)
                        player.Stop();
                    
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    var audioStream = ResourceLoader.GetEmbeddedResourceStream(assembly, item.Path);
                    player.Load(audioStream);
                    player.Play();
                }
            };

            Content = new StackLayout
            {
                Children = {
                    header,
                    picker,
                    button
                }
            };
        }
    }
}
