namespace Excel
{
    class Person
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public Person(string surname, string name, int age, string phoneNumber)
        {
            Surname = surname;
            Name = name;
            Age = age;
            PhoneNumber = phoneNumber;
        }
    }
}