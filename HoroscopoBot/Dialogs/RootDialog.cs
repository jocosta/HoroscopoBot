using System;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HoroscopoBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            //// calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            try
            {
                string Url = $"https://estilo.uol.com.br/horoscopo/{activity.Text}/horoscopo-do-dia/";
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(Url);

                var script = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/script[1]");


                var txtScript = script.InnerHtml;

                var start = txtScript.IndexOf("\"description\"") + "description".Length + 2;
                var end = txtScript.IndexOf("\"author\"");

                var previsao = txtScript.Substring(start + 2, (end - start) - 4);


                var txt = HttpUtility.HtmlDecode(previsao).ToString();

                // return our reply to the user
                await context.PostAsync(txt);
            }
            catch (Exception ex)
            {
                await context.PostAsync(ex.Message);
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}