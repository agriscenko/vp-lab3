using Lab2.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Lab2.Console;

internal class Program
{
    static void Main(string[] args)
    {
        using var db = new DepartmentDbContext();

        db.Database.EnsureCreated();

        Linq1(db);
        Linq2(db);
        Linq3(db);
        Linq4(db);
        Linq5(db);
        Linq6(db);
        Linq7(db);
        Linq8(db);

        ReseedIds(db);
    }

    [Description("Izvadīt nodaļu skaitu pirmajā stāvā.")]
    private static void Linq1(DepartmentDbContext db)
    {
        var results = db.Departments.Where(d => d.FloorNumber == 1);

        System.Console.WriteLine($"Nodaļu skaits pirmajā stāvā: {results.Count()}.\n");
    }

    [Description("Izvadīt nodaļas nosaukumu ar ID=100, izmantojot FirstOrDefault().")]
    private static void Linq2(DepartmentDbContext db)
    {
        var department = db.Departments.FirstOrDefault(d => d.Id == 100);

        if (department != null)
        {
            System.Console.WriteLine($"Atrasta nodaļa: {department.Name}.\n");
        }
        else
        {
            System.Console.WriteLine("Neviena nodaļa ar ID=100 netika atrasta!\n");
        }
    }

    [Description("Pievienot vienu ierakstu tabulai Departments.")]
    private static void Linq3(DepartmentDbContext db)
    {
        var department = new Department
        {
            Name = "test",
            Code = "XX",
            FloorNumber = 1,
            Email = "test@example.com",
            PhoneNumber = "+371 67123000"
        };

        db.Departments.Add(department);

        db.SaveChanges();
    }

    [Description("Pievienot vairākus ierakstus.")]
    private static void Linq4(DepartmentDbContext db)
    {
        var departments = new List<Department>
        {
            new Department
            {
                Name = "Personāla nodaļa",
                Code = "HR",
                FloorNumber = 1,
                PhoneNumber = "+371 67123001",
                Email = "hr@example.com",
                Employees = new List<Employee>
                {
                    new Employee {
                        FirstName = "Anna",
                        LastName = "Bērziņa",
                        DateOfBirth = new DateOnly(1980, 8, 8),
                        PhoneNumber = "+371 99123100",
                        Email = "anna.berzina@example.com",
                        Position = "Personāla vadītāja",
                        Salary = 2500,
                        HireDate = new DateOnly(2020, 3, 12),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Jānis",
                        LastName = "Ozols",
                        DateOfBirth = new DateOnly(1995, 10, 20),
                        PhoneNumber = "+371 99123101",
                        Email = "janis.ozols@example.com",
                        Position = "Personāla speciālists",
                        Salary = 1800,
                        HireDate = new DateOnly(2022, 6, 9),
                        IsOnLeave = true,
                        Department = null
                    },
                    new Employee {
                        FirstName = "Liene",
                        LastName = "Kalniņa",
                        DateOfBirth = new DateOnly(1988, 5, 14),
                        PhoneNumber = "+371 99123102",
                        Email = "liene.kalnina@example.com",
                        Position = "Personāla asistente",
                        Salary = 1600,
                        HireDate = new DateOnly(2021, 9, 1),
                        Department = null
                    }
                }
            },
            new Department
            {
                Name = "Pārdošanas nodaļa",
                Code = "SL",
                FloorNumber = 1,
                PhoneNumber = "+371 67123002",
                Email = "sales@example.com",
                IsHiring = true,
                Employees = new List<Employee>
                {
                    new Employee {
                        FirstName = "Pēteris",
                        LastName = "Lejnieks",
                        DateOfBirth = new DateOnly(1985, 11, 23),
                        PhoneNumber = "+371 99123200",
                        Email = "peteris.lejnieks@example.com",
                        Position = "Pārdošanas vadītājs",
                        Salary = 2800,
                        HireDate = new DateOnly(2019, 4, 15),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Inese",
                        LastName = "Bērziņa",
                        DateOfBirth = new DateOnly(1992, 4, 10),
                        PhoneNumber = "+371 99123201",
                        Email = "inese.berzina@example.com",
                        Position = "Pārdošanas speciāliste",
                        Salary = 1700,
                        HireDate = new DateOnly(2020, 2, 20),
                        IsOnLeave = true,
                        Department = null
                    },
                    new Employee {
                        FirstName = "Gints",
                        LastName = "Kalniņš",
                        DateOfBirth = new DateOnly(1988, 7, 18),
                        PhoneNumber = "+371 99123202",
                        Email = "gints.kalnins@example.com",
                        Position = "Tirdzniecības menedžeris",
                        Salary = 2000,
                        HireDate = new DateOnly(2021, 5, 30),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Sandra",
                        LastName = "Bērziņa",
                        DateOfBirth = new DateOnly(1991, 3, 12),
                        PhoneNumber = "+371 99123203",
                        Email = "sandra.berzina@example.com",
                        Position = "Pārdošanas menedžere",
                        Salary = 1900,
                        HireDate = new DateOnly(2022, 1, 15),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Mārtiņš",
                        LastName = "Liepāns",
                        DateOfBirth = new DateOnly(1987, 8, 9),
                        PhoneNumber = "+371 99123204",
                        Email = "martins.liepans@example.com",
                        Position = "Tirdzniecības speciālists",
                        Salary = 1800,
                        HireDate = new DateOnly(2023, 3, 10),
                        IsOnLeave = true,
                        Department = null
                    }
                }
            },
            new Department
            {
                Name = "Mārketinga nodaļa",
                Code = "MK",
                FloorNumber = 1,
                PhoneNumber = "+371 67123003",
                Email = "marketing@example.com",
                Employees = new List<Employee>
                {
                    new Employee {
                        FirstName = "Ilze",
                        LastName = "Zariņa",
                        DateOfBirth = new DateOnly(1990, 2, 10),
                        PhoneNumber = "+371 99123300",
                        Email = "ilze.zarina@example.com",
                        Position = "Mārketinga vadītāja",
                        Salary = 2800,
                        HireDate = new DateOnly(2021, 7, 20),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Andris",
                        LastName = "Liepāns",
                        DateOfBirth = new DateOnly(1987, 3, 5),
                        PhoneNumber = "+371 99123301",
                        Email = "andris.liepan@example.com",
                        Position = "Mārketinga menedžeris",
                        Salary = 2200,
                        HireDate = new DateOnly(2019, 8, 15),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Sanda",
                        LastName = "Ozoliņa",
                        DateOfBirth = new DateOnly(1993, 9, 25),
                        PhoneNumber = "+371 99123302",
                        Email = "sanda.ozolina@example.com",
                        Position = "Reklāmas speciāliste",
                        Salary = 1800,
                        HireDate = new DateOnly(2020, 11, 10),
                        IsOnLeave = true,
                        Department = null
                    },
                    new Employee {
                        FirstName = "Jānis",
                        LastName = "Bērziņš",
                        DateOfBirth = new DateOnly(1988, 12, 3),
                        PhoneNumber = "+371 99123303",
                        Email = "janis.berzins@example.com",
                        Position = "Mārketinga speciālists",
                        Salary = 1900,
                        HireDate = new DateOnly(2021, 4, 22),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Līga",
                        LastName = "Kalniņa",
                        DateOfBirth = new DateOnly(1992, 5, 16),
                        PhoneNumber = "+371 99123304",
                        Email = "liga.kalnina@example.com",
                        Position = "Reklāmas menedžere",
                        Salary = 1800,
                        HireDate = new DateOnly(2022, 2, 14),
                        Department = null
                    }
                }
            },
            new Department
            {
                Name = "Grāmatvedības nodaļa",
                Code = "AC",
                FloorNumber = 2,
                PhoneNumber = "+371 67123004",
                Email = "accounting@example.com",
                Employees = new List<Employee>
                {
                    new Employee {
                        FirstName = "Andris",
                        LastName = "Bērziņš",
                        DateOfBirth = new DateOnly(1978, 12, 2),
                        PhoneNumber = "+371 99123400",
                        Email = "andris.berzins@example.com",
                        Position = "Galvenais grāmatvedis",
                        Salary = 2400,
                        HireDate = new DateOnly(2018, 5, 10),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Liga",
                        LastName = "Liepāne",
                        DateOfBirth = new DateOnly(1985, 4, 17),
                        PhoneNumber = "+371 99123401",
                        Email = "liga.liepane@example.com",
                        Position = "Grāmatvedības asistente",
                        Salary = 1800,
                        HireDate = new DateOnly(2019, 3, 25),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Mārtiņš",
                        LastName = "Kalniņš",
                        DateOfBirth = new DateOnly(1990, 6, 14),
                        PhoneNumber = "+371 99123402",
                        Email = "martins.kalnins@example.com",
                        Position = "Finanšu analītiķis",
                        Salary = 2400,
                        HireDate = new DateOnly(2020, 9, 10),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Dace",
                        LastName = "Kļaviņa",
                        DateOfBirth = new DateOnly(1987, 7, 22),
                        PhoneNumber = "+371 99123403",
                        Email = "dace.klavina@example.com",
                        Position = "Grāmatvedības speciāliste",
                        Salary = 1900,
                        HireDate = new DateOnly(2021, 1, 12),
                        IsOnLeave = true,
                        Department = null
                    },
                    new Employee {
                        FirstName = "Reinis",
                        LastName = "Bērziņš",
                        DateOfBirth = new DateOnly(1983, 10, 8),
                        PhoneNumber = "+371 99123404",
                        Email = "reinis.berzins@example.com",
                        Position = "Budžeta analītiķis",
                        Salary = 2100,
                        HireDate = new DateOnly(2022, 5, 19),
                        Department = null
                    }
                }
            },
            new Department
            {
                Name = "Klientu atbalsta nodaļa",
                Code = "CS",
                FloorNumber = 2,
                PhoneNumber = "+371 67123005",
                Email = "support@example.com",
                Employees = new List<Employee>
                {
                    new Employee {
                        FirstName = "Inese",
                        LastName = "Kļaviņa",
                        DateOfBirth = new DateOnly(1982, 9, 30),
                        PhoneNumber = "+371 99123500",
                        Email = "inese.klavina@example.com",
                        Position = "Klientu atbalsta vadītāja",
                        Salary = 2400,
                        HireDate = new DateOnly(2020, 11, 5),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Roberts",
                        LastName = "Bērziņš",
                        DateOfBirth = new DateOnly(1989, 1, 12),
                        PhoneNumber = "+371 99123501",
                        Email = "roberts.berzins@example.com",
                        Position = "Klientu atbalsta speciālists",
                        Salary = 1600,
                        HireDate = new DateOnly(2021, 2, 20),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Daina",
                        LastName = "Ozoliņa",
                        DateOfBirth = new DateOnly(1994, 4, 22),
                        PhoneNumber = "+371 99123502",
                        Email = "daina.ozolina@example.com",
                        Position = "Klientu apkalpošanas menedžere",
                        Salary = 1800,
                        HireDate = new DateOnly(2022, 5, 15),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Viktors",
                        LastName = "Bērziņš",
                        DateOfBirth = new DateOnly(1990, 2, 14),
                        PhoneNumber = "+371 99123503",
                        Email = "viktors.berzins@example.com",
                        Position = "Klientu atbalsta speciālists",
                        Salary = 1650,
                        HireDate = new DateOnly(2022, 8, 3),
                        IsOnLeave = true,
                        Department = null
                    },
                    new Employee {
                        FirstName = "Līga",
                        LastName = "Ozoliņa",
                        DateOfBirth = new DateOnly(1993, 9, 10),
                        PhoneNumber = "+371 99123504",
                        Email = "liga.ozolina@example.com",
                        Position = "Klientu apkalpošanas speciāliste",
                        Salary = 1700,
                        HireDate = new DateOnly(2023, 1, 20),
                        Department = null
                    }
                }
            },
            new Department
            {
                Name = "IT nodaļa",
                Code = "IT",
                FloorNumber = 3,
                PhoneNumber = "+371 67123006",
                Email = "it@example.com",
                IsHiring = true,
                Description = "Emails get lost. Tickets get answered. :)",
                Employees = new List<Employee>
                {
                    new Employee {
                        FirstName = "Edgars",
                        LastName = "Lapiņš",
                        DateOfBirth = new DateOnly(1975, 6, 5),
                        PhoneNumber = "+371 99123600",
                        Email = "edgars.lapins@example.com",
                        Position = "IT nodaļas vadītājs",
                        Salary = 3500,
                        HireDate = new DateOnly(2020, 4, 2),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Dace",
                        LastName = "Rudāne",
                        DateOfBirth = new DateOnly(1995, 8, 15),
                        PhoneNumber = "+371 99123601",
                        Email = "dace.rudane@example.com",
                        Position = "IT speciāliste",
                        Salary = 2500,
                        HireDate = new DateOnly(2023, 2, 3),
                        Department = null
                    },
                    new Employee {
                        FirstName = "Rihards",
                        LastName = "Veinbergs",
                        DateOfBirth = new DateOnly(1983, 3, 25),
                        PhoneNumber = "+371 99123602",
                        Email = "rihards.veinbergs@example.com",
                        Position = "Programmētājs",
                        Salary = 2800,
                        HireDate = new DateOnly(2024, 12, 18),
                        IsOnLeave = true,
                        Department = null
                    }
                }
            }
        };

        foreach (var department in departments)
        {
            if (department.Employees != null)
            {
                foreach (var employee in department.Employees)
                {
                    employee.Department = department;
                }
            }
        }

        db.Departments.AddRange(departments);

        db.SaveChanges();
    }

    [Description("Izdēst vienu ierakstu.")]
    private static void Linq5(DepartmentDbContext db)
    {
        var department = db.Departments.FirstOrDefault(d => d.Name == "test");

        if (department != null)
        {
            db.Departments.Remove(department);

            db.SaveChanges();
        }
    }

    [Description("Modificēt vienu ierakstu.")]
    private static void Linq6(DepartmentDbContext db)
    {
        var department = db.Departments.FirstOrDefault(d => d.Name == "Grāmatvedības nodaļa");

        if (department != null)
        {
            department.IsHiring = true;
            department.LastAuditDateTime = DateTime.Now;

            db.SaveChanges();

            System.Console.WriteLine($"Nosaukums: {department.Name}, Ir vakances: {(department.IsHiring ? "Jā" : "Nē")}, Audits veikts: {department.LastAuditDateTime}.\n");
        }
    }

    [Description("Izvadīt visus trešā stāva nodaļu nosaukumus un aprakstus, kā arī katras nodaļas darbiniekus un viņu amatus.")]
    private static void Linq7(DepartmentDbContext db)
    {
        var results = db.Departments
            .Include(d => d.Employees)
            .Where(d => d.FloorNumber == 3);

        System.Console.WriteLine("Dati par 3. stāva nodaļām un to darbiniekiem:");

        foreach (var department in results)
        {
            System.Console.WriteLine($"Nodaļa: {department.Name}, Apraksts: {department.Description}");

            foreach (var employee in department.Employees)
            {
                System.Console.WriteLine($"Darbinieks: {employee.FirstName} {employee.LastName}, Amats: {employee.Position}");
            }
        }
    }

    [Description("Izdēst visus ierakstus.")]
    private static void Linq8(DepartmentDbContext db)
    {
        var all_employees = db.Employees.ToList();
        var all_departments = db.Departments.ToList();

        db.RemoveRange(all_employees);
        db.RemoveRange(all_departments);

        db.SaveChanges();
    }

    private static void ReseedIds(DepartmentDbContext db)
    {
        var tables = new List<string> { "Departments", "Employees" };

        foreach (var table in tables)
        {
            var sql = $"DBCC CHECKIDENT ('{table}', RESEED, 0);";

            db.Database.ExecuteSqlRaw(sql);
        }
    }
}
