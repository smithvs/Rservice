using Microsoft.EntityFrameworkCore;
using Polly;
using RService.Data;
using RService.Models;
using RService.Repositories;
using RService.Repositories.Interfaces;
using RServiceRecordBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bots;
using Telegram.Bots.Http;
//using Telegram.Bots.Types;
//using Message = Telegram.Bot.Types.Message;
using Update = Telegram.Bot.Types.Update;

namespace RServiceRecordBot.Services
{
    internal class MessageService
    {
        private static Dictionary<long, List<MessageInfo>> _userState = new Dictionary<long, List<MessageInfo>>();
        private static Dictionary<long, Command> _userLastCommand = new Dictionary<long, Command>();

        public MessageService()
        {
            
        }


        public async Task<Reply> HandleAsync(Update update)
        {
            //using (RServiceContext _context = new RServiceContext())
            //{
            //    var c = _context.Client.ToList();
            //}

            
            if (update.Type == UpdateType.Message)
            {
                return await HandleMessageAsync(update);
            }
            if (update.Type == UpdateType.CallbackQuery)
            {
                return await HandleCallbackQuery(update);
            }
            return null;
        }
        
        public async Task<Reply> HandleMessageAsync(Update update) {
            string Time = DateTime.Now.ToShortTimeString();
            var message = update.Message;
            if (message.Text.ToLower() == "/start")
            {
                return await HandleStartCommand(message);
            }
            else
            {
                return await HandleText(message);
            }
            return null;
        }

        public async Task<Reply> HandleStartCommand(Message message)
        {
            var userGUID = message.From.Id;
            _userLastCommand[userGUID] = Command.SetName;
            using RServiceContext _context = new RServiceContext();
            var client = await _context.Client.FirstOrDefaultAsync(c => c.GUID == userGUID.ToString());
            if (client == null)
            {
                return new Reply("Пользователь не найден, введите ваше имя!");
            }
            else
            {

                return new Reply($"Приветствую, {client.Name}! Выбирите действие", KeyboardsService.MainMenu);
            }


        }
        public async Task<Reply> HandleText(Message message)
        {
            var userGUID = message.From.Id;
            if (!_userLastCommand.ContainsKey(userGUID))
            {
                return new Reply("Извините, мне непонятен ваш ответ, давайте начнём сначала");
            }
            var lastCommand = _userLastCommand[userGUID];
            switch (lastCommand)
            {
                case Command.SetName:
                    if (AddCommand(userGUID, Command.SetName, message.Text))
                    {
                        _userLastCommand[userGUID] = Command.SetTelephone;
                        return new Reply($"Имя получено, введите ваш телефон");
                    }
                    else
                    {
                        return new Reply($"При записи имени произошла ошибка придётся начать сначала");
                    }
                    break;
                case Command.SetTelephone:
                    try
                    {
                        AddCommand(userGUID, Command.SetTelephone, message.Text);
                        using (RServiceContext _context = new RServiceContext())
                        {
                            var name = GetResultCommand(userGUID, Command.SetName);
                            var client = new Client()
                            {
                                GUID = userGUID.ToString(),
                                Name = name,
                                Telephone = message.Text
                            };
                            await _context.Client.AddAsync(client);
                            await _context.SaveChangesAsync();
                        }
                        return new Reply($"Приветствую, {message.Text}! Выбирите действие", KeyboardsService.MainMenu);
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"ERROR: {ex.Message}");
                        return new Reply("При записи пользователя произошла ошибка. Придётся всё начать с начала.");
                    }
                    break;
                default:
                    return null;
                    break;
            }            
        }

        private async Task<Reply> HandleCallbackQuery(Update update)
        {
            var userGUID = update.CallbackQuery.From.Id;
            var data = update.CallbackQuery.Data;
            try
            {
                if (data.Contains("new_record"))
                {
                    _userLastCommand[userGUID] = Command.SelectOfficeType;
                    List<MenuElement> officeTypes = await GetOfficeTypesAsync();

                    return new Reply("Выберите тип организации", KeyboardsService.GetCollection(officeTypes, "office_types"));
                }
                //if (data.Contains("my_records"))
                //{
                //    _userLastCommand[userGUID] = Command.Records;

                //    List<MenuElement> records = await GetRecordsAsync(userGUID);

                //    return new Reply("Выберите тип организации", KeyboardsService.GetCollection(officeTypes, "office_types"));
                //}
                if (data.Contains("office_types"))
                {
                    string[] info = data.Split('=');
                    AddCommand(userGUID, Command.SelectOfficeType, info[1]);
                    _userLastCommand[userGUID] = Command.SelectOffice;
                    List<MenuElement> offices = await GetOfficesAsync(int.Parse(info[1]));
                    return new Reply("Выберите организацию", KeyboardsService.GetCollection(offices, "offices"));
                }
                if (data.Contains("offices"))
                {
                    string[] info = data.Split('=');
                    AddCommand(userGUID, Command.SelectOffice, info[1]);
                    _userLastCommand[userGUID] = Command.SelectService;
                    List<MenuElement> services = await GetServicesAsync(int.Parse(info[1]));
                    return new Reply("Выберите услугу", KeyboardsService.GetCollection(services, "services"));
                }
                if (data.Contains("services"))
                {
                    string[] info = data.Split('=');
                    AddCommand(userGUID, Command.SelectService, info[1]);
                    _userLastCommand[userGUID] = Command.SelectDate;
                    List<MenuElement> dates = await GetDatesAsync(int.Parse(GetResultCommand(userGUID, Command.SelectOffice)), int.Parse(info[1]));
                    return new Reply("Выберите дату", KeyboardsService.GetCollectionDates(dates, "dates"));
                }
                if (data.Contains("dates"))
                {
                    string[] info = data.Split('=');
                    AddCommand(userGUID, Command.SelectDate, info[1]);
                    _userLastCommand[userGUID] = Command.SelectTime;
                    List<MenuElement> times = await GetTimesAsync(int.Parse(GetResultCommand(userGUID, Command.SelectOffice)), int.Parse(GetResultCommand(userGUID, Command.SelectService)), info[1]);
                    return new Reply("Выберите дату", KeyboardsService.GetCollection(times, "times"));
                }
                if (data.Contains("times"))
                {
                    string[] info = data.Split('=');
                    AddCommand(userGUID, Command.SelectTime, info[1]);
                    _userLastCommand[userGUID] = Command.MainMenu;
                    RServiceContext _context = new RServiceContext();
                    var client = await _context.Client.FirstOrDefaultAsync(c => c.GUID == userGUID.ToString());
                    var record = await _context.Record.FirstOrDefaultAsync(r => r.Id == int.Parse(info[1]));
                    if (record != null && client != null) 
                    {
                        record.ClientId = client.Id;                        
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return new Reply("Во время выполнения произошла ошибка. Начните сначала, введя команду /start");
                    }
                    return new Reply("Запись успешно добавлена", KeyboardsService.MainMenu);
                }
                else
                {
                    return new Reply("Команда пока не реализована");
                }
            }
            catch (Exception ex)
            {
                return new Reply("Во время выполнения произошла ошибка. Начните сначала, введя команду /start");
            }
        }

        //private async Task<List<MenuElement>> GetRecordsAsync(long userGUID)
        //{
        //    using RServiceContext _context = new RServiceContext();
        //    var client = await _context.Client.FirstOrDefaultAsync(c => c.GUID == userGUID.ToString());
        //    if (client != null)
        //    {
        //        var records = await _context.Record.Where(r => r.ClientId == client.Id).ToListAsync();
        //        var services = await _context.Service.
        //    }    
            

        //}

        private async Task<List<MenuElement>> GetOfficeTypesAsync()
        {
            using RServiceContext _context = new RServiceContext();
            return await _context.OfficeType.Select(ot=>new MenuElement() { Id=ot.Id, Text = ot.Name}).ToListAsync();
        }

        private async Task<List<MenuElement>> GetOfficesAsync(int officeTypeId)
        {
            using RServiceContext _context = new RServiceContext();
            return await _context.Office.Where(o=>o.OfficeTypeId==officeTypeId && o.IsActive).Select(ot => new MenuElement() { Id = ot.Id, Text = ot.Name }).ToListAsync();
        }

        private async Task<List<MenuElement>> GetServicesAsync(int officeId)
        {
            using RServiceContext _context = new RServiceContext();
            var serviceIds = await _context.ServiceOffice.Where(s => s.OfficeId == officeId).Select(s => s.ServiceId).ToListAsync();
            return await _context.Service.Where(s => serviceIds.Contains(s.Id)).Select(ot => new MenuElement() { Id = ot.Id, Text = ot.Name }).ToListAsync();
        }

        private async Task<List<MenuElement>> GetDatesAsync(int officeId, int serviceId)
        {
            using RServiceContext _context = new RServiceContext();
            return await _context.Record.Where(r => r.OfficeId == officeId && r.ServiceId == serviceId && r.ClientId==null)
                .Select(s => s.Date.ToString("dd.MM.yyyy")).Distinct()
                .Select(ot => new MenuElement() { Id = -1, Text = ot }).ToListAsync();
        }

        private async Task<List<MenuElement>> GetTimesAsync(int officeId, int serviceId, string date)
        {
            using RServiceContext _context = new RServiceContext();
            var d = DateTime.Parse(date);
            var times = await _context.Record.Where(r => r.OfficeId == officeId && r.ServiceId == serviceId && r.Date == d && r.ClientId == null).ToListAsync();
            return times.Select(r => new MenuElement() { Id = r.Id, Text = $"{r.TimeStart:t} - {r.TimeEnd:t}" }).ToList();
        }

        private string GetResultCommand(long userGUID, Command command)
        {
            if (_userState.ContainsKey(userGUID))
            {
                return _userState[userGUID].FirstOrDefault(x => x.Key == command)?.Value ?? "";                
            }
            return "";
        }

        private bool AddCommand(long guid, Command command, string messageText)
        {
            try
            {
                if (!_userState.ContainsKey(guid))
                {
                    _userState[guid] = new List<MessageInfo>();
                }
                var oldCommand = _userState[guid].Find(x => x.Key == command);
                if (oldCommand != null)
                {
                    _userState[guid].Remove(oldCommand);
                }
                _userState[guid].Add(new MessageInfo() { Key = command, Value = messageText, Time = DateTime.Now });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
