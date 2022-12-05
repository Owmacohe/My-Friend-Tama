using UnityEngine;

public class ChatGenerator
{
    static readonly string[] firstNames = {
        "Aaliyah",
        "Aaron",
        "Abby",
        "Abigail",
        "Abraham",
        "Adam",
        "Addison",
        "Adrian",
        "Adriana",
        "Adrianna",
        "Aidan",
        "Aiden",
        "Alan",
        "Alana",
        "Alejandro",
        "Alex",
        "Alexa",
        "Alexander",
        "Alexandra",
        "Alexandria",
        "Alexia",
        "Alexis",
        "Alicia",
        "Allison",
        "Alondra",
        "Alyssa",
        "Amanda",
        "Amber",
        "Amelia",
        "Amy",
        "Ana",
        "Andrea",
        "Andres",
        "Andrew",
        "Angel",
        "Angela",
        "Angelica",
        "Angelina",
        "Anna",
        "Anthony",
        "Antonio",
        "Ariana",
        "Arianna",
        "Ashley",
        "Ashlyn",
        "Ashton",
        "Aubrey",
        "Audrey",
        "Austin",
        "Autumn",
        "Ava",
        "Avery",
        "Ayden",
        "Bailey",
        "Benjamin",
        "Bianca",
        "Blake",
        "Braden",
        "Bradley",
        "Brady",
        "Brandon",
        "Brayden",
        "Breanna",
        "Brendan",
        "Brian",
        "Briana",
        "Brianna",
        "Brittany",
        "Brody",
        "Brooke",
        "Brooklyn",
        "Bryan",
        "Bryce",
        "Bryson",
        "Caden",
        "Caitlin",
        "Caitlyn",
        "Caleb",
        "Cameron",
        "Camila",
        "Carlos",
        "Caroline",
        "Carson",
        "Carter",
        "Cassandra",
        "Cassidy",
        "Catherine",
        "Cesar",
        "Charles",
        "Charlotte",
        "Chase",
        "Chelsea",
        "Cheyenne",
        "Chloe",
        "Christian",
        "Christina",
        "Christopher",
        "Claire",
        "Cody",
        "Colby",
        "Cole",
        "Colin",
        "Collin",
        "Colton",
        "Conner",
        "Connor",
        "Cooper",
        "Courtney",
        "Cristian",
        "Crystal",
        "Daisy",
        "Dakota",
        "Dalton",
        "Damian",
        "Daniel",
        "Daniela",
        "Danielle",
        "David",
        "Delaney",
        "Derek",
        "Destiny",
        "Devin",
        "Devon",
        "Diana",
        "Diego",
        "Dominic",
        "Donovan",
        "Dylan",
        "Edgar",
        "Eduardo",
        "Edward",
        "Edwin",
        "Eli",
        "Elias",
        "Elijah",
        "Elizabeth",
        "Ella",
        "Ellie",
        "Emily",
        "Emma",
        "Emmanuel",
        "Eric",
        "Erica",
        "Erick",
        "Erik",
        "Erin",
        "Ethan",
        "Eva",
        "Evan",
        "Evelyn",
        "Faith",
        "Fernando",
        "Francisco",
        "Gabriel",
        "Gabriela",
        "Gabriella",
        "Gabrielle",
        "Gage",
        "Garrett",
        "Gavin",
        "Genesis",
        "George",
        "Gianna",
        "Giovanni",
        "Giselle",
        "Grace",
        "Gracie",
        "Grant",
        "Gregory",
        "Hailey",
        "Haley",
        "Hannah",
        "Hayden",
        "Hector",
        "Henry",
        "Hope",
        "Hunter",
        "Ian",
        "Isaac",
        "Isabel",
        "Isabella",
        "Isabelle",
        "Isaiah",
        "Ivan",
        "Jack",
        "Jackson",
        "Jacob",
        "Jacqueline",
        "Jada",
        "Jade",
        "Jaden",
        "Jake",
        "Jalen",
        "James",
        "Jared",
        "Jasmin",
        "Jasmine",
        "Jason",
        "Javier",
        "Jayden",
        "Jayla",
        "Jazmin",
        "Jeffrey",
        "Jenna",
        "Jennifer",
        "Jeremiah",
        "Jeremy",
        "Jesse",
        "Jessica",
        "Jesus",
        "Jillian",
        "Jocelyn",
        "Joel",
        "John",
        "Johnathan",
        "Jonah",
        "Jonathan",
        "Jordan",
        "Jordyn",
        "Jorge",
        "Jose",
        "Joseph",
        "Joshua",
        "Josiah",
        "Juan",
        "Julia",
        "Julian",
        "Juliana",
        "Justin",
        "Kaden",
        "Kaitlyn",
        "Kaleb",
        "Karen",
        "Karina",
        "Kate",
        "Katelyn",
        "Katherine",
        "Kathryn",
        "Katie",
        "Kayla",
        "Kaylee",
        "Kelly",
        "Kelsey",
        "Kendall",
        "Kennedy",
        "Kenneth",
        "Kevin",
        "Kiara",
        "Kimberly",
        "Kyle",
        "Kylee",
        "Kylie",
        "Landon",
        "Laura",
        "Lauren",
        "Layla",
        "Leah",
        "Leonardo",
        "Leslie",
        "Levi",
        "Liam",
        "Liliana",
        "Lillian",
        "Lilly",
        "Lily",
        "Lindsey",
        "Logan",
        "Lucas",
        "Lucy",
        "Luis",
        "Luke",
        "Lydia",
        "Mackenzie",
        "Madeline",
        "Madelyn",
        "Madison",
        "Makayla",
        "Makenzie",
        "Malachi",
        "Manuel",
        "Marco",
        "Marcus",
        "Margaret",
        "Maria",
        "Mariah",
        "Mario",
        "Marissa",
        "Mark",
        "Martin",
        "Mary",
        "Mason",
        "Matthew",
        "Max",
        "Maxwell",
        "Maya",
        "Mckenzie",
        "Megan",
        "Melanie",
        "Melissa",
        "Mia",
        "Micah",
        "Michael",
        "Michelle",
        "Miguel",
        "Mikayla",
        "Miranda",
        "Molly",
        "Morgan",
        "Mya",
        "Naomi",
        "Natalia",
        "Natalie",
        "Nathan",
        "Nathaniel",
        "Nevaeh",
        "Nicholas",
        "Nicolas",
        "Nicole",
        "Noah",
        "Nolan",
        "Oliver",
        "Olivia",
        "Omar",
        "Oscar",
        "Owen",
        "Paige",
        "Parker",
        "Patrick",
        "Paul",
        "Payton",
        "Peter",
        "Peyton",
        "Preston",
        "Rachel",
        "Raymond",
        "Reagan",
        "Rebecca",
        "Ricardo",
        "Richard",
        "Riley",
        "Robert",
        "Ruby",
        "Ryan",
        "Rylee",
        "Sabrina",
        "Sadie",
        "Samantha",
        "Samuel",
        "Sara",
        "Sarah",
        "Savannah",
        "Sean",
        "Sebastian",
        "Serenity",
        "Sergio",
        "Seth",
        "Shane",
        "Shawn",
        "Shelby",
        "Sierra",
        "Skylar",
        "Sofia",
        "Sophia",
        "Sophie",
        "Spencer",
        "Stephanie",
        "Stephen",
        "Steven",
        "Summer",
        "Sydney",
        "Tanner",
        "Taylor",
        "Thomas",
        "Tiffany",
        "Timothy",
        "Travis",
        "Trenton",
        "Trevor",
        "Trinity",
        "Tristan",
        "Tyler",
        "Valeria",
        "Valerie",
        "Vanessa",
        "Veronica",
        "Victor",
        "Victoria",
        "Vincent",
        "Wesley",
        "William",
        "Wyatt",
        "Xavier",
        "Zachary",
        "Zoe",
        "Zoey"
    };

    static readonly string[] messages = {
        "Where are you?",
        "This is boring",
        "What are you doing?",
        "Did you see that?",
        "You're not supposed to be here!",
        "Where can I find this place?",
        "Ew gross",
        "This is so spooky",
        "How did you get in?",
        "Pog",
        "Hey, what's up",
        "I finally caught you live!",
        "Wow!",
        "I've been here",
        "Is this place abandoned?",
        "Did I see something moving?",
        "I'm new, what's going on?",
        "I went to this mall as a kid",
        "Huh, weird",
        "It's so dark!",
        "Are you streaming tomorrow?",
        "What a weird place...",
        "Nope nope nope nope",
        "Was that a rat I saw?"
    };

    static readonly string[] evolveMessages = {
        "What's going on?",
        "What is that thing?",
        "That's so gross! I hate eggs",
        "It's going to become a Charizard",
        "It's so cute!",
        "I have a bad feeling about this",
        "What's that egg thing?",
        "Where can I buy that?"
    };

    static string RandomFirstName()
    {
        string firstName = "";

        string temp = firstNames[Random.Range(0, firstNames.Length)].ToLower();

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].Equals('o') && Random.Range(0, 5) == 0)
            {
                firstName += '0';
            }
            else if (temp[i].Equals('i') && Random.Range(0, 5) == 0)
            {
                firstName += '1';
            }
            else if (temp[i].Equals('e') && Random.Range(0, 5) == 0)
            {
                firstName += '3';
            }
            else if (temp[i].Equals('s') && Random.Range(0, 5) == 0)
            {
                firstName += '5';
            }
            else if (temp[i].Equals('b') && Random.Range(0, 5) == 0)
            {
                firstName += '8';
            }
            else if (temp[i].Equals('g') && Random.Range(0, 5) == 0)
            {
                firstName += '9';
            }
            else
            {
                firstName += Random.Range(0, 7) == 0 ? temp[i].ToString().ToUpper() : temp[i];   
            }

            if (Random.Range(0, 10) == 0)
            {
                string specialCharacters = "?<>_+=|~.-";
                firstName += specialCharacters[Random.Range(0, specialCharacters.Length)];
            }
        }

        return firstName;
    }

    public static string RandomName()
    {
        string name = RandomFirstName();

        if (Random.Range(0, 10) == 0)
        {
            string temp = RandomFirstName();
            name += "_" + temp.Substring(0, Random.Range(0, temp.Length));
        }

        if (Random.Range(0, 2) == 0)
        {
            for (int j = 0; j < Random.Range(1, 3); j++)
            {
                name += Random.Range(0, 9);
            }
        }
        else if (Random.Range(0, 5) == 0)
        {
            name = "xX_" + name + "_Xx";
        }
        
        return name;
    }

    public static string RandomMessage(bool evolve)
    {
        if (Random.Range(0, 10) == 0)
        {
            return "[DONATED $" + Random.Range(5, 100) + "]";
        }
        
        if (Random.Range(0, 10) == 0)
        {
            return "[SUBSCRIBED]";
        }

        string[] temp = evolve ? evolveMessages : messages;

        string message = temp[Random.Range(0, temp.Length)];

        if (Random.Range(0, 5) == 0)
        {
            message = message.ToLower();
        }
        else if (Random.Range(0, 10) == 0)
        {
            message = message.ToUpper();
        }

        return message;
    }
}