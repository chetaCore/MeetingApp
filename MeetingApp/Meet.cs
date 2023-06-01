namespace MeetingApp
{
    public class Meet
    {
        private string _name;
        private DateTime _startTime;
        private DateTime _endTime;
        private DateTime _notificationTime;
        private Timer _reminderTimer;

        private string _meetingContent;
        public bool IsCorrect;

        public DateTime EndTime { get => _endTime; set => _endTime = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value;}
        public string Name { get => _name; set => _name = value; }
        public DateTime NotificationTime { get => _notificationTime;}
        public string MeetingContent { get => _meetingContent; set => _meetingContent = value; }

        public void SetNotificationTime(DateTime notificationTime)
        {
            IsCorrect = false;

            _notificationTime = notificationTime;

            Notify();

            while (!IsCorrect)
            {
                Console.WriteLine("Вы указали время, которое уже прошло\nнажмите на любую кнопку, чтобы повторить");
                Console.ReadLine();
                Notify();
            }

        }


        public Meet(string name, DateTime startTime, DateTime endTime, DateTime notificationTime, string meetingContent = "")
        {
            _name = name;
            _startTime = startTime;
            _endTime = endTime;
            _notificationTime = notificationTime;
            _meetingContent = meetingContent;

            Notify();

            if (IsCorrect)
            {
                Console.Clear();
                Console.WriteLine($"Уведомление {Name} создано\n" +
                  $"Начало встречи в {startTime}\n" +
                  $"Конец встречи в {endTime}\n" +
                  $"Уведомление придет в {NotificationTime}\n");

                Console.WriteLine("Нажимте любую кнопку, чтобы продолжить");
                Console.ReadLine();
            }
        }

        private void Notify()
        {
            _reminderTimer?.Dispose();

            TimeSpan timeUntilMeeting = _notificationTime - DateTime.Now;

            try
            {
                TimerCallback timerCallback = new TimerCallback(ShowMeet);
                _reminderTimer = new(timerCallback, null, timeUntilMeeting, TimeSpan.Zero);
                IsCorrect = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Вы указали время, которое уже прошло\nнажмите на любую кнопку, чтобы продолжить");
                Console.ReadLine();
            }
        }

        private void ShowMeet(object state)
        {
            Console.WriteLine($"Встреча {Name} скоро состоится\n" +
                $"Начало встречи в {_startTime}\n" +
                $"Конец встречи в {_endTime}\n");

            if (_meetingContent != "")
                Console.WriteLine($"Коментарий к встрече:\n{_meetingContent}");
        }

        public void ShowMeetInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Встреча {Name}\nна время {StartTime}\nкоментарий: {_meetingContent}");

            Console.WriteLine($"Уведомление {_name} создано\n" +
                   $"Начало встречи в {_startTime}\n" +
                   $"Конец встречи в {_endTime}\n" +
                   $"Уведомление придет в {_notificationTime}\n" +
                   $"Коментарий к встрече:\n{_meetingContent}\n");

            Console.ResetColor();
        }
    }
}