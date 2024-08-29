namespace MovieCardsAPI.DTOs
{
    public record DirectorForMovieDetailsDTO(
        int Id,
        string Name,
        string Email,
        DateOnly DateOfBirth
    );
}
