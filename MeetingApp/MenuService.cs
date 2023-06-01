namespace MeetingApp
{
    public class MenuService
    {
        public MenuService()
        {
            int number = 0;

            Constans.AppList.ForEach((_) =>
            {
                Console.WriteLine(number + ":" + _);
                number++;
            });

            Console.WriteLine("Введите номер приложения");

            int currentAppNumber = 0;

            int.TryParse(Console.ReadLine(), out currentAppNumber);
            AppSelecter(currentAppNumber);
        }

        private void AppSelecter(int currentAppNumber)
        {
            switch (currentAppNumber)
            {
                case 0:
                    new MeetService();
                    break;

                default:
                    Console.WriteLine("Номер недействительный, повторите ввод");
                    int.TryParse(Console.ReadLine(), out currentAppNumber);
                    AppSelecter(currentAppNumber);
                    break;
            }
        }
    }
}