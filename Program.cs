
using System.Text.Json;

namespace catregistry
{

    // CatRegistry class
    public class CatRegistry {

        private string file = @"catregistry.json"; // JSON-file
        private List<Cat> cats = new List<Cat>(); // List with cats

        // Read file
        public CatRegistry() {
            if(File.Exists(@"catregistry.json")==true) {
                string jsonString = File.ReadAllText(file);
                cats = JsonSerializer.Deserialize<List<Cat>>(jsonString);
            }
        }

        // Get cats
        public List<Cat> getCats() {
            return cats;
        }

        // Add new cat
        public Cat addCat(Cat cat) {
            cats.Add(cat);
            marshal();
            return cat;
        }

        // Delete cat
        public int deleteCat(int index) {
            cats.RemoveAt(index);
            marshal();
            return index;
        }

        // Serialize cats
        private void marshal() {
            var jsonString = JsonSerializer.Serialize(cats);
            File.WriteAllText(file, jsonString);
        }

    }


    // Cat class
    public class Cat {

        public string Name { get; set; } // Cat name
        public string Gender { get; set; } // Cat gender
        public string Age { get; set; } // Cat age
        public string Breed { get; set; } // Cat breed
        public string Room { get; set; } // Cat room
        public string StartDate { get; set; } // Boarding start date
        public string EndDate { get; set; } // Boarding end date
        public string PhoneNumber { get; set; } // Phone number to cat owner

    }


    // Program
    class Program {

        static void Main(string[] args) {

            CatRegistry catregistry = new CatRegistry();
            int i = 0;

            while(true) {
                Console.Clear(); Console.CursorVisible = false;

                // Print text & menu
                Console.WriteLine("~*~ Tassens Kattpensionat ~*~");
                Console.WriteLine("----------------------------- \n");
                Console.WriteLine("** MENY **");
                Console.WriteLine("[1] Visa gullig katt");
                Console.WriteLine("[2] Visa katt som jagar mus");
                Console.WriteLine("[3] Lägg till ny katt");
                Console.WriteLine("[4] Ta bort katt");
                Console.WriteLine("[X] Avsluta");
                
                bool showCats = true;
                if(showCats == true) {
                    Console.WriteLine("\n** KATTER ** \n------------");
                    i = 0;
                    foreach(Cat cat in catregistry.getCats()) {
                        Console.WriteLine("[" + i++ + "] " + cat.Name + " | " + cat.Gender + " | " + cat.Age + " år | " + cat.Breed + " | Rum: " + cat.Room);
                        Console.WriteLine("Start: " + cat.StartDate + " | Slut: " + cat.EndDate);
                        Console.WriteLine("Tel: " + cat.PhoneNumber + "\n");
                    }
                }
                if(showCats == false) {
                    Console.WriteLine("Inga katter att visa här.");
                }

                // Save key input in variable
                int input = (int) Console.ReadKey(true).Key;

                // Switch listening to input
                switch(input) {
                    case '1': // 1 - Show cute cat (text art)
                        Console.CursorVisible = true;

                        Console.WriteLine("           __..--''``---....___   _..._    __");
                        Console.WriteLine(" /// //_.-'    .-/'';  `        ``<._  ``.''_ `. / // /");
                        Console.WriteLine("///_.-' _..--.'_    	|                    `( ) ) // //");
                        Console.WriteLine("/ (_..-' // (< _     ;_..__               ; `' / ///");
                        Console.WriteLine(" / // // //  `-._,_)' // / ``--...____..-' /// / //");
                        Console.WriteLine("Visst är detta en gullig katt? :)");
                        string answer = Console.ReadLine();

                        break;
                    case '2': // 2 - Show another cute cat (text art)
                        Console.CursorVisible = true;
                        
                        Console.WriteLine("               )|._.,--....,'``.  ");
                        Console.WriteLine(" .b--.        /;   _.. |   _|  (`._ ,.");
                        Console.WriteLine("`=,-,-'~~~   `----(,_..'--(,_..'`-.;.'");
                        Console.WriteLine("Kommer den fånga musen?");
                        string answer2 = Console.ReadLine();

                        break;
                    case '3': // 3 - Add new cat
                        Console.CursorVisible = true;

                        // Get information about cat
                        Console.WriteLine("\nLÄGG TILL NY KATT \n-----------------");
                        Console.Write("Namn: "); // Name
                        string name = Console.ReadLine();
                        Console.Write("Kön (M/F): "); // Gender
                        string gender = Console.ReadLine();
                        if(gender == "M") { gender = "♂"; } if(gender == "F") { gender = "♀"; } // Convert gender to symbol
                        Console.Write("Ålder: "); // Age
                        string age = Console.ReadLine();
                        Console.Write("Ras: "); // Breed
                        string breed = Console.ReadLine();
                        Console.Write("Rum: "); // Room
                        string room = Console.ReadLine();
                        Console.Write("Startdatum: "); // Start date
                        string startdate = Console.ReadLine();
                        Console.Write("Slutdatum: "); // End date
                        string enddate = Console.ReadLine();
                        Console.Write("Ägarens telefonnummer: "); // Owner's phone number
                        string phonenumber = Console.ReadLine();

                        // Check if null
                        if(String.IsNullOrEmpty(name) || String.IsNullOrEmpty(gender)) {
                            Console.WriteLine("Du måste ange namn och kön på katten.");
                            string r = Console.ReadLine();
                            break;
                        }

                        if(String.IsNullOrEmpty(room) || String.IsNullOrEmpty(startdate) || String.IsNullOrEmpty(enddate) || String.IsNullOrEmpty(phonenumber)) {
                            Console.WriteLine("Du måste ange rum, start- och sluttdatum samt telefonnummer till ägare.");
                            string r = Console.ReadLine();
                            break;
                        }

                        else {
                            // Create new cat in registry
                            Cat obj = new Cat();
                            obj.Name = name; obj.Gender = gender; obj.Age = age; obj.Breed = breed;
                            obj.Room = room; obj.StartDate = startdate; obj.EndDate = enddate; obj.PhoneNumber = phonenumber;
                            catregistry.addCat(obj); // Add cat to list

                            // Break
                            break;
                        }
                    case '4': // 4 - Delete cat
                        Console.CursorVisible = true;
                        Console.Write("Ange index på katt du vill radera: "); // Index
                        string indexDel = Console.ReadLine();
                        Console.Write("Du vill radera katt med index " + indexDel + ". Stämmer detta (y/n)? "); // Control if correct index
                        string inp = Console.ReadLine();
                        if(inp == "y") {
                            catregistry.deleteCat(Convert.ToInt32(indexDel)); // Delete cat
                            break;
                        } else {
                            break;
                        }
                    case 88: // Press X
                        Environment.Exit(0);
                        break;
                }
                
            }

        }

    }

}