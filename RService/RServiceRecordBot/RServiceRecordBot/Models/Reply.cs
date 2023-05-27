using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace RServiceRecordBot.Models
{
    internal class Reply
    {
        public string Text { get; set; }
        public IReplyMarkup? ReplyMarkup { get; set; }

        public Reply(string text, IReplyMarkup? replyMarkup = null) {
            Text = text;
            ReplyMarkup = replyMarkup;
        }
    }
}
