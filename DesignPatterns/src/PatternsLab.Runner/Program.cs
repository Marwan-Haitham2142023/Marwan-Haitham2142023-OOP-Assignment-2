using System;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Builder;

Console.WriteLine("================================================");
Console.WriteLine("  Patterns Lab - Runner");
Console.WriteLine("================================================");
Console.WriteLine();

// ============ PROTOTYPE ============
Console.WriteLine("### PROTOTYPE PATTERN ###");
Console.WriteLine();

Console.WriteLine("1. Creating prototype (slow, once):");
var orcPrototype = new Orc();
Console.WriteLine($"   Prototype ModelId: {orcPrototype.ModelId}");
Console.WriteLine();

Console.WriteLine("2. Cloning (fast, no loading):");
var orc1 = orcPrototype.Clone();
var orc2 = orcPrototype.Clone();
Console.WriteLine($"   Clone 1 ModelId: {orc1.ModelId}");
Console.WriteLine($"   Clone 2 ModelId: {orc2.ModelId}");
Console.WriteLine();

Console.WriteLine("3. Same ModelId?");
Console.WriteLine($"   orc1 == prototype ModelId: {orc1.ModelId == orcPrototype.ModelId}");
Console.WriteLine($"   orc2 == prototype ModelId: {orc2.ModelId == orcPrototype.ModelId}");
Console.WriteLine();

Console.WriteLine("4. Deep copy test:");
orc1.Weapon.Name = "Sword";
orc1.Abilities.Add("Fire");
Console.WriteLine($"   Original weapon:    {orcPrototype.Weapon.Name}");
Console.WriteLine($"   Clone weapon:       {orc1.Weapon.Name}");
Console.WriteLine($"   Original abilities: {string.Join(", ", orcPrototype.Abilities)}");
Console.WriteLine($"   Clone abilities:    {string.Join(", ", orc1.Abilities)}");
Console.WriteLine();

Console.WriteLine("5. Registry test:");
var registry = new EnemyRegistry();
registry.Register("orc", orcPrototype);
registry.Register("elf", new Elf());

var r1 = registry.Get("orc");
var r2 = registry.Get("orc");
Console.WriteLine($"   Got orc from registry: {r1.Name}, weapon={r1.Weapon.Name}");
Console.WriteLine($"   Got another orc:       {r2.Name}, weapon={r2.Weapon.Name}");
Console.WriteLine($"   Same object? {ReferenceEquals(r1, r2)}");
Console.WriteLine();

Console.WriteLine();

// ============ BUILDER ============
Console.WriteLine("### BUILDER PATTERN ###");
Console.WriteLine();

Console.WriteLine("1. Live group registration:");
var live = RegistrationCallSites.CreateLiveStudent();
Console.WriteLine("   " + live);
Console.WriteLine();

Console.WriteLine("2. Videos-only registration:");
var video = RegistrationCallSites.CreateVideosOnly();
Console.WriteLine("   " + video);
Console.WriteLine();

Console.WriteLine("3. Validation tests:");
try
{
    new CourseRegistrationBuilder()
        .ForCourse("SEF-101")
        .AsVideosOnly()
        .Build();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("   Caught: " + ex.Message);
}

try
{
    new CourseRegistrationBuilder()
        .ForStudent("x@mail.com")
        .AsLiveGroup("")
        .Build();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("   Caught: " + ex.Message);
}

Console.WriteLine();

// ============ SINGLETON ============
Console.WriteLine("### SINGLETON PATTERN ###");
Console.WriteLine();

Console.WriteLine("1. First access (slow, once):");
var config1 = AppConfig.Instance;
Console.WriteLine($"   LoadCount: {AppConfig.LoadCount}");
Console.WriteLine();

Console.WriteLine("2. Second access (fast, no reload):");
var config2 = AppConfig.Instance;
Console.WriteLine($"   LoadCount: {AppConfig.LoadCount}");
Console.WriteLine();

Console.WriteLine("3. Same instance?");
Console.WriteLine($"   ReferenceEquals: {ReferenceEquals(config1, config2)}");
Console.WriteLine();

Console.WriteLine("4. Shared state test:");
config1.Theme = "Dark";
Console.WriteLine($"   config1.Theme = Dark");
Console.WriteLine($"   config2.Theme = {config2.Theme}");
Console.WriteLine();

Console.WriteLine("5. Services using singleton:");
var db = new DatabaseService();
var ui = new UiService();
db.Connect();
ui.Render();

Console.WriteLine();
Console.WriteLine("================================================");
Console.WriteLine("  Done.");
Console.WriteLine("================================================");
