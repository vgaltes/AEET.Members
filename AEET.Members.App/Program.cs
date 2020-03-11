using System;
using System.IO;
using System.Linq;
using AEET.Members;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;

namespace AEET.Members.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string usersFile = GetUsersFile();
            string membersFile = GetMembersFile();

            List<User> users = GetUsers(usersFile);
            List<Member> members = GetMembers(membersFile);

            var result = UsersMembersJoiner.Join(users, members);

            WriteResultFile(result);
            WriteOnlyUsersWarningInformation(result);
            WriteOnlyMembersErrorInformation(result);

            Console.WriteLine("Pulse cualquier tecla para terminar.");
            Console.ReadKey();
        }

        private static void WriteOnlyMembersErrorInformation(UserMemberJoinResult result)
        {
            if (result.OnlyOnMembers.Any())
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Los siguientes usuarios exiten solo en el fichero de socios, no en el de usuarios.");
                result.OnlyOnMembers.ToList().ForEach(x => Console.WriteLine(x.Docidentidad));
                Console.ResetColor();
            }
        }

        private static void WriteOnlyUsersWarningInformation(UserMemberJoinResult result)
        {
            if (result.OnlyOnUsers.Any())
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Los siguientes usuarios exiten solo en el fichero de usuarios, no en el de socios.");
                result.OnlyOnUsers.ToList().ForEach(x => Console.WriteLine(x.docidentidad));
                Console.ResetColor();
            }
        }

        private static void WriteResultFile(UserMemberJoinResult result)
        {
            var fileName = $"sociosAEET_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.csv";
            Console.WriteLine($"Creando fichero {fileName}");

            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(result.UsersMembers);
            }
        }

        private static List<Member> GetMembers(string membersFile)
        {
            var members = new List<Member>();
            Console.WriteLine($"Importando fichero de miembros {membersFile}");
            using (var reader = new StreamReader(membersFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                members = csv.GetRecords<Member>().ToList();
                Console.WriteLine($"Se han importado {members.Count()} socios");
            }

            return members;
        }

        private static List<User> GetUsers(string usersFile)
        {
            var users = new List<User>();
            Console.WriteLine($"Importando fichero de usuarios {usersFile}");
            using (var reader = new StreamReader(usersFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                users = csv.GetRecords<User>().ToList();
                Console.WriteLine($"Se han importado {users.Count()} usuarios.");
            }

            return users;
        }

        private static string GetMembersFile()
        {
            var membersFile = Directory.EnumerateFiles("./", "socio.csv").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(membersFile))
            {
                Console.WriteLine("No se ha podido encontrar el fichero de socios. Cancelando la operación. Pulse una tecla para salir.");
                Console.ReadKey();
                Environment.Exit(2);
            }

            Console.WriteLine($"Se ha encontrado el fichero de socios {membersFile}");
            return membersFile;
        }

        private static string GetUsersFile()
        {
            var usersFile = Directory.EnumerateFiles("./", "ExtranetUsers*.csv").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(usersFile))
            {
                Console.WriteLine("No se ha podido encontrar el fichero de usuarios. Cancelando la operación. Pulse una tecla para salir.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            Console.WriteLine($"Se ha encontrado el fichero de usuarios {usersFile}");
            return usersFile;
        }
    }
}
