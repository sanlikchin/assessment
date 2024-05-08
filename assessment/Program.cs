using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

internal class Program
{


    //global variable
   readonly static List<string> CATEGORIES = new List<string>()
            {
                "Laptop","desktop","others(such as smartphone or drones)"
            };

    static int LaptopCounter = 0, DesktopCounter, othersCounter;

    static string topdevice;

    static float topexpensivedevice;

    static float totalvalueforsinurancecounter = 0;
   
    //method and function
    //check proceed function if user press <enter> the program will look or when the user press"x" the program stop and display summary
    static string CheckProceed()
    {
        while (true)
        {
            Console.WriteLine("press <Enter> to add another device or type 'X' to exit");
            string checkProceed = Console.ReadLine();

            checkProceed = checkProceed.ToUpper();

            if (checkProceed.Equals("") || checkProceed.Equals("X"))
            {
                
                return checkProceed;
            }

            DisplayErrorMessage(checkProceed);
        }
       
    }
    //checnkname method: Check the user name and capitalise it error if user enter invalid data
    static string CheckName()
    {

        while (true)
        {
            Console.WriteLine("enter the device name:\n");
            string name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
            {

                name = name[0].ToString().ToUpper() + name.Substring(1);
                return name;

            }
            DisplayErrorMessage("ERROR: You must enter a name");
        }

    }
    //check int method: chceck user integer when entered check if the integer is invalid
    static int CheckInt(string question, int min, int max)
    {
        while (true)
        {
            try
            {

                Console.WriteLine(question);

                int userInt = Convert.ToInt32(Console.ReadLine());

                if (userInt >= min && userInt <= max)
                {
                    return userInt;
                }
                DisplayErrorMessage($"ERROR: you must enter an integer between{min} and{max}");
            }
            catch
            {
                DisplayErrorMessage($"ERROR: you must enter an integer between{min} and{max}");
            }
        }
    }
    //checkfloat method: chceck float the user enter check if flaot is invalid
    static float CheckFloat(string question, float min, float max)
    {
        while (true)
        {
            try
            {

                Console.WriteLine(question);

                float userfloat = (float)Convert.ToDecimal(Console.ReadLine());

                if (userfloat >= min && userfloat <= max)
                {
                    return userfloat;
                }
                DisplayErrorMessage($"ERROR: you must enter an integer between{min} and{max}");
            }
            catch
            {
                DisplayErrorMessage($"ERROR: you must enter an integer between{min} and{max}");
            }
        }
    }
    //generatemenu method: loop the menu for the user to eneter again 
    static string GenerateMenu(string menuType, List<string> listData)
    {
        string menu = $"select the {menuType}:\n";
        for (int loop = 0; loop < listData.Count; loop++)
        {
            menu += $"{loop + 1}. {listData[loop]}\n";
        }
        return menu;
    }
    //displayer error message when user enter invalid data
    private static void DisplayErrorMessage(string Error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(Error);
        Console.ForegroundColor = ConsoleColor.White;

    }

   //calculate device insurnace for when the user enter data 
    static void DeviceInsurance()
    {
        string deviceName;
        int numberofdevices, category;
        float deviceCost;

        deviceName = CheckName();
        
        category = CheckInt(GenerateMenu("category", CATEGORIES), 1, 3);

        deviceCost = CheckFloat("Enter the device cost;\n",0, 100000000);

        numberofdevices = CheckInt("Enter the number of device used at school;\n", 0, 100000000);

        Console.WriteLine($"device name: {deviceName}\n" +
               $"cost of each device: {deviceCost}\n" +
               $"number of device use: {numberofdevices}\n" +
               $"device category: {category}");

        float insuranceCost = 0;
        if (numberofdevices > 5)
        {
            insuranceCost += 5 * deviceCost;
            insuranceCost += (numberofdevices - 5) * deviceCost * 0.9f;
        }
        else
        {
            insuranceCost += numberofdevices * deviceCost;
        }

        Console.WriteLine($"{deviceName}");
        Console.WriteLine($"total cost for {numberofdevices} x {deviceName} devices is equal to ${insuranceCost}");

        Console.WriteLine("month\t value loss");

        float depreciation = deviceCost;
        for (int month = 0; month < 6; month++)
        {
            depreciation = depreciation * 0.95f;
            Console.WriteLine($"{month + 1}\t{depreciation}");
        }
        // calculate the most expensive device 
        if (deviceCost > topexpensivedevice)
        {
            topexpensivedevice = deviceCost;
            topdevice = deviceName;

        }
         //count device category to each category they belong to 
       if (category.Equals (1))
        {
            LaptopCounter += numberofdevices;
        }
       else if (category.Equals(2))
        {
            DesktopCounter += numberofdevices;
        }
       else        
        {
            othersCounter += numberofdevices;
        }

       if (insuranceCost > 0)
        {
            totalvalueforsinurancecounter += insuranceCost; 
        }

        
    }

   
    //main process or when run
    //device display title
    static void Main(string[] args)
    {
        Console.WriteLine(
                ".__                                                                               \n" +
                "|__| ____   ________ ______________    ____   ____  ____   _____  ______ ______   \n" +
                "|  |/    \\ /  ___/  |  \\_  __ \\__  \\  /    \\_/ ___\\/ __ \\  \\__  \\ \\____ \\____ \\  \n" +
                "|  |   |  \\___  \\|  |  /|  | \\// __ \\|   |  \\  \\__\\  ___/   / __ \\|  |_> >  |_> > \n" +
                "|__|___|  /____  >____/ |__| " +
                " (____  /___|  /\\___  >___  > (____  /   __/|   __/  \n" +
                "        \\/     \\/                  \\/     \\/     \\/    \\/       \\/|__|   |__|     \n"
           );
       
        string proceed = "";
        while (proceed.Equals(""))
        {
            DeviceInsurance();

            proceed = CheckProceed();
        }
        //display device summary when user press "x" to exit 
        Console.WriteLine($"number of laptop is {LaptopCounter}"  );
        Console.WriteLine($"number of desktop is {DesktopCounter}");
        Console.WriteLine($"number of others is {othersCounter}");

        Console.WriteLine($"Most expensive device - {topdevice} ${topexpensivedevice}" );

        Console.WriteLine($"Total value for insurance  ${totalvalueforsinurancecounter}");

    

    }

}   








