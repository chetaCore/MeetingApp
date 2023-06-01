using System.Collections.Generic;

namespace MeetingApp
{
    public class Constans
    {
        static public readonly List<string> AppList = new List<string>
        {
            "MeetingApp"
        };

        static public readonly List<string> MeetServiceActionList = new List<string>
        {
            "CreateMeet", "ShowAllMeets", "DeleteMeet", "EditMeet", "ChooseMeetingDay"
        };

        static public readonly List<string> MeetParams = new List<string>
        {
            "Название", "Время уведомления", "Время начала", "Время конца", "Коментарий"
        };

    }
}