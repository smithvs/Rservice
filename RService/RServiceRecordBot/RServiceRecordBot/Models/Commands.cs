using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RServiceRecordBot.Models
{
    enum Command
    {
        SetName =0,
        SetTelephone = 1,

        SelectOfficeType = 2,
        SelectOffice = 3,
        SelectService = 4,
        SelectDate = 5,
        SelectTime = 6,
        MainMenu = 7,
        Records = 8,
        OneRecord = 9,
        CancelRecord = 10
    }
}
