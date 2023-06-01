namespace MeetingApp
{
    public class MeetService
    {
        public List<Meet> MeetList { get; set; }

        public MeetService()
        {
            MeetList = new();
            MeetMenu();
        }

        private void MeetMenu()
        {
            Console.Clear();

            Console.WriteLine("Выбирите действие");

            int number = 0;
            Constans.MeetServiceActionList.ForEach((_) =>
            {
                Console.WriteLine(number + ":" + _);
                number++;
            });

            int.TryParse(Console.ReadLine(), out int currentActionNumber);

            ActionSelecter(currentActionNumber);
        }

        private void ActionSelecter(int currentActionNumber)
        {
            switch (currentActionNumber)
            {
                case 0:
                    CreateNewMeet();
                    break;

                case 1:
                    ShowAllMeets();
                    break;

                case 2:
                    DeleteMeet();
                    break;

                case 3:
                    EditMeet();
                    break;

                case 4:
                    ChooseMeetingDay();
                    break;

                default:
                    Console.WriteLine("Номер недействительный, повторите ввод");
                    int.TryParse(Console.ReadLine(), out currentActionNumber);
                    ActionSelecter(currentActionNumber);
                    break;
            }
        }

        public void CreateNewMeet()
        {
            Console.Clear();

            Console.WriteLine("Укажите название события");
            var name = Console.ReadLine();

            bool isCorrectTime = false;
            while (!isCorrectTime)
            {
                try
                {
                    Console.WriteLine("Укажите время для уведомления в формате: \"dd/mm/yyyy hh:mm\"");
                    var inputTime = Console.ReadLine();
                    string format = "dd/MM/yyyy HH:mm";
                    var notificationTime = DateTime.ParseExact(inputTime, format, null);

                    Console.WriteLine("Укажите время начала встречи в формате: \"dd/mm/yyyy hh:mm\"");
                    inputTime = Console.ReadLine();
                    var startTime = DateTime.ParseExact(inputTime, format, null);

                    Console.WriteLine("Укажите время конца встречи в формате: \"dd/mm/yyyy hh:mm\"");
                    inputTime = Console.ReadLine();
                    var endTime = DateTime.ParseExact(inputTime, format, null);

                    if (endTime < startTime)
                    {
                        isCorrectTime = false;
                        Console.WriteLine("Дата конца раньше, чем дата начала, потворите попытку");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }

                    if (CheckMeetingIntersection(startTime, endTime))
                    {
                        isCorrectTime = false;
                        Console.WriteLine("Уведомления пересекаются, повторите попытку");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                    }

                    Console.WriteLine("Введите коментарий к событию");
                    var meetingContent = Console.ReadLine();

                    var newMeet = new Meet(name, startTime, endTime, notificationTime, meetingContent);

                    if (newMeet.IsCorrect)
                        MeetList.Add(newMeet);

                    isCorrectTime = true;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Некорректый ввод даты, нажмите любую кноку для повторной попытки");
                    Console.ReadLine();
                }
            }

            MeetMenu();
        }

        private void ShowAllMeets()
        {
            Console.Clear();

            if (MeetList.Count == 0)
            {
                Console.WriteLine("Нет существующих событий, нажмите любую кнопку, чтоб вернуться в главное меню");
                Console.ReadLine();
                MeetMenu();
                return;
            }

            int number = 0;
            MeetList.ForEach((_) =>
            {
                Console.WriteLine($"Заметка {number}:");
                _.ShowMeetInfo();
                number++;
            });

            Console.WriteLine("Нажимте любую кнопку, чтобы продолжить");
            Console.ReadLine();
            MeetMenu();
        }

        private bool CheckMeetingIntersection(DateTime curentTimeStart, DateTime curentTimeEnd)
        {
            foreach (var meeting in MeetList)
            {
                if (curentTimeStart < meeting.EndTime && curentTimeEnd > meeting.StartTime)
                    return true;
            }

            return false;
        }

        private void DeleteMeet()
        {
            Console.Clear();

            if (MeetList.Count == 0)
            {
                Console.WriteLine("Нет существующих событий, нажмите любую кнопку, чтоб вернуться в главное меню");
                Console.ReadLine();
                MeetMenu();
                return;
            }

            int number = 0;
            foreach (var meet in MeetList)
            {
                Console.WriteLine($"Встреча {number}");
                meet.ShowMeetInfo();
                number++;
            }
            Console.WriteLine("Введите номер встречи, котрую необходимо удалить");
            int deleteNumber;
            int.TryParse(Console.ReadLine(), out deleteNumber);

            MeetList.RemoveAt(deleteNumber);
            Console.Clear();
            Console.WriteLine($"Заметка {deleteNumber} удалена");

            Console.WriteLine("Нажимте любую кнопку, чтобы продолжить");
            Console.ReadLine();

            MeetMenu();
        }

        private void EditMeet()
        {
            Console.Clear();

            if (MeetList.Count == 0)
            {
                Console.WriteLine("Нет существующих событий, нажмите любую кнопку, чтоб вернуться в главное меню");
                Console.ReadLine();
                MeetMenu();
                return;
            }

            int number = 0;
            foreach (var meet in MeetList)
            {
                Console.WriteLine($"Встреча {number}");
                meet.ShowMeetInfo();
                number++;
            }

            int editMeet = 0;
            do
            {
                Console.WriteLine("Введите номер встречи, котрую необходимо изменить");
                int.TryParse(Console.ReadLine(), out editMeet);

                if (editMeet < 0 || editMeet > MeetList.Count - 1)
                {
                    Console.WriteLine("Некорректый ввод даты, нажмите любую кноку для повторной попытки");
                    Console.ReadLine();
                }
            }
            while (editMeet < 0 || editMeet > MeetList.Count - 1);

            Console.Clear();
            MeetList[editMeet].ShowMeetInfo();

            Console.WriteLine("Введите номер пункта, который необходимо изменить");
            number = 0;
            Constans.MeetParams.ForEach((_) =>
            {
                Console.WriteLine(number + ":" + _);
                number++;
            });

            int editNumber;
            int.TryParse(Console.ReadLine(), out editNumber);

            EditMeetSelecter(editMeet, editNumber);
        }

        private void EditMeetSelecter(int editMeet, int editNumber)
        {
            switch (editNumber)
            {
                case 0:
                    Console.WriteLine("Введите новое название");
                    MeetList[editMeet].Name = Console.ReadLine();
                    Console.WriteLine("Изменения сохранены, нажмите любую кнопку, чтобы продолжить");
                    Console.ReadLine();
                    MeetMenu();
                    break;

                case 1:
                    Console.WriteLine("Введите новое время уведомления");
                    var isCorrectTime = false;
                    while (!isCorrectTime)
                    {
                        try
                        {
                            Console.WriteLine("Укажите время для уведомления в формате: \"dd/mm/yyyy hh:mm\"");
                            var inputTime = Console.ReadLine();
                            string format = "dd/MM/yyyy HH:mm";
                            var notificationTime = DateTime.ParseExact(inputTime, format, null);
                            MeetList[editMeet].SetNotificationTime(notificationTime);
                            isCorrectTime = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректый ввод даты, нажмите любую кноку для повторной попытки");
                            Console.ReadLine();
                        }
                    }
                    Console.WriteLine("Изменения сохранены, нажмите любую кнопку, чтобы продолжить");
                    Console.ReadLine();
                    MeetMenu();
                    break;

                case 2:
                    Console.WriteLine("Введите новое время начала");
                    isCorrectTime = false;
                    while (!isCorrectTime)
                    {
                        try
                        {
                            Console.WriteLine("Укажите время начала встречи в формате: \"dd/mm/yyyy hh:mm\"");
                            var inputTime = Console.ReadLine();
                            string format = "dd/MM/yyyy HH:mm";
                            var startTime = DateTime.ParseExact(inputTime, format, null);
                            MeetList[editMeet].StartTime = startTime;
                            isCorrectTime = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректый ввод даты, нажмите любую кноку для повторной попытки");
                            Console.ReadLine();
                        }
                    }
                    Console.WriteLine("Изменения сохранены, нажмите любую кнопку, чтобы продолжить");
                    Console.ReadLine();
                    MeetMenu();
                    break;

                case 3:
                    Console.WriteLine("Введите новое время конца");
                    isCorrectTime = false;
                    while (!isCorrectTime)
                    {
                        try
                        {
                            Console.WriteLine("Укажите время конца встречи в формате: \"dd/mm/yyyy hh:mm\"");
                            var inputTime = Console.ReadLine();
                            string format = "dd/MM/yyyy HH:mm";
                            var endTime = DateTime.ParseExact(inputTime, format, null);
                            MeetList[editMeet].EndTime = endTime;
                            isCorrectTime = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Некорректый ввод даты, нажмите любую кноку для повторной попытки");
                            Console.ReadLine();
                        }
                    }
                    Console.WriteLine("Изменения сохранены, нажмите любую кнопку, чтобы продолжить");
                    Console.ReadLine();
                    MeetMenu();
                    break;

                case 4:
                    Console.WriteLine("Введите новый коментарий");
                    MeetList[editMeet].MeetingContent = Console.ReadLine();
                    Console.WriteLine("Изменения сохранены, нажмите любую кнопку, чтобы продолжить");
                    Console.ReadLine();
                    MeetMenu();
                    break;

                default:
                    Console.WriteLine("Номер недействительный, повторите ввод");
                    int.TryParse(Console.ReadLine(), out editNumber);
                    EditMeetSelecter(editNumber, editMeet);
                    break;
            }
        }

        private void ChooseMeetingDay()
        {
            List<Meet> currentMeetList = new();

            Console.WriteLine("Укажите дату в формате: \"dd/mm/yyyy\"");
            var isCorrectTime = false;

            while (!isCorrectTime)
            {
                try
                {
                    var inputTime = Console.ReadLine();
                    string format = "dd/MM/yyyy";
                    var currentTime = DateTime.ParseExact(inputTime, format, null);
                    var currentTimeEnd = currentTime.AddDays(1);
                    int number = 0;
                    Console.Clear();
                    foreach (var meet in MeetList)
                    {
                        if (meet.StartTime >= currentTime && meet.StartTime <= currentTimeEnd)
                        {
                            Console.WriteLine($"Встреча {number}");
                            meet.ShowMeetInfo();

                            currentMeetList.Add(meet);
                            
                            number++;
                        }
                    }

                    if (number == 0)
                    {
                        Console.WriteLine("заметок на указанный день нет, нажмите любую кнопку, чтобы вернуться в главное меню");
                        Console.ReadLine();
                        MeetMenu();
                        return;
                    }
                    isCorrectTime = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Некорректый ввод даты, нажмите любую кноку для повторной попытки");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("Введите номер действия\n0:вернуться в главное меню\n1:скачать заметку файлом");
            int.TryParse(Console.ReadLine(), out int currentActionNumber);

            MeetingDaySelecter(currentActionNumber, currentMeetList);
        }

        private void MeetingDaySelecter(int currentActionNumber, List<Meet> currentMeetList)
        {
            switch (currentActionNumber)
            {
                case 0:
                    MeetMenu();
                    break;

                case 1:
                    Console.WriteLine("Введите номер встречи");
                    int currentMeetNumber = 0;
                    do
                    {
                        int.TryParse(Console.ReadLine(), out currentMeetNumber);
                       
                        
                        if (currentMeetNumber < 0 || currentMeetNumber > currentMeetList.Count - 1)
                        {
                            Console.WriteLine("Некорректый номер встречи, нажмите любую кноку для повторной попытки");
                            Console.ReadLine();
                        }


                    } while (currentMeetNumber < 0 || currentMeetNumber > currentMeetList.Count - 1);

                    DownloadMeet(currentMeetList[currentMeetNumber]);
                    break;

                default:
                    Console.WriteLine("Номер недействительный, повторите ввод");
                    int.TryParse(Console.ReadLine(), out currentActionNumber);
                    MeetingDaySelecter(currentActionNumber, currentMeetList);
                    break;
            }
        }

        private void DownloadMeet(Meet meet)
        {
            string fileName = "Meet.txt";
            string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(directoryPath, fileName);

            string content =
                  $"Встреча {meet.Name}\n" +
                  $"Начало встречи в {meet.StartTime}\n" +
                  $"Конец встречи в {meet.EndTime}\n" +
                  $"Уведомление придет в {meet.NotificationTime}\n" +
                  $"Коментарий к встрече:\n{meet.MeetingContent}\n";

            File.WriteAllText(filePath, content);

            Console.WriteLine("Файл с заметкой создан в папку MyDocuments\nНажмите любую кнопку, чтобы продолжить");
            Console.ReadLine();
            MeetMenu();
        }
    }
}