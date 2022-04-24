using System.Runtime.Serialization.Formatters.Binary;

string binPath = @"c:\Users\Admin\Downloads\Students.dat";
string createPath = @"d:\___\";
var file = new FileInfo(binPath);

try
{
    if (file.Exists)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        using (var fs = new FileStream(binPath, FileMode.OpenOrCreate))
        {
            // Ошибка считывания файла!
            //var newPet = (List<Student>)formatter.Deserialize(fs);

            // Вместо этого создаём объект в памяти
            List<Student> studentsList = new List<Student>()
            {
                new Student
                {
                    Name = "Иван",
                    Group = "Группа 1",
                    DateOfBirth = new DateTime(1990, 01, 01)
                },
                new Student
                {
                    Name = "Василий",
                    Group = "Группа 2",
                    DateOfBirth = new DateTime(2000, 02, 02)
                },
                new Student
                {
                    Name = "Марья",
                    Group = "Группа 2",
                    DateOfBirth = new DateTime(1980, 03, 03)
                }
            };

            if (!Directory.Exists(createPath + "Students"))
                Directory.CreateDirectory(createPath + "Students");

            var studentsGroups = studentsList.GroupBy(x => x.Group)
                .Select(group => new { GroupName = group.Key, Groups = group.ToList() })
                .ToList();



            foreach (var studentGroup in studentsGroups)
            {

                if (!File.Exists(createPath + "Students\\" + studentGroup.GroupName + ".txt"))
                    using (FileStream fsStudentGroup = File.Create(createPath + "Students\\" + studentGroup.GroupName + ".txt"))
                    {
                        //you can use the filstream here to put stuff in the file if you want to
                    }
                //File.Create(createPath + "Students\\" + studentGroup.GroupName + ".txt");

                foreach (var student in studentGroup.Groups)
                    File.AppendAllText(createPath + "Students\\" + studentGroup.GroupName + ".txt",
                        $"{student.Name}, {student.DateOfBirth:dd.MM.yyyy}");
            }
        }
    }
    else
        Console.WriteLine($"Некорректный путь до файла '{binPath}'!");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка! {ex.Message}");
}

[Serializable]
class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public DateTime DateOfBirth { get; set; }

    //public Student(string name, string group, DateTime dateOfBirth)
    //{
    //    Name = name;
    //    Group = group;
    //    DateOfBirth = dateOfBirth;
    //}
}