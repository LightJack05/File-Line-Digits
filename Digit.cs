/// <summary>
/// Class that represents the digits within a line of the file. Can contain a last and first index.
/// </summary>
public class Digit : IEquatable<Digit>, IEquatable<char>
{
    public char Character { get; set; }
    public int FirstIndex { get; set; }
    public int LastIndex { get; set; }
    public Digit(char character)
    {
        Character = character;
    }
    public bool Equals(Digit? other)
    {
        return this.Character == other.Character;
    }

    public bool Equals(char other)
    {
        return this.Character == other;
    }
}