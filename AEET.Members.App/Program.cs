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
            var usersFile = Directory.EnumerateFiles("./", "ExtranetUsers*.csv").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(usersFile))
            {
                Console.WriteLine("No se ha podido encontrar el fichero de usuarios. Cancelando la operación. Pulse una tecla para salir.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            Console.WriteLine($"Se ha encontrado el fichero de usuarios {usersFile}");

            var membersFile = Directory.EnumerateFiles("./", "socio.csv").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(membersFile))
            {
                Console.WriteLine("No se ha podido encontrar el fichero de socios. Cancelando la operación. Pulse una tecla para salir.");
                Console.ReadKey();
                Environment.Exit(2);
            }

            Console.WriteLine($"Se ha encontrado el fichero de socios {membersFile}");

            Console.WriteLine($"Importando fichero de usuarios {usersFile}");
            var users = new List<User>();
            using (var reader = new StreamReader(usersFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                users = csv.GetRecords<User>().ToList();
                Console.WriteLine($"Se han importado {users.Count()} usuarios.");
            }

            Console.WriteLine($"Importando fichero de miembros {membersFile}");
            var members = new List<Member>();
            using (var reader = new StreamReader(membersFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                members = csv.GetRecords<Member>().ToList();
                Console.WriteLine($"Se han importado {members.Count()} socios");
            }

            var joined = UsersMembersJoiner.Join(users, members);

            using (var writer = new StreamWriter($"sociosAEET_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(joined.UsersMembers);
            }

            Console.WriteLine("Pulse cualquier tecla para terminar.");
            Console.ReadKey();            
        }
    }
}
