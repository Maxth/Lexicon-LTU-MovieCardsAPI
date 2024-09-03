namespace Application.Dtos.DirectorDtos
{
    public record DirectorForMovieDetailsDTO(
        int Id,
        string Name,
        string? Email,
        DateOnly DateOfBirth
    );
}
