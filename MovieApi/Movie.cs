namespace MovieApi;

public class Movie
{
    public string Name { get; }
    public string Description { get; }
    public int ReleaseYear { get; }
    public string Genre { get; }
    

    public Movie(string name, string description, int releaseYear, string genre)
    {
        Name = name;
        Description = description;
        ReleaseYear = releaseYear;
        Genre = genre;
    }

    protected bool Equals(Movie other)
    {
        return Name == other.Name && Description == other.Description && ReleaseYear == other.ReleaseYear && Genre == other.Genre;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Movie)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Description, ReleaseYear, Genre);
    }
}