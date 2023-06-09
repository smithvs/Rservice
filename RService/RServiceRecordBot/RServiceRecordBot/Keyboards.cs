﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace RServiceRecordBot
{
        public static class kb
        {
            public static ReplyKeyboardMarkup Register = new(new[]
            {
            new KeyboardButton[] { "/reg" },
            })
            { ResizeKeyboard = true };

            public static InlineKeyboardMarkup Role = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "Водитель", callbackData: "driver"),
                    InlineKeyboardButton.WithCallbackData(text: "Диспетчер", callbackData: "controller"),
                },
            });
           public static InlineKeyboardMarkup Menu = new(new[]
            {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Записаться...", callbackData: "newrecord"),
            }
        });
            public static InlineKeyboardMarkup ToMenu = new(new[]
            {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Меню", callbackData: "menu"),
            },
        });
            public static InlineKeyboardMarkup StartRegDriver = new(new[]
            {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Ваше ФИО", callbackData: "DriverName"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Идентификатор маршрута", callbackData: "IdRoute"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Регистрационный номер тс", callbackData: "VehichleRegNum"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Серийный номер устройства", callbackData: "DeviceSerialNum"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Окончить Регистрацию", callbackData: "FinReg"),
            },
        });
            public static InlineKeyboardMarkup StartRegTC = new(new[]
            {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Ваше ФИО", callbackData: "TcName"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Окончить Регистрацию", callbackData: "FinReg"),
            },
        });
            public static InlineKeyboardMarkup MsgToDriver = new(new[]
            {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Водитель", callbackData: "driver"),
                InlineKeyboardButton.WithCallbackData(text: "Диспетчер", callbackData: "controller"),
            },
        });
            public static InlineKeyboardMarkup MsgDispetcher = new(new[]
            {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Диспетчер", callbackData: "callTC"),
            },
        });
            public static InlineKeyboardMarkup TextAll = new(new[]
            {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Написать всем", callbackData: "textall"),
            },
        });
        }
    
}
