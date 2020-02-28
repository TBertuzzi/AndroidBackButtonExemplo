# AndroidBackButtonExemplo

Este exemplo ensina como exibir uma mensagem para o usuario antes de fechar seu app Android com Xamarin.Forms

<img src="https://github.com/TBertuzzi/AndroidBackButtonExemplo/blob/master/Resources/androidDialog.png?raw=true" alt="Android Dialog" height="500" width="300">

Avisar o usuario que o app esta na ultima tela e ira fechar pode ser algo agradavel na experiencia de uso. Mesmo que seu app Android seja feito com Xamarin.Forms é possivel manipular o botão "Voltar" do Android. Mesmo que você tenha uma ContentPage Simples, uma NavigationPage ou uma TabbedPage.

Em nosso App.xaml.cs vamos implementar o código abaixo :

```c#
   public bool ConfirmaFecharApp
        {
            get
            {
                bool ultimaView = false;
                switch (MainPage)
                {
                    // Unica Pagina
                    case ContentPage _:
                        ultimaView = true;
                        break;
                    // É NavigationPage ou TabbedPage
                    case NavigationPage mainPage when mainPage.CurrentPage is TabbedPage tabbedPage
                                                      && tabbedPage.CurrentPage is NavigationPage navigationPage:
                        ultimaView = navigationPage.Navigation.NavigationStack.Count <= 1;
                        break;
                    // A NavigationPage é a Pagina Principal
                    case NavigationPage mainPage:
                        ultimaView = mainPage.Navigation.NavigationStack.Count <= 1;
                        break;
                    // A TabbedPage é a Pagina Principal
                    case TabbedPage tabbedPage when tabbedPage.CurrentPage is NavigationPage navigationPage:
                        ultimaView = navigationPage.Navigation.NavigationStack.Count <= 1;
                        break;
                }
                return ultimaView;
            }
        }
```

Este sera o responsavel por verificar se a View na tela é a ultima/principal do seu app.

Em Seguida na classe MainActivity.cs do projeto Android vamos Implementar o seguinte codigo :

```c#
   public override void OnBackPressed()
        {
            if (((AndroidBackButtonExemplo.App)App.Current).ConfirmaFecharApp)
            {
                using (var alert = new AlertDialog.Builder(this))
                {
                    alert.SetTitle("Vou fechar em");
                    alert.SetMessage("Tem certeza que deseja sair do APP?");
                    alert.SetPositiveButton("Sim", (sender, args) => { FinishAffinity(); });
                    alert.SetNegativeButton("Não", (sender, args) => { });

                    var dialog = alert.Create();
                    dialog.Show();
                }
                return;
            }
            base.OnBackPressed();
        }
```

Pronto ! Quando o usuario estiver na ultima tela e pressionar o botão voltar o dialogo como na imagem acima sera apresentado.

É possivel tambem ao inves do dialogo fazer isso com Toast, para isso basta alterar o código da MainActivity.cs  :

```c#
   private bool _isBackPressed;
        public override void OnBackPressed()
        {
            if (((AndroidBackButtonExemplo.App)App.Current).ConfirmaFecharApp)
            {
                using (var alert = new AlertDialog.Builder(this))
                {
                    Android.Widget.Toast.MakeText(this, "Pressione mais uma vez para fechar", Android.Widget.ToastLength.Short).Show();

                    // O Alerta vai sumir em 2 segundos
                    new Handler().PostDelayed(() =>
                    {
                        _isBackPressed = false;
                    }, 2000);
                }
                return;
            }
            base.OnBackPressed();
        }
```

Então o toast abaixo sera exibido :

<img src="https://github.com/TBertuzzi/AndroidBackButtonExemplo/blob/master/Resources/androidToast.png?raw=true" alt="Android Dialog" height="500" width="300">

Caso fique a duvida este repositorio tem um exemplo da implementação completa.

Quer ver outros artigos sobre Xamarin ? [Clique aqui](https://github.com/TBertuzzi/XXamarin)

Espero ter ajudado!

Aquele abraço!
