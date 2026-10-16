namespace Api.Modules.Doctors.Domain.ValueObjects;

public sealed record Speciality
{
    public static readonly Speciality Cardiologia  = new("cardiologia");
    public static readonly Speciality Pediatria  = new("pediatria");
    public static readonly Speciality Dermatologia = new("dermatologia");
    

    private static readonly Speciality[] All = [Cardiologia, Pediatria, Dermatologia];

    public string Value { get; }

    private Speciality(string value) 
        => Value = value;

    public static Speciality From(string value) =>
        All.FirstOrDefault(s => s.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
            ?? throw new ArgumentException($"Invalid speciality: {value}", nameof(value));

    public override string ToString() => Value;
}