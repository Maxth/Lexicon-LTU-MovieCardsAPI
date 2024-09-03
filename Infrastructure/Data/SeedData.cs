using Bogus;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SeedData
    {
        private static Faker faker = new Faker("en");
        private static Random rnd = new Random();

        public static async Task InitAsync(MovieCardsDbContext context)
        {
            //If data exists dont run again!!!
            if (await context.Director.AnyAsync())
            {
                return;
            }
            var directors = GenerateDirectors(20);
            await context.AddRangeAsync(directors);
            await context.SaveChangesAsync();
        }

        private static Director[] GenerateDirectors(int numberOfDirectors)
        {
            var directors = new Director[numberOfDirectors];
            var actors = GenerateActors(40);
            var genres = GenerateGenres(10);

            for (int i = 0; i < numberOfDirectors; i++)
            {
                DateOnly dob = faker.Date.BetweenDateOnly(
                    new DateOnly(1900, 01, 01),
                    new DateOnly(2010, 01, 01)
                );
                directors[i] = new Director()
                {
                    Name = faker.Name.FullName(),
                    DateOfBirth = dob,
                    Movie = GenerateMovies(
                        faker.Random.ArrayElements(actors, rnd.Next(3, 12)),
                        faker.Random.ArrayElements(genres, rnd.Next(1, 4))
                    ),
                    ContactInformation = new ContactInformation()
                    {
                        Email = faker.Internet.Email(),
                        PhoneNumber = faker.Phone.PhoneNumber(),
                    }
                };
            }

            return directors;
        }

        private static Genre[] GenerateGenres(int numberOfGenres)
        {
            HashSet<string> uniqueGenreNames = [];
            while (uniqueGenreNames.Count < numberOfGenres)
            {
                uniqueGenreNames.Add(faker.Music.Genre());
            }
            return uniqueGenreNames.Select(n => new Genre(n)).ToArray();
        }

        private static Movie[] GenerateMovies(Actor[] actors, Genre[] genres)
        {
            Movie[] movies = new Movie[rnd.Next(1, 6)];
            for (int i = 0; i < movies.Length; i++)
            {
                movies[i] = new Movie()
                {
                    Title = faker.Company.CatchPhrase(),
                    ReleaseDate = faker.Date.BetweenDateOnly(
                        new DateOnly(1900, 01, 01),
                        new DateOnly(2024, 01, 01)
                    ),
                    Rating = double.Round(faker.Random.Double(0, 10), 1),
                    Description = faker.Lorem.Sentence(8),
                    Actor = actors,
                    Genre = genres
                };
            }

            return movies;
        }

        private static Actor[] GenerateActors(int numberOfActors)
        {
            Actor[] actors = new Actor[numberOfActors];

            for (int i = 0; i < actors.Length; i++)
            {
                actors[i] = new Actor()
                {
                    Name = faker.Name.FullName(),
                    DateOfBirth = faker.Date.BetweenDateOnly(
                        new DateOnly(1900, 01, 01),
                        new DateOnly(2015, 01, 01)
                    )
                };
            }

            return actors;
        }
    }
}
