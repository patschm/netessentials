using ACME.DAL.EntityFramework;
using ACME.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

string constring = @"Server=.\sqlexpress;Database=acme;Trusted_Connection=True;MultipleActiveResultSets=true";
//CreateDatabase();
//PopulateDatabase();
//ModifyData();
//DeleteData();

QueriesExtension();

void QueriesExtension()
{
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    builder.LogTo(evt => Console.WriteLine(evt), LogLevel.Information);
    builder.UseSqlServer(constring);
    ACMEDbContext context = new ACMEDbContext(builder.Options);

    var query = context.People
        .Where(p => p.LastName.StartsWith("N"))
        .Select(p => new { p.FirstName, p.LastName });

    var query2 = context.People
        .Include(px=>px.Hobbies)
            .ThenInclude(ph=>ph.Hobby)
                //.ThenInclude(h=>h.People)
        .OrderBy(p => p.Age);//.Skip(1).Take(2);

    foreach(var p in query2)
    {
        Console.WriteLine($"{p.FirstName} {p.LastName} ({p.Age})");
        //context.Entry(p).Collection(px=>px.Hobbies).Load();
        foreach(var ph in p.Hobbies)
        {
            //context.Entry(ph).Reference(px=>px.Hobby).Load();
            Console.WriteLine($"\t{ph.Hobby?.Description}");
        }
    }

}

//bool StartsWithN(Person p)
//{
//    return p.LastName.StartsWith("N");
//}

void DeleteData()
{
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    builder.LogTo(evt => Console.WriteLine(evt), LogLevel.Information);
    builder.UseSqlServer(constring);
    ACMEDbContext context = new ACMEDbContext(builder.Options);

    Hobby? h1 = context.Hobbies?.FirstOrDefault(h=>h.Id == 1);
    Person p1 = new Person { FirstName = "Harrie", LastName = "Nak", Age = 42 };
    p1.Hobbies.Add(new PersonHobby { Person = p1, Hobby = h1 });
    context.People?.Add(p1);
    context.SaveChanges();

    Console.WriteLine("Press enter to delete");
    Console.ReadLine();

    context = new ACMEDbContext(builder.Options);
    var victim = context.People?.FirstOrDefault(p => p.FirstName == "Harrie");
    context.Remove(victim!);
    Console.WriteLine(context.Entry(victim!)?.State);

    context.SaveChanges();

}

void ModifyData()
{
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    builder.LogTo(evt => Console.WriteLine(evt), LogLevel.Information);
    builder.UseSqlServer(constring);
    ACMEDbContext context = new ACMEDbContext(builder.Options);

    var p1 = context.People?.FirstOrDefault(p => p.Id == 1);
    p1.FirstName = "Patrick";

    context.SaveChanges();
    Console.WriteLine(context.Entry(p1!)?.State);
    Console.WriteLine(context.Entry(p1!)?.CurrentValues[nameof(p1.FirstName)]);
    Console.WriteLine(context.Entry(p1!)?.OriginalValues[nameof(p1.FirstName)]);


}

void PopulateDatabase()
{
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    builder.LogTo(evt => Console.WriteLine(evt), LogLevel.Information);
    builder.UseSqlServer(constring);
    ACMEDbContext context = new ACMEDbContext(builder.Options);

    Hobby h1 = new Hobby { Description = "Zeilen" };
    Hobby h2 = new Hobby { Description = "Sigarenbandjes" };
    Hobby h3 = new Hobby { Description = "Breien" };

    Person p1 = new Person { FirstName = "Jan", LastName = "Pieters", Age = 42 };
    p1.Hobbies.Add(new PersonHobby { Person = p1, Hobby = h1 });
    p1.Hobbies.Add(new PersonHobby { Person = p1, Hobby = h2 });

    Person p2 = new Person { FirstName = "Marijke", LastName = "Hermsen", Age = 36 };
    p2.Hobbies.Add(new PersonHobby { Person = p2, Hobby = h1 });
    p2.Hobbies.Add(new PersonHobby { Person = p2, Hobby = h3 });

    context.People?.AddRange(p1, p2);

    var entry = context.Entry(p1).CurrentValues[nameof(p2.FirstName)];
    Console.WriteLine(context.Entry(p1).State);
    Console.WriteLine(entry);

    //context.SaveChanges();

}

void CreateDatabase()
{
    DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
    builder.LogTo(evt => Console.WriteLine(evt));
    builder.UseSqlServer(constring);

    ACMEDbContext context = new ACMEDbContext(builder.Options);
    context.Database.EnsureCreated();

}

Console.WriteLine("Gelukt!");
Console.ReadLine();