using Telegram.Bot;
//using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Update = Telegram.Bot.Types.Update;
using Telegram.Bot.Types.Enums;
using RServiceRecordBot;
using Telegram.Bot.Polling;
using RService.Models;
using System.ComponentModel.DataAnnotations;
using RService.Data;
using Microsoft.AspNetCore.Builder;
using RService.Repositories.Interfaces;
using RService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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


                #region


                //if (message.Text.ToLower() == "/reg")
                //{
                //    await DataBaseMethods.ToggleInDialogStatus(update.Message.Chat.Id, 0);
                //    await botClient.SendTextMessageAsync(
                //        message.Chat.Id,
                //        "загрузка...",
                //        replyMarkup: new ReplyKeyboardRemove());

                //    object userData = null;//await DataBaseMethods.GetUserRole(message.From.Id);
                //    if (userData != null)
                //    {
                //        await botClient.SendTextMessageAsync(
                //            message.Chat.Id,
                //            text: "Вы уже зарегистрировались",
                //            replyMarkup: kb.ToMenu);
                //    }
                //    else
                //    {

                //        await botClient.SendTextMessageAsync(
                //            message.Chat.Id,
                //            text: "Выберите свою должность",
                //            replyMarkup: kb.Role);
                //    }
                //    return;
                //}


                //// это условие для того, чтобы команды не попадали туда куда не надо
                //if (!message.Text.StartsWith("/"))
                //{
                //    int dialogStatus = await DataBaseMethods.GetStatus(message.Chat.Id);
                //    var userDataReg = await DataBaseMethods.GetUserRole(message.Chat.Id);
                //    string role = "Роль";//userDataReg.Role;
                //    int countReg = 1;//userDataReg.StageReg;
                //    // так выглядит регистрациЯ ДИСПЕТЧЕРА И ВОДИТЕЛЯ, собственно счетчики нужны для корректного отслеживания сообщений от пользователя                    
                //    if (countReg == 1 && role == "driver" && dialogStatus == 0)
                //    {
                //        await DataBaseMethods.AddDataDriverName(message.From.Id, message.Text);
                //    }
                //    if (countReg == 1 && role == "controller" && dialogStatus == 0)
                //    {
                //        await DataBaseMethods.AddDataTcName(message.From.Id, message.Text);
                //        await botClient.SendTextMessageAsync(
                //            message.Chat.Id,
                //            text: "Хотите вернуться в меню?",
                //            replyMarkup: kb.ToMenu);
                //    }
                //    if (countReg == 2 && dialogStatus == 0)
                //    {
                //        await DataBaseMethods.AddDataDriverIdRoute(message.From.Id, message.Text);
                //    }
                //    if (countReg == 3 && dialogStatus == 0)
                //    {
                //        await DataBaseMethods.AddDataDriverVehichleRegNum(message.From.Id, message.Text);
                //    }
                //    if (countReg == 4 && dialogStatus == 0)
                //    {
                //        await DataBaseMethods.AddDataDriverDeviceSerialNum(message.From.Id, message.Text);
                //        await botClient.SendTextMessageAsync(
                //            message.Chat.Id,
                //            text: "Хотите вернуться в меню?",
                //            replyMarkup: kb.ToMenu);
                //    }
                //    // а вот пригодились статусы, чтобы оставаться в неком диалоге, внутри бота,
                //    // пока юзер не вернется в меню 
                //    if (role == "driver" && dialogStatus == 1)
                //    {
                //        var getDialog = await DataBaseMethods.GetThreadByDriver(
                //            message.Chat.Id);
                //        var msgID = await DataBaseMethods.MsgCreateByDriverToTc(
                //            getDialog,
                //            message.Text,
                //            Time,
                //            "3");
                //        var reciever = await DataBaseMethods.MsgRecievierTc(msgID, getDialog, message.Chat.Id);
                //        var msgFrom = await DataBaseMethods.GetDriverData(message.Chat.Id);
                //        await botClient.SendTextMessageAsync(
                //            reciever,
                //            text: $"{"Имя"}:" + "\n" +
                //            $"Маршрут: {1}:" + "\n" +
                //            $"{message.Text}");
                //    }
                //    if (role == "controller" && dialogStatus == 1)
                //    {
                //        var getAddress = await DataBaseMethods.GetAddress(message.Chat.Id);
                //        var getDialog = await DataBaseMethods.GetThreadByTc(
                //            message.Chat.Id,
                //            IdDriver: getAddress);
                //        var msgID = await DataBaseMethods.MsgCreateByTcToDriver(
                //            getDialog,
                //            message.Text,
                //            Time,
                //            "3");
                //        var reciever = await DataBaseMethods.MsgRecievierDriver(msgID, getDialog, message.Chat.Id);
                //        var msgFrom = await DataBaseMethods.GetTcData(message.Chat.Id);
                //        await botClient.SendTextMessageAsync(
                //            reciever,
                //            text: $"{"Имя"}:" + "\n" +
                //            $"{message.Text}");
                //    }
                //    // это отдельная фича под рассылку всем В от Д в боте
                //    if (role == "controller" && dialogStatus == 2)
                //    {
                //        var getAddress = DataBaseMethods.GetAllDriversId("driver");
                //        foreach (var address in getAddress)
                //        {
                //            var getDialog = await DataBaseMethods.GetThreadByTc(
                //                message.Chat.Id,
                //                IdDriver: address);
                //            var msgID = await DataBaseMethods.MsgCreateByTcToDriver(
                //                getDialog,
                //                message.Text,
                //                Time,
                //                "3");
                //            var reciever = await DataBaseMethods.MsgRecievierDriver(msgID, getDialog, message.Chat.Id);
                //            var msgFrom = await DataBaseMethods.GetTcData(message.Chat.Id);
                //            await botClient.SendTextMessageAsync(
                //                reciever,
                //                text: $"{"Имя"}:" + "\n" +
                //                $"{message.Text}");
                //        }
                //    }
                //    else
                //        return;
                //}

                #endregion
            
            if (update.Type == UpdateType.CallbackQuery)
            {
                // Тут идет обработка всех нажатий на кнопки, тут никаких особых доп условий не надо, тк у каждой кнопки своя ссылка
                var callbackQuery = update.CallbackQuery;

                if (callbackQuery.Data == "menu")
                {
                    //await DataBaseMethods.ToggleInDialogStatus(callbackQuery.Message.Chat.Id, 0);
                    await botClient.DeleteMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            callbackQuery.Message.MessageId);

                    await botClient.SendTextMessageAsync(
                        callbackQuery.Message.Chat.Id,
                        text: "Главное Меню",
                        replyMarkup: kb.Menu);
                }
                else if (callbackQuery.Data == "newrecord")
                {
                    await botClient.DeleteMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            callbackQuery.Message.MessageId);
                    HttpClient httpclient = new HttpClient();
                    HttpResponseMessage response = await httpclient.GetAsync("https://localhost:7110/api/OfficeTypes");

                    InlineKeyboardMarkup inlineKeyboardMarkup;
                    if (response.IsSuccessStatusCode)
                    {
                        var officeTypes = response.Content.ReadAsStringAsync().Result;
                    }

                    await botClient.SendTextMessageAsync(
                        callbackQuery.Message.Chat.Id,
                        text: "Выберите тип услуги",
                        replyMarkup: kb.Menu);
                }

                //<-----Гавно---->
                //var userRole = await DataBaseMethods.GetUserRole(callbackQuery.Message.Chat.Id);
                //long userTgId;
                //try
                //{
                //    userTgId = Convert.ToInt64(callbackQuery.Data);
                //}
                //catch
                //{
                //    userTgId = 0;
                //}
                //var checkUserCallback = await DataBaseMethods.GetUserRole(userTgId);
                //// тут единственнок место где условие чуть сложнее
                //// здесь по простому мы запоминаем ид пользвоателя в отд бд, откуда в дальнейем рлдгрузим данные
                //if (checkUserCallback != null)
                //{
                //    if (callbackQuery.Data == 1.ToString() != null && "contraler" == "controller")
                //    {
                //        await DataBaseMethods.DialogCreate(userTgId, callbackQuery.Message.Chat.Id);
                //        await DataBaseMethods.ToggleInDialogStatus(callbackQuery.Message.Chat.Id, 1, userTgId);
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Напишите сообщение водителю",
                //            replyMarkup: kb.ToMenu);
                //    }
                //    if (callbackQuery.Data == 1.ToString() && "driver" == "driver")
                //    {
                //        await DataBaseMethods.DialogCreate(userTgId, callbackQuery.Message.Chat.Id);
                //        await DataBaseMethods.ToggleInDialogStatus(callbackQuery.Message.Chat.Id, 1, userTgId);
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Напишите сообщение диспетчеру",
                //            replyMarkup: kb.ToMenu);
                //    }
                //}
                //if (callbackQuery.Data == "menu")
                //{
                //    await DataBaseMethods.ToggleInDialogStatus(callbackQuery.Message.Chat.Id, 0);
                //    await botClient.DeleteMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            callbackQuery.Message.MessageId);

                //    await botClient.SendTextMessageAsync(
                //        callbackQuery.Message.Chat.Id,
                //        text: "Главное Меню",
                //        replyMarkup: kb.Menu);
                //}
                //if (callbackQuery.Data == "register")
                //{
                //    await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            "/reg это способ пройти регистрацию");

                //}
                //if (callbackQuery.Data == "profile")
                //{
                //    await botClient.DeleteMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            callbackQuery.Message.MessageId);

                //    var driverData = await DataBaseMethods.GetDriverData(callbackQuery.Message.Chat.Id);
                //    var tcData = await DataBaseMethods.GetTcData(callbackQuery.Message.Chat.Id);
                //    if (userRole != null && driverData != null)
                //    {
                //        var name = "Имя";
                //        var route = "Маршрут";
                //        var vrn = "Номер";
                //        var dsn = "Номер машины";
                //        var role = "Роль";
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: $"ваша должность: {role}");
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: $"Ваше ФИО: {name}" + "\n" +
                //            $"Маршрут номер: {route}" + "\n" +
                //            $"Номер тс: {vrn}" + "\n" +
                //            $"Номер устройства: {dsn}");
                //    }
                //    if (userRole != null && tcData != null)
                //    {
                //        var name = "Имя";
                //        var role = "Роль";
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: $"ваша должность: {role}");
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: $"Ваше ФИО: {name}");
                //    }
                //    else
                //    {
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "для регистрации нажмите /reg",
                //            replyMarkup: kb.Register);
                //    }

                //    await botClient.SendTextMessageAsync(
                //        callbackQuery.Message.Chat.Id,
                //        text: "Хотите вернуться в меню?",
                //        replyMarkup: kb.ToMenu);
                //}
                //if (callbackQuery.Data == "dialogs")
                //{
                //    var role = "Роль";//userRole.Role;
                //    if (role == "controller")
                //    {
                //        await botClient.DeleteMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            callbackQuery.Message.MessageId);

                //        var driversList = DataBaseMethods.GetAllDriversId("driver");

                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            "Водители:",
                //            replyMarkup: kb.TextAll);

                //        foreach (var driver in driversList)
                //        {
                //            var driverName = await DataBaseMethods.GetDriverData(driver);
                //            InlineKeyboardMarkup driverButton = new(new[]
                //            {
                //                    new []
                //                    {
                //                        InlineKeyboardButton.WithCallbackData(text: $"{driver}", callbackData: $"{driver}"),
                //                    },
                //                });
                //            await botClient.SendTextMessageAsync(
                //                callbackQuery.Message.Chat.Id,
                //                $"<code>{"123"}</code> ",
                //                1,
                //                replyMarkup: driverButton);
                //        }
                //    }
                //    else
                //    {   
                //        await botClient.DeleteMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            callbackQuery.Message.MessageId);

                //        var tcList = DataBaseMethods.GetAllDriversId("controller");

                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            "Диспетчеры:");

                //        foreach (var tc in tcList)
                //        {
                //            var tcName = await DataBaseMethods.GetTcData(tc);
                //            InlineKeyboardMarkup tcButton = new(new[]
                //            {
                //                    new []
                //                    {
                //                        InlineKeyboardButton.WithCallbackData(text: $"{tc}", callbackData: $"{tc}"),
                //                    },
                //                });
                //            await botClient.SendTextMessageAsync(
                //                callbackQuery.Message.Chat.Id,
                //                $"<code>{"Имя"}</code> ",
                //                1,
                //                replyMarkup: tcButton);
                //        }
                //    }
                //    }
                //    if (callbackQuery.Data == "textall")
                //    {
                //        var allDrivers = DataBaseMethods.GetAllDriversId("driver");
                //        foreach (long driver in allDrivers)
                //        {
                //            await DataBaseMethods.DialogCreate(callbackQuery.Message.Chat.Id, driver);
                //            await DataBaseMethods.ToggleInDialogStatus(callbackQuery.Message.Chat.Id, 2, driver);
                //        }
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Напишите сообщение для всех водителей",
                //            replyMarkup: kb.ToMenu);
                //    }
                //    if (callbackQuery.Data == "driver")
                //    {
                //        await botClient.DeleteMessageAsync(
                //                callbackQuery.Message.Chat.Id,
                //                callbackQuery.Message.MessageId);

                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Нажмите на кнопку для ввода данных",
                //            replyMarkup: kb.StartRegDriver);

                //        int StageRegDriver = 1;
                //        await DataBaseMethods.AddOrUpdateUser(
                //            callbackQuery.Message.Chat.Id,
                //            callbackQuery.Data.ToString(),
                //            callbackQuery.From.Username,
                //            callbackQuery.Message.From.Id,
                //            StageRegDriver);

                //        await DataBaseMethods.AddDriver(
                //            callbackQuery.Message.Chat.Id);
                //    } // начало регистрации Водителя
                //    if (callbackQuery.Data == "DriverName")
                //    {
                //        await DataBaseMethods.StageIncrement(callbackQuery.Message.Chat.Id, 1);
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Введите ФИО:");
                //    }
                //    if (callbackQuery.Data == "IdRoute")
                //    {
                //        await DataBaseMethods.StageIncrement(callbackQuery.Message.Chat.Id, 2);
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Введите номер маршрута:");
                //    }
                //    if (callbackQuery.Data == "VehichleRegNum")
                //    {
                //        await DataBaseMethods.StageIncrement(callbackQuery.Message.Chat.Id, 3);
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Введите номер тс:");
                //    }
                //    if (callbackQuery.Data == "DeviceSerialNum")
                //    {
                //        await DataBaseMethods.StageIncrement(callbackQuery.Message.Chat.Id, 4);
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Введите номер устройства:");
                //    }
                //    if (callbackQuery.Data == "controller")
                //    {
                //        await botClient.DeleteMessageAsync(
                //                callbackQuery.Message.Chat.Id,
                //                callbackQuery.Message.MessageId);

                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Нажмите на кнопку для ввода данных",
                //            replyMarkup: kb.StartRegTC);

                //        int StageRegTC = 1;
                //        await DataBaseMethods.AddOrUpdateUser(
                //            callbackQuery.Message.Chat.Id,
                //            callbackQuery.Data.ToString(),
                //            callbackQuery.From.Username,
                //            callbackQuery.Message.From.Id,
                //            StageRegTC);

                //        await DataBaseMethods.AddTc(
                //            callbackQuery.Message.Chat.Id);
                //    } // начало регистрации Диспетчера
                //    if (callbackQuery.Data == "TcName")
                //    {
                //        await DataBaseMethods.StageIncrement(callbackQuery.Message.Chat.Id, 1);
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Введите ФИО:");
                //    }
                //    if (callbackQuery.Data == "FinReg")
                //    {
                //        await DataBaseMethods.StageIncrement(callbackQuery.Message.Chat.Id, 5);
                //        await botClient.SendTextMessageAsync(
                //            callbackQuery.Message.Chat.Id,
                //            text: "Регистрация окончена",
                //            replyMarkup: kb.ToMenu);
                //    } // общее окончание Регистрации
                //}
            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        static void Main(string[] args)
        {
                //var builder = WebApplication.CreateBuilder(args);

                //builder.Services.AddDbContext<RServiceContext>(options =>

                //    options.UseSqlServer(builder.Configuration.GetConnectionString("RServiceContext") ?? throw new InvalidOperationException("Connection string 'RServiceContext' not found.")));
                //builder.Services.AddScoped<IClientRepository, ClientRepository>();
                // Add services to the container.

                //builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                //builder.Services.AddEndpointsApiExplorer();
                //builder.Services.AddSwaggerGen();


                //var app = builder.Build();

                //// Configure the HTTP request pipeline.
                //if (app.Environment.IsDevelopment())
                //{
                //    app.UseSwagger();
                //    app.UseSwaggerUI();
                //}

                //app.UseHttpsRedirection();

                //app.UseAuthorization();

                //app.MapControllers();

                //app.Run();

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
    



//using Microsoft.EntityFrameworkCore;
//using RService.Data;
//using RService.Models;
//using Telegram.Bot;
//using Telegram.Bot.Exceptions;
//using Telegram.Bot.Polling;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;


//HttpClient client = new HttpClient();
//var botClient = new TelegramBotClient("5931881915:AAETNUpUqm1cxa4sSePqrm_Auz6wMbxVsX4");

//using CancellationTokenSource cts = new();

//// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
//ReceiverOptions receiverOptions = new()
//{
//    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
//};

//botClient.StartReceiving(
//    updateHandler: HandleUpdateAsync,
//    pollingErrorHandler: HandlePollingErrorAsync,
//    receiverOptions: receiverOptions,
//    cancellationToken: cts.Token
//);

//var me = await botClient.GetMeAsync();

//Console.WriteLine($"Start listening for @{me.Username}");
//Console.ReadLine();

//// Send cancellation request to stop bot
//cts.Cancel();

//async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
//{
//    // Only process Message updates: https://core.telegram.org/bots/api#message
//    if (update.Message is not { } message)
//        return;
//    // Only process text messages
//    if (message.Text is not { } messageText)
//        return;

//    var chatId = message.Chat.Id;

//    Message sentMessage;

//    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
//    if(messageText.ToLower().Contains("клиент"))
//    {
//        HttpResponseMessage response = await client.GetAsync("https://localhost:7110/api/Clients");
//        if (response.IsSuccessStatusCode)
//        {
//            sentMessage = await botClient.SendTextMessageAsync(
//            chatId: chatId,
//            text: "Клиенты:\n" + response.Content.ReadAsStringAsync().Result,
//            cancellationToken: cancellationToken);
//        }  
//    }
//    else
//    {
//        sentMessage = await botClient.SendTextMessageAsync(
//        chatId: chatId,
//        text: "Запрос не понятен",
//        cancellationToken: cancellationToken);
//    }

//    // Echo received message text
//    //Message sentMessage = await botClient.SendTextMessageAsync(
//    //    chatId: chatId,
//    //    text: "You said:\n" + messageText,
//    //    cancellationToken: cancellationToken);
//}

//Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
//{
//    var ErrorMessage = exception switch
//    {
//        ApiRequestException apiRequestException
//            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
//        _ => exception.ToString()
//    };

//    Console.WriteLine(ErrorMessage);
//    return Task.CompletedTask;
//}