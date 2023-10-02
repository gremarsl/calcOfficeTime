# CalcOfficeTime

This is a small program to calculate the remaining required office work hours for one dedicated month.
Since the vacation days influence the number of working days the program distinguishes between the federal state the person is employed.
Based on your inputs the program outputs the results within your console.

# Usage 

Call the calcOfficeTime.exe and pass seven arguments. 

1. Year in consideration (e.g. 2023)
2. Month to be calculated (e.g. 9 for September)
3. Federal State in Germany you are employed. Please pass the corresponding number as an argument 
      BadenWuerttemberg     = 0
      Bayern                = 1
      Berlin                = 2
      Brandenburg           = 3
      Bremen                = 4
      Hamburg               = 5
      Hessen                = 6
      MecklenburgVorpommern = 7
      Niedersachsen         = 8
      NordrheinWestfalen    = 9
      RheinlandPfalz        = 10
      Saaland               = 11
      Sachsen               = 12
      SachsenAnhalt         = 13
      SchleswigHolstein     = 14
      Thueringen            = 15
4. Portion of allowed / desired home office, unit: [%] (e.g. 33,3)
5. Working hours per week, unit: [h] (e.g. 40)
6. Number of Gleittage / vacation days within considered month, unit [d]( e.g. 3)
7. Already accomplished working hours in the office, unit [h] (e.g. 8)

# How to build stand-alone .exe: 

dotnet publish -r win10-x64 -p:PublishSingleFile=true --self-contained true

# Usage 

>'CalcOfficeWork.exe 2023 9 0 33,33 40 0 0
"Calculate and show the amount of hours I still have to work in September 2023 with 3 days of vacation (employed in The Länd) after 8h in the office.)
