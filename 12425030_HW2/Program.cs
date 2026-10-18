using System.Globalization;

namespace _12425030_HW2
{
    public class Person
    {
        private string name;
        private string address;
        private double salary;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        public Person(string name, string address, double salary)
        {
            this.name = name;
            this.address = address;
            this.salary = salary;
        }
    }
    internal class Program
    {
        public static Person inputPersonInfo(string name, string address, string sSalary)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("You must input name.");
            if (string.IsNullOrWhiteSpace(address))
                throw new Exception("You must input address.");
            if (string.IsNullOrWhiteSpace(sSalary))
                throw new Exception("You must input Salary.");

            string normalizedSalary = sSalary.Trim().Replace(',', '.');

            double salary;

            bool isNumber = double.TryParse(
                normalizedSalary,

                NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,

                CultureInfo.InvariantCulture,

                out salary);

            if (!isNumber || double.IsNaN(salary) || double.IsInfinity(salary))
                throw new Exception("You must input digit.");

            if (salary <= 0)
                throw new Exception("Salary is greater than zero");

            return new Person(name.Trim(), address.Trim(), salary);
        }

        public static void displayPersonInfo(Person person)
        {
            Console.WriteLine("Name: " + person.Name);

            Console.WriteLine("Address: " + person.Address);

            Console.WriteLine("Salary: " + person.Salary.ToString("0.##", CultureInfo.InvariantCulture));

            Console.WriteLine();
        }

        public static Person[] sortBySalary(Person[] person)
        {
            if (person == null || person.Length == 0)
                throw new Exception("Can't Sort Person");

            Person[] sortedPeople = (Person[])person.Clone();

            for (int i = 0; i < sortedPeople.Length - 1; i++)
            {
                for (int j = 0; j < sortedPeople.Length - 1 - i; j++)
                {
                    if (sortedPeople[j] == null || sortedPeople[j + 1] == null)
                        throw new Exception("Can't Sort Person");

                    if (sortedPeople[j].Salary > sortedPeople[j + 1].Salary)
                    {
                        Person temp = sortedPeople[j];

                        sortedPeople[j] = sortedPeople[j + 1];

                        sortedPeople[j + 1] = temp;
                    }
                }
            }
            return sortedPeople;
        }
        private static string ReadNonEmptyText(string prompt, string errorMessage)
        {
            while (true)
            {
                Console.Write(prompt);

                string value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();

                Console.WriteLine(errorMessage);
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            const int numberOfPeople = 3;

            Person[] people = new Person[numberOfPeople];

            Console.WriteLine("===== Management Person Program =====");

            Console.WriteLine();

            for (int i = 0; i < numberOfPeople; i++)
            {
                Console.WriteLine("Input Information of Person " + (i + 1));

                string name = ReadNonEmptyText("Please input name: ", "You must input name.");

                string address = ReadNonEmptyText("Please input address: ", "You must input address.");

                while (true)
                {
                    Console.Write("Please input salary: ");

                    string sSalary = Console.ReadLine();

                    try
                    {
                        people[i] = inputPersonInfo(name, address, sSalary);
                        break;
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("===== Information of Person you have entered =====");

            foreach (Person person in people)
            {
                displayPersonInfo(person);
            }

            try
            {
                Person[] sortedPeople = sortBySalary(people);

                Console.WriteLine("===== Person Information Sorted by Salary (Ascending) =====");

                foreach (Person person in sortedPeople)
                {
                    displayPersonInfo(person);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}