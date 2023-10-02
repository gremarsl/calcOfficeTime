
internal class main

{
    enum FederalState : int
    {
        BadenWuerttemberg = 0,
        Bayern = 1,
        Berlin = 2,
        Brandenburg = 3,
        Bremen = 4,
        Hamburg = 5,
        Hessen = 6,
        MecklenburgVorpommern = 7,
        Niedersachsen = 8,
        NordrheinWestfalen = 9,
        RheinlandPfalz = 10,
        Saaland = 11,
        Sachsen = 12,
        SachsenAnhalt = 13,
        SchleswigHolstein = 14,
        Thueringen = 15
    }

    enum Month : int
    {
        Januar = 1,
        Februar = 2,
        März = 3,
        April = 4,
        Mai = 5,
        Juni = 6,
        Juli = 7,
        August = 8,
        September = 9,
        Oktober = 10,
        November = 11,
        Dezember = 12
    }

    enum NumOfDays : int
    {
        Januar = 31,
        Februar = 28,
        März = 31,
        April = 30,
        Mai = 31,
        Juni = 30,
        Juli = 31,
        August = 31,
        September = 30,
        Oktober = 31,
        November = 30,
        Dezember = 31
    }

    private static int federalState;

    private static void Main(string[] args)
    {

        // Inputs.
        int year = Int32.Parse(args[0]);
        int month = Int32.Parse(args[1]);
        federalState = Int32.Parse(args[2]);
        decimal portionOfOfficeHours = decimal.Parse(args[3]); //in percent
        decimal weeklyWorkingHours = decimal.Parse(args[4]);
        decimal numOfFreeDays = decimal.Parse(args[5]);
        decimal officeWorkingHoursAccomplished = decimal.Parse(args[6]);

        Console.WriteLine($"Program executed with:" +
            $"\nyear:                                        {year}" +
            $"\nmonth:                                       {month}" +
            $"\nfederal state:                               {federalState}" +
            $"\nPortion of office hours [%]:                 {portionOfOfficeHours}" +
            $"\nWeekly working hours [h]:                    {weeklyWorkingHours}" +
            $"\nNumber of Gleittage / Vacationdays [d]:      {numOfFreeDays}" +
            $"\nAlready accomplished working hours [h]:      {officeWorkingHoursAccomplished}"
            );


        decimal workingDays = CalcWorkingDays(year, month);

        // Calculate number of free days in this month
        int numOfHolidaysAreWeekdays = 0;
        CalcNumFixHolidaysInMonth(year, month, ref numOfHolidaysAreWeekdays);
        CalcNumVariableHolidaysInMonth(year, month, ref numOfHolidaysAreWeekdays);

        workingDays = workingDays - numOfHolidaysAreWeekdays - numOfFreeDays;

        decimal requiredOfficeWorkingHours = Math.Round((decimal)(workingDays * weeklyWorkingHours / 5 * portionOfOfficeHours / 100), 2);

        decimal officeWorkingHoursTodo = requiredOfficeWorkingHours - officeWorkingHoursAccomplished;

        Console.WriteLine($"\n \n \n++++++++++++++++++++++++++++++" +
          $"\nIn {year}-{month} you have:" +
        $"\nworkingDays:                   {workingDays}" +
        $"\nnumber of required hours:      {requiredOfficeWorkingHours}" +
        $"\nOffice hours left to do [h]:   {officeWorkingHoursTodo}" +
        $"\n++++++++++++++++++++++++++++++"
        );
    }

    private static decimal CalcWorkingDays(in int year, in int month)
    {
        DateTime first_of_month = new DateTime(year, month, 1);
        DayOfWeek dayOne = first_of_month.DayOfWeek;

        // Calculate number of working days in this month 
        int daysOfMonth = GetDaysOfMonth(year, month);

        int rest = daysOfMonth % 7;
        int numOfWeekendDays = (int)((daysOfMonth / 7) * 2);

        if (rest == 3)
        {
            switch (dayOne)
            {
                case (DayOfWeek.Thursday): numOfWeekendDays++; break;
                case (DayOfWeek.Friday): numOfWeekendDays = numOfWeekendDays + 2; break;
                case (DayOfWeek.Saturday): numOfWeekendDays = numOfWeekendDays + 2; break;
                case (DayOfWeek.Sunday): numOfWeekendDays++; break;
            }
        }

        if (rest == 2)
        {
            switch (dayOne)
            {
                case (DayOfWeek.Friday): numOfWeekendDays++; break;
                case (DayOfWeek.Saturday): numOfWeekendDays = numOfWeekendDays + 2; break;
                case (DayOfWeek.Sunday): numOfWeekendDays++; break;
            }
        }

        decimal workingDays = daysOfMonth - numOfWeekendDays;
        return workingDays;
    }

    private static DateTime[] GetFixHolidays(int year)
    {
        List<DateTime> fixHolidays = new List<DateTime>();

        // fixHolidays national wide
        fixHolidays.Add(new DateTime(year, 01, 01));
        fixHolidays.Add(new DateTime(year, 05, 01));
        fixHolidays.Add(new DateTime(year, 10, 03));
        fixHolidays.Add(new DateTime(year, 12, 25));
        fixHolidays.Add(new DateTime(year, 12, 26));

        switch ((FederalState)federalState)
        {
            case FederalState.BadenWuerttemberg:
                fixHolidays.Add(new DateTime(year, 01, 06));
                fixHolidays.Add(new DateTime(year, 11, 01));
                break;

            case FederalState.Bayern:
                fixHolidays.Add(new DateTime(year, 01, 06));
                fixHolidays.Add(new DateTime(year, 11, 01));
                fixHolidays.Add(new DateTime(year, 08, 15)); // TODO not in all kommunen - maria himmelfahrt
                break;

            case FederalState.Berlin:
                fixHolidays.Add(new DateTime(year, 03, 08)); // Frauentag
                break;

            case FederalState.Brandenburg:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Bremen:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Hamburg:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Hessen:
                break;

            case FederalState.MecklenburgVorpommern:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Niedersachsen:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.NordrheinWestfalen:
                fixHolidays.Add(new DateTime(year, 11, 01));
                break;

            case FederalState.RheinlandPfalz:
                fixHolidays.Add(new DateTime(year, 11, 01));
                break;

            case FederalState.Saaland:
                fixHolidays.Add(new DateTime(year, 08, 15));
                fixHolidays.Add(new DateTime(year, 11, 01));
                break;

            case FederalState.Sachsen:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.SachsenAnhalt:
                fixHolidays.Add(new DateTime(year, 01, 06));
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.SchleswigHolstein:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            case FederalState.Thueringen:
                fixHolidays.Add(new DateTime(year, 10, 31));
                break;

            default:
                Console.WriteLine("Unkown federal state. Please check your arguments.");
                break;
        }

        return fixHolidays.ToArray();
    }

    private static DateTime[] GetVariableHolidays(int year)
    {
        List<DateTime> variableHolidays = new List<DateTime>();

        DateTime easterSunday = CalcEasterSunday(year);
        int numberOfDayEasterSunday = easterSunday.DayOfYear;

        int karfriday = numberOfDayEasterSunday + -2;
        DateTime karFriday = new DateTime(year, 1, 1).AddDays(karfriday - 1);

        int eastermonday = numberOfDayEasterSunday + 1;
        DateTime easterMonday = new DateTime(year, 1, 1).AddDays(eastermonday - 1);

        // Calculate Christ Ascension
        int numberOfDayChristAscension = numberOfDayEasterSunday + 39;
        DateTime christAscension = new DateTime(year, 1, 1).AddDays(numberOfDayChristAscension - 1);

        // Calculate Pfingstmontag 
        int numberOfDayWhitMonday = numberOfDayEasterSunday + 50;
        DateTime whitmonday = new DateTime(year, 1, 1).AddDays(numberOfDayWhitMonday - 1);

        // Calculate Corpus Christi
        int numberOfDayCorpusChristi = numberOfDayEasterSunday + 60;
        DateTime corpusChristi = new DateTime(year, 1, 1).AddDays(numberOfDayCorpusChristi - 1);

        // Calculate Buss und Bettag
        DateTime endOfChurchCalendarYear = new DateTime(year, 11, 23);
        DayOfWeek endOfChurchCalendarYearDay = endOfChurchCalendarYear.DayOfWeek;

        int numberOfDayEndOfChurchCalendarYearDay = endOfChurchCalendarYear.DayOfYear;

        DateTime bussAndBettag = new DateTime(year, 1, 1);
        switch (endOfChurchCalendarYearDay)
        {
            case DayOfWeek.Thursday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 1);
                break;
            case DayOfWeek.Friday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 2);
                break;
            case DayOfWeek.Saturday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 3);
                break;
            case DayOfWeek.Sunday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 4);
                break;
            case DayOfWeek.Monday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 5);

                break;
            case DayOfWeek.Tuesday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 6);

                break;
            case DayOfWeek.Wednesday:
                bussAndBettag = new DateTime(year, 1, 1).AddDays(numberOfDayEndOfChurchCalendarYearDay - 7);

                break;
            default:
                Console.WriteLine("Unkown day for Buss and Bettag.");
                break;

        }
        // national wide
        variableHolidays.Add(karFriday);
        variableHolidays.Add(easterSunday);
        variableHolidays.Add(easterMonday);
        variableHolidays.Add(christAscension);
        variableHolidays.Add(whitmonday);



        switch ((FederalState)federalState)
        {
            case FederalState.BadenWuerttemberg:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.Bayern:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.Berlin:

                break;
            case FederalState.Brandenburg:

                break;
            case FederalState.Bremen:

                break;
            case FederalState.Hamburg:

                break;
            case FederalState.Hessen:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.MecklenburgVorpommern:
                break;
            case FederalState.Niedersachsen:

                break;
            case FederalState.NordrheinWestfalen:
                variableHolidays.Add(corpusChristi);


                break;
            case FederalState.RheinlandPfalz:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.Saaland:
                variableHolidays.Add(corpusChristi);

                break;
            case FederalState.Sachsen:
                variableHolidays.Add(bussAndBettag);

                break;
            case FederalState.SachsenAnhalt:

                break;
            case FederalState.SchleswigHolstein:

                break;
            case FederalState.Thueringen:

                break;

            default:
                Console.WriteLine("Unkown federal state. Please check your arguments.");
                break;
        }


        return variableHolidays.ToArray();
    }

    private static int GetDaysOfMonth(int year, int month)
    {
        // TODO in jedem case oder immer erst am ende?
        switch ((Month)month)
        {
            case Month.Januar:
                return (int)NumOfDays.Januar;

            case Month.Februar:
                if (DateTime.IsLeapYear(year))
                {
                    return (int)NumOfDays.Februar + 1;
                }
                else
                {
                    return (int)NumOfDays.Februar;
                }

            case Month.März:
                return (int)NumOfDays.März;
            case Month.April:
                return (int)NumOfDays.April;
            case Month.Mai:
                return (int)NumOfDays.Mai;
            case Month.Juni:
                return (int)NumOfDays.Juni;
            case Month.Juli:
                return (int)NumOfDays.Juli;
            case Month.August:
                return (int)NumOfDays.August;
            case Month.September:
                return (int)NumOfDays.September;
            case Month.Oktober:
                return (int)NumOfDays.Oktober;
            case Month.November:
                return (int)NumOfDays.November;
            case Month.Dezember:
                return (int)NumOfDays.Dezember;
            default:
                Console.WriteLine("Unkown month");
                return 0;
                break;
        }
    }

    private static void CalcNumFixHolidaysInMonth(in int year, in int month, ref int numOfHolidaysAreWeekdays)
    {
        DateTime[] fixHolidays = GetFixHolidays(year);

        foreach (DateTime holiday in fixHolidays)
        {
            if (holiday.Month == month && (holiday.DayOfWeek != DayOfWeek.Saturday) && holiday.DayOfWeek != DayOfWeek.Sunday)
            {
                numOfHolidaysAreWeekdays++;
            }
        }

    }
    private static void CalcNumVariableHolidaysInMonth(in int year, in int month, ref int numOfHolidaysAreWeekdays)
    {
        DateTime[] variableholidays = GetVariableHolidays(year);

        foreach (DateTime holiday in variableholidays)
        {
            if (holiday.Month == month && (holiday.DayOfWeek != DayOfWeek.Saturday) && holiday.DayOfWeek != DayOfWeek.Sunday)
            {
                numOfHolidaysAreWeekdays++;
            }
        }
    }

    private static DateTime CalcEasterSunday(int year)
    {
        int day = 0;
        int month = 0;

        int g = year % 19;
        int c = year / 100;
        int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
        int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

        day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
        month = 3;

        if (day > 31)
        {
            month++;
            day -= 31;
        }

        return new DateTime(year, month, day);
    }
}