using JsonPosts.Objetos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace JsonPosts
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Post> datos2 = new List<Post>();
        Post seleciconado = new Post();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            datos2 = await PostsProxy.getPosts();
            listaPosts.ItemsSource = datos2;
        }

        private async void listaPosts_ItemClick(object sender, ItemClickEventArgs e)
        {
            Post post = sender as Post;
            List<Comment> comentarios = await PostsProxy.getComments(post.id);
            listaComentarios.ItemsSource = comentarios;
        }

        private async void listaPosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Comment> comentarios = await PostsProxy.getComments(seleciconado.id);
            listaComentarios.ItemsSource = comentarios;
        }
    }
}
