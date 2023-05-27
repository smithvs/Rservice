using RServiceRecordBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace RServiceRecordBot.Services
{
    internal static class KeyboardsService
    {
        public static InlineKeyboardMarkup MainMenu = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Текущие записи", callbackData: "my_records"),
                    InlineKeyboardButton.WithCallbackData(text: "Добавить запись", callbackData: "new_record"),
                },
            });

        public static InlineKeyboardMarkup GetCollection(List<MenuElement> elements, string name)
        {
            var byttons = elements.Select(e => new[] { InlineKeyboardButton.WithCallbackData(text: e.Text, callbackData: $"{name}={e.Id}") }).ToArray();
            return new(byttons);
        }
        public static InlineKeyboardMarkup GetCollectionDates(List<MenuElement> elements, string name)
        {
            var byttons = elements.Select(e => new[] { InlineKeyboardButton.WithCallbackData(text: e.Text, callbackData: $"{name}={e.Text}") }).ToArray();
            return new(byttons);
        }
    }
}
