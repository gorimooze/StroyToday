using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StroyToday.Application.Enums
{
    public enum DateTimeDisplayType
    {
        Friendly = 1, // Mon, Nov 7th
        MmDdYyyy = 2, // 11/7/2022
        MmDdYyyShort = 3, // Nov 7th
        FriendlyShort = 4, // Mon, Nov 7th, 7:11 PM
        MmDd = 5, // 11/07   
        MD = 6, //11/7/2022
        FriendlyShortNoTime = 7, // Mon, Nov 7th
        FriendlyWithDay = 8, // DATE AND TIMES -> Mon, 7/11 6pm-8pm || DATE -> Mon, 10/11  --> if year different result will be 10/11/28
        DdMm = 9, // 07/11
        DayDate = 10 //used for email, format: Monday, 09/26/2023
    }
}
