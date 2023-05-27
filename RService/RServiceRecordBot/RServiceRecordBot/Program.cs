using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Update = Telegram.Bot.Types.Update;
using Telegram.Bot.Types.Enums;
using RServiceRecordBot;
using Telegram.Bot.Polling;

using RServiceRecordBot.Services;

namespace CETmsgr
{
    class Program
    {
        
        static ITelegramBotClient bot = new TelegramBotClient("5931881915:AAETNUpUqm1cxa4sSePqrm_Auz6wMbxVsX4");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            var _messageService = new MessageService();
            var reply = await _messageService.HandleAsync(update);


            if (update.Type == UpdateType.Message)
            {
                string Time = DateTime.Now.ToShortTimeString();

                await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat,
                        text: reply.Text,
                        replyMarkup: reply.ReplyMarkup);

                return;
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                string Time = DateTime.Now.ToShortTimeString();

                await botClient.DeleteMessageAsync(
                            update.CallbackQuery.Message.Chat.Id,
                            update.CallbackQuery.Message.MessageId);

                await botClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        text: reply.Text,
                        replyMarkup: reply.ReplyMarkup);
            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();

        }
    }
}