using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieCardsApi.Entities;

namespace MovieCardsApi.Data
{
    internal class SeedData
    {
        private static Faker faker = new Faker("en");
        private static Random rnd = new Random();

        internal static async Task InitAsync(MovieCardsDbContext context)
        {
            //If data exists dont run again!!!
            if (await context.Director.AnyAsync())
            {
                return;
            }
            var directors = generateDirectors(20);
            await context.AddRangeAsync(directors);
            await context.SaveChangesAsync();
        }

        private static Director[] generateDirectors(int numberOfDirectors)
        {
            var directors = new Director[numberOfDirectors];
            var actors = generateActors(40);
            var genres = generateGenres(10);

            for (int i = 0; i < numberOfDirectors; i++)
            {
                string fName = faker.Name.FirstName();
                string lName = faker.Name.LastName();
                DateOnly dob = faker.Date.BetweenDateOnly(
                    new DateOnly(1900, 01, 01),
                    new DateOnly(2010, 01, 01)
                );
                directors[i] = new Director()
                {
                    DateOfBirth = dob,
                    Name = $"{fName} {lName}",
                    Movie = generateMovies(
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

        private static Genre[] generateGenres(int numberOfGenres)
        {
            HashSet<string> uniqueGenreNames = [];
            while (uniqueGenreNames.Count < numberOfGenres)
            {
                uniqueGenreNames.Add(faker.Music.Genre());
            }
            return uniqueGenreNames.Select(n => new Genre() { Name = n }).ToArray();
        }

        private static Movie[] generateMovies(Actor[] actors, Genre[] genres)
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
                    Rating = faker.Random.Double(0, 5).ToString("0.0"),
                    Description = faker.Lorem.Sentence(8),
                    Actor = actors,
                    Genre = genres
                };
            }

            return movies;
        }

        private static Actor[] generateActors(int numberOfActors)
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
